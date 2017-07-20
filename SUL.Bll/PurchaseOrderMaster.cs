using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
    public class PurchaseOrderMaster : SUL.Bll.Base.PurchaseOrderMasterBase
    {
        private static SUL.Dal.PurchaseOrderMasterDal Dal = new SUL.Dal.PurchaseOrderMasterDal();
        public PurchaseOrderMaster()
            : base()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMaxPurchaseMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxPurchaseMasterId(lstItems);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetPurchaseOrderListFromViewList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetPurchaseOrderListFromViewList(lstItems);
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public string GetlastSupplierCode(int _supplierId)
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();


            lstItems.Add("@VendorId", _supplierId);
            id = dal.GetlastSupplierCode(lstItems);

            id = id + 1;
            string maxIdS = id.ToString();

            Supplier objSupplier=new Supplier().GetSupplierById(_supplierId);

            string supplierCode = objSupplier.Code;

            maxIdS = supplierCode+"/"+maxIdS+"/"+DateTime.Now.Year;
            return maxIdS;
        }
        public List<PurchaseOrderMaster> GetPurchaseOrderMasterByVandorId(int _id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@VendorId",_id);
            DataTable dt = dal.GetPurchaseOrderMasterByVandorId(lstItems);
            List<PurchaseOrderMaster> PurchaseOrderMasterList = new List<PurchaseOrderMaster>();
            foreach (DataRow dr in dt.Rows)
            {
                PurchaseOrderMasterList.Add(GetObject(dr));
            }
            return PurchaseOrderMasterList;
        }

        public List<PurchaseOrderMaster> GetOrderNoForPI(int _id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@VendorId", _id);
            DataTable dt = dal.GetOrderNoForPI(lstItems);
            List<PurchaseOrderMaster> PurchaseOrderMasterList = new List<PurchaseOrderMaster>();
            foreach (DataRow dr in dt.Rows)
            {
                PurchaseOrderMasterList.Add(GetObject(dr));
            }
            return PurchaseOrderMasterList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public List<PurchaseOrderMaster> GetPurchaseOrderByVandorId(int _id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@VendorId", _id);
            DataTable dt = dal.GetPurchaseOrderMasterByVandorId(lstItems);
            List<PurchaseOrderMaster> PurchaseOrderMasterList = new List<PurchaseOrderMaster>();
            foreach (DataRow dr in dt.Rows)
            {
                PurchaseOrderMasterList.Add(GetObject(dr));
            }
            return PurchaseOrderMasterList;
        }

    }
}
