using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class AppFunctionalityDal : SUL.Dal.Base.AppFunctionalityDalBase
	{
		public AppFunctionalityDal() : base()
		{
		}
        public int GetAppFunctionalityId(Hashtable lstData)
        {
            string whereCondition = " where AppFunctionality.Functionality = @Functionality";
            DataTable dt = new DataTable();
            try
            {
                string FunctionId = ExecuteScaler("AppFunctionality", "Id", whereCondition, lstData);
                if (FunctionId == "")
                {
                    return 0;
                }
                else
                    return int.Parse(FunctionId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetAppFunctionalityWithModule(Hashtable lstData)
        {
            string whereCondition = " where AppFunctionality.Functionality = @Functionality and ParentId = @ParentId";
            DataTable dt = new DataTable();
            try
            {
                string FunctionId = ExecuteScaler("AppFunctionality", "Id", whereCondition, lstData);
                if (FunctionId == "")
                {
                    return 0;
                }
                else
                    return int.Parse(FunctionId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
