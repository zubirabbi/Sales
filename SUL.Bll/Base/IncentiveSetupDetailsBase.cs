using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
    public class IncentiveSetupDetailsBase
    {
        protected static SUL.Dal.IncentiveSetupDetailsDal dal = new SUL.Dal.IncentiveSetupDetailsDal();

        public System.Int32 Id { get; set; }

        public System.Int32 MasterId { get; set; }

        public System.Decimal StartValue { get; set; }

        public System.Decimal EndValue { get; set; }

        public System.Decimal IncentivePcnt { get; set; }

        public System.Decimal IncentiveValue { get; set; }
        
        public System.Int32 Slno { get; set; }

        public System.Int32 StartQuantity { get; set; }

        public System.Int32 EndQuantity { get; set; }

        public Int32 InsertIncentiveSetupDetails()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@StartValue", StartValue.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@EndValue", EndValue.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IncentivePcnt", IncentivePcnt.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IncentiveValue", IncentiveValue.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Slno", Slno.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@StartQuantity", StartQuantity.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@EndQuantity", EndQuantity.ToString(CultureInfo.InvariantCulture));

            return dal.InsertIncentiveSetupDetails(lstItems);
        }

        public Int32 UpdateIncentiveSetupDetails()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@MasterId", MasterId.ToString());
            lstItems.Add("@StartValue", StartValue.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@EndValue", EndValue.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IncentivePcnt", IncentivePcnt.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IncentiveValue", IncentiveValue.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Slno", Slno.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@StartQuantity", StartQuantity.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@EndQuantity", EndQuantity.ToString(CultureInfo.InvariantCulture));

            return dal.UpdateIncentiveSetupDetails(lstItems);
        }

        public Int32 DeleteIncentiveSetupDetailsById(Int32 Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id);

            return dal.DeleteIncentiveSetupDetailsById(lstItems);
        }

        public List<IncentiveSetupDetails> GetAllIncentiveSetupDetails()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllIncentiveSetupDetails(lstItems);
            List<IncentiveSetupDetails> IncentiveSetupDetailsList = new List<IncentiveSetupDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                IncentiveSetupDetailsList.Add(GetObject(dr));
            }
            return IncentiveSetupDetailsList;
        }

        public IncentiveSetupDetails GetIncentiveSetupDetailsById(Int32 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);

            DataTable dt = dal.GetIncentiveSetupDetailsById(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }

        protected IncentiveSetupDetails GetObject(DataRow dr)
        {

            IncentiveSetupDetails objIncentiveSetupDetails = new IncentiveSetupDetails();
            objIncentiveSetupDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
            objIncentiveSetupDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int32)dr["MasterId"];
            objIncentiveSetupDetails.StartValue = (dr["StartValue"] == DBNull.Value) ? 0 : (Decimal)dr["StartValue"];
            objIncentiveSetupDetails.EndValue = (dr["EndValue"] == DBNull.Value) ? 0 : (Decimal)dr["EndValue"];
            objIncentiveSetupDetails.IncentivePcnt = (dr["IncentivePcnt"] == DBNull.Value) ? 0 : (Decimal)dr["IncentivePcnt"];
            objIncentiveSetupDetails.IncentiveValue = (dr["IncentiveValue"] == DBNull.Value) ? 0 : (Decimal)dr["IncentiveValue"];
            objIncentiveSetupDetails.Slno = (dr["Slno"] == DBNull.Value) ? 0 : (Int32)dr["Slno"];
            objIncentiveSetupDetails.StartQuantity = (dr["StartQuantity"] == DBNull.Value) ? 0 : (Int32)dr["StartQuantity"];
            objIncentiveSetupDetails.EndQuantity = (dr["EndQuantity"] == DBNull.Value) ? 0 : (Int32)dr["EndQuantity"];

            return objIncentiveSetupDetails;
        }
    }
}
