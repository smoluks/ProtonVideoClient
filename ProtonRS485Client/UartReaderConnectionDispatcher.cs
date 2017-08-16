using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    enum ConnectionState
    {
        WrongAddress,
        Initiated,
        NotInitiated
    }
    class UartReaderConnectionDispatcher
    { 
        public ConnectionState Connect(UartReader uartReader, byte address)
        {
            if (!IsAddressCorrect(uartReader.DeviceAddress, address)) return ConnectionState.WrongAddress;
            var connectionState = ConnectionState.NotInitiated;
            uartReader.SlaveAddress = address;
            if (IsConnectionRequested(uartReader.DeviceAddress, address) && !uartReader.Connected)
            {
                connectionState = ConnectionState.Initiated;
                uartReader.Connected = true;
                //Событие на коннект
            }
            //Начинаем отсчет таймаута заново
            return connectionState;
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
