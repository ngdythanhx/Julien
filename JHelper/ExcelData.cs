using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHelper
{
    public class ExcelData
    {
        public List<string[]> Data { get; set; }
        public int RowCount => Data?.Count ?? 0;
        public int ColumnCount => Data?.Max(x => x.Length) ?? 0;
    }
}
