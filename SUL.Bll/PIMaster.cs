using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
    public class PIMaster : SUL.Bll.Base.PIMasterBase
    {
        private static SUL.Dal.PIMasterDal Dal = new SUL.Dal.PIMasterDal();
        public PIMaster()
            : base()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMaxPIMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxPIMasterId(lstItems);
        }
        public List<PIMaster> GetAllPIMasterbyVendorId(int _vendorid)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@VendorId",_vendorid);
            DataTable dt = dal.GetAllPIMasterbyVendorId(lstItems);
            List<PIMaster> PIMasterList = new List<PIMaster>();
            foreach (DataRow dr in dt.Rows)
            {
                PIMasterList.Add(GetObject(dr));
            }
            return PIMasterList;
        }

        public PIMaster GetAllPImasterbyPIno(string _piNumber)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@PINo", _piNumber);

            DataTable dt = dal.GetAllPImasterbyPIno(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public List<PIMaster> GetPIByVandorId(int _id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@VendorId", _id);
            DataTable dt = dal.GetPIByVandorId(lstItems);
            List<PIMaster> PIMasterList = new List<PIMaster>();
            foreach (DataRow dr in dt.Rows)
            {
                PIMasterList.Add(GetObject(dr));
            }
            return PIMasterList;
        }
        public List<PIMaster> GetPIByPOId(int _id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@OrderId", _id);
            DataTable dt = dal.GetPIByPOId(lstItems);
            List<PIMaster> PIMasterList = new List<PIMaster>();
            foreach (DataRow dr in dt.Rows)
            {
                PIMasterList.Add(GetObject(dr));
            }
            return PIMasterList;
        }

        public DataTable GetPIFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetPIFromViewList(lstItems);
            return dt;
        }
    }
}
