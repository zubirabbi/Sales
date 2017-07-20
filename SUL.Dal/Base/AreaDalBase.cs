using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class AreaDalBase : SqlServerConnection
	{
		public DataTable GetAllArea(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Area", "*", " Where Area.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetAreaById(Hashtable lstData)
		{
			string whereCondition = " where Area.Id = @Id And Area.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Area", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertArea(Hashtable lstData)
		{
            string sqlQuery = "Insert into Area (AreaName, AreaCode, Description, RegionId, ChanelSpecialities, JrChanelSpecialities, IsActive) values(@AreaName, @AreaCode, @Description, @RegionId, @ChanelSpecialities, @JrChanelSpecialities, @IsActive);";
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

		public int UpdateArea(Hashtable lstData)
		{
            string sqlQuery = "Update Area set AreaName = @AreaName, AreaCode = @AreaCode, Description = @Description, RegionId = @RegionId, ChanelSpecialities = @ChanelSpecialities, JrChanelSpecialities = @JrChanelSpecialities, IsActive = @IsActive where Area.Id = @Id;";
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

		public int DeleteAreaById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Area where Id = @Id;";
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
