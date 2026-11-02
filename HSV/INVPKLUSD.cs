using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSV
{
    public class INVPKLUSD
    {
       // public int ID { get; set; }
        public string Lieu { get; set; }
        public string MauKD { get; set; }
       // public string MauKho { get; set; }
        //public DateTime NgayDatHang { get; set; }
        //public string NgayDatHang_String => NgayDatHang.ToString("yyyy-MM-dd");
        //public string Brand { get; set; }
        //public string CusName { get; set; }
        public string CusPO { get; set; }
        public string CusItemCode { get; set; }

        //public string PONhuom { get; set; }
        //public string PONhuomMoi { get; set; }
       // public string ETD { get; set; }
        //public string ETDNote { get; set; }
        public string Article { get; set; }
        public string ArticleShort => Article.Length > 6 ? Article.Substring(Article.Length - 6, 6) : Article;
        public int Width { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPriceUSD { get; set; }
        public int RollQty
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(RollString))
                        return 0;
                    var x = RollString.Replace(" ", "");
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
        public string RollString { get; set; }
        public string Check
        {
            get
            {
                string chars = "0123456789+*";
                if (RollString.Any(x => !chars.Contains(x)))
                    return "Số cây chứa ký tự không đúng";
                if (string.IsNullOrEmpty(RollString))
                    return "Chưa nhập số cây";
                if (!"0123456789".Contains(RollString.First()))
                    return "Ký tự đầu trong Số cây không đúng";
                if (!"0123456789".Contains(RollString.Last()))
                    return "Ký tự cuối trong Số cây không đúng";
                var arr = RollString.Split('+').Select(x => x.Split('*')).ToArray();
                if (arr.Any(x => x.Any(y => !decimal.TryParse(y, out _))))
                    return "Số cây không hợp lệ";
                if (arr.Any(x => x.Any(y => int.Parse(y) > 100)))
                    return "Có cây lớn hơn 100";
                var e = JHelper.ConvertData.RollStringToQty(RollString);
                if (e.Result <= 0)
                    return e.Msg;
                if (e.Result > Math.Ceiling(Qty))
                    return "Số lượng trên Tem lớn hơn số lượng giao";
                if (e.Result < Math.Ceiling(Qty))
                    return "Số lượng trên Tem nhỏ hơn số lượng giao";
                return "OK";
            }
        }
    }
}
