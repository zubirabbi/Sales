using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class SpairPartsBase
	{
		protected static SUL.Dal.SpairPartsDal dal = new SUL.Dal.SpairPartsDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 ProductId		{ get ; set; }

		public System.Int32 SpairPartId		{ get ; set; }

		public System.Int32 Quentity		{ get ; set; }


		public  Int32 InsertSpairParts()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@SpairPartId", SpairPartId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@Quentity", Quentity.ToString(CultureInfo.InvariantCulture));

			return dal.InsertSpairParts(lstItems);
		}

		public  Int32 UpdateSpairParts()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@ProductId", ProductId.ToString());
			lstItems.Add("@SpairPartId", SpairPartId.ToString());
			lstItems.Add("@Quentity", Quentity.ToString());

			return dal.UpdateSpairParts(lstItems);
		}

		public  Int32 DeleteSpairPartsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteSpairPartsById(lstItems);
		}

		public List<SpairParts> GetAllSpairParts()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllSpairParts(lstItems);
			List<SpairParts> SpairPartsList = new List<SpairParts>();
			foreach (DataRow dr in dt.Rows)
			{
				SpairPartsList.Add(GetObject(dr));
			}
			return SpairPartsList;
		}

		public SpairParts  GetSpairPartsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetSpairPartsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  SpairParts GetObject(DataRow dr)
		{

			SpairParts objSpairParts = new SpairParts();
			objSpairParts.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objSpairParts.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
			objSpairParts.SpairPartId = (dr["SpairPartId"] == DBNull.Value) ? 0 : (Int32)dr["SpairPartId"];
			objSpairParts.Quentity = (dr["Quentity"] == DBNull.Value) ? 0 : (Int32)dr["Quentity"];

			return objSpairParts;
		}
	}
}
