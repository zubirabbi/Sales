using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class AppConfiguration : SUL.Bll.Base.AppConfigurationBase
	{
		private static SUL.Dal.AppConfigurationDal Dal = new SUL.Dal.AppConfigurationDal();
		public AppConfiguration() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetSingleCompany()
        {
            Hashtable lstItems = new Hashtable();

            int SingleId = int.Parse(dal.GetSingleCompany(lstItems));
            return SingleId;
        }
	}
}
