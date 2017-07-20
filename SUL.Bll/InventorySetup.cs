using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class InventorySetup : SUL.Bll.Base.InventorySetupBase
	{
		private static SUL.Dal.InventorySetupDal Dal = new SUL.Dal.InventorySetupDal();
		public InventorySetup() : base()
		{
		}
	}
}
