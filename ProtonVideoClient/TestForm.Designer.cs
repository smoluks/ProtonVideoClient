namespace ProtonVideoClient
{
    partial class TestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.StartSelect = new System.Windows.Forms.RadioButton();
            this.StopSelect = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.CommandField = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ArgumentField = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CommandField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArgumentField)).BeginInit();
            this.SuspendLayout();
            // 
            // StartSelect
            // 
            this.StartSelect.AutoSize = true;
            this.StartSelect.Checked = true;
            this.StartSelect.Location = new System.Drawing.Point(80, 12);
            this.StartSelect.Name = "StartSelect";
            this.StartSelect.Size = new System.Drawing.Size(34, 17);
            this.StartSelect.TabIndex = 0;
            this.StartSelect.TabStop = true;
            this.StartSelect.Text = "1-";
            this.StartSelect.UseVisualStyleBackColor = true;
            // 
            // StopSelect
            // 
            this.StopSelect.AutoSize = true;
            this.StopSelect.Location = new System.Drawing.Point(80, 35);
            this.StopSelect.Name = "StopSelect";
            this.StopSelect.Size = new System.Drawing.Size(34, 17);
            this.StopSelect.TabIndex = 1;
            this.StopSelect.Text = "3-";
            this.StopSelect.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Команда";
            // 
            // CommandField
            // 
            this.CommandField.Location = new System.Drawing.Point(120, 21);
            this.CommandField.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.CommandField.Name = "CommandField";
            this.CommandField.Size = new System.Drawing.Size(69, 20);
            this.CommandField.TabIndex = 3;
            this.CommandField.Value = new decimal(new int[] {
            26,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Аргумент";
            // 
            // ArgumentField
            // 
            this.ArgumentField.Location = new System.Drawing.Point(294, 21);
            this.ArgumentField.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ArgumentField.Name = "ArgumentField";
            this.ArgumentField.Size = new System.Drawing.Size(69, 20);
            this.ArgumentField.TabIndex = 5;
            this.ArgumentField.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(288, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Старт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 107);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ArgumentField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CommandField);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StopSelect);
            this.Controls.Add(this.StartSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestForm";
            this.ShowInTaskbar = false;
            this.Text = "Проверка реакции на команды оповещения";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CommandField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArgumentField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton StartSelect;
        private System.Windows.Forms.RadioButton StopSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown CommandField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ArgumentField;
        private System.Windows.Forms.Button button1;
    }
}