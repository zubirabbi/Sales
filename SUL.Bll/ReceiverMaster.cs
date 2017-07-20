using System;
using System.Globalization;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ReceiverMaster : SUL.Bll.Base.ReceiverMasterBase
	{
		private static SUL.Dal.ReceiverMasterDal Dal = new SUL.Dal.ReceiverMasterDal();
		public ReceiverMaster() : base()
		{
		}
        public string GetlastReceiverCode()
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();

            id = dal.GetlastReceiverCode(lstItems);

            id = id + 1;
            string maxIdS = id.ToString();


            maxIdS = "RC/" + maxIdS + "/" + DateTime.Now.Year;
            return maxIdS;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetMaxReciverMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxReciverMasterId(lstItems);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public DataTable GetReceiverFromViewList(int CompanyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CompanyId", CompanyId);

            DataTable dt = dal.GetReceiverFromViewList(lstItems);
            return dt;
        }

        public Int32 UpdateReceiverMasterByAmount()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@TotalAmount", TotalAmount.ToString(CultureInfo.InvariantCulture));

            return dal.UpdateReceiverMasterByAmount(lstItems);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_isInvoiceCreate"></param>
        /// <returns></returns>
        public int SetIncoiceActiveStatus(int _id, bool _isInvoiceCreate)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@IsInvoiceCreated", _isInvoiceCreate);
            lstItems.Add("@Id", _id);

            return dal.SetIncoiceActiveStatus(lstItems);
        }
        public List<ReceiverMaster> GetReceiverMasterByVandorId(int _id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@VendorId", _id);
            DataTable dt = dal.GetReceiverMasterByVandorId(lstItems);
            List<ReceiverMaster> ReceiverMasterList = new List<ReceiverMaster>();
            foreach (DataRow dr in dt.Rows)
            {
                ReceiverMasterList.Add(GetObject(dr));
            }
            return ReceiverMasterList;
        }

        public List<ReceiverMaster> GetReceiverMasterByLCId(int _id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@LCId", _id);
            DataTable dt = dal.GetReceiverMasterByLCId(lstItems);
            List<ReceiverMaster> ReceiverMasterList = new List<ReceiverMaster>();
            foreach (DataRow dr in dt.Rows)
            {
                ReceiverMasterList.Add(GetObject(dr));
            }
            return ReceiverMasterList;
        }

        public int ChangeReceivingStatus(int id, string status)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Status", status);
            lstItems.Add("@Id", id);

            return dal.SetActiveStatus(lstItems);
        }

	}

}
