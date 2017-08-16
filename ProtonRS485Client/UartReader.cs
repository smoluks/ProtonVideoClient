using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    public enum UartReadState
    {
        Command = 0, 
        Length, 
        Data
    }
    public class UartReader
    {
        public byte SlaveAddress { get; set; }
        public int DeviceAddress { get; set; }
        public UartReadState State { get; set; }
        public bool Connected { get; set; }
        public byte FrameLength { get; set; }
        public byte[] Data { get; set; }
        public byte DataHandle { get; set; }  
    }
}
