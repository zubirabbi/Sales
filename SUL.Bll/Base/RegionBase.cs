using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class RegionBase
	{
		protected static SUL.Dal.RegionDal dal = new SUL.Dal.RegionDal();

		public System.Int32 Id		{ get ; set; }

		public System.String RegionName		{ get ; set; }

		public System.String RegionCode		{ get ; set; }

		public System.String Description		{ get ; set; }

		public System.Int32 ChanelManager		{ get ; set; }

        public System.Int32 AssistantChanelManager { get; set; }

		public System.Boolean IsActive		{ get ; set; }


		public  Int32 InsertRegion()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@RegionName", RegionName);
			lstItems.Add("@RegionCode", RegionCode);
			lstItems.Add("@Description", Description);
			lstItems.Add("@ChanelManager", ChanelManager.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@AssistantChanelManager", AssistantChanelManager.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);

			return dal.InsertRegion(lstItems);
		}

		public  Int32 UpdateRegion()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@RegionName", RegionName);
			lstItems.Add("@RegionCode", RegionCode);
			lstItems.Add("@Description", Description);
			lstItems.Add("@ChanelManager", ChanelManager.ToString());
            lstItems.Add("@AssistantChanelManager", AssistantChanelManager.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);

			return dal.UpdateRegion(lstItems);
		}

		public  Int32 DeleteRegionById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteRegionById(lstItems);
		}

		public List<Region> GetAllRegion()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllRegion(lstItems);
			List<Region> RegionList = new List<Region>();
			foreach (DataRow dr in dt.Rows)
			{
				RegionList.Add(GetObject(dr));
			}
			return RegionList;
		}

		public Region  GetRegionById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetRegionById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  Region GetObject(DataRow dr)
		{

			Region objRegion = new Region();
			objRegion.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objRegion.RegionName = (dr["RegionName"] == DBNull.Value) ? "" : (String)dr["RegionName"];
			objRegion.RegionCode = (dr["RegionCode"] == DBNull.Value) ? "" : (String)dr["RegionCode"];
			objRegion.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];
			objRegion.ChanelManager = (dr["ChanelManager"] == DBNull.Value) ? 0 : (Int32)dr["ChanelManager"];
            objRegion.AssistantChanelManager = (dr["AssistantChanelManager"] == DBNull.Value) ? 0 : (Int32)dr["AssistantChanelManager"];
			objRegion.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];

			return objRegion;
		}
	}
}
