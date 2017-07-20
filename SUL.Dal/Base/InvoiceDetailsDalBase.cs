using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class InvoiceDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllInvoiceDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("InvoiceDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetInvoiceDetailsById(Hashtable lstData)
		{
			string whereCondition = " where InvoiceDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("InvoiceDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertInvoiceDetails(Hashtable lstData)
		{
			string sqlQuery ="Insert into InvoiceDetails (MasterId, CategoryId, ProductId, Quantity, Price, Unit, Discount, Color, Price2) values(@MasterId, @CategoryId, @ProductId, @Quantity, @Price, @Unit, @Discount, @Color, @Price2);";
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

		public int UpdateInvoiceDetails(Hashtable lstData)
		{
			string sqlQuery = "Update InvoiceDetails set MasterId = @MasterId, CategoryId = @CategoryId, ProductId = @ProductId, Quantity = @Quantity, Price = @Price, Unit = @Unit, Discount = @Discount, Color = @Color, Price2 = @Price2 where InvoiceDetails.Id = @Id;";
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

		public int DeleteInvoiceDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  InvoiceDetails where Id = @Id;";
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
