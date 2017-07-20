using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ReceiverMasterBase
	{
		protected static SUL.Dal.ReceiverMasterDal dal = new SUL.Dal.ReceiverMasterDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }

		public System.String ReceivingCode		{ get ; set; }

		public System.Int32 VendorId		{ get ; set; }

		public System.Int32 LCId		{ get ; set; }

		public System.Int32 PIId		{ get ; set; }

		public System.String ReceivedBy		{ get ; set; }

		public System.String InvoiceNo		{ get ; set; }

		public System.Int32 WareHouseId		{ get ; set; }

		public System.String IMEIName		{ get ; set; }

		public System.String IMEIUplodePath		{ get ; set; }

		public System.DateTime ReceivingDate		{ get ; set; }

        public System.Boolean IsInvoiceCreated { get; set; }

        public System.Decimal TotalAmount { get; set; }

        public System.Int32 UserId { get; set; }

        public System.String Status { get; set; }

		public  Int32 InsertReceiverMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ReceivingCode", ReceivingCode);
			lstItems.Add("@VendorId", VendorId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@LCId", LCId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@PIId", PIId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ReceivedBy", ReceivedBy);
			lstItems.Add("@InvoiceNo", InvoiceNo);
			lstItems.Add("@WareHouseId", WareHouseId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IMEIName", IMEIName);
			lstItems.Add("@IMEIUplodePath", IMEIUplodePath);
			lstItems.Add("@ReceivingDate", ReceivingDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IsInvoiceCreated", IsInvoiceCreated);
            lstItems.Add("@TotalAmount", TotalAmount.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Status", Status);

			return dal.InsertReceiverMaster(lstItems);
		}

		public  Int32 UpdateReceiverMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@CompanyId", CompanyId.ToString());
			lstItems.Add("@ReceivingCode", ReceivingCode);
			lstItems.Add("@VendorId", VendorId.ToString());
			lstItems.Add("@LCId", LCId.ToString());
			lstItems.Add("@PIId", PIId.ToString());
			lstItems.Add("@ReceivedBy", ReceivedBy);
			lstItems.Add("@InvoiceNo", InvoiceNo);
			lstItems.Add("@WareHouseId", WareHouseId.ToString());
			lstItems.Add("@IMEIName", IMEIName);
			lstItems.Add("@IMEIUplodePath", IMEIUplodePath);
			lstItems.Add("@ReceivingDate", ReceivingDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IsInvoiceCreated", IsInvoiceCreated);
            lstItems.Add("@TotalAmount", TotalAmount.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Status", Status);

			return dal.UpdateReceiverMaster(lstItems);
		}

		public  Int32 DeleteReceiverMasterById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteReceiverMasterById(lstItems);
		}

		public List<ReceiverMaster> GetAllReceiverMaster(int CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
			DataTable dt = dal.GetAllReceiverMaster(lstItems);
			List<ReceiverMaster> ReceiverMasterList = new List<ReceiverMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				ReceiverMasterList.Add(GetObject(dr));
			}
			return ReceiverMasterList;
		}

		public ReceiverMaster GetReceiverMasterById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetReceiverMasterById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ReceiverMaster GetObject(DataRow dr)
		{

			ReceiverMaster objReceiverMaster = new ReceiverMaster();
			objReceiverMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objReceiverMaster.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];
			objReceiverMaster.ReceivingCode = (dr["ReceivingCode"] == DBNull.Value) ? "" : (String)dr["ReceivingCode"];
			objReceiverMaster.VendorId = (dr["VendorId"] == DBNull.Value) ? 0 : (Int32)dr["VendorId"];
			objReceiverMaster.LCId = (dr["LCId"] == DBNull.Value) ? 0 : (Int32)dr["LCId"];
			objReceiverMaster.PIId = (dr["PIId"] == DBNull.Value) ? 0 : (Int32)dr["PIId"];
			objReceiverMaster.ReceivedBy = (dr["ReceivedBy"] == DBNull.Value) ? "" : (String)dr["ReceivedBy"];
			objReceiverMaster.InvoiceNo = (dr["InvoiceNo"] == DBNull.Value) ? "" : (String)dr["InvoiceNo"];
			objReceiverMaster.WareHouseId = (dr["WareHouseId"] == DBNull.Value) ? 0 : (Int32)dr["WareHouseId"];
			objReceiverMaster.IMEIName = (dr["IMEIName"] == DBNull.Value) ? "" : (String)dr["IMEIName"];
			objReceiverMaster.IMEIUplodePath = (dr["IMEIUplodePath"] == DBNull.Value) ? "" : (String)dr["IMEIUplodePath"];
			objReceiverMaster.ReceivingDate = (dr["ReceivingDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["ReceivingDate"];
            objReceiverMaster.IsInvoiceCreated = (dr["IsInvoiceCreated"] == DBNull.Value) ? false : (Boolean)dr["IsInvoiceCreated"];
		    objReceiverMaster.TotalAmount = (dr["TotalAmount"] == DBNull.Value) ? 0 : (Decimal) dr["TotalAmount"];
            objReceiverMaster.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
            objReceiverMaster.Status = (dr["Status"] == DBNull.Value) ? "" : (String)dr["Status"];

			return objReceiverMaster;
		}
	}
}
