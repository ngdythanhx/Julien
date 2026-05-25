using Julian_Solution;
using  Julian_SolutionDatabase.DTO;
using  Julian_SolutionHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Julian_SolutionDatabase.DAO
{
    public class EmployeeTaskDAO
    {
        private static readonly Lazy<EmployeeTaskDAO> _instance = new Lazy<EmployeeTaskDAO>(() => new EmployeeTaskDAO());

        public static EmployeeTaskDAO Instance
        {
            get { return _instance.Value; }
        }
        public List<EmployeeTask> GetEmployeeTasks()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(Config.ConnectionString);
                var lstRs = db.GetListSP<EmployeeTask>("SP_EmployeeTask_GetAll");
                return lstRs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
        }
        public List<EmployeeTask> GetEmployeeTasks(int employeeId)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_EmployeeId", employeeId);
                db = new DBHelper(Config.ConnectionString);
                var lstRs = db.GetListSP<EmployeeTask>("SP_EmployeeTask_GetBy_EmployeeId", pars);
                return lstRs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
        }
        public ResponseSP CreateEmployeeTask(int employeeId, string taskName, DateTime createdDatetime, DateTime completeDatetime, string description)
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
                db = new DBHelper(Config.ConnectionString);
                var lst = db.GetInstanceSP<ResponseSP>("SP_EmployeeTask_Create", pars);
                return lst;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
        }
        public ResponseSP UpdateEmployeeTask(int id, int employeeId, string taskName, DateTime createdDatetime, DateTime completeDatetime,bool complateState, string description)
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
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_EmployeeTask_UpdateBy_Id", pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
        }
        public ResponseSP UpdateComplateState(int id, bool complateState, DateTime completedDatetime)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_Id", id);
                pars[1] = new SqlParameter("@_CompleteState", complateState);
                pars[2] = new SqlParameter("@_CompleteDatetime", completedDatetime);
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_EmployeeTask_UpdateCompleteStateBy_Id", pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
        }
        public ResponseSP DeleteEmployeeTask(int EmployeeTaskId)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Id", EmployeeTaskId);
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_EmployeeTask_DelBy_Id", pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
        }
    }
}
