using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ProductColor : SUL.Bll.Base.ProductColorBase
	{
		private static SUL.Dal.ProductColorDal Dal = new SUL.Dal.ProductColorDal();
		public ProductColor() : base()
		{
		}
        public int CheckColorIdExistance(int _Id, int productId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ColorId", _Id);
            lstItems.Add("@ProductId", productId);

            return dal.CheckColorIdExistance(lstItems);
        }
        public List<ProductColor> GetAllProductColorbyProductid(int productId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId",productId);
            DataTable dt = dal.GetAllProductColorbyProductid(lstItems);
            List<ProductColor> ProductColorList = new List<ProductColor>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductColorList.Add(GetObject(dr));
            }
            return ProductColorList;
        }

        public DataTable GetAllVewProductColorbyProductid(int productId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId", productId);
            DataTable dt = dal.GetAllVewProductColorbyProductid(lstItems);
            
            return dt;
        }
        public Int32 DeleteProductColorByProductIdId(Int32 Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId", Id);

            return dal.DeleteProductColorByProductIdId(lstItems);
        }
        public Int32 DeleteProductColorByProductIdColorId(Int32 Id,int colorId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId", Id);
            lstItems.Add("@Color", colorId);

            return dal.DeleteProductColorByProductIdId(lstItems);
        }
        public Int32 DeleteProductMappingByColorIdAndProductId(Int32 ProductId, string _ids)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId", ProductId);
            //lstItems.Add("@Ids", _ids);

            return dal.DeleteProductMappingByColorIdAndProductId(lstItems, _ids);
        }
	}
}
