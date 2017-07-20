using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ReceiverMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllReceiverMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ReceiverMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetReceiverMasterById(Hashtable lstData)
		{
			string whereCondition = " where ReceiverMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ReceiverMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertReceiverMaster(Hashtable lstData)
		{
            string sqlQuery = "Insert into ReceiverMaster (CompanyId, ReceivingCode, VendorId, LCId, PIId, ReceivedBy, InvoiceNo, WareHouseId, IMEIName, IMEIUplodePath, ReceivingDate, IsInvoiceCreated, TotalAmount, UserId, Status) values(@CompanyId, @ReceivingCode, @VendorId, @LCId, @PIId, @ReceivedBy, @InvoiceNo, @WareHouseId, @IMEIName, @IMEIUplodePath, @ReceivingDate, @IsInvoiceCreated, @TotalAmount, @UserId, @Status);";
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

		public int UpdateReceiverMaster(Hashtable lstData)
		{
            string sqlQuery = "Update ReceiverMaster set CompanyId = @CompanyId, ReceivingCode = @ReceivingCode, VendorId = @VendorId, LCId = @LCId, PIId = @PIId, ReceivedBy = @ReceivedBy, InvoiceNo = @InvoiceNo, WareHouseId = @WareHouseId, IMEIName = @IMEIName, IMEIUplodePath = @IMEIUplodePath, ReceivingDate = @ReceivingDate, IsInvoiceCreated = @IsInvoiceCreated, TotalAmount = @TotalAmount, UserId = @UserId, Status = @Status where ReceiverMaster.Id = @Id;";
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

		public int DeleteReceiverMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ReceiverMaster where Id = @Id;";
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
