using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class IncentiveSetupDal : SUL.Dal.Base.IncentiveSetupDalBase
	{
		public IncentiveSetupDal() : base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
	    public DataTable GetDealerMaster()
	    {
	        return GetDataTable("IncentiveSetup", "*", " where isActive = 1 and Type = 2");
	    }

	    public int GetIncentiveSetupMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("IncentiveSetup", "Id", 1, "", lstData);
        }
	}
}
