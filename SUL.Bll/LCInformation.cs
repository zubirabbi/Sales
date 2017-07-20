using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class LCInformation : SUL.Bll.Base.LCInformationBase
	{
		private static SUL.Dal.LCInformationDal Dal = new SUL.Dal.LCInformationDal();
		public LCInformation() : base()
		{
		}
        public List<LCInformation> GetAllLCInformationbySupplierId( int _supplierId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@VendorId",_supplierId);
            DataTable dt = dal.GetAllLCInformationbySupplierId(lstItems);
            List<LCInformation> LCInformationList = new List<LCInformation>();
            foreach (DataRow dr in dt.Rows)
            {
                LCInformationList.Add(GetObject(dr));
            }
            return LCInformationList;
        }

	    public int IsLCNoExist(string LCNo, int id=0)
	    {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@LCNumber", LCNo);
	        if (id == 0)
	        {
	            return dal.IsLCNoExist(lstItems, true);
	        }
	        else
	        {
	            lstItems.Add("@Id", id);
                return dal.IsLCNoExist(lstItems, false);	            
	        }
	    }

	    public List<LCInformation> GetLCByPIId(int _id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@PINo", _id);
            DataTable dt = dal.GetLCByPIId(lstItems);
            List<LCInformation> PILcList = new List<LCInformation>();
            foreach (DataRow dr in dt.Rows)
            {
                PILcList.Add(GetObject(dr));
            }
            return PILcList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetLCFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetLCFromViewList(lstItems);
            return dt;
        }
        public int GetMaxLCMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxLCMasterId(lstItems);
        }
	}
}
