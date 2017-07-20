using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class UserRoleMapping : SUL.Bll.Base.UserRoleMappingBase
	{
		private static SUL.Dal.UserRoleMappingDal Dal = new SUL.Dal.UserRoleMappingDal();
		public UserRoleMapping() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Int32 DeleteUserRoleMappingByUserId(Int32 userId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@UserId", userId);

            return dal.DeleteUserRoleMappingByUserId(lstItems);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userId"></param>
        /// <param name="_companyId"></param>
        /// <returns></returns>
        public UserRoleMapping GetUserRoleMappingByUserId(int _userId, int _companyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@UserId", _userId);
            lstItems.Add("@CompanyId", _companyId);

            try
            {
                DataTable dt = dal.GetUserRoleMappingByUserId(lstItems);

                if (dt.Rows.Count > 0)
                {
                    return GetObject(dt.Rows[0]);
                }
                else
                    return new UserRoleMapping();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public List<UserRoleMapping> GetAllUserRoleMappingbyUserId(int _userId, int CompanyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@UserId", _userId);
            lstItems.Add("@CompanyId", CompanyId);
            DataTable dt = dal.GetUserRoleMappingByUserId(lstItems);
            List<UserRoleMapping> UserRoleMappingList = new List<UserRoleMapping>();
            foreach (DataRow dr in dt.Rows)
            {
                UserRoleMappingList.Add(GetObject(dr));
            }
            return UserRoleMappingList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userId"></param>
        /// <param name="_companyId"></param>
        /// <returns></returns>
        public int GetRoleIdForUser(int _userId, int _companyId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@UserId", _userId);
            lstItems.Add("@CompanyId", _companyId);

            try
            {
                string strRoleId = dal.GetRoleIdForUser(lstItems);

                if (strRoleId != null && strRoleId != string.Empty)
                    return int.Parse(strRoleId);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
