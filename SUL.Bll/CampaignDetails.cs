using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SUL.Bll.Base;

namespace SUL.Bll
{
    public class CampaignDetails : SUL.Bll.Base.CampaignDetailsBase
    {
        private static SUL.Dal.CampaignDetailsDal Dal = new SUL.Dal.CampaignDetailsDal();
        public CampaignDetails()
            : base()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaignOn"></param>
        /// <param name="isActive"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<CampaignDetails> GetActiveCampaigns(string campaignOn, bool isActive, DateTime date)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@IsActive",isActive);
            lstItems.Add("@CampaignOn", campaignOn);
            lstItems.Add("@date", date.ToString("MMM dd, yyyy"));

            DataTable dt = dal.GetActiveCampaigns(lstItems);
            if (dt.Rows.Count > 0)
            {
                return (from DataRow dr in dt.Rows select GetObject(dr)).ToList();
            }
            else 
                return new List<CampaignDetails>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaignName"></param>
        /// <param name="id"></param>
        /// <param name="isNewEntry"></param>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public int CheckNameExistance(string campaignName, int id, bool isNewEntry, int campaignId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignId", campaignId);
            lstItems.Add("@CampaignName", campaignName);

            if (!isNewEntry)
                lstItems.Add("@Id", id);

            return dal.CheckNameExistance(lstItems, isNewEntry);
        }

        public Int32 GetLastId()
        {
            return dal.GetLastId();
        }
        public List<CampaignDetails> GetAllProductDetaislbyCampaignId(int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", _MasterId);
            DataTable dt = dal.GetAlltDetaislbyCampaignId(lstItems);
            List<CampaignDetails> CampaignDetailsList = new List<CampaignDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                CampaignDetailsList.Add(GetObject(dr));
            }
            return CampaignDetailsList;
        }
        public CampaignDetails GetProductDetaislbyCampaignId(int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", _MasterId);
            DataTable dt = dal.GetAlltDetaislbyCampaignId(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        public string GetlastCampaignNo(int campaignId)
        {
            Hashtable lstData = new Hashtable {{"@CampaignID", campaignId}};

            string lastCampaignNo = dal.GetlastCampaignNo(lstData);

            if (lastCampaignNo == string.Empty)
                return "A";


            int option = (int)(Convert.ToChar(lastCampaignNo));
            option++;
            return Convert.ToChar(option).ToString();

        }
    }
}
