using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Database.DTO
{
    public class OrderFormInfo
    {
        public string CusCode { get; set; }
        public string SheetName { get; set; }
        public string FilePath { get; set; }
    }
    public class OrderForm
    {
        public string MaKH { get; set; }
        public DateTime NgayDat { get; set; }
        public string PONhuom { get; set; }
        public string PONhuomMoi { get; set; }
        public string MaDonKH { get; set; }
        public string MaHangKH { get; set; }
        public string LieuKH { get; set; }
        public string LieuSd { get; set; }
        public string MauKH { get; set; }
        public string MauSd { get; set; }
        public int Kho { get; set; }
        public double SlDat { get; set; }
        public DateTime ETD { get; set; }
        public double SlXuat { get; set; }
        public DateTime NgayXuat { get; set; }
        public string Invoice { get; set; }
        public string Article { get; set; }
        public string Brand { get; set; }
        public float DonGia { get; set; }
        public float Amount { get; set; }
    }
}
