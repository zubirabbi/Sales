using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class AppConfigurationBase
	{
		protected static SUL.Dal.AppConfigurationDal dal = new SUL.Dal.AppConfigurationDal();

		public System.Int32 Id		{ get ; set; }

		public System.Boolean IsSingleCompany		{ get ; set; }


		public  Int32 InsertAppConfiguration()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsSingleCompany", IsSingleCompany);

			return dal.InsertAppConfiguration(lstItems);
		}

		public  Int32 UpdateAppConfiguration()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@IsSingleCompany", IsSingleCompany);

			return dal.UpdateAppConfiguration(lstItems);
		}

		public  Int32 DeleteAppConfigurationById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteAppConfigurationById(lstItems);
		}

		public List<AppConfiguration> GetAllAppConfiguration(int IsSingleCompany)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@IsSingleCompany", IsSingleCompany);
			DataTable dt = dal.GetAllAppConfiguration(lstItems);
			List<AppConfiguration> AppConfigurationList = new List<AppConfiguration>();
			foreach (DataRow dr in dt.Rows)
			{
				AppConfigurationList.Add(GetObject(dr));
			}
			return AppConfigurationList;
		}

		public AppConfiguration GetAppConfigurationById(int _Id,int _IsSingleCompany)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@IsSingleCompany", _IsSingleCompany);

			DataTable dt = dal.GetAppConfigurationById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  AppConfiguration GetObject(DataRow dr)
		{

			AppConfiguration objAppConfiguration = new AppConfiguration();
			objAppConfiguration.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objAppConfiguration.IsSingleCompany = (dr["IsSingleCompany"] == DBNull.Value) ? false : (Boolean)dr["IsSingleCompany"];

			return objAppConfiguration;
		}
	}
}
