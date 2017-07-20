using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
    public class DesignationDal : SUL.Dal.Base.DesignationDalBase
    {
        public DesignationDal()
            : base()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetAllDesignationbyCompanyId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewDesignation", "*", " Where CompanyId = @CompanyId And IsActive = 1;", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetDesignationByLeveId(Hashtable lstData)
        {
            string queryString = @"select * from viewDesignationLevel where Level in (select ListId from ListTable where ListType='Level' and ListId< @ListId)  ";

            try
            {
                return GetDataTable(queryString, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }

        public DataTable GetAllDesignationByDeptId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("Designation", "*", " Where Designation.DepartmentId = @DepartmentId And Designation.IsActive = 1;", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CheckDesigExistance(Hashtable lstData, bool isNewEntry)
        {

            string whereCondition = string.Empty;

            if (isNewEntry)
                whereCondition = " where Designation.DesignationName = @DesignationName And Designation.DepartmentId = @DepartmentId";
            else
                whereCondition = "where Designation.DesignationName = @DesignationName And Designation.DepartmentId = @DepartmentId And Designation.Id <> @Id";

            int count = 0;
            try
            {
                count = CheckExistence("Designation", "Id", whereCondition, lstData);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
