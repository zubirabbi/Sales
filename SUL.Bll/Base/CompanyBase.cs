using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class CompanyBase
	{
		protected static SUL.Dal.CompanyDal dal = new SUL.Dal.CompanyDal();

		public System.Int32 Id		{ get ; set; }

		public System.String CompanyName		{ get ; set; }

		public System.String CompanyCode		{ get ; set; }

		public System.String Address		{ get ; set; }

		public System.DateTime EstablishDate		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }

		public System.String Phone		{ get ; set; }

		public System.String Email		{ get ; set; }

		public System.String Web		{ get ; set; }

		public System.Boolean IsParentCompany		{ get ; set; }

		public System.String CompanyLogo		{ get ; set; }

        public System.String Address2 { get; set; }


		public  Int32 InsertCompany()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyName", CompanyName);
			lstItems.Add("@CompanyCode", CompanyCode);
			lstItems.Add("@Address", Address);
			lstItems.Add("@EstablishDate", EstablishDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@Phone", Phone);
			lstItems.Add("@Email", Email);
			lstItems.Add("@Web", Web);
			lstItems.Add("@IsParentCompany", IsParentCompany);
			lstItems.Add("@CompanyLogo", CompanyLogo);
            lstItems.Add("@Address2", Address2);

			return dal.InsertCompany(lstItems);
		}

		public  Int32 UpdateCompany()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@CompanyName", CompanyName);
			lstItems.Add("@CompanyCode", CompanyCode);
			lstItems.Add("@Address", Address);
			lstItems.Add("@EstablishDate", EstablishDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@Phone", Phone);
			lstItems.Add("@Email", Email);
			lstItems.Add("@Web", Web);
			lstItems.Add("@IsParentCompany", IsParentCompany);
			lstItems.Add("@CompanyLogo", CompanyLogo);
            lstItems.Add("@Address2", Address2);

			return dal.UpdateCompany(lstItems);
		}

		public  Int32 DeleteCompanyById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteCompanyById(lstItems);
		}

		public List<Company> GetAllCompany(int CompanyLogo)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyLogo", CompanyLogo);
			DataTable dt = dal.GetAllCompany(lstItems);
			List<Company> CompanyList = new List<Company>();
			foreach (DataRow dr in dt.Rows)
			{
				CompanyList.Add(GetObject(dr));
			}
			return CompanyList;
		}

		public Company GetCompanyById(int _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetCompanyById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  Company GetObject(DataRow dr)
		{

			Company objCompany = new Company();
			objCompany.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objCompany.CompanyName = (dr["CompanyName"] == DBNull.Value) ? "" : (String)dr["CompanyName"];
			objCompany.CompanyCode = (dr["CompanyCode"] == DBNull.Value) ? "" : (String)dr["CompanyCode"];
			objCompany.Address = (dr["Address"] == DBNull.Value) ? "" : (String)dr["Address"];
			objCompany.EstablishDate = (dr["EstablishDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["EstablishDate"];
			objCompany.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];
			objCompany.Phone = (dr["Phone"] == DBNull.Value) ? "" : (String)dr["Phone"];
			objCompany.Email = (dr["Email"] == DBNull.Value) ? "" : (String)dr["Email"];
			objCompany.Web = (dr["Web"] == DBNull.Value) ? "" : (String)dr["Web"];
			objCompany.IsParentCompany = (dr["IsParentCompany"] == DBNull.Value) ? false : (Boolean)dr["IsParentCompany"];
			objCompany.CompanyLogo = (dr["CompanyLogo"] == DBNull.Value) ? "" : (String)dr["CompanyLogo"];
            objCompany.Address2 = (dr["Address2"] == DBNull.Value) ? "" : (String)dr["Address2"];

			return objCompany;
		}
	}
}
