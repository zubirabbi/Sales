using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ServiceDal : SUL.Dal.Base.ServiceDalBase
	{
		public ServiceDal() : base()
		{
		}
        public DataTable GetServiceCreationFromViewList(Hashtable lstData)
        {

            string whereCondition = " order by Id desc";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewService", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
