using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace ERP.Forms
{
    public partial class frmCheckOrderInput : Form
    {
        public frmCheckOrderInput()
        {
            InitializeComponent();
        }
        private string SelectFilePath()
        {
            btnSelectFile_OrderForm.Enabled = false;
            var dl = new OpenFileDialog();
            dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (dl.ShowDialog() == DialogResult.OK)
            {
                return dl.FileName;
            }
            return "";
        }
        private async void btnSelectFile_OrderForm_Click(object sender, EventArgs e)
        {


        }
    }
}
