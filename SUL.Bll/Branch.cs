using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class Branch : SUL.Bll.Base.BranchBase
	{
		private static SUL.Dal.BranchDal Dal = new SUL.Dal.BranchDal();
		public Branch() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public DataTable GetAllBranchbyCompanyId(int CompanyId)
	    {
	        Hashtable lstItems = new Hashtable();
	        lstItems.Add("@CompanyId", CompanyId);
	        return dal.GetAllBranchbyCompanyId(lstItems);
	    }
	}
}
