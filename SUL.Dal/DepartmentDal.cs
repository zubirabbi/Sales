using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
    public class DepartmentDal : SUL.Dal.Base.DepartmentDalBase
    {
        public DepartmentDal()
            : base()
        {
        }
        public DataTable GetAllDepartmentbyCompanyId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewDepartment", "*", " Where CompanyId = @CompanyId;", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int ChecDepartmentExistance(Hashtable lstData, bool isNewEntry)
        {

            string whereCondition = string.Empty;

            if (isNewEntry)
                whereCondition = " where Department.DepartmentName = @DepartmentName";
            else
                whereCondition = "where Department.DepartmentName = @DepartmentName And Department.Id <> @Id";

            int count = 0;
            try
            {
                count = CheckExistence("Department", "Id", whereCondition, lstData);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetEmployeeDepartment(Hashtable lstData)
        {

            try
            {
                DataTable dt = new DataTable();
                string whereCondition = " where Id in (select DepartmentId from EmployeeInformation where Id = @Id)";
                dt = GetDataTable("Department", "*", whereCondition, lstData);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
