using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class SupplierBase
	{
		protected static SUL.Dal.SupplierDal dal = new SUL.Dal.SupplierDal();

		public System.Int32 Id		{ get ; set; }

		public System.String Name		{ get ; set; }

		public System.String Code		{ get ; set; }

		public System.String CompanyAddress		{ get ; set; }

		public System.String FactoryAddress		{ get ; set; }

		public System.String Phone		{ get ; set; }

        public System.String Mobile { get; set; }

		public System.String Email		{ get ; set; }

		public System.String ContactPerson		{ get ; set; }

		public System.String Designation		{ get ; set; }

		public System.Decimal TotalDebit		{ get ; set; }

		public System.Decimal TotalCredit		{ get ; set; }

		public System.Decimal Balance		{ get ; set; }


		public  Int32 InsertSupplier()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Name", Name);
			lstItems.Add("@Code", Code);
			lstItems.Add("@CompanyAddress", CompanyAddress);
			lstItems.Add("@FactoryAddress", FactoryAddress);
			lstItems.Add("@Phone", Phone.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Mobile", Mobile.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Email", Email);
			lstItems.Add("@ContactPerson", ContactPerson);
			lstItems.Add("@Designation", Designation);
			lstItems.Add("@TotalDebit", TotalDebit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TotalCredit", TotalCredit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Balance", Balance.ToString(CultureInfo.InvariantCulture));

			return dal.InsertSupplier(lstItems);
		}

		public  Int32 UpdateSupplier()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@Name", Name);
			lstItems.Add("@Code", Code);
			lstItems.Add("@CompanyAddress", CompanyAddress);
			lstItems.Add("@FactoryAddress", FactoryAddress);
			lstItems.Add("@Phone", Phone.ToString());
			lstItems.Add("@Mobile", Mobile.ToString());
			lstItems.Add("@Email", Email);
			lstItems.Add("@ContactPerson", ContactPerson);
			lstItems.Add("@Designation", Designation);
			lstItems.Add("@TotalDebit", TotalDebit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@TotalCredit", TotalCredit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Balance", Balance.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateSupplier(lstItems);
		}

		public  Int32 DeleteSupplierById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteSupplierById(lstItems);
		}

		public List<Supplier> GetAllSupplier()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllSupplier(lstItems);
			List<Supplier> SupplierList = new List<Supplier>();
			foreach (DataRow dr in dt.Rows)
			{
				SupplierList.Add(GetObject(dr));
			}
			return SupplierList;
		}

		public Supplier GetSupplierById(int _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetSupplierById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  Supplier GetObject(DataRow dr)
		{

			Supplier objSupplier = new Supplier();
			objSupplier.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objSupplier.Name = (dr["Name"] == DBNull.Value) ? "" : (String)dr["Name"];
			objSupplier.Code = (dr["Code"] == DBNull.Value) ? "" : (String)dr["Code"];
			objSupplier.CompanyAddress = (dr["CompanyAddress"] == DBNull.Value) ? "" : (String)dr["CompanyAddress"];
			objSupplier.FactoryAddress = (dr["FactoryAddress"] == DBNull.Value) ? "" : (String)dr["FactoryAddress"];
			objSupplier.Phone = (dr["Phone"] == DBNull.Value) ? "0" : (String)dr["Phone"];
            objSupplier.Mobile = (dr["Mobile"] == DBNull.Value) ? "0" : (String)dr["Mobile"];
			objSupplier.Email = (dr["Email"] == DBNull.Value) ? "" : (String)dr["Email"];
			objSupplier.ContactPerson = (dr["ContactPerson"] == DBNull.Value) ? "" : (String)dr["ContactPerson"];
			objSupplier.Designation = (dr["Designation"] == DBNull.Value) ? "" : (String)dr["Designation"];
			objSupplier.TotalDebit = (dr["TotalDebit"] == DBNull.Value) ? 0 : (Decimal)dr["TotalDebit"];
			objSupplier.TotalCredit = (dr["TotalCredit"] == DBNull.Value) ? 0 : (Decimal)dr["TotalCredit"];
			objSupplier.Balance = (dr["Balance"] == DBNull.Value) ? 0 : (Decimal)dr["Balance"];

			return objSupplier;
		}
	}
}
