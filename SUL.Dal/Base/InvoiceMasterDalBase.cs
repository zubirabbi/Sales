using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class InvoiceMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllInvoiceMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("InvoiceMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetInvoiceMasterById(Hashtable lstData)
		{
			string whereCondition = " where InvoiceMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("InvoiceMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertInvoiceMaster(Hashtable lstData)
		{
            string sqlQuery = "Insert into InvoiceMaster (InvoiceNo, InvoiceDate, DealerId, RequisitionId, ItemTotal, Discount, ItemTotal2, UserId) values(@InvoiceNo, @InvoiceDate, @DealerId, @RequisitionId, @ItemTotal, @Discount, @ItemTotal2, @UserId);";
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

		public int UpdateInvoiceMaster(Hashtable lstData)
		{
            string sqlQuery = "Update InvoiceMaster set InvoiceNo = @InvoiceNo, InvoiceDate = @InvoiceDate, DealerId = @DealerId, RequisitionId = @RequisitionId, ItemTotal = @ItemTotal, Discount = @Discount, ItemTotal2 = @ItemTotal2, UserId = @UserId where InvoiceMaster.Id = @Id;";
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

		public int DeleteInvoiceMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  InvoiceMaster where Id = @Id;";
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
