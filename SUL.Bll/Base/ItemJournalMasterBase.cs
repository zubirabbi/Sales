using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ItemJournalMasterBase
	{
		protected static SUL.Dal.ItemJournalMasterDal dal = new SUL.Dal.ItemJournalMasterDal();

		public System.Int64 Id		{ get ; set; }

		public System.DateTime TransactionDate		{ get ; set; }

		public System.String TransactionType		{ get ; set; }

		public System.String SourceId		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }

		public System.Byte[] LastModifiedDate		{ get ; set; }

		public System.Int64 WareHouseId		{ get ; set; }

		public System.Int64 WareHouseIdFrom		{ get ; set; }


		public  Int32 InsertItemJournalMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@TransactionDate", TransactionDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TransactionType", TransactionType);
			lstItems.Add("@SourceId", SourceId);
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@WareHouseId", WareHouseId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@WareHouseIdFrom", WareHouseIdFrom.ToString(CultureInfo.InvariantCulture));

			return dal.InsertItemJournalMaster(lstItems);
		}

		public  Int32 UpdateItemJournalMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@TransactionDate", TransactionDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TransactionType", TransactionType);
			lstItems.Add("@SourceId", SourceId);
			lstItems.Add("@UserId", UserId.ToString());
			lstItems.Add("@WareHouseId", WareHouseId.ToString());
			lstItems.Add("@WareHouseIdFrom", WareHouseIdFrom.ToString());

			return dal.UpdateItemJournalMaster(lstItems);
		}

		public  Int32 DeleteItemJournalMasterById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteItemJournalMasterById(lstItems);
		}

		public List<ItemJournalMaster> GetAllItemJournalMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllItemJournalMaster(lstItems);
			List<ItemJournalMaster> ItemJournalMasterList = new List<ItemJournalMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				ItemJournalMasterList.Add(GetObject(dr));
			}
			return ItemJournalMasterList;
		}

		public ItemJournalMaster  GetItemJournalMasterById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetItemJournalMasterById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ItemJournalMaster GetObject(DataRow dr)
		{

			ItemJournalMaster objItemJournalMaster = new ItemJournalMaster();
			objItemJournalMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objItemJournalMaster.TransactionDate = (dr["TransactionDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["TransactionDate"];
			objItemJournalMaster.TransactionType = (dr["TransactionType"] == DBNull.Value) ? "" : (String)dr["TransactionType"];
			objItemJournalMaster.SourceId = (dr["SourceId"] == DBNull.Value) ? "" : (String)dr["SourceId"];
			objItemJournalMaster.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
			objItemJournalMaster.WareHouseId = (dr["WareHouseId"] == DBNull.Value) ? 0 : (Int64)dr["WareHouseId"];
			objItemJournalMaster.WareHouseIdFrom = (dr["WareHouseIdFrom"] == DBNull.Value) ? 0 : (Int64)dr["WareHouseIdFrom"];

			return objItemJournalMaster;
		}
	}
}
