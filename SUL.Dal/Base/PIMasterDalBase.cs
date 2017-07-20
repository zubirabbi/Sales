using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class PIMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllPIMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("PIMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetPIMasterById(Hashtable lstData)
		{
			string whereCondition = " where PIMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("PIMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertPIMaster(Hashtable lstData)
		{
            string sqlQuery = "Insert into PIMaster (PINo, PIDate, OrderId, VendorId, VendorName, VendorAddress, DocName, DocLocation, CreatedBy,  Status, BankInformation) values(@PINo, @PIDate, @OrderId, @VendorId, @VendorName, @VendorAddress, @DocName, @DocLocation, @CreatedBy,  @Status, @BankInformation);";
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

		public int UpdatePIMaster(Hashtable lstData)
		{
            string sqlQuery = "Update PIMaster set PINo = @PINo, PIDate = @PIDate, OrderId = @OrderId, VendorId = @VendorId, VendorName = @VendorName, VendorAddress = @VendorAddress, DocName = @DocName, DocLocation = @DocLocation, CreatedBy = @CreatedBy, Status = @Status, @BankInformation = BankInformation where PIMaster.Id = @Id;";
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

		public int DeletePIMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  PIMaster where Id = @Id;";
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
