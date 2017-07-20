using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
    public class LCInformationBase
    {
        protected static SUL.Dal.LCInformationDal dal = new SUL.Dal.LCInformationDal();

        public System.Int64 Id { get; set; }

        public System.Int64 VendorId { get; set; }

        public System.String VendorName { get; set; }

        public System.String VendorAddress { get; set; }

        public System.Int64 PINo { get; set; }

        public System.String LCNumber { get; set; }

        public System.DateTime LCDate { get; set; }

        public System.DateTime LCExpiryDate { get; set; }

        public System.Decimal LCValue { get; set; }

        public System.String LCStatus { get; set; }

        public System.Int32 IssusingBank { get; set; }

        public System.Int32 NegotiatingBank { get; set; }

        public System.String FileName { get; set; }

        public System.String FileLocation { get; set; }



        public Int32 InsertLCInformation()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@VendorId", VendorId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@VendorName", VendorName);
            lstItems.Add("@VendorAddress", VendorAddress);
            lstItems.Add("@PINo", PINo.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@LCNumber", LCNumber);
            lstItems.Add("@LCDate", LCDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@LCExpiryDate", LCExpiryDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@LCValue", LCValue.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@LCStatus", LCStatus);
            lstItems.Add("@IssusingBank", IssusingBank.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@NegotiatingBank", NegotiatingBank.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@FileName", FileName);
            lstItems.Add("@FileLocation", FileLocation);

            return dal.InsertLCInformation(lstItems);
        }

        public Int32 UpdateLCInformation()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@VendorId", VendorId.ToString());
            lstItems.Add("@VendorName", VendorName);
            lstItems.Add("@VendorAddress", VendorAddress);
            lstItems.Add("@PINo", PINo.ToString());
            lstItems.Add("@LCNumber", LCNumber);
            lstItems.Add("@LCDate", LCDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@LCExpiryDate", LCExpiryDate.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@LCValue", LCValue.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@LCStatus", LCStatus);
            lstItems.Add("@IssusingBank", IssusingBank.ToString());
            lstItems.Add("@NegotiatingBank", NegotiatingBank.ToString());
            lstItems.Add("@FileName", FileName);
            lstItems.Add("@FileLocation", FileLocation);


            return dal.UpdateLCInformation(lstItems);
        }

        public Int32 DeleteLCInformationById(Int64 Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id);

            return dal.DeleteLCInformationById(lstItems);
        }

        public List<LCInformation> GetAllLCInformation()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllLCInformation(lstItems);
            List<LCInformation> LCInformationList = new List<LCInformation>();
            foreach (DataRow dr in dt.Rows)
            {
                LCInformationList.Add(GetObject(dr));
            }
            return LCInformationList;
        }

        public LCInformation GetLCInformationById(Int64 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);

            DataTable dt = dal.GetLCInformationById(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }

        protected LCInformation GetObject(DataRow dr)
        {

            LCInformation objLCInformation = new LCInformation();
            objLCInformation.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
            objLCInformation.VendorId = (dr["VendorId"] == DBNull.Value) ? 0 : (Int64)dr["VendorId"];
            objLCInformation.VendorName = (dr["VendorName"] == DBNull.Value) ? "" : (String)dr["VendorName"];
            objLCInformation.VendorAddress = (dr["VendorAddress"] == DBNull.Value) ? "" : (String)dr["VendorAddress"];
            objLCInformation.PINo = (dr["PINo"] == DBNull.Value) ? 0 : (Int64)dr["PINo"];
            objLCInformation.LCNumber = (dr["LCNumber"] == DBNull.Value) ? "" : (String)dr["LCNumber"];
            objLCInformation.LCDate = (dr["LCDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["LCDate"];
            objLCInformation.LCExpiryDate = (dr["LCExpiryDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["LCExpiryDate"];
            objLCInformation.LCValue = (dr["LCValue"] == DBNull.Value) ? 0 : (Decimal)dr["LCValue"];
            objLCInformation.LCStatus = (dr["LCStatus"] == DBNull.Value) ? "" : (String)dr["LCStatus"];
            objLCInformation.IssusingBank = (dr["IssusingBank"] == DBNull.Value) ? 0 : (Int32)dr["IssusingBank"];
            objLCInformation.NegotiatingBank = (dr["NegotiatingBank"] == DBNull.Value) ? 0 : (Int32)dr["NegotiatingBank"];
            objLCInformation.FileName = (dr["FileName"] == DBNull.Value) ? "" : (String)dr["FileName"];
            objLCInformation.FileLocation = (dr["FileLocation"] == DBNull.Value) ? "" : (String)dr["FileLocation"];

            return objLCInformation;
        }
    }
}
