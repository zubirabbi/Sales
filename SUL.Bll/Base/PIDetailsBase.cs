using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class PIDetailsBase
	{
		protected static SUL.Dal.PIDetailsDal dal = new SUL.Dal.PIDetailsDal();

		public System.Int64 Id		{ get ; set; }

		public System.Int64 MasterId		{ get ; set; }

		public System.Int64 OrderDetailsId		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }

		public System.String ProductName		{ get ; set; }

		public System.Int32 OrderQuantity		{ get ; set; }

		public System.Int32 PIQuantity		{ get ; set; }

		public System.Int32 ProductUnit		{ get ; set; }

		public System.String UnitName		{ get ; set; }

		public System.Decimal LineTotal		{ get ; set; }

		public System.Int32 PICategory		{ get ; set; }

		public System.Decimal PIUnitePrice		{ get ; set; }


		public  Int32 InsertPIDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@OrderDetailsId", OrderDetailsId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductName", ProductName);
			lstItems.Add("@OrderQuantity", OrderQuantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@PIQuantity", PIQuantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductUnit", ProductUnit.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@UnitName", UnitName);
			lstItems.Add("@LineTotal", LineTotal.ToString(CultureInfo.InvariantCulture));

			lstItems.Add("@PICategory", PICategory.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@PIUnitePrice", PIUnitePrice);

			return dal.InsertPIDetails(lstItems);
		}

		public  Int32 UpdatePIDetails()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@MasterId", MasterId.ToString());
			lstItems.Add("@OrderDetailsId", OrderDetailsId.ToString());
			lstItems.Add("@ProductId", ProductId.ToString());
			lstItems.Add("@ProductName", ProductName);
			lstItems.Add("@OrderQuantity", OrderQuantity.ToString());
			lstItems.Add("@PIQuantity", PIQuantity.ToString());
			lstItems.Add("@ProductUnit", ProductUnit.ToString());
			lstItems.Add("@UnitName", UnitName);
			lstItems.Add("@LineTotal", LineTotal.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@PICategory", PICategory.ToString());
			lstItems.Add("@PIUnitePrice", PIUnitePrice);

			return dal.UpdatePIDetails(lstItems);
		}

		public  Int32 DeletePIDetailsById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeletePIDetailsById(lstItems);
		}

		public List<PIDetails> GetAllPIDetails()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllPIDetails(lstItems);
			List<PIDetails> PIDetailsList = new List<PIDetails>();
			foreach (DataRow dr in dt.Rows)
			{
				PIDetailsList.Add(GetObject(dr));
			}
			return PIDetailsList;
		}

		public PIDetails  GetPIDetailsById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetPIDetailsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  PIDetails GetObject(DataRow dr)
		{

			PIDetails objPIDetails = new PIDetails();
			objPIDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objPIDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int64)dr["MasterId"];
			objPIDetails.OrderDetailsId = (dr["OrderDetailsId"] == DBNull.Value) ? 0 : (Int64)dr["OrderDetailsId"];
			objPIDetails.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			objPIDetails.ProductName = (dr["ProductName"] == DBNull.Value) ? "" : (String)dr["ProductName"];
			objPIDetails.OrderQuantity = (dr["OrderQuantity"] == DBNull.Value) ? 0 : (Int32)dr["OrderQuantity"];
			objPIDetails.PIQuantity = (dr["PIQuantity"] == DBNull.Value) ? 0 : (Int32)dr["PIQuantity"];
			objPIDetails.ProductUnit = (dr["ProductUnit"] == DBNull.Value) ? 0 : (Int32)dr["ProductUnit"];
			objPIDetails.UnitName = (dr["UnitName"] == DBNull.Value) ? "" : (String)dr["UnitName"];
			objPIDetails.LineTotal = (dr["LineTotal"] == DBNull.Value) ? 0 : (Decimal)dr["LineTotal"];
			objPIDetails.PICategory = (dr["PICategory"] == DBNull.Value) ? 0 : (Int32)dr["PICategory"];
            objPIDetails.PIUnitePrice = (dr["PIUnitePrice"] == DBNull.Value) ? 0 : (Decimal)dr["PIUnitePrice"];

			return objPIDetails;
		}
	}
}
