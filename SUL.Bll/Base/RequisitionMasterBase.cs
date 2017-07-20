using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class RequisitionMasterBase
	{
		protected static SUL.Dal.RequisitionMasterDal dal = new SUL.Dal.RequisitionMasterDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int64 DealerId		{ get ; set; }

		public System.String Address		{ get ; set; }

		public System.String Courier		{ get ; set; }

		public System.String RequisitionCode		{ get ; set; }

		public System.DateTime RequisitionDate		{ get ; set; }

		public System.Int32 CSId		{ get ; set; }

	    public System.String Status { get; set; }

	    public System.Boolean IsInvoiceCreated { get; set; }

        public System.Int32 JrCSId { get; set; }

        public System.Decimal ItemTotal { get; set; }

        public System.Decimal ItemTotal2 { get; set; }

        public System.Decimal Discount { get; set; }

        public System.Decimal RequistionTotal { get; set; }

        public System.Decimal RequistionTotal2 { get; set; }

        public System.Int32 UserId { get; set; }

        public System.DateTime CencelDate { get; set; }

        public System.Int32 CencelBy { get; set; }

	    public System.String CencelNote { get; set; }

        public System.Decimal LastBalance { get; set; }

        public System.String Remarks { get; set; }

        public System.Int32 CampaignId { get; set; }

        public System.Int32 UpdateBy { get; set; }

	    public  Int32 InsertRequisitionMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@DealerId", DealerId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Address", Address);
			lstItems.Add("@Courier", Courier);
			lstItems.Add("@RequisitionCode", RequisitionCode);
			lstItems.Add("@RequisitionDate", RequisitionDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CSId", CSId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Status", Status);
            lstItems.Add("@IsInvoiceCreated", IsInvoiceCreated);
            lstItems.Add("@JrCSId", JrCSId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ItemTotal", ItemTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ItemTotal2", ItemTotal2.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CencelDate", CencelDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CencelBy", CencelBy.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CencelNote", CencelNote);
            lstItems.Add("@LastBalance", LastBalance);
	        lstItems.Add("@Remarks", Remarks);
            lstItems.Add("@CampaignId", CampaignId.ToString(CultureInfo.InvariantCulture));
           
            lstItems.Add("@UpdateBy", UpdateBy.ToString(CultureInfo.InvariantCulture));

			return dal.InsertRequisitionMaster(lstItems);
		}

		public  Int32 UpdateRequisitionMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@DealerId", DealerId.ToString());
			lstItems.Add("@Address", Address);
			lstItems.Add("@Courier", Courier);
			lstItems.Add("@RequisitionCode", RequisitionCode);
			lstItems.Add("@RequisitionDate", RequisitionDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CSId", CSId.ToString());
            lstItems.Add("@Status", Status);
            lstItems.Add("@IsInvoiceCreated", IsInvoiceCreated);
            lstItems.Add("@JrCSId", JrCSId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ItemTotal", ItemTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ItemTotal2", ItemTotal2.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CencelDate", CencelDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CencelBy", CencelBy.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CencelNote", CencelNote);
            lstItems.Add("@LastBalance", LastBalance);
            lstItems.Add("@Remarks", Remarks);
            lstItems.Add("@CampaignId", CampaignId.ToString(CultureInfo.InvariantCulture));
           
            lstItems.Add("@UpdateBy", UpdateBy.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateRequisitionMaster(lstItems);
		}

		public  Int32 DeleteRequisitionMasterById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteRequisitionMasterById(lstItems);
		}

		public List<RequisitionMaster> GetAllRequisitionMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllRequisitionMaster(lstItems);
			List<RequisitionMaster> RequisitionMasterList = new List<RequisitionMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				RequisitionMasterList.Add(GetObject(dr));
			}
			return RequisitionMasterList;
		}

		public RequisitionMaster  GetRequisitionMasterById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetRequisitionMasterById(lstItems);
		    if (dt.Rows.Count <= 0)
		        return new RequisitionMaster();
		    else
		    {

		        DataRow dr = dt.Rows[0];
		        return GetObject(dr);
		    }
		}

		protected  RequisitionMaster GetObject(DataRow dr)
		{

			RequisitionMaster objRequisitionMaster = new RequisitionMaster();
			objRequisitionMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objRequisitionMaster.DealerId = (dr["DealerId"] == DBNull.Value) ? 0 : (Int64)dr["DealerId"];
			objRequisitionMaster.Address = (dr["Address"] == DBNull.Value) ? "" : (String)dr["Address"];
			objRequisitionMaster.Courier = (dr["Courier"] == DBNull.Value) ? "" : (String)dr["Courier"];
			objRequisitionMaster.RequisitionCode = (dr["RequisitionCode"] == DBNull.Value) ? "" : (String)dr["RequisitionCode"];
			objRequisitionMaster.RequisitionDate = (dr["RequisitionDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["RequisitionDate"];
			objRequisitionMaster.CSId = (dr["CSId"] == DBNull.Value) ? 0 : (Int32)dr["CSId"];
            objRequisitionMaster.Status = (dr["Status"] == DBNull.Value) ? "" : (String)dr["Status"];
            objRequisitionMaster.IsInvoiceCreated = (dr["IsInvoiceCreated"] == DBNull.Value) ? false : (Boolean)dr["IsInvoiceCreated"];
            objRequisitionMaster.JrCSId = (dr["JrCSId"] == DBNull.Value) ? 0 : (Int32)dr["JrCSId"];
            objRequisitionMaster.ItemTotal = (dr["ItemTotal"] == DBNull.Value) ? 0 : (Decimal)dr["ItemTotal"];
            objRequisitionMaster.ItemTotal2 = (dr["ItemTotal2"] == DBNull.Value) ? 0 : (Decimal)dr["ItemTotal2"];
            objRequisitionMaster.Discount = (dr["Discount"] == DBNull.Value) ? 0 : (Decimal)dr["Discount"];
            objRequisitionMaster.RequistionTotal = (dr["RequistionTotal"] == DBNull.Value) ? 0 : (Decimal)dr["RequistionTotal"];
            objRequisitionMaster.RequistionTotal2 = (dr["RequistionTotal2"] == DBNull.Value) ? 0 : (Decimal)dr["RequistionTotal2"];
            objRequisitionMaster.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
            objRequisitionMaster.CencelDate = (dr["CencelDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["CencelDate"];
            objRequisitionMaster.CencelBy = (dr["CencelBy"] == DBNull.Value) ? 0 : (Int32)dr["CencelBy"];
            objRequisitionMaster.CencelNote = (dr["CencelNote"] == DBNull.Value) ? "" : (String)dr["CencelNote"];
            objRequisitionMaster.LastBalance = (dr["LastBalance"] == DBNull.Value) ? 0 : (Decimal)dr["LastBalance"];
            objRequisitionMaster.Remarks = (dr["Remarks"] == DBNull.Value) ? "" : (String)dr["Remarks"];
            objRequisitionMaster.CampaignId = (dr["CampaignId"] == DBNull.Value) ? 0 : (Int32)dr["CampaignId"];
            objRequisitionMaster.UpdateBy = (dr["UpdateBy"] == DBNull.Value) ? 0 : (Int32)dr["UpdateBy"];
			return objRequisitionMaster;
		}
	}
}
