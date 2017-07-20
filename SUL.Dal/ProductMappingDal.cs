using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
    public class ProductMappingDal : SUL.Dal.Base.ProductMappingDalBase
    {
        public ProductMappingDal()
            : base()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public int CheckProductIdExistance(Hashtable lstData)
        {

            string whereCondition = "where ProductMapping.ProductId = @ProductId And  ProductMapping.SupplierId = @SupplierId";

            int count = 0;
            try
            {
                count = CheckExistence("ProductMapping", "Id", whereCondition, lstData);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public int DeleteProductMappingBySupplierIdAndProductId(Hashtable lstData, string _Ids)
        {
            string sqlQuery = "delete from  ProductMapping where SupplierId = @SupplierId ";
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

        public DataTable GetAllProductMappingBySupplierId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ProductMapping", "*", "where SupplierId = @SupplierId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
