using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ItemJournalBase
	{
		protected static SUL.Dal.ItemJournalDal dal = new SUL.Dal.ItemJournalDal();

		public System.Int64 Id		{ get ; set; }

		public System.DateTime TransactionDate		{ get ; set; }

		public System.Int32 TransactionType		{ get ; set; }

		public System.Int32 SourceId		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }

		public System.Byte[] LastModifiedDate		{ get ; set; }

		public System.Int64 WareHouseId		{ get ; set; }


		public  Int32 InsertItemJournal()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@TransactionDate", TransactionDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TransactionType", TransactionType.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@SourceId", SourceId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
			
			lstItems.Add("@WareHouseId", WareHouseId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertItemJournal(lstItems);
		}

		public  Int32 UpdateItemJournal()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@TransactionDate", TransactionDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TransactionType", TransactionType.ToString());
			lstItems.Add("@SourceId", SourceId.ToString());
			lstItems.Add("@UserId", UserId.ToString());
			
			lstItems.Add("@WareHouseId", WareHouseId.ToString());

			return dal.UpdateItemJournal(lstItems);
		}

		public  Int32 DeleteItemJournalById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteItemJournalById(lstItems);
		}

		public List<ItemJournal> GetAllItemJournal()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllItemJournal(lstItems);
			List<ItemJournal> ItemJournalList = new List<ItemJournal>();
			foreach (DataRow dr in dt.Rows)
			{
				ItemJournalList.Add(GetObject(dr));
			}
			return ItemJournalList;
		}

		public ItemJournal  GetItemJournalById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetItemJournalById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ItemJournal GetObject(DataRow dr)
		{

			ItemJournal objItemJournal = new ItemJournal();
			objItemJournal.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objItemJournal.TransactionDate = (dr["TransactionDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["TransactionDate"];
			objItemJournal.TransactionType = (dr["TransactionType"] == DBNull.Value) ? 0 : (Int32)dr["TransactionType"];
			objItemJournal.SourceId = (dr["SourceId"] == DBNull.Value) ? 0 : (Int32)dr["SourceId"];
			objItemJournal.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
			
			objItemJournal.WareHouseId = (dr["WareHouseId"] == DBNull.Value) ? 0 : (Int64)dr["WareHouseId"];

			return objItemJournal;
		}
	}
}
