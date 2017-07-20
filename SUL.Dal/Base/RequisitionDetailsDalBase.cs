using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class RequisitionDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllRequisitionDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("RequisitionDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetRequisitionDetailsById(Hashtable lstData)
		{
			string whereCondition = " where RequisitionDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("RequisitionDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertRequisitionDetails(Hashtable lstData)
		{
            string sqlQuery = "Insert into RequisitionDetails (MasterId, CategoryId, ProductId, Quantity, Price, Unit, Discount, Color, Price2, CampaignDetailsId, Discount2) values(@MasterId, @CategoryId, @ProductId, @Quantity, @Price, @Unit, @Discount, @Color, @Price2, @CampaignDetailsId, @Discount2);";
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

		public int UpdateRequisitionDetails(Hashtable lstData)
		{
            string sqlQuery = "Update RequisitionDetails set MasterId = @MasterId, CategoryId = @CategoryId, ProductId = @ProductId, Quantity = @Quantity, Price = @Price, Unit = @Unit, Discount = @Discount, Color = @Color, Price2 = @Price2, CampaignDetailsId = @CampaignDetailsId, Discount2 = @Discount2 where RequisitionDetails.Id = @Id;";
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

		public int DeleteRequisitionDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  RequisitionDetails where Id = @Id;";
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
