using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSV
{
    public class PKLXE
    {
        public int ID { get; set; }
        public string PO { get; set; }
        public string BuyMonth { get; set; }
        public string Article { get; set; }
        public string MaterialCode { get; set; }
        public string DyeLot { get; set; }
        public string HSCode { get; set; }
        public string SaleNo { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Thickness { get; set; }
        public decimal Qty { get; set; }
        public decimal TotalQty { get; set; }
        public decimal Roll { get; set; }
        public string Unit => "YARD";
        public int  RollQty => 1;
    }
}
