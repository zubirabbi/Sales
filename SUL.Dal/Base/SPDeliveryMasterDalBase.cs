using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class SPDeliveryMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllSPDeliveryMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SPDeliveryMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetSPDeliveryMasterById(Hashtable lstData)
		{
			string whereCondition = " where SPDeliveryMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SPDeliveryMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertSPDeliveryMaster(Hashtable lstData)
		{
			string sqlQuery ="Insert into SPDeliveryMaster (TransactionCode, CreateDate, ServiceCenterId, DeliveryMethod, DeliveryDate, UserId, ApproveBy, ApproveDate, Status, ReceiveDate, ReceiveBy) values(@TransactionCode, @CreateDate, @ServiceCenterId, @DeliveryMethod, @DeliveryDate, @UserId, @ApproveBy, @ApproveDate, @Status, @ReceiveDate, @ReceiveBy);";
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

		public int UpdateSPDeliveryMaster(Hashtable lstData)
		{
			string sqlQuery = "Update SPDeliveryMaster set TransactionCode = @TransactionCode, CreateDate = @CreateDate, ServiceCenterId = @ServiceCenterId, DeliveryMethod = @DeliveryMethod, DeliveryDate = @DeliveryDate, UserId = @UserId, ApproveBy = @ApproveBy, ApproveDate = @ApproveDate, Status = @Status, ReceiveDate = @ReceiveDate, ReceiveBy = @ReceiveBy where SPDeliveryMaster.Id = @Id;";
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

		public int DeleteSPDeliveryMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  SPDeliveryMaster where Id = @Id;";
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
