using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ServiceDalBase : SqlServerConnection
	{
		public DataTable GetAllService(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Service", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetServiceById(Hashtable lstData)
		{
			string whereCondition = " where Service.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Service", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertService(Hashtable lstData)
		{
			string sqlQuery ="Insert into Service (ServiceName, ServiceDescription, NWCost, WCost, IsSPReqired, ServiceTime, ServiceLevel) values(@ServiceName, @ServiceDescription, @NWCost, @WCost, @IsSPReqired, @ServiceTime, @ServiceLevel);";
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

		public int UpdateService(Hashtable lstData)
		{
			string sqlQuery = "Update Service set ServiceName = @ServiceName, ServiceDescription = @ServiceDescription, NWCost = @NWCost, WCost = @WCost, IsSPReqired = @IsSPReqired, ServiceTime = @ServiceTime, ServiceLevel = @ServiceLevel where Service.Id = @Id;";
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

		public int DeleteServiceById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Service where Id = @Id;";
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
