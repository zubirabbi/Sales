using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class SalesIncentiveSetup : SUL.Bll.Base.SalesIncentiveSetupBase
	{
		private static SUL.Dal.SalesIncentiveSetupDal Dal = new SUL.Dal.SalesIncentiveSetupDal();
		public SalesIncentiveSetup() : base()
		{
		}
	}
}
