using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class LCAmendment : SUL.Bll.Base.LCAmendmentBase
	{
		private static SUL.Dal.LCAmendmentDal Dal = new SUL.Dal.LCAmendmentDal();
		public LCAmendment() : base()
		{
		}

        public List<LCAmendment> GetAllLCAmendmentbyLCIdAndNumber( int LcId,string Anumber)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@LCId",LcId);
            lstItems.Add("@AmendmentNumber", Anumber);
            DataTable dt = dal.GetAllLCAmendmentbyLCIdAndNumber(lstItems);
            List<LCAmendment> LCAmendmentList = new List<LCAmendment>();
            foreach (DataRow dr in dt.Rows)
            {
                LCAmendmentList.Add(GetObject(dr));
            }
            return LCAmendmentList;
        }
        public List<LCAmendment> GetAllLCAmendmentbyLCId(int LcId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@LCId", LcId);
            DataTable dt = dal.GetAllLCAmendmentbyLCId(lstItems);
            List<LCAmendment> LCAmendmentList = new List<LCAmendment>();
            foreach (DataRow dr in dt.Rows)
            {
                LCAmendmentList.Add(GetObject(dr));
            }
            return LCAmendmentList;
        }
	}
}
