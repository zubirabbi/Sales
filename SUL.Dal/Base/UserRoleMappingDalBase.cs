using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class UserRoleMappingDalBase : SqlServerConnection
	{
		public DataTable GetAllUserRoleMapping(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("UserRoleMapping", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetUserRoleMappingById(Hashtable lstData)
		{
			string whereCondition = " where UserRoleMapping.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("UserRoleMapping", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertUserRoleMapping(Hashtable lstData)
		{
			string sqlQuery ="Insert into UserRoleMapping (UserId, RoleId, CompanyId) values(@UserId, @RoleId, @CompanyId);";
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

		public int UpdateUserRoleMapping(Hashtable lstData)
		{
			string sqlQuery = "Update UserRoleMapping set UserId = @UserId, RoleId = @RoleId, CompanyId = @CompanyId where UserRoleMapping.Id = @Id;";
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

		public int DeleteUserRoleMappingById(Hashtable lstData)
		{
			string sqlQuery = "delete from  UserRoleMapping where Id = @Id;";
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
