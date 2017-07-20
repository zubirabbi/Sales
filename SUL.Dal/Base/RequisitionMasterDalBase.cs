using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class RequisitionMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllRequisitionMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("RequisitionMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetRequisitionMasterById(Hashtable lstData)
		{
			string whereCondition = " where RequisitionMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("RequisitionMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertRequisitionMaster(Hashtable lstData)
		{
            string sqlQuery = "Insert into RequisitionMaster (DealerId, Address, Courier, RequisitionCode, RequisitionDate, CSId, Status, IsInvoiceCreated, JrCSId, ItemTotal, ItemTotal2, Discount, UserId, CencelDate, CencelBy, CencelNote, LastBalance, Remarks, CampaignId, UpdateBy) values(@DealerId, @Address, @Courier, @RequisitionCode, @RequisitionDate, @CSId, @Status, @IsInvoiceCreated, @JrCSId, @ItemTotal, @ItemTotal2, @Discount, @UserId, @CencelDate, @CencelBy, @CencelNote, @LastBalance, @Remarks, @CampaignId, @UpdateBy);";
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

		public int UpdateRequisitionMaster(Hashtable lstData)
		{
            string sqlQuery = "Update RequisitionMaster set DealerId = @DealerId, Address = @Address, Courier = @Courier, RequisitionCode = @RequisitionCode, RequisitionDate = @RequisitionDate, CSId = @CSId, Status = @Status, IsInvoiceCreated = @IsInvoiceCreated, JrCSId = @JrCSId, ItemTotal = @ItemTotal, ItemTotal2 = @ItemTotal2, Discount = @Discount, UserId = @UserId, CencelDate = @CencelDate, CencelBy = @CencelBy, CencelNote = @CencelNote, LastBalance = @LastBalance, Remarks = @Remarks, CampaignId = @CampaignId,  UpdateBy = @UpdateBy  where RequisitionMaster.Id = @Id;";
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

		public int DeleteRequisitionMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  RequisitionMaster where Id = @Id;";
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
