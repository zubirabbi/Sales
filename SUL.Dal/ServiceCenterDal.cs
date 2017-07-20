using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ServiceCenterDal : SUL.Dal.Base.ServiceCenterDalBase
	{
		public ServiceCenterDal() : base()
		{
		}
        public DataTable GetServiceCenterFromViewList(Hashtable lstData)
        {

            string whereCondition = " order by Id desc";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewServiceCenterInfo", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
