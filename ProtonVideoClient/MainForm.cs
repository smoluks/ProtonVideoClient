﻿using System;
using System.Windows.Forms;
using ProtonRS485Client;
using System.IO;
using System.Drawing;
using ProtonRS485Client.Uart;

namespace ProtonVideoClient
{
    public partial class MainForm : Form
    {
        RS485Client RS485Library;
        Config config;
        ScenarioKernel scenarioKernel;

        public MainForm(string[] args)
        {
            //Открываем лог-файл
            Log.StartLog();
            //Открываем конфиг
            config = new Config();
            //Парсим параметры командной строки
            bool debug = false;
            foreach (string s in args)
            {
                switch (s)
                {
                    case "-windowedmode":
                        debug = true;
                        break;
                }
            }
            //Отрисовываем компоненты 
            InitializeComponent();
            //
            if (debug)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
            }
            //
            RS485Library = new RS485Client();
            scenarioKernel = new ScenarioKernel(config, this, RS485Library.SendMessage);
            //ProtonEvents.ConnectEvent += Connect;
            //ProtonEvents.ConnectEvent += Disconnect;
            //ProtonEvents.CommandEvent += scenarioKernel.ProcessCommand;
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Size s = SystemInformation.PrimaryMonitorSize;
            Player.Width = s.Width;
            Player.Height = s.Height;
            Player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(scenarioKernel.Player_StatusChange);
            //
            Error err = RS485Library.Connect(config.ComPort);
            switch (err)
            {
                case Error.PortAccessError:
                    ViewerShowError("Порт " + config.ComPort + " занят");
                    break;
                case Error.PortPathError:
                    ViewerShowError("Запись порта \"" + config.ComPort + "\" некорректна");
                    break;
                case Error.PortNotFoundError:
                    ViewerShowError("Порт " + config.ComPort + " не существует");
                    break;
                case Error.OtherError:
                    ViewerShowError("Неопознанная ошибка");
                    break;
                case Error.None:
                    ViewerShowError("Ожидаем подключения ППКОП");
                    break;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            RS485Library.Dispose();
            Log.CloseLog();
        }

        void Connect()
        {
            webBrowser.Invoke(new Action(() => ViewerHideError()));
        }

        void Disconnect()
        {
            webBrowser.Invoke(new Action(() => ViewerShowError("Ожидаем подключения ППКОП")));
        }

        private void WebRefreshTimer_Tick(object sender, EventArgs e)
        {
            webBrowser.Refresh();
        }

        private void проверкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestForm test = new TestForm(scenarioKernel.ProcessCommand);
            test.Show();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm(config);
            configForm.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ViewerHideError();
        }

        private void Player_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
        {
            contextMenuStrip1.Show();
        }

    }
}
