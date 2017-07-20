using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ItemReturnMasterDalBase : SqlServerConnection
	{
		public DataTable GetAllItemReturnMaster(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemReturnMaster", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetItemReturnMasterById(Hashtable lstData)
		{
			string whereCondition = " where ItemReturnMaster.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ItemReturnMaster", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertItemReturnMaster(Hashtable lstData)
		{
            string sqlQuery = "Insert into ItemReturnMaster (ReturnDate, Remarks, ReturnCode, ReceiveBy, UserId, Status, ItemTotal, Charges, DealerId) values(@ReturnDate, @Remarks, @ReturnCode, @ReceiveBy, @UserId, @Status, @ItemTotal, @Charges, @DealerId);";
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

		public int UpdateItemReturnMaster(Hashtable lstData)
		{
            string sqlQuery = "Update ItemReturnMaster set ReturnDate = @ReturnDate, Remarks = @Remarks, ReturnCode = @ReturnCode, ReceiveBy = @ReceiveBy, UserId = @UserId, Status = @Status, ItemTotal = @ItemTotal, Charges = @Charges, DealerId = @DealerId where ItemReturnMaster.Id = @Id;";
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

		public int DeleteItemReturnMasterById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ItemReturnMaster where Id = @Id;";
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
