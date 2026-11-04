using ERP.Models;
using JHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
        public async Task<DataTable> GetAll()
        {
            try
            {
                var result = await _db.GetDataTableSPAsync("sp_CSData_GetAll");

                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<DataTable> GetByCustCode(string custCode)
        {
            try
            {
                var result = await _db.GetDataTableSPAsync(
                    "sp_CSData_GetByCustCode",
                    new DBCommandOptions { RetryCount = 0 },
                    new SqlParameter("@CustCode", custCode));

                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ResponseSP> Insert(string CustCode, string MaterialName, int Width, string Color, string ReplacementColor, string Follow, string Note, string Article, int Confirmed, string AdditionalOptional1, string AdditionalOptional2, string AdditionalOptional3, DateTime CreatedDatetime)
        {
            try
            {
                var result = await _db.GetInstanceSPAsync<ResponseSP>(
                    "sp_CSData_Insert",
                    new DBCommandOptions { RetryCount = 0 },
                    new SqlParameter("@CustCode", CustCode.ToUpper()),
                    new SqlParameter("@MaterialName", MaterialName.ToUpper()),
                    new SqlParameter("@Width", Width),
                    new SqlParameter("@Color", Color.ToUpper()),
                    new SqlParameter("@ReplacementColor", ReplacementColor.ToUpper()),
                    new SqlParameter("@Follow", Follow.ToUpper()),
                    new SqlParameter("@Note", Note),
                    new SqlParameter("@Article", Article.ToUpper()),
                    new SqlParameter("@Confirmed", Confirmed),
                    new SqlParameter("@AdditionalOptional1", AdditionalOptional1),
                    new SqlParameter("@AdditionalOptional2", AdditionalOptional2),
                    new SqlParameter("@AdditionalOptional3", AdditionalOptional3),
                    new SqlParameter("@CreatedDatetime", CreatedDatetime));

                return result ?? new ResponseSP { ErrCode = -1, Msg = "SP không trả về kết quả." };
            }
            catch (Exception ex)
            {
                return new ResponseSP { ErrCode = -1, Msg = ex.Message };
            }
        }
        public async Task<ResponseSP> Update(int ID, string CustCode, string MaterialName, int Width, string Color, string ReplacementColor, string Follow, string Note, string Article, int Confirmed, string AdditionalOptional1, string AdditionalOptional2, string AdditionalOptional3, DateTime CreatedDatetime)
        {
            try
            {
                var result = await _db.GetInstanceSPAsync<ResponseSP>(
                    "sp_CSData_Update",
                    new DBCommandOptions { RetryCount = 0 },
                    new SqlParameter("@ID", ID),
                    new SqlParameter("@CustCode", CustCode.ToUpper()),
                    new SqlParameter("@MaterialName", MaterialName.ToUpper()),
                    new SqlParameter("@Width", Width),
                    new SqlParameter("@Color", Color.ToUpper()),
                    new SqlParameter("@ReplacementColor", ReplacementColor.ToUpper()),
                    new SqlParameter("@Follow", Follow.ToUpper()),
                    new SqlParameter("@Note", Note),
                    new SqlParameter("@Article", Article.ToUpper()),
                    new SqlParameter("@Confirmed", Confirmed),
                    new SqlParameter("@AdditionalOptional1", AdditionalOptional1),
                    new SqlParameter("@AdditionalOptional2", AdditionalOptional2),
                    new SqlParameter("@AdditionalOptional3", AdditionalOptional3),
                    new SqlParameter("@CreatedDatetime", CreatedDatetime));

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
        public CSData FromRow(DataRow dr) => new CSData
        {
            ID = dr.Field<int>("ID"),
            CustCode = dr.Field<string>("CustCode"),
            MaterialName = dr.Field<string>("MaterialName"),
            Width = dr.Field<int>("Width"),
            Color = dr.Field<string>("Color"),
            ReplacementColor = dr.Field<string>("ReplacementColor"),
            Follow = dr.Field<string>("Follow"),
            Note = dr.Field<string>("Note"),
            Article = dr.Field<string>("Article"),
            Confirmed = dr.Field<int?>("Confirmed"),
            AdditionalOptional1 = dr.Field<string>("AdditionalOptional1"),
            AdditionalOptional2 = dr.Field<string>("AdditionalOptional2"),
            AdditionalOptional3 = dr.Field<string>("AdditionalOptional3"),
            CreatedDatetime = dr.Field<DateTime?>("CreatedDatetime"),
        };
    }
}
