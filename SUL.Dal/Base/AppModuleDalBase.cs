using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class AppModuleDalBase : SqlServerConnection
	{
		public DataTable GetAllAppModule(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("AppModule", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetAppModuleById(Hashtable lstData)
		{
			string whereCondition = " where AppModule.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("AppModule", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertAppModule(Hashtable lstData)
		{
			string sqlQuery ="Insert into AppModule (Module, CompanyId, AdminId) values(@Module, @CompanyId, @AdminId);";
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

		public int UpdateAppModule(Hashtable lstData)
		{
			string sqlQuery = "Update AppModule set Module = @Module, CompanyId = @CompanyId, AdminId = @AdminId where AppModule.Id = @Id;";
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

		public int DeleteAppModuleById(Hashtable lstData)
		{
			string sqlQuery = "delete from  AppModule where Id = @Id;";
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
