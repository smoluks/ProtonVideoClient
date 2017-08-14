using System;
using System.IO;

namespace ProtonRS485Client
{   
    static class Log
    {
        static StreamWriter logStream = null;
        const string FileName = "rs485.log";
        public static void OpenLogFile()
        {
            logStream = new StreamWriter(FileName, false);
        }

        public static void LogWrite(string text)
        {
            if (logStream != null)
                logStream.WriteLine(text);
        }

        public static void LogWriteException(Exception e)
        {
            if (logStream != null)
                logStream.WriteLine(e.Message);
        }

        public static void CloseLogFile()
        {
            if (logStream != null)
                logStream.Close();
        }
    }
}
