using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ItemReturnDetails : SUL.Bll.Base.ItemReturnDetailsBase
	{
		private static SUL.Dal.ItemReturnDetailsDal Dal = new SUL.Dal.ItemReturnDetailsDal();
		public ItemReturnDetails() : base()
		{
		}
        public List<ItemReturnDetails> GetAllItemReturnDetailsByProductId(int productId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId", productId);
            DataTable dt = dal.GetAllItemReturnDetailsByProductId(lstItems);
            List<ItemReturnDetails> ItemReturnDetailsList = new List<ItemReturnDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                ItemReturnDetailsList.Add(GetObject(dr));
            }
            return ItemReturnDetailsList;
        }
        public List<ItemReturnDetails> GetAllItemReturnDetailsByMasterId(int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", _MasterId);
            DataTable dt = dal.GetAllItemReturnDetailsByMasterId(lstItems);
            List<ItemReturnDetails> ItemReturnDetailsList = new List<ItemReturnDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                ItemReturnDetailsList.Add(GetObject(dr));
            }
            return ItemReturnDetailsList;
        }
	}
}
