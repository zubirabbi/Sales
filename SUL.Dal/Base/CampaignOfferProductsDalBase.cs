using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class CampaignOfferProductsDalBase : SqlServerConnection
	{
		public DataTable GetAllCampaignOfferProducts(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CampaignOfferProducts", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetCampaignOfferProductsById(Hashtable lstData)
		{
			string whereCondition = " where CampaignOfferProducts.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CampaignOfferProducts", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertCampaignOfferProducts(Hashtable lstData)
		{
            string sqlQuery = "Insert into CampaignOfferProducts (CampaignId, CampaignDetailsId, ProductType, ProductId, Quantity) values(@CampaignId, @CampaignDetailsId, @ProductType, @ProductId, @Quantity);";
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

		public int UpdateCampaignOfferProducts(Hashtable lstData)
		{
            string sqlQuery = "Update CampaignOfferProducts set CampaignId = @CampaignId, CampaignDetailsId = @CampaignDetailsId, ProductType = @ProductType, ProductId = @ProductId, Quantity = @Quantity where CampaignOfferProducts.Id = @Id;";
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

		public int DeleteCampaignOfferProductsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  CampaignOfferProducts where Id = @Id;";
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
