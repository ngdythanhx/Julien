using Julian.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian
{
    public partial class frmXNYVConfig : Form
    {
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        public frmXNYVConfig()
        {
            InitializeComponent();
        }

        private void frmXNYVConfig_Load(object sender, EventArgs e)
        {
            foreach (TextBox textBox in this.Controls.OfType<TextBox>())
            {
                textBox.Text = _iniManager.GetString("XacNhanYVai", textBox.Name.Substring(3));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (TextBox textBox in this.Controls.OfType<TextBox>())
            {
                _iniManager.WriteString("XacNhanYVai", textBox.Name.Substring(3),textBox.Text.Trim().ToUpper());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
