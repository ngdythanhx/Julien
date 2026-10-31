using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trustrace;

namespace SMTTV2
{
    public class OutboundUploadItem
    {
        //TrustraceAPI _trustraceAPI;
        /*public OutboundUploadItem(TrustraceAPI trustraceAPI)
        {
            _trustraceAPI = trustraceAPI;
        }*/
        public string MaterialNumber { get; set; }
        public decimal TotalQtyInKG { get; set; }
        public decimal TotalQtyInYard { get; set; }
        public string T2ID { get; set; }
        public string T1Name { get; set; }
        public string T1Code { get; set; }
        public string Status { get; set; }
        /*public async Task<bool> CheckData()
        {
            Status = "Đang kiểm tra dữ liệu..";
            var check = await _trustraceAPI.SmartSegregation.CheckData(MaterialNumber, T2ID, TotalQtyInKG, T1Code);
            if (check.success)
            {
                Status = "Đã sẵn sàng!";
                return true;
            }
            Status = check.msg;
            return false;
        }
        public async Task<bool> Upload()
        {
            Status = "Đang upload..";
            var upload = await _trustraceAPI.SmartSegregation.UploadOutbound(MaterialNumber, T2ID, TotalQtyInKG, T1Code);
            Status = upload.msg;
            return upload.success;
        }*/
    }
}
