using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class UserRoleBase
	{
		protected static SUL.Dal.UserRoleDal dal = new SUL.Dal.UserRoleDal();

		public System.Int32 Id		{ get ; set; }

		public System.String Role		{ get ; set; }

		public System.String Description		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }


		public  Int32 InsertUserRole()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Role", Role);
			lstItems.Add("@Description", Description);
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertUserRole(lstItems);
		}

		public  Int32 UpdateUserRole()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@Role", Role);
			lstItems.Add("@Description", Description);
			lstItems.Add("@CompanyId", CompanyId.ToString());

			return dal.UpdateUserRole(lstItems);
		}

		public  Int32 DeleteUserRoleById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteUserRoleById(lstItems);
		}

		public List<UserRole> GetAllUserRole(int CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
			DataTable dt = dal.GetAllUserRole(lstItems);
			List<UserRole> UserRoleList = new List<UserRole>();
			foreach (DataRow dr in dt.Rows)
			{
				UserRoleList.Add(GetObject(dr));
			}
			return UserRoleList;
		}

		public UserRole GetUserRoleById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetUserRoleById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  UserRole GetObject(DataRow dr)
		{

			UserRole objUserRole = new UserRole();
			objUserRole.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objUserRole.Role = (dr["Role"] == DBNull.Value) ? "" : (String)dr["Role"];
			objUserRole.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];
			objUserRole.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];

			return objUserRole;
		}
	}
}
