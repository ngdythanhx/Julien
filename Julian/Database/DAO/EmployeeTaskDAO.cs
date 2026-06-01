using Julian.Database.DTO;
using Julian.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Database.DAO
{
    public class EmployeeTaskDAO
    {
        private static readonly Lazy<EmployeeTaskDAO> _instance = new Lazy<EmployeeTaskDAO>(() => new EmployeeTaskDAO());

        public static EmployeeTaskDAO Instance
        {
            get { return _instance.Value; }
        }
        public async Task<List<EmployeeTask>> GetEmployeeTasks()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(Config.Instance.ConnectionString);
                var lstRs =await db.GetListSPAsync<EmployeeTask>("SP_EmployeeTask_GetAll");
                return lstRs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
 
        }
        public async Task<List<EmployeeTask>> GetEmployeeTasks(int employeeId)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_EmployeeId", employeeId);
                db = new DBHelper(Config.Instance.ConnectionString);
                var lstRs =await db.GetListSPAsync<EmployeeTask>("SP_EmployeeTask_GetBy_EmployeeId",default, pars);
                return lstRs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
 
        }
        public async Task<ResponseSP> CreateEmployeeTask(int employeeId, string taskName, DateTime createdDatetime, DateTime completeDatetime, string description)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_EmployeeIdx", employeeId);
                pars[1] = new SqlParameter("@_TaskName", taskName);
                pars[2] = new SqlParameter("@_CreateDatetime", createdDatetime);
                pars[3] = new SqlParameter("@_CompleteDatetime", completeDatetime);
                pars[4] = new SqlParameter("@_Description", description);
                db = new DBHelper(Config.Instance.ConnectionString);
                var lst =await db.GetInstanceSPAsync<ResponseSP>("SP_EmployeeTask_Create",default, pars);
                return lst;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
 
        }
        public async Task<ResponseSP> UpdateEmployeeTask(int id, int employeeId, string taskName, DateTime createdDatetime, DateTime completeDatetime,bool complateState, string description)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@_Id", id);
                pars[1] = new SqlParameter("@_EmployeeId", employeeId);
                pars[2] = new SqlParameter("@_TaskName", taskName);
                pars[3] = new SqlParameter("@_CreateDatetime", createdDatetime);
                pars[4] = new SqlParameter("@_CompleteDatetime", completeDatetime);
                pars[5] = new SqlParameter("@_CompleteDatetime", completeDatetime);
                pars[5] = new SqlParameter("@_CompleteState", complateState);
                pars[6] = new SqlParameter("@_Description", description);
                db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_EmployeeTask_UpdateBy_Id",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            } 
        }
        public async Task<ResponseSP> UpdateComplateState(int id, bool complateState, DateTime completedDatetime)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_Id", id);
                pars[1] = new SqlParameter("@_CompleteState", complateState);
                pars[2] = new SqlParameter("@_CompleteDatetime", completedDatetime);
                db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_EmployeeTask_UpdateCompleteStateBy_Id",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
 
        }
        public async Task<ResponseSP> DeleteEmployeeTask(int EmployeeTaskId)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Id", EmployeeTaskId);
                db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_EmployeeTask_DelBy_Id",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
 
        }
    }
}
