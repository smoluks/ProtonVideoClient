using System;
using System.Timers;

namespace ProtonRS485Client.PackageCreate
{
    /// <summary>
    /// Этот класс будет дергать эвенты подключения и отключения
    /// </summary>
    class PackageConnectDispatcher
    {
        const int timeout = 4100;
        Timer timer;

        public PackageConnectDispatcher()
        {
            timer = new Timer(timeout);
            timer.Elapsed += Timeout;
        }
            /// <summary>
        /// Вызывается при приеме нашего адреса
        /// </summary>
        /// <param name="search">обмен пакетами находится на сосотоянии поиска ведомого</param>
        public void CorrectAddressReceived(bool search)
        {
            if (!search) //начинаем отсчет только если стадия поиска прошла успешно
            {
                ProtonEvents.Connect();
                timer.Stop();
                timer.Start();
            }
        }

        /// <summary>
        /// Вызыввается, если пауза между пакетами больше таймаута
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timeout(object sender,  ElapsedEventArgs e)
        {
            ProtonEvents.Disconnect();
            timer.Stop();
        }
    }
}
