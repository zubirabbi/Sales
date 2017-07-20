using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class IncentiveSetupDalBase : SqlServerConnection
	{
		public DataTable GetAllIncentiveSetup(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("IncentiveSetup", "*", " Where IncentiveSetup.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetIncentiveSetupById(Hashtable lstData)
		{
			string whereCondition = " where IncentiveSetup.Id = @Id And IncentiveSetup.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("IncentiveSetup", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertIncentiveSetup(Hashtable lstData)
		{
            string sqlQuery = "Insert into IncentiveSetup (Type, DesignationId, SetupDate, IsActive, UserId, IncentiveOn, ApplyOn, ProductId) values(@Type, @DesignationId, @SetupDate, @IsActive, @UserId, @IncentiveOn, @ApplyOn, @ProductId);";
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

		public int UpdateIncentiveSetup(Hashtable lstData)
		{
            string sqlQuery = "Update IncentiveSetup set Type = @Type, DesignationId = @DesignationId, SetupDate = @SetupDate, IsActive = @IsActive, UserId = @UserId, IncentiveOn = @IncentiveOn, ApplyOn = @ApplyOn, ProductId = @ProductId where IncentiveSetup.Id = @Id;";
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

		public int DeleteIncentiveSetupById(Hashtable lstData)
		{
			string sqlQuery = "delete from  IncentiveSetup where Id = @Id;";
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
