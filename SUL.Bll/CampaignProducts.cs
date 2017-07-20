using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class CampaignProducts : SUL.Bll.Base.CampaignProductsBase
	{
		private static SUL.Dal.CampaignProductsDal Dal = new SUL.Dal.CampaignProductsDal();
		public CampaignProducts() : base()
		{
		}

        public List<CampaignProducts> GetAllProductDetaislbyCampaignId(Int32 campaignId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignId", CampaignId);

            DataTable dt = dal.GetAllProductDetaislbyCampaignId(lstItems);
            List<CampaignProducts> CampaignProductsList = new List<CampaignProducts>();
            foreach (DataRow dr in dt.Rows)
            {
                CampaignProductsList.Add(GetObject(dr));
            }
            return CampaignProductsList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detailsId"></param>
        /// <returns></returns>
        public List<CampaignProducts> GetProductbyDetailsId(Int32 detailsId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignDetailsId", CampaignDetailsId);

            DataTable dt = dal.GetProductbyDetailsId(lstItems);
            List<CampaignProducts> CampaignProductsList = new List<CampaignProducts>();
            foreach (DataRow dr in dt.Rows)
            {
                CampaignProductsList.Add(GetObject(dr));
            }
            return CampaignProductsList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detailsId"></param>
        /// <returns></returns>
        public List<CampaignProducts> GetProductsByDetailsId(Int32 detailsId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignDetailsId", detailsId);

            DataTable dt = dal.GetProductsByDetailsId(lstItems);
            return (from DataRow dr in dt.Rows select GetObject(dr)).ToList();
        }
	}
}
