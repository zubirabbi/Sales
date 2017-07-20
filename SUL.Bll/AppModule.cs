using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class AppModule : SUL.Bll.Base.AppModuleBase
	{
		private static SUL.Dal.AppModuleDal Dal = new SUL.Dal.AppModuleDal();
		public AppModule() : base()
		{
		}
	}
}
