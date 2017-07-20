using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ItemLedgerBase
	{
		protected static SUL.Dal.ItemLedgerDal dal = new SUL.Dal.ItemLedgerDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int64 ItemId		{ get ; set; }

		public System.Int32 Unit		{ get ; set; }

		public System.String Color		{ get ; set; }

		public System.Int64 WareHouseId		{ get ; set; }

		public System.Int32 TotalIn		{ get ; set; }

		public System.Int32 TotalOut		{ get ; set; }

		public System.Int32 Balance		{ get ; set; }

		public System.Decimal Value		{ get ; set; }


		public  Int32 InsertItemLedger()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@ItemId", ItemId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Unit", Unit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Color", Color);
			lstItems.Add("@WareHouseId", WareHouseId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TotalIn", TotalIn.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TotalOut", TotalOut.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Value", Value.ToString(CultureInfo.InvariantCulture));

			return dal.InsertItemLedger(lstItems);
		}

		public  Int32 UpdateItemLedger()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@ItemId", ItemId.ToString());
			lstItems.Add("@Unit", Unit.ToString());
			lstItems.Add("@Color", Color);
			lstItems.Add("@WareHouseId", WareHouseId.ToString());
			lstItems.Add("@TotalIn", TotalIn.ToString());
			lstItems.Add("@TotalOut", TotalOut.ToString());
			lstItems.Add("@Value", Value.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateItemLedger(lstItems);
		}

		public  Int32 DeleteItemLedgerById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteItemLedgerById(lstItems);
		}

		public List<ItemLedger> GetAllItemLedger()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllItemLedger(lstItems);
			List<ItemLedger> ItemLedgerList = new List<ItemLedger>();
			foreach (DataRow dr in dt.Rows)
			{
				ItemLedgerList.Add(GetObject(dr));
			}
			return ItemLedgerList;
		}

		public ItemLedger  GetItemLedgerById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetItemLedgerById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ItemLedger GetObject(DataRow dr)
		{

			ItemLedger objItemLedger = new ItemLedger();
			objItemLedger.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objItemLedger.ItemId = (dr["ItemId"] == DBNull.Value) ? 0 : (Int64)dr["ItemId"];
			objItemLedger.Unit = (dr["Unit"] == DBNull.Value) ? 0 : (Int32)dr["Unit"];
			objItemLedger.Color = (dr["Color"] == DBNull.Value) ? "" : (String)dr["Color"];
			objItemLedger.WareHouseId = (dr["WareHouseId"] == DBNull.Value) ? 0 : (Int64)dr["WareHouseId"];
			objItemLedger.TotalIn = (dr["TotalIn"] == DBNull.Value) ? 0 : (Int32)dr["TotalIn"];
			objItemLedger.TotalOut = (dr["TotalOut"] == DBNull.Value) ? 0 : (Int32)dr["TotalOut"];
			objItemLedger.Balance = (dr["Balance"] == DBNull.Value) ? 0 : (Int32)dr["Balance"];
			objItemLedger.Value = (dr["Value"] == DBNull.Value) ? 0 : (Decimal)dr["Value"];

			return objItemLedger;
		}
	}
}
