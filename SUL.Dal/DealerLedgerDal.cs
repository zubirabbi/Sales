using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class DealerLedgerDal : SUL.Dal.Base.DealerLedgerDalBase
	{
		public DealerLedgerDal() : base()
		{

		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetDealerLedgerByDealerId(Hashtable lstData)
        {
            string whereCondition = " where DealerId.Id = @DealerId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("DealerLedger", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public string GetLedgerIdForTransaction(Hashtable lstData)
        {
            string whereCondition = " where TransactionType = @TransactionType and sourceId =  @sourceId";

            try
            {
                return ExecuteScaler("DealerLedger", "id", whereCondition, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetLast3TransactionForInvoice(Hashtable lstData)
        {
            string whereCondition = " where DealerId = @DealerId and CONVERT(date,TransactionDate)<= @Date and Id<=@Id Order by TransactionDate desc, id desc";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewDealerLedger", " Top 3 vewDealerLedger.* ", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetLast3Transaction(Hashtable lstData)
        {
            string whereCondition = " where DealerId = @DealerId and cast(CONVERT(varchar(8), TransactionDate, 112) AS datetime)<= @Date Order by TransactionDate desc, id desc";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewDealerLedger", " Top 3 vewDealerLedger.* ", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetDealerLedgerFromViewList(Hashtable lstData)
        {

            string whereCondition = " order by TransactionDate desc ,Id desc";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewDealerLedger", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable SearchDealerItem(Hashtable lstData, string whereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewDealerLedger", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
