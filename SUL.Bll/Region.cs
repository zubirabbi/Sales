using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
    public class Region : SUL.Bll.Base.RegionBase
    {
        private static SUL.Dal.RegionDal Dal = new SUL.Dal.RegionDal();
        public Region()
            : base()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllViewRegion()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllViewRegion(lstItems);

            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_companyId"></param>
        /// <returns></returns>
        public string GetMaxEmpCode()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetlastRegionCode().ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public string GetlastEmpCode()
        {
            int id = 0;
            id = dal.GetlastRegionCode();

            id = id + 1;
            string maxIdS = id.ToString();
            int count = maxIdS.Length;
            while (count < 2)
            {
                maxIdS = "0" + maxIdS;
                count++;
            }


            string RegionCode = "RC_";

            maxIdS = RegionCode + maxIdS;
            return maxIdS;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllRegionFromView()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetAllRegionFromView(lstItems);
           
        }

        public int CheckForCodeExist(string _code, bool isNewEntry, int _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@RegionCode", _code);

            if (!isNewEntry)
                lstItems.Add("@Id", _Id);

            return dal.CheckForCodeExist(lstItems, isNewEntry);
        }
        
    }
}
