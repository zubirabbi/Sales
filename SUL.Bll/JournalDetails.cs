using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class JournalDetails : SUL.Bll.Base.JournalDetailsBase
	{
		private static SUL.Dal.JournalDetailsDal Dal = new SUL.Dal.JournalDetailsDal();
		public JournalDetails() : base()
		{
		}
	}
}
