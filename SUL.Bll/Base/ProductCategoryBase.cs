using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ProductCategoryBase
	{
		protected static SUL.Dal.ProductCategoryDal dal = new SUL.Dal.ProductCategoryDal();

		public System.Int32 Id		{ get ; set; }

		public System.String CategoryCode		{ get ; set; }

		public System.String CategoryDescription		{ get ; set; }


		public  Int32 InsertProductCategory()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CategoryCode", CategoryCode);
			lstItems.Add("@CategoryDescription", CategoryDescription);

			return dal.InsertProductCategory(lstItems);
		}

		public  Int32 UpdateProductCategory()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@CategoryCode", CategoryCode);
			lstItems.Add("@CategoryDescription", CategoryDescription);

			return dal.UpdateProductCategory(lstItems);
		}

		public  Int32 DeleteProductCategoryById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteProductCategoryById(lstItems);
		}

		public List<ProductCategory> GetAllProductCategory()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllProductCategory(lstItems);
			List<ProductCategory> ProductCategoryList = new List<ProductCategory>();
			foreach (DataRow dr in dt.Rows)
			{
				ProductCategoryList.Add(GetObject(dr));
			}
			return ProductCategoryList;
		}

		public ProductCategory  GetProductCategoryById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetProductCategoryById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ProductCategory GetObject(DataRow dr)
		{

			ProductCategory objProductCategory = new ProductCategory();
			objProductCategory.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objProductCategory.CategoryCode = (dr["CategoryCode"] == DBNull.Value) ? "" : (String)dr["CategoryCode"];
			objProductCategory.CategoryDescription = (dr["CategoryDescription"] == DBNull.Value) ? "" : (String)dr["CategoryDescription"];

			return objProductCategory;
		}
	}
}
