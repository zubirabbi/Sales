using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class SPDeliveryMasterBase
	{
		protected static SUL.Dal.SPDeliveryMasterDal dal = new SUL.Dal.SPDeliveryMasterDal();

		public System.Int32 Id		{ get ; set; }

		public System.String TransactionCode		{ get ; set; }

		public System.DateTime CreateDate		{ get ; set; }

		public System.Int32 ServiceCenterId		{ get ; set; }

		public System.String DeliveryMethod		{ get ; set; }

		public System.DateTime DeliveryDate		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }

		public System.Int32 ApproveBy		{ get ; set; }

		public System.DateTime ApproveDate		{ get ; set; }

		public System.String Status		{ get ; set; }

		public System.DateTime ReceiveDate		{ get ; set; }

		public System.Int32 ReceiveBy		{ get ; set; }


		public  Int32 InsertSPDeliveryMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@TransactionCode", TransactionCode);
			lstItems.Add("@CreateDate", CreateDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ServiceCenterId", ServiceCenterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DeliveryMethod", DeliveryMethod);
			lstItems.Add("@DeliveryDate", DeliveryDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ApproveBy", ApproveBy.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ApproveDate", ApproveDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Status", Status);
			lstItems.Add("@ReceiveDate", ReceiveDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ReceiveBy", ReceiveBy.ToString(CultureInfo.InvariantCulture));

			return dal.InsertSPDeliveryMaster(lstItems);
		}

		public  Int32 UpdateSPDeliveryMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@TransactionCode", TransactionCode);
			lstItems.Add("@CreateDate", CreateDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ServiceCenterId", ServiceCenterId.ToString());
			lstItems.Add("@DeliveryMethod", DeliveryMethod);
			lstItems.Add("@DeliveryDate", DeliveryDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UserId", UserId.ToString());
			lstItems.Add("@ApproveBy", ApproveBy.ToString());
			lstItems.Add("@ApproveDate", ApproveDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Status", Status);
			lstItems.Add("@ReceiveDate", ReceiveDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ReceiveBy", ReceiveBy.ToString());

			return dal.UpdateSPDeliveryMaster(lstItems);
		}

		public  Int32 DeleteSPDeliveryMasterById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteSPDeliveryMasterById(lstItems);
		}

		public List<SPDeliveryMaster> GetAllSPDeliveryMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllSPDeliveryMaster(lstItems);
			List<SPDeliveryMaster> SPDeliveryMasterList = new List<SPDeliveryMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				SPDeliveryMasterList.Add(GetObject(dr));
			}
			return SPDeliveryMasterList;
		}

		public SPDeliveryMaster  GetSPDeliveryMasterById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetSPDeliveryMasterById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  SPDeliveryMaster GetObject(DataRow dr)
		{

			SPDeliveryMaster objSPDeliveryMaster = new SPDeliveryMaster();
			objSPDeliveryMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objSPDeliveryMaster.TransactionCode = (dr["TransactionCode"] == DBNull.Value) ? "" : (String)dr["TransactionCode"];
			objSPDeliveryMaster.CreateDate = (dr["CreateDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["CreateDate"];
			objSPDeliveryMaster.ServiceCenterId = (dr["ServiceCenterId"] == DBNull.Value) ? 0 : (Int32)dr["ServiceCenterId"];
            objSPDeliveryMaster.DeliveryMethod = (dr["DeliveryMethod"] == DBNull.Value) ? "" : (String)dr["DeliveryMethod"];
			objSPDeliveryMaster.DeliveryDate = (dr["DeliveryDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["DeliveryDate"];
			objSPDeliveryMaster.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
			objSPDeliveryMaster.ApproveBy = (dr["ApproveBy"] == DBNull.Value) ? 0 : (Int32)dr["ApproveBy"];
			objSPDeliveryMaster.ApproveDate = (dr["ApproveDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["ApproveDate"];
			objSPDeliveryMaster.Status = (dr["Status"] == DBNull.Value) ? "" : (String)dr["Status"];
			objSPDeliveryMaster.ReceiveDate = (dr["ReceiveDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["ReceiveDate"];
			objSPDeliveryMaster.ReceiveBy = (dr["ReceiveBy"] == DBNull.Value) ? 0 : (Int32)dr["ReceiveBy"];

			return objSPDeliveryMaster;
		}
	}
}
