using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ProductColorDal : SUL.Dal.Base.ProductColorDalBase
	{
		public ProductColorDal() : base()
		{
		}
        public int CheckColorIdExistance(Hashtable lstData)
        {

            string whereCondition = "where ProductColor.ProductId = @ProductId And  ProductColor.Color = @ColorId";

            int count = 0;
            try
            {
                count = CheckExistence("ProductColor", "Id", whereCondition, lstData);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllProductColorbyProductid(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ProductColor", "*", "where ProductColor.ProductId = @ProductId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetAllVewProductColorbyProductid(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewColorProduct", "*", "where ProductId = @ProductId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int DeleteProductColorByProductIdId(Hashtable lstData)
        {
            string sqlQuery = "delete from  ProductColor where ProductId = @ProductId;";
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
        public int DeleteProductColorByProductIdColorId(Hashtable lstData)
        {
            string sqlQuery = "delete from  ProductColor where ProductId = @ProductId And Color = @Color;";
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
        public int DeleteProductMappingByColorIdAndProductId(Hashtable lstData, string _Ids)
        {
            string sqlQuery = "delete from  ProductColor where  ProductId = @ProductId ";
            if (_Ids != string.Empty)
                sqlQuery = sqlQuery + " AND (" + _Ids + ")";
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
