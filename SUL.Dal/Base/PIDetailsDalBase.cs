using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class PIDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllPIDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("PIDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetPIDetailsById(Hashtable lstData)
		{
			string whereCondition = " where PIDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("PIDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertPIDetails(Hashtable lstData)
		{
			string sqlQuery ="Insert into PIDetails (MasterId, OrderDetailsId, ProductId, ProductName, OrderQuantity, PIQuantity, ProductUnit, UnitName, LineTotal, PICategory, PIUnitePrice) values(@MasterId, @OrderDetailsId, @ProductId, @ProductName, @OrderQuantity, @PIQuantity, @ProductUnit, @UnitName, @LineTotal,  @PICategory, @PIUnitePrice);";
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

		public int UpdatePIDetails(Hashtable lstData)
		{
			string sqlQuery = "Update PIDetails set MasterId = @MasterId, OrderDetailsId = @OrderDetailsId, ProductId = @ProductId, ProductName = @ProductName, OrderQuantity = @OrderQuantity, PIQuantity = @PIQuantity, ProductUnit = @ProductUnit, UnitName = @UnitName, LineTotal = @LineTotal,  PICategory = @PICategory, PIUnitePrice = @PIUnitePrice where PIDetails.Id = @Id;";
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

		public int DeletePIDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  PIDetails where Id = @Id;";
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
