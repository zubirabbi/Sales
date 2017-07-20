using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class CampaignOfferProducts : SUL.Bll.Base.CampaignOfferProductsBase
	{
		private static SUL.Dal.CampaignOfferProductsDal Dal = new SUL.Dal.CampaignOfferProductsDal();
		public CampaignOfferProducts() : base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CampaignOfferProducts> GetProductsByDetailsId(Int64 id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignDetailsId", id);

            DataTable dt = dal.GetProductsByDetailsId(lstItems);
            return (from DataRow dr in dt.Rows select GetObject(dr)).ToList();
        }
	}
}
