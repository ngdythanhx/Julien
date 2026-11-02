using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ERP.Database.DAO;
namespace ERP.Forms
{
    public partial class frmCS : Form
    {
        public frmCS()
        {
            InitializeComponent();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
          var rp =await  CSDataDAO.Instance.Delete(1);
        }
    }
}
