using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Database.DTO
{
    public class OrderForm
    {
        public string MaKH { get; set; }
        public DateTime? NgayDat { get; set; }
        public string Brand { get; set; }
        public string PONhuom { get; set; }
        public string PONhuomMoi { get; set; }
        public string MaDonKH { get; set; }
        public string MaHangKH { get; set; }
        public string LieuKH { get; set; }
        public string LieuThayThe { get; set; }
        public int Kho { get; set; }
        public string MauKH { get; set; }
        //moi
        public string TenMauDayDu { get; set; }
        public string MauThayThe { get; set; }
        public float SLDat { get; set; }
        public float DonGia { get; set; }
        public double TongTien { get; set; }
        public string ETD { get; set; }
        public DateTime? NgayXuat { get; set; }
        public string InvoiceHoaDon { get; set; }
        public string InvoicePGH { get; set; }
        public string Article { get; set; }
        public string BillNumber { get; set; }
        public string ShippingMethod { get; set; }
        public string T1 {  get; set; }
        public string Season {  get; set; }
        public string ETDNote { get; set; }
        //moi
        public string Category { get; set; }
        //moi
        public string TenGiay { get; set; }

    }
}
