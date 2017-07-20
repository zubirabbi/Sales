using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class DesignationDalBase : SqlServerConnection
	{
		public DataTable GetAllDesignation(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Designation", "*", " Where Designation.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetDesignationById(Hashtable lstData)
		{
			string whereCondition = " where Designation.Id = @Id And Designation.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Designation", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertDesignation(Hashtable lstData)
		{
            string sqlQuery = "Insert into Designation (DesignationName, DepartmentId, DesignationCode, Level, UpperHierarchy, Responsibility, IsActive, CompanyId) values(@DesignationName, @DepartmentId, @DesignationCode, @Level, @UpperHierarchy, @Responsibility, @IsActive, @CompanyId);";
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

		public int UpdateDesignation(Hashtable lstData)
		{
            string sqlQuery = "Update Designation set DesignationName = @DesignationName, DepartmentId = @DepartmentId, DesignationCode = @DesignationCode, Level = @Level, UpperHierarchy = @UpperHierarchy, Responsibility = @Responsibility, IsActive = @IsActive, CompanyId = @CompanyId where Designation.Id = @Id;";
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

		public int DeleteDesignationById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Designation where Id = @Id;";
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
