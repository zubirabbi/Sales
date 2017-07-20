using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class UsersDalBase : SqlServerConnection
	{
		public DataTable GetAllUsers(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Users", "*", " Where Users.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetUsersById(Hashtable lstData)
		{
			string whereCondition = " where Users.Id = @Id And Users.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Users", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertUsers(Hashtable lstData)
		{
			string sqlQuery ="Insert into Users (EmployeeId, UserName, UserPass, CompanyId, IsActive) values(@EmployeeId, @UserName, @UserPass, @CompanyId, @IsActive);";
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

		public int UpdateUsers(Hashtable lstData)
		{
			string sqlQuery = "Update Users set EmployeeId = @EmployeeId, UserName = @UserName, UserPass = @UserPass, CompanyId = @CompanyId, IsActive = @IsActive where Users.Id = @Id;";
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

		public int DeleteUsersById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Users where Id = @Id;";
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
