using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class CalculationCampaignDal : SUL.Dal.Base.CalculationCampaignDalBase
	{
		public CalculationCampaignDal() : base()
		{
		}
        public DataTable GetAllCalculationCampaignByCampaignId(Hashtable lstData)
        {
            string whereCondition = " Where CampaignId = @CampaignId And CampaignName = @CampaignName And DealerId = @DealerId";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("CalculationCampaign", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
