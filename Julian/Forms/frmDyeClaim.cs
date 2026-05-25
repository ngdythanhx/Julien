using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian.Forms
{
    public partial class frmDyeClaim : Form, IFormHandler
    {
        public int ActionType { get => _actionType; set => _actionType = value; }
        private int _actionType;
        public frmDyeClaim()
        {
            InitializeComponent();
        }
        public void LockInputs()
        {
            foreach (TextBox textBox in gb1.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = true;
            }
            btnExportWareHouseData.Enabled = false;
            dgvMonth.Enabled = true;
            dgvClaim.Enabled = true;
        }
        public void UnlockInputs()
        {
            foreach (TextBox textBox in gb1.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = false;
            }
            btnExportWareHouseData.Enabled = ActionType == 1;
            dgvMonth.Enabled = false;
            dgvClaim.Enabled = false;
        }
        public void ResetInputs()
        {
            foreach (TextBox textBox in gb1.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = false;
                textBox.Text = "";
            }
        }
        private bool ValidateData()
        {
            TextBox[] textBoxNotNull = new TextBox[]
            {
                txtDyePO,
                txtDyeCompanyCode,
                txtMaterialCode,
            };
            /*if (string.IsNullOrEmpty(taskName))
            {
                toolTip1.Show("Không thể bỏ trống!", txtTaskName, txtTaskName.Width, 0, 1500);
                txtTaskName.Focus();
                return false;
            }
            string description = txtDescription.Text;
            if (string.IsNullOrEmpty(description))
            {
                toolTip1.Show("Không thể bỏ trống!", txtDescription, txtDescription.Width, 0, 1500);
                txtDescription.Focus();
                return false;
            }*/
            return true;
        }
        public void LoadData()
        {
        }
        public bool CreateData()
        {
            return true;
        }
        public bool UpdateData()
        {
            return true;
        }
        public bool DeleteData()
        {
            return true;
        }
    }
}
