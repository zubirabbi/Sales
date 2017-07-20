using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class SalesIncentiveSetupDalBase : SqlServerConnection
	{
		public DataTable GetAllSalesIncentiveSetup(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SalesIncentiveSetup", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetSalesIncentiveSetupById(Hashtable lstData)
		{
			string whereCondition = " where SalesIncentiveSetup.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SalesIncentiveSetup", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertSalesIncentiveSetup(Hashtable lstData)
		{
			string sqlQuery ="Insert into SalesIncentiveSetup (Designation, Startvalue, EndValue, Discount, Amount, GiftProduct, GiftQuantity) values(@Designation, @Startvalue, @EndValue, @Discount, @Amount, @GiftProduct, @GiftQuantity);";
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

		public int UpdateSalesIncentiveSetup(Hashtable lstData)
		{
			string sqlQuery = "Update SalesIncentiveSetup set Designation = @Designation, Startvalue = @Startvalue, EndValue = @EndValue, Discount = @Discount, Amount = @Amount, GiftProduct = @GiftProduct, GiftQuantity = @GiftQuantity where SalesIncentiveSetup.Id = @Id;";
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

		public int DeleteSalesIncentiveSetupById(Hashtable lstData)
		{
			string sqlQuery = "delete from  SalesIncentiveSetup where Id = @Id;";
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
