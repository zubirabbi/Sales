using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class AppFunctionalityBase
	{
		protected static SUL.Dal.AppFunctionalityDal dal = new SUL.Dal.AppFunctionalityDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }

		public System.Int32 ModuleId		{ get ; set; }

		public System.String Functionality		{ get ; set; }

		public System.String Url		{ get ; set; }

		public System.Int32 ParentId		{ get ; set; }

		public System.Boolean IsCompanySpecific		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }

		public System.Int32 Sequence		{ get ; set; }

		public System.Byte[] LastUpdated		{ get ; set; }

		public System.String FunctionalityArabic		{ get ; set; }


		public  Int32 InsertAppFunctionality()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ModuleId", ModuleId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Functionality", Functionality);
			lstItems.Add("@Url", Url);
			lstItems.Add("@ParentId", ParentId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsCompanySpecific", IsCompanySpecific);
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@Sequence", Sequence.ToString(CultureInfo.InvariantCulture));
			//lstItems.Add("@LastUpdated", LastUpdated.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@FunctionalityArabic", FunctionalityArabic);

			return dal.InsertAppFunctionality(lstItems);
		}

		public  Int32 UpdateAppFunctionality()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@CompanyId", CompanyId.ToString());
			lstItems.Add("@ModuleId", ModuleId.ToString());
			lstItems.Add("@Functionality", Functionality);
			lstItems.Add("@Url", Url);
			lstItems.Add("@ParentId", ParentId.ToString());
			lstItems.Add("@IsCompanySpecific", IsCompanySpecific);
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@Sequence", Sequence.ToString());
			//lstItems.Add("@LastUpdated", LastUpdated.ToString());
			lstItems.Add("@FunctionalityArabic", FunctionalityArabic);

			return dal.UpdateAppFunctionality(lstItems);
		}

		public  Int32 DeleteAppFunctionalityById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteAppFunctionalityById(lstItems);
		}

		public List<AppFunctionality> GetAllAppFunctionality(int IsCompanySpecific)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@IsCompanySpecific", IsCompanySpecific);
			DataTable dt = dal.GetAllAppFunctionality(lstItems);
			List<AppFunctionality> AppFunctionalityList = new List<AppFunctionality>();
			foreach (DataRow dr in dt.Rows)
			{
				AppFunctionalityList.Add(GetObject(dr));
			}
			return AppFunctionalityList;
		}

		public AppFunctionality GetAppFunctionalityById(int _Id,int _IsCompanySpecific)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@IsCompanySpecific", _IsCompanySpecific);

			DataTable dt = dal.GetAppFunctionalityById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  AppFunctionality GetObject(DataRow dr)
		{

			AppFunctionality objAppFunctionality = new AppFunctionality();
			objAppFunctionality.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objAppFunctionality.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];
			objAppFunctionality.ModuleId = (dr["ModuleId"] == DBNull.Value) ? 0 : (Int32)dr["ModuleId"];
			objAppFunctionality.Functionality = (dr["Functionality"] == DBNull.Value) ? "" : (String)dr["Functionality"];
			objAppFunctionality.Url = (dr["Url"] == DBNull.Value) ? "" : (String)dr["Url"];
			objAppFunctionality.ParentId = (dr["ParentId"] == DBNull.Value) ? 0 : (Int32)dr["ParentId"];
			objAppFunctionality.IsCompanySpecific = (dr["IsCompanySpecific"] == DBNull.Value) ? false : (Boolean)dr["IsCompanySpecific"];
			objAppFunctionality.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];
			objAppFunctionality.Sequence = (dr["Sequence"] == DBNull.Value) ? 0 : (Int32)dr["Sequence"];
			//objAppFunctionality.LastUpdated = (dr["LastUpdated"] == DBNull.Value) ? 0 : (Byte[])dr["LastUpdated"];
			objAppFunctionality.FunctionalityArabic = (dr["FunctionalityArabic"] == DBNull.Value) ? "" : (String)dr["FunctionalityArabic"];

			return objAppFunctionality;
		}
	}
}
