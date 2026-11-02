using ERP.Models;
using JHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Database.DAO
{
    public class CSDataDAO
    {
        private static readonly Lazy<CSDataDAO> _instance = new Lazy<CSDataDAO>(() => new CSDataDAO());

        public static CSDataDAO Instance
        {
            get { return _instance.Value; }
        }
        private readonly DBHelperAsync _db = new DBHelperAsync(Config.ConnectionString_BasicDB);

        public async Task<ResponseSP> Insert(string CustCode, string MaterialName, string Color, string Follow, string Note, string Article, string SwatchLocation, string Confirmed, string AdditionalOptional1, string AdditionalOptional2, string AdditionalOptional3)
        {
            try
            {
                var result = await _db.GetInstanceSPAsync<ResponseSP>(
                    "sp_CSData_Insert",
                    new DBCommandOptions { RetryCount = 0 },
                    new SqlParameter("@CustCode", CustCode),
                    new SqlParameter("@MaterialName", MaterialName),
                    new SqlParameter("@Color", Color),
                    new SqlParameter("@Follow", Follow),
                    new SqlParameter("@Note", Note),
                    new SqlParameter("@Article", Article),
                    new SqlParameter("@SwatchLocation", SwatchLocation),
                    new SqlParameter("@Confirmed", Confirmed),
                    new SqlParameter("@AdditionalOptional1", AdditionalOptional1),
                    new SqlParameter("@AdditionalOptional2", AdditionalOptional2),
                    new SqlParameter("@AdditionalOptional3", AdditionalOptional3));

                return result ?? new ResponseSP { ErrCode = -1, Msg = "SP không trả về kết quả." };
            }
            catch (Exception ex)
            {
                return new ResponseSP { ErrCode = -1, Msg = ex.Message };
            }
        }
        public async Task<ResponseSP> Update(int ID, string CustCode, string MaterialName, string Color, string Follow, string Note, string Article, string SwatchLocation, string Confirmed, string AdditionalOptional1, string AdditionalOptional2, string AdditionalOptional3)
        {
            try
            {
                var result = await _db.GetInstanceSPAsync<ResponseSP>(
                    "sp_CSData_Update",
                    new DBCommandOptions { RetryCount = 0 },
                    new SqlParameter("@ID", ID),
                    new SqlParameter("@CustCode", CustCode),
                    new SqlParameter("@MaterialName", MaterialName),
                    new SqlParameter("@Color", Color),
                    new SqlParameter("@Follow", Follow),
                    new SqlParameter("@Note", Note),
                    new SqlParameter("@Article", Article),
                    new SqlParameter("@SwatchLocation", SwatchLocation),
                    new SqlParameter("@Confirmed", Confirmed),
                    new SqlParameter("@AdditionalOptional1", AdditionalOptional1),
                    new SqlParameter("@AdditionalOptional2", AdditionalOptional2),
                    new SqlParameter("@AdditionalOptional3", AdditionalOptional3));

                return result ?? new ResponseSP { ErrCode = -1, Msg = "SP không trả về kết quả." };
            }
            catch (Exception ex)
            {
                return new ResponseSP { ErrCode = -1, Msg = ex.Message };
            }
        }
        public async Task<ResponseSP> Delete(int ID)
        {
            try
            {
                var result = await _db.GetInstanceSPAsync<ResponseSP>(
                    "sp_CSData_Delete",
                    new DBCommandOptions { RetryCount = 0 },
                    new SqlParameter("@ID", ID));

                return result ?? new ResponseSP { ErrCode = -1, Msg = "SP không trả về kết quả." };
            }
            catch (Exception ex)
            {
                return new ResponseSP { ErrCode = -1, Msg = ex.Message };
            }
        }
    }
}
