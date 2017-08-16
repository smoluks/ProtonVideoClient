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
        public void InitializeDataArray()
        {
            Data = new byte[FrameLength];
            Data[0] = SlaveAddress;
            Data[1] = FrameLength;
            DataHandle = DataHandleInitialValue;
        }

        public bool IsLengthInRange(byte length)
        {
            return length >= 4 && length <= 14;
        }

        public bool SetFrameLength(byte length)
        {
            FrameLength = length;
            if (!IsLengthInRange(FrameLength))
            {
                return false;
            }
            InitializeDataArray();
            return true;
        }
        public bool Read(byte input)
        {
            if (DataHandle < FrameLength)
            {
                Data[DataHandle++] = input;
                return false;
            }
            if (UartHelper.GetCrc(Data, 0, FrameLength) == input)
            {
                //Log.LogWrite("receive: " + BitConverter.ToString(_data).Replace("-", " "));
                //SendData(serialPort, _commandLevel.ProcessCommand(_data));  
            }
            return true;
        }
    }
}
