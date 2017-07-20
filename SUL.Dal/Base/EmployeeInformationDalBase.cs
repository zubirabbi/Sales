using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class EmployeeInformationDalBase : SqlServerConnection
	{
		public DataTable GetAllEmployeeInformation(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
                dt = GetDataTable("EmployeeInformation", "*", "", lstData);//And EmployeeInformation.IsActive = 1
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetEmployeeInformationById(Hashtable lstData)
		{
            string whereCondition = " where EmployeeInformation.Id = @Id ";//And EmployeeInformation.IsActive = 1
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("EmployeeInformation", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertEmployeeInformation(Hashtable lstData)
		{
            string sqlQuery = "Insert into EmployeeInformation (Code, EmployeeName, DateOfBirth, IdType, IdNo, Gender, Address, PostCode, Nationality, Country, BloodGroup, Phone, Mobile, Email, Photo, IsActive, CompanyId, JoinDate, DepartmentId, DesignationId, SupervisorId, ResignationDate) values(@Code, @EmployeeName, @DateOfBirth, @IdType, @IdNo, @Gender, @Address, @PostCode, @Nationality, @Country, @BloodGroup, @Phone, @Mobile, @Email, @Photo, @IsActive, @CompanyId, @JoinDate, @DepartmentId, @DesignationId, @SupervisorId, @ResignationDate);";
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

		public int UpdateEmployeeInformation(Hashtable lstData)
		{
            string sqlQuery = "Update EmployeeInformation set Code = @Code, EmployeeName = @EmployeeName, DateOfBirth = @DateOfBirth, IdType = @IdType, IdNo = @IdNo, Gender = @Gender, Address = @Address, PostCode = @PostCode, Nationality = @Nationality, Country = @Country, BloodGroup = @BloodGroup, Phone = @Phone, Mobile = @Mobile, Email = @Email, Photo = @Photo, IsActive = @IsActive, CompanyId = @CompanyId, JoinDate = @JoinDate, DepartmentId = @DepartmentId, DesignationId = @DesignationId, SupervisorId = @SupervisorId, ResignationDate = @ResignationDate where EmployeeInformation.Id = @Id;";
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

		public int DeleteEmployeeInformationById(Hashtable lstData)
		{
			string sqlQuery = "delete from  EmployeeInformation where Id = @Id;";
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
