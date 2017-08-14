using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtonRS485Client
{
    struct SObjectState
    {        
        /// <summary>
        /// Пожар?
        /// </summary>
        public bool Fire; 
        /// <summary>
        /// Паника?
        /// </summary>
        public bool Panic;
        /// <summary>
        /// Тревога?
        /// </summary>
        public bool Alarm;
        /// <summary>
        /// Неисправность?
        /// </summary>
        public bool Error;
        /// <summary>
        /// Состояние раздела
        /// </summary>
        public enum ERazdelState : byte {Off = 0, Programming = 2, Disarmed = 4, WaitingArmed = 5, NotReady = 6, WaitingLoopSelect = 7, Armed = 8, WaitingDisarmed = 9, Bruteforce = 12};
        public ERazdelState RazdelState;
        /// <summary>
        /// Ожидание взятия/снятия?
        /// </summary>
        public bool WaitingArmedChangeState;
        /// <summary>
        /// Время до окончания процесса ожидания взятия/снятия
        /// </summary>
        public byte waitingTime;
        ///Отображение на светодиоде ПУ наличия связи прибора с ПЦН
        public enum ELEdState : byte { Off = 0, Green = 1, Orange = 2, Red = 3};
        public ELEdState LedState;
        /// <summary>
        /// Есть вскрытие корпуса?
        /// </summary>
        public bool TamperOff;
        /// <summary>
        /// Состояние АКБ
        /// </summary>
        public enum EBatteryState : byte { Normal = 0, Discharged = 1, NoBattery = 3};
        public EBatteryState BatteryState;
        /// <summary>
        /// Сетевое питание отсутствует?
        /// </summary>
        public bool MainPowerError;
        /// <summary>
        /// Ответ на команду предыдущего цикла опроса (если команда там была)
        /// </summary>
        public enum ECommandAnswer : byte { NoCommand = 0, accepted = 0x11, WrongPassword = 0x12, Rejected = 0x13};
        public ECommandAnswer CommandAnswer;

        public SObjectState(bool nya)
        {
            Fire = false;
            Panic = false;
            Alarm = false;
            Error = false;
            RazdelState = ERazdelState.Off;
            WaitingArmedChangeState = false;
            waitingTime = 0;
            LedState = ELEdState.Off;
            TamperOff = false;
            BatteryState = EBatteryState.Normal;
            MainPowerError = false;
            CommandAnswer = 0;
        }
    }
}
