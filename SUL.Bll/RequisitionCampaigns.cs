using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class RequisitionCampaigns : SUL.Bll.Base.RequisitionCampaignsBase
	{
		private static SUL.Dal.RequisitionCampaignsDal Dal = new SUL.Dal.RequisitionCampaignsDal();
		public RequisitionCampaigns() : base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaignDetailsId"></param>
        /// <returns></returns>
        public int CheckExistanceByRequisition(Int32 campaignDetailsId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignDetailsId", CampaignDetailsId);

            return dal.CheckExistanceByRequisition(lstItems);
        }

        public int CheckExistanceByCampaignId(Int32 campaignId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignId", campaignId);

            return dal.CheckExistanceByCampaignId(lstItems);
        }
	}
}
