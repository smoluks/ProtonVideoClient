using ProtonRS485Client.Uart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace ProtonRS485UnitTestProject
{
    class FakeUart : HardwareLevelDispatcher
    {
        MemoryStream dataIn;

        public void SetDataIn(byte[] data)
        {
            dataIn = new MemoryStream(data);
        }

        public async Task<byte[]> ReadAsync(int count, CancellationToken token)
        {
            byte[] buffer = new byte[count];
            await dataIn.ReadAsync(buffer, 0, count);
            return buffer;
        }

        public async Task<byte> ReadByteAsync(CancellationToken token)
        {
            byte[] buffer = new byte[1];
            await dataIn.ReadAsync(buffer, 0, 1);
            return buffer[0];
        }

        public Task WriteAsync(byte[] buffer, CancellationToken token)
        {
            foreach (byte b in buffer)
                processByte(b);
            return new Task(() => {});
        }

        public Task WriteByteAsync(byte data, CancellationToken token)
        {
            processByte(data);
            return new Task(() => { });
        }

        public bool packetIsComleted = false;
        public byte[] dataOut = null;

        enum Estate { WaitAddress, WaitLength, CollectData };
        Estate State = Estate.WaitAddress;

        byte address;
        byte handle;

        void processByte(byte b)
        {
            switch (State)
            {
                case Estate.WaitAddress:
                    address = b;
                    State = Estate.WaitLength;
                    break;
                case Estate.WaitLength:
                    dataOut = new byte[b + 1];
                    dataOut[0] = address;
                    dataOut[1] = b;
                    handle = 2;
                    State = Estate.CollectData;
                    break;
                case Estate.CollectData:
                    dataOut[handle++] = b;
                    if (handle == dataOut.Length)
                    {
                        packetIsComleted = true;
                        State = Estate.WaitAddress;
                    }
                    break;
            }
        }
    }
}
