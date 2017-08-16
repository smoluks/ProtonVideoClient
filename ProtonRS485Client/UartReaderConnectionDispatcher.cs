using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    class UartReaderConnectionDispatcher
    { 
        public bool Connect(UartReader uartReader, byte address)
        {
            if (!IsAddressCorrect(uartReader.DeviceAddress, address)) return false;
            uartReader.SlaveAddress = address;
            if (IsConnectionRequested(uartReader.DeviceAddress, address) && !uartReader.Connected)
            {
                uartReader.Connected = true;
                //Событие на коннект
            }
            //Начинаем отсчет таймаута заново
            return true;
        }

        private static bool IsAddressCorrect(int deviceAddress, byte input)
        {
            return (input & 0x7F) == deviceAddress;
        }

        private static bool IsConnectionRequested(int deviceAddress, byte input)
        {
            return input == (deviceAddress | 0x80);
        }     
    }
}
