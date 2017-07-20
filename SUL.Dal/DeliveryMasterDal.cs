using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class DeliveryMasterDal : SUL.Dal.Base.DeliveryMasterDalBase
	{
		public DeliveryMasterDal() : base()
		{
		}
        public int GetlastDeliveryCode(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumIDbyCondition("DeliveryMaster", "cast(Id as int)", 0, "", lstData);

                return maxId;
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
        public int GetMaxDeliveryMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("DeliveryMaster", "Id", 1, "", lstData);
        }
        public DataTable GetDeliveryMasterByRequisitionId(Hashtable lstData)
        {
            string whereCondition = " where DeliveryMaster.RequisitionId = @RequisitionId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("DeliveryMaster", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
