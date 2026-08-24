using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSV
{
    public partial class frmMain2 : Form
    {
        public frmMain2()
        {
            InitializeComponent();
        }

        private void frmMain2_Load(object sender, EventArgs e)
        {
            frmINV frmInv = new frmINV();
            frmInv.FormBorderStyle = FormBorderStyle.None;
            tabPage1.Controls.Add(frmInv);
        }
    }
}
