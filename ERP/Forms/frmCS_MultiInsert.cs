using ERP.Database.DAO;
using ERP.Models;
using ExcelDataReader;
using JServer;
using MiniExcelLibs;
using MiniExcelLibs.OpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Forms
{
    public partial class frmCS_MultiInsert : Form
    {
        public frmCS_MultiInsert()
        {
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dgvMain, true, null);
            dgvMain.AutoGenerateColumns = false;
        }
        private class CSImport
        {
            public string CustName { get; set; } = "";
            public string MaterialName { get; set; } = "";
            public string Color { get; set; } = "";
            public string ReplacementColor { get; set; } = "";
            public string Follow { get; set; } = "";
            public string Note { get; set; } = "";
            public string Article { get; set; } = "";
            public string Confirmed { get; set; } = "";
            public string AdditionalOptional1 { get; set; } = "";
            public string AdditionalOptional2 { get; set; } = "";
            public string AdditionalOptional3 { get; set; } = "";
            public string Check { get; set; } = "";
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnUpload.Enabled = false;
            btnDownloadFile.Enabled = false;
            try
            {
                var dl = new OpenFileDialog();
                dl.Filter = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls|All files (*.*)|*.*";
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dl.FileName;
                    string fileName = System.IO.Path.GetFileName(selectedPath);
                    string extension = System.IO.Path.GetExtension(selectedPath);
                    if (extension.ToLower() != ".xlsx" && extension.ToLower() != ".xls")
                    {
                        MessageBox.Show("File không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    dgvMain.DataSource = null;
                    var lst = new List<CSImport>();
                    using (var stream = File.Open(selectedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        reader.Read();
                        while (reader.Read())
                        {
                            var cs = new CSImport()
                            {
                                CustName = reader.GetValue(0)?.ToString().ToUpper() ?? "",
                                MaterialName = reader.GetValue(1)?.ToString().ToUpper() ?? "",
                                Color = reader.GetValue(2)?.ToString().ToUpper() ?? "",
                                ReplacementColor = reader.GetValue(3)?.ToString().ToUpper() ?? "",
                                Follow = reader.GetValue(4)?.ToString().ToUpper()??"",
                                Note = reader.GetValue(5)?.ToString() ?? "",
                                Article = reader.GetValue(6)?.ToString().ToUpper() ?? "",
                                Confirmed = reader.GetValue(7)?.ToString() ?? "",
                                AdditionalOptional1 = reader.GetValue(8)?.ToString() ?? "",
                                AdditionalOptional2 = reader.GetValue(9)?.ToString() ?? "",
                                AdditionalOptional3 = reader.GetValue(10)?.ToString() ?? "",
                            };
                            if (string.IsNullOrEmpty(cs.CustName) &&
                                string.IsNullOrEmpty(cs.MaterialName) &&
                                string.IsNullOrEmpty(cs.Color) &&
                                string.IsNullOrEmpty(cs.ReplacementColor) &&
                                string.IsNullOrEmpty(cs.Follow) &&
                                string.IsNullOrEmpty(cs.Note) &&
                                string.IsNullOrEmpty(cs.Article) &&
                                string.IsNullOrEmpty(cs.Confirmed))
                                continue;
                            lst.Add(cs);
                        }
                    }
                    dgvMain.DataSource = lst;
                    lblRowCount.Text = lst.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnStart.Enabled = true;
                btnUpload.Enabled = true;
                btnDownloadFile.Enabled = true;
            }
        }
        private List<JServer.Models.Customer> _lstCustFromServer = new List<JServer.Models.Customer>();
        private List<JServer.Models.Material> _lstMaterialFromServer = new List<JServer.Models.Material>();
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
        private void CheckInputs(CSImport csImport)
        {
            //CustName
            if (string.IsNullOrEmpty(csImport.CustName))
            {
                csImport.Check = "Mã K.H = 'blank'";
                return;
            }
            if (!_lstCustFromServer.Any(x => x.CustName.ToUpper() == csImport.CustName.ToUpper()))
            {
                csImport.Check = "Mã K.H không tồn tại trên hệ thống ERP";
                return;
            }
            //MaterialName
            if (string.IsNullOrEmpty(csImport.MaterialName))
            {
                csImport.Check = "Mã Liệu = 'blank'";
                return;
            }
            if (!_lstMaterialFromServer.Any(x => x.Name.ToUpper() == csImport.MaterialName.ToUpper()))
            {
                csImport.Check = "Mã Liệu không tồn tại trên hệ thống ERP";
                return;
            }
            //Color
            if (string.IsNullOrEmpty(csImport.Color))
            {
                csImport.Check = "Màu sắc = 'blank'";
                return;
            }
            //Follow
            if (string.IsNullOrEmpty(csImport.Follow))
            {
                csImport.Check = "Follow = 'blank'";
                return;
            }
            csImport.Check = "OK";
        }
        private async Task UploadCS(CSImport csImport)
        {
            var rp = await CSDataDAO.Instance.Insert(
                csImport.CustName,
                csImport.MaterialName,
                _lstMaterialFromServer.First(x => x.Name.ToUpper() == csImport.MaterialName.ToUpper()).DNWidth,
                csImport.Color,
                csImport.ReplacementColor,
                csImport.Follow,
                csImport.Note,
                csImport.Article,
                int.TryParse(csImport.Confirmed, out int c) ? c > 3 ? 3 : c : 0,
                csImport.AdditionalOptional1,
                csImport.AdditionalOptional2,
                csImport.AdditionalOptional3,
                DateTime.Now
            );
            if (rp.IsSuccess)
            {
                csImport.Check = "Upload thành công!";
            }
            else
            {
                csImport.Check = "Lỗi: " + rp.Msg;
            }
        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnUpload.Enabled = false;
            btnDownloadFile.Enabled = false;
            try
            {
                var lst = dgvMain.DataSource as List<CSImport>;
                if (lst == null)
                {
                    lblStatus.Text = "Chưa upload dữ liệu";
                    return;
                }
                if (!await LoadDataFromServer())
                {
                    lblStatus.Text = "Lỗi tải dữ liêu từ máy chủ ERP";
                    return;
                }
                ParallelOptions parallelOptions = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                };
                await Task.Run(() =>
                Parallel.ForEach(lst, parallelOptions, cs =>
                {
                    CheckInputs(cs);
                }));
                dgvMain.Refresh();
                if (lst.Any(cs => cs.Check != "OK"))
                {
                    lblStatus.Text = "Có lỗi dữ liệu, xem cột 'Kiểm tra dữ liệu'";
                }
                else
                {
                    var lstTask = new List<Task>();
                    foreach (var cs in lst)
                    {
                        lstTask.Add(UploadCS(cs));
                        await Task.Delay(10);
                    }
                    await Task.WhenAll(lstTask);
                    dgvMain.Refresh();
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi hệ thống: " + ex.Message;
            }
            finally
            {
                btnStart.Enabled = true;
                btnUpload.Enabled = true;
                btnDownloadFile.Enabled = true;
            }
        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            var lst = new List<CSImport>() { new CSImport() };
            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Chọn nơi lưu file";
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                sfd.FileName = "CS Upload.xlsx";
                sfd.DefaultExt = "xlsx";
                sfd.AddExtension = true;
                sfd.OverwritePrompt = true;
                //sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (sfd.ShowDialog() != DialogResult.OK) return;

                string path = sfd.FileName;
                var dt = JHelper.ConvertData.DataTableFromGrid(dgvMain, true);
                dt.Columns.Remove(dt.Columns["Kiểm tra dữ liệu"]);
                var config = new OpenXmlConfiguration
                {
                    FastMode = true,
                    EnableAutoWidth = true
                };
                MiniExcel.SaveAs(path, dt, configuration: config, overwriteFile: true);
            }
        }
    }
}
