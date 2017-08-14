using System;
using System.IO;

namespace ProtonVideoClient
{   
    /// <summary>
    /// Реализация ведения лог-файла
    /// </summary>
    static class Log
    {
        static StreamWriter logStream = null;
        const string FileName = "ProtonVideoClient.log";

        public static void StartLog()
        {
            logStream = new StreamWriter(FileName, false);
        }

        public static void StartLog(string FileName)
        {
            logStream = new StreamWriter(FileName, false);
        }

        public static void Write(string text)
        {
            if (logStream != null)
                logStream.WriteLine(text);
        }

        public static void WriteException(Exception e)
        {
            if (logStream != null)
                logStream.WriteLine(e.Message);
        }

        public static void CloseLog()
        {
            if (logStream != null)
                logStream.Close();
        }
    }
}
