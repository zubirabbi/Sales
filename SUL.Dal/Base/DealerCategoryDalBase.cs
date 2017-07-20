using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class DealerCategoryDalBase : SqlServerConnection
	{
		public DataTable GetAllDealerCategory(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DealerCategory", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetDealerCategoryById(Hashtable lstData)
		{
			string whereCondition = " where DealerCategory.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("DealerCategory", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertDealerCategory(Hashtable lstData)
		{
			string sqlQuery ="Insert into DealerCategory (CategoryCode, Description) values(@CategoryCode, @Description);";
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

		public int UpdateDealerCategory(Hashtable lstData)
		{
			string sqlQuery = "Update DealerCategory set CategoryCode = @CategoryCode, Description = @Description where DealerCategory.Id = @Id;";
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

		public int DeleteDealerCategoryById(Hashtable lstData)
		{
			string sqlQuery = "delete from  DealerCategory where Id = @Id;";
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
