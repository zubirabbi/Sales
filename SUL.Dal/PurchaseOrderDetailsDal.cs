using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class PurchaseOrderDetailsDal : SUL.Dal.Base.PurchaseOrderDetailsDalBase
	{
		public PurchaseOrderDetailsDal() : base()
		{
		}

        public DataTable GetAllPurchaseOrderDetailsBymasterId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("PurchaseOrderDetails", "*", " Where MasterId = @PurchaseOrderMasterId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllPurchaseOrderDetailsBymasterIdProductIdCateId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("PurchaseOrderDetails", "*", " Where ProductId = @ProductId And UnitId = @UnitId And MasterId = @PurchaseOrderMasterId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int DeletePurchaseOrderDetailsByPOMasterId(Hashtable lstData)
        {
            string sqlQuery = "delete from  PurchaseOrderDetails where MasterId = @MasterId;";
            try
            {
                int success = ExecuteNonQuery(sqlQuery, lstData);
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }
	}
}
