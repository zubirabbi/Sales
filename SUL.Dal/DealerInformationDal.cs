using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
    public class DealerInformationDal : SUL.Dal.Base.DealerInformationDalBase
    {
        public DealerInformationDal()
            : base()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetlastDealerInfoCode(Hashtable lstData)
        {

            try
            {
                string whereCondition = " where Area = @Area";
                int maxId = GetMaximumIDbyCondition("DealerInformation", "cast(right(DealerCode,2) as int)", 0, whereCondition, lstData);

                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetlasWareHouseInfoCode(Hashtable lstData)
        {

            try
            {
                string whereCondition = "";
                int maxId = GetMaximumIDbyCondition("WareHouse", "cast(right(WareHouseCode,2) as int)", 0, whereCondition, lstData);

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
        public int UpdateDealerCSJCS(Hashtable lstData)
        {
            string sqlQuery = "Update DealerInformation set CS = @CS, JrCS = @JrCS where DealerInformation.Area = @AreaId;";
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

        public int UpdateDealerInformationfordealerLedger(Hashtable lstData)
        {
            string sqlQuery = "Update DealerInformation set TotalDebit = TotalDebit+@TotalDebit, TotalCredit = TotalCredit+@TotalCredit where DealerInformation.Id = @Id;";
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

        public int CheckForCodeExist(Hashtable lstData, bool isNewentry)
        {
            string whereCondition = string.Empty;
            whereCondition = isNewentry ? " where DealerCode = @DealerCode " : " where DealerCode = @DealerCode and Id != @Id";

            try
            {
                return CheckExistence("DealerInformation", "Id", whereCondition, lstData);
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
        public DataTable GetAllDealerInformationView()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("viewDealerInfo", "*", "");
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetAllDealerInformationReportView()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewDealerLedgerReport", "*", "");
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllDealerInformationViewById(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("viewDealerInfo", "*", "Where Id = @Id",lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllDealerInformationViewbyArea(Hashtable lstData)
        {
            string whereCondition = " Where Area = @Area ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("viewDealerInfo", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllDealerInformationViewByRegion(Hashtable lstData)
        {
            string whereCondition = " Where  Area in (select Id from Area where RegionId = @RegionId ) ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("viewDealerInfo", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllDealerCategoryInformation(Hashtable lstData)
        {
            string whereCondition = " where DealerInformation.DealerCategory = @DealerCategory ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("DealerInformation", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable SearchLedgerStatement(Hashtable lstData, string whereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewDealerLedgerReport", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
