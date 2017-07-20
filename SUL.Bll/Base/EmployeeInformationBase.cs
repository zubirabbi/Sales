using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class EmployeeInformationBase
	{
		protected static SUL.Dal.EmployeeInformationDal dal = new SUL.Dal.EmployeeInformationDal();

		public System.Int32 Id		{ get ; set; }

		public System.String Code		{ get ; set; }

		public System.String EmployeeName		{ get ; set; }

		public System.DateTime DateOfBirth		{ get ; set; }

		public System.Int32 IdType		{ get ; set; }

		public System.String IdNo		{ get ; set; }

		public System.Int32 Gender		{ get ; set; }

		public System.String Address		{ get ; set; }

		public System.String PostCode		{ get ; set; }

		public System.Int32 Nationality		{ get ; set; }

		public System.Int32 Country		{ get ; set; }

		public System.String BloodGroup		{ get ; set; }

		public System.String Phone		{ get ; set; }

		public System.String Mobile		{ get ; set; }

		public System.String Email		{ get ; set; }

		public System.String Photo		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }

		public System.DateTime JoinDate		{ get ; set; }

		public System.Int32 DepartmentId		{ get ; set; }

		public System.Int32 DesignationId		{ get ; set; }

        public System.Int32 SupervisorId { get; set; }

        public System.DateTime ResignationDate { get; set; }
		public  Int32 InsertEmployeeInformation()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Code", Code);
			lstItems.Add("@EmployeeName", EmployeeName);
			lstItems.Add("@DateOfBirth", DateOfBirth.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IdType", IdType.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IdNo", IdNo);
			lstItems.Add("@Gender", Gender.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Address", Address);
			lstItems.Add("@PostCode", PostCode);
			lstItems.Add("@Nationality", Nationality.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Country", Country.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@BloodGroup", BloodGroup.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Phone", Phone);
			lstItems.Add("@Mobile", Mobile);
			lstItems.Add("@Email", Email);
			lstItems.Add("@Photo", Photo);
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@JoinDate", JoinDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DepartmentId", DepartmentId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DesignationId", DesignationId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@SupervisorId", SupervisorId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ResignationDate", ResignationDate.ToString(CultureInfo.InvariantCulture));

			return dal.InsertEmployeeInformation(lstItems);
		}

		public  Int32 UpdateEmployeeInformation()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@Code", Code);
			lstItems.Add("@EmployeeName", EmployeeName);
			lstItems.Add("@DateOfBirth", DateOfBirth.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IdType", IdType.ToString());
			lstItems.Add("@IdNo", IdNo);
			lstItems.Add("@Gender", Gender.ToString());
			lstItems.Add("@Address", Address);
			lstItems.Add("@PostCode", PostCode);
			lstItems.Add("@Nationality", Nationality.ToString());
			lstItems.Add("@Country", Country.ToString());
			lstItems.Add("@BloodGroup", BloodGroup.ToString());
			lstItems.Add("@Phone", Phone);
			lstItems.Add("@Mobile", Mobile);
			lstItems.Add("@Email", Email);
			lstItems.Add("@Photo", Photo);
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@CompanyId", CompanyId.ToString());
			lstItems.Add("@JoinDate", JoinDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DepartmentId", DepartmentId.ToString());
			lstItems.Add("@DesignationId", DesignationId.ToString());
            lstItems.Add("@SupervisorId", SupervisorId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ResignationDate", ResignationDate.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateEmployeeInformation(lstItems);
		}

		public  Int32 DeleteEmployeeInformationById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteEmployeeInformationById(lstItems);
		}

		public List<EmployeeInformation> GetAllEmployeeInformation(int CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
			DataTable dt = dal.GetAllEmployeeInformation(lstItems);
			List<EmployeeInformation> EmployeeInformationList = new List<EmployeeInformation>();
			foreach (DataRow dr in dt.Rows)
			{
				EmployeeInformationList.Add(GetObject(dr));
			}
			return EmployeeInformationList;
		}

		public EmployeeInformation GetEmployeeInformationById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetEmployeeInformationById(lstItems);
		    if (dt.Rows.Count > 0)
		    {
		        DataRow dr = dt.Rows[0];
                return GetObject(dr);
		    }
		    else
		    {
                return new EmployeeInformation();
		    }
		    
		}

		protected  EmployeeInformation GetObject(DataRow dr)
		{

			EmployeeInformation objEmployeeInformation = new EmployeeInformation();
			objEmployeeInformation.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objEmployeeInformation.Code = (dr["Code"] == DBNull.Value) ? "" : (String)dr["Code"];
			objEmployeeInformation.EmployeeName = (dr["EmployeeName"] == DBNull.Value) ? "" : (String)dr["EmployeeName"];
			objEmployeeInformation.DateOfBirth = (dr["DateOfBirth"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["DateOfBirth"];
			objEmployeeInformation.IdType = (dr["IdType"] == DBNull.Value) ? 0 : (Int32)dr["IdType"];
			objEmployeeInformation.IdNo = (dr["IdNo"] == DBNull.Value) ? "" : (String)dr["IdNo"];
			objEmployeeInformation.Gender = (dr["Gender"] == DBNull.Value) ? 0 : (Int32)dr["Gender"];
			objEmployeeInformation.Address = (dr["Address"] == DBNull.Value) ? "" : (String)dr["Address"];
			objEmployeeInformation.PostCode = (dr["PostCode"] == DBNull.Value) ? "" : (String)dr["PostCode"];
			objEmployeeInformation.Nationality = (dr["Nationality"] == DBNull.Value) ? 0 : (Int32)dr["Nationality"];
			objEmployeeInformation.Country = (dr["Country"] == DBNull.Value) ? 0 : (Int32)dr["Country"];
			objEmployeeInformation.BloodGroup = (dr["BloodGroup"] == DBNull.Value) ? "" : (string)dr["BloodGroup"];
			objEmployeeInformation.Phone = (dr["Phone"] == DBNull.Value) ? "" : (String)dr["Phone"];
			objEmployeeInformation.Mobile = (dr["Mobile"] == DBNull.Value) ? "" : (String)dr["Mobile"];
			objEmployeeInformation.Email = (dr["Email"] == DBNull.Value) ? "" : (String)dr["Email"];
			objEmployeeInformation.Photo = (dr["Photo"] == DBNull.Value) ? "" : (String)dr["Photo"];
			objEmployeeInformation.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];
			objEmployeeInformation.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];
			objEmployeeInformation.JoinDate = (dr["JoinDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["JoinDate"];
			objEmployeeInformation.DepartmentId = (dr["DepartmentId"] == DBNull.Value) ? 0 : (Int32)dr["DepartmentId"];
			objEmployeeInformation.DesignationId = (dr["DesignationId"] == DBNull.Value) ? 0 : (Int32)dr["DesignationId"];
            objEmployeeInformation.SupervisorId = (dr["SupervisorId"] == DBNull.Value) ? 0 : (Int32)dr["SupervisorId"];
            objEmployeeInformation.ResignationDate = (dr["ResignationDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["ResignationDate"];

			return objEmployeeInformation;
		}
	}
}
