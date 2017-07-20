using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class SetupChannelDalBase : SqlServerConnection
	{
		public DataTable GetAllSetupChannel(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SetupChannel", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetSetupChannelById(Hashtable lstData)
		{
			string whereCondition = " where SetupChannel.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SetupChannel", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertSetupChannel(Hashtable lstData)
		{
			string sqlQuery ="Insert into SetupChannel (ChannelPosition, DesignationId) values(@ChannelPosition, @DesignationId);";
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

		public int UpdateSetupChannel(Hashtable lstData)
		{
			string sqlQuery = "Update SetupChannel set ChannelPosition = @ChannelPosition, DesignationId = @DesignationId where SetupChannel.Id = @Id;";
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

		public int DeleteSetupChannelById(Hashtable lstData)
		{
			string sqlQuery = "delete from  SetupChannel where Id = @Id;";
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
