using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class EmployeeInformation : SUL.Bll.Base.EmployeeInformationBase
	{
		private static SUL.Dal.EmployeeInformationDal Dal = new SUL.Dal.EmployeeInformationDal();
		public EmployeeInformation() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public DataTable GetEmpListFromViewList(int CompanyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CompanyId", CompanyId);

            DataTable dt = dal.GetEmpListFromViewList(lstItems);
            return dt;
        }
        public DataTable GetEmpListFromViewListbyempId(int EmpId, int CompanyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", EmpId);
            lstItems.Add("@CompanyId", CompanyId);

            DataTable dt = dal.GetEmpListFromViewListbyempId(lstItems);
            return dt;
        }
        /// <summary>
        /// Created By Jakia
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public DataTable GetEmpListFromViewSales(int CompanyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CompanyId", CompanyId);

            DataTable dt = dal.GetEmpListFromViewSales(lstItems);
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_companyId"></param>
        /// <returns></returns>
        public string GetMaxEmpCode(int _companyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CompanyId", _companyId);
            return dal.GetlastEmpCode(lstItems).ToString();
        }


        /// <summary>
        /// created by zobayer on 20/04/2015
        /// </summary>
        /// <param name="_employeeId"></param>
        /// <returns></returns>
        //public string GetEmployeeDepartment(int _employeeId)
        //{
        //    Hashtable lstItems = new Hashtable();
        //    lstItems.Add("@Id", _employeeId);
        //    return dal.GetEmployeeDepartment(lstItems).ToString();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public string GetlastEmpCode(int companyId)
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();
            if (companyId == 0)
                id = dal.GetlastEmpCode();
            else
            {
               
                lstItems.Add("@CompanyId", companyId);
                id = dal.GetlastEmpCode(lstItems);
            }

            id = id + 1;
            string maxIdS = id.ToString();
            int count = maxIdS.Length;
            while (count < 4)
            {
                maxIdS = "0" + maxIdS;
                count++;
            }

            if (companyId == 0)
                companyId = new Company().GetParentCompany().Id;
            string comCode = new Company().GetCompanyById(companyId).CompanyCode;

            maxIdS = comCode + maxIdS;
            return maxIdS;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="designation"></param>
        /// <returns></returns>
        public List<EmployeeInformation> GetAllEmployeeInformationByDesignationId(int designation)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DesignationId", designation);
            DataTable dt = dal.GetAllEmployeeInformationByDesignationId(lstItems);
            List<EmployeeInformation> EmployeeInformationList = new List<EmployeeInformation>();
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeInformationList.Add(GetObject(dr));
            }
            return EmployeeInformationList;
        }
        public DataTable GetAllEmployeeInformationByDesignationIdbyTable(int designation)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DesignationId", designation);
            return dal.GetAllEmployeeInformationByDesignationIdbyTable(lstItems);
           
           
        }

        public int CheckForCodeExist(string _code, bool isNewEntry, int _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Code", _code);

            if (!isNewEntry)
                lstItems.Add("@Id", _Id);

            return dal.CheckForCodeExist(lstItems, isNewEntry);
        }
	}
}
