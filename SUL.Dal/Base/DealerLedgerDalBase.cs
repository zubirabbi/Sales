using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class DealerLedgerDalBase : SqlServerConnection
	{
		public DataTable GetAllDealerLedger(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DealerLedger", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetDealerLedgerById(Hashtable lstData)
		{
			string whereCondition = " where DealerLedger.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DealerLedger", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertDealerLedger(Hashtable lstData)
		{
            string sqlQuery = "Insert into DealerLedger (DealerId, TransactionType, TransactionDate, SourceId, UserId, OpeningBalance, Debit, Cradit, SourceNo, Remarks) values(@DealerId, @TransactionType, @TransactionDate, @SourceId, @UserId,  @OpeningBalance, @Debit, @Cradit, @SourceNo, @Remarks);";
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

		public int UpdateDealerLedger(Hashtable lstData)
		{
            string sqlQuery = "Update DealerLedger set DealerId = @DealerId, TransactionType = @TransactionType, TransactionDate = @TransactionDate, SourceId = @SourceId, UserId = @UserId, OpeningBalance = @OpeningBalance, Debit = @Debit, Cradit = @Cradit, SourceNo = @SourceNo, Remarks = @Remarks where DealerLedger.Id = @Id;";
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

		public int DeleteDealerLedgerById(Hashtable lstData)
		{
			string sqlQuery = "delete from  DealerLedger where Id = @Id;";
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
