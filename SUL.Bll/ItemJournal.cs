using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
    public class ItemJournal : SUL.Bll.Base.ItemJournalBase
    {
        private static SUL.Dal.ItemJournalDal Dal = new SUL.Dal.ItemJournalDal();
        public ItemJournal()
            : base()
        {
        }
        public DataTable GetItemJournalFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetItemJournalFromViewList(lstItems);
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dealerId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable SearchItemJournal(int warehouseId, int productId, int colorId, DateTime startDate, DateTime endDate)
        {
            string conditions = string.Empty;

            Hashtable lstItems = new Hashtable();
            if (warehouseId > 0)
            {
                lstItems.Add("@WareHouseId", warehouseId);
                conditions = " where WareHouseId= @WareHouseId";
            }
            if (productId > 0)
            {
                lstItems.Add("@ProductId", productId);
                if (conditions == string.Empty)
                    conditions = " where ProductId= @ProductId";
                else
                    conditions += " And ProductId= @ProductId";
            }
            if (colorId > 0)
            {
                lstItems.Add("@Color", colorId);
                if (conditions == string.Empty)
                    conditions = "where Color = @Color";
                else
                    conditions += " And Color = @Color";
            }
            if (startDate != DateTime.MinValue)
            {
                DateTime sDate = DateTime.Parse(startDate.ToString("MMM dd, yyyy") + " 00:00:00");
                lstItems.Add("@StartDate", sDate);

                if (endDate != DateTime.MinValue)
                {
                    DateTime eDate = DateTime.Parse(endDate.ToString("MMM dd, yyyy") + " 23:59:59");
                    lstItems.Add("@EndDate", eDate);
                }
                else
                {
                    DateTime eDate = DateTime.Parse(startDate.ToString("MMM dd, yyyy") + " 23:59:59");
                    lstItems.Add("@EndDate", eDate);
                }

                if (conditions == string.Empty)
                    conditions = " where (TransactionDate >= @StartDate and TransactionDate <= @EndDate)";
                else
                    conditions += " And (TransactionDate >= @StartDate and TransactionDate <= @EndDate)";

            }
            conditions += " Order by TransactionDate DESC,Id DESC";
            return dal.SearchItemJournal(lstItems, conditions);
        }
    }
}
