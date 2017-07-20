using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ProductMappingBase
	{
		protected static SUL.Dal.ProductMappingDal dal = new SUL.Dal.ProductMappingDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 SupplierId		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }


		public  Int32 InsertProductMapping()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@SupplierId", SupplierId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertProductMapping(lstItems);
		}

		public  Int32 UpdateProductMapping()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@SupplierId", SupplierId.ToString());
			lstItems.Add("@ProductId", ProductId.ToString());

			return dal.UpdateProductMapping(lstItems);
		}

		public  Int32 DeleteProductMappingById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteProductMappingById(lstItems);
		}

		public List<ProductMapping> GetAllProductMapping()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllProductMapping(lstItems);
			List<ProductMapping> ProductMappingList = new List<ProductMapping>();
			foreach (DataRow dr in dt.Rows)
			{
				ProductMappingList.Add(GetObject(dr));
			}
			return ProductMappingList;
		}

		public ProductMapping  GetProductMappingById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetProductMappingById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ProductMapping GetObject(DataRow dr)
		{

			ProductMapping objProductMapping = new ProductMapping();
			objProductMapping.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objProductMapping.SupplierId = (dr["SupplierId"] == DBNull.Value) ? 0 : (Int32)dr["SupplierId"];
			objProductMapping.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];

			return objProductMapping;
		}
	}
}
