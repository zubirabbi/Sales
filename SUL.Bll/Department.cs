using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class Department : SUL.Bll.Base.DepartmentBase
	{
		private static SUL.Dal.DepartmentDal Dal = new SUL.Dal.DepartmentDal();
		public Department() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public DataTable GetAllDepartmentbyCompanyId(int CompanyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CompanyId", CompanyId);
            return dal.GetAllDepartmentbyCompanyId(lstItems);
        }
        public int ChecDepartmentExistance(int _Id, string _Deptname, bool isNewEntry)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DepartmentName", _Deptname);

            if (!isNewEntry) lstItems.Add("@Id", _Id);

            return dal.ChecDepartmentExistance(lstItems, isNewEntry);
        }
        /// <summary>
        /// created by zobayer on 20/04/2015
        /// </summary>
        /// <param name="_employeeId"></param>
        /// <returns></returns>
        public Department GetEmployeeDepartment(int _employeeId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _employeeId);
            DataTable dt = dal.GetEmployeeDepartment(lstItems);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                return GetObject(dr);
            }
            else
            {
                return new Department();
            }
        }
	}
}
