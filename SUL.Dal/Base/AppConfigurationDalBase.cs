using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class AppConfigurationDalBase : SqlServerConnection
	{
		public DataTable GetAllAppConfiguration(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("AppConfiguration", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetAppConfigurationById(Hashtable lstData)
		{
			string whereCondition = " where AppConfiguration.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("AppConfiguration", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertAppConfiguration(Hashtable lstData)
		{
			string sqlQuery ="Insert into AppConfiguration (Id, IsSingleCompany) values(@Id, @IsSingleCompany);";
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

		public int UpdateAppConfiguration(Hashtable lstData)
		{
			string sqlQuery = "Update AppConfiguration set IsSingleCompany = @IsSingleCompany where AppConfiguration.Id = @Id;";
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

		public int DeleteAppConfigurationById(Hashtable lstData)
		{
			string sqlQuery = "delete from  AppConfiguration where Id = @Id;";
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
