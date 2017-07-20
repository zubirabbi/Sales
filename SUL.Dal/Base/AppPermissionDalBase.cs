using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class AppPermissionDalBase : SqlServerConnection
	{
		public DataTable GetAllAppPermission(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("AppPermission", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetAppPermissionById(Hashtable lstData)
		{
			string whereCondition = " where AppPermission.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("AppPermission", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertAppPermission(Hashtable lstData)
		{
			string sqlQuery ="Insert into AppPermission (RoleId, UserId, CompanyId, FunctionalityId, IsView, IsInsert, IsUpdate, IsDelete, IsApprove) values(@RoleId, @UserId, @CompanyId, @FunctionalityId, @IsView, @IsInsert, @IsUpdate, @IsDelete, @IsApprove);";
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

		public int UpdateAppPermission(Hashtable lstData)
		{
			string sqlQuery = "Update AppPermission set RoleId = @RoleId, UserId = @UserId, CompanyId = @CompanyId, FunctionalityId = @FunctionalityId, IsView = @IsView, IsInsert = @IsInsert, IsUpdate = @IsUpdate, IsDelete = @IsDelete, IsApprove = @IsApprove where AppPermission.Id = @Id;";
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

		public int DeleteAppPermissionById(Hashtable lstData)
		{
			string sqlQuery = "delete from  AppPermission where Id = @Id;";
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
