using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    static class UartHelper
    {
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
    }
}
