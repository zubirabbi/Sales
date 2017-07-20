using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ItemJournalDetailsDal : SUL.Dal.Base.ItemJournalDetailsDalBase
	{
		public ItemJournalDetailsDal() : base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetItemJournalDetailsByItem(Hashtable lstData)
        {
            string whereCondition = " where ItemJournalDetails.MasterId = @MasterId and ProductId = @ProductId and Color = @Color";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ItemJournalDetails", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
