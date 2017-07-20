using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ItemJournalDetailsBase
	{
		protected static SUL.Dal.ItemJournalDetailsDal dal = new SUL.Dal.ItemJournalDetailsDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int32 MasterId		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }

		public System.String ProductName		{ get ; set; }

		public System.Int32 Color		{ get ; set; }

		public System.Int32 Unit		{ get ; set; }

		public System.Decimal OpeningBalance		{ get ; set; }

		public System.Int32 QuantityIn		{ get ; set; }

		public System.Int32 QuantityOut		{ get ; set; }

		public System.Decimal ClosingBalance		{ get ; set; }

		public System.Decimal Rate		{ get ; set; }


		public  Int32 InsertItemJournalDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductName", ProductName);
			lstItems.Add("@Color", Color.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Unit", Unit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@OpeningBalance", OpeningBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@QuantityIn", QuantityIn.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@QuantityOut", QuantityOut.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ClosingBalance", ClosingBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Rate", Rate.ToString(CultureInfo.InvariantCulture));

			return dal.InsertItemJournalDetails(lstItems);
		}

		public  Int32 UpdateItemJournalDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@ProductId", ProductId.ToString());
			lstItems.Add("@ProductName", ProductName);
			lstItems.Add("@Color", Color.ToString());
			lstItems.Add("@Unit", Unit.ToString());
			lstItems.Add("@OpeningBalance", OpeningBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@QuantityIn", QuantityIn.ToString());
			lstItems.Add("@QuantityOut", QuantityOut.ToString());
			lstItems.Add("@ClosingBalance", ClosingBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Rate", Rate.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateItemJournalDetails(lstItems);
		}

		public  Int32 DeleteItemJournalDetailsById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteItemJournalDetailsById(lstItems);
		}

		public List<ItemJournalDetails> GetAllItemJournalDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllItemJournalDetails(lstItems);
			List<ItemJournalDetails> ItemJournalDetailsList = new List<ItemJournalDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				ItemJournalDetailsList.Add(GetObject(dr));
			}
			return ItemJournalDetailsList;
		}

		public ItemJournalDetails  GetItemJournalDetailsById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetItemJournalDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ItemJournalDetails GetObject(DataRow dr)
		{

			ItemJournalDetails objItemJournalDetails = new ItemJournalDetails();
			objItemJournalDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objItemJournalDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int32)dr["MasterId"];
			objItemJournalDetails.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			objItemJournalDetails.ProductName = (dr["ProductName"] == DBNull.Value) ? "" : (String)dr["ProductName"];
			objItemJournalDetails.Color = (dr["Color"] == DBNull.Value) ? 0 : (Int32)dr["Color"];
			objItemJournalDetails.Unit = (dr["Unit"] == DBNull.Value) ? 0 : (Int32)dr["Unit"];
			objItemJournalDetails.OpeningBalance = (dr["OpeningBalance"] == DBNull.Value) ? 0 : (Decimal)dr["OpeningBalance"];
			objItemJournalDetails.QuantityIn = (dr["QuantityIn"] == DBNull.Value) ? 0 : (Int32)dr["QuantityIn"];
			objItemJournalDetails.QuantityOut = (dr["QuantityOut"] == DBNull.Value) ? 0 : (Int32)dr["QuantityOut"];
			objItemJournalDetails.ClosingBalance = (dr["ClosingBalance"] == DBNull.Value) ? 0 : (Decimal)dr["ClosingBalance"];
			objItemJournalDetails.Rate = (dr["Rate"] == DBNull.Value) ? 0 : (Decimal)dr["Rate"];

			return objItemJournalDetails;
		}
	}
}
