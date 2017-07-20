using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class EmployeeInformationDal : SUL.Dal.Base.EmployeeInformationDalBase
	{
		public EmployeeInformationDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetEmpListFromViewList(Hashtable lstData)
        {

            string whereCondition = " where CompanyId = @CompanyId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewEmpInfo", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetEmpListFromViewListbyempId(Hashtable lstData)
        {

            string whereCondition = " where Id = @Id And CompanyId = @CompanyId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewEmpInfo", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Created By Jakia
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetEmpListFromViewSales(Hashtable lstData)
        {

            string whereCondition = @" WHERE DepartmentName= 'Sales' And CompanyId = @CompanyId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewEmpInfo", "*", whereCondition, lstData);
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
        public int GetlastEmpCode(Hashtable lstData)
        {

            try
            {
                string whereCondition = " where CompanyId = @CompanyId";
                int maxId = GetMaximumIDbyCondition("EmployeeInformation", "cast(right(Code,4) as int)", 0, whereCondition, lstData);

                return maxId;
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
       

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetlastEmpCode()
        {

            try
            {
                int maxId = GetMaximumID("EmployeePersonalInformation", "cast(right(Code,4) as int)", 0, "");

                return maxId;
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
        public DataTable GetAllEmployeeInformationByDesignationId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("EmployeeInformation", "*", " Where DesignationId = @DesignationId ;", lstData);
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
        public DataTable GetAllEmployeeInformationByDesignationIdbyTable(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewEmpInfo", "*", " Where DesignationId = @DesignationId ", lstData);
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
        /// <param name="isNewentry"></param>
        /// <returns></returns>
        public int CheckForCodeExist(Hashtable lstData, bool isNewentry)
        {
            string whereCondition = string.Empty;
            whereCondition = isNewentry ? " where Code = @Code " : " where Code = @Code and Id != @Id";

            try
            {
                return CheckExistence("EmployeeInformation", "Id", whereCondition, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
