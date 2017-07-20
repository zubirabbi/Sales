using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class CustomerComplainDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllCustomerComplainDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CustomerComplainDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetCustomerComplainDetailsById(Hashtable lstData)
		{
			string whereCondition = " where CustomerComplainDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CustomerComplainDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertCustomerComplainDetails(Hashtable lstData)
		{
			string sqlQuery ="Insert into CustomerComplainDetails (MasterId, Problem, Description) values(@MasterId, @Problem, @Description);";
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

		public int UpdateCustomerComplainDetails(Hashtable lstData)
		{
			string sqlQuery = "Update CustomerComplainDetails set MasterId = @MasterId, Problem = @Problem, Description = @Description where CustomerComplainDetails.Id = @Id;";
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

		public int DeleteCustomerComplainDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  CustomerComplainDetails where Id = @Id;";
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
