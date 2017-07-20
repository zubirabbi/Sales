using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class JournalDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllJournalDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("JournalDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetJournalDetailsById(Hashtable lstData)
		{
			string whereCondition = " where JournalDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("JournalDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertJournalDetails(Hashtable lstData)
		{
			string sqlQuery ="Insert into JournalDetails (MasterId, DealerId, ContraAccount, Debit, Credit, Description) values(@MasterId, @DealerId, @ContraAccount, @Debit, @Credit, @Description);";
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

		public int UpdateJournalDetails(Hashtable lstData)
		{
			string sqlQuery = "Update JournalDetails set MasterId = @MasterId, DealerId = @DealerId, ContraAccount = @ContraAccount, Debit = @Debit, Credit = @Credit, Description = @Description where JournalDetails.Id = @Id;";
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

		public int DeleteJournalDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  JournalDetails where Id = @Id;";
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
