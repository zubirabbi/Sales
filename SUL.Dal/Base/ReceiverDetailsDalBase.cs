using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ReceiverDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllReceiverDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ReceiverDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetReceiverDetailsById(Hashtable lstData)
		{
			string whereCondition = " where ReceiverDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ReceiverDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertReceiverDetails(Hashtable lstData)
		{
            string sqlQuery = "Insert into ReceiverDetails (MasterId, ProductCode, LCQuantity, ReceivedQuantity, Color, ReceiveQuantity, Unit, ProductId) values(@MasterId, @ProductCode, @LCQuantity, @ReceivedQuantity, @Color, @ReceiveQuantity, @Unit, @ProductId);";
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

		public int UpdateReceiverDetails(Hashtable lstData)
		{
            string sqlQuery = "Update ReceiverDetails set MasterId = @MasterId, ProductCode = @ProductCode, LCQuantity = @LCQuantity, ReceivedQuantity = @ReceivedQuantity, Color = @Color, ReceiveQuantity = @ReceiveQuantity, Unit = @Unit, ProductId = @ProductId where ReceiverDetails.Id = @Id;";
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

		public int DeleteReceiverDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  ReceiverDetails where Id = @Id;";
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
