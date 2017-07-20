using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class DataFilterSetup : SUL.Bll.Base.DataFilterSetupBase
	{
		private static SUL.Dal.DataFilterSetupDal Dal = new SUL.Dal.DataFilterSetupDal();
		public DataFilterSetup() : base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <param name="dataItemId"></param>
        /// <returns></returns>
        public List<ListTable> GetApprovedStatusList(int userId, int roleId, int dataItemId)
	    {
            Hashtable lstData = new Hashtable();
            lstData.Add("@UserId", userId);
            lstData.Add("@RoleId", roleId);
            lstData.Add("@DataItemTypeId", dataItemId);

	        DataTable dt = dal.GetApprovedStatusList(lstData);
            List<ListTable> statusList = new List<ListTable>();
	        if (dt.Rows.Count > 0)
	        {
	            statusList.AddRange(from DataRow row in dt.Rows select new ListTable().GetObject(row));
	        }
            return statusList;
	    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <param name="dataItemId"></param>
        /// <returns></returns>
        public List<ListTable> GetPaymetStatusList(int userId, int roleId, int dataItemId)
        {
            Hashtable lstData = new Hashtable();
            lstData.Add("@UserId", userId);
            lstData.Add("@RoleId", roleId);
            lstData.Add("@DataItemTypeId", dataItemId);

            DataTable dt = dal.GetPaymetStatusList(lstData);
            List<ListTable> statusList = new List<ListTable>();
            if (dt.Rows.Count > 0)
            {
                statusList.AddRange(from DataRow row in dt.Rows select new ListTable().GetObject(row));
            }
            return statusList;
        }

        public List<DataFilterSetup> GetSetupByType(int userId, int roleId, int dataItemId)
        {
            Hashtable lstData = new Hashtable();
            lstData.Add("@UserId", userId);
            lstData.Add("@RoleId", roleId);
            lstData.Add("@DataItemTypeId", dataItemId);

            DataTable dt = dal.GetSetupByType(lstData);
            List<DataFilterSetup> statusList = new List<DataFilterSetup>();
            if (dt.Rows.Count > 0)
            {
                statusList.AddRange(from DataRow row in dt.Rows select new DataFilterSetup().GetObject(row));
            }
            return statusList;
        }

        public List<DataFilterSetup> GetSetupByTypebyVewReq(int userId, int roleId, int dataItemId)
        {
            Hashtable lstData = new Hashtable();
            lstData.Add("@UserId", userId);
            lstData.Add("@RoleId", roleId);
            lstData.Add("@DataItemTypeId", dataItemId);

            DataTable dt = dal.GetSetupByTypebyVewReq(lstData);
            List<DataFilterSetup> statusList = new List<DataFilterSetup>();
            if (dt.Rows.Count > 0)
            {
                statusList.AddRange(from DataRow row in dt.Rows select new DataFilterSetup().GetObject(row));
            }
            return statusList;
        }
        public List<DataFilterSetup> GetSetupByTypebyVewPay(int userId, int roleId, int dataItemId)
        {
            Hashtable lstData = new Hashtable();
            lstData.Add("@UserId", userId);
            lstData.Add("@RoleId", roleId);
            lstData.Add("@DataItemTypeId", dataItemId);

            DataTable dt = dal.GetSetupByTypebyVewPay(lstData);
            List<DataFilterSetup> statusList = new List<DataFilterSetup>();
            if (dt.Rows.Count > 0)
            {
                statusList.AddRange(from DataRow row in dt.Rows select new DataFilterSetup().GetObject(row));
            }
            return statusList;
        }
	}
}
