using System;

namespace ProtonRS485Client
{
    /// <summary>
    /// Класс - обработчик кусков данных, принятых с ком-порта
    /// </summary>
    class PackageDataDispatcher
    {
        private byte _mySlaveDeviceAddress; //заданный адрес этого устройства без бита поиска
        private byte _incomingAddress; //пришедший адрес с битом поиска 

        private byte[] _data;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mySlaveDeviceAddress">адрес этого устройства на шине. Для всех модулей оповещения 120, для ППКОП 1 - 15</param>
        public PackageDataDispatcher(byte mySlaveDeviceAddress)
        {
            _mySlaveDeviceAddress = mySlaveDeviceAddress;
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
            _incomingAddress = address;
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
            _data = new byte[length + 1]; //crc теперь тоже храним
            _data[0] = _incomingAddress;
            _data[1] = length;
            return true;
        }

        /// <summary>
        /// Обработчик самого пакета
        /// </summary>
        /// <param name="data">пакет</param>
        /// <returns>корректен ли пакет</returns>
        public bool ProcessPacket(byte[] data)
        {
            if (data.Length != data[1] - 1)
            {
                LogDispatcher.Write("Ошибка длины данных в ProcessPacket. Пришло " + data.Length + " байт, ожидалось " + (data[1] - 1) + " байт");
                return false;
            }
            data.CopyTo(_data, 2);
            //запишем пакет в лог
            DataLogDispatcher.Write("receive: " + BitConverter.ToString(_data).Replace("-", " "));
            //CRC от пакета с CRC на конце равна нулю, это свойство CRC
            if (PackageAlgs.GetCrc(_data) != 0)
            {
                LogDispatcher.Write("Ошибка CRC в пакете " + BitConverter.ToString(_data).Replace("-", " "));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Собранный пакет
        /// </summary>
        public byte[] Packet
        {
            get
            {
                return _data;
            }
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
        /// Проверка длины пкатена на корректность
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private bool IsLengthInRange(byte length)
        {
            return length >= 4 && length <= 14;
        }
    }
}
