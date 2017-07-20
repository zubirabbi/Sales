using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class WarehouseCategory : SUL.Bll.Base.WarehouseCategoryBase
	{
		private static SUL.Dal.WarehouseCategoryDal Dal = new SUL.Dal.WarehouseCategoryDal();
		public WarehouseCategory() : base()
		{
		}
	}
}
