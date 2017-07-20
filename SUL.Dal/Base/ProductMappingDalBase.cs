using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace SUL.Dal.Base
{
	public class ProductMappingDalBase : SqlServerConnection
	{
		public DataTable GetAllProductMapping(Hashtable lstData)
		{
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ProductMapping", "*", "", lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable GetProductMappingById(Hashtable lstData)
		{
			string whereCondition = " where ProductMapping.Id = @Id ";
			DataTable dt = new DataTable();
			try
			{
				dt = GetDataTable("ProductMapping", "*", whereCondition, lstData);
				return dt;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public int InsertProductMapping(Hashtable lstData)
		{
			string sqlQuery ="Insert into ProductMapping (SupplierId, ProductId) values(@SupplierId, @ProductId);";
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

		public int UpdateProductMapping(Hashtable lstData)
		{
			string sqlQuery = "Update ProductMapping set SupplierId = @SupplierId, ProductId = @ProductId where ProductMapping.Id = @Id;";
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

        public int DeleteProductMappingById(Hashtable lstData)
        {
            string sqlQuery = "delete from  ProductMapping where Id = @Id;";
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
