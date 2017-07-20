using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class PurchaseOrderDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllPurchaseOrderDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("PurchaseOrderDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetPurchaseOrderDetailsById(Hashtable lstData)
		{
			string whereCondition = " where PurchaseOrderDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("PurchaseOrderDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertPurchaseOrderDetails(Hashtable lstData)
		{
            string sqlQuery = "Insert into PurchaseOrderDetails (CategoryId, ProductId, ProductName, Quantity, UnitPrice, MasterId, LineTotal, UnitName, UnitId) values(@CategoryId, @ProductId, @ProductName, @Quantity, @UnitPrice, @MasterId, @LineTotal, @UnitName, @UnitId);";
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

		public int UpdatePurchaseOrderDetails(Hashtable lstData)
		{
            string sqlQuery = "Update PurchaseOrderDetails set CategoryId = @CategoryId, ProductId = @ProductId, ProductName = @ProductName, Quantity = @Quantity, UnitPrice = @UnitPrice, MasterId = @MasterId, LineTotal = @LineTotal, UnitName = @UnitName, UnitId = @UnitId where PurchaseOrderDetails.Id = @Id;";
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

		public int DeletePurchaseOrderDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  PurchaseOrderDetails where Id = @Id;";
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
