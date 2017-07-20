using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class UserRoleDalBase : SqlServerConnection
	{
		public DataTable GetAllUserRole(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("UserRole", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetUserRoleById(Hashtable lstData)
		{
			string whereCondition = " where UserRole.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("UserRole", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertUserRole(Hashtable lstData)
		{
			string sqlQuery ="Insert into UserRole (Role, Description, CompanyId) values(@Role, @Description, @CompanyId);";
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

		public int UpdateUserRole(Hashtable lstData)
		{
			string sqlQuery = "Update UserRole set Role = @Role, Description = @Description, CompanyId = @CompanyId where UserRole.Id = @Id;";
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

		public int DeleteUserRoleById(Hashtable lstData)
		{
			string sqlQuery = "delete from  UserRole where Id = @Id;";
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
