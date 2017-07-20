using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class PurchaseOrderMasterBase
	{
		protected static SUL.Dal.PurchaseOrderMasterDal dal = new SUL.Dal.PurchaseOrderMasterDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int32 VendorId		{ get ; set; }

		public System.String VendorName		{ get ; set; }

		public System.String VandorAddress		{ get ; set; }

		public System.String OrderNo		{ get ; set; }

		public System.DateTime OrderDate		{ get ; set; }

		public System.Decimal TotalPrice		{ get ; set; }


		public System.String Status		{ get ; set; }

		public System.Int32 CreatedBy		{ get ; set; }



		public  Int32 InsertPurchaseOrderMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@VendorId", VendorId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@VendorName", VendorName);
			lstItems.Add("@VandorAddress", VandorAddress);
			lstItems.Add("@OrderNo", OrderNo);
			lstItems.Add("@OrderDate", OrderDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TotalPrice", TotalPrice.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Status", Status);
			lstItems.Add("@CreatedBy", CreatedBy.ToString(CultureInfo.InvariantCulture));

			return dal.InsertPurchaseOrderMaster(lstItems);
		}

		public  Int32 UpdatePurchaseOrderMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@VendorId", VendorId.ToString());
			lstItems.Add("@VendorName", VendorName);
			lstItems.Add("@VandorAddress", VandorAddress);
			lstItems.Add("@OrderNo", OrderNo);
			lstItems.Add("@OrderDate", OrderDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TotalPrice", TotalPrice.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Status", Status);
			lstItems.Add("@CreatedBy", CreatedBy.ToString());

			return dal.UpdatePurchaseOrderMaster(lstItems);
		}

		public  Int32 DeletePurchaseOrderMasterById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeletePurchaseOrderMasterById(lstItems);
		}

		public List<PurchaseOrderMaster> GetAllPurchaseOrderMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllPurchaseOrderMaster(lstItems);
			List<PurchaseOrderMaster> PurchaseOrderMasterList = new List<PurchaseOrderMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				PurchaseOrderMasterList.Add(GetObject(dr));
			}
			return PurchaseOrderMasterList;
		}

		public PurchaseOrderMaster  GetPurchaseOrderMasterById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

            DataTable dt = dal.GetPurchaseOrderMasterById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  PurchaseOrderMaster GetObject(DataRow dr)
		{

			PurchaseOrderMaster objPurchaseOrderMaster = new PurchaseOrderMaster();
			objPurchaseOrderMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objPurchaseOrderMaster.VendorId = (dr["VendorId"] == DBNull.Value) ? 0 : (Int32)dr["VendorId"];
			objPurchaseOrderMaster.VendorName = (dr["VendorName"] == DBNull.Value) ? "" : (String)dr["VendorName"];
			objPurchaseOrderMaster.VandorAddress = (dr["VandorAddress"] == DBNull.Value) ? "" : (String)dr["VandorAddress"];
            objPurchaseOrderMaster.OrderNo = (dr["OrderNo"] == DBNull.Value) ? "" : (String)dr["OrderNo"];
			objPurchaseOrderMaster.OrderDate = (dr["OrderDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["OrderDate"];
			objPurchaseOrderMaster.TotalPrice = (dr["TotalPrice"] == DBNull.Value) ? 0 : (Decimal)dr["TotalPrice"];
			objPurchaseOrderMaster.Status = (dr["Status"] == DBNull.Value) ? "" : (String)dr["Status"];
			objPurchaseOrderMaster.CreatedBy = (dr["CreatedBy"] == DBNull.Value) ? 0 : (Int32)dr["CreatedBy"];

			return objPurchaseOrderMaster;
		}
	}
}
