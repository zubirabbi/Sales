using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class UserLoginLogDal : SUL.Dal.Base.UserLoginLogDalBase
	{
		public UserLoginLogDal() : base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetUserLastLogin(Hashtable lstData)
        {
            string whereCondition = " where UserLoginLog.UserId = @UserId and Id=(select max(Id) from UserLoginLog where UserId=@UserId)";
            
            try
            {
                DataTable dt = GetDataTable("UserLoginLog", "*", whereCondition, lstData);
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
        public int UpdateStatus(Hashtable lstData)
        {
            string queryString = "Update UserLoginLog set status = @Status, LogOutTime = @LogOutTime where UserLoginLog.Id = @Id";

            try
            {
                return ExecuteNonQuery(queryString, lstData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
