using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class ListTable : SUL.Bll.Base.ListTableBase
	{
		private static SUL.Dal.ListTableDal Dal = new SUL.Dal.ListTableDal();
		public ListTable() : base()
		{
		}
        public List<ListTable> GetAllListTableByType(string listType)
        {
            Hashtable lstItems = new Hashtable();

            lstItems.Add("@ListType", listType);
            DataTable dt = dal.GetAllListTableByType(lstItems);
            List<ListTable> ListTableList = new List<ListTable>();
            foreach (DataRow dr in dt.Rows)
            {
                ListTableList.Add(GetObject(dr));
            }
            return ListTableList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Id"></param>
        /// <param name="_CompanyId"></param>
        /// <returns></returns>
        public ListTable GetListTableByTypeIdAndValue(int _Id, string type)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);
            lstItems.Add("@ListType", type);

            DataTable dt = dal.GetListTableByTypeIdAndValue(lstItems);
            if (dt.Rows.Count == 0)
            {
                return new ListTable();
            }
            else
            {
                DataRow dr = dt.Rows[0];
                return GetObject(dr);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListType"></param>
        /// <returns></returns>
        public int GetlastListTypeId(string ListType)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ListType", ListType);

            return dal.GetlastListTypeId(lstItems);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public DataTable GetAllListTableByGroup(int CompanyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CompanyId", CompanyId);
            DataTable dt = dal.GetAllListTableByGroup(lstItems);

            return dt;
        }
        public DataTable GetAllDataByListTypeId(string ListType)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ListType", ListType);

            return dal.GetAllDataByListTypeId(lstItems);
        }
        public List<ListTable> GetAllListTableByTypeAndPid(string listType,int Pid)
        {
            Hashtable lstItems = new Hashtable();

            lstItems.Add("@ListType", listType);
            lstItems.Add("@ProductId", Pid);
            DataTable dt = dal.GetAllListTableByTypeAndPid(lstItems);
            List<ListTable> ListTableList = new List<ListTable>();
            foreach (DataRow dr in dt.Rows)
            {
                ListTableList.Add(GetObject(dr));
            }
            return ListTableList;
        }
	}
}
