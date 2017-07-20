using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ServiceBase
	{
		protected static SUL.Dal.ServiceDal dal = new SUL.Dal.ServiceDal();

		public System.Int32 Id		{ get ; set; }

		public System.String ServiceName		{ get ; set; }

		public System.String ServiceDescription		{ get ; set; }

		public System.Decimal NWCost		{ get ; set; }

		public System.Decimal WCost		{ get ; set; }

		public System.Boolean IsSPReqired		{ get ; set; }

		public System.DateTime ServiceTime		{ get ; set; }

		public System.Int32 ServiceLevel		{ get ; set; }


		public  Int32 InsertService()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@ServiceName", ServiceName);
			lstItems.Add("@ServiceDescription", ServiceDescription);
			lstItems.Add("@NWCost", NWCost.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@WCost", WCost.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsSPReqired", IsSPReqired);
			lstItems.Add("@ServiceTime", ServiceTime.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ServiceLevel", ServiceLevel.ToString(CultureInfo.InvariantCulture));

			return dal.InsertService(lstItems);
		}

		public  Int32 UpdateService()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@ServiceName", ServiceName);
			lstItems.Add("@ServiceDescription", ServiceDescription);
			lstItems.Add("@NWCost", NWCost.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@WCost", WCost.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsSPReqired", IsSPReqired);
			lstItems.Add("@ServiceTime", ServiceTime.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ServiceLevel", ServiceLevel.ToString());

			return dal.UpdateService(lstItems);
		}

		public  Int32 DeleteServiceById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteServiceById(lstItems);
		}

		public List<Service> GetAllService()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllService(lstItems);
			List<Service> ServiceList = new List<Service>();
			foreach (DataRow dr in dt.Rows)
			{
				ServiceList.Add(GetObject(dr));
			}
			return ServiceList;
		}

		public Service  GetServiceById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetServiceById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  Service GetObject(DataRow dr)
		{

			Service objService = new Service();
			objService.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objService.ServiceName = (dr["ServiceName"] == DBNull.Value) ? "" : (String)dr["ServiceName"];
			objService.ServiceDescription = (dr["ServiceDescription"] == DBNull.Value) ? "" : (String)dr["ServiceDescription"];
			objService.NWCost = (dr["NWCost"] == DBNull.Value) ? 0 : (Decimal)dr["NWCost"];
			objService.WCost = (dr["WCost"] == DBNull.Value) ? 0 : (Decimal)dr["WCost"];
			objService.IsSPReqired = (dr["IsSPReqired"] == DBNull.Value) ? false : (Boolean)dr["IsSPReqired"];
			objService.ServiceTime = (dr["ServiceTime"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["ServiceTime"];
			objService.ServiceLevel = (dr["ServiceLevel"] == DBNull.Value) ? 0 : (Int32)dr["ServiceLevel"];

			return objService;
		}
	}
}
