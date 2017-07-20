using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class DeliveryDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllDeliveryDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DeliveryDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetDeliveryDetailsById(Hashtable lstData)
		{
			string whereCondition = " where DeliveryDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DeliveryDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertDeliveryDetails(Hashtable lstData)
		{
			string sqlQuery ="Insert into DeliveryDetails (MasterId, ProductId, Quantity, Unit, Color) values(@MasterId, @ProductId, @Quantity, @Unit, @Color);";
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

		public int UpdateDeliveryDetails(Hashtable lstData)
		{
			string sqlQuery = "Update DeliveryDetails set MasterId = @MasterId, ProductId = @ProductId, Quantity = @Quantity, Unit = @Unit, Color = @Color where DeliveryDetails.Id = @Id;";
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

		public int DeleteDeliveryDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  DeliveryDetails where Id = @Id;";
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
