using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JH
{
    public class PKLHD
    {
        public string PO { get; set; }
        public string MaterialCode { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Thickness { get; set; }
        public decimal Qty { get; set; }
        public decimal Roll { get; set; }
        public string Unit => "YARD";
        public int RollQty => 1;
    }
}
