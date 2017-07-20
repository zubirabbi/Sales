using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class SPDeliveryDetailsBase
	{
		protected static SUL.Dal.SPDeliveryDetailsDal dal = new SUL.Dal.SPDeliveryDetailsDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 MasterId		{ get ; set; }

		public System.Int32 SpairPartsId		{ get ; set; }

		public System.Int32 Color		{ get ; set; }

		public System.Decimal Rate		{ get ; set; }

		public System.Decimal Quantity		{ get ; set; }

		public System.Decimal TotalAmount		{ get ; set; }

		public System.Int32 Unit		{ get ; set; }


		public  Int32 InsertSPDeliveryDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@SpairPartsId", SpairPartsId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Color", Color.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Rate", Rate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Quantity", Quantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TotalAmount", TotalAmount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Unit", Unit.ToString(CultureInfo.InvariantCulture));

			return dal.InsertSPDeliveryDetails(lstItems);
		}

		public  Int32 UpdateSPDeliveryDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@SpairPartsId", SpairPartsId.ToString());
			lstItems.Add("@Color", Color.ToString());
			lstItems.Add("@Rate", Rate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Quantity", Quantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TotalAmount", TotalAmount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Unit", Unit.ToString());

			return dal.UpdateSPDeliveryDetails(lstItems);
		}

		public  Int32 DeleteSPDeliveryDetailsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteSPDeliveryDetailsById(lstItems);
		}

		public List<SPDeliveryDetails> GetAllSPDeliveryDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllSPDeliveryDetails(lstItems);
			List<SPDeliveryDetails> SPDeliveryDetailsList = new List<SPDeliveryDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				SPDeliveryDetailsList.Add(GetObject(dr));
			}
			return SPDeliveryDetailsList;
		}

		public SPDeliveryDetails  GetSPDeliveryDetailsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetSPDeliveryDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  SPDeliveryDetails GetObject(DataRow dr)
		{

			SPDeliveryDetails objSPDeliveryDetails = new SPDeliveryDetails();
			objSPDeliveryDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objSPDeliveryDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int32)dr["MasterId"];
			objSPDeliveryDetails.SpairPartsId = (dr["SpairPartsId"] == DBNull.Value) ? 0 : (Int32)dr["SpairPartsId"];
			objSPDeliveryDetails.Color = (dr["Color"] == DBNull.Value) ? 0 : (Int32)dr["Color"];
			objSPDeliveryDetails.Rate = (dr["Rate"] == DBNull.Value) ? 0 : (Decimal)dr["Rate"];
			objSPDeliveryDetails.Quantity = (dr["Quantity"] == DBNull.Value) ? 0 : (Decimal)dr["Quantity"];
			objSPDeliveryDetails.TotalAmount = (dr["TotalAmount"] == DBNull.Value) ? 0 : (Decimal)dr["TotalAmount"];
			objSPDeliveryDetails.Unit = (dr["Unit"] == DBNull.Value) ? 0 : (Int32)dr["Unit"];

			return objSPDeliveryDetails;
		}
	}
}
