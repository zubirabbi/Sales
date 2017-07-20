using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class SPDeliveryMasterDal : SUL.Dal.Base.SPDeliveryMasterDalBase
	{
		public SPDeliveryMasterDal() : base()
		{
		}
        public int GetlastTransactionCode(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumIDbyCondition("SPDeliveryMaster", "cast(Id as int)", 0, "", lstData);

                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int GetMaxSPDeliveryMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("SPDeliveryMaster", "Id", 1, "", lstData);
        }
        public DataTable GetSPDelliveryMasterList(Hashtable lstData)
        {

            string whereCondition = " ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewSPDeliveryMaster", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SetActiveStatus(Hashtable lstData)
        {
            string sqlQuery = "Update SPDeliveryMaster set  Status = @Status, ApproveBy = @ApproveBy, ApproveDate = @ApproveDate where Id=@Id  ;";
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
        public int SetReceiveStatus(Hashtable lstData)
        {
            string sqlQuery = "Update SPDeliveryMaster set  Status = @Status, ReceiveBy = @ReceiveBy, ReceiveDate = @ReceiveDate where Id=@Id  ;";
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
	}
}
