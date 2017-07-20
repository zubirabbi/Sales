using System;
using System.Globalization;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class Supplier : SUL.Bll.Base.SupplierBase
	{
		private static SUL.Dal.SupplierDal Dal = new SUL.Dal.SupplierDal();
		public Supplier() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Id"></param>
        /// <param name="_SupplierCode"></param>
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckSupplierExistance(int _Id, string _SupplierCode, bool isNewEntry)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Code", _SupplierCode);

            if (!isNewEntry) lstItems.Add("@Id", _Id);

            return dal.CheckSupplierExistance(lstItems, isNewEntry);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetlastSupplier()
        {
            Hashtable lstItems = new Hashtable();
            //lstItems.Add("@EmployeeId", _EmployeeId);

            return dal.GetlastSupplier(lstItems);
        }
        public int CheckForCodeExist(string _code, bool isNewEntry, int _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Code", _code);

            if (!isNewEntry)
                lstItems.Add("@Id", _Id);

            return dal.CheckForCodeExist(lstItems, isNewEntry);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public Int32 UpdateSupplierInformationfordealerLedger()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@TotalDebit", TotalDebit.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@TotalCredit", TotalCredit.ToString(CultureInfo.InvariantCulture));

            return dal.UpdateSupplierInformationfordealerLedger(lstItems);
        }
        public DataTable GetSupplierFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetSupplierFromViewList(lstItems);
            return dt;
        }
	}
}
