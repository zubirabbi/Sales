using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class DealerJournalDetailsDal : SUL.Dal.Base.DealerJournalDetailsDalBase
	{
		public DealerJournalDetailsDal() : base()
		{
		}
        public DataTable GetAllDealerJournalDetailsBymasterId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("DealerJournalDetails", "*", " Where MasterId = @MasterId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllBymasterIdDealerId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("DealerJournalDetails", "*", " Where MasterId = @MasterId And DealerId = @DealerId ", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
