using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class SupplierLedgerBase
	{
		protected static SUL.Dal.SupplierLedgerDal dal = new SUL.Dal.SupplierLedgerDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 SupplierId		{ get ; set; }

		public System.String TransactionType		{ get ; set; }

		public System.DateTime TransactionDate		{ get ; set; }

		public System.String SourceId		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }


		public System.Decimal OpeningBalance		{ get ; set; }

		public System.Decimal Debit		{ get ; set; }

		public System.Decimal Cradit		{ get ; set; }

		public System.Decimal ClosingBalance		{ get ; set; }


		public  Int32 InsertSupplierLedger()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@SupplierId", SupplierId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TransactionType", TransactionType);
			lstItems.Add("@TransactionDate", TransactionDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@SourceId", SourceId);
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@OpeningBalance", OpeningBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Debit", Debit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Cradit", Cradit.ToString(CultureInfo.InvariantCulture));
		

			return dal.InsertSupplierLedger(lstItems);
		}

		public  Int32 UpdateSupplierLedger()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@SupplierId", SupplierId.ToString());
			lstItems.Add("@TransactionType", TransactionType);
			lstItems.Add("@TransactionDate", TransactionDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@SourceId", SourceId);
			lstItems.Add("@UserId", UserId.ToString());
			lstItems.Add("@OpeningBalance", OpeningBalance.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Debit", Debit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Cradit", Cradit.ToString(CultureInfo.InvariantCulture));
			
			return dal.UpdateSupplierLedger(lstItems);
		}

		public  Int32 DeleteSupplierLedgerById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteSupplierLedgerById(lstItems);
		}

		public List<SupplierLedger> GetAllSupplierLedger()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllSupplierLedger(lstItems);
			List<SupplierLedger> SupplierLedgerList = new List<SupplierLedger>();
			foreach (DataRow dr in dt.Rows)
			{
				SupplierLedgerList.Add(GetObject(dr));
			}
			return SupplierLedgerList;
		}

		public SupplierLedger  GetSupplierLedgerById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetSupplierLedgerById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  SupplierLedger GetObject(DataRow dr)
		{

			SupplierLedger objSupplierLedger = new SupplierLedger();
			objSupplierLedger.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objSupplierLedger.SupplierId = (dr["SupplierId"] == DBNull.Value) ? 0 : (Int32)dr["SupplierId"];
			objSupplierLedger.TransactionType = (dr["TransactionType"] == DBNull.Value) ? "" : (String)dr["TransactionType"];
			objSupplierLedger.TransactionDate = (dr["TransactionDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["TransactionDate"];
			objSupplierLedger.SourceId = (dr["SourceId"] == DBNull.Value) ? "" : (String)dr["SourceId"];
			objSupplierLedger.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
			objSupplierLedger.OpeningBalance = (dr["OpeningBalance"] == DBNull.Value) ? 0 : (Decimal)dr["OpeningBalance"];
			objSupplierLedger.Debit = (dr["Debit"] == DBNull.Value) ? 0 : (Decimal)dr["Debit"];
			objSupplierLedger.Cradit = (dr["Cradit"] == DBNull.Value) ? 0 : (Decimal)dr["Cradit"];
			objSupplierLedger.ClosingBalance = (dr["ClosingBalance"] == DBNull.Value) ? 0 : (Decimal)dr["ClosingBalance"];

			return objSupplierLedger;
		}
	}
}
