using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class BranchBase
	{
		protected static SUL.Dal.BranchDal dal = new SUL.Dal.BranchDal();

		public System.Int32 Id		{ get ; set; }

		public System.String BranchCode		{ get ; set; }

		public System.String BranchName		{ get ; set; }

		public System.String Location		{ get ; set; }

		public System.Int32 InchargeId		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }


		public  Int32 InsertBranch()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@BranchCode", BranchCode);
			lstItems.Add("@BranchName", BranchName);
			lstItems.Add("@Location", Location);
			lstItems.Add("@InchargeId", InchargeId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertBranch(lstItems);
		}

		public  Int32 UpdateBranch()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@BranchCode", BranchCode);
			lstItems.Add("@BranchName", BranchName);
			lstItems.Add("@Location", Location);
			lstItems.Add("@InchargeId", InchargeId.ToString());
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@CompanyId", CompanyId.ToString());

			return dal.UpdateBranch(lstItems);
		}

		public  Int32 DeleteBranchById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteBranchById(lstItems);
		}

		public List<Branch> GetAllBranch(int CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
			DataTable dt = dal.GetAllBranch(lstItems);
			List<Branch> BranchList = new List<Branch>();
			foreach (DataRow dr in dt.Rows)
			{
				BranchList.Add(GetObject(dr));
			}
			return BranchList;
		}

		public Branch GetBranchById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetBranchById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  Branch GetObject(DataRow dr)
		{

			Branch objBranch = new Branch();
			objBranch.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objBranch.BranchCode = (dr["BranchCode"] == DBNull.Value) ? "" : (String)dr["BranchCode"];
			objBranch.BranchName = (dr["BranchName"] == DBNull.Value) ? "" : (String)dr["BranchName"];
			objBranch.Location = (dr["Location"] == DBNull.Value) ? "" : (String)dr["Location"];
			objBranch.InchargeId = (dr["InchargeId"] == DBNull.Value) ? 0 : (Int32)dr["InchargeId"];
			objBranch.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];
			objBranch.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];

			return objBranch;
		}
	}
}
