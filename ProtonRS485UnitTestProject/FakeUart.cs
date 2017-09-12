using ProtonRS485Client.Uart;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace ProtonRS485UnitTestProject
{
    class FakeUart : HardwareLevelDispatcher
    {
        Queue<byte> dataIn = new Queue<byte>();

        public void SetDataIn(byte[] data)
        {
            foreach (byte b in data)
                dataIn.Enqueue(b);
        }

        public async Task<byte[]> ReadAsync(int count, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                while (dataIn.Count < count) { };
                byte[] buffer = new byte[count];
                for (int i = 0; i < count; i++)
                    buffer[i] = dataIn.Dequeue();
                return buffer;
            });
        }

        public async Task<byte> ReadByteAsync(CancellationToken token)
        {
            return await Task.Run(() =>
            {
                while (dataIn.Count < 1) { };
                return dataIn.Dequeue();
            });
        }

        public Task WriteAsync(byte[] buffer, CancellationToken token)
        {
            return Task.Run(() =>
            {
                foreach (byte b in buffer)
                    ProcessByte(b);
            });
        }

        public Task WriteByteAsync(byte data, CancellationToken token)
        {
            return Task.Run(() =>
            {
                ProcessByte(data);
            });
        }

        public bool packetIsCompleted = false;
        byte[] dataOut = null;

        enum Estate { WaitAddress, WaitLength, CollectData };
        Estate State = Estate.WaitAddress;

        byte address;
        byte handle;

        public byte[] ReceivedPacket
        {
            get
            {
                packetIsCompleted = false;
                return dataOut;
            }
        }

        void ProcessByte(byte b)
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
                        packetIsCompleted = true;
                        State = Estate.WaitAddress;
                    }
                    break;
            }
        }
    }
}
