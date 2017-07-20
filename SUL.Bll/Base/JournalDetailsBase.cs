using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class JournalDetailsBase
	{
		protected static SUL.Dal.JournalDetailsDal dal = new SUL.Dal.JournalDetailsDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 MasterId		{ get ; set; }

		public System.Int32 DealerId		{ get ; set; }

		public System.String ContraAccount		{ get ; set; }

		public System.Decimal Debit		{ get ; set; }

		public System.Decimal Credit		{ get ; set; }

		public System.String Description		{ get ; set; }


		public  Int32 InsertJournalDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DealerId", DealerId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ContraAccount", ContraAccount);
			lstItems.Add("@Debit", Debit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Credit", Credit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Description", Description);

			return dal.InsertJournalDetails(lstItems);
		}

		public  Int32 UpdateJournalDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@DealerId", DealerId.ToString());
			lstItems.Add("@ContraAccount", ContraAccount);
			lstItems.Add("@Debit", Debit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Credit", Credit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Description", Description);

			return dal.UpdateJournalDetails(lstItems);
		}

		public  Int32 DeleteJournalDetailsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteJournalDetailsById(lstItems);
		}

		public List<JournalDetails> GetAllJournalDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllJournalDetails(lstItems);
			List<JournalDetails> JournalDetailsList = new List<JournalDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				JournalDetailsList.Add(GetObject(dr));
			}
			return JournalDetailsList;
		}

		public JournalDetails  GetJournalDetailsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetJournalDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  JournalDetails GetObject(DataRow dr)
		{

			JournalDetails objJournalDetails = new JournalDetails();
			objJournalDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objJournalDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int32)dr["MasterId"];
			objJournalDetails.DealerId = (dr["DealerId"] == DBNull.Value) ? 0 : (Int32)dr["DealerId"];
			objJournalDetails.ContraAccount = (dr["ContraAccount"] == DBNull.Value) ? "" : (String)dr["ContraAccount"];
			objJournalDetails.Debit = (dr["Debit"] == DBNull.Value) ? 0 : (Decimal)dr["Debit"];
			objJournalDetails.Credit = (dr["Credit"] == DBNull.Value) ? 0 : (Decimal)dr["Credit"];
			objJournalDetails.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];

			return objJournalDetails;
		}
	}
}
