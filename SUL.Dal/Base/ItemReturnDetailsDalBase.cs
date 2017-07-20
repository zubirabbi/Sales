using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ItemReturnDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllItemReturnDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemReturnDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetItemReturnDetailsById(Hashtable lstData)
		{
			string whereCondition = " where ItemReturnDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemReturnDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertItemReturnDetails(Hashtable lstData)
		{
            string sqlQuery = "Insert into ItemReturnDetails (MasterId, ProductId, ReturnQuantity, ReturnRate, LineTotal, ColorId, UnitId) values(@MasterId, @ProductId, @ReturnQuantity, @ReturnRate, @LineTotal, @ColorId, @UnitId);";
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

		public int UpdateItemReturnDetails(Hashtable lstData)
		{
            string sqlQuery = "Update ItemReturnDetails set MasterId = @MasterId, ProductId = @ProductId, ReturnQuantity = @ReturnQuantity, ReturnRate = @ReturnRate, LineTotal = @LineTotal, ColorId = @ColorId, UnitId = @UnitId where ItemReturnDetails.Id = @Id;";
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

		public int DeleteItemReturnDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ItemReturnDetails where Id = @Id;";
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
