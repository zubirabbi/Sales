using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class SalesIncentiveSetupBase
	{
		protected static SUL.Dal.SalesIncentiveSetupDal dal = new SUL.Dal.SalesIncentiveSetupDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 Designation		{ get ; set; }

		public System.Decimal Startvalue		{ get ; set; }

		public System.Decimal EndValue		{ get ; set; }

		public System.Decimal Discount		{ get ; set; }

		public System.Decimal Amount		{ get ; set; }

		public System.Int32 GiftProduct		{ get ; set; }

		public System.Decimal GiftQuantity		{ get ; set; }


		public  Int32 InsertSalesIncentiveSetup()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Designation", Designation.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Startvalue", Startvalue.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@EndValue", EndValue.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Amount", Amount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@GiftProduct", GiftProduct.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@GiftQuantity", GiftQuantity.ToString(CultureInfo.InvariantCulture));

			return dal.InsertSalesIncentiveSetup(lstItems);
		}

		public  Int32 UpdateSalesIncentiveSetup()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@Designation", Designation.ToString());
			lstItems.Add("@Startvalue", Startvalue.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@EndValue", EndValue.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Discount", Discount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Amount", Amount.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@GiftProduct", GiftProduct.ToString());
			lstItems.Add("@GiftQuantity", GiftQuantity.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateSalesIncentiveSetup(lstItems);
		}

		public  Int32 DeleteSalesIncentiveSetupById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteSalesIncentiveSetupById(lstItems);
		}

		public List<SalesIncentiveSetup> GetAllSalesIncentiveSetup()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllSalesIncentiveSetup(lstItems);
			List<SalesIncentiveSetup> SalesIncentiveSetupList = new List<SalesIncentiveSetup>();
			foreach (DataRow dr in dt.Rows)
			{
				SalesIncentiveSetupList.Add(GetObject(dr));
			}
			return SalesIncentiveSetupList;
		}

		public SalesIncentiveSetup  GetSalesIncentiveSetupById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetSalesIncentiveSetupById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  SalesIncentiveSetup GetObject(DataRow dr)
		{

			SalesIncentiveSetup objSalesIncentiveSetup = new SalesIncentiveSetup();
			objSalesIncentiveSetup.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objSalesIncentiveSetup.Designation = (dr["Designation"] == DBNull.Value) ? 0 : (Int32)dr["Designation"];
			objSalesIncentiveSetup.Startvalue = (dr["Startvalue"] == DBNull.Value) ? 0 : (Decimal)dr["Startvalue"];
			objSalesIncentiveSetup.EndValue = (dr["EndValue"] == DBNull.Value) ? 0 : (Decimal)dr["EndValue"];
			objSalesIncentiveSetup.Discount = (dr["Discount"] == DBNull.Value) ? 0 : (Decimal)dr["Discount"];
			objSalesIncentiveSetup.Amount = (dr["Amount"] == DBNull.Value) ? 0 : (Decimal)dr["Amount"];
			objSalesIncentiveSetup.GiftProduct = (dr["GiftProduct"] == DBNull.Value) ? 0 : (Int32)dr["GiftProduct"];
			objSalesIncentiveSetup.GiftQuantity = (dr["GiftQuantity"] == DBNull.Value) ? 0 : (Decimal)dr["GiftQuantity"];

			return objSalesIncentiveSetup;
		}
	}
}
