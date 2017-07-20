using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class IncentiveSetup : SUL.Bll.Base.IncentiveSetupBase
	{
		private static SUL.Dal.IncentiveSetupDal Dal = new SUL.Dal.IncentiveSetupDal();
		public IncentiveSetup() : base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
	    public IncentiveSetup GetDealerMaster()
	    {
	        DataTable dt = dal.GetDealerMaster();
	        if (dt.Rows.Count > 0)
	        {
	            DataRow dr = dt.Rows[0];
	            return GetObject(dr);
	        }
	        else
	        {
	            return new IncentiveSetup();
	        }
	    }

	    public int GetIncentiveSetupMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetIncentiveSetupMasterId(lstItems);
        }
	}
}
