using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class IncentiveSetupDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllIncentiveSetupDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("IncentiveSetupDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetIncentiveSetupDetailsById(Hashtable lstData)
		{
			string whereCondition = " where IncentiveSetupDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("IncentiveSetupDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertIncentiveSetupDetails(Hashtable lstData)
		{
            string sqlQuery = "Insert into IncentiveSetupDetails (MasterId, StartValue, EndValue, IncentivePcnt, IncentiveValue, Slno, StartQuantity, EndQuantity) values(@MasterId, @StartValue, @EndValue, @IncentivePcnt, @IncentiveValue, @Slno, @StartQuantity, @EndQuantity);";
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

		public int UpdateIncentiveSetupDetails(Hashtable lstData)
		{
            string sqlQuery = "Update IncentiveSetupDetails set MasterId = @MasterId, StartValue = @StartValue, EndValue = @EndValue, IncentivePcnt = @IncentivePcnt, IncentiveValue = @IncentiveValue, Slno = @Slno, StartQuantity = @StartQuantity, EndQuantity = @EndQuantity where IncentiveSetupDetails.Id = @Id;";
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

		public int DeleteIncentiveSetupDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  IncentiveSetupDetails where Id = @Id;";
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
