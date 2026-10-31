using ClosedXML.Excel;
using HtmlAgilityPack;
using Julian;
using Julian.Helper;
using Julian.Utils;
using Spire.Doc.AI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TyXuan.LocalDatabase.DTO;

namespace TyXuan
{
    public partial class frmNewOrder : Form
    {
        HttpClient _httpClient = null;
        List<Material> _lstMaterial = null;
        List<Follow> _lstFollow = null;
        List<Price> _lstPrice = null;
        public frmNewOrder()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            dgvMain.AutoGenerateColumns = false;
        }
        private class Art
        {
            public string Article { get; set; }
            public decimal Qty { get; set; }
        }
        private class NewOrder
        {
            public int ID { get; set; }
            public DateTime IssueDate { get; set; }
            public string OrderNumber { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public string Season { get; set; }
            public float Qty { get; set; }
            public DateTime RequestDate { get; set; }
            public List<string> Remarks { get; set; } = new List<string>();

            //public string Remarks_ToString => string.Join("\r\n", Remarks);
            public string Remarks_ToString => Remarks.Count == 0 ? "" : Remarks.First().Substring(Remarks.First().IndexOf(')') + 1);
            public List<Art> Articles
            {
                get
                {
                    var l = Remarks.Where(x => x.Contains("(") && x.Contains(")")).ToList();
                    if (l.Count == 0)
                        return new List<Art>();
                    var p = l.Select(x => new Art()
                    {
                        Article = x.Substring(x.IndexOf(')') + 1),
                        Qty = decimal.TryParse(Regex.Match(x, @"\((.*?)\)").Groups[1].Value, out decimal q) ? q : -1
                    }).ToList();
                    var n = p.GroupBy(x => x.Article).Select(x => new Art() { Article = x.First().Article, Qty = x.Sum(y => y.Qty) }).ToList();
                    return n;
                }
            }
            public string Articles_ToString => Articles.Count == 1 ? $"{Articles.First().Article} ({Articles.First().Qty})" : string.Join(Environment.NewLine, Articles.Select(x => $"{x.Article} ({x.Qty})").ToList());
            public int Width => int.TryParse(Description.Substring(0, 2), out int w) ? w : 0;
            public string MaterialName
            {
                get
                {
                    string m = Description.Substring(Description.IndexOf(')') + 1);
                    m = m.Replace("(V)", "");
                    m = m.Replace(" ", "");
                    m = m.Replace("YHS", "YH-S");
                    m = m.Replace("YHS", "YH-S");
                    m = m.Replace("YHM", "YH-M");
                    m = m == "YH-M1093EPM558" ? "YH-M1093EPM5" : m;
                    return m;
                }
            }
        }
        private class NewOrder2
        {
            public int ID { get; set; }
            public DateTime IssueDate { get; set; }
            public string OrderNumber { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public string Season { get; set; }
            public float Qty { get; set; }
            public DateTime RequestDate { get; set; }
            public string Article { get; set; }
            public string Articles_ToString => $"({Qty}) {Article}";
            public int Width => int.TryParse(Description.Substring(0, 2), out int w) ? w : 0;
            public string MaterialName
            {
                get
                {
                    string m = Description.Substring(Description.IndexOf(')') + 1);
                    m = m.Replace("(V)", "");
                    m = m.Replace(" ", "");
                    m = m.Replace("YHS", "YH-S");
                    m = m.Replace("YHS", "YH-S");
                    m = m.Replace("YHM", "YH-M");
                    m = m == "YH-M1093EPM558" ? "YH-M1093EPM5" : m;
                    return m;
                }
            }
        }
        private void dgvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                var selectedCells = dgvMain.SelectedCells
                    .Cast<DataGridViewCell>()
                    .OrderBy(c => c.RowIndex)
                    .ThenBy(c => c.ColumnIndex)
                    .ToList();

                if (selectedCells.Count == 0) return;

                int minRow = selectedCells.Min(c => c.RowIndex);
                int maxRow = selectedCells.Max(c => c.RowIndex);
                int minCol = selectedCells.Min(c => c.ColumnIndex);
                int maxCol = selectedCells.Max(c => c.ColumnIndex);

                var sb = new StringBuilder();

                for (int r = minRow; r <= maxRow; r++)
                {
                    if (r > minRow)
                        sb.AppendLine();

                    for (int c = minCol; c <= maxCol; c++)
                    {
                        if (c > minCol)
                            sb.Append('\t');

                        var cell = dgvMain[c, r];

                        string value = cell.Value?.ToString() ?? "";

                        // Ô có xuống dòng hoặc tab hoặc "
                        if (value.Contains('\n') ||
                            value.Contains('\r') ||
                            value.Contains('\t') ||
                            value.Contains('"'))
                        {
                            value = "\"" + value.Replace("\"", "\"\"") + "\"";
                        }

                        sb.Append(value);
                    }
                }

                Clipboard.SetText(sb.ToString());

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private async void GetData()
        {
            if (_lstMaterial == null)
            {
                _lstMaterial = await Task.FromResult(DAO.DAO.Instance.GetMaterials());
            }
            if (_lstFollow == null)
            {
                _lstFollow = await Task.FromResult(DAO.DAO.Instance.GetFollows());
            }
            if (_lstPrice == null)
            {
                _lstPrice = await Task.FromResult(DAO.DAO.Instance.GetPrices());
            }
        }
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            btnLoad.Enabled = false;
            txtInput.Enabled = false;
            txtUrl.Enabled = false;
            btnCopy.Enabled = false;
            try
            {
                GetData();
                dgvMain.DataSource = null;
                await Task.Yield();
                var response = await _httpClient.GetStringAsync(txtUrl.Text + "?po=" + txtInput.Text.Trim());
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(response);
                //var a = GetDataTable(doc);
                var lstTable = doc.DocumentNode.SelectNodes("//table");
                var table = lstTable.OrderBy(x => x.InnerLength).Last();
                var lst = await Task.FromResult(HtmlTableToNewOrderList(table));
                dgvMain.DataSource = lst;
                if (lst != null && lst.Count > 0)
                {
                    lblIssueDate.Text = lst[0].IssueDate.ToString("yyyy-MM-dd");
                    lblOrderNumber.Text = lst[0].OrderNumber.ToString();

                    lblTotalRow.Text = lst.Count.ToString("#,##0");
                    lblTotalQty.Text = lst.Sum(x => x.Qty).ToString("#,##0.0000");
                }
                else
                {
                    toolTip1.Show("Không có dữ liệu! Xem lại mã PO", txtInput, txtInput.Width, txtInput.Height - 3, 1500);
                    lblIssueDate.Text = "...";
                    lblOrderNumber.Text = "...";

                    lblTotalRow.Text = "0";
                    lblTotalQty.Text = "0";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLoad.Enabled = true;
                txtUrl.Enabled = true;
                txtInput.Enabled = true;
                btnCopy.Enabled = true;
                this.ActiveControl = txtInput;
            }
        }
        private List<NewOrder> HtmlTableToNewOrderList(HtmlNode table)
        {
            try
            {
                var rows = table.SelectNodes(".//tr");
                if (rows == null || rows.Count == 0)
                    return null;
                var dt = new DataTable();
                var headerCells = rows[0].SelectNodes("./th|./td");

                foreach (var cell in headerCells)
                {
                    var colName = HtmlEntity.DeEntitize(cell.InnerText).Trim();

                    if (string.IsNullOrWhiteSpace(colName))
                        colName = $"Column{dt.Columns.Count + 1}";

                    if (dt.Columns.Contains(colName))
                        colName += "_" + dt.Columns.Count;

                    dt.Columns.Add(colName);
                }

                foreach (var row in rows.Skip(1))
                {
                    var cells = row.SelectNodes("./td");

                    if (cells == null)
                        continue;

                    var dr = dt.NewRow();

                    for (int i = 0; i < Math.Min(dt.Columns.Count, cells.Count); i++)
                    {
                        if (cells[i].InnerHtml.Contains("<br"))
                        {
                            var lst = Regex.Split(cells[i].InnerHtml, @"<br\s*/?>", RegexOptions.IgnoreCase);
                            lst = lst.Where(x => x.Contains("(")).Select(x => x.Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim()).ToArray();
                            var a = string.Join("\n", lst);
                            dr[i] = a;
                        }
                        else
                        {
                            dr[i] = HtmlEntity.DeEntitize(cells[i].InnerText)
                                .Replace("\r", "")
                                .Replace("\t", "")
                                .Trim();
                        }

                    }

                    dt.Rows.Add(dr);
                }
                List<NewOrder> lstNewOrder = new List<NewOrder>();
                if (dt.Rows.Count > 0)
                {
                    int id = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        //var a = row["Issue Date:"].ToString();
                        //var IssueDate = DateTime.ParseExact(row["Issue Date:"].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        //var RequestDate = DateTime.ParseExact(row["Buyer Request Date"].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                        var newOrder = new NewOrder()
                        {
                            IssueDate = row["Issue Date:"] == DBNull.Value ? DateTime.MinValue : DateTime.TryParseExact(row["Issue Date:"]?.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var issueDate) ? issueDate : DateTime.MinValue,
                            OrderNumber = row["Order Number:"].ToString(),
                            Code = row["Additional Optional 2"].ToString(),
                            Description = row["Additional Optional 3"].ToString(),
                            Season = row["Season"].ToString(),
                            Qty = float.TryParse(row["Order Quantity"].ToString().Replace(",", ""), out float q) ? q : 0,
                            //RequestDate = DateTime.ParseExact(row["Buyer Request Date"].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                            RequestDate = row["Buyer Request Date"] == DBNull.Value ? DateTime.MinValue : DateTime.TryParseExact(row["Buyer Request Date"]?.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var rDate) ? rDate : DateTime.MinValue,
                            Remarks = row["Additional Optional 4"].ToString().Split('\n').Where(x => x.Substring(x.IndexOf(')') + 1).Length == 6).ToList(),
                        };
                        lstNewOrder.Add(newOrder);
                    }
                }
                return lstNewOrder;
            }
            catch
            {
                return null;
            }
        }
        private DataTable HtmlTableToDataTable(HtmlNode table)
        {
            var dt = new DataTable();

            var rows = table.SelectNodes(".//tr");
            if (rows == null || rows.Count == 0)
                return dt;

            // Header
            var headerCells = rows[0].SelectNodes("./th|./td");

            foreach (var cell in headerCells)
            {
                var colName = HtmlEntity.DeEntitize(cell.InnerText).Trim();

                if (string.IsNullOrWhiteSpace(colName))
                    colName = $"Column{dt.Columns.Count + 1}";

                if (dt.Columns.Contains(colName))
                    colName += "_" + dt.Columns.Count;

                dt.Columns.Add(colName);
            }

            // Data
            foreach (var row in rows.Skip(1))
            {
                var cells = row.SelectNodes("./td");

                if (cells == null)
                    continue;

                var dr = dt.NewRow();

                for (int i = 0; i < Math.Min(dt.Columns.Count, cells.Count); i++)
                {
                    if (cells[i].InnerHtml.Contains("<br"))
                    {
                        var lst = Regex.Split(cells[i].InnerHtml, @"<br\s*/?>", RegexOptions.IgnoreCase);
                        lst = lst.Where(x => x.Contains("(")).Select(x => x.Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim()).ToArray();
                        var a = string.Join("\r\n", lst);
                        dr[i] = string.Join("\r\n", lst);
                    }
                    else
                    {
                        dr[i] = HtmlEntity.DeEntitize(cells[i].InnerText)
                            .Replace("\r", "")
                            .Replace("\t", "")
                            .Trim();
                    }

                }

                dt.Rows.Add(dr);
            }

            return dt;
        }
        private void frmNewOrder_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtInput;
            IniManager iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
            txtUrl.Text = iniManager.GetString("TX", "GetNewOrder", "http://b2b.lacty.com.vn/LVL_B2B/adidas_po_format2.php");
        }

        private void BAOGOI()
        {
            var clipboardText = Clipboard.GetText();
            var lines = clipboardText.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var lstResult = new List<string>();
            foreach (string line in lines)
            {
                string[] lst = line.Split('\n');
                string result = "";
                if (lst.Length > 1 && line.Length >= 2 && line.First() == '\"' && line.Last() == '\"')
                {
                    var l = new List<string>();
                    int n = 1;
                    for (int i = 0; i < lst.Length; i++)
                    {
                        string text = lst[i];
                        if (string.IsNullOrEmpty(text) || text == "\"")
                        {
                            continue;
                        }
                        if (text.ToUpper().Contains("BAO G"))
                        {
                            continue;
                        }
                        if (i == 0 && text.Length > 1 && text[0] == '\"')
                        {
                            text = text.Substring(1);
                        }
                        else if (i == lst.Length - 1 && text.Length > 1 && text[text.Length - 1] == '\"')
                        {
                            text = text.Substring(0, text.Length - 1);
                        }
                        text = Regex.Replace(text, @"^\d+\.+\s*", "");
                        text = n++.ToString() + "." + text;
                        /*if (text.ToUpper().Contains("BAO G") && i != lst.Length - 1)
                        {
                            continue;
                        }
                        if (i == 0 && text.Length >= 3 && text[0] == '"' && char.IsDigit(text[1]) && text[2] == '.')
                        {
                            text = (n++) + text.Substring(2);
                        }
                        if (i > 0 && text.Length >= 2 && char.IsDigit(text[0]) && text[1] == '.')
                        {
                            text = (n++) + text.Substring(1);
                        }
                        if (text.ToUpper().Contains("BAO G") && i == lst.Length - 1)
                        {
                            //text = "\"";
                            continue;
                        }*/
                        l.Add(text);
                    }
                    result = l.Count == 1 ? l[0] : "\"" + string.Join("\n", l) + "\"";
                }
                else
                {
                    result = line;
                }
                lstResult.Add(result);
            }
            string x = string.Join("\r\n", lstResult);
            Clipboard.Clear();
            Clipboard.SetText(x);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            var data = dgvMain.DataSource as List<NewOrder>;
            if (data == null)
            {
                toolTip1.Show("Dữ liệu rỗng!", btnCopy, btnCopy.Width, 0, 1500);
                return;
            }
            var lst = new List<string>();
            foreach (var order in data)
            {
                string text = $"{order.IssueDate.ToString("yyyy-MM-dd")}\t{order.OrderNumber}\t{order.Code}\t{order.Qty}\t{order.Description}\t{order.Remarks_ToString}";
                lst.Add(text);
            }
            var result = string.Join("\r\n", lst);
            Clipboard.SetText(result);
            toolTip1.Show("Đã sao chép!", btnCopy, btnCopy.Width, 0, 1500);
        }

        private void btnExcelToData_Click(object sender, EventArgs e)
        {
            var l = JHelper.ConvertData.ConvertExcelDataToRows(textBox1.Text);
            var data = l.Data;
            var lst = new List<NewOrder2>();
            foreach (var row in data)
            {
                var order = new NewOrder2()
                {
                    IssueDate = DateTime.Now,
                    OrderNumber = row[0],
                    Code = row[1],
                    Description = row[2],
                    Article = row[6],
                    Qty = float.Parse(row[5])
                };
                lst.Add(order);
            }
            var lstG = lst.GroupBy(x => new { x.Code}).Select(x =>
            {
                {
                    var a = new NewOrder()
                    {
                        IssueDate = DateTime.Now,
                        OrderNumber = x.First().OrderNumber,
                        Code = x.First().Code,
                        Description = x.First().Description,
                        Qty = x.Sum(y=> y.Qty),
                        Remarks  = x.Select(y=> y.Articles_ToString).ToList()
                    };
                    return a;
                }
            }).ToList();
            dgvMain.DataSource = lstG;
            if (lstG != null && lstG.Count > 0)
            {
                lblIssueDate.Text = lstG[0].IssueDate.ToString("yyyy-MM-dd");
                lblOrderNumber.Text = lstG[0].OrderNumber.ToString();

                lblTotalRow.Text = lstG.Count.ToString("#,##0");
                lblTotalQty.Text = lstG.Sum(x => x.Qty).ToString("#,##0.0000");
            }
            else
            {
                toolTip1.Show("Không có dữ liệu! Xem lại mã PO", txtInput, txtInput.Width, txtInput.Height - 3, 1500);
                lblIssueDate.Text = "...";
                lblOrderNumber.Text = "...";

                lblTotalRow.Text = "0";
                lblTotalQty.Text = "0";

            }
        }
    }
}
