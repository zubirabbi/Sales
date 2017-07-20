using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class CampaignProductsDal : SUL.Dal.Base.CampaignProductsDalBase
	{
		public CampaignProductsDal() : base()
		{
		}
        
        public DataTable GetAllProductDetaislbyCampaignId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("CampaignProducts", "*", " Where CampaignId = @CampaignId", lstData);
                return dt;
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
        public DataTable GetProductbyDetailsId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("CampaignProducts", "*", " Where CampaignDetailsId = @CampaignDetailsId ", lstData);
                return dt;
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
        public DataTable GetProductsByDetailsId(Hashtable lstData)
        {
            try
            {
                return GetDataTable("CampaignProducts", "*", " Where CampaignDetailsId = @CampaignDetailsId", lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

	}
}
