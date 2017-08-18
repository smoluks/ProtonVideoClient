using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    interface IUartReaderStateHandler
    {
        ConnectionState GetConnectionResult(UartReader reader);
        bool GetFrameLengthChangeResult(UartReader reader);
        ReadState GetReadResult(UartReader reader);
    }
}
