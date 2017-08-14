using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    struct SObjectConfig
    {
        public byte deviceAddress;
        public ushort objectNumber;
        public byte RazdelNumber;
        public byte madeYear; //BCD
        public ushort serialNumber; //BCD
        public byte softwareVersion; //BCD
        public byte softwareReleaseVersion; //BCD
        public SObjectConfig(ushort objectNumber)
        {
            deviceAddress = 120;
            this.objectNumber = objectNumber;
            madeYear = 0x17;
            serialNumber = 0x0001;
            softwareVersion = 0x01;
            softwareReleaseVersion = 0x01;
            RazdelNumber = 120;
        }
    }
}
