using System;

namespace ProtonRS485Client
{    
    /// <summary>
    /// Основной класс проекта
    /// </summary>
    public class RS485ClientMain : IDisposable
    {
        UartDispatcher _uart = new UartDispatcher();
        ObjectConfig _objectConfig = new ObjectConfig();
        PackageStateDispatcher _packageStateDispatcher;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logEnable">записывать обмен по RS485 в лог файл?</param>
        public RS485ClientMain(string port, bool logEnable)
        {
            LogDispatcher.OpenLogFile("proton_rs485_library.log");
            if (logEnable)
                DataLogDispatcher.OpenLogFile("proton_rs485_data.log");
            _uart.Connect(port);
            _packageStateDispatcher = new PackageStateDispatcher(_uart, new PackageDataDispatcher(_objectConfig.deviceAddress), new PackageConnectDispatcher(), _objectConfig, new ObjectState());
        }        

        /// <summary>
        /// Отключение от протона
        /// </summary>
        public void Dispose()
        {
            _uart.Dispose();
            LogDispatcher.CloseLogFile();
            DataLogDispatcher.CloseLogFile();
        }

        /*/// <summary>
        /// ставит сообщение на очередь в отправку
        /// </summary>
        public void SetMessageToSend(EObjectMessages command, byte arg)
        {
            _uartLevel.SetMessageToSend(command, arg);
        }*/
    }
}
