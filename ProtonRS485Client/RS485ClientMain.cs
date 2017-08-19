using System;
using System.Threading;

namespace ProtonRS485Client
{    
    /// <summary>
    /// Основной класс проекта
    /// </summary>
    public class RS485ClientMain
    {
        UartDispatcher _uart = new UartDispatcher();
        ObjectConfig _objectConfig;
        PackageStateDispatcher _packageStateDispatcher;
        CancellationTokenSource _cancelTokenSource;
        CancellationToken _breakToken;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logEnable">записывать обмен по RS485 в лог файл?</param>
        public RS485ClientMain(bool logEnable)
        {
            LogDispatcher.OpenLogFile("proton_rs485_library.log");
            if (logEnable)
                DataLogDispatcher.OpenLogFile("proton_rs485_data.log");
            //
            _cancelTokenSource = new CancellationTokenSource();
            CancellationToken _breakToken = _cancelTokenSource.Token;
        }

        public void Destroy()
        {
            _cancelTokenSource.Cancel();
            _uart.Dispose();
            LogDispatcher.CloseLogFile();
            DataLogDispatcher.CloseLogFile();
        }

        public Err Connect(string port, ObjectConfig objectConfig)
        {
            _objectConfig = objectConfig;
            Err connectionResult = _uart.Connect(port);
            if (connectionResult == Err.noErr)
            {
                _packageStateDispatcher = new PackageStateDispatcher(_uart, new PackageDataDispatcher(_objectConfig.deviceAddress), new PackageConnectDispatcher(), _objectConfig, new ObjectState(), _breakToken);
                _packageStateDispatcher.StartCollect();
            }
            return connectionResult;
        }

        /// <summary>
        /// Отключение от протона
        /// </summary>
        public void Disconnect()
        {
            _cancelTokenSource.Cancel();
            _uart.Dispose();
        }

        /// <summary>
        /// ставит сообщение на очередь в отправку
        /// </summary>
        public void SetMessageToSend(Message message)
        {
            //_uartLevel.SetMessageToSend(command, arg);
            throw new Exception("Not implemented");
        }

        //События подключения и отключения мастера
        public delegate void ConnectionDelegate();
        public event ConnectionDelegate ConnectEvent;
        public event ConnectionDelegate DisconnectEvent;

        //Событие приема команды оповещения
        public delegate void CommandDelegate();
        public event CommandDelegate CommandEvent;
    }
}
