using ProtonRS485Client.PackageCreate;
using System;

namespace ProtonRS485Client.Data
{
    /// <summary>
    /// Класс протонопакета
    /// </summary>
    class Package
    {

        private byte[] packet = null;

        private byte address;
        private bool isAdressReceived = false;

        private byte length;
        private bool isLengthReceived = false;
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
                address = value;
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
                if (!isLengthReceived)
                    throw new Exception("Length not set");
                else
                    return length;
            }
            set
            {
                length = value;
            }
        }

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
                    packet = new byte[length + 2];
                    value.CopyTo(packet, 2);
                }
            }
        }

        public byte Crc
        {
            get
            {
                return PackageAlgs.GetCrc(packet);
            }
        }

        public byte[] GetPacket()
        {
            return packet;
        }
    }
}
