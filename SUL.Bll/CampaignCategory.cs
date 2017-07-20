using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class CampaignCategory : SUL.Bll.Base.CampaignCategoryBase
	{
		private static SUL.Dal.CampaignCategoryDal Dal = new SUL.Dal.CampaignCategoryDal();
		public CampaignCategory() : base()
		{
		}
	}
}
