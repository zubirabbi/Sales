using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ItemReturnMasterDal : SUL.Dal.Base.ItemReturnMasterDalBase
	{
		public ItemReturnMasterDal() : base()
		{
		}
        public int GetlastItemReturnCode(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumIDbyCondition("ItemReturnMaster", "cast(Id as int)", 0, "", lstData);

                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int GetMaxItemReturnMasterId(Hashtable lstData)
        {
            return GetMaximumIDbyCondition("ItemReturnMaster", "Id", 1, "", lstData);
        }
        public DataTable GetItemReturnListFromViewList(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewItemReturnMaster", "*", "", lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SetActiveStatus(Hashtable lstData)
        {
            string sqlQuery = "Update ItemReturnMaster set  Status = @Status where Id=@Id  ;";
            try
            {
                int success = ExecuteNonQuery(sqlQuery, lstData);
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
	}

}
