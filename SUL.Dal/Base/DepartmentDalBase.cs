using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class DepartmentDalBase : SqlServerConnection
	{
		public DataTable GetAllDepartment(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Department", "*", " Where Department.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetDepartmentById(Hashtable lstData)
		{
			string whereCondition = " where Department.Id = @Id And Department.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Department", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertDepartment(Hashtable lstData)
		{
			string sqlQuery ="Insert into Department (DepartmentName, PerentDepartmentId, Location, InchargeId, IsActive, CompanyId) values(@DepartmentName, @PerentDepartmentId, @Location, @InchargeId, @IsActive, @CompanyId);";
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

		public int UpdateDepartment(Hashtable lstData)
		{
			string sqlQuery = "Update Department set DepartmentName = @DepartmentName, PerentDepartmentId = @PerentDepartmentId, Location = @Location, InchargeId = @InchargeId, IsActive = @IsActive, CompanyId = @CompanyId where Department.Id = @Id;";
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

		public int DeleteDepartmentById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Department where Id = @Id;";
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
