using ProtonRS485Client.Data;
using System;
using System.Windows.Forms;
using static ProtonRS485Client.Data.ProtonMessage;

namespace ProtonVideoClient
{
    public delegate void ProcessCommandDelegate(ProtonMessage message);
    public partial class TestForm : Form
    {
        ProcessCommandDelegate _processCommand;
        public TestForm(ProcessCommandDelegate processCommand)
        {
            _processCommand = processCommand;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _processCommand(new ProtonMessage((CommandCodeEnum)CommandField.Value, (StartSelect.Checked ? CommandCodePrefixEnum.On : CommandCodePrefixEnum.Off), (byte)ArgumentField.Value));
        }

    }
}
