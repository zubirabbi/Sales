using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class DealerJournalMasterBase
	{
		protected static SUL.Dal.DealerJournalMasterDal dal = new SUL.Dal.DealerJournalMasterDal();

		public System.Int32 Id		{ get ; set; }

		public System.String JournalId		{ get ; set; }

		public System.DateTime Date		{ get ; set; }

		public System.Int32 Type		{ get ; set; }


		public System.String Description		{ get ; set; }

		public System.String ContraAccount		{ get ; set; }

		public System.Int32 CreatedBy		{ get ; set; }

		public System.DateTime CreatedDate		{ get ; set; }

		public System.Int32 ApproveBy		{ get ; set; }

		public System.DateTime ApproveDate		{ get ; set; }

		public System.Int32 UpdateBy		{ get ; set; }

		public System.DateTime UpdateDate		{ get ; set; }

	    public System.String Status { get; set; }

	    public  Int32 InsertDealerJournalMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@JournalId", JournalId);
			lstItems.Add("@Date", Date.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Type", Type.ToString(CultureInfo.InvariantCulture));
			
			lstItems.Add("@Description", Description);
			lstItems.Add("@ContraAccount", ContraAccount);
			lstItems.Add("@CreatedBy", CreatedBy.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CreatedDate", CreatedDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ApproveBy", ApproveBy.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ApproveDate", ApproveDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UpdateBy", UpdateBy.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UpdateDate", UpdateDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Status", Status);
			return dal.InsertDealerJournalMaster(lstItems);
		}

		public  Int32 UpdateDealerJournalMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@JournalId", JournalId);
			lstItems.Add("@Date", Date.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Type", Type.ToString());
			
			lstItems.Add("@Description", Description);
			lstItems.Add("@ContraAccount", ContraAccount);
			lstItems.Add("@CreatedBy", CreatedBy.ToString());
			lstItems.Add("@CreatedDate", CreatedDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ApproveBy", ApproveBy.ToString());
			lstItems.Add("@ApproveDate", ApproveDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UpdateBy", UpdateBy.ToString());
			lstItems.Add("@UpdateDate", UpdateDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Status", Status);
			return dal.UpdateDealerJournalMaster(lstItems);
		}

		public  Int32 DeleteDealerJournalMasterById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteDealerJournalMasterById(lstItems);
		}

		public List<DealerJournalMaster> GetAllDealerJournalMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllDealerJournalMaster(lstItems);
			List<DealerJournalMaster> DealerJournalMasterList = new List<DealerJournalMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				DealerJournalMasterList.Add(GetObject(dr));
			}
			return DealerJournalMasterList;
		}

		public DealerJournalMaster  GetDealerJournalMasterById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetDealerJournalMasterById(lstItems);
		    if (dt.Rows.Count != 0)
		    {

		        DataRow dr = dt.Rows[0];
		        return GetObject(dr);
		    }
		    else
		    {
		        return new DealerJournalMaster();
		    }
		}

		protected  DealerJournalMaster GetObject(DataRow dr)
		{

			DealerJournalMaster objDealerJournalMaster = new DealerJournalMaster();
			objDealerJournalMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objDealerJournalMaster.JournalId = (dr["JournalId"] == DBNull.Value) ? "" : (String)dr["JournalId"];
			objDealerJournalMaster.Date = (dr["Date"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["Date"];
			objDealerJournalMaster.Type = (dr["Type"] == DBNull.Value) ? 0 : (Int32)dr["Type"];
			objDealerJournalMaster.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];
			objDealerJournalMaster.ContraAccount = (dr["ContraAccount"] == DBNull.Value) ? "" : (String)dr["ContraAccount"];
			objDealerJournalMaster.CreatedBy = (dr["CreatedBy"] == DBNull.Value) ? 0 : (Int32)dr["CreatedBy"];
			objDealerJournalMaster.CreatedDate = (dr["CreatedDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["CreatedDate"];
			objDealerJournalMaster.ApproveBy = (dr["ApproveBy"] == DBNull.Value) ? 0 : (Int32)dr["ApproveBy"];
			objDealerJournalMaster.ApproveDate = (dr["ApproveDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["ApproveDate"];
			objDealerJournalMaster.UpdateBy = (dr["UpdateBy"] == DBNull.Value) ? 0 : (Int32)dr["UpdateBy"];
			objDealerJournalMaster.UpdateDate = (dr["UpdateDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["UpdateDate"];
            objDealerJournalMaster.Status = (dr["Status"] == DBNull.Value) ? "" : (String)dr["Status"];

			return objDealerJournalMaster;
		}
	}
}
