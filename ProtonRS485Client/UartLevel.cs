using System;
using System.IO.Ports;
using System.Timers;

namespace ProtonRS485Client
{
    class UartLevel
    {
        CommandLevel _commandLevel;
        int _deviceAddress;
        ProcessConnectionDelegate _processConnection;
        bool Proton8IsConnected = false;
        Timer ConnectionTimeoutTimer;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="objectConfig">конфиг объекта</param>
        /// <param name="processCommand">колбек для команд оповещения</param>
        /// <param name="processConnection">колбек для контроля связи с протоном</param>
        public UartLevel(SObjectConfig objectConfig, ProcessCommandDelegate processCommand, ProcessConnectionDelegate processConnection)
        {
            _deviceAddress = objectConfig.deviceAddress;
            _processConnection = processConnection;
            //таймер контроля связи с протоном
            ConnectionTimeoutTimer = new Timer(5000);
            ConnectionTimeoutTimer.Enabled = true;
            ConnectionTimeoutTimer.Elapsed += new ElapsedEventHandler(ConnectionTimeoutTimer_Elapsed);
            //
            _commandLevel = new CommandLevel(objectConfig, processCommand);
        }

        private void ConnectionTimeoutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {            
            ConnectionTimeoutTimer.Stop();
            if(Proton8IsConnected)
            {
                Proton8IsConnected = false;
                _processConnection(false);
            }            
        }

        enum EComState { WaitCommand = 0, WaitLength, CollectData };
        EComState comState = EComState.WaitCommand;
        byte receivedSlaveAddress;
        byte lengthOfFrameValue;
        byte dataHandle;
        byte[] data;        

        /// <summary>
        /// Конечный автомат для входных байтов
        /// </summary>
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            while (sp.BytesToRead > 0)
            {
                byte newByte = (byte)sp.ReadByte();
                switch (comState)
                {
                    case EComState.WaitCommand:
                        //адрес ведомого                    
                        if ((newByte & 0x7F) == _deviceAddress)
                        {
                            receivedSlaveAddress = newByte;
                            comState = EComState.WaitLength;
                            if (newByte == (_deviceAddress | 0x80) && !Proton8IsConnected)
                            {
                                Proton8IsConnected = true;
                                _processConnection(true);
                            }
                            ConnectionTimeoutTimer.Stop();
                            ConnectionTimeoutTimer.Start();
                        }
                        break;
                    case EComState.WaitLength:
                        lengthOfFrameValue = newByte;
                        //длина фрейма
                        if (lengthOfFrameValue < 4 || lengthOfFrameValue > 14)
                            comState = EComState.WaitCommand;
                        else
                        {
                            data = new byte[lengthOfFrameValue];
                            data[0] = receivedSlaveAddress;
                            data[1] = lengthOfFrameValue;
                            dataHandle = 2;
                            comState = EComState.CollectData;
                        }
                        break;
                    case EComState.CollectData:
                        //данные
                        if (dataHandle < lengthOfFrameValue)
                        {
                            data[dataHandle] = newByte;
                            dataHandle++;
                        }
                        else
                        {
                            //check crc
                            if (crc(data, 0, data[1]) == newByte)
                            {
                                Log.LogWrite("receive: " + BitConverter.ToString(data).Replace("-", " "));
                                senddata(sp, _commandLevel.ProcessCommand(data));
                            }
                            else Log.LogWrite("Bad CRC");
                            comState = EComState.WaitCommand;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Отправка ответа в rs485
        /// </summary>
        /// <param name="serialPort">порт</param>
        /// <param name="data">данные</param>
        public void senddata(SerialPort serialPort, byte[] data)
        {
            if (data == null)
                return;
            Log.LogWrite("answer: " + BitConverter.ToString(data).Replace("-", " "));
            serialPort.Write(data, 0, data.Length);
            serialPort.Write(new byte[] { crc(data, 0, data.Length) }, 0, 1);
        }

        byte crc(byte[] data, int offset, int length)
        {
            byte crc = 0;
            for (int i = 0; i < length; i++)
            {
                byte b = data[i + offset];
                for (byte p = 0; p < 8; p++)
                {
                    if (((crc ^ b) & 0x01) != 0)
                    {
                        crc = (byte)((crc >> 1) ^ 0x8C);
                    }
                    else
                    {
                        crc >>= 1;
                    }
                    b >>= 1;

                }
            }
            return crc;
        }
        public void SetMessageToSend(EObjectMessages command, byte arg)
        {
            _commandLevel.SetMessageToSend(command, arg);
        }

    }
}
