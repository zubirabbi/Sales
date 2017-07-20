using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ItemJournalDetails : SUL.Bll.Base.ItemJournalDetailsBase
	{
		private static SUL.Dal.ItemJournalDetailsDal Dal = new SUL.Dal.ItemJournalDetailsDal();
		public ItemJournalDetails() : base()
		{
		}

        public ItemJournalDetails GetItemJournalDetailsByItem(int masterId, int productId, int color)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", masterId);
            lstItems.Add("@ProductId", productId);
            lstItems.Add("@Color", color);

            DataTable dt = dal.GetItemJournalDetailsByItem(lstItems);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                return GetObject(dr);
            }
            else
            {
                return new ItemJournalDetails();
            }
        }
	}
}
