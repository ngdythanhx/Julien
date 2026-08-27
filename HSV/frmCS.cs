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
    public partial class frmCS : Form
    {
        private bool _lockItemCheck = false;
        private bool _lockCheckAll = false;
        Dictionary<string, bool> _dictSource = new Dictionary<string, bool>();
        public frmCS()
        {
            InitializeComponent();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
          /*  if (_lockCheckAll)
                return;
            _lockItemCheck = true;

            for (int i = 0; i < lsbData.Items.Count; i++)
            {
                _dictSource[lsbData.Items[i].ToString()] = chkAll.Checked;
                lsbData.SetItemChecked(i, chkAll.Checked);
            }
            _lockItemCheck = false;
            ItemCheckedChange?.Invoke();*/
        }
    }
}
