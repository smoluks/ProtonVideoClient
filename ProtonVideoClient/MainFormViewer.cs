using ProtonRS485Client;
using System.IO;
using System.Windows.Forms;

namespace ProtonVideoClient
{
    public partial class MainForm : Form
    {
        bool isError = false;


        public void ViewerPlayMessage(string path)
        {            
            Log.Write("ViewerPlay: " + path);
            Player.Ctlcontrols.stop();
            ShowPlayer();
            Player.URL = path;
            Player.settings.setMode("loop", config.Repeat);
            Player.Ctlcontrols.play();
        }

        public void ViewerStopMessage()
        {
            Log.Write("ViewerShowStop");
            Player.Ctlcontrols.stop();
            HidePlayer();
            if (isError)
                ShowError();
            else
                ShowScreenSaver();
        }

        public void ViewerShowError(string text)
        {
            Log.Write("ViewerShowError: " + text);
            ErrorText.Text = text;
            ShowError();
            isError = true;
        }

        public void ViewerHideError()
        {
            Log.Write("ViewerHideError");
            isError = false;
            ShowScreenSaver();
        }

        void ShowPlayer()
        {
            Player.Visible = true;
            this.WindowState = FormWindowState.Maximized;
        }

        public void HidePlayer()
        {
            Player.Visible = false;
        }

        void ShowError()
        {
            Player.Visible = false;
            webBrowser.Visible = false;
            ErrorText.Visible = true;
            WebRefreshTimer.Stop();
            this.WindowState = FormWindowState.Maximized;
        }

        void ShowScreenSaver()
        {
            ErrorText.Visible = false;
            switch (config.SplashScreenType)
            {
                case Config.ESplashScreenType.None:
                    Player.Visible = false;
                    webBrowser.Visible = false;
                    this.WindowState = FormWindowState.Maximized;
                    break;
                case Config.ESplashScreenType.Video:
                    if (File.Exists(config.SplashScreenVideoFile))
                    {
                        webBrowser.Visible = false;
                        Player.Visible = true;
                        Player.URL = config.SplashScreenVideoFile;
                        Player.settings.setMode("loop", true);
                        Player.Ctlcontrols.play();
                        this.WindowState = FormWindowState.Maximized;
                    }
                    else ViewerShowError("Файл не найден: " + config.SplashScreenVideoFile);
                    break;
                case Config.ESplashScreenType.WebPage:
                    if (config.SplashScreenUrl != null)
                    {
                        Player.Visible = false;
                        webBrowser.Visible = true;
                        webBrowser.Navigate(config.SplashScreenUrl);
                        if (config.WebRefreshInterval != 0)
                        {
                            WebRefreshTimer.Interval = config.WebRefreshInterval * 1000;
                            WebRefreshTimer.Start();
                        }
                        this.WindowState = FormWindowState.Maximized;
                    }
                    break;
                case Config.ESplashScreenType.Back:
                    this.WindowState = FormWindowState.Minimized;
                    break;
            }
        }

    }
}
