using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class DeliveryDetails : SUL.Bll.Base.DeliveryDetailsBase
	{
		private static SUL.Dal.DeliveryDetailsDal Dal = new SUL.Dal.DeliveryDetailsDal();
		public DeliveryDetails() : base()
		{
		}
	}
}
