using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ItemJournalMaster : SUL.Bll.Base.ItemJournalMasterBase
	{
		private static SUL.Dal.ItemJournalMasterDal Dal = new SUL.Dal.ItemJournalMasterDal();
		public ItemJournalMaster() : base()
		{
		}
        
        public int GetMaxItemDetailsJournalMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxItemDetailsJournalMasterId(lstItems);
        }

        public DataTable GetItemJournalMasterByRefId(string refType, string refNo)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@TransactionType", refType);
            lstItems.Add("@SourceId", refNo);

            return dal.GetItemJournalMasterByRefId(lstItems);
        }
	}
}
