using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
    public class InvoiceMasterDal : SUL.Dal.Base.InvoiceMasterDalBase
    {
        public InvoiceMasterDal()
            : base()
        {
        }
        public int GetlastInvoiceCode(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumIDbyCondition("InvoiceMaster", "cast(Id as int)", 0, "", lstData);

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
        public int GetMaxInvoiceMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("InvoiceMaster", "Id", 1, "", lstData);
        }

        public DataTable GetInvoiceMasterByRequisitionId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("InvoiceMaster", "*", " Where RequisitionId = @RequisitionId", lstData);
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
        public int UpdateInvoiceMasterInformation(Hashtable lstData)
        {
            string sqlQuery = "Update InvoiceMaster set  ItemTotal = @ItemTotal, Discount = @Discount, ItemTotal2 = @ItemTotal2  where InvoiceMaster.Id = @Id;";
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
        public DataTable GetInvoiceFromViewListByDealerId(Hashtable lstData)
        {

            string whereCondition = "Where DealerId = @DealerId group by ProductId, ProductName";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewInvoice", "ProductId,ProductName,sum(Quantity)", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetInvoiceFromViewListByDealerIdbyProductId(Hashtable lstData)
        {

            string whereCondition = "Where DealerId = @DealerId And ProductId = @ProductId ";
            DataTable dt = new DataTable();
            try
            {
                string quantity = ExecuteScaler("vewInvoice", "sum(Quantity)", whereCondition, lstData);

                return quantity;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetInvoiceFromViewList(Hashtable lstData)
        {

            string whereCondition = " Order by InvoiceDate DESC,Id DESC";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewInvoice", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable SearchDealerItem(Hashtable lstData, string whereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewInvoice", "*", whereCondition, lstData);
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
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public DataTable SearchInvoiceDateItem(Hashtable lstData)
        {

            string queryString = @"select DealerId, DealerName As Name,SUM(ItemTotal) as ItemTotal,sum(Discount) as  Discount,sum(Total) as Total,count(Id) as NumberOfInvoice  from vewInvoiceReport 
	                               where (cast(CONVERT(varchar(8), InvoiceDate, 112) AS datetime) >= @StartDate) and (cast(CONVERT(varchar(8), InvoiceDate, 112) AS datetime) <= @EndDate)
	                               group by DealerId,DealerName ";

            try
            {
                return GetDataTable(queryString, lstData);
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
        public DataTable DealerSummary(Hashtable lstData)
        {
            string queryString = @"select DealerName As Name,SUM(ItemTotal) as ItemTotal,sum(Discount) as  Discount,sum(Total) as Total,count(Id) as NumberOfInvoice  from vewInvoiceReport 
	                               where InvoiceDate between @StartDate And @EndDate
	                               group by DealerId,DealerName ";

            try
            {
                return GetDataTable(queryString, lstData);
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
        public DataTable AreaSummary(Hashtable lstData)
        {
            string queryString = @"select RegionName As Name,SUM(ItemTotal) as ItemTotal,sum(Discount) as  Discount,sum(Total) as Total,count(Id) as NumberOfInvoice  from vewInvoiceReport 
	                                where InvoiceDate between @StartDate And @EndDate
	                                group by RegionName";

            try
            {
                return GetDataTable(queryString, lstData);
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
        public DataTable RegionSummary(Hashtable lstData)
        {
            string queryString = @"select AreaName As Name,SUM(ItemTotal) as ItemTotal,sum(Discount) as  Discount,sum(Total) as Total,count(Id) as NumberOfInvoice  from vewInvoiceReport 
	                                where InvoiceDate between @StartDate And @EndDate
	                                group by AreaName";

            try
            {
                return GetDataTable(queryString, lstData);
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
        public DataTable CMSummary(Hashtable lstData)
        {
            string queryString = @"select CM As Name,AreaCM AS AsstName,SUM(ItemTotal) as ItemTotal,sum(Discount) as  Discount,sum(Total) as Total,count(Id) as NumberOfInvoice  from vewInvoiceReport 
	                                where InvoiceDate between @StartDate And @EndDate
	                                group by CM,AreaCM";

            try
            {
                return GetDataTable(queryString, lstData);
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
        /// /
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable ACMSummary(Hashtable lstData)
        {
            string queryString = @"select CM As Name,AreaCM AS AsstName,SUM(ItemTotal) as ItemTotal,sum(Discount) as  Discount,sum(Total) as Total,count(Id) as NumberOfInvoice  from vewInvoiceReport 
	                                where InvoiceDate between @StartDate And @EndDate
	                                group by CM,AreaCM";

            try
            {
                return GetDataTable(queryString, lstData);
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
        /// /
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable CsSummary(Hashtable lstData)
        {
            string queryString = @"Select Cs As Name,JrCs,SUM(ItemTotal) as ItemTotal,sum(Discount) as  Discount,sum(Total) as Total,count(Id) as NumberOfInvoice  from vewInvoiceReport 
	                                where InvoiceDate between @StartDate And @EndDate
	                                group by Cs,JrCs";

            try
            {
                return GetDataTable(queryString, lstData);
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
        public DataTable JrCsSummary(Hashtable lstData)
        {
            string queryString = @"Select Cs As Name,JrCs,SUM(ItemTotal) as ItemTotal,sum(Discount) as  Discount,sum(Total) as Total,count(Id) as NumberOfInvoice  from vewInvoiceReport 
	                                where InvoiceDate between @StartDate And @EndDate
	                                group by Cs,JrCs";

            try
            {
                return GetDataTable(queryString, lstData);
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
