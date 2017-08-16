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
        /*private readonly UartReaderConnectionDispatcher _connectionDispatcher;
        private readonly UartReaderDataDispatcher _dataDispatcher;*/
        private IUartReaderStateHandler _stateHandler;
        public UartReaderStateDispatcher(UartReader uartReader, IUartReaderStateHandler stateHandler)
        {
            _uartReader = uartReader;
            _stateHandler = stateHandler;
        }
        public void SetState()
        {
            if (_uartReader.State == UartReadState.Command)
            {
                if (_stateHandler.GetConnectionResult(_uartReader) != ConnectionState.WrongAddress)
                    _uartReader.State = UartReadState.Length;
            }
            else if (_uartReader.State == UartReadState.Length)
            {
                _uartReader.State =
                    _stateHandler.GetFrameLengthChangeResult(_uartReader)
                        ? UartReadState.Data
                        : UartReadState.Command;
            }
            else if (_uartReader.State == UartReadState.Data)
            {
                if (_stateHandler.GetReadResult(_uartReader) != ReadState.InProcess)
                    _uartReader.State = UartReadState.Command;
            }
        }
    }
}
