using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JH
{
    public class PhieuGiaoHang
    {
        public int ID { get; set; }
        public string PO { get; set; }
        public string MaterialCode { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Width { get; set; }
        public int Weight { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; } = "YARD";
        public string Roll { get; set; }
        public int RollQty
        {
            get
            {
                try
                {
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
