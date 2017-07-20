using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class DataFilterSetupBase
	{
		protected static SUL.Dal.DataFilterSetupDal dal = new SUL.Dal.DataFilterSetupDal();

		public System.Int32 Id		{ get ; set; }

		public System.Int32 UserId		{ get ; set; }

		public System.Int32 RoleId		{ get ; set; }

		public System.Int32 DataItemTypeId		{ get ; set; }

		public System.Int32 StatusId		{ get ; set; }

		public System.Boolean IsAllowed		{ get ; set; }

		public System.Boolean IsLoadInitially		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }


		public  Int32 InsertDataFilterSetup()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@UserId", UserId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@RoleId", RoleId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@DataItemTypeId", DataItemTypeId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@StatusId", StatusId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsAllowed", IsAllowed);
			lstItems.Add("@IsLoadInitially", IsLoadInitially);
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));

			return dal.InsertDataFilterSetup(lstItems);
		}

		public  Int32 UpdateDataFilterSetup()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@UserId", UserId.ToString());
			lstItems.Add("@RoleId", RoleId.ToString());
			lstItems.Add("@DataItemTypeId", DataItemTypeId.ToString());
			lstItems.Add("@StatusId", StatusId.ToString());
			lstItems.Add("@IsAllowed", IsAllowed);
			lstItems.Add("@IsLoadInitially", IsLoadInitially);
			lstItems.Add("@CompanyId", CompanyId.ToString());

			return dal.UpdateDataFilterSetup(lstItems);
		}

		public  Int32 DeleteDataFilterSetupById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteDataFilterSetupById(lstItems);
		}

		public List<DataFilterSetup> GetAllDataFilterSetup(int CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
			DataTable dt = dal.GetAllDataFilterSetup(lstItems);
			List<DataFilterSetup> DataFilterSetupList = new List<DataFilterSetup>();
			foreach (DataRow dr in dt.Rows)
			{
				DataFilterSetupList.Add(GetObject(dr));
			}
			return DataFilterSetupList;
		}

		public DataFilterSetup GetDataFilterSetupById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetDataFilterSetupById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  DataFilterSetup GetObject(DataRow dr)
		{

			DataFilterSetup objDataFilterSetup = new DataFilterSetup();
			objDataFilterSetup.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objDataFilterSetup.UserId = (dr["UserId"] == DBNull.Value) ? 0 : (Int32)dr["UserId"];
			objDataFilterSetup.RoleId = (dr["RoleId"] == DBNull.Value) ? 0 : (Int32)dr["RoleId"];
			objDataFilterSetup.DataItemTypeId = (dr["DataItemTypeId"] == DBNull.Value) ? 0 : (Int32)dr["DataItemTypeId"];
			objDataFilterSetup.StatusId = (dr["StatusId"] == DBNull.Value) ? 0 : (Int32)dr["StatusId"];
			objDataFilterSetup.IsAllowed = (dr["IsAllowed"] == DBNull.Value) ? false : (Boolean)dr["IsAllowed"];
			objDataFilterSetup.IsLoadInitially = (dr["IsLoadInitially"] == DBNull.Value) ? false : (Boolean)dr["IsLoadInitially"];
			objDataFilterSetup.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];

			return objDataFilterSetup;
		}
	}
}
