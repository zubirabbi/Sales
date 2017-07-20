using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class PurchaseOrderDetails : SUL.Bll.Base.PurchaseOrderDetailsBase
	{
		private static SUL.Dal.PurchaseOrderDetailsDal Dal = new SUL.Dal.PurchaseOrderDetailsDal();
		public PurchaseOrderDetails() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_MasterId"></param>
        /// <returns></returns>
        public List<PurchaseOrderDetails> GetAllPurchaseOrderDetailsBymasterId(int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@PurchaseOrderMasterId",_MasterId);
            DataTable dt = dal.GetAllPurchaseOrderDetailsBymasterId(lstItems);
            List<PurchaseOrderDetails> PurchaseOrderDetailsList = new List<PurchaseOrderDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                PurchaseOrderDetailsList.Add(GetObject(dr));
            }
            return PurchaseOrderDetailsList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_MasterId"></param>
        /// <param name="_productId"></param>
        /// <param name="_unitId"></param>
        /// <returns></returns>
        public List<PurchaseOrderDetails> GetAllPurchaseOrderDetailsBymasterIdProductIdCateId( int _productId, int _unitId, int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@PurchaseOrderMasterId", _MasterId);
            lstItems.Add("@ProductId", _productId);
            lstItems.Add("@UnitId", _unitId);
            DataTable dt = dal.GetAllPurchaseOrderDetailsBymasterIdProductIdCateId(lstItems);
            List<PurchaseOrderDetails> PurchaseOrderDetailsList = new List<PurchaseOrderDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                PurchaseOrderDetailsList.Add(GetObject(dr));
            }
            return PurchaseOrderDetailsList;
        }
        public Int32 DeletePurchaseOrderDetailsByPOMasterId(Int64 Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", Id);

            return dal.DeletePurchaseOrderDetailsByPOMasterId(lstItems);
        }
	}
}
