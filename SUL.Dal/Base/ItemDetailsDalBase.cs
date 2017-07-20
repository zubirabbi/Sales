using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ItemDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllItemDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetItemDetailsById(Hashtable lstData)
		{
			string whereCondition = " where ItemDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertItemDetails(Hashtable lstData)
		{
			string sqlQuery ="Insert into ItemDetails (MasterId, ProductId, ProductName, Color, Unit, OpeningBalance, QuantityIn, QuantityOut, ClosingBalance, Rate, Value) values(@MasterId, @ProductId, @ProductName, @Color, @Unit, @OpeningBalance, @QuantityIn, @QuantityOut, @ClosingBalance, @Rate, @Value);";
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

		public int UpdateItemDetails(Hashtable lstData)
		{
			string sqlQuery = "Update ItemDetails set MasterId = @MasterId, ProductId = @ProductId, ProductName = @ProductName, Color = @Color, Unit = @Unit, OpeningBalance = @OpeningBalance, QuantityIn = @QuantityIn, QuantityOut = @QuantityOut, ClosingBalance = @ClosingBalance, Rate = @Rate, Value = @Value where ItemDetails.Id = @Id;";
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

		public int DeleteItemDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ItemDetails where Id = @Id;";
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
