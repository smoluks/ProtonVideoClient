
namespace ProtonVideoClient
{
    class Command
    {
        public byte command;
        public byte arg;

        public Command(byte command, byte arg)
        {
            this.command = command;
            this.arg = arg;
        }
    }
}
