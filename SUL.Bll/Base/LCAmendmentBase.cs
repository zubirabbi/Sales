using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class LCAmendmentBase
	{
		protected static SUL.Dal.LCAmendmentDal dal = new SUL.Dal.LCAmendmentDal();

		public System.Int32 Id		{ get ; set; }

		public System.String AmendmentNumber		{ get ; set; }

		public System.DateTime AmendementDate		{ get ; set; }

		public System.String AmendmentDescription		{ get ; set; }

		public System.Int32 LCId		{ get ; set; }


		public  Int32 InsertLCAmendment()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@AmendmentNumber", AmendmentNumber);
			lstItems.Add("@AmendementDate", AmendementDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@AmendmentDescription", AmendmentDescription);
			lstItems.Add("@LCId", LCId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertLCAmendment(lstItems);
		}

		public  Int32 UpdateLCAmendment()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@AmendmentNumber", AmendmentNumber);
			lstItems.Add("@AmendementDate", AmendementDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@AmendmentDescription", AmendmentDescription);
			lstItems.Add("@LCId", LCId.ToString());

			return dal.UpdateLCAmendment(lstItems);
		}

		public  Int32 DeleteLCAmendmentById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteLCAmendmentById(lstItems);
		}

		public List<LCAmendment> GetAllLCAmendment()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllLCAmendment(lstItems);
			List<LCAmendment> LCAmendmentList = new List<LCAmendment>();
			foreach (DataRow dr in dt.Rows)
			{
				LCAmendmentList.Add(GetObject(dr));
			}
			return LCAmendmentList;
		}

		public LCAmendment  GetLCAmendmentById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetLCAmendmentById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  LCAmendment GetObject(DataRow dr)
		{

			LCAmendment objLCAmendment = new LCAmendment();
			objLCAmendment.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objLCAmendment.AmendmentNumber = (dr["AmendmentNumber"] == DBNull.Value) ? "" : (String)dr["AmendmentNumber"];
			objLCAmendment.AmendementDate = (dr["AmendementDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["AmendementDate"];
			objLCAmendment.AmendmentDescription = (dr["AmendmentDescription"] == DBNull.Value) ? "" : (String)dr["AmendmentDescription"];
			objLCAmendment.LCId = (dr["LCId"] == DBNull.Value) ? 0 : (Int32)dr["LCId"];

			return objLCAmendment;
		}
	}
}
