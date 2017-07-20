using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class AppConfigurationDal : SUL.Dal.Base.AppConfigurationDalBase
	{
		public AppConfigurationDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public String GetSingleCompany(Hashtable lstData)
        {
            string whereCondition = " where IsSingleCompany = 1;";
         
            try
            {
                return ExecuteScaler("appconfiguration", "Id", whereCondition, lstData);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
