using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class CourierInformation : SUL.Bll.Base.CourierInformationBase
	{
		private static SUL.Dal.CourierInformationDal Dal = new SUL.Dal.CourierInformationDal();
		public CourierInformation() : base()
		{
		}
        public DataTable GetTransportViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetTransportViewList(lstItems);
            return dt;
        }
	}
}
