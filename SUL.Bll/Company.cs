using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class Company : SUL.Bll.Base.CompanyBase
	{
		private static SUL.Dal.CompanyDal Dal = new SUL.Dal.CompanyDal();
		public Company() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Company GetParentCompany()
        {

            DataTable dt = dal.GetParentCompany();
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                return GetObject(dr);
            }
            else
                return new Company();
        }
	}
}
