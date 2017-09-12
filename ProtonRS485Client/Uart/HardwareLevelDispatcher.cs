using System.Threading;
using System.Threading.Tasks;

namespace ProtonRS485Client.Uart
{
    public interface HardwareLevelDispatcher
    {
        /// <summary>
        /// Чтение порта
        /// </summary>
        /// <param name="count">количество</param>
        /// <returns>считанные данные</returns>
        Task<byte[]> ReadAsync(int count, CancellationToken token);

        /// <summary>
        /// Чтение одного байта из порта
        /// </summary>
        /// <param name="count">количество</param>
        /// <returns>считанные данные</returns>
        Task<byte> ReadByteAsync(CancellationToken token);

        /// <summary>
        /// Запись в порт
        /// </summary>
        /// <param name="buffer">данные для записи в порт</param>
        Task WriteAsync(byte[] buffer, CancellationToken token);

        /// <summary>
        /// Запись в порт
        /// </summary>
        /// <param name="data">данные для записи в порт</param>
        Task WriteByteAsync(byte data, CancellationToken token);
    }
}
