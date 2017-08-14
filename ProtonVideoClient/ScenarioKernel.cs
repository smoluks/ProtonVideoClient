﻿using ProtonRS485Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProtonVideoClient
{
    public delegate void SetMessageToSendDelegate(EObjectMessages command, byte arg);
    public class ScenarioKernel
    {
        MainForm mainForm;
        Config config;
        SetMessageToSendDelegate setMessageToSendDelegate;
        public ScenarioKernel(Config config, MainForm mainForm, SetMessageToSendDelegate setMessageToSendDelegate)
        {
            this.config = config;
            this.mainForm = mainForm;
            this.setMessageToSendDelegate = setMessageToSendDelegate;
        }

        Queue<Command> CommandsQueue = new Queue<Command>();

        /// <summary>
        /// Колбек от приемника команд
        /// </summary>
        public void ProcessCommand(byte command, byte arg, bool start)
        {
            Log.Write("Command " + (start ? "1-" : "3-") + command + ", arg" + arg);
            if (start)
            {
                Command c = new Command(command, arg);
                CommandsQueue.Enqueue(c);
                if (!isNoisePlaying)
                {
                    PlayNext();
                }
            }
            else
            {
                StopPlay();
            }
        }

        bool isNoisePlaying = false;
        bool noiseOnSent = false;


        public void Player_StatusChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (isNoisePlaying)
            {
                switch (e.newState)
                {
                    //stop
                    case 1:
                        if (!PlayNext())
                        {
                            StopPlay();
                        }
                        break;
                    //play
                    case 3:
                        if (!noiseOnSent)
                        {
                            setMessageToSendDelegate(EObjectMessages.NoiseOn, 6);
                            noiseOnSent = true;
                        }
                        break;
                }
            }
        }

        public bool PlayNext()
        {
            if (CommandsQueue.Count == 0)
                return false;
            //
            bool playSuccess = false;
            do
            {
                Command command = CommandsQueue.Dequeue();
                //поиск пути файла
                string path;
                if (command.command == 26)
                {
                    if (!config.FilePaths.ContainsKey(command.arg))
                    {
                        mainForm.Invoke(new Action(() => mainForm.ViewerShowError("Нет записи файла для сообщения: " + command.arg.ToString())));
                        break;
                    }
                    path = config.FilePaths[command.arg];
                }
                else if (command.command == 27)
                {
                    if (command.arg == 1)
                    {
                        path = config.SteadySiren;
                    }
                    else if (command.arg == 2)
                    {
                        path = config.WailSiren;
                    }
                    else if (command.arg == 3)
                    {
                        path = config.SilentTest;
                    }
                    else break;
                }
                else break;
                //поиск файла
                if (!File.Exists(path))
                {
                    mainForm.Invoke(new Action(() => mainForm.ViewerShowError("Файл не найден: " + path)));
                    break;
                }
                mainForm.Invoke(new Action(() => mainForm.ViewerPlayMessage(path)));
                playSuccess = true;
                isNoisePlaying = true;
            }
            while (!playSuccess && CommandsQueue.Count != 0);
            return playSuccess;
        }

        public void StopPlay()
        {
            Log.Write("Stop");
            mainForm.Invoke(new Action(() => mainForm.ViewerStopMessage()));
            CommandsQueue.Clear();
            isNoisePlaying = false;
            noiseOnSent = false;
            setMessageToSendDelegate(EObjectMessages.NoiseOff, 6);            
        }
    }
}
