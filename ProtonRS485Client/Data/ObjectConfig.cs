namespace ProtonRS485Client.Data
{
    /// <summary>
    /// Все настройки модуля
    /// </summary>
    public static class ObjectConfig
    {
        /// <summary>
        /// Железный адрес устройства на шине. ППКОПы имеют адреса 1-15, ПУ 16-31, модуль оповещения - 120
        /// </summary>
        public static byte DeviceAddress = 120;
        /// <summary>
        /// Номер объекта в АРМ. 
        /// </summary>
        public static ushort ObjectNumber = 13;
        /// <summary>
        /// Номер раздела устройства. Если вам непонятно, что это, читайте инструкцию на протон-8. Для модуля оповещения 120
        /// </summary>
        /// см. выше
        public static byte RazdelNumber = 120;
        /// <summary>
        /// Год выпуска в BCD
        /// </summary>
        public static byte madeYear = 0x17;
        /// <summary>
        /// Номер устройства в BCD
        /// </summary>
        public static ushort serialNumber = 0x0001;
        /// <summary>
        /// Версия ПО в BCD
        /// </summary>
        public static byte softwareVersion = 0x01;
        /// <summary>
        /// Версия релиза ПО в BCD
        /// </summary>
        public static byte softwareReleaseVersion = 0x01;
        /// <summary>
        /// Записывать весь обмен данными по RS485 в лог?
        /// </summary>
        public static bool dataLogging = false;
    }
}
