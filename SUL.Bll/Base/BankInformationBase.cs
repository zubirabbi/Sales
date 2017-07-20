using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class BankInformationBase
	{
		protected static SUL.Dal.BankInformationDal dal = new SUL.Dal.BankInformationDal();

		public System.Int32 Id		{ get ; set; }

		public System.String Type		{ get ; set; }

		public System.Int32 TypeId		{ get ; set; }

		public System.String BankName		{ get ; set; }

		public System.String BranchName		{ get ; set; }

		public System.String AccountNo		{ get ; set; }

		public System.String AccountTitle		{ get ; set; }

		public System.String SWIFTCode		{ get ; set; }

		public System.Int32 Country		{ get ; set; }

		public System.Boolean IsDefault		{ get ; set; }

        public System.String ShortName { get; set; }

		public  Int32 InsertBankInformation()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Type", Type);
			lstItems.Add("@TypeId", TypeId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@BankName", BankName);
			lstItems.Add("@BranchName", BranchName);
			lstItems.Add("@AccountNo", AccountNo);
			lstItems.Add("@AccountTitle", AccountTitle);
			lstItems.Add("@SWIFTCode", SWIFTCode);
			lstItems.Add("@Country", Country.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsDefault", IsDefault);
            lstItems.Add("@ShortName", ShortName);

			return dal.InsertBankInformation(lstItems);
		}

		public  Int32 UpdateBankInformation()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@Type", Type);
			lstItems.Add("@TypeId", TypeId.ToString());
			lstItems.Add("@BankName", BankName);
			lstItems.Add("@BranchName", BranchName);
			lstItems.Add("@AccountNo", AccountNo);
			lstItems.Add("@AccountTitle", AccountTitle);
			lstItems.Add("@SWIFTCode", SWIFTCode);
			lstItems.Add("@Country", Country.ToString());
			lstItems.Add("@IsDefault", IsDefault);
            lstItems.Add("@ShortName", ShortName);

			return dal.UpdateBankInformation(lstItems);
		}

		public  Int32 DeleteBankInformationById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteBankInformationById(lstItems);
		}

		public List<BankInformation> GetAllBankInformation()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllBankInformation(lstItems);
			List<BankInformation> BankInformationList = new List<BankInformation>();
			foreach (DataRow dr in dt.Rows)
			{
				BankInformationList.Add(GetObject(dr));
			}
			return BankInformationList;
		}

		public BankInformation  GetBankInformationById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetBankInformationById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  BankInformation GetObject(DataRow dr)
		{

			BankInformation objBankInformation = new BankInformation();
			objBankInformation.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objBankInformation.Type = (dr["Type"] == DBNull.Value) ? "" : (String)dr["Type"];
			objBankInformation.TypeId = (dr["TypeId"] == DBNull.Value) ? 0 : (Int32)dr["TypeId"];
			objBankInformation.BankName = (dr["BankName"] == DBNull.Value) ? "" : (String)dr["BankName"];
			objBankInformation.BranchName = (dr["BranchName"] == DBNull.Value) ? "" : (String)dr["BranchName"];
			objBankInformation.AccountNo = (dr["AccountNo"] == DBNull.Value) ? "" : (String)dr["AccountNo"];
			objBankInformation.AccountTitle = (dr["AccountTitle"] == DBNull.Value) ? "" : (String)dr["AccountTitle"];
			objBankInformation.SWIFTCode = (dr["SWIFTCode"] == DBNull.Value) ? "" : (String)dr["SWIFTCode"];
			objBankInformation.Country = (dr["Country"] == DBNull.Value) ? 0 : (Int32)dr["Country"];
			objBankInformation.IsDefault = (dr["IsDefault"] == DBNull.Value) ? false : (Boolean)dr["IsDefault"];
            objBankInformation.ShortName = (dr["ShortName"] == DBNull.Value) ? "" : (String)dr["ShortName"];

			return objBankInformation;
		}
	}
}
