using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ProductCategoryDal : SUL.Dal.Base.ProductCategoryDalBase
	{
		public ProductCategoryDal() : base()
		{
		}
        public int CheckForCodeExist(Hashtable lstData, bool isNewentry)
        {
            string whereCondition = string.Empty;
            whereCondition = isNewentry ? " where CategoryCode = @CategoryCode " : " where CategoryCode = @CategoryCode and Id != @Id";

            try
            {
                return CheckExistence("ProductCategory", "Id", whereCondition, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
