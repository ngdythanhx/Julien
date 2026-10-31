using JServer;
using JServer.Models.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ERP.Forms
{
    public partial class frmFinder : Form
    {
        public frmFinder()
        {
            InitializeComponent();
        }

        private void frmFinder_Load(object sender, EventArgs e)
        {
            
        }
        private void CreateTableQuery()
        {
            var dt = new DataTable();
            string custCode = txtCustCode.Text.Trim().ToUpper();//客戶代碼
            string[] lstMadon = new string[0];
            string[] lstMaLieu = new string[0];
            string[] lstSoLuong = new string[0];
            if (chkCustomerOrderNumber.Checked)
            {
                DataColumn col = new DataColumn("客戶單號", typeof(string));
                col.Caption = "Mã đơn Khách Hàng";
                dt.Columns.Add(col);
                lstMadon = txtCustomerOrderNumber.Text.TrimEnd('\r', '\n').Split(new[] { "\r\n" }, StringSplitOptions.None);
            }
            if (chkCustomerMaterialCode.Checked)
            {
                DataColumn col = new DataColumn("客戶(採購)單號", typeof(string));
                col.Caption = "Mã liệu Khách Hàng";
                dt.Columns.Add(col);
                lstMaLieu = txtCustomerMaterialCode.Text.TrimEnd('\r', '\n').Split(new[] { "\r\n" }, StringSplitOptions.None);
            }
            if (chkCustomerQty.Checked)
            {
                DataColumn col = new DataColumn("客戶訂量", typeof(string));
                col.Caption = "Số lượng đặt hàng";
                dt.Columns.Add(col);
                lstSoLuong = txtCustomerQty.Text.TrimEnd('\r', '\n').Split(new[] { "\r\n" }, StringSplitOptions.None);
            }
            if (dt.Columns.Count == 0)
                return;
            int maxRows = Math.Max(lstMadon.Length, Math.Max(lstMaLieu.Length, lstSoLuong.Length));
            if (!string.IsNullOrEmpty(custCode))
            {
                DataColumn col = new DataColumn("客戶代碼", typeof(string));
                col.Caption = "Mã Khách Hàng";
                dt.Columns.Add(col);
                for (int i = 0; i < maxRows; i++)
                {
                    DataRow row = dt.NewRow();
                    if (chkCustomerOrderNumber.Checked)
                    {
                        row["客戶單號"] = i < lstMadon.Length ? lstMadon[i] : "";
                    }
                    if (chkCustomerMaterialCode.Checked)
                    {
                        row["客戶(採購)單號"] = i < lstMaLieu.Length ? lstMaLieu[i] : "";
                    }
                    if (chkCustomerQty.Checked)
                    {
                        row["客戶訂量"] = i >= lstSoLuong.Length ? -1 : decimal.TryParse(lstSoLuong[i], out decimal qty) ? qty : -1;
                    }
                    row["客戶代碼"] = custCode;
                    dt.Rows.Add(row);
                }
            }
            else
            {
                for (int i = 0; i < maxRows; i++)
                {
                    DataRow row = dt.NewRow();
                    if (chkCustomerOrderNumber.Checked)
                    {
                        row["客戶單號"] = i < lstMadon.Length ? lstMadon[i] : "";
                    }
                    if (chkCustomerMaterialCode.Checked)
                    {
                        row["客戶(採購)單號"] = i < lstMaLieu.Length ? lstMaLieu[i] : "";
                    }
                    if (chkCustomerQty.Checked)
                    {
                        row["客戶訂量"] = i >= lstSoLuong.Length ? -1 : decimal.TryParse(lstSoLuong[i], out decimal qty) ? qty : -1;
                    }
                    dt.Rows.Add(row);
                }
            }
            btnSearch.Enabled = dt.Rows.Count > 0;
            dgvFind.DataSource = dt;
        }
        public static string BuildQuery(DataTable dt)
        {
            if (dt == null || dt.Columns.Count == 0 || dt.Rows.Count == 0)
                throw new ArgumentException("DataTable không có dữ liệu.");

            string columns = string.Join(", ",
                dt.Columns.Cast<DataColumn>()
                .Select(c => $"[{c.ColumnName.Replace("]", "]]")}]"));

            string conditions = string.Join(" AND ",
                dt.Columns.Cast<DataColumn>()
                .Select(c => $"t.[{c.ColumnName.Replace("]", "]]")}] = s.[{c.ColumnName.Replace("]", "]]")}]"));

            string values = string.Join(",\n",
                dt.Rows.Cast<DataRow>().Select(row =>
                    "(" + string.Join(", ",
                        dt.Columns.Cast<DataColumn>()
                        .Select(c => ToSqlValue(row[c], c.DataType))) + ")"));

            return $@"SELECT t.* FROM ledway.vwOrderNoDelMain t WHERE EXISTS (SELECT 1 FROM (VALUES {values} ) s({columns}) WHERE {conditions})";
        }

        private static string ToSqlValue(object value, Type type)
        {
            if (value == DBNull.Value || value == null)
            {
                string sqlType = type == typeof(string) ? "NVARCHAR(MAX)"
                    : type == typeof(DateTime) ? "DATETIME2"
                    : type == typeof(bool) ? "BIT"
                    : type == typeof(Guid) ? "UNIQUEIDENTIFIER"
                    : type == typeof(byte[]) ? "VARBINARY(MAX)"
                    : type == typeof(double) ? "FLOAT"
                    : type == typeof(float) ? "REAL"
                    : type == typeof(decimal) ? "DECIMAL(38, 10)"
                    : type == typeof(long) || type == typeof(ulong) ? "BIGINT"
                    : type == typeof(int) || type == typeof(uint) ? "INT"
                    : type == typeof(short) || type == typeof(ushort) ? "SMALLINT"
                    : type == typeof(byte) || type == typeof(sbyte) ? "TINYINT"
                    : throw new NotSupportedException($"Kiểu dữ liệu: {type.Name}");

                return $"CAST(NULL AS {sqlType})";
            }

            if (type == typeof(string))
                return "N'" + value.ToString().Replace("'", "''") + "'";

            if (type == typeof(DateTime))
                return $"CAST('{((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ss.fffffff", CultureInfo.InvariantCulture)}' AS DATETIME2)";

            if (type == typeof(bool))
                return (bool)value ? "1" : "0";

            if (type == typeof(Guid))
                return $"CAST('{value}' AS UNIQUEIDENTIFIER)";

            if (type == typeof(byte[]))
                return "0x" + BitConverter.ToString((byte[])value).Replace("-", "");

            if (type == typeof(double) || type == typeof(float) ||
                type == typeof(decimal) || type == typeof(int) ||
                type == typeof(uint) || type == typeof(long) ||
                type == typeof(ulong) || type == typeof(short) ||
                type == typeof(ushort) || type == typeof(byte) ||
                type == typeof(sbyte))
                return Convert.ToString(value, CultureInfo.InvariantCulture);

            throw new NotSupportedException($"Kiểu dữ liệu: {type.Name}");
        }

        private void chkCustomerOrderNumber_CheckedChanged(object sender, EventArgs e)
        {
            CreateTableQuery();
            txtCustomerOrderNumber.Enabled = chkCustomerOrderNumber.Checked;
        }

        private void chkCustomerMaterialCode_CheckedChanged(object sender, EventArgs e)
        {
            CreateTableQuery();
            txtCustomerMaterialCode.Enabled = chkCustomerMaterialCode.Checked;
        }

        private void chkCustomerQty_CheckedChanged(object sender, EventArgs e)
        {
            CreateTableQuery();
            txtCustomerQty.Enabled = chkCustomerQty.Checked;
        }

        private void txtCustomerOrderNumber_TextChanged(object sender, EventArgs e)
        {
            CreateTableQuery();
        }

        private void txtCustomerMaterialCode_TextChanged(object sender, EventArgs e)
        {
            CreateTableQuery();
        }

        private void txtCustomerQty_TextChanged(object sender, EventArgs e)
        {
            CreateTableQuery();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var dt = dgvFind.DataSource as DataTable;
            var x = BuildQuery(dt);
            var api = new ServerAPI();
            var a = await api.Login("b019", "b019");
            var b = await api.GetUserInfo();
            var c = await api.ExQuery<OrderNoDelMain>(x);
            var d = c.Recordset[0];
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            try
            {
                var dt = dgvFind.DataSource as DataTable;
                var x = BuildQuery(dt);
                var api = new ServerAPI();
                var a = await api.Login("b019", "b019");
                var b = await api.GetUserInfo();
                var c = await api.ExQuery<OrderNoDelMain>(x);
                dgvMain.DataSource = c.Recordset;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSearch.Enabled = true;
            }
        }
    }
}
