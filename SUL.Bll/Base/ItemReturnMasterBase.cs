using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ItemReturnMasterBase
	{
		protected static SUL.Dal.ItemReturnMasterDal dal = new SUL.Dal.ItemReturnMasterDal();

		public System.Int32 Id		{ get ; set; }

		public System.DateTime ReturnDate		{ get ; set; }

		public System.String Remarks		{ get ; set; }

		public System.String ReturnCode		{ get ; set; }

		public System.Int32 ReceiveBy		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }

		public System.String Status		{ get ; set; }

		public System.Decimal ItemTotal		{ get ; set; }

		public System.Decimal Charges		{ get ; set; }

		public System.Decimal ReturnTotal		{ get ; set; }

        public System.Int32 DealerId { get; set; }


		public  Int32 InsertItemReturnMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@ReturnDate", ReturnDate.ToString(CultureInfo.InvariantCulture));
			
			lstItems.Add("@Remarks", Remarks);
			lstItems.Add("@ReturnCode", ReturnCode);
			lstItems.Add("@ReceiveBy", ReceiveBy.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Status", Status);
			lstItems.Add("@ItemTotal", ItemTotal.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Charges", Charges.ToString(CultureInfo.InvariantCulture));
			//lstItems.Add("@ReturnTotal", ReturnTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@DealerId", DealerId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertItemReturnMaster(lstItems);
		}

		public  Int32 UpdateItemReturnMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@ReturnDate", ReturnDate.ToString(CultureInfo.InvariantCulture));
			
			lstItems.Add("@Remarks", Remarks);
			lstItems.Add("@ReturnCode", ReturnCode);
			lstItems.Add("@ReceiveBy", ReceiveBy.ToString());
			lstItems.Add("@UserId", UserId.ToString());
			lstItems.Add("@Status", Status);
			lstItems.Add("@ItemTotal", ItemTotal.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Charges", Charges.ToString(CultureInfo.InvariantCulture));
			//lstItems.Add("@ReturnTotal", ReturnTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@DealerId", DealerId.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateItemReturnMaster(lstItems);
		}

		public  Int32 DeleteItemReturnMasterById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteItemReturnMasterById(lstItems);
		}

		public List<ItemReturnMaster> GetAllItemReturnMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllItemReturnMaster(lstItems);
			List<ItemReturnMaster> ItemReturnMasterList = new List<ItemReturnMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				ItemReturnMasterList.Add(GetObject(dr));
			}
			return ItemReturnMasterList;
		}

		public ItemReturnMaster  GetItemReturnMasterById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetItemReturnMasterById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ItemReturnMaster GetObject(DataRow dr)
		{

			ItemReturnMaster objItemReturnMaster = new ItemReturnMaster();
			objItemReturnMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objItemReturnMaster.ReturnDate = (dr["ReturnDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["ReturnDate"];
			
			objItemReturnMaster.Remarks = (dr["Remarks"] == DBNull.Value) ? "" : (String)dr["Remarks"];
			objItemReturnMaster.ReturnCode = (dr["ReturnCode"] == DBNull.Value) ? "" : (String)dr["ReturnCode"];
			objItemReturnMaster.ReceiveBy = (dr["ReceiveBy"] == DBNull.Value) ? 0 : (Int32)dr["ReceiveBy"];
			objItemReturnMaster.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
			objItemReturnMaster.Status = (dr["Status"] == DBNull.Value) ? "" : (String)dr["Status"];
			objItemReturnMaster.ItemTotal = (dr["ItemTotal"] == DBNull.Value) ? 0 : (Decimal)dr["ItemTotal"];
			objItemReturnMaster.Charges = (dr["Charges"] == DBNull.Value) ? 0 : (Decimal)dr["Charges"];
			objItemReturnMaster.ReturnTotal = (dr["ReturnTotal"] == DBNull.Value) ? 0 : (Decimal)dr["ReturnTotal"];
            objItemReturnMaster.DealerId = (dr["DealerId"] == DBNull.Value) ? 0 : (Int32)dr["DealerId"];

			return objItemReturnMaster;
		}
	}
}
