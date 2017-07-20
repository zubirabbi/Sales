using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ProductUnitDalBase : SqlServerConnection
	{
		public DataTable GetAllProductUnit(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ProductUnit", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetProductUnitById(Hashtable lstData)
		{
			string whereCondition = " where ProductUnit.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ProductUnit", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertProductUnit(Hashtable lstData)
		{
			string sqlQuery ="Insert into ProductUnit (UnitCode, UnitDescription) values(@UnitCode, @UnitDescription);";
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

		public int UpdateProductUnit(Hashtable lstData)
		{
			string sqlQuery = "Update ProductUnit set UnitCode = @UnitCode, UnitDescription = @UnitDescription where ProductUnit.Id = @Id;";
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

		public int DeleteProductUnitById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ProductUnit where Id = @Id;";
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
