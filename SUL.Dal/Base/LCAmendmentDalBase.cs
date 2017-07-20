using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class LCAmendmentDalBase : SqlServerConnection
	{
		public DataTable GetAllLCAmendment(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("LCAmendment", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetLCAmendmentById(Hashtable lstData)
		{
			string whereCondition = " where LCAmendment.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("LCAmendment", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertLCAmendment(Hashtable lstData)
		{
			string sqlQuery ="Insert into LCAmendment (AmendmentNumber, AmendementDate, AmendmentDescription, LCId) values(@AmendmentNumber, @AmendementDate, @AmendmentDescription, @LCId);";
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

		public int UpdateLCAmendment(Hashtable lstData)
		{
			string sqlQuery = "Update LCAmendment set AmendmentNumber = @AmendmentNumber, AmendementDate = @AmendementDate, AmendmentDescription = @AmendmentDescription, LCId = @LCId where LCAmendment.Id = @Id;";
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

		public int DeleteLCAmendmentById(Hashtable lstData)
		{
			string sqlQuery = "delete from  LCAmendment where Id = @Id;";
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
