using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Julian.Enums;

namespace  Julian.Forms
{
    public interface IFormHandler
    {
        int ActionType { get; set; }
        Task LoadData();
        void LockInputs();
        void UnlockInputs();
        void ResetInputs();
        Task<bool> CreateData();
        Task<bool> UpdateData();
        Task<bool> DeleteData();
    }
}
