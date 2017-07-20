using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ReceiverDetailsBase
	{
		protected static SUL.Dal.ReceiverDetailsDal dal = new SUL.Dal.ReceiverDetailsDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int64 MasterId		{ get ; set; }

		public System.String ProductCode		{ get ; set; }

		public System.Int32 LCQuantity		{ get ; set; }

		public System.Int32 ReceivedQuantity		{ get ; set; }

		public System.String Color		{ get ; set; }

		public System.Int32 ReceiveQuantity		{ get ; set; }

		public System.Int32 Unit		{ get ; set; }

        public System.Int32 ProductId { get; set; }


		public  Int32 InsertReceiverDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductCode", ProductCode);
			lstItems.Add("@LCQuantity", LCQuantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ReceivedQuantity", ReceivedQuantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Color", Color);
			lstItems.Add("@ReceiveQuantity", ReceiveQuantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Unit", Unit.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertReceiverDetails(lstItems);
		}

		public  Int32 UpdateReceiverDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@ProductCode", ProductCode);
			lstItems.Add("@LCQuantity", LCQuantity.ToString());
			lstItems.Add("@ReceivedQuantity", ReceivedQuantity.ToString());
			lstItems.Add("@Color", Color);
			lstItems.Add("@ReceiveQuantity", ReceiveQuantity.ToString());
			lstItems.Add("@Unit", Unit.ToString());
            lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateReceiverDetails(lstItems);
		}

		public  Int32 DeleteReceiverDetailsById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteReceiverDetailsById(lstItems);
		}

		public List<ReceiverDetails> GetAllReceiverDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllReceiverDetails(lstItems);
			List<ReceiverDetails> ReceiverDetailsList = new List<ReceiverDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				ReceiverDetailsList.Add(GetObject(dr));
			}
			return ReceiverDetailsList;
		}

		public ReceiverDetails  GetReceiverDetailsById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetReceiverDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ReceiverDetails GetObject(DataRow dr)
		{

			ReceiverDetails objReceiverDetails = new ReceiverDetails();
			objReceiverDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objReceiverDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int64)dr["MasterId"];
			objReceiverDetails.ProductCode = (dr["ProductCode"] == DBNull.Value) ? "" : (String)dr["ProductCode"];
			objReceiverDetails.LCQuantity = (dr["LCQuantity"] == DBNull.Value) ? 0 : (Int32)dr["LCQuantity"];
			objReceiverDetails.ReceivedQuantity = (dr["ReceivedQuantity"] == DBNull.Value) ? 0 : (Int32)dr["ReceivedQuantity"];
			objReceiverDetails.Color = (dr["Color"] == DBNull.Value) ? "" : (String)dr["Color"];
			objReceiverDetails.ReceiveQuantity = (dr["ReceiveQuantity"] == DBNull.Value) ? 0 : (Int32)dr["ReceiveQuantity"];
			objReceiverDetails.Unit = (dr["Unit"] == DBNull.Value) ? 0 : (Int32)dr["Unit"];
            objReceiverDetails.Unit = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];

			return objReceiverDetails;
		}
	}
}
