using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ReceiverMasterDal : SUL.Dal.Base.ReceiverMasterDalBase
	{
		public ReceiverMasterDal() : base()
		{
		}
        public int GetlastReceiverCode(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumIDbyCondition("ReceiverMaster", "cast(Id as int)", 0, "", lstData);

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
        public int GetMaxReciverMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("ReceiverMaster", "Id", 1, "", lstData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetReceiverFromViewList(Hashtable lstData)
        {

            string whereCondition = " where CompanyId = @CompanyId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewReceiverMaster", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateReceiverMasterByAmount(Hashtable lstData)
        {
            string sqlQuery = "Update ReceiverMaster set TotalAmount = @TotalAmount where ReceiverMaster.Id = @Id;";
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
        public int SetIncoiceActiveStatus(Hashtable lstData)
        {
            string sqlQuery = "Update ReceiverMaster set  IsInvoiceCreated = @IsInvoiceCreated where Id=@Id  ;";
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

        public DataTable GetReceiverMasterByVandorId(Hashtable lstData)
        {
            string whereCondition = " where ReceiverMaster.VendorId = @VendorId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ReceiverMaster", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetReceiverMasterByLCId(Hashtable lstData)
        {
            string whereCondition = " where ReceiverMaster.LCId = @LCId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ReceiverMaster", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SetActiveStatus(Hashtable lstData)
        {
            string sqlQuery = "Update ReceiverMaster set  Status = @Status where Id=@Id  ;";
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
