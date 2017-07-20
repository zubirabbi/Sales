using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ItemLedgerDalBase : SqlServerConnection
	{
		public DataTable GetAllItemLedger(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemLedger", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetItemLedgerById(Hashtable lstData)
		{
			string whereCondition = " where ItemLedger.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemLedger", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertItemLedger(Hashtable lstData)
		{
			string sqlQuery ="Insert into ItemLedger (ItemId, Unit, Color, WareHouseId, TotalIn, TotalOut, Value) values(@ItemId, @Unit, @Color, @WareHouseId, @TotalIn, @TotalOut,  @Value);";
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

		public int UpdateItemLedger(Hashtable lstData)
		{
            string sqlQuery = "Update ItemLedger set ItemId = @ItemId, Unit = @Unit, Color = @Color, WareHouseId = @WareHouseId, TotalIn = TotalIn+@TotalIn, TotalOut = TotalOut+@TotalOut, Value = @Value where ItemLedger.Id = @Id;";
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

		public int DeleteItemLedgerById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ItemLedger where Id = @Id;";
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
