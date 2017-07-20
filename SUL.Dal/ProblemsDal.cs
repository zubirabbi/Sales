using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ProblemsDal : SUL.Dal.Base.ProblemsDalBase
	{
		public ProblemsDal() : base()
		{
		}
        public DataTable GetCustomerProblemFromViewListTable(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewCustomerProblem", "*", "", lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
