using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    class UartReaderStateHandler : IUartReaderStateHandler
    {
        private byte CurrentByte { get; set; }
        private readonly UartReaderConnectionDispatcher _connectionDispatcher;
        private readonly UartReaderDataDispatcher _dataDispatcher;
        public UartReaderStateHandler(UartReaderConnectionDispatcher connectionDispatcher, UartReaderDataDispatcher dataDispatcher)
        {
            _connectionDispatcher = connectionDispatcher;
            _dataDispatcher = dataDispatcher; 
        }
        public ConnectionState GetConnectionResult(UartReader reader)
        {
            return _connectionDispatcher.Connect(reader, CurrentByte);
        }

        public bool GetFrameLengthChangeResult(UartReader reader)
        {
            return _dataDispatcher.SetFrameLength(reader, CurrentByte);
        }

        public ReadState GetReadResult(UartReader reader)
        {
            return _dataDispatcher.Read(reader, CurrentByte);
        }      
        public void SetCurrentByte(byte input)
        {
            CurrentByte = input;
        }
    }
}
