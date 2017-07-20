using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class DataFilterSetupDal : SUL.Dal.Base.DataFilterSetupDalBase
	{
		public DataFilterSetupDal() : base()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
	    public DataTable GetApprovedStatusList(Hashtable lstData)
	    {
            const string whereCondition = " where ListType='RequisitionStatus' and ListId in(select StatusId from DataFilterSetup where UserId = @UserId And RoleId = @RoleId And DataItemTypeId = @DataItemTypeId and IsAllowed = 1)";
            
            try
            {
                DataTable dt = GetDataTable("ListTable", "*", whereCondition, lstData);
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
        public DataTable GetPaymetStatusList(Hashtable lstData)
        {
            const string whereCondition = " where ListType='PaymentStatus' and ListId in(select StatusId from DataFilterSetup where UserId = @UserId And RoleId = @RoleId And DataItemTypeId = @DataItemTypeId and IsAllowed = 1)";

            try
            {
                DataTable dt = GetDataTable("ListTable", "*", whereCondition, lstData);
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
        public DataTable GetSetupByType(Hashtable lstData)
	    {
            const string whereCondition = " where UserId = @UserId And RoleId = @RoleId And DataItemTypeId = @DataItemTypeId and IsAllowed = 1";
            
            try
            {
                DataTable dt = GetDataTable("DataFilterSetup", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
	    }
        public DataTable GetSetupByTypebyVewReq(Hashtable lstData)
        {
            const string whereCondition = " where UserId = @UserId And RoleId = @RoleId And DataItemTypeId = @DataItemTypeId"; 
            try
            {
                DataTable dt = GetDataTable("vewDataFilterReq", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetSetupByTypebyVewPay(Hashtable lstData)
        {
            const string whereCondition = " where UserId = @UserId And RoleId = @RoleId And DataItemTypeId = @DataItemTypeId ";
            try
            {
                DataTable dt = GetDataTable("vewDataFilterPay", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
