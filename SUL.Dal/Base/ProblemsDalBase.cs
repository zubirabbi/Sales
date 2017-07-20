using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ProblemsDalBase : SqlServerConnection
	{
		public DataTable GetAllProblems(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Problems", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetProblemsById(Hashtable lstData)
		{
			string whereCondition = " where Problems.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Problems", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertProblems(Hashtable lstData)
		{
			string sqlQuery ="Insert into Problems (Name, Description, SeverLevel) values(@Name, @Description, @SeverLevel);";
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

		public int UpdateProblems(Hashtable lstData)
		{
			string sqlQuery = "Update Problems set Name = @Name, Description = @Description, SeverLevel = @SeverLevel where Problems.Id = @Id;";
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

		public int DeleteProblemsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Problems where Id = @Id;";
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
