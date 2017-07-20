using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class WareHouseBase
	{
		protected static SUL.Dal.WareHouseDal dal = new SUL.Dal.WareHouseDal();

		public System.Int64 Id		{ get ; set; }

		public System.String Code		{ get ; set; }

		public System.String Name		{ get ; set; }

		public System.String Location		{ get ; set; }

		public System.String Incharge		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }

        public System.Int32 CategoryId { get; set; }


		public  Int32 InsertWareHouse()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Code", Code);
			lstItems.Add("@Name", Name);
			lstItems.Add("@Location", Location);
			lstItems.Add("@Incharge", Incharge);
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CategoryId", CategoryId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertWareHouse(lstItems);
		}

		public  Int32 UpdateWareHouse()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@Code", Code);
			lstItems.Add("@Name", Name);
			lstItems.Add("@Location", Location);
			lstItems.Add("@Incharge", Incharge);
			lstItems.Add("@IsActive", IsActive);
			lstItems.Add("@CompanyId", CompanyId.ToString());
            lstItems.Add("@CategoryId", CategoryId.ToString(CultureInfo.InvariantCulture));

			return dal.UpdateWareHouse(lstItems);
		}

		public  Int32 DeleteWareHouseById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteWareHouseById(lstItems);
		}

		public List<WareHouse> GetAllWareHouse(int CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
			DataTable dt = dal.GetAllWareHouse(lstItems);
			List<WareHouse> WareHouseList = new List<WareHouse>();
			foreach (DataRow dr in dt.Rows)
			{
				WareHouseList.Add(GetObject(dr));
			}
			return WareHouseList;
		}
        public List<WareHouse> GetAllWareHousebyWareHouseID()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllWareHousebyWareHouseID(lstItems);
            List<WareHouse> WareHouseList = new List<WareHouse>();
            foreach (DataRow dr in dt.Rows)
            {
                WareHouseList.Add(GetObject(dr));
            }
            return WareHouseList;
        }

		public WareHouse GetWareHouseById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetWareHouseById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  WareHouse GetObject(DataRow dr)
		{

			WareHouse objWareHouse = new WareHouse();
			objWareHouse.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objWareHouse.Code = (dr["Code"] == DBNull.Value) ? "" : (String)dr["Code"];
			objWareHouse.Name = (dr["Name"] == DBNull.Value) ? "" : (String)dr["Name"];
			objWareHouse.Location = (dr["Location"] == DBNull.Value) ? "" : (String)dr["Location"];
			objWareHouse.Incharge = (dr["Incharge"] == DBNull.Value) ? "" : (String)dr["Incharge"];
			objWareHouse.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];
			objWareHouse.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];
            objWareHouse.CategoryId = (dr["CategoryId"] == DBNull.Value) ? 0 : (Int32)dr["CategoryId"];

			return objWareHouse;
		}
	}
}
