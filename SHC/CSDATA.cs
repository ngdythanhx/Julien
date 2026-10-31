using Julian.Database.DTO;
using Julian.Helper;
using Julian.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC
{
    public class CSDATA
    {
        private static readonly Lazy<CSDATA> _instance = new Lazy<CSDATA>(() => new CSDATA());
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        public static CSDATA Instance
        {
            get { return _instance.Value; }
        }
        public async Task<DataTable> GetAll()
        {
            DBHelper db = null;
            try
            {
                //db = new DBHelper(_iniManager.GetString("Default", "ConnectionString"));
                db = new DBHelper(_iniManager.GetString("Default", "ConnectionString"));
                var tb = await db.GetDataTableSPAsync("SP_HSVCS_GetAll");
                return tb;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<DataTable> Get(params string[] materials)
        {
            DBHelper db = null;
            try
            {
                //db = new DBHelper(_iniManager.GetString("Default", "ConnectionString"));
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@List", string.Join(";", materials));
                db = new DBHelper(_iniManager.GetString("Default", "ConnectionString"));
                var tb = await db.GetDataTableSPAsync("SP_HSVCS_GetByMaterials", default, pars);
                return tb;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<ResponseSP> Add(string article, string material, string color, string follow, string purchaseSpecification, string note, string swatchLocation, bool confirmed)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[8];
                pars[0] = new SqlParameter("@Article", article);
                pars[1] = new SqlParameter("@Material", material);
                pars[2] = new SqlParameter("@Color", color);
                pars[3] = new SqlParameter("@Follow", follow);
                pars[4] = new SqlParameter("@PurchaseSpecification", purchaseSpecification);
                pars[5] = new SqlParameter("@Note", note);
                pars[6] = new SqlParameter("@SwatchLocation", swatchLocation);
                pars[7] = new SqlParameter("@Confirmed", confirmed);
                db = new DBHelper(_iniManager.GetString("Default", "ConnectionString"));
                return await db.GetInstanceSPAsync<ResponseSP>("SP_HSVCS_Add", default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }

        }
        public async Task<ResponseSP> Update(int ID, string article, string material, string color, string follow, string purchaseSpecification, string note, string swatchLocation, bool confirmed)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[9];
                pars[0] = new SqlParameter("@ID", ID);
                pars[1] = new SqlParameter("@Article", article);
                pars[2] = new SqlParameter("@Material", material);
                pars[3] = new SqlParameter("@Color", color);
                pars[4] = new SqlParameter("@Follow", follow);
                pars[5] = new SqlParameter("@PurchaseSpecification", purchaseSpecification);
                pars[6] = new SqlParameter("@Note", note);
                pars[7] = new SqlParameter("@SwatchLocation", swatchLocation);
                pars[8] = new SqlParameter("@Confirmed", confirmed);
                db = new DBHelper(_iniManager.GetString("Default", "ConnectionString"));
                return await db.GetInstanceSPAsync<ResponseSP>("SP_HSVCS_Update", default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
        }
        public async Task<ResponseSP> UpdateConfirm(int ID, bool confirmed)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@ID", ID);
                pars[1] = new SqlParameter("@Confirmed", confirmed);
                db = new DBHelper(_iniManager.GetString("Default", "ConnectionString"));
                return await db.GetInstanceSPAsync<ResponseSP>("SP_HSVCS_UpdateConfirm", default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
        }
        public async Task<ResponseSP> Delete(int ID)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@ID", ID);
                db = new DBHelper(_iniManager.GetString("Default", "ConnectionString"));
                return await db.GetInstanceSPAsync<ResponseSP>("SP_HSVCS_Delete", default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
        }
    }
}
