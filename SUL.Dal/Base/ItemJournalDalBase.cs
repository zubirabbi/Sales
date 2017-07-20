using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ItemJournalDalBase : SqlServerConnection
	{
		public DataTable GetAllItemJournal(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemJournal", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetItemJournalById(Hashtable lstData)
		{
			string whereCondition = " where ItemJournal.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemJournal", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertItemJournal(Hashtable lstData)
		{
			string sqlQuery ="Insert into ItemJournal (TransactionDate, TransactionType, SourceId, UserId,  WareHouseId) values(@TransactionDate, @TransactionType, @SourceId, @UserId,  @WareHouseId);";
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

		public int UpdateItemJournal(Hashtable lstData)
		{
			string sqlQuery = "Update ItemJournal set TransactionDate = @TransactionDate, TransactionType = @TransactionType, SourceId = @SourceId, UserId = @UserId,  WareHouseId = @WareHouseId where ItemJournal.Id = @Id;";
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

		public int DeleteItemJournalById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ItemJournal where Id = @Id;";
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
