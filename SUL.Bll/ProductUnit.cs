using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ProductUnit : SUL.Bll.Base.ProductUnitBase
	{
		private static SUL.Dal.ProductUnitDal Dal = new SUL.Dal.ProductUnitDal();
		public ProductUnit() : base()
		{
		}
        public List<ProductUnit> GetProductUnitByBaseId(Int32 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);

            DataTable dt = dal.GetProductUnitById(lstItems);
            List<ProductUnit> ProductUnitList = new List<ProductUnit>();
            ProductUnitList.Add(GetObject(dt.Rows[0]));
            return ProductUnitList;
        }
	}
}
