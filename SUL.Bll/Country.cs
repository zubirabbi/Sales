using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class Country : SUL.Bll.Base.CountryBase
	{
		private static SUL.Dal.CountryDal Dal = new SUL.Dal.CountryDal();
		public Country() : base()
		{
		}
	}
}
