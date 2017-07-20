using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class CalculationCampaignBase
	{
		protected static SUL.Dal.CalculationCampaignDal dal = new SUL.Dal.CalculationCampaignDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 DealerId		{ get ; set; }

		public System.String DealerName		{ get ; set; }

		public System.Int32 CampaignId		{ get ; set; }

		public System.String CampaignName		{ get ; set; }

		public System.Decimal Amount		{ get ; set; }

		public System.Decimal Discount		{ get ; set; }

		public System.Boolean IsAppiled		{ get ; set; }

		public System.Int32 ApproveBy		{ get ; set; }


		public  Int32 InsertCalculationCampaign()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@DealerId", DealerId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DealerName", DealerName);
			lstItems.Add("@CampaignId", CampaignId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CampaignName", CampaignName);
			lstItems.Add("@Amount", Amount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsAppiled", IsAppiled);
			lstItems.Add("@ApproveBy", ApproveBy.ToString(CultureInfo.InvariantCulture));

			return dal.InsertCalculationCampaign(lstItems);
		}

		public  Int32 UpdateCalculationCampaign()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@DealerId", DealerId.ToString());
			lstItems.Add("@DealerName", DealerName);
			lstItems.Add("@CampaignId", CampaignId.ToString());
			lstItems.Add("@CampaignName", CampaignName);
			lstItems.Add("@Amount", Amount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsAppiled", IsAppiled);
			lstItems.Add("@ApproveBy", ApproveBy.ToString());

			return dal.UpdateCalculationCampaign(lstItems);
		}

		public  Int32 DeleteCalculationCampaignById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteCalculationCampaignById(lstItems);
		}

		public List<CalculationCampaign> GetAllCalculationCampaign()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllCalculationCampaign(lstItems);
			List<CalculationCampaign> CalculationCampaignList = new List<CalculationCampaign>();
			foreach (DataRow dr in dt.Rows)
			{
				CalculationCampaignList.Add(GetObject(dr));
			}
			return CalculationCampaignList;
		}

		public CalculationCampaign  GetCalculationCampaignById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetCalculationCampaignById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  CalculationCampaign GetObject(DataRow dr)
		{

			CalculationCampaign objCalculationCampaign = new CalculationCampaign();
			objCalculationCampaign.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objCalculationCampaign.DealerId = (dr["DealerId"] == DBNull.Value) ? 0 : (Int32)dr["DealerId"];
			objCalculationCampaign.DealerName = (dr["DealerName"] == DBNull.Value) ? "" : (String)dr["DealerName"];
			objCalculationCampaign.CampaignId = (dr["CampaignId"] == DBNull.Value) ? 0 : (Int32)dr["CampaignId"];
			objCalculationCampaign.CampaignName = (dr["CampaignName"] == DBNull.Value) ? "" : (String)dr["CampaignName"];
			objCalculationCampaign.Amount = (dr["Amount"] == DBNull.Value) ? 0 : (Decimal)dr["Amount"];
			objCalculationCampaign.Discount = (dr["Discount"] == DBNull.Value) ? 0 : (Decimal)dr["Discount"];
			objCalculationCampaign.IsAppiled = (dr["IsAppiled"] == DBNull.Value) ? false : (Boolean)dr["IsAppiled"];
			objCalculationCampaign.ApproveBy = (dr["ApproveBy"] == DBNull.Value) ? 0 : (Int32)dr["ApproveBy"];

			return objCalculationCampaign;
		}
	}
}
