using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class PIMasterDal : SUL.Dal.Base.PIMasterDalBase
	{
		public PIMasterDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public int GetMaxPIMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("PIMaster", "Id", 1, "", lstData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetAllPIMasterbyVendorId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("PIMaster", "*", "Where VendorId = @VendorId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllPImasterbyPIno(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("PIMaster", "*", "Where PINo = @PINo", lstData);
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
        public DataTable GetPIByVandorId(Hashtable lstData)
        {
            string whereCondition = " where VendorId =@VendorId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("PIMaster", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetPIByPOId(Hashtable lstData)
        {
            string whereCondition = " where OrderId =@OrderId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("PIMaster", "*", whereCondition, lstData);
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
        public DataTable GetPIFromViewList(Hashtable lstData)
        {

            string whereCondition = " ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewPIMaster", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
     
	}
}
