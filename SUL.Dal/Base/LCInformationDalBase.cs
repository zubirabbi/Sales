using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class LCInformationDalBase : SqlServerConnection
	{
		public DataTable GetAllLCInformation(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("LCInformation", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetLCInformationById(Hashtable lstData)
		{
			string whereCondition = " where LCInformation.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("LCInformation", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertLCInformation(Hashtable lstData)
		{
			string sqlQuery ="Insert into LCInformation (VendorId, VendorName, VendorAddress, PINo, LCNumber, LCDate, LCExpiryDate, LCValue, LCStatus, IssusingBank, NegotiatingBank, FileName, FileLocation) values(@VendorId, @VendorName, @VendorAddress, @PINo, @LCNumber, @LCDate, @LCExpiryDate, @LCValue, @LCStatus, @IssusingBank, @NegotiatingBank, @FileName, @FileLocation);";
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

		public int UpdateLCInformation(Hashtable lstData)
		{
			string sqlQuery = "Update LCInformation set VendorId = @VendorId, VendorName = @VendorName, VendorAddress = @VendorAddress, PINo = @PINo, LCNumber = @LCNumber, LCDate = @LCDate, LCExpiryDate = @LCExpiryDate, LCValue = @LCValue, LCStatus = @LCStatus, IssusingBank = @IssusingBank, NegotiatingBank = @NegotiatingBank, FileName = @FileName, FileLocation = @FileLocation where LCInformation.Id = @Id;";
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

		public int DeleteLCInformationById(Hashtable lstData)
		{
			string sqlQuery = "delete from  LCInformation where Id = @Id;";
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
