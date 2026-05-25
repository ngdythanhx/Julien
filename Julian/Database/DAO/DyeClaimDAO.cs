using Julian.Database.DTO;
using Julian.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Database.DAO
{
    public class DyeClaimDAO
    {
        private static readonly Lazy<DyeClaimDAO> _instance = new Lazy<DyeClaimDAO>(() => new DyeClaimDAO());

        public static DyeClaimDAO Instance
        {
            get { return _instance.Value; }
        }
        public List<DyeClaim> GetDyeClaimAll()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(Config.ConnectionString);
                var lstRs = db.GetListSP<DyeClaim>("SP_DyeClaim_GetAll");
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
        public List<DyeClaim> GetDyeClaim(string employeeCode)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_EmployeeCode", employeeCode);
                db = new DBHelper(Config.ConnectionString);
                var lstRs = db.GetListSP<DyeClaim>("SP_DyeClaim_GetBy_EmployeeCode", pars);
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
        public ResponseSP Add(int Id, string EmployeeCode, string DyePO, string MaterialCode, string ColorCode, string CkNO, int Width,
                                         decimal InitialStockQty, decimal CurrentStockQty, decimal ShipmentQty, decimal DefectiveQty, decimal DefectivePercent,
                                         DateTime ShipDate, DateTime ReturnDate, DateTime ETD, decimal NGQty, string NGDescription, string ProductSolution, string DyeCompanyCode,
                                         string Note, string PurchaseSolution, string WarehouseSolution, string CreatedDate)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[22];
                pars[0] = new SqlParameter("@_EmployeeCode", EmployeeCode);
                pars[1] = new SqlParameter("@_DyePO", DyePO);
                pars[2] = new SqlParameter("@_MaterialCode", MaterialCode);
                pars[3] = new SqlParameter("@_ColorCode", ColorCode);
                pars[4] = new SqlParameter("@_CKNo", CkNO);
                pars[5] = new SqlParameter("@_Width", Width);
                pars[6] = new SqlParameter("@_InitialStockQty", InitialStockQty);
                pars[7] = new SqlParameter("@_CurrentStockQty", CurrentStockQty);
                pars[8] = new SqlParameter("@_ShipmentQty", ShipmentQty);
                pars[9] = new SqlParameter("@_DefectiveQty", DefectiveQty);
                pars[10] = new SqlParameter("@_DefectivePercent", DefectivePercent);
                pars[11] = new SqlParameter("@_ShipDate", ShipDate);
                pars[12] = new SqlParameter("@_ReturnDate", ReturnDate);
                pars[13] = new SqlParameter("@_ETD", ETD);
                pars[14] = new SqlParameter("@_NGQty", NGQty);
                pars[15] = new SqlParameter("@_NGDescription", NGDescription);
                pars[16] = new SqlParameter("@_ProductSolution", ProductSolution);
                pars[17] = new SqlParameter("@_DyeCompanyCode", DyeCompanyCode);
                pars[18] = new SqlParameter("@_Note", Note);
                pars[19] = new SqlParameter("@_PurchaseSolution", PurchaseSolution);
                pars[20] = new SqlParameter("@_WarehouseSolution", WarehouseSolution);
                pars[21] = new SqlParameter("@_CreatedDate", CreatedDate);
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_DyeClaim_Add", pars);
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
        public ResponseSP Update(int Id, string EmployeeCode, string DyePO, string MaterialCode, string ColorCode, string CkNO, int Width,
                                         decimal InitialStockQty, decimal CurrentStockQty, decimal ShipmentQty, decimal DefectiveQty, decimal DefectivePercent,
                                         DateTime ShipDate, DateTime ReturnDate, DateTime ETD, decimal NGQty, string NGDescription, string ProductSolution, string DyeCompanyCode,
                                         string Note, string PurchaseSolution, string WarehouseSolution, string CreatedDate)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[22];
                pars[0] = new SqlParameter("@_Id", Id);
                pars[1] = new SqlParameter("@_DyePO", DyePO);
                pars[2] = new SqlParameter("@_MaterialCode", MaterialCode);
                pars[3] = new SqlParameter("@_ColorCode", ColorCode);
                pars[4] = new SqlParameter("@_CKNo", CkNO);
                pars[5] = new SqlParameter("@_Width", Width);
                pars[6] = new SqlParameter("@_InitialStockQty", InitialStockQty);
                pars[7] = new SqlParameter("@_CurrentStockQty", CurrentStockQty);
                pars[8] = new SqlParameter("@_ShipmentQty", ShipmentQty);
                pars[9] = new SqlParameter("@_DefectiveQty", DefectiveQty);
                pars[10] = new SqlParameter("@_DefectivePercent", DefectivePercent);
                pars[11] = new SqlParameter("@_ShipDate", ShipDate);
                pars[12] = new SqlParameter("@_ReturnDate", ReturnDate);
                pars[13] = new SqlParameter("@_ETD", ETD);
                pars[14] = new SqlParameter("@_NGQty", NGQty);
                pars[15] = new SqlParameter("@_NGDescription", NGDescription);
                pars[16] = new SqlParameter("@_ProductSolution", ProductSolution);
                pars[17] = new SqlParameter("@_DyeCompanyCode", DyeCompanyCode);
                pars[18] = new SqlParameter("@_Note", Note);
                pars[19] = new SqlParameter("@_PurchaseSolution", PurchaseSolution);
                pars[20] = new SqlParameter("@_WarehouseSolution", WarehouseSolution);
                pars[21] = new SqlParameter("@_CreatedDate", CreatedDate);
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_DyeClaim_UpdateBy_Id", pars);
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
        public ResponseSP Delete(int DyeClaimId)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_Id", DyeClaimId);
                db = new DBHelper(Config.ConnectionString);
                return db.GetInstanceSP<ResponseSP>("SP_DyeClaim_DelBy_Id", pars);
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
