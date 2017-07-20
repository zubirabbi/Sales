using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class RequisitionCampaignsDalBase : SqlServerConnection
	{
		public DataTable GetAllRequisitionCampaigns(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("RequisitionCampaigns", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetRequisitionCampaignsById(Hashtable lstData)
		{
			string whereCondition = " where RequisitionCampaigns.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("RequisitionCampaigns", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertRequisitionCampaigns(Hashtable lstData)
		{
			string sqlQuery ="Insert into RequisitionCampaigns (RequisitionId, CampaignDetailsId, Quantity) values(@RequisitionId, @CampaignDetailsId, @Quantity);";
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

		public int UpdateRequisitionCampaigns(Hashtable lstData)
		{
			string sqlQuery = "Update RequisitionCampaigns set RequisitionId = @RequisitionId, CampaignDetailsId = @CampaignDetailsId, Quantity = @Quantity where RequisitionCampaigns.Id = @Id;";
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

		public int DeleteRequisitionCampaignsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  RequisitionCampaigns where Id = @Id;";
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
