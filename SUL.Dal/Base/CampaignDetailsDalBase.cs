using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class CampaignDetailsDalBase : SqlServerConnection
	{
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
	    public int GetLastId()
	    {
	        return GetMaximumID("CampaignDetails", "Id", 0, "");
	    }

	    public DataTable GetAllCampaignDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CampaignDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetCampaignDetailsById(Hashtable lstData)
		{
			string whereCondition = " where CampaignDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CampaignDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertCampaignDetails(Hashtable lstData)
		{
			string sqlQuery ="Insert into CampaignDetails (CampaignId, CampaignName, StartValue, EndValue, DiscountPcnt, OfferAmount) values(@CampaignId, @CampaignName, @StartValue, @EndValue, @DiscountPcnt, @OfferAmount);";
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

		public int UpdateCampaignDetails(Hashtable lstData)
		{
			string sqlQuery = "Update CampaignDetails set CampaignId = @CampaignId,  CampaignName = @CampaignName, StartValue = @StartValue, EndValue = @EndValue, DiscountPcnt = @DiscountPcnt, OfferAmount = @OfferAmount where CampaignDetails.Id = @Id;";
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

		public int DeleteCampaignDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  CampaignDetails where Id = @Id;";
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
