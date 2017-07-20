using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class SPDeliveryDetailsDalBase : SqlServerConnection
	{
		public DataTable GetAllSPDeliveryDetails(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SPDeliveryDetails", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetSPDeliveryDetailsById(Hashtable lstData)
		{
			string whereCondition = " where SPDeliveryDetails.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SPDeliveryDetails", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertSPDeliveryDetails(Hashtable lstData)
		{
			string sqlQuery ="Insert into SPDeliveryDetails (MasterId, SpairPartsId, Color, Rate, Quantity, TotalAmount, Unit) values(@MasterId, @SpairPartsId, @Color, @Rate, @Quantity, @TotalAmount, @Unit);";
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

		public int UpdateSPDeliveryDetails(Hashtable lstData)
		{
			string sqlQuery = "Update SPDeliveryDetails set MasterId = @MasterId, SpairPartsId = @SpairPartsId, Color = @Color, Rate = @Rate, Quantity = @Quantity, TotalAmount = @TotalAmount, Unit = @Unit where SPDeliveryDetails.Id = @Id;";
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

		public int DeleteSPDeliveryDetailsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  SPDeliveryDetails where Id = @Id;";
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
