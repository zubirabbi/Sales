using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Globalization;
using SUL.Bll.Base;

namespace SUL.Bll
{
    public class RequisitionMaster : SUL.Bll.Base.RequisitionMasterBase
    {
        private static SUL.Dal.RequisitionMasterDal Dal = new SUL.Dal.RequisitionMasterDal();
        public RequisitionMaster()
            : base()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetlastRequisitionCode()
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();

            id = dal.GetlastRequisitionCode(lstItems);

            RequisitionMaster master = new RequisitionMaster().GetRequisitionMasterById(id);
            string reqCode = master.RequisitionCode;
            string newId = reqCode.Substring(reqCode.IndexOf("/") + 1, reqCode.Length - (reqCode.IndexOf("/") + 6));

            id = int.Parse(newId) + 1;
            string maxIdS = id.ToString();


            maxIdS = "RQ/" + maxIdS + "/" + DateTime.Now.Year;
            return maxIdS;
        }

        public string GetlastCampaignRequisitionCode()
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();

            id = dal.GetlastRequisitionCode(lstItems);
            RequisitionMaster master = new RequisitionMaster().GetRequisitionMasterById(id);
            string reqCode = master.RequisitionCode;
            string newId = reqCode.Substring(reqCode.IndexOf("/") + 1, reqCode.Length - (reqCode.IndexOf("/") + 6));

            id = int.Parse(newId) + 1;
            string maxIdS = id.ToString();


            maxIdS = "CRQ/" + maxIdS + "/" + DateTime.Now.Year;
            return maxIdS;
        }


        //public string GetlastRequisitionCode()
        //{
        //    int id = 0;
        //    Hashtable lstItems = new Hashtable();

        //    id = dal.GetlastRequisitionCode(lstItems);

        //    id = id + 1;
        //    string maxIdS = id.ToString();


        //    maxIdS = "RQ/" + maxIdS + "/" + DateTime.Now.Year;
        //    return maxIdS;
        //}

        //public string GetlastCampaignRequisitionCode()
        //{
        //    int id = 0;
        //    Hashtable lstItems = new Hashtable();

        //    id = dal.GetlastRequisitionCode(lstItems);

        //    id = id + 1;
        //    string maxIdS = id.ToString();


        //    maxIdS = "CRQ/" + maxIdS + "/" + DateTime.Now.Year;
        //    return maxIdS;
        //}
        public int GetMaxRequisitionMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxRequisitionMasterId(lstItems);
        }

        public int CheckCodeExistence(string requisitionCode, int id, bool isNewEntry)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@RequisitionCode",requisitionCode);
            
            if(!isNewEntry) lstItems.Add("@Id", id);

            return dal.CheckCodeExistance(lstItems, isNewEntry);
        }

        public DataTable GetRequisitionListFromViewList(string whereCondition)
        {
            Hashtable lstItems = new Hashtable();
            whereCondition += " Order By RequisitionDate desc,Id Desc";
            DataTable dt = dal.GetRequisitionListFromViewList(lstItems, whereCondition);
            return dt;
        }
        public DataTable GetAllInfoFromViewList(string whereCondition)
        {
            Hashtable lstItems = new Hashtable();
            whereCondition += " Order By RequisitionDate desc,Id Desc";
            DataTable dt = dal.GetAllInfoFromViewList(lstItems, whereCondition);
            return dt;
        }

        public DataTable GetAllInfoFromViewListByGroup(string whereCondition2, string havingCondition2)
        {
            Hashtable lstItems = new Hashtable();
            whereCondition2 += " group by DealerName,DealerId,RegeonId,AId,CSId,JrCSId,CMId,ACM,RegionName,AreaName,Cs,JrCs,CM,AreaCM,ProductName, Price, DealerId,RegeonId";
            DataTable dt = dal.GetAllInfoFromViewListByGroup(lstItems, whereCondition2, havingCondition2);
            return dt;
        }
        public DataTable GetAllInfoFromViewListByProduct(string whereCondition2, string havingCondition2)
        {
            Hashtable lstItems = new Hashtable();
            whereCondition2 += " group by ProductName";
            DataTable dt = dal.GetAllInfoFromViewListByProduct(lstItems, whereCondition2, havingCondition2);
            return dt;
        }
        public DataTable GetAllInfoFromViewListByDealer(string whereCondition2, string havingCondition2,string groupElement)
        {
            Hashtable lstItems = new Hashtable();
            if (whereCondition2!=string.Empty)
                whereCondition2 += " And " + groupElement + " is not null group by ProductName, " + groupElement;
            else
                whereCondition2 += " Where " + groupElement + " is not null group by ProductName, " + groupElement;
            DataTable dt = dal.GetAllInfoFromViewListByDealer(lstItems, whereCondition2, havingCondition2, groupElement);
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_isActive"></param>
        /// <returns></returns>
        public int ChangeRequisitionStatus(int id,int updateId, string status)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Status", status);
            lstItems.Add("@UpdateBy", updateId);
            lstItems.Add("@Id", id);

            return dal.SetActiveStatus(lstItems);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_isInvoiceCreate"></param>
        /// <returns></returns>
        public int SetIncoiceActiveStatus(int _id, int updateId, bool _isInvoiceCreate)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@IsInvoiceCreated", _isInvoiceCreate);
            lstItems.Add("@UpdateBy", updateId);
            lstItems.Add("@Id", _id);

            return dal.SetIncoiceActiveStatus(lstItems);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dealerId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable SearchRequisition(int regionId, int areaId, int dealerId, DateTime startDate, DateTime endDate, string whereCondition)
        {
            string conditions;
            if (whereCondition == string.Empty)
            {
                conditions = "";
            }

            else
            {
                conditions = whereCondition;
            }

            Hashtable lstItems = new Hashtable();
            if (regionId > 0)
            {
                lstItems.Add("@Id", regionId);
                if (conditions == string.Empty)
                {
                    conditions += (@"Where DealerId in( 
	                            select Id from DealerInformation where Area in (
	                            select Id from Area where RegionId in (
	                            select Id from Region where Id = @Id)))");
                }
                else
                {
                    conditions += (@"And DealerId in( 
	                            select Id from DealerInformation where Area in (
	                            select Id from Area where RegionId in (
	                            select Id from Region where Id = @Id)))");
                }
            }
            if (areaId > 0)
            {
                lstItems.Add("@AreaId", areaId);
                if (conditions == string.Empty)
                {
                    conditions += (@"Where DealerId in( 
	                            select Id from DealerInformation where Area in (
	                            select Id from Area where Id = @AreaId))");
                }
                else
                {
                    conditions += (@"And DealerId in( 
	                            select Id from DealerInformation where Area in (
	                            select Id from Area where Id = @AreaId))");
                }
            }
            if (dealerId > 0)
            {
                lstItems.Add("@DealerId", dealerId);
                if (conditions == string.Empty)
                {
                    conditions += " Where DealerId= @DealerId";
                }
                else
                {
                    conditions += " And DealerId= @DealerId";
                }
            }
            if (startDate != DateTime.MinValue)
            {
                string sDate =startDate.ToString("MMM dd, yyyy");
                lstItems.Add("@StartDate", sDate);

                if (endDate != DateTime.MinValue)
                {
                    string eDate = endDate.ToString("MMM dd, yyyy");
                    lstItems.Add("@EndDate", eDate);
                }
                else
                {
                    string eDate = startDate.ToString("MMM dd, yyyy");
                    lstItems.Add("@EndDate", eDate);
                }

                if (conditions == string.Empty)
                    conditions = " Where (cast(CONVERT(varchar(8), RequisitionDate, 112) AS datetime)>= @StartDate) and (cast(CONVERT(varchar(8), RequisitionDate, 112) AS datetime) <= @EndDate)";
                else
                    conditions += " And (cast(CONVERT(varchar(8), RequisitionDate, 112) AS datetime) >= @StartDate) and (cast(CONVERT(varchar(8), RequisitionDate, 112) AS datetime) <= @EndDate)";

            }
            conditions += "  order by RequisitionDate desc ,Id desc";
            return dal.SearchRequisition(lstItems, conditions);
        }

        public Int32 UpdateRequisitionMasterByPrice()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@ItemTotal", ItemTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ItemTotal2", ItemTotal2.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
           
            return dal.UpdateRequisitionMasterByPrice(lstItems);
        }



        public Int32 UpdateRequisitionMasterByCancel()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@Status", Status.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CencelDate", CencelDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CencelBy", CencelBy.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CencelNote", Discount.ToString(CultureInfo.InvariantCulture));

            return dal.UpdateRequisitionMasterByCencel(lstItems);
        }

        public DataTable GetInformationByCampaignId(int campaignId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignId",campaignId);
            DataTable dt = dal.GetInformationByCampaignId(lstItems);
            return dt;
        }
    }
}
