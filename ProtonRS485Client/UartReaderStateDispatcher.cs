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
        private readonly UartReaderDataDispatcher _dataDispatcher;
        public UartReaderStateDispatcher(UartReader uartReader, UartReaderConnectionDispatcher connectionDispatcher, UartReaderDataDispatcher dataDispatcher)
        {
            _uartReader = uartReader;
            _connectionDispatcher = connectionDispatcher;
            _dataDispatcher = dataDispatcher;
        }

        public void SetState(byte input)
        {
            if (_uartReader.State == UartReadState.Command)
            {
                if (_connectionDispatcher.Connect(_uartReader, input) != ConnectionState.WrongAddress)
                    _uartReader.State = UartReadState.Length;
            }
            else if (_uartReader.State == UartReadState.Length)
            {     
                _uartReader.State =
                    _dataDispatcher.SetFrameLength(_uartReader, input)
                        ? UartReadState.Data
                        : UartReadState.Command;
            }
            else if (_uartReader.State == UartReadState.Data)
            {
                if (_dataDispatcher.Read(_uartReader, input))
                    _uartReader.State = UartReadState.Command;
            }
        }
    }
}
