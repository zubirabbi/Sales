using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
    public class IncentiveSetupBase
    {
        protected static SUL.Dal.IncentiveSetupDal dal = new SUL.Dal.IncentiveSetupDal();

        public System.Int32 Id { get; set; }

        public System.String Type { get; set; }

        public System.Int32 DesignationId { get; set; }

        public System.DateTime SetupDate { get; set; }

        public System.Boolean IsActive { get; set; }

        public System.Int32 UserId { get; set; }

        public System.String IncentiveOn { get; set; }
        public System.String ApplyOn { get; set; }

        public System.Int32 ProductId { get; set; }


        public Int32 InsertIncentiveSetup()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Type", Type);
            lstItems.Add("@DesignationId", DesignationId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@SetupDate", SetupDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IsActive", IsActive);
            lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IncentiveOn", IncentiveOn);
            lstItems.Add("@ApplyOn", ApplyOn);
            lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));

            return dal.InsertIncentiveSetup(lstItems);
        }

        public Int32 UpdateIncentiveSetup()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@Type", Type);
            lstItems.Add("@DesignationId", DesignationId.ToString());
            lstItems.Add("@SetupDate", SetupDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IsActive", IsActive);
            lstItems.Add("@UserId", UserId.ToString());
            lstItems.Add("@IncentiveOn", IncentiveOn);
            lstItems.Add("@ApplyOn", ApplyOn);
            lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));

            return dal.UpdateIncentiveSetup(lstItems);
        }

        public Int32 DeleteIncentiveSetupById(Int32 Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id);

            return dal.DeleteIncentiveSetupById(lstItems);
        }

        public List<IncentiveSetup> GetAllIncentiveSetup()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllIncentiveSetup(lstItems);
            List<IncentiveSetup> IncentiveSetupList = new List<IncentiveSetup>();
            foreach (DataRow dr in dt.Rows)
            {
                IncentiveSetupList.Add(GetObject(dr));
            }
            return IncentiveSetupList;
        }

        public IncentiveSetup GetIncentiveSetupById(Int32 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);

            DataTable dt = dal.GetIncentiveSetupById(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }

        protected IncentiveSetup GetObject(DataRow dr)
        {

            IncentiveSetup objIncentiveSetup = new IncentiveSetup();
            objIncentiveSetup.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
            objIncentiveSetup.Type = (dr["Type"] == DBNull.Value) ? "" : (String)dr["Type"];
            objIncentiveSetup.DesignationId = (dr["DesignationId"] == DBNull.Value) ? 0 : (Int32)dr["DesignationId"];
            objIncentiveSetup.SetupDate = (dr["SetupDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["SetupDate"];
            objIncentiveSetup.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];
            objIncentiveSetup.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
            objIncentiveSetup.IncentiveOn = (dr["IncentiveOn"] == DBNull.Value) ? "" : (String)dr["IncentiveOn"];
            objIncentiveSetup.ApplyOn = (dr["ApplyOn"] == DBNull.Value) ? "" : (String)dr["ApplyOn"];
            objIncentiveSetup.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];

            return objIncentiveSetup;
        }
    }
}
