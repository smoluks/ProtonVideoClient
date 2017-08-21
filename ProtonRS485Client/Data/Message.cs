
namespace ProtonRS485Client
{ 
    /// <summary>
    /// Класс, описывающий объектово-серверные сообщения системы протон
    /// </summary>
    /// todo Все публичные поля сделать свойствами
    /// Все публичные члены классов назвать с большой буквы
    /// Для всех enum убрать префикс Е, значения enum с большой буквы
    /// Не меняющиеся значения сделать константами
    public class Message
    {
        /// <summary>
        /// Расположение бита, определяющего, эта команда включения или выключения (сработки - восстановления, если от объекта)
        /// </summary>
        const ushort _stateBit = 0x400;
        /// <summary>
        /// Код команды или сообщения
        /// </summary>
        public enum ECommandCode : ushort { FileMessage = 26, SirenMessage = 27, Feedback = 155 };
        public enum ECommandCodePrefix : ushort { On = _stateBit, Off = 0 };

        private ECommandCode _command;
        private ECommandCodePrefix _state;
        private byte _argument;

        /// <summary>
        /// Конструктор сообщения для известной команды
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="state">Сработка или восстановление</param>
        /// <param name="arg">Аргумент команды, как правило номер шлейфа</param>
        public Message(ECommandCode command, ECommandCodePrefix state, byte arg)
        {
            _command = command;
            _state = state;
            _argument = arg;
        }

        /// <summary>
        /// Конструктор сообщения по пришедшим кодам
        /// </summary>
        /// <param name="commandCode"></param>
        /// <param name="arg"></param>
        public Message(ushort commandCode, byte arg)
        {
            //Инкапсулировать commandCode & 0x3FF
            _command = (ECommandCode)(commandCode & 0x3FF); //поскольку у нас есть еще 5 бит сверху, которые могут задействовать, не надо сюда пихать инверсию _stateBit
            _state = (ECommandCodePrefix)(commandCode & _stateBit);
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

        //public byte Argument { get; private set; }
    }
}
