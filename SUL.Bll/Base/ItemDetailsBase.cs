using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ItemDetailsBase
	{
		protected static SUL.Dal.ItemDetailsDal dal = new SUL.Dal.ItemDetailsDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int64 MasterId		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }

		public System.String ProductName		{ get ; set; }

		public System.String Color		{ get ; set; }

		public System.Int32 Unit		{ get ; set; }

		public System.Decimal OpeningBalance		{ get ; set; }

		public System.Int32 QuantityIn		{ get ; set; }

		public System.Int32 QuantityOut		{ get ; set; }

		public System.Decimal ClosingBalance		{ get ; set; }

		public System.Decimal Rate		{ get ; set; }

		public System.Decimal Value		{ get ; set; }


		public  Int32 InsertItemDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductName", ProductName);
			lstItems.Add("@Color", Color);
			lstItems.Add("@Unit", Unit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@OpeningBalance", OpeningBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@QuantityIn", QuantityIn.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@QuantityOut", QuantityOut.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ClosingBalance", ClosingBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Rate", Rate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Value", Value.ToString(CultureInfo.InvariantCulture));

			return dal.InsertItemDetails(lstItems);
		}

		public  Int32 UpdateItemDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@ProductId", ProductId.ToString());
			lstItems.Add("@ProductName", ProductName);
			lstItems.Add("@Color", Color);
			lstItems.Add("@Unit", Unit.ToString());
			lstItems.Add("@OpeningBalance", OpeningBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@QuantityIn", QuantityIn.ToString());
			lstItems.Add("@QuantityOut", QuantityOut.ToString());
			lstItems.Add("@ClosingBalance", ClosingBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Rate", Rate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Value", Value.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateItemDetails(lstItems);
		}

		public  Int32 DeleteItemDetailsById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteItemDetailsById(lstItems);
		}

		public List<ItemDetails> GetAllItemDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllItemDetails(lstItems);
			List<ItemDetails> ItemDetailsList = new List<ItemDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				ItemDetailsList.Add(GetObject(dr));
			}
			return ItemDetailsList;
		}

		public ItemDetails  GetItemDetailsById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetItemDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ItemDetails GetObject(DataRow dr)
		{

			ItemDetails objItemDetails = new ItemDetails();
			objItemDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objItemDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int64)dr["MasterId"];
			objItemDetails.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			objItemDetails.ProductName = (dr["ProductName"] == DBNull.Value) ? "" : (String)dr["ProductName"];
			objItemDetails.Color = (dr["Color"] == DBNull.Value) ? "" : (String)dr["Color"];
			objItemDetails.Unit = (dr["Unit"] == DBNull.Value) ? 0 : (Int32)dr["Unit"];
			objItemDetails.OpeningBalance = (dr["OpeningBalance"] == DBNull.Value) ? 0 : (Decimal)dr["OpeningBalance"];
			objItemDetails.QuantityIn = (dr["QuantityIn"] == DBNull.Value) ? 0 : (Int32)dr["QuantityIn"];
			objItemDetails.QuantityOut = (dr["QuantityOut"] == DBNull.Value) ? 0 : (Int32)dr["QuantityOut"];
			objItemDetails.ClosingBalance = (dr["ClosingBalance"] == DBNull.Value) ? 0 : (Decimal)dr["ClosingBalance"];
			objItemDetails.Rate = (dr["Rate"] == DBNull.Value) ? 0 : (Decimal)dr["Rate"];
			objItemDetails.Value = (dr["Value"] == DBNull.Value) ? 0 : (Decimal)dr["Value"];

			return objItemDetails;
		}
	}
}
