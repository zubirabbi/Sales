using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class InventorySetupDalBase : SqlServerConnection
	{
		public DataTable GetAllInventorySetup(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("InventorySetup", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetInventorySetupById(Hashtable lstData)
		{
			string whereCondition = " where InventorySetup.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("InventorySetup", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertInventorySetup(Hashtable lstData)
		{
			string sqlQuery ="Insert into InventorySetup (PurchaseLocationId) values(@PurchaseLocationId);";
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

		public int UpdateInventorySetup(Hashtable lstData)
		{
			string sqlQuery = "Update InventorySetup set PurchaseLocationId = @PurchaseLocationId where InventorySetup.Id = @Id;";
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

		public int DeleteInventorySetupById(Hashtable lstData)
		{
			string sqlQuery = "delete from  InventorySetup where Id = @Id;";
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
