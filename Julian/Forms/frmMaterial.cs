using  Julian.Database.DAO;
using  Julian.Database.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace  Julian.Forms
{
    public partial class frmMaterial : Form, IFormHandler
    {
        public int ActionType { get => _actionType; set => _actionType = value; }
        private int _actionType;
        List<Material> Materials;
        Material _curMaterial;
        public frmMaterial()
        {
            InitializeComponent();
            LockInputs();
            this.Text = "Vật Liệu";
            dgvMaterials.AutoGenerateColumns = false;
        }
        public void LoadData()
        {
            Materials = MaterialDAO.Instance.GetMaterials();
            dgvMaterials.DataSource = Materials;
            if (dgvMaterials.Rows.Count == 0)
                ClearUIData();
        }
        private void ClearUIData()
        {
            foreach (TextBox textBox in tableLayoutPanel2.Controls.OfType<TextBox>())
            {
                textBox.Text = "...";
            }
        }
        public void LockInputs()
        {
            foreach (TextBox textBox in tableLayoutPanel2.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = true;
                textBox.BorderStyle = BorderStyle.None;
                textBox.Margin = new Padding(6, 6, 1, 0);
            }
            dgvMaterials.Enabled = true;
        }
        public void UnlockInputs()
        {
            foreach (TextBox textBox in tableLayoutPanel2.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = false;
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Margin = new Padding(3, 3, 1, 1);
            }
            dgvMaterials.Enabled = false;
        }
        public void ResetInputs()
        {
            foreach (TextBox textBox in tableLayoutPanel2.Controls.OfType<TextBox>())
            {
                textBox.Text = "";
            }
            txtMaterialCode.Focus();
        }
        private bool ValidateData()
        {
            foreach (TextBox textBox in tableLayoutPanel2.Controls.OfType<TextBox>())
            {
                if (textBox.Name != "txtDescription" && string.IsNullOrEmpty(textBox.Text))
                {
                    toolTip1.Show("Không thể bỏ trống!", textBox, textBox.Width, 0, 1500);
                    textBox.Focus();
                    return false;
                }
            }
            return true;
        }
        private void dgvMaterials_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMaterials.CurrentRow?.DataBoundItem is Material em)
            {
                _curMaterial = em;
                LockInputs();

                txtMaterialCode.Text = em.MaterialCode;
                txtMaterialColor.Text = em.MaterialColor;
                txtCustomerColor.Text = em.CustomerColor;
                txtStandardColor.Text = em.StandardColor;
                txtDescription.Text = em.Description;
            }
            else
                _curMaterial = null;
        }
        public bool CreateData()
        {
            if (!ValidateData())
            {
                return false;
            }
            string code = txtMaterialCode.Text;
            string maColor = txtMaterialColor.Text;
            string cusColor = txtCustomerColor.Text;
            string saColoer = txtStandardColor.Text;
            string des = txtDescription.Text;
            var response = MaterialDAO.Instance.CreateMaterial(code, maColor, cusColor,saColoer,des);
            if ((response.ErrCode == 0))
            {
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LockInputs();
                return true;
            }
            else
            {
                MessageBox.Show(response.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaterialCode.Focus();
                return false;
            }
        }
        public bool UpdateData()
        {
            if (_curMaterial == null)
            {
                MessageBox.Show("Không có dữ liệu Nhân Viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!ValidateData())
            {
                return false;
            }
            int id = _curMaterial.Id;
            string code = txtMaterialCode.Text;
            string maColor = txtMaterialColor.Text;
            string cusColor = txtCustomerColor.Text;
            string saColoer = txtStandardColor.Text;
            string des = txtDescription.Text;
            var response = MaterialDAO.Instance.UpdateMaterial(id, code, maColor, cusColor, saColoer, des);
            if ((response.ErrCode == 0))
            {
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LockInputs();
                return true;
            }
            else
            {
                MessageBox.Show(response.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaterialCode.Focus();
                return false;
            }
        }
        public bool DeleteData()
        {
            if (_curMaterial == null) return false;
            try
            {
                if (MessageBox.Show($"'{_curMaterial.MaterialCode}' và các dữ liệu liên quan(Nhiệm Vụ,..) sẽ bị xóa vĩnh viễn.\nĐồng ý?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    var response = MaterialDAO.Instance.DeleteMaterial(_curMaterial.Id);
                    if ((response.ErrCode == 0))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        LockInputs();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(response.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
