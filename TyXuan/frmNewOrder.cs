using ClosedXML.Excel;
using Julian;
using Julian.Database.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Globalization;
using Spire.Doc.AI.Model;
using Julian.Utils;

namespace TyXuan
{
    public partial class frmNewOrder : Form
    {
        HttpClient _httpClient = null;
        public frmNewOrder()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            dgvMain.AutoGenerateColumns = false;
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
            public string Remarks_ToString => string.Join("\r\n", Remarks);
        }
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            btnLoad.Enabled = false;
            txtInput.Enabled = false;
            txtUrl.Enabled = false;
            try
            {
                dgvMain.DataSource = null;
                await Task.Yield();
                var response = await _httpClient.GetStringAsync(txtUrl.Text + "?po=" + txtInput.Text);
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(response);
                //var a = GetDataTable(doc);
                var lstTable = doc.DocumentNode.SelectNodes("//table");
                var table = lstTable.OrderBy(XacNhanYVai => XacNhanYVai.InnerLength).Last();
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
                            ID = id++,
                            IssueDate = DateTime.ParseExact(row["Issue Date:"].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                            OrderNumber = (String)row["Order Number:"],
                            Code = (String)row["Additional Optional 2"],
                            Description = (String)row["Additional Optional 3"],
                            Season = (String)row["Season"],
                            Qty = float.Parse(row["Order Quantity"].ToString().Replace(",", "")),
                            RequestDate = DateTime.ParseExact(row["Buyer Request Date"].ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                            Remarks = ((String)row["Additional Optional 4"]).Split('\n').ToList(),
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
    }
}
