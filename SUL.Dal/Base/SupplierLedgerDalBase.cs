using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class SupplierLedgerDalBase : SqlServerConnection
	{
		public DataTable GetAllSupplierLedger(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SupplierLedger", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetSupplierLedgerById(Hashtable lstData)
		{
			string whereCondition = " where SupplierLedger.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SupplierLedger", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertSupplierLedger(Hashtable lstData)
		{
			string sqlQuery ="Insert into SupplierLedger (SupplierId, TransactionType, TransactionDate, SourceId, UserId,  OpeningBalance, Debit, Cradit) values(@SupplierId, @TransactionType, @TransactionDate, @SourceId, @UserId, @OpeningBalance, @Debit, @Cradit);";
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

		public int UpdateSupplierLedger(Hashtable lstData)
		{
			string sqlQuery = "Update SupplierLedger set SupplierId = @SupplierId, TransactionType = @TransactionType, TransactionDate = @TransactionDate, SourceId = @SourceId, UserId = @UserId, OpeningBalance = @OpeningBalance, Debit = @Debit, Cradit = @Cradit where SupplierLedger.Id = @Id;";
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

		public int DeleteSupplierLedgerById(Hashtable lstData)
		{
			string sqlQuery = "delete from  SupplierLedger where Id = @Id;";
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
