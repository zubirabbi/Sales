using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ProductColorBase
	{
		protected static SUL.Dal.ProductColorDal dal = new SUL.Dal.ProductColorDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }

		public System.Int32 Color		{ get ; set; }


		public  Int32 InsertProductColor()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Color", Color.ToString(CultureInfo.InvariantCulture));

			return dal.InsertProductColor(lstItems);
		}

		public  Int32 UpdateProductColor()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@ProductId", ProductId.ToString());
			lstItems.Add("@Color", Color.ToString());

			return dal.UpdateProductColor(lstItems);
		}

		public  Int32 DeleteProductColorById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteProductColorById(lstItems);
		}

		public List<ProductColor> GetAllProductColor()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllProductColor(lstItems);
			List<ProductColor> ProductColorList = new List<ProductColor>();
			foreach (DataRow dr in dt.Rows)
			{
				ProductColorList.Add(GetObject(dr));
			}
			return ProductColorList;
		}

		public ProductColor  GetProductColorById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetProductColorById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ProductColor GetObject(DataRow dr)
		{

			ProductColor objProductColor = new ProductColor();
			objProductColor.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objProductColor.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			objProductColor.Color = (dr["Color"] == DBNull.Value) ? 0 : (Int32)dr["Color"];

			return objProductColor;
		}
	}
}
