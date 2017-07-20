using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class CompanyDal : SUL.Dal.Base.CompanyDalBase
	{
		public CompanyDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetParentCompany()
        {
            string whereCondition = " where isParentCompany = 1;";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("Company", "*", whereCondition, new Hashtable());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
	}
}
