
namespace ProtonRS485Client
{
    public enum EObjectMessages : ushort { NoiseOn = 155, NoiseOff = 155 + 0x400 };
    class Message
    {       
        public EObjectMessages MessageCode;
        public byte Argument;

        public Message(EObjectMessages command, byte arg)
        {
            MessageCode = command;
            Argument = arg;
        }
    }
}
