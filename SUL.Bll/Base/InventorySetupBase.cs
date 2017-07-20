using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class InventorySetupBase
	{
		protected static SUL.Dal.InventorySetupDal dal = new SUL.Dal.InventorySetupDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 PurchaseLocationId		{ get ; set; }


		public  Int32 InsertInventorySetup()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@PurchaseLocationId", PurchaseLocationId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertInventorySetup(lstItems);
		}

		public  Int32 UpdateInventorySetup()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@PurchaseLocationId", PurchaseLocationId.ToString());

			return dal.UpdateInventorySetup(lstItems);
		}

		public  Int32 DeleteInventorySetupById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteInventorySetupById(lstItems);
		}

		public List<InventorySetup> GetAllInventorySetup()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllInventorySetup(lstItems);
			List<InventorySetup> InventorySetupList = new List<InventorySetup>();
			foreach (DataRow dr in dt.Rows)
			{
				InventorySetupList.Add(GetObject(dr));
			}
			return InventorySetupList;
		}

		public InventorySetup  GetInventorySetupById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetInventorySetupById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  InventorySetup GetObject(DataRow dr)
		{

			InventorySetup objInventorySetup = new InventorySetup();
			objInventorySetup.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objInventorySetup.PurchaseLocationId = (dr["PurchaseLocationId"] == DBNull.Value) ? 0 : (Int32)dr["PurchaseLocationId"];

			return objInventorySetup;
		}
	}
}
