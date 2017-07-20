using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class UserLoginLogDalBase : SqlServerConnection
	{
		public DataTable GetAllUserLoginLog(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("UserLoginLog", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetUserLoginLogById(Hashtable lstData)
		{
			string whereCondition = " where UserLoginLog.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("UserLoginLog", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertUserLoginLog(Hashtable lstData)
		{
			string sqlQuery ="Insert into UserLoginLog (UserId, Status, SessionId, LoginTime, LoginPCName, IpAddress, LogOutTime) values(@UserId, @Status, @SessionId, @LoginTime, @LoginPCName, @IpAddress, @LogOutTime);";
			try
			{
				int success = ExecuteNonQuery(sqlQuery, lstData);
				return success;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
			}
		}

		public int UpdateUserLoginLog(Hashtable lstData)
		{
			string sqlQuery = "Update UserLoginLog set UserId = @UserId, Status = @Status, SessionId = @SessionId, LoginTime = @LoginTime, LoginPCName = @LoginPCName, IpAddress = @IpAddress, LogOutTime = @LogOutTime where UserLoginLog.Id = @Id;";
			try
			{
				int success = ExecuteNonQuery(sqlQuery, lstData);
				return success;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
			}
		}

		public int DeleteUserLoginLogById(Hashtable lstData)
		{
			string sqlQuery = "delete from  UserLoginLog where Id = @Id;";
			try
			{
				int success = ExecuteNonQuery(sqlQuery, lstData);
				return success;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
			}
		}
	}
}
