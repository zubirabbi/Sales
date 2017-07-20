using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class AppFunctionality : SUL.Bll.Base.AppFunctionalityBase
	{
		private static SUL.Dal.AppFunctionalityDal Dal = new SUL.Dal.AppFunctionalityDal();
		public AppFunctionality() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Functionality"></param>
        /// <returns></returns>
        public int GetAppFunctionalityId(string _Functionality)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Functionality", _Functionality);

            return dal.GetAppFunctionalityId(lstItems);
        }

        public int GetAppFunctionalityId(string _Functionality, int _Module)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Functionality", _Functionality);
            lstItems.Add("@ParentId", _Module);

            return dal.GetAppFunctionalityWithModule(lstItems);
        }
	}
}
