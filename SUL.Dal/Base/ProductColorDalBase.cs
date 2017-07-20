using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ProductColorDalBase : SqlServerConnection
	{
		public DataTable GetAllProductColor(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ProductColor", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetProductColorById(Hashtable lstData)
		{
			string whereCondition = " where ProductColor.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ProductColor", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertProductColor(Hashtable lstData)
		{
			string sqlQuery ="Insert into ProductColor (ProductId, Color) values(@ProductId, @Color);";
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

		public int UpdateProductColor(Hashtable lstData)
		{
			string sqlQuery = "Update ProductColor set ProductId = @ProductId, Color = @Color where ProductColor.Id = @Id;";
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

		public int DeleteProductColorById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ProductColor where Id = @Id;";
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
