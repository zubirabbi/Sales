using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class SPDeliveryDetailsDal : SUL.Dal.Base.SPDeliveryDetailsDalBase
	{
		public SPDeliveryDetailsDal() : base()
		{
		}
        public DataTable GetAllSPDealerDetailsBymasterId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("SPDeliveryDetails", "*", " Where MasterId = @MasterId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllSPDealerDetailsBymasterIdSpIdCateIdColorId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("SPDeliveryDetails", "*", " Where SpairPartsId = @SpairPartsId And Unit = @Unit And Color = @Color And MasterId = @MasterId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
