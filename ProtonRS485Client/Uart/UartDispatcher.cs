using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace ProtonRS485Client.Uart
{
    /// <summary>
    /// ���� ������������ ������
    /// </summary>
    public enum Error { None = 0, PortPathError, PortAccessError, PortNotFoundError, OtherError };


    /// <summary>
    /// ���������� ������ � COM-������
    /// </summary>
    class UartDispatcher : IDisposable
    {
        private SerialPort _serialPort;
        private bool _connected;

        /// <summary>
        /// �������� �����
        /// </summary>
        /// <param name="port">COM-����</param>
        /// <returns>��� ������</returns>
        public Error Connect(string port)
        {
            try
            {
                _serialPort = new SerialPort(port, 19200, Parity.None, 8, StopBits.One);
                _serialPort.ReadTimeout = 1000;
                _serialPort.Open();
                _connected = true;
            }
            catch (Exception e)
            {
                LogDispatcher.WriteException(e);
                if (e is ArgumentException)
                    return Error.PortPathError;
                if (e is UnauthorizedAccessException)
                    return Error.PortAccessError;
                if (e is IOException)
                    return Error.PortNotFoundError;
                return Error.OtherError;
            }
            return Error.None;
        }

        /// <summary>
        /// �������� �����
        /// </summary>
        public void Disconnect()
        {
            if (_connected)
            {
                _serialPort.Close();
            }
        }

        public void Dispose()
        {
            Disconnect();
        }

        /// <summary>
        /// ������ �����
        /// </summary>
        /// <param name="count">����������</param>
        /// <returns>��������� ������</returns>
        public async Task<byte[]> ReadAsync(int count, CancellationToken token)
        {
            var buffer = new byte[count];
            await _serialPort.BaseStream.ReadAsync(buffer, 0, count, token);
            return buffer;
        }

        /// <summary>
        /// ������ ������ ����� �� �����
        /// </summary>
        /// <param name="count">����������</param>
        /// <returns>��������� ������</returns>
        public async Task<byte> ReadByteAsync(CancellationToken token)
        {
            var buffer = new byte[1];
            await _serialPort.BaseStream.ReadAsync(buffer, 0, 1, token);
            return buffer[0];
        }

        /// <summary>
        /// ������ � ����
        /// </summary>
        /// <param name="buffer">������ ��� ������ � ����</param>
        public async Task WriteAsync(byte[] buffer, CancellationToken token)
        {
            await _serialPort.BaseStream.WriteAsync(buffer, 0, buffer.Length, token);
        }

        /// <summary>
        /// ������ � ����
        /// </summary>
        /// <param name="data">������ ��� ������ � ����</param>
        public async Task WriteByteAsync(byte data, CancellationToken token)
        {
            await _serialPort.BaseStream.WriteAsync(new byte[] { data }, 0, 1, token);
        }
    }
}
