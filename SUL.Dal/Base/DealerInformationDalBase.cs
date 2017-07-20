using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class DealerInformationDalBase : SqlServerConnection
	{
		public DataTable GetAllDealerInformation(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DealerInformation", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetDealerInformationById(Hashtable lstData)
		{
			string whereCondition = " where DealerInformation.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DealerInformation", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertDealerInformation(Hashtable lstData)
		{
            string sqlQuery = "Insert into DealerInformation (DealerName, DealerCode, Area, Address, CS, ProprietorName, Phone, Mobile, Email, DealerCategory, StartDate, IsActive, TotalDebit, TotalCredit, JrCS, WareHouseCode, CreditLimit) values(@DealerName, @DealerCode, @Area, @Address, @CS, @ProprietorName, @Phone, @Mobile, @Email, @DealerCategory, @StartDate, @IsActive, @TotalDebit, @TotalCredit, @JrCS, @WareHouseCode, @CreditLimit);";
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

		public int UpdateDealerInformation(Hashtable lstData)
		{
            string sqlQuery = "Update DealerInformation set DealerName = @DealerName, DealerCode = @DealerCode, Area = @Area, Address = @Address, CS = @CS, ProprietorName = @ProprietorName, Phone = @Phone, Mobile = @Mobile, Email = @Email, DealerCategory = @DealerCategory, StartDate = @StartDate, IsActive = @IsActive,  JrCS = @JrCS, WareHouseCode = @WareHouseCode, CreditLimit = @CreditLimit where DealerInformation.Id = @Id;";
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

		public int DeleteDealerInformationById(Hashtable lstData)
		{
			string sqlQuery = "delete from  DealerInformation where Id = @Id;";
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
