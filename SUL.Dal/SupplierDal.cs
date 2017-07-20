using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class SupplierDal : SUL.Dal.Base.SupplierDalBase
	{
		public SupplierDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckSupplierExistance(Hashtable lstData, bool isNewEntry)
        {

            string whereCondition = string.Empty;

            if (isNewEntry)
                whereCondition = " where Supplier.Code = @Code";
            else
                whereCondition = "where Supplier.Code = @Code And Supplier.Id <> @Id";

            int count = 0;
            try
            {
                count = CheckExistence("Supplier", "Id", whereCondition, lstData);
                return count;
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
        public int GetlastSupplier(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumID("Supplier", "Id", 0, "");
                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CheckForCodeExist(Hashtable lstData, bool isNewentry)
        {
            string whereCondition = string.Empty;
            whereCondition = isNewentry ? " where Code = @Code " : " where Code = @Code and Id != @Id";

            try
            {
                return CheckExistence("Supplier", "Id", whereCondition, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateSupplierInformationfordealerLedger(Hashtable lstData)
        {
            string sqlQuery = "Update Supplier set TotalDebit = TotalDebit+@TotalDebit, TotalCredit = TotalCredit+@TotalCredit where Supplier.Id = @Id;";
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
        public DataTable GetSupplierFromViewList(Hashtable lstData)
        {

            string whereCondition = "";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewSupplier", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}

}
