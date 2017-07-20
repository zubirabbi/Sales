using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class CalculationCampaign : SUL.Bll.Base.CalculationCampaignBase
	{
		private static SUL.Dal.CalculationCampaignDal Dal = new SUL.Dal.CalculationCampaignDal();
		public CalculationCampaign() : base()
		{
		}
        public List<CalculationCampaign> GetAllCalculationCampaignByCampaignId(int _cmpId,string _cmpName,int _dealerId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignId", _cmpId);
            lstItems.Add("@CampaignName", _cmpName);
            lstItems.Add("@DealerId", _dealerId);
            DataTable dt = dal.GetAllCalculationCampaignByCampaignId(lstItems);
            List<CalculationCampaign> CalculationCampaignList = new List<CalculationCampaign>();
            foreach (DataRow dr in dt.Rows)
            {
                CalculationCampaignList.Add(GetObject(dr));
            }
            return CalculationCampaignList;
        }
	}
}
