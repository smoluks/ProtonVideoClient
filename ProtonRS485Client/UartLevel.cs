using System;
using System.IO.Ports;
using System.Timers;

namespace ProtonRS485Client
{
    class UartLevel
    {
        private enum EComState {
            WaitCommand = 0,
            WaitLength,
            CollectData
        };

        private CommandLevel _commandLevel;
        private int _deviceAddress;
        private ProcessConnectionDelegate _processConnection;
        private bool _proton8IsConnected = false;
        private Timer _connectionTimeoutTimer;       
        private EComState _comState = EComState.WaitCommand;
        private byte _receivedSlaveAddress;
        private byte _lengthOfFrameValue;
        private byte _dataHandle;
        private byte[] _data;

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
            _connectionTimeoutTimer = new Timer(5000);
            _connectionTimeoutTimer.Enabled = true;
            _connectionTimeoutTimer.Elapsed += new ElapsedEventHandler(ConnectionTimeoutTimer_Elapsed);
            //
            _commandLevel = new CommandLevel(objectConfig, processCommand);
        }

        private void ConnectionTimeoutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {            
            _connectionTimeoutTimer.Stop();
            if(_proton8IsConnected)
            {
                _proton8IsConnected = false;
                _processConnection(false);
            }            
        }       

        private const byte addressMask = 0x7F;

        private bool IsCurrentAddress(byte inputByte)
        {
            return (inputByte & addressMask) == _deviceAddress;
        }

        /// <summary>
        /// Конечный автомат для входных байтов
        /// </summary>
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = (SerialPort)sender;
            while (serialPort.BytesToRead > 0)
            {
                var currentByte = (byte)serialPort.ReadByte();
                switch (_comState)
                {
                    case EComState.WaitCommand:
                        //адрес ведомого                    
                        if (IsCurrentAddress(currentByte))
                        {
                            _receivedSlaveAddress = currentByte;
                            _comState = EComState.WaitLength;
                            if (currentByte == (_deviceAddress | 0x80) && !_proton8IsConnected)
                            {
                                _proton8IsConnected = true;
                                _processConnection(true);
                            }
                            _connectionTimeoutTimer.Stop();
                            _connectionTimeoutTimer.Start();
                        }
                        break;
                    case EComState.WaitLength:
                        _lengthOfFrameValue = currentByte;
                        //длина фрейма
                        if (_lengthOfFrameValue < 4 || _lengthOfFrameValue > 14)
                            _comState = EComState.WaitCommand;
                        else
                        {
                            _data = new byte[_lengthOfFrameValue];
                            _data[0] = _receivedSlaveAddress;
                            _data[1] = _lengthOfFrameValue;
                            _dataHandle = 2;
                            _comState = EComState.CollectData;
                        }
                        break;
                    case EComState.CollectData:
                        //данные
                        if (_dataHandle < _lengthOfFrameValue)
                        {
                            _data[_dataHandle] = currentByte;
                            _dataHandle++;
                        }
                        else
                        {
                            //check crc
                            if (GetCrc(_data, 0, _data[1]) == currentByte)
                            {
                                Log.LogWrite("receive: " + BitConverter.ToString(_data).Replace("-", " "));
                                SendData(serialPort, _commandLevel.ProcessCommand(_data));
                            }
                            else Log.LogWrite("Bad CRC");
                            _comState = EComState.WaitCommand;
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
        public void SendData(SerialPort serialPort, byte[] data)
        {
            if (data == null)
                return;
            Log.LogWrite("answer: " + BitConverter.ToString(data).Replace("-", " "));
            serialPort.Write(data, 0, data.Length);
            serialPort.Write(new byte[] { GetCrc(data, 0, data.Length) }, 0, 1);
        }

        byte GetCrc(byte[] data, int offset, int length)
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
