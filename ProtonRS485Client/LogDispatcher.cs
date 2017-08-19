using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ProtonRS485Client
{
    /// <summary>
    /// Класс, реализующий запись в лог для всего проекта
    /// </summary>
    static class LogDispatcher
    {
        static StreamWriter logStream = null;
        const string defaultFileName = "log.txt";

        /// <summary>
        /// Начать логирование. 
        /// Не забываем в конце работы вызвать CloseLogFile
        /// </summary>
        public static void OpenLogFile()
        {
            try
            {
                logStream = new StreamWriter(defaultFileName, false);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ошибка открытия лог-файла с именем " + defaultFileName + ": " + exception.Message);
            }
        }

        /// <summary>
        /// Начать логирование в заданный файл.
        /// Не забываем в конце работы вызвать CloseLogFile
        /// </summary>
        public static void OpenLogFile(string fileName)
        {
            try
            {
                logStream = new StreamWriter(fileName, false);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Ошибка открытия лог-файла с именем " + fileName + ": " + exception.Message);
            }
        }

        /// <summary>
        /// Записать текст в лог-файл
        /// </summary>
        /// <param name="text">текст</param>
        public static void Write(string text)
        {
            if (logStream == null)
                return;
            DateTime localDate = DateTime.Now;
            logStream.WriteLine(localDate.ToString() + " " + text + " "+ Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// Записать сообщение исключения в лог-файл
        /// </summary>
        /// <param name="e">Exception</param>
        public static void LogWriteException(Exception e)
        {
            if (logStream == null)
                return;
            DateTime localDate = DateTime.Now;
            logStream.WriteLine(localDate.ToString() + " " + e.Message +" "+ Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// Закрыть файл лога
        /// </summary>
        public static void CloseLogFile()
        {
            if (logStream != null)
                logStream.Close();
        }

    }
}
