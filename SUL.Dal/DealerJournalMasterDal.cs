using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class DealerJournalMasterDal : SUL.Dal.Base.DealerJournalMasterDalBase
	{
		public DealerJournalMasterDal() : base()
		{
		}
        public int GetMaxJournalMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("DealerJournalMaster", "Id", 1, "", lstData);
        }
        public DataTable GetDealerJournalMasterFromViewList(Hashtable lstData, string whereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewDealerJournalMaster", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SetActiveStatus(Hashtable lstData)
        {
            string sqlQuery = "Update DealerJournalMaster set  Status = @Status, ApproveBy = @UpdateBy, ApproveDate= @UpdateDate where Id=@Id  ;";
            try
            {
                int success = ExecuteNonQuery(sqlQuery, lstData);
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }
        public int GetlastDealerJournalCode(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumIDbyCondition("DealerJournalMaster", "cast(Id as int)", 0, "", lstData);

                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
	}
}
