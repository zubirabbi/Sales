using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class RegionDal : SUL.Dal.Base.RegionDalBase
	{
		public RegionDal() : base()
		{
		}
        public DataTable GetAllViewRegion(Hashtable lstData)
        {
            string whereCondition = "";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewRegion", "*", whereCondition, lstData);
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
        /// <returns></returns>
        public int GetlastRegionCode()
        {

            try
            {
                int maxId = GetMaximumID("Region", "cast(right(RegionCode,2) as int)", 0, "");

                return maxId;
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
        public DataTable GetAllRegionFromView(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewRegion", "*", " Where IsActive = 1;", lstData);
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
        /// <param name="isNewentry"></param>
        /// <returns></returns>
        public int CheckForCodeExist(Hashtable lstData, bool isNewentry)
        {
            string whereCondition = string.Empty;
            whereCondition = isNewentry ? " where RegionCode = @RegionCode " : " where RegionCode = @RegionCode and Id != @Id";

            try
            {
                return CheckExistence("Region", "Id", whereCondition, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
