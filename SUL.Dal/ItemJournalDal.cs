using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ItemJournalDal : SUL.Dal.Base.ItemJournalDalBase
	{
		public ItemJournalDal() : base()
		{
		}

        public DataTable GetItemJournalFromViewList(Hashtable lstData)
        {

            string whereCondition = " Order by TransactionDate DESC,Id DESC";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewItemJournal", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable SearchItemJournal(Hashtable lstData, string whereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewItemJournal", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
