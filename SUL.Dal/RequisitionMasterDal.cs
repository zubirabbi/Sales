using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class RequisitionMasterDal : SUL.Dal.Base.RequisitionMasterDalBase
	{
		public RequisitionMasterDal() : base()
		{
		}
        public int GetlastRequisitionCode(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumIDbyCondition("RequisitionMaster", "cast(Id as int)", 0, "", lstData);

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
        public DataTable GetRequisitionListFromViewList(Hashtable lstData, string whereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewRequisitionMaster", "*", whereCondition, lstData);
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
        public DataTable GetAllInfoFromViewList(Hashtable lstData, string whereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewAllInfo", "*", whereCondition, lstData);
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
        /// <param name="whereCondition2"></param>
        /// <param name="havingCondition2"></param>
        /// <returns></returns>
        public DataTable GetAllInfoFromViewListByGroup(Hashtable lstData, string whereCondition2, string havingCondition2)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewAllInfo", @"DealerId,RegeonId,AId,CSId,JrCSId,CMId,ACM,DealerName,RegionName,AreaName,
                                                Cs,JrCs,CM,AreaCM, ProductName, sum(Quantity) as qnty, Price, 
                                                sum(LineTotal2) as total, sum(discount) as disccount", whereCondition2 + " " + havingCondition2, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetAllInfoFromViewListByProduct(Hashtable lstData, string whereCondition2, string havingCondition2)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewAllInfo", @"ProductName,sum(Quantity) as totalQuantity,sum(LineTotal2) as TotalPrice,Count(RequisitionCode) as ReqCode"
                                            , whereCondition2 + " " + havingCondition2, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllInfoFromViewListByDealer(Hashtable lstData, string whereCondition2, string havingCondition2, string groupElement)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewAllInfo", groupElement + @" as GroupElement, ProductName,sum(Quantity) as totalQuantity,sum(LineTotal2) as TotalPrice,Count(RequisitionCode) as ReqCode"
                                            , whereCondition2 + " " + havingCondition2, lstData);
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
        public int GetMaxRequisitionMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("RequisitionMaster", "Id", 1, "", lstData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckCodeExistance(Hashtable lstData, bool isNewEntry)
        {
            string whereCondition = (isNewEntry) ? " where RequisitionCode=@RequisitionCode" : " where RequisitionCode=@RequisitionCode and Id<>@Id";
            return CheckExistence("RequisitionMaster", "Id", whereCondition, lstData);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public int SetActiveStatus(Hashtable lstData)
        {
            string sqlQuery = "Update RequisitionMaster set  Status = @Status, UpdateBy = @UpdateBy where Id=@Id  ;";
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
            string sqlQuery = "Update RequisitionMaster set status='Invoiced', UpdateBy = @UpdateBy, IsInvoiceCreated = @IsInvoiceCreated where Id=@Id  ;";
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

        public DataTable SearchRequisition(Hashtable lstData, string whereCondition)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewRequisitionMaster", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateRequisitionMasterByPrice(Hashtable lstData)
        {
            string sqlQuery = "Update RequisitionMaster set ItemTotal = @ItemTotal, ItemTotal2 = @ItemTotal2, Discount = @Discount  where RequisitionMaster.Id = @Id;";
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

        public int UpdateRequisitionMasterByCencel(Hashtable lstData)
        {
            string sqlQuery = "Update RequisitionMaster set Status = @Status, CencelDate = @CencelDate, CencelBy = @CencelBy, CencelNote = @CencelNote where RequisitionMaster.Id = @Id;";
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

        public DataTable GetInformationByCampaignId(Hashtable lstData)
        {
            string whereCondition = " Where CampaignId = @CampaignId";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewCampaignCalculation", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

	}
}
