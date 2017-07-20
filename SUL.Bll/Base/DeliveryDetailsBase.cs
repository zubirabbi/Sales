using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class DeliveryDetailsBase
	{
		protected static SUL.Dal.DeliveryDetailsDal dal = new SUL.Dal.DeliveryDetailsDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 MasterId		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }

		public System.Int32 Quantity		{ get ; set; }

		public System.Int32 Unit		{ get ; set; }

		public System.Int32 Color		{ get ; set; }


		public  Int32 InsertDeliveryDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Quantity", Quantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Unit", Unit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Color", Color.ToString(CultureInfo.InvariantCulture));

			return dal.InsertDeliveryDetails(lstItems);
		}

		public  Int32 UpdateDeliveryDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@ProductId", ProductId.ToString());
			lstItems.Add("@Quantity", Quantity.ToString());
			lstItems.Add("@Unit", Unit.ToString());
			lstItems.Add("@Color", Color.ToString());

			return dal.UpdateDeliveryDetails(lstItems);
		}

		public  Int32 DeleteDeliveryDetailsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteDeliveryDetailsById(lstItems);
		}

		public List<DeliveryDetails> GetAllDeliveryDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllDeliveryDetails(lstItems);
			List<DeliveryDetails> DeliveryDetailsList = new List<DeliveryDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				DeliveryDetailsList.Add(GetObject(dr));
			}
			return DeliveryDetailsList;
		}

		public DeliveryDetails  GetDeliveryDetailsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetDeliveryDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  DeliveryDetails GetObject(DataRow dr)
		{

			DeliveryDetails objDeliveryDetails = new DeliveryDetails();
			objDeliveryDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objDeliveryDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int32)dr["MasterId"];
			objDeliveryDetails.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			objDeliveryDetails.Quantity = (dr["Quantity"] == DBNull.Value) ? 0 : (Int32)dr["Quantity"];
			objDeliveryDetails.Unit = (dr["Unit"] == DBNull.Value) ? 0 : (Int32)dr["Unit"];
			objDeliveryDetails.Color = (dr["Color"] == DBNull.Value) ? 0 : (Int32)dr["Color"];

			return objDeliveryDetails;
		}
	}
}
