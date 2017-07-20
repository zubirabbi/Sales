using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class DealerJournalDetails : SUL.Bll.Base.DealerJournalDetailsBase
	{
		private static SUL.Dal.DealerJournalDetailsDal Dal = new SUL.Dal.DealerJournalDetailsDal();
		public DealerJournalDetails() : base()
		{
		}
        public List<DealerJournalDetails> GetAllDealerJournalDetailsBymasterId(int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", _MasterId);
            DataTable dt = dal.GetAllDealerJournalDetailsBymasterId(lstItems);
            return (from DataRow dr in dt.Rows select GetObject(dr)).ToList();
        }
        public List<DealerJournalDetails> GetAllBymasterIdDealerId(int dealerId, int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DealerId", dealerId);
            lstItems.Add("@MasterId", _MasterId);
            DataTable dt = dal.GetAllBymasterIdDealerId(lstItems);
            List<DealerJournalDetails> RequisitionDetailsList = new List<DealerJournalDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                RequisitionDetailsList.Add(GetObject(dr));
            }
            return RequisitionDetailsList;
        }
	}
}
