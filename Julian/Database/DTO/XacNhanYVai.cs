using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Database.DTO
{
    public class XacNhanYVai
    {
        [DisplayName("Ngày Nhập")]
        public object NgayNhap { get; set; }
        [DisplayName("PO Nhuộm")]
        public string PONhuom { get; set; }
        [DisplayName("P/S")]
        public string PS { get; set; }
        [DisplayName("Liệu")]
        public string Lieu { get; set; }
        [DisplayName("Màu")]
        public string Mau { get; set; }
        [DisplayName("Kết quả")]
        public string KetQua { get; set; }
        [DisplayName("Chi tiết")]
        public string ChiTiet { get; set; }
        [DisplayName("ETD")]
        public object ETD { get; set; }
        [DisplayName("Ghi chú giao hàng")]
        public string GhiChuGiaoHang { get; set; }

    }
}
