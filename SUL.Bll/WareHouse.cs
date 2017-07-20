using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class WareHouse : SUL.Bll.Base.WareHouseBase
	{
		private static SUL.Dal.WareHouseDal Dal = new SUL.Dal.WareHouseDal();
		public WareHouse() : base()
		{
		}
        public DataTable GetAllWareHousebyCompanyId(int CompanyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CompanyId", CompanyId);
            return dal.GetAllWareHousebyCompanyId(lstItems);
        }

        public string GetlastwareHouseCode()
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();

            id = dal.GetlastwareHouseCode(lstItems);

            id = id + 1;
            string maxIdS = id.ToString();
            int count = maxIdS.Length;
            while (count < 4)
            {
                maxIdS = "0" + maxIdS;
                count++;
            }
           


            maxIdS = "WH_" + maxIdS ;
            return maxIdS;
        }
	}
}
