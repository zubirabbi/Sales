using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ServiceCenter : SUL.Bll.Base.ServiceCenterBase
	{
		private static SUL.Dal.ServiceCenterDal Dal = new SUL.Dal.ServiceCenterDal();
		public ServiceCenter() : base()
		{
		}
        public DataTable GetServiceCenterFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetServiceCenterFromViewList(lstItems);
            return dt;
        }
	}
}
