using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class JournalMaster : SUL.Bll.Base.JournalMasterBase
	{
		private static SUL.Dal.JournalMasterDal Dal = new SUL.Dal.JournalMasterDal();
		public JournalMaster() : base()
		{
		}
	}
}
