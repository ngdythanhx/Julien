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
    public class SHCFollowDAO
    {
        private static readonly Lazy<SHCFollowDAO> _instance = new Lazy<SHCFollowDAO>(() => new SHCFollowDAO());
        public static SHCFollowDAO Instance=> _instance.Value;
        public async Task<List<SHCFollow>> GetSHCFollows()
        {
            try
            {
                var db = new DBHelper(Config.Instance.ConnectionString);
                var lstRs =await db.GetListSPAsync<SHCFollow>("SP_SHCFollow_GetAll");
                return lstRs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ResponseSP> Add(string season, string materialCode, string article,string follow,string note)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@Season", season);
                pars[1] = new SqlParameter("@MaterialCode", materialCode);
                pars[2] = new SqlParameter("@Article", article);
                pars[3] = new SqlParameter("@Follow", follow);
                pars[4] = new SqlParameter("@Note", note);
                db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_SHCFollow_Add",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
        }
        public async Task<ResponseSP> Update(int id,string season, string materialCode, string article, string follow, string note)
        {
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@ID", id);
                pars[1] = new SqlParameter("@Season", season);
                pars[2] = new SqlParameter("@MaterialCode", materialCode);
                pars[3] = new SqlParameter("@Article", article);
                pars[4] = new SqlParameter("@Follow", follow);
                pars[5] = new SqlParameter("@Note", note);
               var  db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_SHCFollow_Update",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }

        }
        public async Task<ResponseSP> DeleteSHCFollow(int customerId)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Id", customerId);
               var db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_SHCFollow_Delete",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
        }
    }
}
