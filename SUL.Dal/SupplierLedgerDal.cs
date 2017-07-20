using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class SupplierLedgerDal : SUL.Dal.Base.SupplierLedgerDalBase
	{
		public SupplierLedgerDal() : base()
		{
		}
        public DataTable GetSupplierLedgerFromViewList(Hashtable lstData)
        {

            string whereCondition = " order by TransactionDate desc ,Id desc";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewSupplyLedger", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
