using ProtonRS485Client.Data;
using ProtonRS485Client.PackageCreate;
using ProtonRS485Client.Uart;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    /// <summary>
    /// Основной класс проекта
    /// </summary>
    public class RS485Client : IDisposable
    {
        UartDispatcher _uart = new UartDispatcher();
        PackageStateDispatcher _packageStateDispatcher;
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logEnable">записывать обмен по RS485 в лог файл?</param>
        public RS485Client()
        {
            LogDispatcher.Open("proton_rs485_library.log");
            LogDispatcher.Write("ProtonRS485Client started");
        }

        /// <summary>
        /// Начать обмен с мастером
        /// </summary>
        /// <param name="port">Com-порт, к которому подключен мастер</param>
        /// <returns>ошибка, возникшая при подключении</returns>
        public Error Connect(string port)
        {
            LogDispatcher.Write("ProtonRS485Client connect with port " + port);
            //
            Error connectionResult = _uart.Connect(port);
            if (connectionResult != Error.None)
                return connectionResult;
            //
            _packageStateDispatcher = new PackageStateDispatcher(_uart, new PackageDataDispatcher(), new PackageConnectDispatcher());
            CollectPacketsAsync();
            //
            return Error.None;
        }

        async void CollectPacketsAsync()
        {
            await _packageStateDispatcher.CollectPacketsAsync();
        }

        /// <summary>
        /// Прекратить обмен с мастером
        /// </summary>
        public void Disconnect()
        {
            _packageStateDispatcher.KillTask();
            _uart.Dispose();
            LogDispatcher.Write("ProtonRS485Client disconnect");
        }

        /// <summary>
        /// Деструктор
        /// </summary>
        public void Dispose()
        {
            _packageStateDispatcher.KillTask();
            _uart.Dispose();
            LogDispatcher.Write("ProtonRS485Client closed");
            LogDispatcher.Close();
        }

        /// <summary>
        /// читает команду с объекта
        /// </summary>
        /// <returns></returns>
        public Task<ProtonMessage> ReadCommand()
        {
            return new Task<ProtonMessage>(() =>
            {
                ProtonMessage command;
                while (ExternalDataContract.CommandQueue.TryDequeue(out command)) { };
                return command;
            }
            );
        }

        /// <summary>
        /// Cтавит объектовое сообщение на очередь в отправку
        /// </summary>
        public void SendMessage(ProtonMessage message)
        {
            ExternalDataContract.MessageBuffer.Enqueue(message);
        }

        /// <summary>
        /// Флаг, подключился ли протон-8
        /// </summary>
        public bool MasterIsConnected
        {
            get
            {
                return ExternalDataContract.IsMasterConnect;
            }
        }
    }
}
