using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ItemLedgerDal : SUL.Dal.Base.ItemLedgerDalBase
	{
		public ItemLedgerDal() : base()
		{
		}
        public DataTable GetItemLedgerFromViewList(Hashtable lstData)
        {

            string whereCondition = " ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewItemLedger", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllItemLedgersByItemIdUnitColor(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ItemLedger", "*", " Where ItemId = @ItemId And Unit = @Unit And Color = @Color ", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllItemLedgersByItemIdUnit(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ItemLedger", "*", " Where ItemId = @ItemId And Unit = @Unit", lstData);
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
        public int UpdateItemLedgerNew(Hashtable lstData)
        {
            string sqlQuery = "Update ItemLedger set TotalIn = TotalIn+ @TotalIn, TotalOut = TotalOut + @TotalOut where ItemLedger.Id = @Id;";
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
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public DataTable SearchItemLedger(Hashtable lstData, string whereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewItemLedger", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
