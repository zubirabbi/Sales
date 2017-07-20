using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class DeliveryMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllDeliveryMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DeliveryMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetDeliveryMasterById(Hashtable lstData)
		{
			string whereCondition = " where DeliveryMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DeliveryMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertDeliveryMaster(Hashtable lstData)
		{
			string sqlQuery ="Insert into DeliveryMaster (DeliveryNo, DeliveryDate, DealerId, RequisitionId, WareHouseId, CourierId, DeliveryAddress) values(@DeliveryNo, @DeliveryDate, @DealerId, @RequisitionId, @WareHouseId, @CourierId, @DeliveryAddress);";
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

		public int UpdateDeliveryMaster(Hashtable lstData)
		{
			string sqlQuery = "Update DeliveryMaster set DeliveryNo = @DeliveryNo, DeliveryDate = @DeliveryDate, DealerId = @DealerId, RequisitionId = @RequisitionId, WareHouseId = @WareHouseId, CourierId = @CourierId, DeliveryAddress = @DeliveryAddress where DeliveryMaster.Id = @Id;";
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

		public int DeleteDeliveryMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  DeliveryMaster where Id = @Id;";
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
