using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class CampaignOfferProductsBase
	{
		protected static SUL.Dal.CampaignOfferProductsDal dal = new SUL.Dal.CampaignOfferProductsDal();

		public System.Int32 Id		{ get ; set; }

        public System.Int32 CampaignId { get; set; }

		public System.Int32 CampaignDetailsId		{ get ; set; }

		public System.String ProductType		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }

        public System.Int32 Quantity { get; set; }


		public  Int32 InsertCampaignOfferProducts()
		{
			Hashtable lstItems = new Hashtable();
            lstItems.Add("@CampaignId", CampaignId.ToString());
			lstItems.Add("@CampaignDetailsId", CampaignDetailsId.ToString());
			lstItems.Add("@ProductType", ProductType);
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Quantity", Quantity.ToString(CultureInfo.InvariantCulture));

			return dal.InsertCampaignOfferProducts(lstItems);
		}

		public  Int32 UpdateCampaignOfferProducts()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@CampaignId", CampaignId.ToString());
			lstItems.Add("@CampaignDetailsId", CampaignDetailsId.ToString());
			lstItems.Add("@ProductType", ProductType);
			lstItems.Add("@ProductId", ProductId.ToString());
			lstItems.Add("@Quantity", Quantity.ToString());

			return dal.UpdateCampaignOfferProducts(lstItems);
		}

		public  Int32 DeleteCampaignOfferProductsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteCampaignOfferProductsById(lstItems);
		}

		public List<CampaignOfferProducts> GetAllCampaignOfferProducts()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllCampaignOfferProducts(lstItems);
			List<CampaignOfferProducts> CampaignOfferProductsList = new List<CampaignOfferProducts>();
			foreach (DataRow dr in dt.Rows)
			{
				CampaignOfferProductsList.Add(GetObject(dr));
			}
			return CampaignOfferProductsList;
		}

		public CampaignOfferProducts  GetCampaignOfferProductsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetCampaignOfferProductsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  CampaignOfferProducts GetObject(DataRow dr)
		{

			CampaignOfferProducts objCampaignOfferProducts = new CampaignOfferProducts();
			objCampaignOfferProducts.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
            objCampaignOfferProducts.CampaignId = (dr["CampaignId"] == DBNull.Value) ? 0 : (Int32)dr["CampaignId"];			
            objCampaignOfferProducts.CampaignDetailsId = (dr["CampaignDetailsId"] == DBNull.Value) ? 0 : (Int32)dr["CampaignDetailsId"];
			objCampaignOfferProducts.ProductType = (dr["ProductType"] == DBNull.Value) ? "" : (String)dr["ProductType"];
			objCampaignOfferProducts.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			objCampaignOfferProducts.Quantity = (dr["Quantity"] == DBNull.Value) ? 0 : (Int32)dr["Quantity"];

			return objCampaignOfferProducts;
		}
	}
}
