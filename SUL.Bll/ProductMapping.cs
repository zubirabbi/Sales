using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ProductMapping : SUL.Bll.Base.ProductMappingBase
	{
		private static SUL.Dal.ProductMappingDal Dal = new SUL.Dal.ProductMappingDal();
		public ProductMapping() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Id"></param>
        /// <returns></returns>
        public int CheckProductIdExistance(int _Id, int suppId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId", _Id);
            lstItems.Add("@SupplierId", suppId);

            return dal.CheckProductIdExistance(lstItems);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Int32 DeleteProductMappingBySupplierIdAndProductId(Int32 _supplierId, string _ids)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@SupplierId", _supplierId);
            //lstItems.Add("@Ids", _ids);

            return dal.DeleteProductMappingBySupplierIdAndProductId(lstItems,_ids);
        }
        public List<ProductMapping> GetAllProductMappingBySupplierId(Int32 _supplierId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@SupplierId",_supplierId);
            DataTable dt = dal.GetAllProductMappingBySupplierId(lstItems);
            List<ProductMapping> ProductMappingList = new List<ProductMapping>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductMappingList.Add(GetObject(dr));
            }
            return ProductMappingList;
        }
	}
}
