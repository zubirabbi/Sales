using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ItemDetails : SUL.Bll.Base.ItemDetailsBase
	{
		private static SUL.Dal.ItemDetailsDal Dal = new SUL.Dal.ItemDetailsDal();
		public ItemDetails() : base()
		{
		}
	}
}
