using ProtonRS485Client.Data;
using ProtonRS485Client.PackageCreate;
using ProtonRS485Client.Uart;
using System;
using System.Threading;

namespace ProtonRS485Client
{
    /// <summary>
    /// Основной класс проекта
    /// </summary>
    public class RS485ClientMain: IDisposable
    {
        UartDispatcher _uart = new UartDispatcher();
        PackageStateDispatcher _packageStateDispatcher;        

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logEnable">записывать обмен по RS485 в лог файл?</param>
        public RS485ClientMain()
        {
            LogDispatcher.Open("proton_rs485_library.log");
            LogDispatcher.Write("ProtonRS485Client started");        
        }

        public void Dispose()
        {
            _packageStateDispatcher.KillTask();
            _uart.Dispose();
            LogDispatcher.Write("ProtonRS485Client closed");
            LogDispatcher.Close();
        }

        public Error Connect(string port)
        {
            LogDispatcher.Write("ProtonRS485Client connect with port " + port);
            Error connectionResult = _uart.Connect(port);
            if (connectionResult == Error.None)
            {
                _packageStateDispatcher = new PackageStateDispatcher(_uart, new PackageDataDispatcher(), new PackageConnectDispatcher());
                CollectPacketsAsync();
                //
            }
            return connectionResult;
        }

        async void CollectPacketsAsync()
        {
            await _packageStateDispatcher.CollectPacketsAsync();
        }

        /// <summary>
        /// Отключение от протона
        /// </summary>
        public void Disconnect()
        {
            LogDispatcher.Write("ProtonRS485Client disconnect");
            _packageStateDispatcher.KillTask();           
            _uart.Dispose();
        }

        /// <summary>
        /// ставит сообщение на очередь в отправку
        /// </summary>
        public void SetMessageToSend(ProtonMessage message)
        {
            //_uartLevel.SetMessageToSend(command, arg);
            throw new Exception("Not implemented");
        }
    }
}
