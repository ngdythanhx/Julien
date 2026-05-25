using ClosedXML.Excel;
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

namespace Julian_Server
{
    public partial class frmThoaBeSHD : Form
    {
        string _filePath_OF = "";
        XLWorkbook workbook_Order = null;
        IniManager _iniManager = new IniManager(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        List<SHDFilter> _lstSHDFilter = new List<SHDFilter>();
        List<IXLRangeRow> _lstRangeRows = new List<IXLRangeRow>();
        List<IXLRangeRow> _lstFilteredRangeRows = new List<IXLRangeRow>();
        public frmThoaBeSHD()
        {
            InitializeComponent();
        }
        private void SetStatus(string status)
        {
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Text = status;
            });
        }
        private void EnableControl(bool enable)
        {
            cbSheet.Enabled = enable;
            chkView.Enabled = enable;
            gbPO.Enabled = enable;
            gbCode.Enabled = enable;
            gbQty.Enabled = enable;
            gbDeliveryDate.Enabled = enable;
            btnSave.Enabled = enable;
            //txtMaDonKH.Enabled = enable;
            //txtMaHang.Enabled = enable;
            //txtNgayXuatHang.Enabled = enable;
            //txtGhiChuXuatHang.Enabled = enable;
        }
        private bool CheckReadWriteFile(string filePath)
        {
            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return true;
                }
            }
            catch (IOException)
            {

                return false;
            }
        }
        private async void btnSelectFile_OrderForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label1;
                btnSelectFile_OrderForm.Enabled = false;
                this.Enabled = false;
                btnSelectFile_OrderForm.Enabled = true;
                var dl = new OpenFileDialog();
                dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dl.FileName;
                    string fileName = System.IO.Path.GetFileName(selectedPath);
                    string extension = System.IO.Path.GetExtension(selectedPath);
                    if (extension.ToLower() != ".xlsx")
                    {
                        MessageBox.Show("File không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!CheckReadWriteFile(selectedPath))
                    {
                        MessageBox.Show("File đang mở, đóng lại trước khi thao tác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    workbook_Order?.Dispose();
                    workbook_Order = null;
                    SetStatus("Đang load OrderForm...");
                    await Task.Run(() =>
                    {
                        workbook_Order?.Dispose();
                        _filePath_OF = selectedPath;
                        workbook_Order = new XLWorkbook(selectedPath);
                    });
                    var worksheets = workbook_Order.Worksheets.ToList();
                    lblFileName_OrderForm.Text = fileName;
                    cbSheet.DataSource = worksheets;
                    cbSheet.DisplayMember = "Name";
                    if (workbook_Order != null)
                    {
                        EnableControl(true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSelectFile_OrderForm.Enabled = true;
                SetStatus("...");
            }
        }
        private DataTable GetTable(List<IXLRangeRow> rangeRows)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PODate", typeof(string));
            dt.Columns.Add("PO", typeof(string));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Qty", typeof(double));
            dt.Columns.Add("RequestedDeliveryDate", typeof(DateTime));
            if (rangeRows != null)
                foreach (var row in rangeRows)
                {
                    dt.Rows.Add
                    (
                        row.Cell(txtConfig_PODate.Text).TryGetValue<DateTime>(out DateTime poDate) ? poDate.Date : DateTime.MinValue.Date,
                        row.Cell(txtConfig_PO.Text).GetString(),
                        row.Cell(txtConfig_Code.Text).GetString(),
                        row.Cell(txtConfig_Qty.Text).TryGetValue<double>(out double qty) ? qty : -1,
                        row.Cell(txtConfig_DeliveryNote.Text).GetString()
                    );
                }
            return dt;
        }
        private void btnSelectSheet_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetTable(_lstRangeRows);
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
