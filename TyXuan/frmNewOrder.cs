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

namespace TyXuan
{
    public partial class frmNewOrder : Form
    {
        HttpClient _httpClient = null;
        public frmNewOrder()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            var response = await _httpClient.GetStringAsync(
                "http://b2b.lacty.com.vn/LVL_B2B/adidas_po_format2.php?po=20260601806");

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(response);
            //var a = GetDataTable(doc);
            var table = doc.DocumentNode
                .SelectNodes("//table")
                .Last();

            var rows = ParseTable(table);
            var a = rows[1];
            var b = HtmlTableToDataTable(table);
            var c = HtmlTableToDataTableMerge(table);
        }
        public DataTable HtmlTableToDataTableMerge(HtmlNode table)
        {
            var dt = new DataTable();

            var trs = table.SelectNodes(".//tr");
            if (trs == null || trs.Count == 0)
                return dt;

            // Tìm số cột thực
            int maxCols = trs.Max(r =>
                r.SelectNodes("./td|./th")?
                .Sum(c => c.GetAttributeValue("colspan", 1)) ?? 0);

            for (int i = 0; i < maxCols; i++)
                dt.Columns.Add($"Col{i}");

            var rowspanMap = new Dictionary<int, (int MasterRow, int Remain)>();

            foreach (var tr in trs)
            {
                var dr = dt.NewRow();

                int col = 0;

                // fill rowspan cũ
                while (rowspanMap.ContainsKey(col))
                    col++;

                var cells = tr.SelectNodes("./td|./th");
                if (cells == null)
                    continue;

                foreach (var cell in cells)
                {
                    while (rowspanMap.ContainsKey(col))
                        col++;

                    string value = HtmlEntity.DeEntitize(cell.InnerText).Trim();

                    int rowspan = cell.GetAttributeValue("rowspan", 1);
                    int colspan = cell.GetAttributeValue("colspan", 1);

                    for (int c = 0; c < colspan; c++)
                    {
                        dr[col + c] = value;

                        if (rowspan > 1)
                        {
                            rowspanMap[col + c] =
                                (dt.Rows.Count, rowspan - 1);
                        }
                    }

                    col += colspan;
                }

                dt.Rows.Add(dr);

                // xử lý rowspan
                foreach (var key in rowspanMap.Keys.ToList())
                {
                    var info = rowspanMap[key];

                    string master =
                        dt.Rows[info.MasterRow][key]?.ToString() ?? "";

                    string current =
                        dt.Rows[dt.Rows.Count - 1][key]?.ToString() ?? "";

                    if (!string.IsNullOrWhiteSpace(current) &&
                        current != master)
                    {
                        dt.Rows[info.MasterRow][key] =
                            master + Environment.NewLine + current;

                        dt.Rows[dt.Rows.Count - 1][key] = DBNull.Value;
                    }

                    if (info.Remain == 1)
                        rowspanMap.Remove(key);
                    else
                        rowspanMap[key] = (info.MasterRow, info.Remain - 1);
                }
            }

            // Xóa dòng phụ chỉ chứa dữ liệu rowspan
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                bool empty = true;

                foreach (DataColumn col in dt.Columns)
                {
                    if (!string.IsNullOrWhiteSpace(dt.Rows[i][col]?.ToString()))
                    {
                        empty = false;
                        break;
                    }
                }

                if (empty)
                    dt.Rows.RemoveAt(i);
            }

            return dt;
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
                        // có br
                    }
                    else
                    {
                        // không có br
                    }
                    dr[i] = HtmlEntity.DeEntitize(cells[i].InnerText)
                        .Replace("\r", "")
                        .Replace("\t", "")
                        .Trim();
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }
        private List<List<string>> ParseTable(HtmlNode table)
        {
            var result = new List<List<string>>();
            var rowspanMap = new Dictionary<int, (string Value, int RemainRows)>();

            foreach (var tr in table.SelectNodes("./tr"))
            {
                var row = new List<string>();
                int colIndex = 0;

                // fill rowspan từ dòng trước
                while (rowspanMap.TryGetValue(colIndex, out var rs))
                {
                    row.Add(rs.Value);

                    if (rs.RemainRows == 1)
                        rowspanMap.Remove(colIndex);
                    else
                        rowspanMap[colIndex] = (rs.Value, rs.RemainRows - 1);

                    colIndex++;
                }

                foreach (var cell in tr.SelectNodes("./td|./th") ?? Enumerable.Empty<HtmlNode>())
                {
                    while (rowspanMap.TryGetValue(colIndex, out (string Value, int RemainRows) rs))
                    {
                        row.Add(rs.Value);

                        if (rs.RemainRows == 1)
                            rowspanMap.Remove(colIndex);
                        else
                            rowspanMap[colIndex] = (rs.Value, rs.RemainRows - 1);

                        colIndex++;
                    }

                    string value = HtmlEntity.DeEntitize(cell.InnerText).Trim();

                    int colspan = cell.GetAttributeValue("colspan", 1);
                    int rowspan = cell.GetAttributeValue("rowspan", 1);

                    for (int i = 0; i < colspan; i++)
                    {
                        row.Add(value);

                        if (rowspan > 1)
                            rowspanMap[colIndex] = (value, rowspan - 1);

                        colIndex++;
                    }
                }

                result.Add(row);
            }

            return result;
        }
        private DataTable GetDataTable(HtmlAgilityPack.HtmlDocument doc)
        {
            var table = new DataTable();

            var rows = doc.DocumentNode.SelectNodes("//table//tr");

            // Header
            var headers = rows[0].SelectNodes("./td|./th")
                .Select(x => HtmlEntity.DeEntitize(x.InnerText).Trim())
                .ToList();

            foreach (var header in headers)
            {
                table.Columns.Add(header);
            }

            // Data
            foreach (var row in rows.Skip(1))
            {
                var cells = row.SelectNodes("./td");
                if (cells == null)
                    continue;

                var dr = table.NewRow();

                for (int i = 0; i < Math.Min(table.Columns.Count, cells.Count); i++)
                {
                    dr[i] = HtmlEntity.DeEntitize(cells[i].InnerText)
                        .Replace("&nbsp;", "")
                        .Trim();
                }

                table.Rows.Add(dr);
            }
            return table;
        }
    }
}
