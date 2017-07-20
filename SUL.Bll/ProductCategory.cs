using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ProductCategory : SUL.Bll.Base.ProductCategoryBase
	{
		private static SUL.Dal.ProductCategoryDal Dal = new SUL.Dal.ProductCategoryDal();
		public ProductCategory() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_code"></param>
        /// <param name="isNewEntry"></param>
        /// <param name="_Id"></param>
        /// <returns></returns>
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
