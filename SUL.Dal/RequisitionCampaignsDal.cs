using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class RequisitionCampaignsDal : SUL.Dal.Base.RequisitionCampaignsDalBase
	{
		public RequisitionCampaignsDal() : base()
		{
		}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public int CheckExistanceByRequisition(Hashtable lstData)
        {
            try
            {
                const string whereCondition = " where RequisitionCampaigns.CampaignDetailsId = @CampaignDetailsId ";
                return CheckExistence("RequisitionCampaigns", "Id", whereCondition, lstData);
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
        public int CheckExistanceByCampaignId(Hashtable lstData)
        {
            try
            {
                const string whereCondition = " where RequisitionCampaigns.CampaignDetailsId in(select Id from CampaignDetails where CampaignId = @CampaignId) ";
                return CheckExistence("RequisitionCampaigns", "Id", whereCondition, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
