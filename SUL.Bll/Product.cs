using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class Product : SUL.Bll.Base.ProductBase
	{
		private static SUL.Dal.ProductDal Dal = new SUL.Dal.ProductDal();

        public System.String ProductInfo { get; set; }

        public System.String Unit { get; set; }


		public Product() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Id"></param>
        /// <param name="_productCode"></param>
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckProductCodeExistance(int _Id, string _productCode, bool isNewEntry)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductCode", _productCode);

            if (!isNewEntry) lstItems.Add("@Id", _Id);

            return dal.CheckProductCodeExistance(lstItems, isNewEntry);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Id"></param>
        /// <param name="_productName"></param>
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckProductNameExistance(int _Id, string _productName, bool isNewEntry)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductName", _productName);

            if (!isNewEntry) lstItems.Add("@Id", _Id);

            return dal.CheckProductNameExistance(lstItems, isNewEntry);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Id"></param>
        /// <param name="_modelNo"></param>
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckProductModelExistance(int _Id, string _modelNo, bool isNewEntry)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ModelNo", _modelNo);

            if (!isNewEntry) lstItems.Add("@Id", _Id);

            return dal.CheckProductModelExistance(lstItems, isNewEntry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetProductFromViewList(lstItems);
            return dt;
        }

        public DataTable GetAllProductFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetAllProductFromViewList(lstItems);
            return dt;
        }

        /// <summary>
        /// Created by zobayer on 10/06/2015
        /// </summary>
        /// <returns></returns>
	    public List<Product> GetAllProductList()
	    {
            List<Product> productList =new List<Product>();
            DataTable dt = dal.GetAllProductFromViewList(new Hashtable());
	        if (dt.Rows.Count > 0)
	        {
	            foreach (DataRow row in dt.Rows)
	            {
	                Product product = GetObject(row);

	                product.ProductInfo = row["ProInfo"].ToString();
                    product.Unit = row["UnitCode"].ToString();

                    productList.Add(product);
	            }
	        }

	        return productList;
	    }


	    public DataTable GetSpairProductFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetSpairProductFromViewList(lstItems);
            return dt;
        }
        public DataTable GetProductByMasterIdFromViewList(int MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId",MasterId);

            DataTable dt = dal.GetAllProductbyPiDetails(lstItems);
            return dt;
        }
        /// <summary>
        /// Create by zobayer
        /// Create Date: 01/06/2015
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public DataTable GetProductsByCategory(string category)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CategoryCode", category);

            DataTable dt = dal.GetProductsByCategory(lstItems);
            return dt;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_catId"></param>
        /// <returns></returns>
        public DataTable GetProductFromViewListByCategoryId(int _catId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductCategory",_catId);
            DataTable dt = dal.GetProductFromViewListByCategoryId(lstItems);
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ProductCatId"></param>
        /// <param name="_SupplierId"></param>
        /// <returns></returns>
        public List<Product> GetAllProductbyProductCatId( int _ProductCatId, int _SupplierId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductCategory",_ProductCatId);
            lstItems.Add("@SupplierId",_SupplierId);
            DataTable dt = dal.GetAllProductbyProductCatId(lstItems);
            List<Product> ProductList = new List<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(GetObject(dr));
            }
            return ProductList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ProductCatId"></param>
        /// <returns></returns>
        public List<Product> GetAllProductbyProductCategoryId(int _ProductCatId, int _SupplierId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductCategory", _ProductCatId);
            lstItems.Add("@SupplierId", _SupplierId);
            DataTable dt = dal.GetAllProductbyProductCategoryId(lstItems);
            List<Product> ProductList = new List<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(GetObject(dr));
            }
            return ProductList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ProductCatId"></param>
        /// <returns></returns>
        public List<Product> GetAllProductbySupplierId(int _SupplierId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@SupplierId", _SupplierId);
            DataTable dt = dal.GetAllProductbySupplierId(lstItems);
            List<Product> ProductList = new List<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(GetObject(dr));
            }
            return ProductList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_SupplierId"></param>
        /// <returns></returns>
        public List<Product> GetAllProductbyProduct( int _SupplierId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@SupplierId", _SupplierId);
            DataTable dt = dal.GetAllProductbyProduct(lstItems);
            List<Product> ProductList = new List<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(GetObject(dr));
            }
            return ProductList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_SupplierId"></param>
        /// <returns></returns>
        public List<Product> GetAllProductbyProductForList(int _SupplierId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@SupplierId", _SupplierId);
            DataTable dt = dal.GetAllProductbyProductForList(lstItems);
            List<Product> ProductList = new List<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(GetObject(dr));
            }
            return ProductList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_code"></param>
        /// <param name="isNewEntry"></param>
        /// <param name="_Id"></param>
        /// <returns></returns>
        public int CheckForCodeExist(string _code, bool isNewEntry, int _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductCode", _code);

            if (!isNewEntry)
                lstItems.Add("@Id", _Id);

            return dal.CheckForCodeExist(lstItems, isNewEntry);
        }
        public int GetlastProductId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetlastProductId();
        }

        public List<Product> GetAllProductbyPiDetails( int _PimasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", _PimasterId);
            DataTable dt = dal.GetAllProductbyPiDetails(lstItems);
            List<Product> ProductList = new List<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(GetObject(dr));
            }
            return ProductList;
        }
	}
}
