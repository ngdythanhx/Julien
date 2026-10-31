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
    public partial class frmCSModify : Form
    {
        private int _actionType = 1;
        private CS _cs = null;
        public frmCSModify(int actionType)
        {
            InitializeComponent();
            _actionType = actionType;
            this.Text = "Thêm dữ liệu CS";
            lblID.Text = "N/A";
        }
        public frmCSModify(int actionType, CS cs)
        {
            InitializeComponent();
            _actionType = actionType;
            _cs = cs;
            this.Text = "Sửa dữ liệu CS";
            lblID.Text = cs.ID.ToString();
            txtArticle.Text = cs.Article;
            txtMaterial.Text = cs.Material;
            txtColor.Text = cs.Color;
            txtFollow.Text = cs.Follow;
            txtPurchaseSpecification.Text = cs.PurchaseSpecification;
            txtNote.Text = cs.Note;
            txtLocation.Text = cs.SwatchLocation;
            rbYes.Checked = cs.Confirmed;
        }
        private void EnableTextBox(bool enable)
        {
            txtArticle.ReadOnly = enable;
            txtMaterial.ReadOnly = enable;
            txtColor.ReadOnly = enable;
            txtFollow.ReadOnly = enable;
            txtPurchaseSpecification.ReadOnly = enable;
            txtNote.ReadOnly = enable;
        }
        public void LockInputs()
        {
            foreach (TextBox textBox in this.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = true;
                //textBox.BorderStyle = BorderStyle.None;
                //textBox.Margin = new Padding(6, 6, 1, 0);
            }
            rbYes.AutoCheck = false;
            rbNo.AutoCheck = false;
        }

        public void UnlockInputs()
        {
            foreach (TextBox textBox in this.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = false;
                textBox.BorderStyle = BorderStyle.None;
                //textBox.Margin = new Padding(6, 6, 1, 0);
            }
            rbYes.AutoCheck = true;
            rbNo.AutoCheck = true;
        }
        public void ResetInputs()
        {
            foreach (TextBox textBox in this.Controls.OfType<TextBox>())
            {
                textBox.Text = "";
            }
            rbYes.Checked = false;
            rbNo.Checked = true;
        }
        private (bool success, string msg) ValidateInputData()
        {
            string art = txtArticle.Text.Trim();
            if (art.Length != 6)
            {
                return (false, "Article không đúng!");
            }
            string mtl = txtMaterial.Text.Trim().ToUpper();
            if (mtl.Length < 5)
            {
                return (false, "Tên liệu không hợp lệ!");
            }
            if(_actionType==2 && _cs==null)
            {
                return (false, "Có lỗi dữ liệu đầu vào!");
            }    
            return (true, "");
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            try
            {
                var check = ValidateInputData();
                if(!check.success)
                {
                    MessageBox.Show(check.msg);
                    return;
                }
                if (_actionType == 1)
                {
                    var rsp = await CSDATA.Instance.Add(
                          txtArticle.Text.Trim().ToUpper(),
                          txtMaterial.Text.Trim().ToUpper(),
                          txtColor.Text.Trim().ToUpper(),
                          txtFollow.Text,
                          txtPurchaseSpecification.Text.Trim(),
                          txtNote.Text,
                          txtLocation.Text,
                          rbYes.Checked
                    );
                    MessageBox.Show(rsp.Msg);
                    if (rsp.ErrCode == 0)
                    {
                        ResetInputs();
                    }
                }
                else if (_actionType == 2)
                {
                    var rsp = await CSDATA.Instance.Update(
                          _cs.ID,
                          txtArticle.Text.Trim().ToUpper(),
                          txtMaterial.Text.Trim().ToUpper(),
                          txtColor.Text.Trim().ToUpper(),
                          txtFollow.Text,
                          txtPurchaseSpecification.Text.Trim(),
                          txtNote.Text,
                          txtLocation.Text,
                          rbYes.Checked
                    );
                    MessageBox.Show(rsp.Msg);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
