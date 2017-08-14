using System;
using System.Collections.Generic;
using IniParser;
using IniParser.Model;
using System.IO;
using System.Windows.Forms;

namespace ProtonVideoClient
{
    public class Config
    {
        /// <summary>
        /// Сом-порт как он передается системе
        /// </summary>
        public string ComPort = null;
        /// <summary>
        /// Записывать весь обмен RS485 в лог?
        /// </summary>
        public bool RS485LogEnable = false;
        /// <summary>
        /// Объектовый номер
        /// </summary>
        public ushort ObjectNumber = 13;
        /// <summary>
        /// Тип заставки
        /// </summary>
        public enum ESplashScreenType { None = 0, WebPage = 1, Video = 2, Back = 3 };
        public ESplashScreenType SplashScreenType;
        /// <summary>
        ///Адрес веб-странички заставки
        /// </summary>
        public string SplashScreenUrl;
        /// <summary>
        /// Частота обновления веб-странички
        /// </summary>
        public int WebRefreshInterval = 60000;
        /// <summary>
        ///Путь к видеофайлу заставки
        /// </summary>
        public string SplashScreenVideoFile;
        /// <summary>
        /// Пути файлов сообщений
        /// </summary>
        public Dictionary<byte, string> FilePaths = new Dictionary<byte, string>();
        /// <summary>
        /// 
        /// </summary>
        public string SteadySiren;
        /// <summary>
        /// 
        /// </summary>
        public string WailSiren;
        /// <summary>
        /// 
        /// </summary>
        public string SilentTest;
        /// <summary>
        /// Повторять файлы сообщений
        /// </summary>
        public bool Repeat = false;

        public Config()
        {
            ReadConfig();
        }

        public void ReadConfig()
        {
            var parser = new FileIniDataParser();
            try
            {
                ///-----чтение файла конфига-----
                IniData data = parser.ReadFile("config.ini");
                //Hardware
                ComPort = data.Sections["Hardware"].GetKeyData("ComPort").Value;
                bool.TryParse(data.Sections["Hardware"].GetKeyData("RS485log").Value, out RS485LogEnable);
                ushort.TryParse(data.Sections["Hardware"].GetKeyData("ObjectNumber").Value, out ObjectNumber);
                //SplashScreen
                switch (data.Sections["SplashScreen"].GetKeyData("SplashScreenType")?.Value)
                {
                    case "none":
                    default:
                        SplashScreenType = ESplashScreenType.None;
                        break;
                    case "webpage":
                        SplashScreenType = ESplashScreenType.WebPage;
                        break;
                    case "video":
                        SplashScreenType = ESplashScreenType.Video;
                        break;
                    case "back":
                        SplashScreenType = ESplashScreenType.Back;
                        break;
                }
                SplashScreenUrl = data.Sections["SplashScreen"].GetKeyData("SplashScreenUrl")?.Value;
                int.TryParse(data.Sections["SplashScreen"].GetKeyData("WebRefreshInterval").Value, out WebRefreshInterval);
                SplashScreenVideoFile = data.Sections["SplashScreen"].GetKeyData("SplashScreenVideoFile")?.Value;
                //Media
                foreach (var Key in data.Sections["Media"])
                {
                    byte number;
                    if (byte.TryParse(Key.KeyName, out number))
                    {
                        if (FilePaths.ContainsKey(number))
                            FilePaths.Remove(number);
                        FilePaths.Add(number, Key.Value);
                    }
                }
                SteadySiren = data.Sections["Media"].GetKeyData("SteadySiren").Value;
                WailSiren = data.Sections["Media"].GetKeyData("WailSiren").Value;
                SilentTest = data.Sections["Media"].GetKeyData("SilentTest").Value;
                bool.TryParse(data.Sections["Media"].GetKeyData("Repeat").Value, out Repeat);
            }
            catch (Exception e)
            {
                Log.Write("Ошибка при открытии ini файла: " + e.Message);
                DialogResult result = MessageBox.Show("В файле конфигурации есть ошибки. Пересоздать?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (result == DialogResult.Cancel)
                    Environment.Exit(0);
                ReadConfigDefault();
                WriteConfig();
            }
        }

        public void ReadConfigDefault()
        {
            //
            ComPort = "COM6";
            //
            SplashScreenType = ESplashScreenType.WebPage;
            SplashScreenVideoFile = @"D:\video\1183172488213.swf";
            SplashScreenUrl = @"https://yandex.ru/pogoda/samara";
            //
            FilePaths.Clear();
            FilePaths.Add(1, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.01[BDRemux].mkv");
            FilePaths.Add(2, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.02[BDRemux].mkv");
            FilePaths.Add(3, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.03[BDRemux].mkv");
            FilePaths.Add(4, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.04[BDRemux].mkv");
            FilePaths.Add(5, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.05[BDRemux].mkv");
            FilePaths.Add(6, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.06[BDRemux].mkv");
            FilePaths.Add(7, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.07[BDRemux].mkv");
            FilePaths.Add(8, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.08[BDRemux].mkv");
            FilePaths.Add(9, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.09[BDRemux].mkv");
            FilePaths.Add(10, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.10[BDRemux].mkv");
            FilePaths.Add(11, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.11[BDRemux].mkv");
            FilePaths.Add(12, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.12[BDRemux].mkv");
            FilePaths.Add(13, @"D:\serials\Zan Sayonara Zetsubou Sensei\[anti-raws]Zan Sayonara Zetsubou Sensei ep.13[BDRemux].mkv");
            SteadySiren = @"D:\video\1494435487359.webm";
            WailSiren = @"D:\video\1495032273157-0.mp4";
            SilentTest = @"D:\video\14099096013600.webm";
        }

        public void WriteConfig()
        {
            var parser = new FileIniDataParser();
            IniData ini = new IniData();
            //Hardware
            SectionData HardwareSection = new SectionData("Hardware");
            HardwareSection.Keys.AddKey("ComPort", ComPort);
            HardwareSection.Keys.AddKey("RS485log", RS485LogEnable.ToString());
            HardwareSection.Keys.AddKey("ObjectNumber", ObjectNumber.ToString());
            ini.Sections.Add(HardwareSection);
            //Common
            SectionData CommonSection = new SectionData("SplashScreen");
            switch (SplashScreenType)
            {
                case ESplashScreenType.None:
                    CommonSection.Keys.AddKey("SplashScreenType", "none");
                    break;
                case ESplashScreenType.WebPage:
                    CommonSection.Keys.AddKey("SplashScreenType", "webpage");
                    break;
                case ESplashScreenType.Video:
                    CommonSection.Keys.AddKey("SplashScreenType", "video");
                    break;
                case ESplashScreenType.Back:
                    CommonSection.Keys.AddKey("SplashScreenType", "back");
                    break;
            }
            CommonSection.Keys.AddKey("SplashScreenVideoFile", SplashScreenVideoFile);
            CommonSection.Keys.AddKey("SplashScreenUrl", SplashScreenUrl);
            CommonSection.Keys.AddKey("WebRefreshInterval", WebRefreshInterval.ToString());
            ini.Sections.Add(CommonSection);
            //Media
            SectionData MediaSection = new SectionData("Media");
            foreach (KeyValuePair<byte, string> filePath in FilePaths)
                MediaSection.Keys.AddKey(filePath.Key.ToString(), filePath.Value);
            MediaSection.Keys.AddKey("SteadySiren", SteadySiren);
            MediaSection.Keys.AddKey("WailSiren", WailSiren);
            MediaSection.Keys.AddKey("SilentTest", SilentTest);
            MediaSection.Keys.AddKey("Repeat", Repeat.ToString());
            ini.Sections.Add(MediaSection);
            //
            parser.WriteFile("config.ini", ini);
        }

        public Dictionary<byte, string> CloneDictionary()
        {
            Dictionary<byte, string> ret = new Dictionary<byte, string>(FilePaths.Count);
            foreach (KeyValuePair<byte, string> entry in FilePaths)
            {
                ret.Add(entry.Key, entry.Value);
            }
            return ret;
        }
    }
}
