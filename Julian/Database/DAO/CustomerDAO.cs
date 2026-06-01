using Julian.Database.DTO;
using Julian.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Database.DAO
{
    public class CustomerDAO
    {
        private static readonly Lazy<CustomerDAO> _instance = new Lazy<CustomerDAO>(() => new CustomerDAO());

        public static CustomerDAO Instance
        {
            get { return _instance.Value; }
        }
        public async Task<List<Customer>> GetCustomers()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(Config.Instance.ConnectionString);
                var lstRs = await db.GetListSPAsync<Customer>("SP_Customer_GetAll");
                return lstRs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ResponseSP> CreateCustomer(string customerCode, string customerName, string address)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_CustomerCode", customerCode);
                pars[1] = new SqlParameter("@_CustomerName", customerName);
                pars[2] = new SqlParameter("@_Address", address);
                db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_Customer_Create",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
        }
        public async Task<ResponseSP> UpdateCustomer(int id, string customerCode, string customerName, string address)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_Id", id);
                pars[1] = new SqlParameter("@_CustomerCode", customerCode);
                pars[2] = new SqlParameter("@_CustomerName", customerName);
                pars[3] = new SqlParameter("@_Address", address);
                db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_Customer_UpdateBy_Id",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
        }
        public async Task<ResponseSP> DeleteCustomer(int customerId)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Id", customerId);
                db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_Customer_DelBy_Id",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }

        }
    }
}
