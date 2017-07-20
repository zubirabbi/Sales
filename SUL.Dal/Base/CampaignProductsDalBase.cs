using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class CampaignProductsDalBase : SqlServerConnection
	{
		public DataTable GetAllCampaignProducts(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CampaignProducts", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetCampaignProductsById(Hashtable lstData)
		{
			string whereCondition = " where CampaignProducts.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CampaignProducts", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertCampaignProducts(Hashtable lstData)
		{
            string sqlQuery = "Insert into CampaignProducts ( CampaignId, CampaignDetailsId, ProductId, StartQuantity, EndQuantity, Price) values( @CampaignId, @CampaignDetailsId, @ProductId, @StartQuantity, @EndQuantity, @Price);";
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

		public int UpdateCampaignProducts(Hashtable lstData)
		{
            string sqlQuery = "Update CampaignProducts set CampaignId = @CampaignId, CampaignDetailsId = @CampaignDetailsId, ProductId = @ProductId, StartQuantity = @StartQuantity, EndQuantity = @EndQuantity, Price = @Price where CampaignProducts.Id = @Id;";
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

		public int DeleteCampaignProductsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  CampaignProducts where Id = @Id;";
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
