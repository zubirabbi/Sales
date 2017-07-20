using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class CampaignMaster : SUL.Bll.Base.CampaignMasterBase
	{
		private static SUL.Dal.CampaignMasterDal Dal = new SUL.Dal.CampaignMasterDal();
		public CampaignMaster() : base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaignCode"></param>
        /// <param name="id"></param>
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckCodeExistance(string campaignCode, int id, bool isNewEntry)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignCode", campaignCode);

            if (!isNewEntry)
                lstItems.Add("@Id", id);

            return dal.CheckCodeExistance(lstItems, isNewEntry);
        }

        public int GetMaxCampaignMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxCampaignMasterId(lstItems);
        }
        public DataTable GetCampaignViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetCampaignViewList(lstItems);
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaignOn"></param>
        /// <param name="isActive"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<CampaignMaster> GetAllCampaignByType(string campaignOn, bool isActive, DateTime date)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignOn", campaignOn);
            lstItems.Add("@IsActive", isActive);
            lstItems.Add("@date", date.ToString("MMM dd, yyyy"));

            DataTable dt = dal.GetAllCampaignByType(lstItems);
            return (from DataRow dr in dt.Rows select GetObject(dr)).ToList();
        }
        /// <summary>
        /// Created by Fazle rabby
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetAllCampaignInfoByAdjust()
        {
            Hashtable lstItems = new Hashtable();     
            DataTable dt = dal.GetAllCampaignInfoByAdjust(lstItems);
            return dt;
        }
	}
}
