using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class CustomerComplainMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllCustomerComplainMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CustomerComplainMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetCustomerComplainMasterById(Hashtable lstData)
		{
			string whereCondition = " where CustomerComplainMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CustomerComplainMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertCustomerComplainMaster(Hashtable lstData)
		{
			string sqlQuery ="Insert into CustomerComplainMaster (ComplainCode, ComplainDate, CustomerName, MobileNo, Address, AlternativeMobile, ProductId, Imei, PurchaseDate, WarentyExpairDate, Description, PurchaseReceiptNo, DeliveryDate, UserId) values(@ComplainCode, @ComplainDate, @CustomerName, @MobileNo, @Address, @AlternativeMobile, @ProductId, @Imei, @PurchaseDate, @WarentyExpairDate, @Description, @PurchaseReceiptNo, @DeliveryDate, @UserId);";
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

		public int UpdateCustomerComplainMaster(Hashtable lstData)
		{
			string sqlQuery = "Update CustomerComplainMaster set ComplainCode = @ComplainCode, ComplainDate = @ComplainDate, CustomerName = @CustomerName, MobileNo = @MobileNo, Address = @Address, AlternativeMobile = @AlternativeMobile, ProductId = @ProductId, Imei = @Imei, PurchaseDate = @PurchaseDate, WarentyExpairDate = @WarentyExpairDate, Description = @Description, PurchaseReceiptNo = @PurchaseReceiptNo, DeliveryDate = @DeliveryDate, UserId = @UserId where CustomerComplainMaster.Id = @Id;";
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

		public int DeleteCustomerComplainMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  CustomerComplainMaster where Id = @Id;";
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
