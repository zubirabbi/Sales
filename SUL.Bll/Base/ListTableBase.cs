using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ListTableBase
	{
		protected static SUL.Dal.ListTableDal dal = new SUL.Dal.ListTableDal();

		public System.Int32 Id		{ get ; set; }

		public System.String ListType		{ get ; set; }

		public System.Int32 ListId		{ get ; set; }

		public System.String ListValue		{ get ; set; }

		public System.Int32 CompanyId		{ get ; set; }

		public System.Boolean IsActive		{ get ; set; }


		public  Int32 InsertListTable()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@ListType", ListType);
			lstItems.Add("@ListId", ListId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@ListValue", ListValue);
			lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));
			lstItems.Add("@IsActive", IsActive);

			return dal.InsertListTable(lstItems);
		}

		public  Int32 UpdateListTable()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@ListType", ListType);
			lstItems.Add("@ListId", ListId.ToString());
			lstItems.Add("@ListValue", ListValue);
			lstItems.Add("@CompanyId", CompanyId.ToString());
			lstItems.Add("@IsActive", IsActive);

			return dal.UpdateListTable(lstItems);
		}

		public  Int32 DeleteListTableById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteListTableById(lstItems);
		}

		public List<ListTable> GetAllListTable(int CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@CompanyId", CompanyId);
			DataTable dt = dal.GetAllListTable(lstItems);
			List<ListTable> ListTableList = new List<ListTable>();
			foreach (DataRow dr in dt.Rows)
			{
				ListTableList.Add(GetObject(dr));
			}
			return ListTableList;
		}

		public ListTable GetListTableById(int _Id,int _CompanyId)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);
			lstItems.Add("@CompanyId", _CompanyId);

			DataTable dt = dal.GetListTableById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		public  ListTable GetObject(DataRow dr)
		{

			ListTable objListTable = new ListTable();
			objListTable.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objListTable.ListType = (dr["ListType"] == DBNull.Value) ? "" : (String)dr["ListType"];
			objListTable.ListId = (dr["ListId"] == DBNull.Value) ? 0 : (Int32)dr["ListId"];
			objListTable.ListValue = (dr["ListValue"] == DBNull.Value) ? "" : (String)dr["ListValue"];
			objListTable.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];
			objListTable.IsActive = (dr["IsActive"] != DBNull.Value) && (Boolean)dr["IsActive"];

			return objListTable;
		}
	}
}
