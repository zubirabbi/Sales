using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class SPDeliveryDetails : SUL.Bll.Base.SPDeliveryDetailsBase
	{
		private static SUL.Dal.SPDeliveryDetailsDal Dal = new SUL.Dal.SPDeliveryDetailsDal();
		public SPDeliveryDetails() : base()
		{
		}
        public List<SPDeliveryDetails> GetAllSPDealerDetailsBymasterId(int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", _MasterId);
            DataTable dt = dal.GetAllSPDealerDetailsBymasterId(lstItems);
            List<SPDeliveryDetails> spDeliveryDetailsesList = new List<SPDeliveryDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                spDeliveryDetailsesList.Add(GetObject(dr));
            }
            return spDeliveryDetailsesList;
        }
        public List<SPDeliveryDetails> GetAllSPDealerDetailsBymasterIdSpIdCateIdColorId(int _spId, int _unitId, int _colorId, int _MasterId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@SpairPartsId", _spId);
            lstItems.Add("@Unit", _unitId);
            lstItems.Add("@Color", _colorId);
            lstItems.Add("@MasterId", _MasterId);
            DataTable dt = dal.GetAllSPDealerDetailsBymasterIdSpIdCateIdColorId(lstItems);
            List<SPDeliveryDetails> spDeliveryDetailsesList = new List<SPDeliveryDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                spDeliveryDetailsesList.Add(GetObject(dr));
            }
            return spDeliveryDetailsesList;
        }
	}
}
