using ProtonRS485Client.Data;
using System;

namespace ProtonRS485Client
{
    /// <summary>
    /// Класс - обработчик кусков данных, принятых с ком-порта
    /// </summary>
    /// Слишком много всего в одном классе
    /// 1) Вынести хранение
    /// 2) Вынести чтение
    public class PackageDataDispatcher
    {
        private byte _mySlaveDeviceAddress; //заданный адрес этого устройства без бита поиска
        public Package package { get; } //

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mySlaveDeviceAddress">адрес этого устройства на шине. Для всех модулей оповещения 120, для ППКОП 1 - 15</param>
        public PackageDataDispatcher()
        {
            _mySlaveDeviceAddress = ObjectConfig.DeviceAddress;
            package = new Package();
        }

        /// <summary>
        /// Обработчик пришедшего адреса
        /// </summary>
        /// <param name="address"></param>
        /// <returns>корректен ли адрес</returns>
        public bool ProcessAddress(byte address)
        {
            if (!IsAddressCorrect(address))
                //это не наш адрес - это норма, не нужно писать это в лог
                return false;
            package.Address = address;
            return true;
        }

        /// <summary>
        /// Обработчик пришедшей длины
        /// </summary>
        /// <param name="length">длина</param>
        /// <returns>корректна ли длина</returns>
        public bool ProcessFrameLength(byte length)
        {
            if (!IsLengthInRange(length))
                //длина побилась или не поддерживается - это тоже боле-менее норма, не нужно писать это в лог
                return false;
            package.Length = length;
            return true;
        }

        /// <summary>
        /// Обработчик самого пакета
        /// </summary>
        /// <param name="data">пакет</param>
        /// <returns>корректен ли пакет</returns>
        public bool ProcessPacket(byte[] data)
        {
            if (data.Length != package.Length - 2)
            {
                LogDispatcher.Write("Ошибка длины данных в ProcessPacket. Пришло " + data.Length + " байт, ожидалось " + (package.Length - 2) + " байт");
                return false;
            }
            package.Data = data;            
            return true;
        }

        public bool ProcessCRC(byte crc)
        {
            if (package.Crc != crc)
            {
                LogDispatcher.WriteData("Ошибка CRC в пакете ", package.GetPacket());
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверка адреса на соответствие нашему
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsAddressCorrect(byte input)
        {
            return (input & 0x7F) == _mySlaveDeviceAddress;
        }

        /// <summary>
        /// Проверка длины пакета на корректность
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private bool IsLengthInRange(byte length)
        {
            return length >= 4 && length <= 14;
        }
    }
}
