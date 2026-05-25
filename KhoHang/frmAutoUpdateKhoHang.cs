using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhoHang
{
    public partial class frmAutoUpdateKhoHang : Form
    {
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        string _connStr = string.Empty;
        string _excelPath = string.Empty;
        DataTable _tbTonKho = new DataTable("TonKho");
        DataTable _tbXuatKho = new DataTable("XuatKho");
        public frmAutoUpdateKhoHang()
        {
            InitializeComponent();
            _connStr = _iniManager.GetString("KhoHang", "ConnectionString");
            _excelPath = Encoding.UTF8.GetString(Encoding.Default.GetBytes(_iniManager.GetString("KhoHang", "ExcelPath")));
            chkTonKho.Checked = true;
            chkXuatKho.Checked = true;

            //TonKho
            _tbTonKho.Columns.Add("NgayNhap", typeof(DateTime));
            _tbTonKho.Columns.Add("KhachHang", typeof(string));
            _tbTonKho.Columns.Add("Lieu", typeof(string));
            _tbTonKho.Columns.Add("MauSac", typeof(string));
            _tbTonKho.Columns.Add("MoTaMau", typeof(string));
            _tbTonKho.Columns.Add("PO", typeof(string));
            _tbTonKho.Columns.Add("CK", typeof(string));
            _tbTonKho.Columns.Add("STTGia", typeof(string));
            _tbTonKho.Columns.Add("SLBanDau", typeof(float));
            _tbTonKho.Columns.Add("SLDauThang", typeof(float));
            _tbTonKho.Columns.Add("SLCuoiThang", typeof(float));
            _tbTonKho.Columns.Add("DuLieuXuat", typeof(string));
            //XuatKho
            _tbXuatKho.Columns.Add("NgayXuat", typeof(DateTime));
            _tbXuatKho.Columns.Add("KhachHang", typeof(string));
            _tbXuatKho.Columns.Add("Lieu", typeof(string));
            _tbXuatKho.Columns.Add("Mau", typeof(string));
            _tbXuatKho.Columns.Add("PO", typeof(string));
            _tbXuatKho.Columns.Add("CK", typeof(string));
            _tbXuatKho.Columns.Add("SLXuat", typeof(float));
        }
        private void WriteLog(string text)
        {

        }
        private async Task UpdateXuatKho(XLWorkbook workbook)
        {
            await Task.Run(() =>
            {
                _tbXuatKho.Clear();

                _tbXuatKho.BeginLoadData();

                try
                {
                    var sheet = workbook.Worksheet("出货（xuat）");

                    var rows = sheet.RangeUsed().RowsUsed().Skip(3);

                    foreach (var row in rows)
                    {
                        var cellA = row.Cell("A");
                        var cellJ = row.Cell("J");

                        if (!cellA.TryGetValue(out DateTime ngayxuat))
                            continue;

                        if (!cellJ.TryGetValue(out float slxuat))
                            continue;

                        string khachhang = row.Cell("B").GetString().Replace(" ", "");
                        string lieu = row.Cell("C").GetString().Replace(" ", "").Replace("-", "").Replace("\"", "");
                        string mau = row.Cell("D").GetString().Replace(" ", "");
                        string po = row.Cell("F").GetString().Replace(" ", "");
                        string ck = row.Cell("G").GetString().Replace(" ", "");

                        if (string.IsNullOrEmpty(khachhang) ||
                            string.IsNullOrEmpty(lieu) ||
                            string.IsNullOrEmpty(mau) ||
                            string.IsNullOrEmpty(po))
                        {
                            continue;
                        }
                        _tbXuatKho.Rows.Add(
                            ngayxuat.Date,
                            khachhang,
                            lieu,
                            mau,
                            po,
                            ck,
                            slxuat
                        );
                    }

                    _tbXuatKho.EndLoadData();

                    if (_tbXuatKho.Rows.Count > 0)
                    {
                        BulkInsert(_tbXuatKho, "XuatKho");
                    }
                }
                finally
                {
                    _tbXuatKho.EndLoadData();
                }
            });
        }
        private async Task UpdateTonKho(XLWorkbook workbook)
        {
            await Task.Run(() =>
            {
                _tbTonKho.Clear();
                _tbTonKho.BeginLoadData();

                try
                {
                    var sheet = workbook.Worksheet("tồn kho");
                    var rows = sheet.RangeUsed().RowsUsed().Skip(3);
                    foreach (var row in rows)
                    {
                        var cellA = row.Cell("A");
                        var cellJ = row.Cell("J");

                        if (!cellA.TryGetValue(out DateTime ngaynhap))
                            continue;

                        string khachhang = row.Cell("B").GetString().Replace(" ", "");
                        string lieu = row.Cell("C").GetString().Replace(" ", "").Replace("-", "").Replace("\"", "");
                        string mau = row.Cell("D").GetString().Replace(" ", "");
                        string motamau = row.Cell("E").GetString().Replace(" ", "");
                        string po = row.Cell("F").GetString().Replace(" ", "");
                        string ck = row.Cell("G").GetString().Replace(" ", "");
                        string gia = row.Cell("I").GetString().Replace(" ", "");
                        float slbandau = row.Cell("J").TryGetValue<float>(out float _slbandau) ? _slbandau : 0;
                        float sldauthang = row.Cell("K").TryGetValue<float>(out float _sldauthang) ? _slbandau : 0;
                        float slcuoithang = row.Cell("N").TryGetValue<float>(out float _slcuoithang) ? _slbandau : 0;
                        string dulieuxuat = row.Cell("I").GetString().Replace(" ", "");
                        /*
                        _tbTonKho.Columns.Add("NhayNhap", typeof(DateTime));
                        _tbTonKho.Columns.Add("KhachHang", typeof(string));
                        _tbTonKho.Columns.Add("Lieu", typeof(string));
                        _tbTonKho.Columns.Add("Mau", typeof(string));
                        _tbTonKho.Columns.Add("MoTaMau", typeof(string));
                        _tbTonKho.Columns.Add("PO", typeof(string));
                        _tbTonKho.Columns.Add("CK", typeof(string));
                        _tbTonKho.Columns.Add("STTGia", typeof(string));
                        _tbTonKho.Columns.Add("SLBanDau", typeof(string));
                        _tbTonKho.Columns.Add("SLDauThang", typeof(string));
                        _tbTonKho.Columns.Add("SLCuoiThang", typeof(string));
                        _tbTonKho.Columns.Add("DuLieuXuat", typeof(string));
                        */
                        if (string.IsNullOrEmpty(khachhang) ||
                            string.IsNullOrEmpty(lieu) ||
                            string.IsNullOrEmpty(mau) ||
                            string.IsNullOrEmpty(po))
                        {
                            continue;
                        }
                        _tbTonKho.Rows.Add(
                            ngaynhap.Date,
                            khachhang,
                            lieu,
                            mau,
                            motamau,
                            po,
                            ck,
                            gia,
                            slbandau,
                            sldauthang,
                            slcuoithang,
                            dulieuxuat
                        );
                    }

                    _tbTonKho.EndLoadData();

                    if (_tbTonKho.Rows.Count > 0)
                    {
                        BulkInsert(_tbTonKho, "TonKho");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    _tbTonKho.EndLoadData(); 
                }
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var task = Task.Run(async () =>
            {
                while (true)
                {
                    XLWorkbook workbook = null;
                    await Task.Run(() =>
                    {
                        try
                        {
                            using (FileStream fileStream = new FileStream(_excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                workbook = new XLWorkbook(fileStream);
                            }
                        }
                        catch
                        {

                        }
                    });
                    if (workbook == null)
                    {
                        WriteLog($"Lỗi đọc file \"{_excelPath}\"");
                    }
                    else
                    {
                        var tasks = new List<Task>();
                        if (chkXuatKho.Checked)
                        {
                            tasks.Add( UpdateXuatKho(workbook));
                        }
                        if (chkTonKho.Checked)
                        {
                            tasks.Add(UpdateTonKho(workbook));
                        }
                        await Task.WhenAll(tasks);
                    }
                    await Task.Delay(TimeSpan.FromSeconds((int)updDelayTime.Value));
                }
            });
        }
        public void BulkInsert(DataTable dt, string tableNameSql)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand($"TRUNCATE TABLE {tableNameSql}_Loading", conn, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // bulk copy
                        using (SqlBulkCopy bulk = new SqlBulkCopy(
                            conn,
                            SqlBulkCopyOptions.TableLock,
                            tran))
                        {
                            bulk.DestinationTableName = $"dbo.{tableNameSql}_Loading";

                            bulk.BatchSize = 20000;
                            bulk.BulkCopyTimeout = 0;

                            foreach (DataRow row in _tbTonKho.Rows)
                            {
                                foreach (DataColumn col in _tbTonKho.Columns)
                                {
                                    Console.WriteLine(
                                        $"{col.ColumnName} = {row[col]} | {row[col]?.GetType()}");
                                }

                                break;
                            }

                            bulk.WriteToServer(dt);

                        }

                        // swap view
                        using (SqlCommand cmd = new SqlCommand($@"
                            BEGIN TRAN
                            TRUNCATE TABLE {tableNameSql}_Current
                            INSERT INTO {tableNameSql}_Current WITH (TABLOCK)
                            SELECT *
                            FROM {tableNameSql}_Loading

                            COMMIT",
                            conn,
                            tran))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
        public void DonHangTM()
        {

        }
    }
}
