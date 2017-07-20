using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class SPDeliveryMaster : SUL.Bll.Base.SPDeliveryMasterBase
	{
		private static SUL.Dal.SPDeliveryMasterDal Dal = new SUL.Dal.SPDeliveryMasterDal();
		public SPDeliveryMaster() : base()
		{
		}
        public string GetlastTransactionCode()
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();

            id = dal.GetlastTransactionCode(lstItems);
            string newId;
            if (id != 0)
            {
                SPDeliveryMaster master = new SPDeliveryMaster().GetSPDeliveryMasterById(id);
                string reqCode = master.TransactionCode;
                newId = reqCode.Substring(reqCode.IndexOf("/") + 1, reqCode.Length - (reqCode.IndexOf("/") + 6));
            }
            else
            {
                newId = "0";
            }
            id = int.Parse(newId) + 1;
            string maxIdS = id.ToString();


            maxIdS = "SP/" + maxIdS + "/" + DateTime.Now.Year;
            return maxIdS;
        }
        public int GetMaxSPDeliveryMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxSPDeliveryMasterId(lstItems);
        }
        public DataTable GetSPDelliveryMasterList()
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetSPDelliveryMasterList(lstItems);
            return dt;
        }
        public int ChangeSPDeliveryApproveStatus(int id, int ApproveBy, string status, DateTime approveDate)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Status", status);
            lstItems.Add("@ApproveBy", ApproveBy);
            lstItems.Add("@Id", id);
            lstItems.Add("@ApproveDate", approveDate.ToString("MMM dd,yyyy"));

            return dal.SetActiveStatus(lstItems);
        }
        public int ChangeSPDeliveryReceiveStatus(int id, int ReceiveBy, string status, DateTime receiveDate)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Status", status);
            lstItems.Add("@ReceiveBy", ReceiveBy);
            lstItems.Add("@Id", id);
            lstItems.Add("@ReceiveDate", receiveDate.ToString("MMM dd,yyyy"));

            return dal.SetReceiveStatus(lstItems);
        }
	}
}
