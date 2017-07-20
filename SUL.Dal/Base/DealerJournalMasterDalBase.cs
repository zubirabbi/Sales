using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class DealerJournalMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllDealerJournalMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DealerJournalMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetDealerJournalMasterById(Hashtable lstData)
		{
			string whereCondition = " where DealerJournalMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DealerJournalMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertDealerJournalMaster(Hashtable lstData)
		{
            string sqlQuery = "Insert into DealerJournalMaster (JournalId, Date, Type, Description, ContraAccount, CreatedBy, CreatedDate, ApproveBy, ApproveDate, UpdateBy, UpdateDate,Status) values(@JournalId, @Date, @Type, @Description, @ContraAccount, @CreatedBy, @CreatedDate, @ApproveBy, @ApproveDate, @UpdateBy, @UpdateDate,@Status);";
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

		public int UpdateDealerJournalMaster(Hashtable lstData)
		{
            string sqlQuery = "Update DealerJournalMaster set JournalId = @JournalId, Date = @Date, Type = @Type,  Description = @Description, ContraAccount = @ContraAccount, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, ApproveBy = @ApproveBy, ApproveDate = @ApproveDate, UpdateBy = @UpdateBy, UpdateDate = @UpdateDate, Status = @Status where DealerJournalMaster.Id = @Id;";
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

		public int DeleteDealerJournalMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  DealerJournalMaster where Id = @Id;";
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
