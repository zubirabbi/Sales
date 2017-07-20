using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class BranchDal : SUL.Dal.Base.BranchDalBase
	{
		public BranchDal() : base()
		{
		}
        public DataTable GetAllBranchbyCompanyId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewBranchInfo", "*", " Where CompanyId = @CompanyId And IsActive = 1;", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
