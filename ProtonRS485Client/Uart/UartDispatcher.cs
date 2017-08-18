using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ProtonRS485Client
{
    /// <summary>
    /// Коды возвращаемых ошибок
    /// </summary>
    public enum Err { noErr = 0, PortPathError, PortAccessError, PortNotFoundError, OtherError };


    /// <summary>
    /// Реализация работы с COM-портом
    /// </summary>
    class UartDispatcher : IDisposable
    {
        private SerialPort _serialPort;
        private bool _connected;

        /// <summary>
        /// Открытие порта
        /// </summary>
        /// <param name="port">COM-порт</param>
        /// <returns>код ошибки</returns>
        public Err Connect(string port)
        {
            LogDispatcher.OpenLogFile("Connecting with port name: " + port);
            try
            {
                _serialPort = new SerialPort(port, 19200, Parity.None, 8, StopBits.One);
                _serialPort.ReadTimeout = 1000;
                _serialPort.Open();
                _connected = true;
            }
            catch (Exception e)
            {
                LogDispatcher.LogWriteException(e);
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
        /// Закрытие порта
        /// </summary>
        public void Disconnect()
        {
            if (_connected)
            {
                _serialPort.Close();
                LogDispatcher.OpenLogFile("Port " + _serialPort.PortName + " closed");
            }
            LogDispatcher.CloseLogFile();
        }

        public void Dispose()
        {
            Disconnect();
        }

        /// <summary>
        /// Чтение порта
        /// </summary>
        /// <param name="count">количество</param>
        /// <returns>считанные данные</returns>
        public async Task<byte[]> ReadAsync(int count, CancellationToken token)
        {
            var buffer = new byte[count];
            await _serialPort.BaseStream.ReadAsync(buffer, 0, count, token);
            return buffer;
        }

        /// <summary>
        /// Чтение одного байта из порта
        /// </summary>
        /// <param name="count">количество</param>
        /// <returns>считанные данные</returns>
        public async Task<byte> ReadByteAsync(CancellationToken token)
        {
            var buffer = new byte[1];
            await _serialPort.BaseStream.ReadAsync(buffer, 0, 1, token);
            return buffer[0];
        }

        /// <summary>
        /// Запись в порт
        /// </summary>
        /// <param name="buffer">данные для записи в порт</param>
        public async Task WriteAsync(byte[] buffer, CancellationToken token)
        {
            await _serialPort.BaseStream.WriteAsync(buffer, 0, buffer.Length, token);
        }
    }
}
