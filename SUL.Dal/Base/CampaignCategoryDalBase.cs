using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class CampaignCategoryDalBase : SqlServerConnection
	{
		public DataTable GetAllCampaignCategory(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CampaignCategory", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetCampaignCategoryById(Hashtable lstData)
		{
			string whereCondition = " where CampaignCategory.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CampaignCategory", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertCampaignCategory(Hashtable lstData)
		{
			string sqlQuery ="Insert into CampaignCategory (CampaignCategory, IsOnProduct, IsOnQuantity, IsOnSalesValue, IsGiftItemIncluded) values(@CampaignCategory, @IsOnProduct, @IsOnQuantity, @IsOnSalesValue, @IsGiftItemIncluded);";
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

		public int UpdateCampaignCategory(Hashtable lstData)
		{
			string sqlQuery = "Update CampaignCategory set CampaignCategory = @CampaignCategory, IsOnProduct = @IsOnProduct, IsOnQuantity = @IsOnQuantity, IsOnSalesValue = @IsOnSalesValue, IsGiftItemIncluded = @IsGiftItemIncluded where CampaignCategory.Id = @Id;";
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

		public int DeleteCampaignCategoryById(Hashtable lstData)
		{
			string sqlQuery = "delete from  CampaignCategory where Id = @Id;";
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
