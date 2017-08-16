using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    enum ReadState
    {
        InProcess,
        CrcCorrect,
        CrcIncorrect
    }
    class UartReaderDataDispatcher
    {   
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
            uartReader.InitializeDataArray();
            return true;
        }
        public ReadState Read(UartReader uartReader, byte input)
        {
            if (uartReader.DataHandle < uartReader.FrameLength)
            {
                uartReader.Data[uartReader.DataHandle++] = input;
                return ReadState.InProcess;
            }
            if (UartHelper.GetCrc(uartReader.Data) != input) return ReadState.CrcIncorrect;
            //Log.LogWrite("receive: " + BitConverter.ToString(_data).Replace("-", " "));
            //SendData(serialPort, _commandLevel.ProcessCommand(_data)); 
            return ReadState.CrcCorrect;           
        }
    }
}
