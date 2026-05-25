using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Database.DTO
{
    public class DyeClaim
    {
        public int Id { get; set; }
        public string ClaimCode { get; set; }
        public string EmployeeCode { get; set; }
        public string DyePO { get; set; }
        public string MaterialCode { get; set; }
        public string ColorCode { get; set; }
        public string CkNO { get; set; }
        public int Width { get; set; }
        public decimal InitialStockQty { get; set; }
        public decimal CurrentStockQty { get; set; }
        public decimal ShipmentQty { get; set; }
        public decimal DefectiveQty { get; set; }
        public decimal DefectivePercent { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime ETD { get; set; }
        public decimal NGQty { get; set; }
        public string NGDescription { get; set; }
        public string ProductSolution { get; set; }
        public string DyeCompanyCode { get; set; }
        public string Note { get; set; }
        public string PurchaseSolution { get; set; }
        public string WarehouseSolution { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
