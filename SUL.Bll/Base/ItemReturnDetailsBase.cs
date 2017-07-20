using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ItemReturnDetailsBase
	{
		protected static SUL.Dal.ItemReturnDetailsDal dal = new SUL.Dal.ItemReturnDetailsDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 MasterId		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }

		public System.Int32 ReturnQuantity		{ get ; set; }

		public System.Decimal ReturnRate		{ get ; set; }

		public System.Decimal LineTotal		{ get ; set; }

        public System.Int32 ColorId { get; set; }
        public System.Int32 UnitId { get; set; }


		public  Int32 InsertItemReturnDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
		
			lstItems.Add("@ReturnQuantity", ReturnQuantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ReturnRate", ReturnRate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@LineTotal", LineTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ColorId", ColorId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UnitId", UnitId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertItemReturnDetails(lstItems);
		}

		public  Int32 UpdateItemReturnDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@ProductId", ProductId.ToString());
			
			lstItems.Add("@ReturnQuantity", ReturnQuantity.ToString());
			lstItems.Add("@ReturnRate", ReturnRate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@LineTotal", LineTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ColorId", ColorId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UnitId", UnitId.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateItemReturnDetails(lstItems);
		}

		public  Int32 DeleteItemReturnDetailsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteItemReturnDetailsById(lstItems);
		}

		public List<ItemReturnDetails> GetAllItemReturnDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllItemReturnDetails(lstItems);
			List<ItemReturnDetails> ItemReturnDetailsList = new List<ItemReturnDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				ItemReturnDetailsList.Add(GetObject(dr));
			}
			return ItemReturnDetailsList;
		}

		public ItemReturnDetails  GetItemReturnDetailsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetItemReturnDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ItemReturnDetails GetObject(DataRow dr)
		{

			ItemReturnDetails objItemReturnDetails = new ItemReturnDetails();
			objItemReturnDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objItemReturnDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int32)dr["MasterId"];
			objItemReturnDetails.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			
			objItemReturnDetails.ReturnQuantity = (dr["ReturnQuantity"] == DBNull.Value) ? 0 : (Int32)dr["ReturnQuantity"];
			objItemReturnDetails.ReturnRate = (dr["ReturnRate"] == DBNull.Value) ? 0 : (Decimal)dr["ReturnRate"];
			objItemReturnDetails.LineTotal = (dr["LineTotal"] == DBNull.Value) ? 0 : (Decimal)dr["LineTotal"];
            objItemReturnDetails.ColorId = (dr["ColorId"] == DBNull.Value) ? 0 : (Int32)dr["ColorId"];
            objItemReturnDetails.UnitId = (dr["UnitId"] == DBNull.Value) ? 0 : (Int32)dr["UnitId"];

			return objItemReturnDetails;
		}
	}
}
