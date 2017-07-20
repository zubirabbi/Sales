using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class CampaignMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllCampaignMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CampaignMaster", "*", " Where CampaignMaster.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        

		public DataTable GetCampaignMasterById(Hashtable lstData)
		{
			string whereCondition = " where CampaignMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CampaignMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertCampaignMaster(Hashtable lstData)
		{
            string sqlQuery = "Insert into CampaignMaster (StartDate, EndDate, RegionId, CampaignCode, Description, IsAdjustedAfterEnd, CreatedBy, CreateDate, IsActive, IsExcludedfromIncentive, CampaignOn) values(@StartDate, @EndDate, @RegionId, @CampaignCode, @Description, @IsAdjustedAfterEnd, @CreatedBy, @CreateDate, @IsActive, @IsExcludedfromIncentive, @CampaignOn);";
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

		public int UpdateCampaignMaster(Hashtable lstData)
		{
            string sqlQuery = "Update CampaignMaster set StartDate = @StartDate, EndDate = @EndDate, RegionId = @RegionId, CampaignCode = @CampaignCode, Description = @Description, IsAdjustedAfterEnd = @IsAdjustedAfterEnd, CreatedBy = @CreatedBy, CreateDate = @CreateDate, IsActive = @IsActive, IsExcludedfromIncentive = @IsExcludedfromIncentive, CampaignOn = @CampaignOn where CampaignMaster.Id = @Id;";
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

		public int DeleteCampaignMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  CampaignMaster where Id = @Id;";
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
