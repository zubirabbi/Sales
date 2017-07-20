using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class WarehouseCategoryDalBase : SqlServerConnection
	{
		public DataTable GetAllWarehouseCategory(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("WarehouseCategory", "*", " Where WarehouseCategory.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetWarehouseCategoryById(Hashtable lstData)
		{
			string whereCondition = " where WarehouseCategory.Id = @Id And WarehouseCategory.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("WarehouseCategory", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertWarehouseCategory(Hashtable lstData)
		{
			string sqlQuery ="Insert into WarehouseCategory (CategoryName, IsVirtual, IsActive) values(@CategoryName, @IsVirtual, @IsActive);";
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

		public int UpdateWarehouseCategory(Hashtable lstData)
		{
			string sqlQuery = "Update WarehouseCategory set CategoryName = @CategoryName, IsVirtual = @IsVirtual, IsActive = @IsActive where WarehouseCategory.Id = @Id;";
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

		public int DeleteWarehouseCategoryById(Hashtable lstData)
		{
			string sqlQuery = "delete from  WarehouseCategory where Id = @Id;";
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
