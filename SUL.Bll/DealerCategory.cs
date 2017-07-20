using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class DealerCategory : SUL.Bll.Base.DealerCategoryBase
	{
		private static SUL.Dal.DealerCategoryDal Dal = new SUL.Dal.DealerCategoryDal();
		public DealerCategory() : base()
		{
		}
        public int CheckForCodeExist(string _code, bool isNewEntry, int _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CategoryCode", _code);

            if (!isNewEntry)
                lstItems.Add("@Id", _Id);

            return dal.CheckForCodeExist(lstItems, isNewEntry);
        }
	}
}
