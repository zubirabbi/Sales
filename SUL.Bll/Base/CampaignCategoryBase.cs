using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class CampaignCategoryBase
	{
		protected static SUL.Dal.CampaignCategoryDal dal = new SUL.Dal.CampaignCategoryDal();

		public System.Int32 Id		{ get ; set; }

		public System.String CampaignCategory		{ get ; set; }

		public System.Boolean IsOnProduct		{ get ; set; }

		public System.Boolean IsOnQuantity		{ get ; set; }

		public System.Boolean IsOnSalesValue		{ get ; set; }

		public System.Boolean IsGiftItemIncluded		{ get ; set; }


		public  Int32 InsertCampaignCategory()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CampaignCategory", CampaignCategory);
			lstItems.Add("@IsOnProduct", IsOnProduct);
			lstItems.Add("@IsOnQuantity", IsOnQuantity);
			lstItems.Add("@IsOnSalesValue", IsOnSalesValue);
			lstItems.Add("@IsGiftItemIncluded", IsGiftItemIncluded);

			return dal.InsertCampaignCategory(lstItems);
		}

		public  Int32 UpdateCampaignCategory()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@CampaignCategory", CampaignCategory);
			lstItems.Add("@IsOnProduct", IsOnProduct);
			lstItems.Add("@IsOnQuantity", IsOnQuantity);
			lstItems.Add("@IsOnSalesValue", IsOnSalesValue);
			lstItems.Add("@IsGiftItemIncluded", IsGiftItemIncluded);

			return dal.UpdateCampaignCategory(lstItems);
		}

		public  Int32 DeleteCampaignCategoryById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteCampaignCategoryById(lstItems);
		}

		public List<CampaignCategory> GetAllCampaignCategory()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllCampaignCategory(lstItems);
			List<CampaignCategory> CampaignCategoryList = new List<CampaignCategory>();
			foreach (DataRow dr in dt.Rows)
			{
				CampaignCategoryList.Add(GetObject(dr));
			}
			return CampaignCategoryList;
		}

		public CampaignCategory  GetCampaignCategoryById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetCampaignCategoryById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  CampaignCategory GetObject(DataRow dr)
		{

			CampaignCategory objCampaignCategory = new CampaignCategory();
			objCampaignCategory.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objCampaignCategory.CampaignCategory = (dr["CampaignCategory"] == DBNull.Value) ? "" : (String)dr["CampaignCategory"];
			objCampaignCategory.IsOnProduct = (dr["IsOnProduct"] == DBNull.Value) ? false : (Boolean)dr["IsOnProduct"];
			objCampaignCategory.IsOnQuantity = (dr["IsOnQuantity"] == DBNull.Value) ? false : (Boolean)dr["IsOnQuantity"];
			objCampaignCategory.IsOnSalesValue = (dr["IsOnSalesValue"] == DBNull.Value) ? false : (Boolean)dr["IsOnSalesValue"];
			objCampaignCategory.IsGiftItemIncluded = (dr["IsGiftItemIncluded"] == DBNull.Value) ? false : (Boolean)dr["IsGiftItemIncluded"];

			return objCampaignCategory;
		}
	}
}
