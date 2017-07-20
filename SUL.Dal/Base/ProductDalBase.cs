using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ProductDalBase : SqlServerConnection
	{
		public DataTable GetAllProduct(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Product", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetProductById(Hashtable lstData)
		{
			string whereCondition = " where Product.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Product", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertProduct(Hashtable lstData)
		{
            string sqlQuery = "Insert into Product (ProductCode, ProductName, ModelNo, ProductCategory, BaseUnit, MRP, DP, RP, CostPrice, CurrentBalance, DP2) values(@ProductCode, @ProductName, @ModelNo, @ProductCategory, @BaseUnit, @MRP, @DP, @RP, @CostPrice, @CurrentBalance, @DP2);";
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

		public int UpdateProduct(Hashtable lstData)
		{
            string sqlQuery = "Update Product set ProductCode = @ProductCode, ProductName = @ProductName, ModelNo = @ModelNo, ProductCategory = @ProductCategory, BaseUnit = @BaseUnit, MRP = @MRP, DP = @DP, RP = @RP, CostPrice = @CostPrice, CurrentBalance = @CurrentBalance, DP2 = @DP2 where Product.Id = @Id;";
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

		public int DeleteProductById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Product where Id = @Id;";
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
