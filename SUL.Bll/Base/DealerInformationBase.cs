using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class DealerInformationBase
	{
		protected static SUL.Dal.DealerInformationDal dal = new SUL.Dal.DealerInformationDal();

		public System.Int64 Id		{ get ; set; }

		public System.String DealerName		{ get ; set; }

		public System.String DealerCode		{ get ; set; }

		public System.Int32 Area		{ get ; set; }

		public System.String Address		{ get ; set; }

		public System.Int32 CS		{ get ; set; }

		public System.String ProprietorName		{ get ; set; }

        public System.String Phone { get; set; }

        public System.String Mobile { get; set; }

		public System.String Email		{ get ; set; }

		public System.Int32 DealerCategory		{ get ; set; }

		public System.DateTime StartDate		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }

		public System.Decimal TotalDebit		{ get ; set; }

		public System.Decimal TotalCredit		{ get ; set; }

		public System.Decimal Balance		{ get ; set; }

        public System.Int32 JrCS { get; set; }

	    public System.String WareHouseCode { get; set; }

        public System.Decimal CreditLimit { get; set; }

	    public  Int32 InsertDealerInformation()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@DealerName", DealerName);
			lstItems.Add("@DealerCode", DealerCode);
			lstItems.Add("@Area", Area.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Address", Address);
			lstItems.Add("@CS", CS.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProprietorName", ProprietorName);
			lstItems.Add("@Phone", Phone.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Mobile", Mobile.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Email", Email);
			lstItems.Add("@DealerCategory", DealerCategory.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@StartDate", StartDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@TotalDebit", TotalDebit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TotalCredit", TotalCredit.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@JrCS", JrCS.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@WareHouseCode", WareHouseCode);
            lstItems.Add("@CreditLimit", CreditLimit.ToString(CultureInfo.InvariantCulture));

			return dal.InsertDealerInformation(lstItems);
		}

		public  Int32 UpdateDealerInformation()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@DealerName", DealerName);
			lstItems.Add("@DealerCode", DealerCode);
			lstItems.Add("@Area", Area.ToString());
			lstItems.Add("@Address", Address);
			lstItems.Add("@CS", CS.ToString());
			lstItems.Add("@ProprietorName", ProprietorName);
			lstItems.Add("@Phone", Phone.ToString());
			lstItems.Add("@Mobile", Mobile.ToString());
			lstItems.Add("@Email", Email);
			lstItems.Add("@DealerCategory", DealerCategory.ToString());
			lstItems.Add("@StartDate", StartDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);
            lstItems.Add("@JrCS", JrCS.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@WareHouseCode", WareHouseCode);
            lstItems.Add("@CreditLimit", CreditLimit.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateDealerInformation(lstItems);
		}

		public  Int32 DeleteDealerInformationById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteDealerInformationById(lstItems);
		}

		public List<DealerInformation> GetAllDealerInformation()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllDealerInformation(lstItems);
			List<DealerInformation> DealerInformationList = new List<DealerInformation>();
			foreach (DataRow dr in dt.Rows)
			{
				DealerInformationList.Add(GetObject(dr));
			}
			return DealerInformationList;
		}

	    public DealerInformation GetDealerInformationById(Int64 _Id)
	    {
	        Hashtable lstItems = new Hashtable();
	        lstItems.Add("@Id", _Id);

	        DataTable dt = dal.GetDealerInformationById(lstItems);
	        if (dt.Rows.Count > 0)
	        {
	            DataRow dr = dt.Rows[0];
	            return GetObject(dr);
	        }
            return  new DealerInformation();
	    }

	    protected  DealerInformation GetObject(DataRow dr)
		{

			DealerInformation objDealerInformation = new DealerInformation();
			objDealerInformation.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objDealerInformation.DealerName = (dr["DealerName"] == DBNull.Value) ? "" : (String)dr["DealerName"];
			objDealerInformation.DealerCode = (dr["DealerCode"] == DBNull.Value) ? "" : (String)dr["DealerCode"];
			objDealerInformation.Area = (dr["Area"] == DBNull.Value) ? 0 : (Int32)dr["Area"];
			objDealerInformation.Address = (dr["Address"] == DBNull.Value) ? "" : (String)dr["Address"];
			objDealerInformation.CS = (dr["CS"] == DBNull.Value) ? 0 : (Int32)dr["CS"];
			objDealerInformation.ProprietorName = (dr["ProprietorName"] == DBNull.Value) ? "" : (String)dr["ProprietorName"];
            objDealerInformation.Phone = (dr["Phone"] == DBNull.Value) ? "" : (String)dr["Phone"];
            objDealerInformation.Mobile = (dr["Mobile"] == DBNull.Value) ? "" : (String)dr["Mobile"];
			objDealerInformation.Email = (dr["Email"] == DBNull.Value) ? "" : (String)dr["Email"];
			objDealerInformation.DealerCategory = (dr["DealerCategory"] == DBNull.Value) ? 0 : (Int32)dr["DealerCategory"];
			objDealerInformation.StartDate = (dr["StartDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["StartDate"];
			objDealerInformation.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];
			objDealerInformation.TotalDebit = (dr["TotalDebit"] == DBNull.Value) ? 0 : (Decimal)dr["TotalDebit"];
			objDealerInformation.TotalCredit = (dr["TotalCredit"] == DBNull.Value) ? 0 : (Decimal)dr["TotalCredit"];
			objDealerInformation.Balance = (dr["Balance"] == DBNull.Value) ? 0 : (Decimal)dr["Balance"];
            objDealerInformation.JrCS = (dr["JrCS"] == DBNull.Value) ? 0 : (Int32)dr["JrCS"];
            objDealerInformation.WareHouseCode = (dr["WareHouseCode"] == DBNull.Value) ? "" : (String)dr["WareHouseCode"];
            objDealerInformation.CreditLimit = (dr["CreditLimit"] == DBNull.Value) ? 0 : (Decimal)dr["CreditLimit"];

			return objDealerInformation;
		}
	}
}
