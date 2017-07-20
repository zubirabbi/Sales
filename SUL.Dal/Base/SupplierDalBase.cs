using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class SupplierDalBase : SqlServerConnection
	{
		public DataTable GetAllSupplier(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Supplier", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetSupplierById(Hashtable lstData)
		{
			string whereCondition = " where Supplier.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("Supplier", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertSupplier(Hashtable lstData)
		{
			string sqlQuery ="Insert into Supplier (Name, Code, CompanyAddress, FactoryAddress, Phone, Mobile, Email, ContactPerson, Designation, TotalDebit, TotalCredit, Balance) values(@Name, @Code, @CompanyAddress, @FactoryAddress, @Phone, @Mobile, @Email, @ContactPerson, @Designation, @TotalDebit, @TotalCredit, @Balance);";
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

		public int UpdateSupplier(Hashtable lstData)
		{
			string sqlQuery = "Update Supplier set Name = @Name, Code = @Code, CompanyAddress = @CompanyAddress, FactoryAddress = @FactoryAddress, Phone = @Phone, Mobile = @Mobile, Email = @Email, ContactPerson = @ContactPerson, Designation = @Designation, TotalDebit = @TotalDebit, TotalCredit = @TotalCredit, Balance = @Balance where Supplier.Id = @Id;";
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

		public int DeleteSupplierById(Hashtable lstData)
		{
			string sqlQuery = "delete from  Supplier where Id = @Id;";
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
