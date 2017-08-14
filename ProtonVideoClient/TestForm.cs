using System;
using System.Windows.Forms;

namespace ProtonVideoClient
{
    public delegate void ProcessCommandDelegate(byte command, byte arg, bool start);
    public partial class TestForm : Form
    {
        ProcessCommandDelegate _processCommand;
        public TestForm(ProcessCommandDelegate processCommand)
        {
            _processCommand = processCommand;
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _processCommand((byte)CommandField.Value, (byte)ArgumentField.Value, StartSelect.Checked);
        }
    }
}
