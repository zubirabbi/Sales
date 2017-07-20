using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class CampaignOfferProductsDal : SUL.Dal.Base.CampaignOfferProductsDalBase
	{
		public CampaignOfferProductsDal() : base()
		{
		}

        public DataTable GetProductsByDetailsId(Hashtable lstData)
        {
            const string whereCondition = " where CampaignDetailsId = @CampaignDetailsId";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("CampaignOfferProducts", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
