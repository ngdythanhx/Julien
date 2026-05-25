using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Database.DTO
{
    public class EmployeeTask
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string TaskName { get; set; }
        public DateTime CreateDatetime { get; set; }
        public DateTime CompleteDatetime { get; set; }
        public bool CompleteState { get; set; }
        public string CompleteStateText => CompleteState ? "Đã xong" : "Chưa xong";
        public string Description { get; set; }
        public bool IsHide { get; set; }
    }
}
