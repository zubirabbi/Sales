using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class UsersDal : SUL.Dal.Base.UsersDalBase
	{
		public UsersDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckUserNameExistance(Hashtable lstData, bool isNewEntry)
        {

            string whereCondition = string.Empty;

            if (isNewEntry)
                whereCondition = " where Users.UserName = @UserName";
            else
                whereCondition = "where Users.UserName = @UserName And Users.Id <> @Id";

            int count = 0;
            try
            {
                count = CheckExistence("Users", "Id", whereCondition, lstData);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public int CheckUserExistance(Hashtable lstData)
        {
            string whereCondition = " where Users.EmployeeId = @EmployeeId";
            int count = 0;
            try
            {
                count = CheckExistence("Users", "Id", whereCondition, lstData);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int GetLastId(int companyId)
        {

            string whereCondition = " Where CompanyId = '" + companyId + "'";

            try
            {
                return GetMaximumID("Users", "Id", 0, whereCondition);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetUserByUserName(Hashtable lstData)
        {

            string whereCondition = " where Users.UserName = @UserName and isActive = 1";

            try
            {
                return GetDataTable("Users", "*", whereCondition, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetAllUsersList(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("Users", "*", " Where Users.IsActive = 1;", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public int DeleteUsersById(Hashtable lstData)
        {

            try
            {
                int success = ExecuteStoreProcedure("DeleteUser", lstData);
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }
        public DataTable GetUserListFromViewList(Hashtable lstData)
        {

            string whereCondition = "  ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewUserInfo", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
