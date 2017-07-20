using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Globalization;
using SUL.Bll.Base;

namespace SUL.Bll
{
    public class Payment : SUL.Bll.Base.PaymentBase
    {
        private static SUL.Dal.PaymentDal Dal = new SUL.Dal.PaymentDal();
        public Payment()
            : base()
        {
        }
        public string GetlastMoneyReceiptCode()
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();

            id = dal.GetlastMoneyReceiptCode(lstItems);
            Payment master = new Payment().GetPaymentById(id);
            string reqCode = master.MoneyReceiptNo;
            string newId = reqCode.Substring(reqCode.IndexOf("/") + 1, reqCode.Length - (reqCode.IndexOf("/") + 6));

            id = int.Parse(newId) + 1;

            string maxIdS = id.ToString();


            maxIdS = "MR/" + maxIdS + "/" + DateTime.Now.Year;
            return maxIdS;
        }
        public DataTable GetAllViewpayment(string whereCondition)
        {
            Hashtable lstItems = new Hashtable();

            DataTable dt = dal.GetAllViewpayment(lstItems, whereCondition);
            return dt;
        }
        public Payment GetPaymentByRequisitionId(Int64 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@RequisitionId", _Id);

            DataTable dt = dal.GetPaymentByRequisitionId(lstItems);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                return GetObject(dr);
            }
            else
                return new Payment();

        }
        public int SetvarifiedPayment(int _id, int _userId,bool _isVarified)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@IsVarified", _isVarified);
            lstItems.Add("@Id", _id);
            lstItems.Add("@UpdateBy", _userId);

            int success = dal.SetvarifiedPayment(lstItems);
            if (success > 0)
            {
                success = this.ChangeStatus(_id, "Verified",_userId);
            }

            return success;
        }

        public int ChangeStatus(int id, string status, int _userId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Status", status);
            lstItems.Add("@Id", id);
            lstItems.Add("@UpdateBy", _userId);

            return dal.ChangeStatus(lstItems);
        }
        public int GetlastPAymentCode()
        {
            int id = 0;
            Hashtable lstItems = new Hashtable();

            id = dal.GetlastPaymentCode(lstItems);


            return id;
        }
        public DataTable SearchPayment(int regionId, int areaId, int dealerId, DateTime startDate, DateTime endDate, string whereCondition)
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
                    conditions = " Where (cast(CONVERT(varchar(8), PaymentDate, 112) AS datetime)>= @StartDate) and (cast(CONVERT(varchar(8), PaymentDate, 112) AS datetime) <= @EndDate)";
                else
                    conditions += " And (cast(CONVERT(varchar(8), PaymentDate, 112) AS datetime)>= @StartDate) and (cast(CONVERT(varchar(8), PaymentDate, 112) AS datetime) <= @EndDate)";

            }
            return dal.SearchPayment(lstItems, conditions);
        }
        public int GetMaxPaymentId()
        {
            Hashtable lstItems = new Hashtable();
            return dal.GetMaxPaymentId(lstItems);
        }

        public int ChangePaymentStatus(int id, string status, int _userId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Status", status);
            lstItems.Add("@Id", id);
            lstItems.Add("@UpdateBy", _userId);


            return dal.SetActiveStatus(lstItems);
        }
    }
}
