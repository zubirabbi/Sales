using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class DealerLedger : SUL.Bll.Base.DealerLedgerBase
	{
		private static SUL.Dal.DealerLedgerDal Dal = new SUL.Dal.DealerLedgerDal();
		public DealerLedger() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dealerId"></param>
        /// <returns></returns>
        public DealerLedger  GetDealerLedgerByDealerId(Int32 _dealerId)
		{
			Hashtable lstItems = new Hashtable();
            lstItems.Add("@DealerId", _dealerId);

            DataTable dt = dal.GetDealerLedgerByDealerId(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

        public int GetLedgerIdForTransaction(Int32 _sourceId, string transactionType)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@SourceId", _sourceId);
            lstItems.Add("@TransactionType", transactionType);

            string strId = dal.GetLedgerIdForTransaction(lstItems);
            if (strId != null && strId == string.Empty)
                return 0;
            else
                return int.Parse(strId);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="dealerId"></param>
        /// <param name="date"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetLast3Transaction(Int64 dealerId, DateTime date)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DealerId", dealerId);
            lstItems.Add("@Date", date.ToString("MMM dd, yyyy"));

            DataTable dt = dal.GetLast3Transaction(lstItems);
            
            return dt;
        }

        public DataTable GetLast3TransactionForInvoice(Int64 dealerId, int invoiceId, DateTime date)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DealerId", dealerId);
            lstItems.Add("@Date", date.ToString("MMM dd, yyyy"));

            int ledgerId = new DealerLedger().GetLedgerIdForTransaction(invoiceId, "Invoice");
            lstItems.Add("@Id", ledgerId);
 
            DataTable dt = dal.GetLast3TransactionForInvoice(lstItems);

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetDealerLedgerFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetDealerLedgerFromViewList(lstItems);
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DealerId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable SearchDealerItem(int DealerId, DateTime startDate, DateTime endDate)
        {
            string conditions = string.Empty;

            Hashtable lstItems = new Hashtable();
            if (DealerId > 0)
            {
                lstItems.Add("@DealerId", DealerId);
                conditions = " where DealerId= @DealerId";
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
            conditions += " order by TransactionDate desc ,Id desc";
            return dal.SearchDealerItem(lstItems, conditions);
        }

	    public Decimal checkCreditLimit(int DealerId, decimal SalesAmount, decimal Payment)
	    {
            DealerInformation objDealer=new DealerInformation().GetDealerInformationById(DealerId);
            decimal NewBalance;
            NewBalance = objDealer.Balance - SalesAmount + Payment;
	        return NewBalance;
	    }

	}

}
