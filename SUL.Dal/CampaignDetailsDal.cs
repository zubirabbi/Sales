using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class CampaignDetailsDal : SUL.Dal.Base.CampaignDetailsDalBase
	{
		public CampaignDetailsDal() : base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckNameExistance(Hashtable lstData, bool isNewEntry)
        {
            string whereCondition = (isNewEntry) ? " where CampaignId = @CampaignId and CampaignName = @CampaignName" : " where CampaignId = @CampaignId and CampaignName = @CampaignName and Id <> @Id";

            return CheckExistence("CampaignDetails", "Id", whereCondition, lstData);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetActiveCampaigns(Hashtable lstData)
        {
            const string whereCondition =
                " where CampaignId in(select id from CampaignMaster where  @date >= cast(CONVERT(varchar(8), StartDate, 112) AS datetime) And" +
                                    " @date <= cast(CONVERT(varchar(8), EndDate, 112) AS datetime) and IsActive=@IsActive and CampaignOn=@CampaignOn) ";
             
            try
            {
                DataTable dt = GetDataTable("CampaignDetails", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetAlltDetaislbyCampaignId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("CampaignDetails", "*", " Where CampaignId = @MasterId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetlastCampaignNo(Hashtable lstData)
        {
            try
            {
                string lastCampaignNo = ExecuteScaler("CampaignDetails", "OptionNo", " where Id=(select max(id) from CampaignDetails where CampaignId=@CampaignId)", lstData);

                return lastCampaignNo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
