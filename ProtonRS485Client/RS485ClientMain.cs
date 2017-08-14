using System;
using System.IO;
using System.IO.Ports;

namespace ProtonRS485Client
{
    /// <summary>
    /// Коды возвращаемых ошибок
    /// </summary>
    public enum Err { noErr = 0, PortPathError, PortAccessError, PortNotFoundError, OtherError };

    /// <summary>
    /// Колбек для обработки команд оповещения
    /// </summary>
    /// <param name="command">номер команды</param>
    /// <param name="arg">номер аргумента</param>
    /// <param name="start">true - запуск команды, иначе останов</param>
    public delegate void ProcessCommandDelegate(byte command, byte arg, bool start);

    /// <summary>
    /// Колбек для обработки подключения/отключения протона
    /// </summary>
    /// <param name="connect">true - подключение, иначе отключение</param>
    public delegate void ProcessConnectionDelegate(bool connect);

    public class RS485ClientMain : IDisposable
    {
        SerialPort _serialPort;
        UartLevel _uartLevel;
        bool connected = false;

       /// <summary>
       /// Конструктор
       /// </summary>
       /// <param name="logEnable">записывать обмен по RS485 в лог файл?</param>
        public RS485ClientMain(bool logEnable)
        {
            if (logEnable)
                Log.OpenLogFile();
        }

        /// <summary>
        /// Подключение к протону
        /// </summary>
        /// <param name="devpath">COM-порт</param>
        /// <param name="objectNumber">Объектовый номер</param>
        /// <param name="processCommand">Колбек для команд</param>
        /// <param name="processConnection">Колбек контроля связи с протоном</param>
        /// <returns>код ошибки</returns>
        public Err Connect(string devpath, ushort objectNumber, ProcessCommandDelegate processCommand, ProcessConnectionDelegate processConnection)
        {
            Log.LogWrite("Connecting with port: " + devpath);
            SObjectConfig objectConfig = new SObjectConfig(objectNumber);
            _uartLevel = new UartLevel(objectConfig, processCommand, processConnection);
            try
            {
                _serialPort = new SerialPort(devpath, 19200, Parity.None, 8, StopBits.One);
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(_uartLevel.DataReceivedHandler);
                _serialPort.ReadTimeout = 1000;
                _serialPort.Open();
                connected = true;
            }
            catch (Exception e)
            {
                Log.LogWriteException(e);
                if (e is ArgumentException)
                    return Err.PortPathError;
                if (e is UnauthorizedAccessException)
                    return Err.PortAccessError;
                if (e is IOException)
                    return Err.PortNotFoundError;
                return Err.OtherError;
            }
            return Err.noErr;
        }

        /// <summary>
        /// Отключение от протона
        /// </summary>
        public void Dispose()
        {
            if (connected)
            {
                _serialPort.Close();
                Log.LogWrite("Port " + _serialPort.PortName + " closed");
            }
            Log.CloseLogFile();
        }

        /// <summary>
        /// ставит сообщение на очередь в отправку
        /// </summary>
        public void SetMessageToSend(EObjectMessages command, byte arg)
        {
            _uartLevel.SetMessageToSend(command, arg);
        }
    }
}
