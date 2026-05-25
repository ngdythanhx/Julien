using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Helper
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
    }
}
