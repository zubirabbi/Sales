using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ItemJournalMasterDal : SUL.Dal.Base.ItemJournalMasterDalBase
	{
		public ItemJournalMasterDal() : base()
		{
		}
        public int GetMaxItemDetailsJournalMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("ItemJournalMaster", "Id", 1, "", lstData);
        }

        public DataTable GetItemJournalMasterByRefId(Hashtable lstData)
        {
            string whereCondition = " where TransactionType = @TransactionType and SourceId = @SourceId";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ItemJournalMaster", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
