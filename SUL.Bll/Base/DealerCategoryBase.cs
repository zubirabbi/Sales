using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class DealerCategoryBase
	{
		protected static SUL.Dal.DealerCategoryDal dal = new SUL.Dal.DealerCategoryDal();

		public System.Int64 Id		{ get ; set; }

		public System.String CategoryCode		{ get ; set; }

		public System.String Description		{ get ; set; }


		public  Int32 InsertDealerCategory()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CategoryCode", CategoryCode);
			lstItems.Add("@Description", Description);

			return dal.InsertDealerCategory(lstItems);
		}

		public  Int32 UpdateDealerCategory()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@CategoryCode", CategoryCode);
			lstItems.Add("@Description", Description);

			return dal.UpdateDealerCategory(lstItems);
		}

		public  Int32 DeleteDealerCategoryById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteDealerCategoryById(lstItems);
		}

		public List<DealerCategory> GetAllDealerCategory()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllDealerCategory(lstItems);
			List<DealerCategory> DealerCategoryList = new List<DealerCategory>();
			foreach (DataRow dr in dt.Rows)
			{
				DealerCategoryList.Add(GetObject(dr));
			}
			return DealerCategoryList;
		}

		public DealerCategory  GetDealerCategoryById(Int64 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetDealerCategoryById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  DealerCategory GetObject(DataRow dr)
		{

			DealerCategory objDealerCategory = new DealerCategory();
			objDealerCategory.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objDealerCategory.CategoryCode = (dr["CategoryCode"] == DBNull.Value) ? "" : (String)dr["CategoryCode"];
			objDealerCategory.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];

			return objDealerCategory;
		}
	}
}
