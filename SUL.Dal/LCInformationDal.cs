using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class LCInformationDal : SUL.Dal.Base.LCInformationDalBase
	{
		public LCInformationDal() : base()
		{
		}
        public DataTable GetAllLCInformationbySupplierId(Hashtable lstData)
        {
            string whereCondition = " where LCInformation.VendorId = @VendorId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("LCInformation", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetLCByPIId(Hashtable lstData)
        {
            string whereCondition = " where PINo =@PINo ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("LCInformation", "*", whereCondition, lstData);
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
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int IsLCNoExist(Hashtable lstData, bool isNewEntry)
        {
            string whereCondition = (isNewEntry) ? " where LCNumber = @LCNumber " : "  where LCNumber = @LCNumber and Id<>@Id";
            try
            {
                int count = CheckExistence("LCInformation", "Id", whereCondition, lstData);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetLCFromViewList(Hashtable lstData)
        {

            string whereCondition = " ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewLcInfo", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int GetMaxLCMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("LCInformation", "Id", 1, "", lstData);
        }
	}
}
