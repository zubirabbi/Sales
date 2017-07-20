using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
    public class InvoiceMaster : SUL.Bll.Base.InvoiceMasterBase
    {
        private static SUL.Dal.InvoiceMasterDal Dal = new SUL.Dal.InvoiceMasterDal();
        public InvoiceMaster()
            : base()
        {
        }
        public string GetlastInvoiceCode()
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();

            id = dal.GetlastInvoiceCode(lstItems);

            InvoiceMaster master = new InvoiceMaster().GetInvoiceMasterById(id);
            string reqCode = master.InvoiceNo;
            string newId = reqCode.Substring(reqCode.IndexOf("/") + 1, reqCode.Length - (reqCode.IndexOf("/") + 6));

            id = int.Parse(newId) + 1;
            string maxIdS = id.ToString();


            maxIdS = "INV/" + maxIdS + "/" + DateTime.Now.Year;
            return maxIdS;
        }
        public int GetMaxInvoiceMasterId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxInvoiceMasterId(lstItems);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_requisition"></param>
        /// <returns></returns>
        public InvoiceMaster GetInvoiceMasterByRequisitionId(Int64 _requisition)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@RequisitionId", _requisition);
            DataTable dt = dal.GetInvoiceMasterByRequisitionId(lstItems);
            if (dt.Rows.Count > 0)
                return GetObject(dt.Rows[0]);
            else
                return new InvoiceMaster();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_requisition"></param>
        /// <returns></returns>
        public InvoiceMaster invoiceMasterDetails(int _requisition)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@RequisitionId", _requisition);
            DataTable dt = dal.GetInvoiceMasterByRequisitionId(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Int32 UpdateInvoiceMasterInformation()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@ItemTotal", ItemTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ItemTotal2", ItemTotal2.ToString(CultureInfo.InvariantCulture));

            return dal.UpdateInvoiceMasterInformation(lstItems);
        }


        public DataTable GetInvoiceFromViewListByDealerId(int _dealerId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DealerId",_dealerId);
            DataTable dt = dal.GetInvoiceFromViewListByDealerId(lstItems);
            return dt;
        }
        public string GetInvoiceFromViewListByDealerIdByProductId(int _dealerId,int _productId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DealerId", _dealerId);
            lstItems.Add("@ProductId", _productId);
            string dt = dal.GetInvoiceFromViewListByDealerIdbyProductId(lstItems);
            return dt;
        }
        public DataTable GetInvoiceFromViewList()
        {
            Hashtable lstItems = new Hashtable();
           
            DataTable dt = dal.GetInvoiceFromViewList(lstItems);
            return dt;
        }

        public DataTable SearchDealerItem(int DealerId, int productId, DateTime startDate, DateTime endDate)
        {
            string conditions = string.Empty;

            Hashtable lstItems = new Hashtable();
            if (DealerId > 0)
            {
                lstItems.Add("@DealerId", DealerId);
                conditions = " where DealerId= @DealerId";
            }
            if (productId > 0)
            {
                lstItems.Add("@ProductId", productId);
                if (conditions == string.Empty)
                    conditions = " where ProductId= @ProductId";
                else
                    conditions += " And ProductId= @ProductId";
            }
            if (startDate != DateTime.MinValue)
            {
                DateTime sDate = DateTime.Parse(startDate.ToString("MMM dd, yyyy") + " 00:00:00");
                lstItems.Add("@StartDate", sDate);

                if (endDate != DateTime.MinValue)
                {
                    DateTime eDate = DateTime.Parse(endDate.ToString("MMM dd, yyyy") + " 23:59:59");
                    lstItems.Add("@EndDate", eDate);
                }
                else
                {
                    DateTime eDate = DateTime.Parse(startDate.ToString("MMM dd, yyyy") + " 23:59:59");
                    lstItems.Add("@EndDate", eDate);
                }

                if (conditions == string.Empty)
                    conditions = " where (InvoiceDate >= @StartDate and InvoiceDate <= @EndDate)";
                else
                    conditions += " And (InvoiceDate >= @StartDate and InvoiceDate <= @EndDate)";

            }
            conditions += " Order by InvoiceDate DESC,Id DESC";
            return dal.SearchDealerItem(lstItems, conditions);
        }

        public DataTable SearchInvoiceDateItem(DateTime startDate, DateTime endDate)
        {
            

            Hashtable lstItems = new Hashtable();
            lstItems.Add("@StartDate", startDate.ToString("MMM dd,yyyy"));
            lstItems.Add("@EndDate", endDate.ToString("MMM dd,yyyy"));
            DataTable dt = dal.SearchInvoiceDateItem(lstItems);

            return dt;
        }
        public DataTable GetInvoicesummary(string Name,DateTime StartDate,DateTime EndDate)
        {
            Hashtable lstItems = new Hashtable();
            
            lstItems.Add("@EndDate", EndDate);
            lstItems.Add("@StartDate",StartDate);
            DataTable dt=new DataTable();
            switch (Name)
            {
                case "dealer":
                    dt = dal.DealerSummary(lstItems);
                    break;
                case "area":
                    dt = dal.AreaSummary(lstItems);
                    break;
                case "region":
                    dt = dal.RegionSummary(lstItems);
                    break;
                case "cs":
                    dt = dal.CsSummary(lstItems);
                    break;
                case "jcs":
                    dt = dal.JrCsSummary(lstItems);
                    break;
                case "acm":
                    dt = dal.ACMSummary(lstItems);
                    break;
                case "cm":
                    dt = dal.CMSummary(lstItems);
                    break;
            }
            return dt;

            
        }
       
    }
}
