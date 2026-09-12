using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSV
{
    public class ListGuiDiem2
    {
        public int ID { get; set; }
        public string Lieu {  get; set; }
        public string MauKD {  get; set; }
        public string MauKho {  get; set; }
        public DateTime NgayDatHang { get; set; }
        public string NgayDatHang_String => NgayDatHang.ToString("yyyy-MM-dd");
        public string Brand { get; set; }
        public string CusName { get; set; }
        public string CusPO { get; set; }
        public string CusItemCode { get; set; }

        public string PONhuom {  get; set; }
        public string PONhuomMoi {  get; set; }
        public string ETD {  get; set; }
        public string ETDNote {  get; set; }
        public string CusNote {  get; set; }
        public string Article {  get; set; }
        public int Width {  get; set; }
        public decimal Qty {  get; set; }
    }
}
