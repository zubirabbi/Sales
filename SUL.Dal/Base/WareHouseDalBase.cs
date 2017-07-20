using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class WareHouseDalBase : SqlServerConnection
	{
		public DataTable GetAllWareHouse(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("WareHouse", "*", " Where WareHouse.IsActive = 1;", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        public DataTable GetAllWareHousebyWareHouseID(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("WareHouse", "*", " Where WareHouse.CategoryId in(select Id from WareHouseCategory where IsVirtual = 0) And WareHouse.IsActive = 1;", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		public DataTable GetWareHouseById(Hashtable lstData)
		{
			string whereCondition = " where WareHouse.Id = @Id And WareHouse.IsActive = 1";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("WareHouse", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertWareHouse(Hashtable lstData)
		{
            string sqlQuery = "Insert into WareHouse (Code, Name, Location, Incharge, IsActive, CompanyId, CategoryId) values(@Code, @Name, @Location, @Incharge, @IsActive, @CompanyId, @CategoryId);";
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

		public int UpdateWareHouse(Hashtable lstData)
		{
            string sqlQuery = "Update WareHouse set Code = @Code, Name = @Name, Location = @Location, Incharge = @Incharge, IsActive = @IsActive, CompanyId = @CompanyId, CategoryId = @CategoryId where WareHouse.Id = @Id;";
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

		public int DeleteWareHouseById(Hashtable lstData)
		{
			string sqlQuery = "delete from  WareHouse where Id = @Id;";
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
