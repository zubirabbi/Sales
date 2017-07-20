using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class JournalMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllJournalMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("JournalMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetJournalMasterByJournalId(Hashtable lstData)
		{
			string whereCondition = " where JournalMaster.JournalId = @JournalId ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("JournalMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertJournalMaster(Hashtable lstData)
		{
			string sqlQuery ="Insert into JournalMaster (JournalCode, JournalTypeId, JDate, Description, UserId, Status) values(@JournalCode, @JournalTypeId, @JDate, @Description, @UserId, @Status);";
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

		public int UpdateJournalMaster(Hashtable lstData)
		{
			string sqlQuery = "Update JournalMaster set JournalCode = @JournalCode, JournalTypeId = @JournalTypeId, JDate = @JDate, Description = @Description, UserId = @UserId, Status = @Status where JournalMaster.JournalId = @JournalId;";
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

		public int DeleteJournalMasterByJournalId(Hashtable lstData)
		{
			string sqlQuery = "delete from  JournalMaster where JournalId = @JournalId;";
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
