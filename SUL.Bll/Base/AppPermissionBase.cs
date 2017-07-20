using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class AppPermissionBase
	{
		protected static SUL.Dal.AppPermissionDal dal = new SUL.Dal.AppPermissionDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 RoleId		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }

		public System.Int32 FunctionalityId		{ get ; set; }

		public System.Boolean IsView		{ get ; set; }

		public System.Boolean IsInsert		{ get ; set; }

		public System.Boolean IsUpdate		{ get ; set; }

		public System.Boolean IsDelete		{ get ; set; }

		public System.Boolean IsApprove		{ get ; set; }


		public  Int32 InsertAppPermission()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@RoleId", RoleId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@FunctionalityId", FunctionalityId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsView", IsView);
			lstItems.Add("@IsInsert", IsInsert);
			lstItems.Add("@IsUpdate", IsUpdate);
			lstItems.Add("@IsDelete", IsDelete);
			lstItems.Add("@IsApprove", IsApprove);

			return dal.InsertAppPermission(lstItems);
		}

		public  Int32 UpdateAppPermission()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@RoleId", RoleId.ToString());
			lstItems.Add("@UserId", UserId.ToString());
			lstItems.Add("@CompanyId", CompanyId.ToString());
			lstItems.Add("@FunctionalityId", FunctionalityId.ToString());
			lstItems.Add("@IsView", IsView);
			lstItems.Add("@IsInsert", IsInsert);
			lstItems.Add("@IsUpdate", IsUpdate);
			lstItems.Add("@IsDelete", IsDelete);
			lstItems.Add("@IsApprove", IsApprove);

			return dal.UpdateAppPermission(lstItems);
		}

		public  Int32 DeleteAppPermissionById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteAppPermissionById(lstItems);
		}

		public List<AppPermission> GetAllAppPermission(int CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
			DataTable dt = dal.GetAllAppPermission(lstItems);
			List<AppPermission> AppPermissionList = new List<AppPermission>();
			foreach (DataRow dr in dt.Rows)
			{
				AppPermissionList.Add(GetObject(dr));
			}
			return AppPermissionList;
		}

		public AppPermission GetAppPermissionById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetAppPermissionById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  AppPermission GetObject(DataRow dr)
		{

			AppPermission objAppPermission = new AppPermission();
			objAppPermission.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objAppPermission.RoleId = (dr["RoleId"] == DBNull.Value) ? 0 : (Int32)dr["RoleId"];
			objAppPermission.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
			objAppPermission.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];
			objAppPermission.FunctionalityId = (dr["FunctionalityId"] == DBNull.Value) ? 0 : (Int32)dr["FunctionalityId"];
			objAppPermission.IsView = (dr["IsView"] == DBNull.Value) ? false : (Boolean)dr["IsView"];
			objAppPermission.IsInsert = (dr["IsInsert"] == DBNull.Value) ? false : (Boolean)dr["IsInsert"];
			objAppPermission.IsUpdate = (dr["IsUpdate"] == DBNull.Value) ? false : (Boolean)dr["IsUpdate"];
			objAppPermission.IsDelete = (dr["IsDelete"] == DBNull.Value) ? false : (Boolean)dr["IsDelete"];
			objAppPermission.IsApprove = (dr["IsApprove"] == DBNull.Value) ? false : (Boolean)dr["IsApprove"];

			return objAppPermission;
		}
	}
}
