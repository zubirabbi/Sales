using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using Microsoft.Win32;
using SUL.Bll.Base;

namespace SUL.Bll
{
    public class ReceiverDetails : SUL.Bll.Base.ReceiverDetailsBase
    {
        private static SUL.Dal.ReceiverDetailsDal Dal = new SUL.Dal.ReceiverDetailsDal();
        public ReceiverDetails()
            : base()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_piNo"></param>
        /// <returns></returns>
        public DataTable GetPIDetailsTable(string _piNo)
        {
            Hashtable lstData = new Hashtable();
            lstData.Add("@PINo", _piNo);

            DataTable dt = dal.GetPIDetailsTable(lstData);

            return dt;
        }

        public DataTable GetReceiverDetailsTable(int _masterId)
        {
            Hashtable lstData = new Hashtable();
            lstData.Add("@MasterId", _masterId);

            DataTable dt = dal.GetReceiverDetailsTable(lstData);

            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_piNo"></param>
        /// <param name="_ProductId"></param>
        /// <returns></returns>
        public DataTable GetPIDetailsTableByProductId(string _piNo,int _ProductId)
        {
            Hashtable lstData = new Hashtable();
            lstData.Add("@PINo", _piNo);
            lstData.Add("@ProductId", _ProductId);
            DataTable dt = dal.GetPIDetailsTableByProductId(lstData);

            return dt;
        }
    }
}
