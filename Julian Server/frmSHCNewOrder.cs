using Julian;
using Julian.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian_Server
{
    public partial class frmSHCNewOrder : Form
    {
        public frmSHCNewOrder()
        {
            InitializeComponent();
        }

        private void frmSHCNewOrder_Load(object sender, EventArgs e)
        {
            Config.Instance.ConnectionString = _iniManager.GetString("Database", "ConnectionString", "Data Source=DESKTOP-TLNO6C7;Initial Catalog=Julian;User ID=client;Password=dythanh94@;Connect Timeout=1;");
        }
    }
}
