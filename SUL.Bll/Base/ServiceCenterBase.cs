using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ServiceCenterBase
	{
		protected static SUL.Dal.ServiceCenterDal dal = new SUL.Dal.ServiceCenterDal();

		public System.Int32 Id		{ get ; set; }

		public System.String SCCode		{ get ; set; }

		public System.Int32 AreaId		{ get ; set; }

		public System.String SCName		{ get ; set; }

		public System.String SCAddress		{ get ; set; }

		public System.Int32 InChargeId		{ get ; set; }

		public System.DateTime EstablishDate		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }


		public  Int32 InsertServiceCenter()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@SCCode", SCCode);
			lstItems.Add("@AreaId", AreaId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@SCName", SCName);
			lstItems.Add("@SCAddress", SCAddress);
			lstItems.Add("@InChargeId", InChargeId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@EstablishDate", EstablishDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);

			return dal.InsertServiceCenter(lstItems);
		}

		public  Int32 UpdateServiceCenter()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@SCCode", SCCode);
			lstItems.Add("@AreaId", AreaId.ToString());
			lstItems.Add("@SCName", SCName);
			lstItems.Add("@SCAddress", SCAddress);
			lstItems.Add("@InChargeId", InChargeId.ToString());
			lstItems.Add("@EstablishDate", EstablishDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);

			return dal.UpdateServiceCenter(lstItems);
		}

		public  Int32 DeleteServiceCenterById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteServiceCenterById(lstItems);
		}

		public List<ServiceCenter> GetAllServiceCenter()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllServiceCenter(lstItems);
			List<ServiceCenter> ServiceCenterList = new List<ServiceCenter>();
			foreach (DataRow dr in dt.Rows)
			{
				ServiceCenterList.Add(GetObject(dr));
			}
			return ServiceCenterList;
		}

		public ServiceCenter  GetServiceCenterById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetServiceCenterById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  ServiceCenter GetObject(DataRow dr)
		{

			ServiceCenter objServiceCenter = new ServiceCenter();
			objServiceCenter.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objServiceCenter.SCCode = (dr["SCCode"] == DBNull.Value) ? "" : (String)dr["SCCode"];
			objServiceCenter.AreaId = (dr["AreaId"] == DBNull.Value) ? 0 : (Int32)dr["AreaId"];
			objServiceCenter.SCName = (dr["SCName"] == DBNull.Value) ? "" : (String)dr["SCName"];
			objServiceCenter.SCAddress = (dr["SCAddress"] == DBNull.Value) ? "" : (String)dr["SCAddress"];
			objServiceCenter.InChargeId = (dr["InChargeId"] == DBNull.Value) ? 0 : (Int32)dr["InChargeId"];
			objServiceCenter.EstablishDate = (dr["EstablishDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["EstablishDate"];
			objServiceCenter.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];

			return objServiceCenter;
		}
	}
}
