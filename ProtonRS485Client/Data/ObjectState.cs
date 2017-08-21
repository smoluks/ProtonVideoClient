namespace ProtonRS485Client
{
    /// <summary>
    /// Все состояния модуля как slave устройства на шине протона
    /// </summary>
    /// 
    class ObjectState
    {
        /// <summary>
        /// Пожар?
        /// </summary>
        /// тут лучше свойства
        public bool Fire = false;
        /// <summary>
        /// Паника?
        /// </summary>
        public bool Panic = false;
        /// <summary>
        /// Тревога?
        /// </summary>
        public bool Alarm = false;
        /// <summary>
        /// Неисправность?
        /// </summary>
        public bool Error = false;
        /// <summary>
        /// Состояние раздела
        /// </summary>
        /// E надо убрать
        public enum ERazdelState : byte { Off = 0, Programming = 2, Disarmed = 4, WaitingArmed = 5, NotReady = 6, WaitingLoopSelect = 7, Armed = 8, WaitingDisarmed = 9, Bruteforce = 12 };
        public ERazdelState RazdelState = ERazdelState.Off;
        /// <summary>
        /// Ожидание взятия/снятия?
        /// </summary>
        public bool WaitingArmedChangeState = false;
        /// <summary>
        /// Время до окончания процесса ожидания взятия/снятия
        /// </summary>
        public byte waitingTime = 0;
        ///Отображение на светодиоде ПУ наличия связи прибора с ПЦН
        public enum ELEdState : byte { Off = 0, Green = 1, Orange = 2, Red = 3 };
        public ELEdState LedState = ELEdState.Off;
        /// <summary>
        /// Есть вскрытие корпуса?
        /// </summary>
        public bool TamperOff = false;
        /// <summary>
        /// Состояние АКБ
        /// </summary>
        public enum EBatteryState : byte { Normal = 0, Discharged = 1, NoBattery = 3 };
        public EBatteryState BatteryState = EBatteryState.Normal;
        /// <summary>
        /// Сетевое питание отсутствует?
        /// </summary>
        public bool MainPowerError = false;
        /// <summary>
        /// Ответ на команду предыдущего цикла опроса (если команда там была)
        /// </summary>
        public enum ECommandAnswer : byte { NoCommand = 0, accepted = 0x11, WrongPassword = 0x12, Rejected = 0x13 };
        public ECommandAnswer CommandAnswer = ECommandAnswer.NoCommand;

    }
}
