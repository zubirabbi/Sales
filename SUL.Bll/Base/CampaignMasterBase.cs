using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Linq;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class CampaignMasterBase
	{
		protected static SUL.Dal.CampaignMasterDal dal = new SUL.Dal.CampaignMasterDal();

		public System.Int64 Id		{ get ; set; }

		public System.DateTime StartDate		{ get ; set; }

        public System.DateTime EndDate { get; set; }

		public System.Int32 RegionId		{ get ; set; }

		public System.String CampaignCode		{ get ; set; }

		public System.String Description		{ get ; set; }

		public System.Boolean IsAdjustedAfterEnd		{ get ; set; }

		public System.Int32 CreatedBy		{ get ; set; }

		public System.DateTime CreateDate		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }

        public System.Boolean IsExcludedfromIncentive { get; set; }

        public System.String CampaignOn { get; set; }


		public  Int32 InsertCampaignMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@StartDate", StartDate.ToString("MMM dd, yyyy"));
            lstItems.Add("@EndDate", EndDate.ToString("MMM dd, yyyy"));
			lstItems.Add("@RegionId", RegionId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CampaignCode", CampaignCode);
			lstItems.Add("@Description", Description);
			lstItems.Add("@IsAdjustedAfterEnd", IsAdjustedAfterEnd);
			lstItems.Add("@CreatedBy", CreatedBy.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CreateDate", CreateDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);
            lstItems.Add("@IsExcludedfromIncentive", IsExcludedfromIncentive);
            lstItems.Add("@CampaignOn", CampaignOn);

			return dal.InsertCampaignMaster(lstItems);
		}

		public  Int32 UpdateCampaignMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@StartDate", StartDate);
			lstItems.Add("@EndDate", EndDate);
			lstItems.Add("@RegionId", RegionId.ToString());
			lstItems.Add("@CampaignCode", CampaignCode);
			lstItems.Add("@Description", Description);
			lstItems.Add("@IsAdjustedAfterEnd", IsAdjustedAfterEnd);
			lstItems.Add("@CreatedBy", CreatedBy.ToString());
			lstItems.Add("@CreateDate", CreateDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);
            lstItems.Add("@IsExcludedfromIncentive", IsExcludedfromIncentive);
            lstItems.Add("@CampaignOn", CampaignOn);

			return dal.UpdateCampaignMaster(lstItems);
		}

		public  Int32 DeleteCampaignMasterById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteCampaignMasterById(lstItems);
		}

		public List<CampaignMaster> GetAllCampaignMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllCampaignMaster(lstItems);
		    return (from DataRow dr in dt.Rows select GetObject(dr)).ToList();
		}
        
        

	    public CampaignMaster GetCampaignMasterById(Int64 id)
	    {
	        Hashtable lstItems = new Hashtable();
	        lstItems.Add("@Id", id);

	        DataTable dt = dal.GetCampaignMasterById(lstItems);
	        if (dt.Rows.Count > 0)
	        {
	            DataRow dr = dt.Rows[0];
	            return GetObject(dr);
	        }
	        else
	        {
	            return new CampaignMaster();
	        }
	    }

	    protected  CampaignMaster GetObject(DataRow dr)
		{

			CampaignMaster objCampaignMaster = new CampaignMaster();
			objCampaignMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
            objCampaignMaster.StartDate = (dr["StartDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["StartDate"];
            objCampaignMaster.EndDate = (dr["EndDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["EndDate"];
			objCampaignMaster.RegionId = (dr["RegionId"] == DBNull.Value) ? 0 : (Int32)dr["RegionId"];
			objCampaignMaster.CampaignCode = (dr["CampaignCode"] == DBNull.Value) ? "" : (String)dr["CampaignCode"];
			objCampaignMaster.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];
			objCampaignMaster.IsAdjustedAfterEnd = (dr["IsAdjustedAfterEnd"] == DBNull.Value) ? false : (Boolean)dr["IsAdjustedAfterEnd"];
			objCampaignMaster.CreatedBy = (dr["CreatedBy"] == DBNull.Value) ? 0 : (Int32)dr["CreatedBy"];
			objCampaignMaster.CreateDate = (dr["CreateDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["CreateDate"];
			objCampaignMaster.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];
            objCampaignMaster.IsExcludedfromIncentive = (dr["IsExcludedfromIncentive"] == DBNull.Value) ? false : (Boolean)dr["IsExcludedfromIncentive"];
            objCampaignMaster.CampaignOn = (dr["CampaignOn"] == DBNull.Value) ? "" : (String)dr["CampaignOn"];

			return objCampaignMaster;
		}
	}
}
