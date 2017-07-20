using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class WarehouseCategoryBase
	{
		protected static SUL.Dal.WarehouseCategoryDal dal = new SUL.Dal.WarehouseCategoryDal();

		public System.Int32 Id		{ get ; set; }

		public System.String CategoryName		{ get ; set; }

		public System.Boolean IsVirtual		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }


		public  Int32 InsertWarehouseCategory()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CategoryName", CategoryName);
			lstItems.Add("@IsVirtual", IsVirtual);
			lstItems.Add("@IsActive", IsActive);

			return dal.InsertWarehouseCategory(lstItems);
		}

		public  Int32 UpdateWarehouseCategory()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@CategoryName", CategoryName);
			lstItems.Add("@IsVirtual", IsVirtual);
			lstItems.Add("@IsActive", IsActive);

			return dal.UpdateWarehouseCategory(lstItems);
		}

		public  Int32 DeleteWarehouseCategoryById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteWarehouseCategoryById(lstItems);
		}

		public List<WarehouseCategory> GetAllWarehouseCategory()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllWarehouseCategory(lstItems);
			List<WarehouseCategory> WarehouseCategoryList = new List<WarehouseCategory>();
			foreach (DataRow dr in dt.Rows)
			{
				WarehouseCategoryList.Add(GetObject(dr));
			}
			return WarehouseCategoryList;
		}

		public WarehouseCategory  GetWarehouseCategoryById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetWarehouseCategoryById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  WarehouseCategory GetObject(DataRow dr)
		{

			WarehouseCategory objWarehouseCategory = new WarehouseCategory();
			objWarehouseCategory.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objWarehouseCategory.CategoryName = (dr["CategoryName"] == DBNull.Value) ? "" : (String)dr["CategoryName"];
			objWarehouseCategory.IsVirtual = (dr["IsVirtual"] == DBNull.Value) ? false : (Boolean)dr["IsVirtual"];
			objWarehouseCategory.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];

			return objWarehouseCategory;
		}
	}
}
