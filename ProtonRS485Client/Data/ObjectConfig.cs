namespace ProtonRS485Client
{
    /// <summary>
    /// Все настройки модуля как slave устройства на шине протона
    /// </summary>
    public class ObjectConfig
    {
        /// <summary>
        /// Железный адрес устройства на шине. ППКОПы имеют адреса 1-15, ПУ 16-31, модуль оповещения - 120
        /// </summary>
        public byte deviceAddress = 120;
        /// <summary>
        /// Номер объекта в АРМ. 
        /// </summary>
        public ushort objectNumber = 13;
        /// <summary>
        /// Номер раздела устройства. Если вам непонятно, что это, читайте инструкцию на протон-8. Для модуля оповещения 120
        /// </summary>
        public byte RazdelNumber = 120;
        /// <summary>
        /// Год выпуска в BCD
        /// </summary>
        public byte madeYear = 0x17;
        /// <summary>
        /// Номер устройства в BCD
        /// </summary>
        public ushort serialNumber = 0x0001;
        /// <summary>
        /// Версия ПО в BCD
        /// </summary>
        public byte softwareVersion = 0x01;
        /// <summary>
        /// Версия релиза ПО в BCD
        /// </summary>
        public byte softwareReleaseVersion = 0x01;
    }
}
