using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class BankInformationDalBase : SqlServerConnection
	{
		public DataTable GetAllBankInformation(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("BankInformation", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetBankInformationById(Hashtable lstData)
		{
			string whereCondition = " where BankInformation.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("BankInformation", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertBankInformation(Hashtable lstData)
		{
            string sqlQuery = "Insert into BankInformation (Type, TypeId, BankName, BranchName, AccountNo, AccountTitle, SWIFTCode, Country, IsDefault, ShortName) values(@Type, @TypeId, @BankName, @BranchName, @AccountNo, @AccountTitle, @SWIFTCode, @Country, @IsDefault, @ShortName);";
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

		public int UpdateBankInformation(Hashtable lstData)
		{
            string sqlQuery = "Update BankInformation set Type = @Type, TypeId = @TypeId, BankName = @BankName, BranchName = @BranchName, AccountNo = @AccountNo, AccountTitle = @AccountTitle, SWIFTCode = @SWIFTCode, Country = @Country, IsDefault = @IsDefault, ShortName = @ShortName where BankInformation.Id = @Id;";
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

		public int DeleteBankInformationById(Hashtable lstData)
		{
			string sqlQuery = "delete from  BankInformation where Id = @Id;";
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
