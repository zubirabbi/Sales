using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class SupplierLedger : SUL.Bll.Base.SupplierLedgerBase
	{
		private static SUL.Dal.SupplierLedgerDal Dal = new SUL.Dal.SupplierLedgerDal();
		public SupplierLedger() : base()
		{
		}
        public DataTable GetSupplierLedgerFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetSupplierLedgerFromViewList(lstItems);
            return dt;
        }
	}
}
