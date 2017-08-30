using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ProtonRS485Client.Data
{
    /// <summary>
    /// Данные, которыми обмениваются библиотека и программа
    /// Они не торчат наружу, а обернуты в RS485Client
    /// </summary>
    internal static class ExternalDataContract
    {
        /// <summary>
        /// Подключен ли протон-8
        /// </summary>
        public static bool IsMasterConnect = false;

        /// <summary>
        /// очередь команд, полученных от сервера
        /// </summary>
        public static ConcurrentQueue<ProtonMessage> CommandQueue = new ConcurrentQueue<ProtonMessage>();

        /// <summary>
        /// очередь сообщений, которые надо отправить серверу
        /// </summary>
        public static ConcurrentQueue<ProtonMessage> MessageBuffer = new ConcurrentQueue<ProtonMessage>();
    }
}
