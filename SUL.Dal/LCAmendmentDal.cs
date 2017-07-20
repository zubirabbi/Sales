using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class LCAmendmentDal : SUL.Dal.Base.LCAmendmentDalBase
	{
		public LCAmendmentDal() : base()
		{
		}
        public DataTable GetAllLCAmendmentbyLCIdAndNumber(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("LCAmendment", "*", " Where LCId = @LCId And AmendmentNumber = @AmendmentNumber", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllLCAmendmentbyLCId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("LCAmendment", "*", " Where LCId = @LCId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
