using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class RequisitionDetailsBase
	{
		protected static SUL.Dal.RequisitionDetailsDal dal = new SUL.Dal.RequisitionDetailsDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int64 MasterId		{ get ; set; }

		public System.Int32 CategoryId		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }

		public System.Int32 Quantity		{ get ; set; }

		public System.Decimal Price		{ get ; set; }

		public System.Int32 Unit		{ get ; set; }

		public System.Decimal Discount		{ get ; set; }

		public System.Int32 Color		{ get ; set; }

        public System.Decimal Price2 { get; set; }

        public System.Int64 CampaignDetailsId { get; set; }

        public System.Decimal Discount2 { get; set; }


		public  Int32 InsertRequisitionDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CategoryId", CategoryId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Quantity", Quantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Price", Price.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Unit", Unit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Color", Color.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Price2", Price.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CampaignDetailsId", CampaignDetailsId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Discount2", Discount2.ToString(CultureInfo.InvariantCulture));


			return dal.InsertRequisitionDetails(lstItems);
		}

		public  Int32 UpdateRequisitionDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@CategoryId", CategoryId.ToString());
			lstItems.Add("@ProductId", ProductId.ToString());
			lstItems.Add("@Quantity", Quantity.ToString());
			lstItems.Add("@Price", Price.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Unit", Unit.ToString());
			lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Color", Color.ToString());
            lstItems.Add("@Price2", Price.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CampaignDetailsId", CampaignDetailsId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Discount2", Discount2.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateRequisitionDetails(lstItems);
		}

		public  Int32 DeleteRequisitionDetailsById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteRequisitionDetailsById(lstItems);
		}

		public List<RequisitionDetails> GetAllRequisitionDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllRequisitionDetails(lstItems);
			List<RequisitionDetails> RequisitionDetailsList = new List<RequisitionDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				RequisitionDetailsList.Add(GetObject(dr));
			}
			return RequisitionDetailsList;
		}

		public RequisitionDetails  GetRequisitionDetailsById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetRequisitionDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  RequisitionDetails GetObject(DataRow dr)
		{
			RequisitionDetails objRequisitionDetails = new RequisitionDetails();
			objRequisitionDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objRequisitionDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int64)dr["MasterId"];
			objRequisitionDetails.CategoryId = (dr["CategoryId"] == DBNull.Value) ? 0 : (Int32)dr["CategoryId"];
			objRequisitionDetails.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			objRequisitionDetails.Quantity = (dr["Quantity"] == DBNull.Value) ? 0 : (Int32)dr["Quantity"];
			objRequisitionDetails.Price = (dr["Price"] == DBNull.Value) ? 0 : (Decimal)dr["Price"];
			objRequisitionDetails.Unit = (dr["Unit"] == DBNull.Value) ? 0 : (Int32)dr["Unit"];
			objRequisitionDetails.Discount = (dr["Discount"] == DBNull.Value) ? 0 : (Decimal)dr["Discount"];
			objRequisitionDetails.Color = (dr["Color"] == DBNull.Value) ? 0 : (Int32)dr["Color"];
            objRequisitionDetails.Price2 = (dr["Price2"] == DBNull.Value) ? 0 : (Decimal)dr["Price2"];
            objRequisitionDetails.CampaignDetailsId = (dr["CampaignDetailsId"] == DBNull.Value) ? 0 : (Int32)dr["CampaignDetailsId"];
            objRequisitionDetails.Discount2 = (dr["Discount2"] == DBNull.Value) ? 0 : (Decimal)dr["Discount2"];
            
            return objRequisitionDetails;
		}
	}
}
