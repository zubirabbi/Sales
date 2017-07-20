using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class UserLoginLog : SUL.Bll.Base.UserLoginLogBase
	{
		private static SUL.Dal.UserLoginLogDal Dal = new SUL.Dal.UserLoginLogDal();
		public UserLoginLog() : base()
		{
		}

        public UserLoginLog GetUserLastLogin(Int64 _UserId)
	    {
	        Hashtable lstItems = new Hashtable();
	        lstItems.Add("@UserId", _UserId);

	        DataTable dt = dal.GetUserLastLogin(lstItems);
	        if (dt.Rows.Count > 0)
	        {
	            DataRow dr = dt.Rows[0];
	            return GetObject(dr);
	        }
	        else
	        {
	            return new UserLoginLog();
	        }
	    }

        public int UpdateStatus(Int64 _Id, string _status)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);
            lstItems.Add("@Status", _status);
            lstItems.Add("@LogOutTime", DateTime.Now);

            return dal.UpdateStatus(lstItems);
        }
	}
}
