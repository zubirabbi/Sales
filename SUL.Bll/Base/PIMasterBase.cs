using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class PIMasterBase
	{
		protected static SUL.Dal.PIMasterDal dal = new SUL.Dal.PIMasterDal();

		public System.Int64 Id		{ get ; set; }

		public System.String PINo		{ get ; set; }

		public System.DateTime PIDate		{ get ; set; }

		public System.Int64 OrderId		{ get ; set; }

		public System.Int32 VendorId		{ get ; set; }

		public System.String VendorName		{ get ; set; }

		public System.String VendorAddress		{ get ; set; }

		public System.String DocName		{ get ; set; }

		public System.String DocLocation		{ get ; set; }

		public System.Int32 CreatedBy		{ get ; set; }

		public System.String Status		{ get ; set; }

	    public System.Int32 BankInformation { get; set; }

	    public  Int32 InsertPIMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@PINo", PINo);
			lstItems.Add("@PIDate", PIDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@OrderId", OrderId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@VendorId", VendorId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@VendorName", VendorName);
			lstItems.Add("@VendorAddress", VendorAddress);
			lstItems.Add("@DocName", DocName);
			lstItems.Add("@DocLocation", DocLocation);
			lstItems.Add("@CreatedBy", CreatedBy.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Status", Status);
            lstItems.Add("@BankInformation", BankInformation.ToString(CultureInfo.InvariantCulture));

			return dal.InsertPIMaster(lstItems);
		}

		public  Int32 UpdatePIMaster()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@PINo", PINo);
			lstItems.Add("@PIDate", PIDate.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@OrderId", OrderId.ToString());
			lstItems.Add("@VendorId", VendorId.ToString());
			lstItems.Add("@VendorName", VendorName);
			lstItems.Add("@VendorAddress", VendorAddress);
			lstItems.Add("@DocName", DocName);
			lstItems.Add("@DocLocation", DocLocation);
			lstItems.Add("@CreatedBy", CreatedBy.ToString());
			lstItems.Add("@Status", Status);
            lstItems.Add("@BankInformation", BankInformation.ToString(CultureInfo.InvariantCulture));

			return dal.UpdatePIMaster(lstItems);
		}

		public  Int32 DeletePIMasterById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeletePIMasterById(lstItems);
		}

		public List<PIMaster> GetAllPIMaster()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllPIMaster(lstItems);
			List<PIMaster> PIMasterList = new List<PIMaster>();
			foreach (DataRow dr in dt.Rows)
			{
				PIMasterList.Add(GetObject(dr));
			}
			return PIMasterList;
		}

		public PIMaster  GetPIMasterById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetPIMasterById(lstItems);
		    if (dt.Rows.Count > 0)
		    {
		        DataRow dr = dt.Rows[0];

		        return GetObject(dr);
		    }
		    else
		    {
		        return new PIMaster();
		    }
		}

		protected  PIMaster GetObject(DataRow dr)
		{

			PIMaster objPIMaster = new PIMaster();
			objPIMaster.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objPIMaster.PINo = (dr["PINo"] == DBNull.Value) ? "" : (String)dr["PINo"];
			objPIMaster.PIDate = (dr["PIDate"] == DBNull.Value) ? DateTime.MinValue : (DateTime)dr["PIDate"];
			objPIMaster.OrderId = (dr["OrderId"] == DBNull.Value) ? 0 : (Int64)dr["OrderId"];
			objPIMaster.VendorId = (dr["VendorId"] == DBNull.Value) ? 0 : (Int32)dr["VendorId"];
			objPIMaster.VendorName = (dr["VendorName"] == DBNull.Value) ? "" : (String)dr["VendorName"];
			objPIMaster.VendorAddress = (dr["VendorAddress"] == DBNull.Value) ? "" : (String)dr["VendorAddress"];
			objPIMaster.DocName = (dr["DocName"] == DBNull.Value) ? "" : (String)dr["DocName"];
			objPIMaster.DocLocation = (dr["DocLocation"] == DBNull.Value) ? "" : (String)dr["DocLocation"];
			objPIMaster.CreatedBy = (dr["CreatedBy"] == DBNull.Value) ? 0 : (Int32)dr["CreatedBy"];
			objPIMaster.Status = (dr["Status"] == DBNull.Value) ? "" : (String)dr["Status"];
            objPIMaster.BankInformation = (dr["BankInformation"] == DBNull.Value) ? 0 : (Int32)dr["BankInformation"];

			return objPIMaster;
		}
	}
}
