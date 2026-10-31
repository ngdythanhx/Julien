using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Office.Interop;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
namespace JHelper
{
    public class ConvertData
    {
        public static List<T> ConvertToList<T>(DataTable dt) where T : new()
        {
            var list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                T obj = new T();

                foreach (var prop in typeof(T).GetProperties())
                {
                    if (dt.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType));
                    }
                }

                list.Add(obj);
            }

            return list;
        }
        public static DataTable ToDataTable<T>(List<T> data)
        {
            if (data == null)
                return null;
            DataTable dt = new DataTable(typeof(T).Name);

            // Tạo cột
            foreach (var prop in typeof(T).GetProperties())
            {
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // Đổ dữ liệu
            foreach (var item in data)
            {
                var values = new object[dt.Columns.Count];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    values[i] = typeof(T).GetProperties()[i].GetValue(item, null);
                }

                dt.Rows.Add(values);
            }

            return dt;
        }
        public static ExcelData ConvertExcelDataToRows(string text)
        {
            // Loại bỏ dấu xuống dòng thừa ở cuối chuỗi text tổng nếu có để tránh tạo hàng trống
            text = text.TrimEnd('\r', '\n');

            var cells = text.Split('\t');
            var lstRow = new List<string[]>();
            var curRow = new List<string>();

            foreach (var cell in cells)
            {
                if (cell.StartsWith("\"") && cell.EndsWith("\"") && cell.Count(c => c == '\"') == 2)
                {
                    string cleanCell = cell.Substring(1, cell.Length - 2);
                    curRow.Add(cleanCell);
                }
                else if (cell.Contains("\r\n"))
                {
                    var s = cell.Split(new[] { "\r\n" }, StringSplitOptions.None);
                    if (s.Length == 2)
                    {
                        curRow.Add(s[0]); 
                        lstRow.Add(curRow.ToArray()); 

                        curRow = new List<string>(); 
                        curRow.Add(s[1]); 
                    }
                }
                else
                {
                    curRow.Add(cell);
                }
            }
            if (curRow.Count > 0)
            {
                lstRow.Add(curRow.ToArray());
            }

            var excel = new ExcelData() { Data = lstRow };
            return excel;
        }

    }
}
