using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ItemJournalMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllItemJournalMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemJournalMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetItemJournalMasterById(Hashtable lstData)
		{
			string whereCondition = " where ItemJournalMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemJournalMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertItemJournalMaster(Hashtable lstData)
		{
			string sqlQuery ="Insert into ItemJournalMaster (TransactionDate, TransactionType, SourceId, UserId, WareHouseId, WareHouseIdFrom) values(@TransactionDate, @TransactionType, @SourceId, @UserId, @WareHouseId, @WareHouseIdFrom);";
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

		public int UpdateItemJournalMaster(Hashtable lstData)
		{
			string sqlQuery = "Update ItemJournalMaster set TransactionDate = @TransactionDate, TransactionType = @TransactionType, SourceId = @SourceId, UserId = @UserId, WareHouseId = @WareHouseId, WareHouseIdFrom = @WareHouseIdFrom where ItemJournalMaster.Id = @Id;";
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

		public int DeleteItemJournalMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ItemJournalMaster where Id = @Id;";
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
