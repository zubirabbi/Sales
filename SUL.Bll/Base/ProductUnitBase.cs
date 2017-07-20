using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ProductUnitBase
	{
		protected static SUL.Dal.ProductUnitDal dal = new SUL.Dal.ProductUnitDal();

		public System.Int32 Id		{ get ; set; }

		public System.String UnitCode		{ get ; set; }

		public System.String UnitDescription		{ get ; set; }


		public  Int32 InsertProductUnit()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@UnitCode", UnitCode);
			lstItems.Add("@UnitDescription", UnitDescription);

			return dal.InsertProductUnit(lstItems);
		}

		public  Int32 UpdateProductUnit()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@UnitCode", UnitCode);
			lstItems.Add("@UnitDescription", UnitDescription);

			return dal.UpdateProductUnit(lstItems);
		}

		public  Int32 DeleteProductUnitById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteProductUnitById(lstItems);
		}

		public List<ProductUnit> GetAllProductUnit()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllProductUnit(lstItems);
			List<ProductUnit> ProductUnitList = new List<ProductUnit>();
			foreach (DataRow dr in dt.Rows)
			{
				ProductUnitList.Add(GetObject(dr));
			}
			return ProductUnitList;
		}

		public ProductUnit  GetProductUnitById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetProductUnitById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ProductUnit GetObject(DataRow dr)
		{

			ProductUnit objProductUnit = new ProductUnit();
			objProductUnit.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objProductUnit.UnitCode = (dr["UnitCode"] == DBNull.Value) ? "" : (String)dr["UnitCode"];
			objProductUnit.UnitDescription = (dr["UnitDescription"] == DBNull.Value) ? "" : (String)dr["UnitDescription"];

			return objProductUnit;
		}
	}
}
