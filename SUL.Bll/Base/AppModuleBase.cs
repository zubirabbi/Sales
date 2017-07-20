using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class AppModuleBase
	{
		protected static SUL.Dal.AppModuleDal dal = new SUL.Dal.AppModuleDal();

		public System.Int32 Id		{ get ; set; }

		public System.String Module		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }

		public System.Int32 AdminId		{ get ; set; }


		public  Int32 InsertAppModule()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Module", Module);
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@AdminId", AdminId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertAppModule(lstItems);
		}

		public  Int32 UpdateAppModule()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@Module", Module);
			lstItems.Add("@CompanyId", CompanyId.ToString());
			lstItems.Add("@AdminId", AdminId.ToString());

			return dal.UpdateAppModule(lstItems);
		}

		public  Int32 DeleteAppModuleById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteAppModuleById(lstItems);
		}

        public List<AppModule> GetAllAppModule(int CompanyId, int _userId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
            lstItems.Add("@UserId", _userId);

			DataTable dt = dal.GetAllAppModule(lstItems);
			List<AppModule> AppModuleList = new List<AppModule>();
			foreach (DataRow dr in dt.Rows)
			{
				AppModuleList.Add(GetObject(dr));
			}
			return AppModuleList;
		}

		public AppModule GetAppModuleById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetAppModuleById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  AppModule GetObject(DataRow dr)
		{

			AppModule objAppModule = new AppModule();
			objAppModule.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objAppModule.Module = (dr["Module"] == DBNull.Value) ? "" : (String)dr["Module"];
			objAppModule.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];
			objAppModule.AdminId = (dr["AdminId"] == DBNull.Value) ? 0 : (Int32)dr["AdminId"];

			return objAppModule;
		}
	}
}
