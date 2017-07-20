using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class DealerJournalMaster : SUL.Bll.Base.DealerJournalMasterBase
	{
		private static SUL.Dal.DealerJournalMasterDal Dal = new SUL.Dal.DealerJournalMasterDal();
		public DealerJournalMaster() : base()
		{
		}
        public int GetMaxDealerJournalnMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxJournalMasterId(lstItems);
        }
        public DataTable GetDealerJournalMasterFromViewList()
        {
            Hashtable lstItems = new Hashtable();
            string whereCondition = "";
            DataTable dt = dal.GetDealerJournalMasterFromViewList(lstItems, whereCondition);
            return dt;
        }
        public int ChangeDealerJournalStatus(int id, int updateId, string status)
        {
            Hashtable lstItems = new Hashtable();

            lstItems.Add("@Status", status);
            lstItems.Add("@UpdateBy", updateId);
            lstItems.Add("@UpdateDate", DateTime.Now.ToString("MMM dd,yyyy"));
            lstItems.Add("@Id", id);

            return dal.SetActiveStatus(lstItems);
        }
        public string GetlastDealerJournalCode()
        {
            int id = 0;
            string newId;
            Hashtable lstItems = new Hashtable();

            id = dal.GetlastDealerJournalCode(lstItems);


            if (id> 0)
            {
                DealerJournalMaster master = new DealerJournalMaster().GetDealerJournalMasterById(id);
                string reqCode = master.JournalId;

                newId = reqCode.Substring(reqCode.IndexOf("/") + 1, reqCode.Length - (reqCode.IndexOf("/") + 6));
            }
            else
            {
                newId = string.Empty;
            }
            if (newId == string.Empty)
                id = 1;
            else
                id = int.Parse(newId) + 1;

            string maxIdS = id.ToString();


            maxIdS = "JV/" + maxIdS + "/" + DateTime.Now.Year;
            return maxIdS;
        }
	}
}
