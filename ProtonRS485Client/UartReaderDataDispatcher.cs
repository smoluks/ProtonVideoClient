using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    class UartReaderDataDispatcher
    {       
        private const byte DataHandleInitialValue = 2;
        public void InitializeDataArray(UartReader uartReader)
        {
            uartReader.Data = new byte[uartReader.FrameLength];
            uartReader.Data[0] = uartReader.SlaveAddress;
            uartReader.Data[1] = uartReader.FrameLength;
            uartReader.DataHandle = DataHandleInitialValue;
        }

        public bool IsLengthInRange(byte length)
        {
            return length >= 4 && length <= 14;
        }

        public bool SetFrameLength(UartReader uartReader, byte length)
        {
            uartReader.FrameLength = length;
            if (!IsLengthInRange(uartReader.FrameLength))
            {
                return false;
            }
            InitializeDataArray(uartReader);
            return true;
        }
        public bool Read(UartReader uartReader, byte input)
        {
            if (uartReader.DataHandle < uartReader.FrameLength)
            {
                uartReader.Data[uartReader.DataHandle++] = input;
                return false;
            }
            if (UartHelper.GetCrc(uartReader.Data, 0, uartReader.FrameLength) == input)
            {
                //Log.LogWrite("receive: " + BitConverter.ToString(_data).Replace("-", " "));
                //SendData(serialPort, _commandLevel.ProcessCommand(_data));  
            }
            return true;
        }
    }
}
