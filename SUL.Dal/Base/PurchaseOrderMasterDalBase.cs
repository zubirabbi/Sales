using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class PurchaseOrderMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllPurchaseOrderMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("PurchaseOrderMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        public DataTable GetPurchaseOrderMasterById(Hashtable lstData)
		{
			string whereCondition = " where PurchaseOrderMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("PurchaseOrderMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertPurchaseOrderMaster(Hashtable lstData)
		{
			string sqlQuery ="Insert into PurchaseOrderMaster (VendorId, VendorName, VandorAddress, OrderNo, OrderDate, TotalPrice, Status, CreatedBy) values(@VendorId, @VendorName, @VandorAddress, @OrderNo, @OrderDate, @TotalPrice, @Status, @CreatedBy);";
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

		public int UpdatePurchaseOrderMaster(Hashtable lstData)
		{
			string sqlQuery = "Update PurchaseOrderMaster set VendorId = @VendorId, VendorName = @VendorName, VandorAddress = @VandorAddress, OrderNo = @OrderNo, OrderDate = @OrderDate, TotalPrice = @TotalPrice, Status = @Status, CreatedBy = @CreatedBy where PurchaseOrderMaster.Id = @Id;";
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

		public int DeletePurchaseOrderMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  PurchaseOrderMaster where Id = @Id;";
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
