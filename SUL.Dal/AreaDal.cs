using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class AreaDal : SUL.Dal.Base.AreaDalBase
	{
		public AreaDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetAllViewArea(Hashtable lstData)
        {
            string whereCondition = "";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewArea", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllViewAreaByRegionId(Hashtable lstData)
        {
            string whereCondition = "where RegionId = @RegionId";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewArea", "*", whereCondition, lstData);
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
        public int GetlastAreaCode(Hashtable lstData)
        {

            try
            {
                string whereCondition = " where RegionId = @RegionId";
                int maxId = GetMaximumIDbyCondition("Area", "cast(right(AreaCode,2) as int)", 0, whereCondition,lstData);

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
        /// <param name="isNewentry"></param>
        /// <returns></returns>
        public int CheckForCodeExist(Hashtable lstData, bool isNewentry)
        {
            string whereCondition = string.Empty;
            whereCondition = isNewentry ? " where AreaCode = @AreaCode " : " where AreaCode = @AreaCode and Id != @Id";

            try
            {
                return CheckExistence("Area", "Id", whereCondition, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
