using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows.Forms;
using static ProtonVideoClient.Config;

namespace ProtonVideoClient
{
    public partial class ConfigForm : Form
    {
        Config _config;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="config">Ссылка на конфиг приложения</param>
        public ConfigForm(Config config)
        {
            _config = config;
            InitializeComponent();
        }

        void ConfigForm_Load(object sender, EventArgs e)
        {
            //-----Hardware-----
            //serial
            foreach (string sp in SerialPort.GetPortNames())
            {
                PortList.Items.Add(sp);
            }
            PortList.Text = _config.ComPort;
            //
            RS485LogCb.Checked = _config.RS485LogEnable;
            //
            ObjNumberNud.Value = _config.ObjectNumber;
            //-----Screensaver-----
            //
            ScreensaverTypeCb.SelectedIndex = (int)_config.SplashScreenType;
            //
            ScreensaverUrlTb.Text = _config.SplashScreenUrl.ToString();
            //
            WebRefreshNUD.Value = _config.WebRefreshInterval;
            //
            ScreensaverVideoTb.Text = _config.SplashScreenVideoFile;
            //-----Media-----
            //
            WriteTable(_config.CloneDictionary());
            //
            SteadyTb.Text = _config.SteadySiren;
            //
            WailTb.Text = _config.WailSiren;
            //
            SilentTb.Text = _config.SilentTest;
            //
            RepeatCb.Checked = _config.Repeat;
        }

        /// <summary>
        /// Обновить список портов
        /// </summary>
        private void RefreshPortList_Click(object sender, EventArgs e)
        {
            PortList.Items.Clear();
            foreach (string sp in SerialPort.GetPortNames())
            {
                PortList.Items.Add(sp);
            }
        }

        /// <summary>
        /// Кнопка ОК
        /// </summary>
        private void OKBtn_Click(object sender, EventArgs e)
        {
            //
            if (_config.ComPort != PortList.Text)
            {
                _config.ComPort = PortList.Text;
                MessageBox.Show("Для смены порта необходимо перезапустить программу");
            }
            _config.RS485LogEnable = RS485LogCb.Checked;
            _config.ObjectNumber = (ushort)ObjNumberNud.Value;
            //
            _config.SplashScreenType = (ESplashScreenType)ScreensaverTypeCb.SelectedIndex;
            _config.SplashScreenUrl = ScreensaverUrlTb.Text;
            _config.WebRefreshInterval = (int)WebRefreshNUD.Value;
            _config.SplashScreenVideoFile = ScreensaverVideoTb.Text;
            //
            _config.FilePaths = ReadTable();
            _config.SteadySiren = SteadyTb.Text;
            _config.WailSiren = WailTb.Text;
            _config.SilentTest = SilentTb.Text;
            _config.Repeat = RepeatCb.Checked;
            //
            _config.WriteConfig();
            //
            this.Close();
        }

        /// <summary>
        /// Кнопка Отмена
        /// </summary>
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        private void AddBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Выберите файл для сообщения " + numSelect.Value;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                bool isFound = false;
                //поиск такого сообщения в таблице
                foreach (DataGridViewRow d in FilePathsTable.Rows)
                {
                    if (d.Cells[0].Value != null && (byte)d.Cells[0].Value == (byte)numSelect.Value)
                    {
                        d.Cells[1].Value = openFileDialog1.FileName;
                        isFound = true;
                    }
                }
                //
                if (!isFound)
                    FilePathsTable.Rows.Add((byte)numSelect.Value, openFileDialog1.FileName);
                //
                FilePathsTable.Sort(FilePathsTable.Columns["number"], ListSortDirection.Ascending);
                //
                if (numSelect.Value < 255)
                    numSelect.Value++;
            }
        }

        private void DeleteSelectedBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in FilePathsTable.SelectedCells)
            {
                FilePathsTable.Rows.Remove(FilePathsTable.Rows[cell.RowIndex]);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            //поиск такого сообщения в таблице
            foreach (DataGridViewRow d in FilePathsTable.Rows)
            {
                if (d.Cells[0].Value != null && (byte)d.Cells[0].Value == (byte)numSelect.Value)
                {
                    FilePathsTable.Rows.Remove(d);
                    return;
                }
            }
        }

        void WriteTable(Dictionary<byte, string> pathsDictionary)
        {
            foreach (KeyValuePair<byte, string> entry in pathsDictionary)
            {
                FilePathsTable.Rows.Add(entry.Key, entry.Value);
            }
        }

        Dictionary<byte, string> ReadTable()
        {
            Dictionary<byte, string> ret = new Dictionary<byte, string>(FilePathsTable.Rows.Count);
            foreach (DataGridViewRow d in FilePathsTable.Rows)
            {
                if (d.Cells[0].Value != null && d.Cells[1].Value != null)
                    ret.Add((byte)d.Cells[0].Value, (string)d.Cells[1].Value);
            }
            return ret;
        }

        private void SetSteadyBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Выберите видеофайл для непрерывной сирены";
                      
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                SteadyTb.Text = openFileDialog1.FileName;
            }
        }

        private void SetVideoScreenSaverBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Выберите видеофайл для заставки";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                ScreensaverVideoTb.Text = openFileDialog1.FileName;
            }
        }

        private void SetWailBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Выберите видеофайл для прерывистой сирены";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                WailTb.Text = openFileDialog1.FileName;
            }
        }

        private void SetSilentBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Выберите видеофайл для тихого теста";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                SilentTb.Text = openFileDialog1.FileName;
            }
        }

        private void FilePathsTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            openFileDialog1.Title = "Выберите файл для сообщения " + FilePathsTable.Rows[e.RowIndex].Cells[0].Value;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                FilePathsTable.Rows[e.RowIndex].Cells[1].Value = openFileDialog1.FileName;
            }
        }
    }
}
