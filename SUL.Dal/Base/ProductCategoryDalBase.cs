using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ProductCategoryDalBase : SqlServerConnection
	{
		public DataTable GetAllProductCategory(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ProductCategory", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetProductCategoryById(Hashtable lstData)
		{
			string whereCondition = " where ProductCategory.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ProductCategory", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertProductCategory(Hashtable lstData)
		{
			string sqlQuery ="Insert into ProductCategory (CategoryCode, CategoryDescription) values(@CategoryCode, @CategoryDescription);";
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

		public int UpdateProductCategory(Hashtable lstData)
		{
			string sqlQuery = "Update ProductCategory set CategoryCode = @CategoryCode, CategoryDescription = @CategoryDescription where ProductCategory.Id = @Id;";
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

		public int DeleteProductCategoryById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ProductCategory where Id = @Id;";
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
