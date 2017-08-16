using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    class UartReaderStateDispatcher
    {
        private readonly UartReader _uartReader;
        private readonly UartReaderConnectionDispatcher _connectionDispatcher;
        public UartReaderStateDispatcher(UartReader uartReader, UartReaderConnectionDispatcher connectionDispatcher)
        {
            _uartReader = uartReader;
            _connectionDispatcher = connectionDispatcher;
        }
        public void SetState(byte input)
        {
            if (_uartReader.State == UartReadState.Command)
            {
                if (_connectionDispatcher.Connect(_uartReader, input))
                    _uartReader.State = UartReadState.Length;
            }
            else if (_uartReader.State == UartReadState.Length)
            {     
                _uartReader.State =
                    _uartReader.SetFrameLength(input)
                        ? UartReadState.Data
                        : UartReadState.Command;
            }
            else if (_uartReader.State == UartReadState.Data)
            {
                if (_uartReader.Read(input))
                    _uartReader.State = UartReadState.Command;
            }
        }
    }
}
