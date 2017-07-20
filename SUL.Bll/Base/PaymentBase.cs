using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
    public class PaymentBase
    {
        protected static SUL.Dal.PaymentDal dal = new SUL.Dal.PaymentDal();

        public System.Int64 Id { get; set; }

        public System.String MoneyReceiptNo { get; set; }

        public System.DateTime PaymentDate { get; set; }

        public System.Int64 DealerId { get; set; }

        public System.String Address { get; set; }

        public System.Int32 PaymentType { get; set; }

        public System.Decimal Amount { get; set; }

        public System.Int32 PaymentMode { get; set; }

        public System.Int32 BankNameId { get; set; }

        public System.String ReferenceNo { get; set; }

        public System.Int32 RequisitionId { get; set; }

        public System.Boolean IsVarified { get; set; }

        public System.Decimal BankCharge { get; set; }

        public System.Decimal LastBalance { get; set; }

        public System.String Branch { get; set; }

        public System.String Status { get; set; }
        public System.Int64 UpdateBy { get; set; }

        public System.Int64 CreatedBy { get; set; }

        public System.String ChequeBank { get; set; }

        public System.String ChequeBranch { get; set; }

        public System.DateTime ChequeDate { get; set; }

        public Int32 InsertPayment()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MoneyReceiptNo", MoneyReceiptNo);
            lstItems.Add("@PaymentDate", PaymentDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@DealerId", DealerId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Address", Address);
            lstItems.Add("@PaymentType", PaymentType.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Amount", Amount.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@PaymentMode", PaymentMode.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@BankNameId", BankNameId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ReferenceNo", ReferenceNo);
            lstItems.Add("@RequisitionId", RequisitionId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IsVarified", IsVarified);
            lstItems.Add("@BankCharge", BankCharge.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@LastBalance", LastBalance.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Branch", Branch);
            lstItems.Add("@Status", Status);
            lstItems.Add("@UpdateBy", UpdateBy.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CreatedBy", CreatedBy.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ChequeBank", ChequeBank);
            lstItems.Add("@ChequeBranch", ChequeBranch);
            lstItems.Add("@ChequeDate", ChequeDate.ToString(CultureInfo.InvariantCulture));

            return dal.InsertPayment(lstItems);
        }

        public Int32 UpdatePayment()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@MoneyReceiptNo", MoneyReceiptNo);
            lstItems.Add("@PaymentDate", PaymentDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@DealerId", DealerId.ToString());
            lstItems.Add("@Address", Address);
            lstItems.Add("@PaymentType", PaymentType.ToString());
            lstItems.Add("@Amount", Amount.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@PaymentMode", PaymentMode.ToString());
            lstItems.Add("@BankNameId", BankNameId.ToString());
            lstItems.Add("@ReferenceNo", ReferenceNo);
            lstItems.Add("@RequisitionId", RequisitionId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IsVarified", IsVarified);
            lstItems.Add("@BankCharge", BankCharge.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@LastBalance", LastBalance.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Branch", Branch);
            lstItems.Add("@Status", Status);
            lstItems.Add("@UpdateBy", UpdateBy.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CreatedBy", CreatedBy.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ChequeBank", ChequeBank);
            lstItems.Add("@ChequeBranch", ChequeBranch);
            lstItems.Add("@ChequeDate", ChequeDate.ToString(CultureInfo.InvariantCulture));

            return dal.UpdatePayment(lstItems);
        }

        public Int32 DeletePaymentById(Int64 Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id);

            return dal.DeletePaymentById(lstItems);
        }

        public List<Payment> GetAllPayment()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllPayment(lstItems);
            List<Payment> PaymentList = new List<Payment>();
            foreach (DataRow dr in dt.Rows)
            {
                PaymentList.Add(GetObject(dr));
            }
            return PaymentList;
        }

        public Payment GetPaymentById(Int64 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);

            DataTable dt = dal.GetPaymentById(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }

        protected Payment GetObject(DataRow dr)
        {

            Payment objPayment = new Payment();
            objPayment.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
            objPayment.MoneyReceiptNo = (dr["MoneyReceiptNo"] == DBNull.Value) ? "" : (String)dr["MoneyReceiptNo"];
            objPayment.PaymentDate = (dr["PaymentDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["PaymentDate"];
            objPayment.DealerId = (dr["DealerId"] == DBNull.Value) ? 0 : (Int64)dr["DealerId"];
            objPayment.Address = (dr["Address"] == DBNull.Value) ? "" : (String)dr["Address"];
            objPayment.PaymentType = (dr["PaymentType"] == DBNull.Value) ? 0 : (Int32)dr["PaymentType"];
            objPayment.Amount = (dr["Amount"] == DBNull.Value) ? 0 : (Decimal)dr["Amount"];
            objPayment.PaymentMode = (dr["PaymentMode"] == DBNull.Value) ? 0 : (Int32)dr["PaymentMode"];
            objPayment.BankNameId = (dr["BankNameId"] == DBNull.Value) ? 0 : (Int32)dr["BankNameId"];
            objPayment.ReferenceNo = (dr["ReferenceNo"] == DBNull.Value) ? "" : (String)dr["ReferenceNo"];
            objPayment.RequisitionId = (dr["RequisitionId"] == DBNull.Value) ? 0 : Int32.Parse(dr["RequisitionId"].ToString());
            objPayment.IsVarified = (dr["IsVarified"] == DBNull.Value) ? false : (Boolean)dr["IsVarified"];
            objPayment.BankCharge = (dr["BankCharge"] == DBNull.Value) ? 0 : (Decimal)dr["BankCharge"];
            objPayment.LastBalance = (dr["LastBalance"] == DBNull.Value) ? 0 : (Decimal)dr["LastBalance"];
            objPayment.Branch = (dr["Branch"] == DBNull.Value) ? "" : (String)dr["Branch"];
            objPayment.Status = (dr["Status"] == DBNull.Value) ? "" : (String)dr["Status"];
            objPayment.UpdateBy = (dr["UpdateBy"] == DBNull.Value) ? 0 : Int64.Parse(dr["UpdateBy"].ToString());
            objPayment.CreatedBy = (dr["CreatedBy"] == DBNull.Value) ? 0 : Int64.Parse(dr["CreatedBy"].ToString());
            objPayment.ChequeBank = (dr["ChequeBank"] == DBNull.Value) ? "" : (String)dr["ChequeBank"];
            objPayment.ChequeBranch = (dr["ChequeBranch"] == DBNull.Value) ? "" : (String)dr["ChequeBranch"];
            objPayment.ChequeDate = (dr["ChequeDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["ChequeDate"];

            return objPayment;
        }
    }
}
