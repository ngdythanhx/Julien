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
    public class EmployeeDAO
    {
        private static readonly Lazy<EmployeeDAO> _instance = new Lazy<EmployeeDAO>(() => new EmployeeDAO());

        public static EmployeeDAO Instance
        {
            get { return _instance.Value; }
        }
        public List<Employee> GetEmployees()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(Config.ConnectionString);
                var lst = db.GetList<Employee>("SP_Employee_GetAll");
                return lst;
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
        public Employee GetEmployee(string employeeCode)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_EmployeeCode", employeeCode);
                db = new DBHelper(Config.ConnectionString);
                var em = db.GetInstanceSP<Employee>("SP_Employee_GetBy_EmployeeCode",pars);
                return em;
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
        public ResponseSP CreateEmployee(string employeeCode,string employeeName, DateTime brithday)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_EmployeeCode", employeeCode);
                pars[1] = new SqlParameter("@_EmployeeName", employeeName);
                pars[2] = new SqlParameter("@_Birthday", brithday);
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_Employee_Create", pars);
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
        public ResponseSP UpdateEmployee(int id,string employeeCode, string employeeName, DateTime brithday)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_Id", id);
                pars[1] = new SqlParameter("@_EmployeeCode", employeeCode);
                pars[2] = new SqlParameter("@_EmployeeName", employeeName);
                pars[3] = new SqlParameter("@_Birthday", brithday);
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_Employee_UpdateBy_Id", pars);
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
        public ResponseSP DeleteEmployee(int EmployeeId)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Id", EmployeeId);
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_Employee_DelBy_Id", pars);
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
