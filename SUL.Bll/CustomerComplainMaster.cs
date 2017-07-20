using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class CustomerComplainMaster : SUL.Bll.Base.CustomerComplainMasterBase
	{
		private static SUL.Dal.CustomerComplainMasterDal Dal = new SUL.Dal.CustomerComplainMasterDal();
		public CustomerComplainMaster() : base()
		{
		}
	}
}
