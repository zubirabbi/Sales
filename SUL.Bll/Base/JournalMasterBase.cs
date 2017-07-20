using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class JournalMasterBase
	{
		protected static SUL.Dal.JournalMasterDal dal = new SUL.Dal.JournalMasterDal();

		public System.Int32 JournalId		{ get ; set; }

		public System.String JournalCode		{ get ; set; }

		public System.Int32 JournalTypeId		{ get ; set; }

		public System.DateTime JDate		{ get ; set; }

		public System.String Description		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }

		public System.String Status		{ get ; set; }


		public  Int32 InsertJournalMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@JournalCode", JournalCode);
			lstItems.Add("@JournalTypeId", JournalTypeId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@JDate", JDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Description", Description);
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Status", Status);

			return dal.InsertJournalMaster(lstItems);
		}

		public  Int32 UpdateJournalMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@JournalId", JournalId.ToString());
			lstItems.Add("@JournalCode", JournalCode);
			lstItems.Add("@JournalTypeId", JournalTypeId.ToString());
			lstItems.Add("@JDate", JDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Description", Description);
			lstItems.Add("@UserId", UserId.ToString());
			lstItems.Add("@Status", Status);

			return dal.UpdateJournalMaster(lstItems);
		}

		public  Int32 DeleteJournalMasterByJournalId(Int32 JournalId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@JournalId", JournalId);

			return dal.DeleteJournalMasterByJournalId(lstItems);
		}

		public List<JournalMaster> GetAllJournalMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllJournalMaster(lstItems);
			List<JournalMaster> JournalMasterList = new List<JournalMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				JournalMasterList.Add(GetObject(dr));
			}
			return JournalMasterList;
		}

		public JournalMaster  GetJournalMasterByJournalId(Int32 _JournalId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@JournalId", _JournalId);

			DataTable dt = dal.GetJournalMasterByJournalId(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  JournalMaster GetObject(DataRow dr)
		{

			JournalMaster objJournalMaster = new JournalMaster();
			objJournalMaster.JournalId = (dr["JournalId"] == DBNull.Value) ? 0 : (Int32)dr["JournalId"];
			objJournalMaster.JournalCode = (dr["JournalCode"] == DBNull.Value) ? "" : (String)dr["JournalCode"];
			objJournalMaster.JournalTypeId = (dr["JournalTypeId"] == DBNull.Value) ? 0 : (Int32)dr["JournalTypeId"];
			objJournalMaster.JDate = (dr["JDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["JDate"];
			objJournalMaster.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];
			objJournalMaster.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
			objJournalMaster.Status = (dr["Status"] == DBNull.Value) ? "" : (String)dr["Status"];

			return objJournalMaster;
		}
	}
}
