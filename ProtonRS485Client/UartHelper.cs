using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    static class UartHelper
    {
        public static byte GetCrc(IList<byte> data, int offset, int length)
        {
            byte crc = 0;
            for (var i = 0; i < length; i++)
            {
                var currentByte = data[i + offset];
                for (byte p = 0; p < 8; p++)
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
    }
}
