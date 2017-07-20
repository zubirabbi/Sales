using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class CustomerComplainMasterBase
	{
		protected static SUL.Dal.CustomerComplainMasterDal dal = new SUL.Dal.CustomerComplainMasterDal();

		public System.Int32 Id		{ get ; set; }

		public System.String ComplainCode		{ get ; set; }

		public System.DateTime ComplainDate		{ get ; set; }

		public System.String CustomerName		{ get ; set; }

		public System.String MobileNo		{ get ; set; }

		public System.String Address		{ get ; set; }

		public System.String AlternativeMobile		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }

		public System.String Imei		{ get ; set; }

		public System.DateTime PurchaseDate		{ get ; set; }

		public System.DateTime WarentyExpairDate		{ get ; set; }

		public System.String Description		{ get ; set; }

		public System.String PurchaseReceiptNo		{ get ; set; }

		public System.DateTime DeliveryDate		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }


		public  Int32 InsertCustomerComplainMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@ComplainCode", ComplainCode);
			lstItems.Add("@ComplainDate", ComplainDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CustomerName", CustomerName);
			lstItems.Add("@MobileNo", MobileNo);
			lstItems.Add("@Address", Address);
			lstItems.Add("@AlternativeMobile", AlternativeMobile);
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Imei", Imei);
			lstItems.Add("@PurchaseDate", PurchaseDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@WarentyExpairDate", WarentyExpairDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Description", Description);
			lstItems.Add("@PurchaseReceiptNo", PurchaseReceiptNo);
			lstItems.Add("@DeliveryDate", DeliveryDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertCustomerComplainMaster(lstItems);
		}

		public  Int32 UpdateCustomerComplainMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@ComplainCode", ComplainCode);
			lstItems.Add("@ComplainDate", ComplainDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CustomerName", CustomerName);
			lstItems.Add("@MobileNo", MobileNo);
			lstItems.Add("@Address", Address);
			lstItems.Add("@AlternativeMobile", AlternativeMobile);
			lstItems.Add("@ProductId", ProductId.ToString());
			lstItems.Add("@Imei", Imei);
			lstItems.Add("@PurchaseDate", PurchaseDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@WarentyExpairDate", WarentyExpairDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Description", Description);
			lstItems.Add("@PurchaseReceiptNo", PurchaseReceiptNo);
			lstItems.Add("@DeliveryDate", DeliveryDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UserId", UserId.ToString());

			return dal.UpdateCustomerComplainMaster(lstItems);
		}

		public  Int32 DeleteCustomerComplainMasterById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteCustomerComplainMasterById(lstItems);
		}

		public List<CustomerComplainMaster> GetAllCustomerComplainMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllCustomerComplainMaster(lstItems);
			List<CustomerComplainMaster> CustomerComplainMasterList = new List<CustomerComplainMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				CustomerComplainMasterList.Add(GetObject(dr));
			}
			return CustomerComplainMasterList;
		}

		public CustomerComplainMaster  GetCustomerComplainMasterById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetCustomerComplainMasterById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  CustomerComplainMaster GetObject(DataRow dr)
		{

			CustomerComplainMaster objCustomerComplainMaster = new CustomerComplainMaster();
			objCustomerComplainMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objCustomerComplainMaster.ComplainCode = (dr["ComplainCode"] == DBNull.Value) ? "" : (String)dr["ComplainCode"];
			objCustomerComplainMaster.ComplainDate = (dr["ComplainDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["ComplainDate"];
			objCustomerComplainMaster.CustomerName = (dr["CustomerName"] == DBNull.Value) ? "" : (String)dr["CustomerName"];
			objCustomerComplainMaster.MobileNo = (dr["MobileNo"] == DBNull.Value) ? "" : (String)dr["MobileNo"];
			objCustomerComplainMaster.Address = (dr["Address"] == DBNull.Value) ? "" : (String)dr["Address"];
			objCustomerComplainMaster.AlternativeMobile = (dr["AlternativeMobile"] == DBNull.Value) ? "" : (String)dr["AlternativeMobile"];
			objCustomerComplainMaster.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			objCustomerComplainMaster.Imei = (dr["Imei"] == DBNull.Value) ? "" : (String)dr["Imei"];
			objCustomerComplainMaster.PurchaseDate = (dr["PurchaseDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["PurchaseDate"];
			objCustomerComplainMaster.WarentyExpairDate = (dr["WarentyExpairDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["WarentyExpairDate"];
			objCustomerComplainMaster.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];
			objCustomerComplainMaster.PurchaseReceiptNo = (dr["PurchaseReceiptNo"] == DBNull.Value) ? "" : (String)dr["PurchaseReceiptNo"];
			objCustomerComplainMaster.DeliveryDate = (dr["DeliveryDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["DeliveryDate"];
			objCustomerComplainMaster.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];

			return objCustomerComplainMaster;
		}
	}
}
