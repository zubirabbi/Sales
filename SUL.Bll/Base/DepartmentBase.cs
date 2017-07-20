using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class DepartmentBase
	{
		protected static SUL.Dal.DepartmentDal dal = new SUL.Dal.DepartmentDal();

		public System.Int32 Id		{ get ; set; }

		public System.String DepartmentName		{ get ; set; }

		public System.Int32 PerentDepartmentId		{ get ; set; }

		public System.String Location		{ get ; set; }

		public System.Int32 InchargeId		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }


		public  Int32 InsertDepartment()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@DepartmentName", DepartmentName);
			lstItems.Add("@PerentDepartmentId", PerentDepartmentId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Location", Location);
			lstItems.Add("@InchargeId", InchargeId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertDepartment(lstItems);
		}

		public  Int32 UpdateDepartment()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@DepartmentName", DepartmentName);
			lstItems.Add("@PerentDepartmentId", PerentDepartmentId.ToString());
			lstItems.Add("@Location", Location);
			lstItems.Add("@InchargeId", InchargeId.ToString());
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@CompanyId", CompanyId.ToString());

			return dal.UpdateDepartment(lstItems);
		}

		public  Int32 DeleteDepartmentById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteDepartmentById(lstItems);
		}

		public List<Department> GetAllDepartment(int CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
			DataTable dt = dal.GetAllDepartment(lstItems);
			List<Department> DepartmentList = new List<Department>();
			foreach (DataRow dr in dt.Rows)
			{
				DepartmentList.Add(GetObject(dr));
			}
			return DepartmentList;
		}

		public Department GetDepartmentById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetDepartmentById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  Department GetObject(DataRow dr)
		{

			Department objDepartment = new Department();
			objDepartment.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objDepartment.DepartmentName = (dr["DepartmentName"] == DBNull.Value) ? "" : (String)dr["DepartmentName"];
			objDepartment.PerentDepartmentId = (dr["PerentDepartmentId"] == DBNull.Value) ? 0 : (Int32)dr["PerentDepartmentId"];
			objDepartment.Location = (dr["Location"] == DBNull.Value) ? "" : (String)dr["Location"];
			objDepartment.InchargeId = (dr["InchargeId"] == DBNull.Value) ? 0 : (Int32)dr["InchargeId"];
			objDepartment.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];
			objDepartment.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];

			return objDepartment;
		}
	}
}
