using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class Area : SUL.Bll.Base.AreaBase
	{
		private static SUL.Dal.AreaDal Dal = new SUL.Dal.AreaDal();
		public Area() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllViewArea()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllViewArea(lstItems);

            return dt;
        }
        public DataTable GetAllViewAreaByRegionId(int regionId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@RegionId", regionId);
            DataTable dt = dal.GetAllViewAreaByRegionId(lstItems);

            return dt;
        }
        public string GetMaxAreaCode(int _regionId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@RegionId", _regionId);
            return dal.GetlastAreaCode(lstItems).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public string GetlastAreaCode(int _regionId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@RegionId", _regionId);
            int id = 0;
            id = dal.GetlastAreaCode(lstItems);

            id = id + 1;
            string maxIdS = id.ToString();
            int count = maxIdS.Length;
            while (count < 2)
            {
                maxIdS = "0" + maxIdS;
                count++;
            }

            Region objRegion=new Region().GetRegionById(_regionId);

            string regionCode = objRegion.RegionCode.Substring(3);

            string areaCode = "AC_"+regionCode+"_"+maxIdS;

            return areaCode;
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
            lstItems.Add("@AreaCode", _code);

            if (!isNewEntry)
                lstItems.Add("@Id", _Id);

            return dal.CheckForCodeExist(lstItems, isNewEntry);
        }
       
	}
}
