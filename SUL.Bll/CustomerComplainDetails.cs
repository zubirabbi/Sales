using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class CustomerComplainDetails : SUL.Bll.Base.CustomerComplainDetailsBase
	{
		private static SUL.Dal.CustomerComplainDetailsDal Dal = new SUL.Dal.CustomerComplainDetailsDal();
		public CustomerComplainDetails() : base()
		{
		}
	}
}
