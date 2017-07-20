using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class BankInformation : SUL.Bll.Base.BankInformationBase
	{
		private static SUL.Dal.BankInformationDal Dal = new SUL.Dal.BankInformationDal();
		public BankInformation() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Id"></param>
        /// <param name="supplier"></param>
        /// <returns></returns>
        public BankInformation GetBankInformationBySupplierId(Int32 _Id,string supplier)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@TypeId", _Id);
            lstItems.Add("@Type", supplier);

            DataTable dt = dal.GetBankInformationBySupplierId(lstItems);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                return GetObject(dr);
            }
            else
            {
                return new BankInformation();
            }
            
        }
        public List<BankInformation> GetBankInformationBySupplier(Int32 _Id, string supplier)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@TypeId", _Id);
            lstItems.Add("@Type", supplier);

            DataTable dt = dal.GetBankInformationBySupplierId(lstItems);
            List<BankInformation> BankInformationList = new List<BankInformation>();
            foreach (DataRow dr in dt.Rows)
            {
                BankInformationList.Add(GetObject(dr));
            }
            return BankInformationList;
        }

        public DataTable GetAllViewbankInfo()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetAllViewbankInfo(lstItems);
            return dt;
        }
        public DataTable GetAllViewbankInfoByCompany()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetAllViewbankInfoByCompany(lstItems);
            return dt;
        }
	}
}
