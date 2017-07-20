using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class DealerLedgerBase
	{
		protected static SUL.Dal.DealerLedgerDal dal = new SUL.Dal.DealerLedgerDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 DealerId		{ get ; set; }

		public System.String TransactionType		{ get ; set; }

		public System.DateTime TransactionDate		{ get ; set; }

		public System.String SourceId		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }

        public System.Decimal OpeningBalance		{ get ; set; }

		public System.Decimal Debit		{ get ; set; }

		public System.Decimal Cradit		{ get ; set; }

		public System.Decimal ClosingBalance		{ get ; set; }

        public System.String SourceNo { get; set; }

        public System.String Remarks { get; set; }


		public  Int32 InsertDealerLedger()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@DealerId", DealerId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TransactionType", TransactionType);
			lstItems.Add("@TransactionDate", TransactionDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@SourceId", SourceId);
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@OpeningBalance", OpeningBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Debit", Debit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Cradit", Cradit.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@SourceNo", SourceNo.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Remarks", Remarks.ToString(CultureInfo.InvariantCulture));

			return dal.InsertDealerLedger(lstItems);
		}

		public  Int32 UpdateDealerLedger()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@DealerId", DealerId.ToString());
			lstItems.Add("@TransactionType", TransactionType);
			lstItems.Add("@TransactionDate", TransactionDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@SourceId", SourceId);
			lstItems.Add("@UserId", UserId.ToString());
			lstItems.Add("@OpeningBalance", OpeningBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Debit", Debit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Cradit", Cradit.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@SourceNo", SourceNo.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Remarks", Remarks.ToString(CultureInfo.InvariantCulture));
			

			return dal.UpdateDealerLedger(lstItems);
		}

		public  Int32 DeleteDealerLedgerById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteDealerLedgerById(lstItems);
		}

		public List<DealerLedger> GetAllDealerLedger()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllDealerLedger(lstItems);
			List<DealerLedger> DealerLedgerList = new List<DealerLedger>();
			foreach (DataRow dr in dt.Rows)
			{
				DealerLedgerList.Add(GetObject(dr));
			}
			return DealerLedgerList;
		}

		public DealerLedger  GetDealerLedgerById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetDealerLedgerById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  DealerLedger GetObject(DataRow dr)
		{

			DealerLedger objDealerLedger = new DealerLedger();
			objDealerLedger.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objDealerLedger.DealerId = (dr["DealerId"] == DBNull.Value) ? 0 : (Int32)dr["DealerId"];
			objDealerLedger.TransactionType = (dr["TransactionType"] == DBNull.Value) ? "" : (String)dr["TransactionType"];
			objDealerLedger.TransactionDate = (dr["TransactionDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["TransactionDate"];
			objDealerLedger.SourceId = (dr["SourceId"] == DBNull.Value) ? "" : (String)dr["SourceId"];
			objDealerLedger.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
			
			objDealerLedger.OpeningBalance = (dr["OpeningBalance"] == DBNull.Value) ? 0 : (Decimal)dr["OpeningBalance"];
			objDealerLedger.Debit = (dr["Debit"] == DBNull.Value) ? 0 : (Decimal)dr["Debit"];
			objDealerLedger.Cradit = (dr["Cradit"] == DBNull.Value) ? 0 : (Decimal)dr["Cradit"];
			objDealerLedger.ClosingBalance = (dr["ClosingBalance"] == DBNull.Value) ? 0 : (Decimal)dr["ClosingBalance"];
            objDealerLedger.SourceNo = (dr["SourceNo"] == DBNull.Value) ? "" : (String)dr["SourceNo"];
            objDealerLedger.Remarks = (dr["Remarks"] == DBNull.Value) ? "" : (String)dr["Remarks"];

			return objDealerLedger;
		}
	}
}
