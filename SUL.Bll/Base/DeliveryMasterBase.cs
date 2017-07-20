using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class DeliveryMasterBase
	{
		protected static SUL.Dal.DeliveryMasterDal dal = new SUL.Dal.DeliveryMasterDal();

		public System.Int32 Id		{ get ; set; }

		public System.String DeliveryNo		{ get ; set; }

		public System.DateTime DeliveryDate		{ get ; set; }

		public System.Int32 DealerId		{ get ; set; }

		public System.Int32 RequisitionId		{ get ; set; }

		public System.Int32 WareHouseId		{ get ; set; }

		public System.Int32 CourierId		{ get ; set; }

		public System.String DeliveryAddress		{ get ; set; }


		public  Int32 InsertDeliveryMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@DeliveryNo", DeliveryNo);
			lstItems.Add("@DeliveryDate", DeliveryDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DealerId", DealerId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@RequisitionId", RequisitionId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@WareHouseId", WareHouseId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CourierId", CourierId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DeliveryAddress", DeliveryAddress);

			return dal.InsertDeliveryMaster(lstItems);
		}

		public  Int32 UpdateDeliveryMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@DeliveryNo", DeliveryNo);
			lstItems.Add("@DeliveryDate", DeliveryDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DealerId", DealerId.ToString());
			lstItems.Add("@RequisitionId", RequisitionId.ToString());
			lstItems.Add("@WareHouseId", WareHouseId.ToString());
			lstItems.Add("@CourierId", CourierId.ToString());
			lstItems.Add("@DeliveryAddress", DeliveryAddress);

			return dal.UpdateDeliveryMaster(lstItems);
		}

		public  Int32 DeleteDeliveryMasterById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteDeliveryMasterById(lstItems);
		}

		public List<DeliveryMaster> GetAllDeliveryMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllDeliveryMaster(lstItems);
			List<DeliveryMaster> DeliveryMasterList = new List<DeliveryMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				DeliveryMasterList.Add(GetObject(dr));
			}
			return DeliveryMasterList;
		}

		public DeliveryMaster  GetDeliveryMasterById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetDeliveryMasterById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  DeliveryMaster GetObject(DataRow dr)
		{

			DeliveryMaster objDeliveryMaster = new DeliveryMaster();
			objDeliveryMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objDeliveryMaster.DeliveryNo = (dr["DeliveryNo"] == DBNull.Value) ? "" : (String)dr["DeliveryNo"];
			objDeliveryMaster.DeliveryDate = (dr["DeliveryDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["DeliveryDate"];
			objDeliveryMaster.DealerId = (dr["DealerId"] == DBNull.Value) ? 0 : (Int32)dr["DealerId"];
			objDeliveryMaster.RequisitionId = (dr["RequisitionId"] == DBNull.Value) ? 0 : (Int32)dr["RequisitionId"];
			objDeliveryMaster.WareHouseId = (dr["WareHouseId"] == DBNull.Value) ? 0 : (Int32)dr["WareHouseId"];
			objDeliveryMaster.CourierId = (dr["CourierId"] == DBNull.Value) ? 0 : (Int32)dr["CourierId"];
			objDeliveryMaster.DeliveryAddress = (dr["DeliveryAddress"] == DBNull.Value) ? "" : (String)dr["DeliveryAddress"];

			return objDeliveryMaster;
		}
	}
}
