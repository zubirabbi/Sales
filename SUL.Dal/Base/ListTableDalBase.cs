using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ListTableDalBase : SqlServerConnection
	{
		public DataTable GetAllListTable(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ListTable", "*", " Where ListTable.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetListTableById(Hashtable lstData)
		{
			string whereCondition = " where ListTable.Id = @Id And ListTable.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ListTable", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertListTable(Hashtable lstData)
		{
			string sqlQuery ="Insert into ListTable (ListType, ListId, ListValue, CompanyId, IsActive) values(@ListType, @ListId, @ListValue, @CompanyId, @IsActive);";
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

		public int UpdateListTable(Hashtable lstData)
		{
			string sqlQuery = "Update ListTable set ListType = @ListType, ListId = @ListId, ListValue = @ListValue, CompanyId = @CompanyId, IsActive = @IsActive where ListTable.Id = @Id;";
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

		public int DeleteListTableById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ListTable where Id = @Id;";
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
