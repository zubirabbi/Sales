using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class RegionDalBase : SqlServerConnection
	{
		public DataTable GetAllRegion(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Region", "*", " Where Region.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetRegionById(Hashtable lstData)
		{
			string whereCondition = " where Region.Id = @Id And Region.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Region", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertRegion(Hashtable lstData)
		{
            string sqlQuery = "Insert into Region (RegionName, RegionCode, Description, ChanelManager, AssistantChanelManager, IsActive) values(@RegionName, @RegionCode, @Description, @ChanelManager, @AssistantChanelManager, @IsActive);";
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

		public int UpdateRegion(Hashtable lstData)
		{
            string sqlQuery = "Update Region set RegionName = @RegionName, RegionCode = @RegionCode, Description = @Description, ChanelManager = @ChanelManager, AssistantChanelManager = @AssistantChanelManager, IsActive = @IsActive where Region.Id = @Id;";
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

		public int DeleteRegionById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Region where Id = @Id;";
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
