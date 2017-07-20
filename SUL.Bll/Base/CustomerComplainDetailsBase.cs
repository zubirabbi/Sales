using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class CustomerComplainDetailsBase
	{
		protected static SUL.Dal.CustomerComplainDetailsDal dal = new SUL.Dal.CustomerComplainDetailsDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 MasterId		{ get ; set; }

		public System.String Problem		{ get ; set; }

		public System.String Description		{ get ; set; }


		public  Int32 InsertCustomerComplainDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Problem", Problem);
			lstItems.Add("@Description", Description);

			return dal.InsertCustomerComplainDetails(lstItems);
		}

		public  Int32 UpdateCustomerComplainDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@Problem", Problem);
			lstItems.Add("@Description", Description);

			return dal.UpdateCustomerComplainDetails(lstItems);
		}

		public  Int32 DeleteCustomerComplainDetailsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteCustomerComplainDetailsById(lstItems);
		}

		public List<CustomerComplainDetails> GetAllCustomerComplainDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllCustomerComplainDetails(lstItems);
			List<CustomerComplainDetails> CustomerComplainDetailsList = new List<CustomerComplainDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				CustomerComplainDetailsList.Add(GetObject(dr));
			}
			return CustomerComplainDetailsList;
		}

		public CustomerComplainDetails  GetCustomerComplainDetailsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetCustomerComplainDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  CustomerComplainDetails GetObject(DataRow dr)
		{

			CustomerComplainDetails objCustomerComplainDetails = new CustomerComplainDetails();
			objCustomerComplainDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objCustomerComplainDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int32)dr["MasterId"];
			objCustomerComplainDetails.Problem = (dr["Problem"] == DBNull.Value) ? "" : (String)dr["Problem"];
			objCustomerComplainDetails.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];

			return objCustomerComplainDetails;
		}
	}
}
