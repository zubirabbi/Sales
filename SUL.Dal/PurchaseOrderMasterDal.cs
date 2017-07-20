using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class PurchaseOrderMasterDal : SUL.Dal.Base.PurchaseOrderMasterDalBase
	{
		public PurchaseOrderMasterDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public int GetMaxPurchaseMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("PurchaseOrderMaster", "Id", 1, "", lstData);
        }

        public DataTable GetPurchaseOrderListFromViewList(Hashtable lstData)
        {

            string whereCondition = " ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewPurchaseMaster", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public int GetlastSupplierCode(Hashtable lstData)
        {

            try
            {
                string whereCondition = " where VendorId = @VendorId";
                int maxId = GetMaximumIDbyCondition("PurchaseOrderMaster", "cast(Id as int)", 0, whereCondition, lstData);

                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllPurchaseOrderMasterbySupplierId(Hashtable lstData)
        {
            string whereCondition = " where PurchaseOrderMaster.VendorId = @VendorId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("PurchaseOrderMaster", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetPurchaseOrderMasterByVandorId(Hashtable lstData)
        {
            string whereCondition = " where VendorId =@VendorId and Id not in (select OrderId from PIMaster) ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("PurchaseOrderMaster", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetOrderNoForPI(Hashtable lstData)
        {
            string whereCondition = " where VendorId =@VendorId and Id in (select OrderId from PIMaster) ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("PurchaseOrderMaster", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetPurchaseOrderByVandorId(Hashtable lstData)
        {
            string whereCondition = " where VendorId =@VendorId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("PurchaseOrderMaster", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
