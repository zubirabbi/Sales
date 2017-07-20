using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class CampaignDetailsBase
	{
		protected static SUL.Dal.CampaignDetailsDal dal = new SUL.Dal.CampaignDetailsDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int64 CampaignId		{ get ; set; }

		public System.String CampaignName		{ get ; set; }

		public System.Decimal StartValue		{ get ; set; }

		public System.Decimal EndValue		{ get ; set; }

		public System.Decimal DiscountPcnt		{ get ; set; }

		public System.Decimal OfferAmount		{ get ; set; }


		public  Int32 InsertCampaignDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CampaignId", CampaignId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CampaignName", CampaignName.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@StartValue", StartValue.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@EndValue", EndValue.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DiscountPcnt", DiscountPcnt.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@OfferAmount", OfferAmount.ToString(CultureInfo.InvariantCulture));

			return dal.InsertCampaignDetails(lstItems);
		}

		public  Int32 UpdateCampaignDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@CampaignId", CampaignId.ToString());
			lstItems.Add("@CampaignName", CampaignName.ToString());
			lstItems.Add("@StartValue", StartValue.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@EndValue", EndValue.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DiscountPcnt", DiscountPcnt.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@OfferAmount", OfferAmount.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateCampaignDetails(lstItems);
		}

		public  Int32 DeleteCampaignDetailsById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteCampaignDetailsById(lstItems);
		}

		public List<CampaignDetails> GetAllCampaignDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllCampaignDetails(lstItems);
			List<CampaignDetails> CampaignDetailsList = new List<CampaignDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				CampaignDetailsList.Add(GetObject(dr));
			}
			return CampaignDetailsList;
		}

		public CampaignDetails  GetCampaignDetailsById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetCampaignDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  CampaignDetails GetObject(DataRow dr)
		{

			CampaignDetails objCampaignDetails = new CampaignDetails();
			objCampaignDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objCampaignDetails.CampaignId = (dr["CampaignId"] == DBNull.Value) ? 0 : (Int64)dr["CampaignId"];
			objCampaignDetails.CampaignName = (dr["CampaignName"] == DBNull.Value) ? "" : (String)dr["CampaignName"];
			objCampaignDetails.StartValue = (dr["StartValue"] == DBNull.Value) ? 0 : (Decimal)dr["StartValue"];
			objCampaignDetails.EndValue = (dr["EndValue"] == DBNull.Value) ? 0 : (Decimal)dr["EndValue"];
			objCampaignDetails.DiscountPcnt = (dr["DiscountPcnt"] == DBNull.Value) ? 0 : (Decimal)dr["DiscountPcnt"];
			objCampaignDetails.OfferAmount = (dr["OfferAmount"] == DBNull.Value) ? 0 : (Decimal)dr["OfferAmount"];

			return objCampaignDetails;
		}
	}
}
