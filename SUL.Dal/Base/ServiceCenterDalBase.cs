using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ServiceCenterDalBase : SqlServerConnection
	{
		public DataTable GetAllServiceCenter(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ServiceCenter", "*", " Where ServiceCenter.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetServiceCenterById(Hashtable lstData)
		{
			string whereCondition = " where ServiceCenter.Id = @Id And ServiceCenter.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ServiceCenter", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertServiceCenter(Hashtable lstData)
		{
			string sqlQuery ="Insert into ServiceCenter (SCCode, AreaId, SCName, SCAddress, InChargeId, EstablishDate, IsActive) values(@SCCode, @AreaId, @SCName, @SCAddress, @InChargeId, @EstablishDate, @IsActive);";
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

		public int UpdateServiceCenter(Hashtable lstData)
		{
			string sqlQuery = "Update ServiceCenter set SCCode = @SCCode, AreaId = @AreaId, SCName = @SCName, SCAddress = @SCAddress, InChargeId = @InChargeId, EstablishDate = @EstablishDate, IsActive = @IsActive where ServiceCenter.Id = @Id;";
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

		public int DeleteServiceCenterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ServiceCenter where Id = @Id;";
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
