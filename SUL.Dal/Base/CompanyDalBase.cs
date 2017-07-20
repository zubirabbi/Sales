using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class CompanyDalBase : SqlServerConnection
	{
		public DataTable GetAllCompany(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Company", "*", " Where Company.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetCompanyById(Hashtable lstData)
		{
			string whereCondition = " where Company.Id = @Id And Company.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Company", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertCompany(Hashtable lstData)
		{
            string sqlQuery = "Insert into Company (CompanyName, CompanyCode, Address, EstablishDate, IsActive, Phone, Email, Web, IsParentCompany, CompanyLogo, Address2) values(@CompanyName, @CompanyCode, @Address, @EstablishDate, @IsActive, @Phone, @Email, @Web, @IsParentCompany, @CompanyLogo, @Address2);";
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

		public int UpdateCompany(Hashtable lstData)
		{
            string sqlQuery = "Update Company set CompanyName = @CompanyName, CompanyCode = @CompanyCode, Address = @Address, EstablishDate = @EstablishDate, IsActive = @IsActive, Phone = @Phone, Email = @Email, Web = @Web, IsParentCompany = @IsParentCompany, CompanyLogo = @CompanyLogo, Address2 = @Address2 where Company.Id = @Id;";
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

		public int DeleteCompanyById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Company where Id = @Id;";
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
