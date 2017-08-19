using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProtonRS485Client
{
    /// <summary>
    /// Основной класс проекта
    /// </summary>
    public class RS485ClientMain: IDisposable
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
        public RS485ClientMain()
        {
            LogDispatcher.Open("proton_rs485_library.log");
            LogDispatcher.Write("ProtonRS485Client started");
            //
            _cancelTokenSource = new CancellationTokenSource();
            _breakToken = _cancelTokenSource.Token;
        }

        public void Dispose()
        {           
            _cancelTokenSource.Cancel();
            _uart.Dispose();
            LogDispatcher.Write("ProtonRS485Client closed");
            LogDispatcher.Close();
        }

        public Err Connect(string port, ObjectConfig objectConfig)
        {
            LogDispatcher.Write("ProtonRS485Client connect with port " + port);
            _objectConfig = objectConfig;
            Err connectionResult = _uart.Connect(port);
            if (connectionResult == Err.noErr)
            {
                _packageStateDispatcher = new PackageStateDispatcher(_uart, new PackageDataDispatcher(_objectConfig.deviceAddress), new PackageConnectDispatcher(), _objectConfig, new ObjectState(), _breakToken);
                CollectPacketsAsync();
                //
            }
            return connectionResult;
        }

        async void CollectPacketsAsync()
        {
            LogDispatcher.Write("Start to CollectPacketsAsync");
            await _packageStateDispatcher.CollectPacketsAsync();
        }

        /// <summary>
        /// Отключение от протона
        /// </summary>
        public void Disconnect()
        {
            LogDispatcher.Write("ProtonRS485Client disconnect");
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
