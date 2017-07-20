using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class RequisitionDetailsDal : SUL.Dal.Base.RequisitionDetailsDalBase
	{
		public RequisitionDetailsDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetAllRequisitionDetailsBymasterId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("RequisitionDetails", "*", " Where MasterId = @MasterId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetAllRequisitionDetailsBymasterIdProductIdCateIdColorId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("RequisitionDetails", "*", " Where ProductId = @ProductId And Unit = @Unit And Color = @Color And MasterId = @PIMasterId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetReqDetailsByProductCampaign(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("RequisitionDetails", "*", " Where ProductId = @ProductId And Unit = @Unit And Color = @Color And MasterId = @PIMasterId And CampaignDetailsId = @CampaignDetailsId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetRequistionFromView(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewRequisitionDetail", "*", " Where MasterId = @MasterId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public int CheckExistanceByCampaignDetailsId(Hashtable lstData)
        {
            try
            {
                const string whereCondition = " where CampaignDetailsId = @CampaignDetailsId ";
                return CheckExistence("RequisitionDetails", "Id", whereCondition, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
