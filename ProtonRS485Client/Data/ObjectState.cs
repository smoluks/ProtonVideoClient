namespace ProtonRS485Client.Data
{
    /// <summary>
    /// Все состояния модуля как slave устройства на шине протона
    /// </summary>
    /// 
    public static class ObjectState
    {
        /// <summary>
        /// Пожар?
        /// </summary>
        /// тут лучше свойства
        public static bool Fire = false;
        /// <summary>
        /// Паника?
        /// </summary>
        public static bool Panic = false;
        /// <summary>
        /// Тревога?
        /// </summary>
        public static bool Alarm = false;
        /// <summary>
        /// Неисправность?
        /// </summary>
        public static bool Error = false;
        /// <summary>
        /// Состояние раздела
        /// </summary>
        /// E надо убрать
        public enum ERazdelState : byte { Off = 0, Programming = 2, Disarmed = 4, WaitingArmed = 5, NotReady = 6, WaitingLoopSelect = 7, Armed = 8, WaitingDisarmed = 9, Bruteforce = 12 };
        public static ERazdelState RazdelState = ERazdelState.Off;
        /// <summary>
        /// Ожидание взятия/снятия?
        /// </summary>
        public static bool WaitingArmedChangeState = false;
        /// <summary>
        /// Время до окончания процесса ожидания взятия/снятия
        /// </summary>
        public static byte waitingTime = 0;
        ///Отображение на светодиоде ПУ наличия связи прибора с ПЦН
        public enum ELEdState : byte { Off = 0, Green = 1, Orange = 2, Red = 3 };
        public static ELEdState LedState = ELEdState.Off;
        /// <summary>
        /// Есть вскрытие корпуса?
        /// </summary>
        public static bool TamperOff = false;
        /// <summary>
        /// Состояние АКБ
        /// </summary>
        public enum EBatteryState : byte { Normal = 0, Discharged = 1, NoBattery = 3 };
        public static EBatteryState BatteryState = EBatteryState.Normal;
        /// <summary>
        /// Сетевое питание отсутствует?
        /// </summary>
        public static bool MainPowerError = false;
        /// <summary>
        /// Ответ на команду предыдущего цикла опроса (если команда там была)
        /// </summary>
        public enum ECommandAnswer : byte { NoCommand = 0, accepted = 0x11, WrongPassword = 0x12, Rejected = 0x13 };
        public static ECommandAnswer CommandAnswer = ECommandAnswer.NoCommand;

    }
}
