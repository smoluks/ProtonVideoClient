using ProtonRS485Client.PackageCreate;
using System;

namespace ProtonRS485Client.Data
{
    /// <summary>
    /// Класс протонопакета
    /// </summary>
    public class Package
    {
        private byte[] packet = null;

        private byte address = 0;
        private bool isAdressReceived = false;

        private byte length;
        private bool isLengthReceived = false;

        private bool isDataReceived = false;
        /// <summary>
        /// Адрес модуля, которому предназначается пакет
        /// </summary>
        public byte Address
        {
            get
            {
                if (!isAdressReceived)
                    throw new Exception("Address not set");
                else
                    return address;
            }
            set
            {
                if (!isDataReceived)
                    address = value;
                else
                    packet[0] = value;
                isAdressReceived = true;
            }
        }

        /// <summary>
        /// Длина пакета без crc
        /// </summary>
        public byte Length
        {
            get
            {
                //https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/property
                if (!isLengthReceived)
                    throw new Exception("Length not set");
                else
                    return length;
            }
            set
            {
                if (isLengthReceived)
                    throw new Exception("Length already set");
                else
                {
                    length = value;
                    isLengthReceived = true;
                }
            }
        }

        /// <summary>
        /// Блок данных пакета
        /// </summary>
        public byte[] Data
        {
            set
            {
                if (!isLengthReceived)
                    throw new Exception("Length not set");
                else if (value.Length != length - 2)
                    throw new Exception("Length not correct");
                else
                {
                    packet = new byte[length];
                    packet[0] = address;
                    packet[1] = length;
                    value.CopyTo(packet, 2);
                    isDataReceived = true;
                }
            }
        }

        /// <summary>
        /// CRC пакета
        /// </summary>
        public byte Crc
        {
            get
            {
                if (!isAdressReceived)
                    throw new Exception("Address not set");
                else if (!isLengthReceived)
                    throw new Exception("Length not set");
                else if (!isDataReceived)
                    throw new Exception("Data not set");
                else
                    return PackageStaticMethods.GetCrc(packet);
            }
        }

        /// <summary>
        /// Получить пакет
        /// </summary>
        /// <returns></returns>
        public byte[] GetPacket()
        {
            if (!isAdressReceived)
                throw new Exception("Address not set");
            else if (!isLengthReceived)
                throw new Exception("Length not set");
            else if (!isDataReceived)
                throw new Exception("Data not set");
            else
                return packet;
        }
    }
}
