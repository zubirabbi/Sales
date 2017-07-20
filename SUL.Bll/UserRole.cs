using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class UserRole : SUL.Bll.Base.UserRoleBase
	{
		private static SUL.Dal.UserRoleDal Dal = new SUL.Dal.UserRoleDal();
		public UserRole() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Id"></param>
        /// <param name="_Role"></param>
        /// <param name="isNewEntry"></param>
        /// <returns></returns>
        public int CheckRoleExistance(int _Id, string _Role, bool isNewEntry)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Role", _Role);

            if (!isNewEntry) lstItems.Add("@Id", _Id);

            return dal.CheckRoleExistance(lstItems, isNewEntry);
        }
	}
}
