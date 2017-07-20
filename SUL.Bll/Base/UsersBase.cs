using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class UsersBase
	{
		protected static SUL.Dal.UsersDal dal = new SUL.Dal.UsersDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 EmployeeId		{ get ; set; }

		public System.String UserName		{ get ; set; }

		public System.String UserPass		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }


		public  Int32 InsertUsers()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@EmployeeId", EmployeeId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UserName", UserName);
			lstItems.Add("@UserPass", UserPass);
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);

			return dal.InsertUsers(lstItems);
		}

		public  Int32 UpdateUsers()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@EmployeeId", EmployeeId.ToString());
			lstItems.Add("@UserName", UserName);
			lstItems.Add("@UserPass", UserPass);
			lstItems.Add("@CompanyId", CompanyId.ToString());
			lstItems.Add("@IsActive", IsActive);

			return dal.UpdateUsers(lstItems);
		}

		public  Int32 DeleteUsersById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
            lstItems.Add("@userId", Id);

			return dal.DeleteUsersById(lstItems);
		}

		public List<Users> GetAllUsers(int CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
			DataTable dt = dal.GetAllUsers(lstItems);
			List<Users> UsersList = new List<Users>();
			foreach (DataRow dr in dt.Rows)
			{
				UsersList.Add(GetObject(dr));
			}
			return UsersList;
		}

		public Users GetUsersById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetUsersById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  Users GetObject(DataRow dr)
		{

			Users objUsers = new Users();
			objUsers.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objUsers.EmployeeId = (dr["EmployeeId"] == DBNull.Value) ? 0 : (Int32)dr["EmployeeId"];
			objUsers.UserName = (dr["UserName"] == DBNull.Value) ? "" : (String)dr["UserName"];
			objUsers.UserPass = (dr["UserPass"] == DBNull.Value) ? "" : (String)dr["UserPass"];
			objUsers.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];
			objUsers.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];

			return objUsers;
		}
	}
}
