using System;
using System.Globalization;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class DealerInformation : SUL.Bll.Base.DealerInformationBase
	{
		private static SUL.Dal.DealerInformationDal Dal = new SUL.Dal.DealerInformationDal();
		public DealerInformation() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public string GetlastDealerInfoCode(int _areaId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Area", _areaId);
            int id = 0;
            id = dal.GetlastDealerInfoCode(lstItems);

            id = id + 1;
            string maxIdS = id.ToString();
            int count = maxIdS.Length;
            while (count < 2)
            {
                maxIdS = "0" + maxIdS;
                count++;
            }

            Area objArea = new Area().GetAreaById(_areaId);

            string areaCode = objArea.AreaCode.Substring(3);

            string dealerCode = "D_" + areaCode + "_" + maxIdS;

            return dealerCode;
        }

        public string GetlasWareHouseInfoCode()
        {
            Hashtable lstItems = new Hashtable();
            int id = 0;
            id = dal.GetlasWareHouseInfoCode(lstItems);

            id = id + 1;
            string maxIdS = id.ToString();
            int count = maxIdS.Length;
            while (count < 2)
            {
                maxIdS = "0" + maxIdS;
                count++;
            }

            string wareHouseCode = "W_"+ maxIdS;

            return wareHouseCode;
        }


        public Int32 UpdateDealerInformationfordealerLedger()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@TotalDebit", TotalDebit.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@TotalCredit", TotalCredit.ToString(CultureInfo.InvariantCulture));

            return dal.UpdateDealerInformationfordealerLedger(lstItems);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="cs"></param>
        /// <param name="jcs"></param>
        /// <returns></returns>
        public Int32 UpdateDealerCSJCS(int areaId, int cs, int jcs)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@AreaId", areaId.ToString());
            lstItems.Add("@CS", cs.ToString());
            lstItems.Add("@JrCS", jcs.ToString());

            return dal.UpdateDealerCSJCS(lstItems);
        }

        public int CheckForCodeExist(string _code, bool isNewEntry, int _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DealerCode", _code);

            if (!isNewEntry)
                lstItems.Add("@Id", _Id);

            return dal.CheckForCodeExist(lstItems, isNewEntry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDealerInformationView()
        {
            DataTable dt = dal.GetAllDealerInformationView();
            return dt;
        }

        public DataTable GetAllDealerInformationReportView()
        {
            DataTable dt = dal.GetAllDealerInformationReportView();
            return dt;
        }
        public DataTable GetAllDealerInformationViewById(int Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id);
            DataTable dt = dal.GetAllDealerInformationViewById(lstItems);
            return dt;
        }
        public DataTable GetAllDealerInformationViewByArea(int Area)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Area",Area);
            DataTable dt = dal.GetAllDealerInformationViewbyArea(lstItems);
            return dt;
        }
        public DataTable GetAllDealerInformationViewByRegion(int regionId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@RegionId", regionId);
            DataTable dt = dal.GetAllDealerInformationViewByRegion(lstItems);
            return dt;
        }
        public List<DealerInformation> GetAllDealerCategoryInformation(int DealerCat)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DealerCategory", DealerCat);
            DataTable dt = dal.GetAllDealerCategoryInformation(lstItems);
            List<DealerInformation> DealerInformationList = new List<DealerInformation>();
            foreach (DataRow dr in dt.Rows)
            {
                DealerInformationList.Add(GetObject(dr));
            }
            return DealerInformationList;
        }

        public DataTable SearchLedgerStatement(int DealerId, DateTime startDate, DateTime endDate)
        {
            string conditions = string.Empty;

            Hashtable lstItems = new Hashtable();
            if (DealerId > 0)
            {
                lstItems.Add("@DealerId", DealerId);
                conditions = " where DealerId= @DealerId";
            }
            if (startDate != DateTime.MinValue)
            {
                string sDate = startDate.ToString("MMM dd, yyyy");
                lstItems.Add("@StartDate", sDate);

                if (endDate != DateTime.MinValue)
                {
                    string eDate = endDate.ToString("MMM dd,yyyy");
                    lstItems.Add("@EndDate", eDate);
                }
                else
                {
                    DateTime eDate = DateTime.Parse(startDate.ToString("MM/dd/yyyy") + " 23:59:59");
                    lstItems.Add("@EndDate", eDate);
                }

                if (conditions == string.Empty)
                    conditions = " where (cast(CONVERT(varchar(8), TransactionDate, 112) AS datetime) >= @StartDate) and (cast(CONVERT(varchar(8), TransactionDate, 112) AS datetime) <= @EndDate)";
                else
                    conditions += " And (cast(CONVERT(varchar(8), TransactionDate, 112) AS datetime) >= @StartDate) and (cast(CONVERT(varchar(8), TransactionDate, 112) AS datetime) <= @EndDate)";


            }
            //conditions += " order by TransactionDate desc";
            return dal.SearchLedgerStatement(lstItems, conditions);
        }
	}
}
