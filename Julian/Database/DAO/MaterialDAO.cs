using Julian.Database.DTO;
using Julian.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Database.DAO
{
    public class MaterialDAO
    {
        private static readonly Lazy<MaterialDAO> _instance = new Lazy<MaterialDAO>(() => new MaterialDAO());

        public static MaterialDAO Instance
        {
            get { return _instance.Value; }
        }
        public async Task<List<Material>> GetMaterials()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(Config.Instance.ConnectionString);
                var lstRs =await db.GetListSPAsync<Material>("SP_Material_GetAll");
                return lstRs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
 
        }
        public async Task<ResponseSP> CreateMaterial(string materialCode,string materialColor,string customerColor,string standardColor,string description)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_MaterialCode", materialCode);
                pars[1] = new SqlParameter("@_MaterialColor", materialColor);
                pars[2] = new SqlParameter("@_CustomerColor", customerColor);
                pars[3] = new SqlParameter("@_StandardColor", standardColor);
                pars[4] = new SqlParameter("@_Description",description);
                db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_Material_Create",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
 
        }
        public async Task<ResponseSP> UpdateMaterial(int id,string materialCode, string materialColor, string customerColor, string standardColor, string description)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@_Id", id);
                pars[1] = new SqlParameter("@_MaterialCode", materialCode);
                pars[2] = new SqlParameter("@_MaterialColor", materialColor);
                pars[3] = new SqlParameter("@_CustomerColor", customerColor);
                pars[4] = new SqlParameter("@_StandardColor", standardColor);
                pars[5] = new SqlParameter("@_Description", description);
                db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_Material_UpdateBy_Id",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
 
        }
        public async Task<ResponseSP> DeleteMaterial(int MaterialId)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Id", MaterialId);
                db = new DBHelper(Config.Instance.ConnectionString);
                return await db.GetInstanceSPAsync<ResponseSP>("SP_Material_DelBy_Id",default, pars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResponseSP() { ErrCode = -1, Msg = ex.Message };
            }
 
        }
    }
}
