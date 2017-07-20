using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Linq;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class RequisitionDetails : SUL.Bll.Base.RequisitionDetailsBase
	{
		private static SUL.Dal.RequisitionDetailsDal Dal = new SUL.Dal.RequisitionDetailsDal();
		public RequisitionDetails() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_MasterId"></param>
        /// <returns></returns>
        public List<RequisitionDetails> GetAllRequisitionDetailsBymasterId(int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", _MasterId);
            DataTable dt = dal.GetAllRequisitionDetailsBymasterId(lstItems);
            return (from DataRow dr in dt.Rows select GetObject(dr)).ToList();
        }
        public List<RequisitionDetails> GetAllRequisitionDetailsBymasterIdProductIdCateIdColorId(int _productId, int _unitId, int _colorId,int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId", _productId);
            lstItems.Add("@Unit", _unitId);
            lstItems.Add("@Color", _colorId);
            lstItems.Add("@PIMasterId", _MasterId);
            DataTable dt = dal.GetAllRequisitionDetailsBymasterIdProductIdCateIdColorId(lstItems);
            List<RequisitionDetails> RequisitionDetailsList = new List<RequisitionDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                RequisitionDetailsList.Add(GetObject(dr));
            }
            return RequisitionDetailsList;
        }

        public RequisitionDetails GetReqDetailsByProductCampaign(int _productId, int _unitId, int _colorId, int _MasterId, int campaignId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId", _productId);
            lstItems.Add("@Unit", _unitId);
            lstItems.Add("@Color", _colorId);
            lstItems.Add("@PIMasterId", _MasterId);
            lstItems.Add("@CampaignDetailsId", campaignId);

            DataTable dt = dal.GetReqDetailsByProductCampaign(lstItems);
            RequisitionDetails detail = new RequisitionDetails();

            if(dt.Rows.Count>0)
             detail =   GetObject(dt.Rows[0]);

            return detail;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public DataTable GetRequistionFromView(Int64 masterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", masterId);
            DataTable dt = dal.GetRequistionFromView(lstItems);
            
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="campaignDetailsId"></param>
        /// <returns></returns>
        public int CheckExistanceByCampaignDetailsId(Int32 campaignDetailsId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignDetailsId", campaignDetailsId);

            return dal.CheckExistanceByCampaignDetailsId(lstItems);
        }
	}
}
