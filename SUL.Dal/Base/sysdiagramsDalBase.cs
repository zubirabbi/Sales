using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class sysdiagramsDalBase : SqlServerConnection
	{
		public DataTable GetAllsysdiagrams(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("sysdiagrams", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetsysdiagramsByname(Hashtable lstData)
		{
			string whereCondition = " where sysdiagrams.name = @name ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("sysdiagrams", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int Insertsysdiagrams(Hashtable lstData)
		{
			string sqlQuery ="Insert into sysdiagrams (name, principal_id, version, definition) values(@name, @principal_id, @version, @definition);";
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

		public int Updatesysdiagrams(Hashtable lstData)
		{
			string sqlQuery = "Update sysdiagrams set principal_id = @principal_id, version = @version, definition = @definition where sysdiagrams.name = @name;";
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

		public int DeletesysdiagramsByname(Hashtable lstData)
		{
			string sqlQuery = "delete from  sysdiagrams where name = @name;";
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
