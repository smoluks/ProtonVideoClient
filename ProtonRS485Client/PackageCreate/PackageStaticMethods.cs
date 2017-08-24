using System.Collections.Generic;

//ProtonRS485Client.PackageCreate
namespace ProtonRS485Client.PackageCreate
{
    /// <summary>
    /// Всякие алгоритмы и другие заведомо неизменные вещи
    /// </summary>
    static class PackageStaticMethods
    {
        /// <summary>
        /// подсчет Dallas CRC8
        /// </summary>
        /// <param name="data">данные</param>
        /// <returns>CRC8</returns>
        public static byte GetCrc(IList<byte> data)
        {
            byte crc = 0;
            for (var index = 0; index < data.Count; index++)
            {
                var currentByte = data[index];
                for (byte bitCounter = 0; bitCounter < 8; bitCounter++)
                {
                    if (((crc ^ currentByte) & 0x01) != 0)
                    {
                        crc = (byte)((crc >> 1) ^ 0x8C);
                    }
                    else
                    {
                        crc >>= 1;
                    }
                    currentByte >>= 1;       
                }
            }
            return crc;
        }

        /// <summary>
        /// Контрольная сумма для команд
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte GetCommandCrc()
        {
            return 0;
        }

        /// <summary>
        /// Выделяет из адреса бит поиска
        /// </summary>
        /// <param name="address">адрес</param>
        /// <returns>мастер производит поиск этого устройства?</returns>
        public static bool isAddressInSearch(byte address)
        {
            return ((address & 0x80) == 0);
        }
    }
}
