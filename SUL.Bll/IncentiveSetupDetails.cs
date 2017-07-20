using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class IncentiveSetupDetails : SUL.Bll.Base.IncentiveSetupDetailsBase
	{
		private static SUL.Dal.IncentiveSetupDetailsDal Dal = new SUL.Dal.IncentiveSetupDetailsDal();
		public IncentiveSetupDetails() : base()
		{
		}
        public List<IncentiveSetupDetails> GetIncentiveSetupDetailsByMasterId(Int32 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);

            DataTable dt = dal.GetIncentiveSetupDetailsByMasterId(lstItems);
            List<IncentiveSetupDetails> IncentiveSetupDetailsList = new List<IncentiveSetupDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                IncentiveSetupDetailsList.Add(GetObject(dr));
            }
            return IncentiveSetupDetailsList;
        }


        public List<IncentiveSetupDetails> GetIncentiveDetailsForDealeer()
        {
            DataTable dt = dal.GetIncentiveDetailsForDealer();
            return (from DataRow dr in dt.Rows select GetObject(dr)).ToList();
        }

        public List<IncentiveSetupDetails> GetIncentiveDetailsForDesignation(int designationId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DesignationId", designationId);

            DataTable dt = dal.GetIncentiveDetailsForDesignation(lstItems);
            return (from DataRow dr in dt.Rows select GetObject(dr)).ToList();
        }

        public List<IncentiveSetupDetails> GetIncentiveDetailsForDesignationAndApplyOn(int designationId,string ApplyOn)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DesignationId", designationId);
            lstItems.Add("@ApplyOn", ApplyOn);
            DataTable dt = dal.GetIncentiveDetailsForDesignation(lstItems);
            return (from DataRow dr in dt.Rows select GetObject(dr)).ToList();
        }
        public int GetlastLastSlno(int MasterId)
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId",MasterId);
            id = dal.GetlastLastSlno(lstItems);

            id = id + 1;
            int maxIdS = id;

            return maxIdS;


        }
	}
}
