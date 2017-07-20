using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class CourierInformationDalBase : SqlServerConnection
	{
		public DataTable GetAllCourierInformation(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CourierInformation", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetCourierInformationById(Hashtable lstData)
		{
			string whereCondition = " where CourierInformation.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("CourierInformation", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertCourierInformation(Hashtable lstData)
		{
			string sqlQuery ="Insert into CourierInformation (Name, Address, ContactName, ContactNo) values(@Name, @Address, @ContactName, @ContactNo);";
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

		public int UpdateCourierInformation(Hashtable lstData)
		{
			string sqlQuery = "Update CourierInformation set Name = @Name, Address = @Address, ContactName = @ContactName, ContactNo = @ContactNo where CourierInformation.Id = @Id;";
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

		public int DeleteCourierInformationById(Hashtable lstData)
		{
            string sqlQuery = "delete from  CourierInformation where CourierInformation.Id = @Id;";
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
