using ERP.Database.DAO;
using ERP.Models;
using JServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Forms
{
    public partial class frmCS_Modify : Form
    {
        ServerAPI serverAPI = new ServerAPI();
        int _type = 0;
        private List<JServer.Models.Customer> _lstCustFromServer = new List<JServer.Models.Customer>();
        private List<JServer.Models.Material> _lstMaterialFromServer = new List<JServer.Models.Material>();
        public frmCS_Modify(List<JServer.Models.Customer> lstCustFromServer, List<JServer.Models.Material> lstMaterialFromServer)
        {
            _lstCustFromServer = lstCustFromServer;
            _lstMaterialFromServer = lstMaterialFromServer;
            InitializeComponent();
        }
        public frmCS_Modify(List<JServer.Models.Customer> lstCustFromServer, List<JServer.Models.Material> lstMaterialFromServer, CSData cSData)
        {
            _lstCustFromServer = lstCustFromServer;
            _lstMaterialFromServer = lstMaterialFromServer;
            InitializeComponent();
            txtID.Text = cSData.ID.ToString();
        }
        public frmCS_Modify()
        {
            InitializeComponent();
            _type = 1;
            txtID.Enabled = false;
        }
        public frmCS_Modify(CSData cSData)
        {
            InitializeComponent();
            txtID.Text = cSData.ID.ToString();
            txtCustName.Text = cSData.CustCode;
            txtMaterialName.Text = cSData.MaterialName;
            txtColor.Text = cSData.Color;
            txtReplacementColor.Text = cSData.ReplacementColor;
            txtArticle.Text = cSData.Article;
            //txtSwatchLocation.Text = cSData.SwatchLocation;
            txtFollow.Text = cSData.Follow;
            txtNote.Text = cSData.Note;
            txtAdditionalOptional1.Text = cSData.AdditionalOptional1;
            txtAdditionalOptional2.Text = cSData.AdditionalOptional2;
            txtAdditionalOptional3.Text = cSData.AdditionalOptional3;
            _type = 2;
        }
        private void frmCS_Modify_Load(object sender, EventArgs e)
        {

        }
        private bool _isLoadDataFromServer { get; set; } = false;
        public async Task<bool> LoadDataFromServer()
        {
            if (_isLoadDataFromServer)
                return true;
            try
            {
                ServerAPI serverAPI = new ServerAPI();
                var login = await serverAPI.Login("b019", "b019");
                if (login.Success)
                {
                    var task1 = serverAPI.GetMaterialListAsync();
                    var task2 = serverAPI.GetCustomerListAsync();
                    await Task.WhenAll(task1, task2);
                    _lstMaterialFromServer = task1.Result;
                    _lstCustFromServer = task2.Result;
                    if (_lstMaterialFromServer != null && _lstMaterialFromServer.Count > 0 && _lstCustFromServer != null && _lstCustFromServer.Count > 0)
                    {
                        _isLoadDataFromServer = true;
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> CheckCustomer(string custName)
        {
            return await LoadDataFromServer() && _lstCustFromServer.Any(x => x.CustName.ToUpper() == custName.ToUpper());
        }
        private async Task<bool> CheckMaterial(string materialName)
        {
            return await LoadDataFromServer() && _lstMaterialFromServer.Any(x => x.Name.ToUpper() == materialName.ToUpper());
        }
        private async Task<bool> CheckInputs()
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustName.Text.Trim()))
                {
                    txtCustName.Focus();
                    toolTip1.Show("Vui lòng điền tên Khách hàng", txtCustName, txtCustName.Width - 5, txtCustName.Height - 5, 1500);
                    return false;
                }
                if (!await CheckCustomer(txtCustName.Text.Trim()))
                {
                    txtCustName.Focus();
                    toolTip1.Show("Mã khách hàng không tồn tại trên CSDL ERP", txtCustName, txtCustName.Width - 5, txtCustName.Height - 5, 1500);
                    return false;
                }
                if (string.IsNullOrEmpty(txtMaterialName.Text.Trim()))
                {
                    txtMaterialName.Focus();
                    txtMaterialName.Focus();
                    toolTip1.Show("Vui lòng điền tên Liệu", txtMaterialName, txtMaterialName.Width - 5, txtMaterialName.Height - 5, 1500);
                    return false;
                }
                if (!await CheckMaterial(txtMaterialName.Text.Trim()))
                {
                    txtMaterialName.Focus();
                    toolTip1.Show("Mã Liệu không tồn tại trên CSDL ERP", txtMaterialName, txtMaterialName.Width - 5, txtMaterialName.Height - 5, 1500);
                    return false;
                }
                if (string.IsNullOrEmpty(txtColor.Text.Trim()))
                {
                    txtColor.Focus();
                    toolTip1.Show("Vui lòng điền Màu sắc", txtColor, txtColor.Width - 5, txtColor.Height - 5, 1500);
                    return false;
                }
                if (string.IsNullOrEmpty(txtFollow.Text.Trim()))
                {
                    txtFollow.Focus();
                    toolTip1.Show("Vui lòng điền Follow, nếu chưa có điền CS", txtFollow, txtFollow.Width - 5, txtFollow.Height - 5, 1500);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private async void InsertCS()
        {
            try
            {
                if (!await CheckInputs())
                    return;
                var rp = await CSDataDAO.Instance.Insert(
                    txtCustName.Text.Trim(),
                    txtMaterialName.Text.Trim(),
                    _lstMaterialFromServer.First(x => x.Name.ToUpper() == txtMaterialName.Text.Trim().ToUpper()).DNWidth,
                    txtColor.Text.Trim(),
                    txtReplacementColor.Text.Trim(),
                    txtFollow.Text.Trim(),
                    txtNote.Text.Trim(),
                    txtArticle.Text.Trim(),
                    0,
                    txtAdditionalOptional1.Text.Trim(),
                    txtAdditionalOptional2.Text.Trim(),
                    txtAdditionalOptional3.Text.Trim(),
                    DateTime.Now);
                if (rp.ErrCode != 0)
                {
                    MessageBox.Show(rp.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(rp.Msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaterialName.Text = "";
                    txtColor.Text = "";
                    txtReplacementColor.Text = "";
                    txtFollow.Text = "";
                    txtNote.Text = "";
                    txtArticle.Text = "";
                    txtAdditionalOptional1.Text = "";
                    txtAdditionalOptional2.Text = "";
                    txtAdditionalOptional3.Text = "";

                    txtMaterialName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void UpdateCS()
        {
            try
            {
                if (!await CheckInputs())
                    return;
                var rp = await CSDataDAO.Instance.Update(
                    int.TryParse(txtID.Text.Trim(), out int id) ? id : 0,
                    txtCustName.Text.Trim(),
                    txtMaterialName.Text.Trim(),
                    _lstMaterialFromServer.First(x => x.Name.ToUpper() == txtMaterialName.Text.Trim().ToUpper()).DNWidth,
                    txtColor.Text.Trim(),
                    txtReplacementColor.Text.Trim(),
                    txtFollow.Text.Trim(),
                    txtNote.Text.Trim(),
                    txtArticle.Text.Trim(),
                    0,
                    txtAdditionalOptional1.Text.Trim(),
                    txtAdditionalOptional2.Text.Trim(),
                    txtAdditionalOptional3.Text.Trim(),
                    DateTime.Now);
                if (rp.ErrCode != 0)
                {
                    MessageBox.Show(rp.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(rp.Msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    /*txtMaterialName.Text = "";
                    txtColor.Text = "";
                    txtReplacementColor.Text = "";
                    txtFollow.Text = "";
                    txtNote.Text = "";
                    txtArticle.Text = "";
                    txtSwatchLocation.Text = "";
                    txtAdditionalOptional1.Text = "";
                    txtAdditionalOptional2.Text = "";
                    txtAdditionalOptional3.Text = "";
                    */
                    txtMaterialName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if(control is TextBox)
                {
                    control.Enabled = false;
                }
            }
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            try
            {
                if (_type == 1)
                {
                    if (!await CheckInputs())
                        return;
                    var rp = await CSDataDAO.Instance.Insert(
                        txtCustName.Text.Trim(),
                        txtMaterialName.Text.Trim(),
                        _lstMaterialFromServer.First(x => x.Name.ToUpper() == txtMaterialName.Text.Trim().ToUpper()).DNWidth,
                        txtColor.Text.Trim(),
                        txtReplacementColor.Text.Trim(),
                        txtFollow.Text.Trim(),
                        txtNote.Text.Trim(),
                        txtArticle.Text.Trim(),
                        0,
                        txtAdditionalOptional1.Text.Trim(),
                        txtAdditionalOptional2.Text.Trim(),
                        txtAdditionalOptional3.Text.Trim(),
                        DateTime.Now);
                    if (rp.ErrCode != 0)
                    {
                        MessageBox.Show(rp.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(rp.Msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMaterialName.Text = "";
                        txtColor.Text = "";
                        txtReplacementColor.Text = "";
                        txtFollow.Text = "";
                        txtNote.Text = "";
                        txtArticle.Text = "";
                        txtAdditionalOptional1.Text = "";
                        txtAdditionalOptional2.Text = "";
                        txtAdditionalOptional3.Text = "";

                        txtMaterialName.Focus();
                    }
                }
                if (_type == 2)
                {
                    if (!await CheckInputs())
                        return;
                    var rp = await CSDataDAO.Instance.Update(
                        int.TryParse(txtID.Text.Trim(), out int id) ? id : 0,
                        txtCustName.Text.Trim(),
                        txtMaterialName.Text.Trim(),
                        _lstMaterialFromServer.First(x => x.Name.ToUpper() == txtMaterialName.Text.Trim().ToUpper()).DNWidth,
                        txtColor.Text.Trim(),
                        txtReplacementColor.Text.Trim(),
                        txtFollow.Text.Trim(),
                        txtNote.Text.Trim(),
                        txtArticle.Text.Trim(),
                        0,
                        txtAdditionalOptional1.Text.Trim(),
                        txtAdditionalOptional2.Text.Trim(),
                        txtAdditionalOptional3.Text.Trim(),
                        DateTime.Now);
                    if (rp.ErrCode != 0)
                    {
                        MessageBox.Show(rp.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(rp.Msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        /*txtMaterialName.Text = "";
                        txtColor.Text = "";
                        txtReplacementColor.Text = "";
                        txtFollow.Text = "";
                        txtNote.Text = "";
                        txtArticle.Text = "";
                        txtSwatchLocation.Text = "";
                        txtAdditionalOptional1.Text = "";
                        txtAdditionalOptional2.Text = "";
                        txtAdditionalOptional3.Text = "";
                        */
                        txtMaterialName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                foreach (Control control in this.Controls)
                {
                    if (control is TextBox)
                    {
                        control.Enabled = true;
                    }
                }
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
