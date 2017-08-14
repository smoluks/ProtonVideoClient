namespace ProtonVideoClient
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.RefreshPortList = new System.Windows.Forms.Button();
            this.PortList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.RS485LogCb = new System.Windows.Forms.CheckBox();
            this.ScreensaverUrlTb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SetSilentBtn = new System.Windows.Forms.Button();
            this.SetWailBtn = new System.Windows.Forms.Button();
            this.SetSteadyBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.SilentTb = new System.Windows.Forms.TextBox();
            this.WailTb = new System.Windows.Forms.TextBox();
            this.SteadyTb = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.RepeatCb = new System.Windows.Forms.CheckBox();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.DeleteSelectedBtn = new System.Windows.Forms.Button();
            this.numSelect = new System.Windows.Forms.NumericUpDown();
            this.AddBtn = new System.Windows.Forms.Button();
            this.FilePathsTable = new System.Windows.Forms.DataGridView();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SetVideoScreenSaverBtn = new System.Windows.Forms.Button();
            this.ScreensaverTypeCb = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.WebRefreshNUD = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.ScreensaverVideoTb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ObjNumberNud = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilePathsTable)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WebRefreshNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjNumberNud)).BeginInit();
            this.SuspendLayout();
            // 
            // RefreshPortList
            // 
            this.RefreshPortList.Image = ((System.Drawing.Image)(resources.GetObject("RefreshPortList.Image")));
            this.RefreshPortList.Location = new System.Drawing.Point(191, 10);
            this.RefreshPortList.Name = "RefreshPortList";
            this.RefreshPortList.Size = new System.Drawing.Size(22, 23);
            this.RefreshPortList.TabIndex = 7;
            this.RefreshPortList.UseVisualStyleBackColor = true;
            this.RefreshPortList.Click += new System.EventHandler(this.RefreshPortList_Click);
            // 
            // PortList
            // 
            this.PortList.FormattingEnabled = true;
            this.PortList.Location = new System.Drawing.Point(78, 12);
            this.PortList.Name = "PortList";
            this.PortList.Size = new System.Drawing.Size(107, 21);
            this.PortList.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "COM порт:";
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.Location = new System.Drawing.Point(481, 623);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 9;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.Location = new System.Drawing.Point(400, 623);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 10;
            this.CancelBtn.Text = "Отмена";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // RS485LogCb
            // 
            this.RS485LogCb.AutoSize = true;
            this.RS485LogCb.Location = new System.Drawing.Point(229, 14);
            this.RS485LogCb.Name = "RS485LogCb";
            this.RS485LogCb.Size = new System.Drawing.Size(152, 17);
            this.RS485LogCb.TabIndex = 11;
            this.RS485LogCb.Text = "Записывать обмен в лог";
            this.RS485LogCb.UseVisualStyleBackColor = true;
            // 
            // ScreensaverUrlTb
            // 
            this.ScreensaverUrlTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScreensaverUrlTb.Location = new System.Drawing.Point(6, 72);
            this.ScreensaverUrlTb.Name = "ScreensaverUrlTb";
            this.ScreensaverUrlTb.Size = new System.Drawing.Size(535, 20);
            this.ScreensaverUrlTb.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Адрес сайта:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.SetSilentBtn);
            this.groupBox1.Controls.Add(this.SetWailBtn);
            this.groupBox1.Controls.Add(this.SetSteadyBtn);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.SilentTb);
            this.groupBox1.Controls.Add(this.WailTb);
            this.groupBox1.Controls.Add(this.SteadyTb);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.RepeatCb);
            this.groupBox1.Controls.Add(this.DeleteBtn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.DeleteSelectedBtn);
            this.groupBox1.Controls.Add(this.numSelect);
            this.groupBox1.Controls.Add(this.AddBtn);
            this.groupBox1.Controls.Add(this.FilePathsTable);
            this.groupBox1.Location = new System.Drawing.Point(9, 246);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(547, 371);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Файлы";
            // 
            // SetSilentBtn
            // 
            this.SetSilentBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SetSilentBtn.Location = new System.Drawing.Point(496, 317);
            this.SetSilentBtn.Name = "SetSilentBtn";
            this.SetSilentBtn.Size = new System.Drawing.Size(42, 23);
            this.SetSilentBtn.TabIndex = 35;
            this.SetSilentBtn.Text = "Open";
            this.SetSilentBtn.UseVisualStyleBackColor = true;
            this.SetSilentBtn.Click += new System.EventHandler(this.SetSilentBtn_Click);
            // 
            // SetWailBtn
            // 
            this.SetWailBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SetWailBtn.Location = new System.Drawing.Point(495, 279);
            this.SetWailBtn.Name = "SetWailBtn";
            this.SetWailBtn.Size = new System.Drawing.Size(43, 23);
            this.SetWailBtn.TabIndex = 34;
            this.SetWailBtn.Text = "Open";
            this.SetWailBtn.UseVisualStyleBackColor = true;
            this.SetWailBtn.Click += new System.EventHandler(this.SetWailBtn_Click);
            // 
            // SetSteadyBtn
            // 
            this.SetSteadyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SetSteadyBtn.Location = new System.Drawing.Point(496, 241);
            this.SetSteadyBtn.Name = "SetSteadyBtn";
            this.SetSteadyBtn.Size = new System.Drawing.Size(43, 23);
            this.SetSteadyBtn.TabIndex = 21;
            this.SetSteadyBtn.Text = "Open";
            this.SetSteadyBtn.UseVisualStyleBackColor = true;
            this.SetSteadyBtn.Click += new System.EventHandler(this.SetSteadyBtn_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 304);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Тихий тест:";
            // 
            // SilentTb
            // 
            this.SilentTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SilentTb.Location = new System.Drawing.Point(6, 320);
            this.SilentTb.Name = "SilentTb";
            this.SilentTb.Size = new System.Drawing.Size(484, 20);
            this.SilentTb.TabIndex = 32;
            // 
            // WailTb
            // 
            this.WailTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WailTb.Location = new System.Drawing.Point(6, 281);
            this.WailTb.Name = "WailTb";
            this.WailTb.Size = new System.Drawing.Size(484, 20);
            this.WailTb.TabIndex = 31;
            // 
            // SteadyTb
            // 
            this.SteadyTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SteadyTb.Location = new System.Drawing.Point(6, 243);
            this.SteadyTb.Name = "SteadyTb";
            this.SteadyTb.Size = new System.Drawing.Size(484, 20);
            this.SteadyTb.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 265);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Прерывистая сирена:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 227);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Непрерывная сирена:";
            // 
            // RepeatCb
            // 
            this.RepeatCb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RepeatCb.AutoSize = true;
            this.RepeatCb.Location = new System.Drawing.Point(6, 346);
            this.RepeatCb.Name = "RepeatCb";
            this.RepeatCb.Size = new System.Drawing.Size(205, 17);
            this.RepeatCb.TabIndex = 29;
            this.RepeatCb.Text = "Повторять файл до команды отбоя";
            this.RepeatCb.UseVisualStyleBackColor = true;
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteBtn.Location = new System.Drawing.Point(254, 195);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(75, 23);
            this.DeleteBtn.TabIndex = 27;
            this.DeleteBtn.Text = "Удалить";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "N:";
            // 
            // DeleteSelectedBtn
            // 
            this.DeleteSelectedBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteSelectedBtn.Location = new System.Drawing.Point(386, 195);
            this.DeleteSelectedBtn.Name = "DeleteSelectedBtn";
            this.DeleteSelectedBtn.Size = new System.Drawing.Size(152, 23);
            this.DeleteSelectedBtn.TabIndex = 24;
            this.DeleteSelectedBtn.Text = "Удалить выделенные";
            this.DeleteSelectedBtn.UseVisualStyleBackColor = true;
            this.DeleteSelectedBtn.Click += new System.EventHandler(this.DeleteSelectedBtn_Click);
            // 
            // numSelect
            // 
            this.numSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numSelect.Location = new System.Drawing.Point(32, 198);
            this.numSelect.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numSelect.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSelect.Name = "numSelect";
            this.numSelect.Size = new System.Drawing.Size(54, 20);
            this.numSelect.TabIndex = 23;
            this.numSelect.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AddBtn
            // 
            this.AddBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddBtn.Location = new System.Drawing.Point(92, 195);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(156, 23);
            this.AddBtn.TabIndex = 22;
            this.AddBtn.Text = "Добавить или изменить";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // FilePathsTable
            // 
            this.FilePathsTable.AllowUserToResizeRows = false;
            this.FilePathsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilePathsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FilePathsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.path});
            this.FilePathsTable.Location = new System.Drawing.Point(9, 19);
            this.FilePathsTable.Name = "FilePathsTable";
            this.FilePathsTable.Size = new System.Drawing.Size(530, 170);
            this.FilePathsTable.TabIndex = 21;
            this.FilePathsTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FilePathsTable_CellDoubleClick);
            // 
            // number
            // 
            this.number.Frozen = true;
            this.number.HeaderText = "N";
            this.number.MinimumWidth = 50;
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.number.Width = 50;
            // 
            // path
            // 
            this.path.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.path.HeaderText = "Путь";
            this.path.Name = "path";
            this.path.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.SetVideoScreenSaverBtn);
            this.groupBox2.Controls.Add(this.ScreensaverTypeCb);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.WebRefreshNUD);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.ScreensaverVideoTb);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.ScreensaverUrlTb);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(9, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(547, 169);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Заставка";
            // 
            // SetVideoScreenSaverBtn
            // 
            this.SetVideoScreenSaverBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SetVideoScreenSaverBtn.Location = new System.Drawing.Point(495, 138);
            this.SetVideoScreenSaverBtn.Name = "SetVideoScreenSaverBtn";
            this.SetVideoScreenSaverBtn.Size = new System.Drawing.Size(43, 23);
            this.SetVideoScreenSaverBtn.TabIndex = 20;
            this.SetVideoScreenSaverBtn.Text = "Open";
            this.SetVideoScreenSaverBtn.UseVisualStyleBackColor = true;
            this.SetVideoScreenSaverBtn.Click += new System.EventHandler(this.SetVideoScreenSaverBtn_Click);
            // 
            // ScreensaverTypeCb
            // 
            this.ScreensaverTypeCb.FormattingEnabled = true;
            this.ScreensaverTypeCb.Items.AddRange(new object[] {
            "Нет",
            "Веб-страница",
            "Видео",
            "Сворачивать программу"});
            this.ScreensaverTypeCb.Location = new System.Drawing.Point(6, 32);
            this.ScreensaverTypeCb.Name = "ScreensaverTypeCb";
            this.ScreensaverTypeCb.Size = new System.Drawing.Size(170, 21);
            this.ScreensaverTypeCb.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Тип заставки:";
            // 
            // WebRefreshNUD
            // 
            this.WebRefreshNUD.Location = new System.Drawing.Point(147, 95);
            this.WebRefreshNUD.Maximum = new decimal(new int[] {
            2147483,
            0,
            0,
            0});
            this.WebRefreshNUD.Name = "WebRefreshNUD";
            this.WebRefreshNUD.Size = new System.Drawing.Size(91, 20);
            this.WebRefreshNUD.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Период обновления, сек:";
            // 
            // ScreensaverVideoTb
            // 
            this.ScreensaverVideoTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScreensaverVideoTb.Location = new System.Drawing.Point(6, 140);
            this.ScreensaverVideoTb.Name = "ScreensaverVideoTb";
            this.ScreensaverVideoTb.Size = new System.Drawing.Size(487, 20);
            this.ScreensaverVideoTb.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Видеофайл:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Объектовый номер:";
            // 
            // ObjNumberNud
            // 
            this.ObjNumberNud.Location = new System.Drawing.Point(127, 45);
            this.ObjNumberNud.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ObjNumberNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ObjNumberNud.Name = "ObjNumberNud";
            this.ObjNumberNud.Size = new System.Drawing.Size(58, 20);
            this.ObjNumberNud.TabIndex = 28;
            this.ObjNumberNud.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 658);
            this.Controls.Add(this.ObjNumberNud);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RS485LogCb);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RefreshPortList);
            this.Controls.Add(this.PortList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.ShowInTaskbar = false;
            this.Text = "Настройки приложения";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FilePathsTable)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WebRefreshNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ObjNumberNud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RefreshPortList;
        private System.Windows.Forms.ComboBox PortList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.CheckBox RS485LogCb;
        private System.Windows.Forms.TextBox ScreensaverUrlTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView FilePathsTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
        private System.Windows.Forms.CheckBox RepeatCb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button SetVideoScreenSaverBtn;
        private System.Windows.Forms.ComboBox ScreensaverTypeCb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown WebRefreshNUD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ScreensaverVideoTb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown ObjNumberNud;
        private System.Windows.Forms.Button SetSilentBtn;
        private System.Windows.Forms.Button SetWailBtn;
        private System.Windows.Forms.Button SetSteadyBtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox SilentTb;
        private System.Windows.Forms.TextBox WailTb;
        private System.Windows.Forms.TextBox SteadyTb;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button DeleteSelectedBtn;
        private System.Windows.Forms.NumericUpDown numSelect;
        private System.Windows.Forms.Button AddBtn;
    }
}