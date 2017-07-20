using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class CampaignMasterDal : SUL.Dal.Base.CampaignMasterDalBase
	{
		public CampaignMasterDal() : base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetAllCampaignByType(Hashtable lstData)
        {
            string whereCondition = " Where @date >= cast(CONVERT(varchar(8), StartDate, 112) AS datetime) And" +
                                    " @date <= cast(CONVERT(varchar(8), EndDate, 112) AS datetime) and " +
                                    "CampaignOn = @CampaignOn and IsActive = @IsActive Order by CampaignCode;";
            try
            {
                DataTable dt = GetDataTable("CampaignMaster", "*", whereCondition, lstData);
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
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckCodeExistance(Hashtable lstData, bool isNewEntry)
        {
            string whereCondition = (isNewEntry) ? " where CampaignCode = @CampaignCode" : " where CampaignCode = @CampaignCode and Id <> @Id";
            
            return CheckExistence("CampaignMaster", "Id", whereCondition, lstData);
        }

        public int GetMaxCampaignMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("CampaignMaster", "Id", 1, "", lstData);
        }
        public DataTable GetCampaignViewList(Hashtable lstData)
        {

            string whereCondition = " ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewCampaignMaster", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// created by Fazle rabby
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetAllCampaignInfoByAdjust(Hashtable lstData)
        {

            string whereCondition = " Where IsAdjustedAfterEnd = 1";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("CampaignMaster", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
