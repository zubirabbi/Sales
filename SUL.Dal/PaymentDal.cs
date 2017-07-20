using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class PaymentDal : SUL.Dal.Base.PaymentDalBase
	{
		public PaymentDal() : base()
		{
		}
        public int GetlastMoneyReceiptCode(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumIDbyCondition("Payment", "cast(Id as int)", 0, "", lstData);

                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllViewpayment(Hashtable lstData, string whereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
               
                dt = GetDataTable("vewPayment", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetPaymentByRequisitionId(Hashtable lstData)
        {
            string whereCondition = " where Payment.RequisitionId = @RequisitionId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("Payment", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int SetvarifiedPayment(Hashtable lstData)
        {
            string sqlQuery = "Update Payment set  IsVarified = @IsVarified, UpdateBy = @UpdateBy  where Id=@Id  ;";
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public int ChangeStatus(Hashtable lstData)
        {
            string sqlQuery = "Update Payment set  Status = @Status, UpdateBy = @UpdateBy where Id=@Id  ;";
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

        public int GetlastPaymentCode(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumIDbyCondition("Payment", "cast(Id as int)", 0, "", lstData);

                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable SearchPayment(Hashtable lstData, string whereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewPayment", "*", whereCondition + " Order by PaymentDate DESC,Id DESC", lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int GetMaxPaymentId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("Payment", "Id", 1, "", lstData);
        }

        public int SetActiveStatus(Hashtable lstData)
        {
            string sqlQuery = "Update Payment set  Status = @Status, UpdateBy = @UpdateBy where Id=@Id  ;";
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
