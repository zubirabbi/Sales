using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class InvoiceDetailsBase
	{
		protected static SUL.Dal.InvoiceDetailsDal dal = new SUL.Dal.InvoiceDetailsDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 MasterId		{ get ; set; }

		public System.Int32 CategoryId		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }

		public System.Int32 Quantity		{ get ; set; }

		public System.Decimal Price		{ get ; set; }

		public System.Int32 Unit		{ get ; set; }

        public System.Decimal Discount { get; set; }

		public System.Int32 Color		{ get ; set; }

		public System.Decimal Price2		{ get ; set; }

        public System.Decimal ItemTotal { get; set; }

        public System.Decimal ItemTotalAfterDiscount { get; set; }

        public System.Decimal ItemTotal2 { get; set; }

        public System.Decimal ItemTotal2AfterDiscount { get; set; }


		public  Int32 InsertInvoiceDetails()
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
			lstItems.Add("@Price2", Price2.ToString(CultureInfo.InvariantCulture));

			return dal.InsertInvoiceDetails(lstItems);
		}

		public  Int32 UpdateInvoiceDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@CategoryId", CategoryId.ToString());
			lstItems.Add("@ProductId", ProductId.ToString());
			lstItems.Add("@Quantity", Quantity.ToString());
			lstItems.Add("@Price", Price.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Unit", Unit.ToString());
			lstItems.Add("@Discount", Discount.ToString());
			lstItems.Add("@Color", Color.ToString());
			lstItems.Add("@Price2", Price2.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateInvoiceDetails(lstItems);
		}

		public  Int32 DeleteInvoiceDetailsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteInvoiceDetailsById(lstItems);
		}

		public List<InvoiceDetails> GetAllInvoiceDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllInvoiceDetails(lstItems);
			List<InvoiceDetails> InvoiceDetailsList = new List<InvoiceDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				InvoiceDetailsList.Add(GetObject(dr));
			}
			return InvoiceDetailsList;
		}

		public InvoiceDetails  GetInvoiceDetailsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetInvoiceDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  InvoiceDetails GetObject(DataRow dr)
		{

			InvoiceDetails objInvoiceDetails = new InvoiceDetails();
			objInvoiceDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objInvoiceDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int32)dr["MasterId"];
			objInvoiceDetails.CategoryId = (dr["CategoryId"] == DBNull.Value) ? 0 : (Int32)dr["CategoryId"];
			objInvoiceDetails.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			objInvoiceDetails.Quantity = (dr["Quantity"] == DBNull.Value) ? 0 : (Int32)dr["Quantity"];
			objInvoiceDetails.Price = (dr["Price"] == DBNull.Value) ? 0 : (Decimal)dr["Price"];
			objInvoiceDetails.Unit = (dr["Unit"] == DBNull.Value) ? 0 : (Int32)dr["Unit"];
            objInvoiceDetails.Discount = (dr["Discount"] == DBNull.Value) ? 0 : (Decimal)dr["Discount"];
			objInvoiceDetails.Color = (dr["Color"] == DBNull.Value) ? 0 : (Int32)dr["Color"];
			objInvoiceDetails.Price2 = (dr["Price2"] == DBNull.Value) ? 0 : (Decimal)dr["Price2"];
            objInvoiceDetails.ItemTotal = (dr["ItemTotal"] == DBNull.Value) ? 0 : (Decimal)dr["ItemTotal"];
            objInvoiceDetails.ItemTotalAfterDiscount = (dr["ItemTotalAfterDiscount"] == DBNull.Value) ? 0 : (Decimal)dr["ItemTotalAfterDiscount"];
            objInvoiceDetails.Price2 = (dr["ItemTotal2"] == DBNull.Value) ? 0 : (Decimal)dr["ItemTotal2"];
            objInvoiceDetails.Price2 = (dr["ItemTotal2AfterDiscount"] == DBNull.Value) ? 0 : (Decimal)dr["ItemTotal2AfterDiscount"];

			return objInvoiceDetails;
		}
	}
}
