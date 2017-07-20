using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ItemReturnMaster : SUL.Bll.Base.ItemReturnMasterBase
	{
		private static SUL.Dal.ItemReturnMasterDal Dal = new SUL.Dal.ItemReturnMasterDal();
		public ItemReturnMaster() : base()
		{
		}
        public string GetlastItemReturnCode()
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();

            id = dal.GetlastItemReturnCode(lstItems);

            id = id + 1;
            string maxIdS = id.ToString();


            maxIdS = "IR/" + maxIdS + "/" + DateTime.Now.Year;
            return maxIdS;
        }
        public int GetMaxItemReturnMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxItemReturnMasterId(lstItems);
        }
        public DataTable GetItemReturnListFromViewList()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetItemReturnListFromViewList(lstItems);
            return dt;
        }
        public int ChangeItemReturnStatus(int id, string status)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Status", status);
            lstItems.Add("@Id", id);

            return dal.SetActiveStatus(lstItems);
        }
	}
}
