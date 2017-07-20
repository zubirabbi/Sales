using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class DataFilterSetupDalBase : SqlServerConnection
	{
		public DataTable GetAllDataFilterSetup(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DataFilterSetup", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetDataFilterSetupById(Hashtable lstData)
		{
			string whereCondition = " where DataFilterSetup.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DataFilterSetup", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertDataFilterSetup(Hashtable lstData)
		{
			string sqlQuery ="Insert into DataFilterSetup (UserId, RoleId, DataItemTypeId, StatusId, IsAllowed, IsLoadInitially, CompanyId) values(@UserId, @RoleId, @DataItemTypeId, @StatusId, @IsAllowed, @IsLoadInitially, @CompanyId);";
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

		public int UpdateDataFilterSetup(Hashtable lstData)
		{
			string sqlQuery = "Update DataFilterSetup set UserId = @UserId, RoleId = @RoleId, DataItemTypeId = @DataItemTypeId, StatusId = @StatusId, IsAllowed = @IsAllowed, IsLoadInitially = @IsLoadInitially, CompanyId = @CompanyId where DataFilterSetup.Id = @Id;";
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

		public int DeleteDataFilterSetupById(Hashtable lstData)
		{
			string sqlQuery = "delete from  DataFilterSetup where Id = @Id;";
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
