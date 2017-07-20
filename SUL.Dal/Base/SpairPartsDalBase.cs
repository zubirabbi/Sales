using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class SpairPartsDalBase : SqlServerConnection
	{
		public DataTable GetAllSpairParts(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SpairParts", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetSpairPartsById(Hashtable lstData)
		{
			string whereCondition = " where SpairParts.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("SpairParts", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertSpairParts(Hashtable lstData)
		{
			string sqlQuery ="Insert into SpairParts (ProductId, SpairPartId, Quentity) values(@ProductId, @SpairPartId, @Quentity);";
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

		public int UpdateSpairParts(Hashtable lstData)
		{
			string sqlQuery = "Update SpairParts set ProductId = @ProductId, SpairPartId = @SpairPartId, Quentity = @Quentity where SpairParts.Id = @Id;";
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

		public int DeleteSpairPartsById(Hashtable lstData)
		{
			string sqlQuery = "delete from  SpairParts where Id = @Id;";
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
