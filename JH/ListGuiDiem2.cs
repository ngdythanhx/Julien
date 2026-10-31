using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JH
{
    public class ListGuiDiem2
    {
        public int ID { get; set; }
        public string Lieu { get; set; }
        public string MauKD { get; set; }
        public string MauKho { get; set; }
        public DateTime NgayDatHang { get; set; }
        public string NgayDatHang_String => NgayDatHang.ToString("yyyy-MM-dd");
        public string Brand { get; set; }
        public string CusName { get; set; }
        public string CusPO { get; set; }
        public string CusItemCode { get; set; }

        public string PONhuom { get; set; }
        public string PONhuomMoi { get; set; }
        public string ETD { get; set; }
        public string ETDNote { get; set; }
        public string CusNote { get; set; }
        public string Article { get; set; }
        public int Width { get; set; }
        public int Weight { get; set; }
        public decimal Qty { get; set; } = 0;
        public decimal UnitPrice { get; set; } = 0;
        public decimal UnitPriceVND => UnitPrice * ExchangeRate;
        public decimal AmountVND => Qty * ExchangeRate * UnitPrice;
        public int ExchangeRate { get; set; }
        public string Roll { get; set; }
        public int RollQty
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(Roll))
                        return 0;
                    var x = Roll.Replace(" ", "");
                    var l = x.Split('+');
                    int n = 0;
                    foreach (var item in l)
                    {
                        if (item.Contains("*"))
                        {
                            n += int.Parse(item.Substring(0, item.IndexOf('*')));
                        }
                        else
                            n++;
                    }
                    return n;
                }
                catch
                {
                    return -1
                        ;
                }
            }
        }
    }
}
