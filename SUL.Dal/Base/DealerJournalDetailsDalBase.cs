using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class DealerJournalDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllDealerJournalDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DealerJournalDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetDealerJournalDetailsById(Hashtable lstData)
		{
			string whereCondition = " where DealerJournalDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DealerJournalDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertDealerJournalDetails(Hashtable lstData)
		{
			string sqlQuery ="Insert into DealerJournalDetails (MasterId, DealerId, CurrentBalance, Debit, Credit, Description) values(@MasterId, @DealerId, @CurrentBalance, @Debit, @Credit, @Description);";
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

		public int UpdateDealerJournalDetails(Hashtable lstData)
		{
			string sqlQuery = "Update DealerJournalDetails set MasterId = @MasterId, DealerId = @DealerId, CurrentBalance = @CurrentBalance, Debit = @Debit, Credit = @Credit, Description = @Description where DealerJournalDetails.Id = @Id;";
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

		public int DeleteDealerJournalDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  DealerJournalDetails where Id = @Id;";
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
