using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class RequisitionCampaignsBase
	{
		protected static SUL.Dal.RequisitionCampaignsDal dal = new SUL.Dal.RequisitionCampaignsDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 RequisitionId		{ get ; set; }

		public System.Int32 CampaignDetailsId		{ get ; set; }

		public System.Int32 Quantity		{ get ; set; }


		public  Int32 InsertRequisitionCampaigns()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@RequisitionId", RequisitionId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CampaignDetailsId", CampaignDetailsId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Quantity", Quantity.ToString(CultureInfo.InvariantCulture));

			return dal.InsertRequisitionCampaigns(lstItems);
		}

		public  Int32 UpdateRequisitionCampaigns()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@RequisitionId", RequisitionId.ToString());
			lstItems.Add("@CampaignDetailsId", CampaignDetailsId.ToString());
			lstItems.Add("@Quantity", Quantity.ToString());

			return dal.UpdateRequisitionCampaigns(lstItems);
		}

		public  Int32 DeleteRequisitionCampaignsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteRequisitionCampaignsById(lstItems);
		}

		public List<RequisitionCampaigns> GetAllRequisitionCampaigns()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllRequisitionCampaigns(lstItems);
			List<RequisitionCampaigns> RequisitionCampaignsList = new List<RequisitionCampaigns>();
			foreach (DataRow dr in dt.Rows)
			{
				RequisitionCampaignsList.Add(GetObject(dr));
			}
			return RequisitionCampaignsList;
		}

		public RequisitionCampaigns  GetRequisitionCampaignsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetRequisitionCampaignsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  RequisitionCampaigns GetObject(DataRow dr)
		{

			RequisitionCampaigns objRequisitionCampaigns = new RequisitionCampaigns();
			objRequisitionCampaigns.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objRequisitionCampaigns.RequisitionId = (dr["RequisitionId"] == DBNull.Value) ? 0 : (Int32)dr["RequisitionId"];
			objRequisitionCampaigns.CampaignDetailsId = (dr["CampaignDetailsId"] == DBNull.Value) ? 0 : (Int32)dr["CampaignDetailsId"];
			objRequisitionCampaigns.Quantity = (dr["Quantity"] == DBNull.Value) ? 0 : (Int32)dr["Quantity"];

			return objRequisitionCampaigns;
		}
	}
}
