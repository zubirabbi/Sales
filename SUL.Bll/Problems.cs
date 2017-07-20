using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class Problems : SUL.Bll.Base.ProblemsBase
	{
		private static SUL.Dal.ProblemsDal Dal = new SUL.Dal.ProblemsDal();
		public Problems() : base()
		{
		}
        public DataTable GetCustomerProblemFromViewListTable()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetCustomerProblemFromViewListTable(lstItems);
            return dt;
        }
	}
}
