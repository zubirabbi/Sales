using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ProductDal : SUL.Dal.Base.ProductDalBase
	{
		public ProductDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckProductCodeExistance(Hashtable lstData, bool isNewEntry)
        {

            string whereCondition = string.Empty;

            if (isNewEntry)
                whereCondition = " where Product.ProductCode = @ProductCode";
            else
                whereCondition = "where Product.ProductCode = @ProductCode And Product.Id <> @Id";

            int count = 0;
            try
            {
                count = CheckExistence("Product", "Id", whereCondition, lstData);
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
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckProductNameExistance(Hashtable lstData, bool isNewEntry)
        {

            string whereCondition = string.Empty;

            if (isNewEntry)
                whereCondition = " where Product.ProductName = @ProductName";
            else
                whereCondition = "where Product.ProductName = @ProductName And Product.Id <> @Id";

            int count = 0;
            try
            {
                count = CheckExistence("Product", "Id", whereCondition, lstData);
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
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckProductModelExistance(Hashtable lstData, bool isNewEntry)
        {

            string whereCondition = string.Empty;

            if (isNewEntry)
                whereCondition = " where Product.ModelNo = @ModelNo";
            else
                whereCondition = "where Product.ModelNo = @ModelNo And Product.Id <> @Id";

            int count = 0;
            try
            {
                count = CheckExistence("Product", "Id", whereCondition, lstData);
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
        public DataTable GetProductFromViewList(Hashtable lstData)
        {
            const string whereCondition = "where productCategory not in(select Id from ProductCategory where  CategoryCode='Spair Parts' Or CategoryCode='Gift Product')";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewProduct", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllProductFromViewList(Hashtable lstData)
        {

            string whereCondition = "";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewProduct", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetSpairProductFromViewList(Hashtable lstData)
        {

            string whereCondition = "where productCategory in(select Id from ProductCategory where  CategoryCode='Spair Parts')";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewProduct", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetProductFromViewListByCategoryId(Hashtable lstData)
        {

            string whereCondition = " where ProductCategory = @ProductCategory";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewProduct", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Create by zobayer
        /// Create Date: 01/06/2015
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetProductsByCategory(Hashtable lstData)
        {
            const string whereCondition = " where CategoryCode = @CategoryCode";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewProduct", "*", whereCondition, lstData);
                return dt;
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
        public DataTable GetAllProductbyProductCatId(Hashtable lstData)
        {
            string whereCondition = @"where ProductCategory = @ProductCategory and Id not in (select ProductId from ProductMapping where SupplierId = @SupplierId)";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("Product", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetAllProductbyProductCategoryId(Hashtable lstData)
        {
            string whereCondition = @"where Id in (select ProductId from ProductMapping where SupplierId = @SupplierId) and ProductCategory= @ProductCategory";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("Product", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetAllProductbySupplierId(Hashtable lstData)
        {
            string whereCondition = @"where Id in (select ProductId from ProductMapping where SupplierId = @SupplierId) ;";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("Product", "*", whereCondition, lstData);
                return dt;
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
        public DataTable GetAllProductbyProduct(Hashtable lstData)
        {
            string whereCondition = @"where Id not in (select ProductId from ProductMapping where SupplierId = @SupplierId)";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("Product", "*", whereCondition, lstData);
                return dt;
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
        public DataTable GetAllProductbyProductForList(Hashtable lstData)
        {
            string whereCondition = @"where Id in (select ProductId from ProductMapping where SupplierId = @SupplierId)";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("Product", "*", whereCondition, lstData);
                return dt;
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
        /// <param name="isNewentry"></param>
        /// <returns></returns>
        public int CheckForCodeExist(Hashtable lstData, bool isNewentry)
        {
            string whereCondition = string.Empty;
            whereCondition = isNewentry ? " where ProductCode = @ProductCode " : " where ProductCode = @ProductCode and Id != @Id";

            try
            {
                return CheckExistence("Product", "Id", whereCondition, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetlastProductId()
        {

            try
            {
                int maxId = GetMaximumID("Product", "Id", 0, "");

                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
        public DataTable GetAllProductbyPiDetails(Hashtable lstData)
        {
            string whereCondition = @"Where Id In (select ProductId from PIDetails where MasterId = @MasterId)";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewProduct", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}

}
