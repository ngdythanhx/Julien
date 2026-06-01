using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian.Forms
{
    public partial class frmThemVaiMoc : Form
    {
        List<string> _lstFileThuMua = new List<string>();
        string _fileBaoCao = string.Empty;
        public frmThemVaiMoc()
        {
            InitializeComponent();
        }

        private void btnSelectFiles_ThuMua_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label1;
                var dl = new OpenFileDialog();
                dl.Multiselect = true;
                dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                _lstFileThuMua.Clear();
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string[] files = dl.FileNames;
                    foreach (string file in files)
                    {
                        string fileName = System.IO.Path.GetFileName(file);
                        string extension = System.IO.Path.GetExtension(file);
                        if (extension == ".xlsx")
                        {
                            _lstFileThuMua.Add(file);
                            listBox1.Items.Add(fileName);
                        }
                    }
                    if (_lstFileThuMua.Count > 0 && !string.IsNullOrEmpty(_fileBaoCao))
                    {
                        btnStart.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectFile_BaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label1;
                var dl = new OpenFileDialog();
                dl.Multiselect = false;
                dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string file = dl.FileName;
                    string fileName = System.IO.Path.GetFileName(file);
                    string extension = System.IO.Path.GetExtension(file);
                    if (extension == ".xlsx")
                    {
                        lblFileName_BaoCao.Text = fileName;
                        _fileBaoCao = file;
                    }
                    if (_lstFileThuMua.Count > 0 && !string.IsNullOrEmpty(_fileBaoCao))
                    {
                        btnStart.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            XLWorkbook workbook_BaoCao = null;
            List<XLWorkbook> workbooks_ThuMua = new List<XLWorkbook>();
            List<Task> lstTask = new List<Task>();
            //try
            //{
            lstTask.Add(Task.Run(() => workbook_BaoCao = new XLWorkbook(_fileBaoCao)));
            foreach (var file in _lstFileThuMua)
            {
                lstTask.Add(Task.Run(() =>
                {
                    using (FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        workbooks_ThuMua.Add(new XLWorkbook(stream));
                    };
                }));
            }
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            await Task.WhenAll(lstTask);
            progressBar1.Style = ProgressBarStyle.Blocks;
            if (lstTask.All(w => w != null))
            {
                var table_BaoCao = workbook_BaoCao.Worksheet(1).Table("tbData");
                var patternDict = new Dictionary<string, string>();
                await Task.Run(() =>
                {
                    foreach (var workbook in workbooks_ThuMua)
                    {
                        List<IXLRangeRow> rangeRows = workbook.Worksheet(1).RangeUsed().RowsUsed().Skip(3).ToList();
                        foreach (var rangeRow in rangeRows)
                        {
                            string CtyMoc = rangeRow.Cell("I").GetString();
                            string MaDon = rangeRow.Cell("E").GetString();
                            if (!string.IsNullOrWhiteSpace(CtyMoc) && !string.IsNullOrWhiteSpace(MaDon))
                            {
                                patternDict[MaDon] = CtyMoc;
                            }
                        }
                    }
                });
                var rangeRows_BaoCao = table_BaoCao.RowsUsed().ToArray();
                progressBar1.Maximum = rangeRows_BaoCao.Length;
                lblProcessing.Text = $"0/{rangeRows_BaoCao.Length}";
                int processed = 0;
                await Task.Run(() =>
                {
                    ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                    Parallel.ForEach(rangeRows_BaoCao, parallelOptions, (rangeRow, state) =>
                    {
                        //var rangeRow = rangeRows_BaoCao[i];
                        string[] lstMaNhaNhuom = rangeRow.Cell("BE").GetString().Split('\n');
                        List<string> lstMaCongTy = new List<string>();
                        foreach (var line in lstMaNhaNhuom)
                        {
                            if (patternDict.TryGetValue(line, out string TenCty))
                            {
                                lstMaCongTy.Add(TenCty);
                            }
                        }
                        if (lstMaCongTy.Count > 0)
                            rangeRow.Cell("BH").Value = string.Join("\n", lstMaCongTy);
                        //Reporter(totalRows, totalRows);
                        int done = Interlocked.Increment(ref processed);
                        Reporter(done, rangeRows_BaoCao.Length);
                    });
                });
                /*for (int i = 0; i < rangeRows_BaoCao.Length; i++)
                {
                    var rangeRow = rangeRows_BaoCao[i];
                    string[] lstMaNhaNhuom = rangeRow.Cell("BE").GetString().Split('\n');
                    foreach (var line in lstMaNhaNhuom)
                    {
                        if (patternDict.TryGetValue(line, out string TenCty))
                        {
                            rangeRow.Cell("BH").Value = TenCty;
                        }
                    }
                    progressBar1.Value = i + 1;
                    lblProcessing.Text = $"{i + 1}/{rangeRows_BaoCao.Length}";
                }*/
                workbook_BaoCao.Save();
            }
            /*}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            {
                btnStart.Enabled = true;
                foreach (var w in workbooks_ThuMua)
                {
                    w?.Dispose();
                }
            }*/
        }
        private void Reporter(int curRows, int totalRows)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                progressBar1.Value = curRows;
                progressBar1.Maximum = totalRows;
                lblProcessing.Text = $"{curRows}/{totalRows}";
            });
        }
    }
}
