using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class WareHouseDal : SUL.Dal.Base.WareHouseDalBase
	{
		public WareHouseDal() : base()
		{
		}
        public DataTable GetAllWareHousebyCompanyId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewWareHouse", "*", " Where CompanyId = @CompanyId And IsActive = 1;", lstData);
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
        public int GetlastwareHouseCode(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumID("WareHouse", "Id", 0, "");
                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
