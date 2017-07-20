using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ItemJournalDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllItemJournalDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemJournalDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetItemJournalDetailsById(Hashtable lstData)
		{
			string whereCondition = " where ItemJournalDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemJournalDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertItemJournalDetails(Hashtable lstData)
		{
			string sqlQuery ="Insert into ItemJournalDetails (MasterId, ProductId, ProductName, Color, Unit, OpeningBalance, QuantityIn, QuantityOut, ClosingBalance, Rate) values(@MasterId, @ProductId, @ProductName, @Color, @Unit, @OpeningBalance, @QuantityIn, @QuantityOut, @ClosingBalance, @Rate);";
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

		public int UpdateItemJournalDetails(Hashtable lstData)
		{
			string sqlQuery = "Update ItemJournalDetails set MasterId = @MasterId, ProductId = @ProductId, ProductName = @ProductName, Color = @Color, Unit = @Unit, OpeningBalance = @OpeningBalance, QuantityIn = @QuantityIn, QuantityOut = @QuantityOut, ClosingBalance = @ClosingBalance, Rate = @Rate where ItemJournalDetails.Id = @Id;";
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

		public int DeleteItemJournalDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ItemJournalDetails where Id = @Id;";
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
