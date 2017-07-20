using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class UserRoleMappingBase
	{
		protected static SUL.Dal.UserRoleMappingDal dal = new SUL.Dal.UserRoleMappingDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }

		public System.Int32 RoleId		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }


		public  Int32 InsertUserRoleMapping()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@RoleId", RoleId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertUserRoleMapping(lstItems);
		}

		public  Int32 UpdateUserRoleMapping()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@UserId", UserId.ToString());
			lstItems.Add("@RoleId", RoleId.ToString());
			lstItems.Add("@CompanyId", CompanyId.ToString());

			return dal.UpdateUserRoleMapping(lstItems);
		}

		public  Int32 DeleteUserRoleMappingById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteUserRoleMappingById(lstItems);
		}

		public List<UserRoleMapping> GetAllUserRoleMapping(int CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
			DataTable dt = dal.GetAllUserRoleMapping(lstItems);
			List<UserRoleMapping> UserRoleMappingList = new List<UserRoleMapping>();
			foreach (DataRow dr in dt.Rows)
			{
				UserRoleMappingList.Add(GetObject(dr));
			}
			return UserRoleMappingList;
		}

		public UserRoleMapping GetUserRoleMappingById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetUserRoleMappingById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  UserRoleMapping GetObject(DataRow dr)
		{

			UserRoleMapping objUserRoleMapping = new UserRoleMapping();
			objUserRoleMapping.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objUserRoleMapping.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
			objUserRoleMapping.RoleId = (dr["RoleId"] == DBNull.Value) ? 0 : (Int32)dr["RoleId"];
			objUserRoleMapping.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];

			return objUserRoleMapping;
		}
	}
}
