using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class DeliveryMaster : SUL.Bll.Base.DeliveryMasterBase
	{
		private static SUL.Dal.DeliveryMasterDal Dal = new SUL.Dal.DeliveryMasterDal();
		public DeliveryMaster() : base()
		{
		}
        public string GetlastDeliveryCode()
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();

            id = dal.GetlastDeliveryCode(lstItems);
            DeliveryMaster master = new DeliveryMaster().GetDeliveryMasterById(id);
            string reqCode = master.DeliveryNo;
            string newId = reqCode.Substring(reqCode.IndexOf("/") + 1, reqCode.Length - (reqCode.IndexOf("/") + 6));

            id = id + 1;
            string maxIdS = id.ToString();


            maxIdS = "DV/" + maxIdS + "/" + DateTime.Now.Year;
            return maxIdS;
        }
        public int GetMaxDeliveryMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxDeliveryMasterId(lstItems);
        }
        public DeliveryMaster GetDeliveryMasterByRequisitionId(Int32 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@RequisitionId", _Id);

            DataTable dt = dal.GetDeliveryMasterByRequisitionId(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }
	}
}
