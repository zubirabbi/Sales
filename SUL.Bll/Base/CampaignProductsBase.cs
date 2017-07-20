using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class CampaignProductsBase
	{
		protected static SUL.Dal.CampaignProductsDal dal = new SUL.Dal.CampaignProductsDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 CampaignId		{ get ; set; }

        public System.Int32 CampaignDetailsId { get; set; }

		public System.Int32 ProductId		{ get ; set; }

		public System.Decimal StartQuantity		{ get ; set; }

		public System.Decimal EndQuantity		{ get ; set; }

        public System.Decimal Price { get; set; }


		public  Int32 InsertCampaignProducts()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@CampaignId", CampaignId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CampaignDetailsId", CampaignDetailsId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@StartQuantity", StartQuantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@EndQuantity", EndQuantity.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Price", Price.ToString(CultureInfo.InvariantCulture));

			return dal.InsertCampaignProducts(lstItems);
		}

		public  Int32 UpdateCampaignProducts()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@CampaignId", CampaignId.ToString());
            lstItems.Add("@CampaignDetailsId", CampaignDetailsId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ProductId", ProductId.ToString());
			lstItems.Add("@StartQuantity", StartQuantity.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@EndQuantity", EndQuantity.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Price", Price.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateCampaignProducts(lstItems);
		}

		public  Int32 DeleteCampaignProductsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteCampaignProductsById(lstItems);
		}

		public List<CampaignProducts> GetAllCampaignProducts()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllCampaignProducts(lstItems);
			List<CampaignProducts> CampaignProductsList = new List<CampaignProducts>();
			foreach (DataRow dr in dt.Rows)
			{
				CampaignProductsList.Add(GetObject(dr));
			}
			return CampaignProductsList;
		}

		public CampaignProducts  GetCampaignProductsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetCampaignProductsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  CampaignProducts GetObject(DataRow dr)
		{
			CampaignProducts objCampaignProducts = new CampaignProducts();
			objCampaignProducts.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objCampaignProducts.CampaignId = (dr["CampaignId"] == DBNull.Value) ? 0 : (Int32)dr["CampaignId"];
            objCampaignProducts.CampaignDetailsId = (dr["CampaignDetailsId"] == DBNull.Value) ? 0 : (Int32)dr["CampaignDetailsId"];
			objCampaignProducts.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			objCampaignProducts.StartQuantity = (dr["StartQuantity"] == DBNull.Value) ? 0 : (Decimal)dr["StartQuantity"];
			objCampaignProducts.EndQuantity = (dr["EndQuantity"] == DBNull.Value) ? 0 : (Decimal)dr["EndQuantity"];
            objCampaignProducts.Price = (dr["Price"] == DBNull.Value) ? 0 : (Decimal)dr["Price"];

			return objCampaignProducts;
		}
	}
}
