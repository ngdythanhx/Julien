using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian_Server
{
    public class FilterByText
    {
        public string MaDonKH {  get; set; }
        public string MaHangKH {  get; set; }
        public int SlDat { get; set; } = -1;
        public DateTime NgayGiao { get; set; } = DateTime.MinValue;
    }
}
