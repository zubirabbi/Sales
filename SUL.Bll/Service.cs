using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class Service : SUL.Bll.Base.ServiceBase
	{
		private static SUL.Dal.ServiceDal Dal = new SUL.Dal.ServiceDal();
		public Service() : base()
		{
		}
        public DataTable GetServiceCreationFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetServiceCreationFromViewList(lstItems);
            return dt;
        }
	}
}
