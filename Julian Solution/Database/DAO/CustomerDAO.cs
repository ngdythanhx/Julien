using  Julian_SolutionDatabase.DTO;
using  Julian_SolutionHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Julian_Solution.Database.DTO;
using Julian_Solution;

namespace  Julian_SolutionDatabase.DAO
{
    public class CustomerDAO
    {
        private static readonly Lazy<CustomerDAO> _instance = new Lazy<CustomerDAO>(() => new CustomerDAO());

        public static CustomerDAO Instance
        {
            get { return _instance.Value; }
        }
        public List<Customer> GetCustomers()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(Config.ConnectionString);
                var lstRs = db.GetListSP<Customer>("SP_Customer_GetAll");
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
        public ResponseSP CreateCustomer(string customerCode, string customerName, string address)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_CustomerCode", customerCode);
                pars[1] = new SqlParameter("@_CustomerName", customerName);
                pars[2] = new SqlParameter("@_Address", address);
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_Customer_Create", pars);
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
        public ResponseSP UpdateCustomer(int id, string customerCode, string customerName, string address)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_Id", id);
                pars[1] = new SqlParameter("@_CustomerCode", customerCode);
                pars[2] = new SqlParameter("@_CustomerName", customerName);
                pars[3] = new SqlParameter("@_Address", address);
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_Customer_UpdateBy_Id", pars);
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
        public ResponseSP DeleteCustomer(int customerId)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Id", customerId);
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_Customer_DelBy_Id", pars);
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
