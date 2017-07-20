using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class InvoiceMasterBase
	{
		protected static SUL.Dal.InvoiceMasterDal dal = new SUL.Dal.InvoiceMasterDal();

		public System.Int32 Id		{ get ; set; }

		public System.String InvoiceNo		{ get ; set; }

		public System.DateTime InvoiceDate		{ get ; set; }

		public System.Int32 DealerId		{ get ; set; }

		public System.Int32 RequisitionId		{ get ; set; }

        public System.Decimal ItemTotal { get; set; }

        public System.Decimal Discount { get; set; }

        public System.Decimal InvoiceTotal { get; set; }

        public System.Decimal ItemTotal2 { get; set; }

        public System.Decimal InvoiceTotal2 { get; set; }

        public System.Int32 UserId { get; set; }

		public  Int32 InsertInvoiceMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@InvoiceNo", InvoiceNo);
			lstItems.Add("@InvoiceDate", InvoiceDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DealerId", DealerId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@RequisitionId", RequisitionId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ItemTotal", ItemTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ItemTotal2", ItemTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertInvoiceMaster(lstItems);
		}

		public  Int32 UpdateInvoiceMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@InvoiceNo", InvoiceNo);
			lstItems.Add("@InvoiceDate", InvoiceDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DealerId", DealerId.ToString());
			lstItems.Add("@RequisitionId", RequisitionId.ToString());
            lstItems.Add("@ItemTotal", ItemTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ItemTotal2", ItemTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateInvoiceMaster(lstItems);
		}

		public  Int32 DeleteInvoiceMasterById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteInvoiceMasterById(lstItems);
		}

		public List<InvoiceMaster> GetAllInvoiceMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllInvoiceMaster(lstItems);
			List<InvoiceMaster> InvoiceMasterList = new List<InvoiceMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				InvoiceMasterList.Add(GetObject(dr));
			}
			return InvoiceMasterList;
		}

		public InvoiceMaster  GetInvoiceMasterById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetInvoiceMasterById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  InvoiceMaster GetObject(DataRow dr)
		{

			InvoiceMaster objInvoiceMaster = new InvoiceMaster();
			objInvoiceMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objInvoiceMaster.InvoiceNo = (dr["InvoiceNo"] == DBNull.Value) ? "" : (String)dr["InvoiceNo"];
			objInvoiceMaster.InvoiceDate = (dr["InvoiceDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["InvoiceDate"];
			objInvoiceMaster.DealerId = (dr["DealerId"] == DBNull.Value) ? 0 : (Int32)dr["DealerId"];
			objInvoiceMaster.RequisitionId = (dr["RequisitionId"] == DBNull.Value) ? 0 : (Int32)dr["RequisitionId"];
            objInvoiceMaster.ItemTotal = (dr["ItemTotal"] == DBNull.Value) ? 0 : (Decimal)dr["ItemTotal"];
            objInvoiceMaster.Discount = (dr["Discount"] == DBNull.Value) ? 0 : (Decimal)dr["Discount"];
            objInvoiceMaster.InvoiceTotal = (dr["InvoiceTotal"] == DBNull.Value) ? 0 : (Decimal)dr["InvoiceTotal"];
            objInvoiceMaster.ItemTotal2 = (dr["ItemTotal2"] == DBNull.Value) ? 0 : (Decimal)dr["ItemTotal2"];
            objInvoiceMaster.InvoiceTotal2 = (dr["InvoiceTotal2"] == DBNull.Value) ? 0 : (Decimal)dr["InvoiceTotal2"];
            objInvoiceMaster.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];

			return objInvoiceMaster;
		}
	}
}
