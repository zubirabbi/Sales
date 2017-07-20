using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class Designation : SUL.Bll.Base.DesignationBase
	{
		private static SUL.Dal.DesignationDal Dal = new SUL.Dal.DesignationDal();
		public Designation() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public DataTable GetAllDesignationbyCompanyId(int CompanyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CompanyId", CompanyId);
            return dal.GetAllDesignationbyCompanyId(lstItems);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListId"></param>
        /// <returns></returns>
        public DataTable GetDesignationByLeveId(int ListId)
        {
            Hashtable lstData = new Hashtable();
            lstData.Add("@ListId", ListId);

            DataTable dt = dal.GetDesignationByLeveId(lstData);
            return dt;
        }
        public DataTable GetAllDesignationByDeptId( int DepartmentId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DepartmentId", DepartmentId);
            return dal.GetAllDesignationByDeptId(lstItems);
        }
        public int CheckDesigExistance(int _Id, string _Designame, int deptId,bool isNewEntry)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DesignationName", _Designame);
            lstItems.Add("@DepartmentId", deptId);

            if (!isNewEntry) lstItems.Add("@Id", _Id);

            return dal.CheckDesigExistance(lstItems, isNewEntry);
        }
	}
}
