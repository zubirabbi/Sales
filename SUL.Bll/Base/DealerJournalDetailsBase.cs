using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class DealerJournalDetailsBase
	{
		protected static SUL.Dal.DealerJournalDetailsDal dal = new SUL.Dal.DealerJournalDetailsDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 MasterId		{ get ; set; }

		public System.Int32 DealerId		{ get ; set; }

		public System.Decimal CurrentBalance		{ get ; set; }

		public System.Decimal Debit		{ get ; set; }

		public System.Decimal Credit		{ get ; set; }

		public System.String Description		{ get ; set; }


		public  Int32 InsertDealerJournalDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DealerId", DealerId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CurrentBalance", CurrentBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Debit", Debit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Credit", Credit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Description", Description);

			return dal.InsertDealerJournalDetails(lstItems);
		}

		public  Int32 UpdateDealerJournalDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@DealerId", DealerId.ToString());
			lstItems.Add("@CurrentBalance", CurrentBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Debit", Debit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Credit", Credit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Description", Description);

			return dal.UpdateDealerJournalDetails(lstItems);
		}

		public  Int32 DeleteDealerJournalDetailsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteDealerJournalDetailsById(lstItems);
		}

		public List<DealerJournalDetails> GetAllDealerJournalDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllDealerJournalDetails(lstItems);
			List<DealerJournalDetails> DealerJournalDetailsList = new List<DealerJournalDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				DealerJournalDetailsList.Add(GetObject(dr));
			}
			return DealerJournalDetailsList;
		}

		public DealerJournalDetails  GetDealerJournalDetailsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetDealerJournalDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  DealerJournalDetails GetObject(DataRow dr)
		{

			DealerJournalDetails objDealerJournalDetails = new DealerJournalDetails();
			objDealerJournalDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objDealerJournalDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int32)dr["MasterId"];
			objDealerJournalDetails.DealerId = (dr["DealerId"] == DBNull.Value) ? 0 : (Int32)dr["DealerId"];
			objDealerJournalDetails.CurrentBalance = (dr["CurrentBalance"] == DBNull.Value) ? 0 : (Decimal)dr["CurrentBalance"];
			objDealerJournalDetails.Debit = (dr["Debit"] == DBNull.Value) ? 0 : (Decimal)dr["Debit"];
			objDealerJournalDetails.Credit = (dr["Credit"] == DBNull.Value) ? 0 : (Decimal)dr["Credit"];
			objDealerJournalDetails.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];

			return objDealerJournalDetails;
		}
	}
}
