
namespace ProtonRS485Client
{
    public enum EObjectMessages : ushort { Feedback = 155 };
    public enum EObjectMessagePrefixs : ushort { On = 0x400, Off = 0 };

    /// <summary>
    /// Класс, описывающий сообщение от объекта серверу 
    /// </summary>
    class Message
    {
        private EObjectMessages _command;
        private EObjectMessagePrefixs _state;
        private byte _argument;

        /// <summary>
        /// Конструктор сообщения
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="state">Сработка или восстановление</param>
        /// <param name="arg">Аргумент команды, как правило номер шлейфа</param>
        public Message(EObjectMessages command, EObjectMessagePrefixs state, byte arg)
        {
            _command = command;
            _state = state;
            _argument = arg;
        }

        /// <summary>
        /// Код команды
        /// </summary>
        public ushort CommandCode
        {
            get
            {
                return (ushort)((int)_command + (int)_state);
            }
        }

        /// <summary>
        /// Аргумент
        /// </summary>
        public byte Argument
        {
            get
            {
                return _argument;
            }
        }
    }
}
