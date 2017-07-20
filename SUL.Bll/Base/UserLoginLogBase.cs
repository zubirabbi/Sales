using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class UserLoginLogBase
	{
		protected static SUL.Dal.UserLoginLogDal dal = new SUL.Dal.UserLoginLogDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }

		public System.String Status		{ get ; set; }

		public System.String SessionId		{ get ; set; }

		public System.DateTime LoginTime		{ get ; set; }

		public System.String LoginPCName		{ get ; set; }

		public System.String IpAddress		{ get ; set; }

		public System.DateTime LogOutTime		{ get ; set; }


		public  Int32 InsertUserLoginLog()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Status", Status);
			lstItems.Add("@SessionId", SessionId);
			lstItems.Add("@LoginTime", LoginTime.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@LoginPCName", LoginPCName);
			lstItems.Add("@IpAddress", IpAddress);
			lstItems.Add("@LogOutTime", LogOutTime);

			return dal.InsertUserLoginLog(lstItems);
		}

		public  Int32 UpdateUserLoginLog()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@UserId", UserId.ToString());
			lstItems.Add("@Status", Status);
			lstItems.Add("@SessionId", SessionId);
			lstItems.Add("@LoginTime", LoginTime.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@LoginPCName", LoginPCName);
			lstItems.Add("@IpAddress", IpAddress);
			lstItems.Add("@LogOutTime", LogOutTime);

			return dal.UpdateUserLoginLog(lstItems);
		}

		public  Int32 DeleteUserLoginLogById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteUserLoginLogById(lstItems);
		}

		public List<UserLoginLog> GetAllUserLoginLog()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllUserLoginLog(lstItems);
			List<UserLoginLog> UserLoginLogList = new List<UserLoginLog>();
			foreach (DataRow dr in dt.Rows)
			{
				UserLoginLogList.Add(GetObject(dr));
			}
			return UserLoginLogList;
		}

		public UserLoginLog  GetUserLoginLogById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetUserLoginLogById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  UserLoginLog GetObject(DataRow dr)
		{

			UserLoginLog objUserLoginLog = new UserLoginLog();
			objUserLoginLog.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objUserLoginLog.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
			objUserLoginLog.Status = (dr["Status"] == DBNull.Value) ? "" : (String)dr["Status"];
			objUserLoginLog.SessionId = (dr["SessionId"] == DBNull.Value) ? "" : (String)dr["SessionId"];
			objUserLoginLog.LoginTime = (dr["LoginTime"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["LoginTime"];
			objUserLoginLog.LoginPCName = (dr["LoginPCName"] == DBNull.Value) ? "" : (String)dr["LoginPCName"];
			objUserLoginLog.IpAddress = (dr["IpAddress"] == DBNull.Value) ? "" : (String)dr["IpAddress"];
			objUserLoginLog.LogOutTime = (dr["LogOutTime"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["LogOutTime"];

			return objUserLoginLog;
		}
	}
}
