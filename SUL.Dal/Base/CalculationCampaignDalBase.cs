using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class CalculationCampaignDalBase : SqlServerConnection
	{
		public DataTable GetAllCalculationCampaign(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CalculationCampaign", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetCalculationCampaignById(Hashtable lstData)
		{
			string whereCondition = " where CalculationCampaign.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CalculationCampaign", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertCalculationCampaign(Hashtable lstData)
		{
			string sqlQuery ="Insert into CalculationCampaign (DealerId, DealerName, CampaignId, CampaignName, Amount, Discount, IsAppiled, ApproveBy) values(@DealerId, @DealerName, @CampaignId, @CampaignName, @Amount, @Discount, @IsAppiled, @ApproveBy);";
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

		public int UpdateCalculationCampaign(Hashtable lstData)
		{
			string sqlQuery = "Update CalculationCampaign set DealerId = @DealerId, DealerName = @DealerName, CampaignId = @CampaignId, CampaignName = @CampaignName, Amount = @Amount, Discount = @Discount, IsAppiled = @IsAppiled, ApproveBy = @ApproveBy where CalculationCampaign.Id = @Id;";
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

		public int DeleteCalculationCampaignById(Hashtable lstData)
		{
			string sqlQuery = "delete from  CalculationCampaign where Id = @Id;";
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
