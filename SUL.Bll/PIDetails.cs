using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
    public class PIDetails : SUL.Bll.Base.PIDetailsBase
    {
        private static SUL.Dal.PIDetailsDal Dal = new SUL.Dal.PIDetailsDal();
        public PIDetails()
            : base()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_MasterId"></param>
        /// <returns></returns>
        public List<PIDetails> GetAllPIDetailsBymasterId(int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@PIMasterId", _MasterId);
            DataTable dt = dal.GetAllPIDetailsBymasterId(lstItems);
            List<PIDetails> PIDetailsList = new List<PIDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                PIDetailsList.Add(GetObject(dr));
            }
            return PIDetailsList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_productId"></param>
        /// <param name="_unitId"></param>
        /// <param name="_MasterId"></param>
        /// <returns></returns>
        public List<PIDetails> GetAllPIDetailsOrderDetailsBymasterIdProductIdCateId(int _productId, int _unitId, int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId", _productId);
            lstItems.Add("@ProductUnit", _unitId);
            lstItems.Add("@PIMasterId", _MasterId);
            DataTable dt = dal.GetAllPIDetailsOrderDetailsBymasterIdProductIdCateId(lstItems);
            List<PIDetails> PIDetailsList = new List<PIDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                PIDetailsList.Add(GetObject(dr));
            }
            return PIDetailsList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_MasterId"></param>
        /// <param name="_productId"></param>
        /// <returns></returns>
        public PIDetails GetAllPIDetailsOrderDetailsByIdProductId(int _MasterId, int _productId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", _MasterId);
            lstItems.Add("@ProductId", _productId);

            DataTable dt = dal.GetAllPIDetailsOrderDetailsByIdProductId(lstItems);
            DataRow dr = dt.Rows[0];

            return GetObject(dr);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_productId"></param>
        /// <param name="_unitId"></param>
        /// <returns></returns>
        public PIDetails GetAllPIDetailsOrderDetailsBymasterIdProductId(int _productId, int _unitId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId", _productId);
            lstItems.Add("@ProductUnit", _unitId);
            DataTable dt = dal.GetAllPIDetailsOrderDetailsBymasterIdProductId(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Int32 DeletePIDetailsByMasterId(Int64 Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", Id);

            return dal.DeletePIDetailsByMasterId(lstItems);
        }
    }
}
