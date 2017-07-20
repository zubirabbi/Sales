using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ListTableDal : SUL.Dal.Base.ListTableDalBase
	{
		public ListTableDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetAllListTableByType(Hashtable lstData)
        {

            string whereCondition = " where ListTable.ListType = @ListType And ListTable.IsActive = 1";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ListTable", "*", whereCondition, lstData);
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
        public DataTable GetListTableByTypeIdAndValue(Hashtable lstData)
        {
            string whereCondition = " where ListTable.ListId = @Id And ListTable.ListType = @ListType";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ListTable", "*", whereCondition, lstData);
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
        public int GetlastListTypeId(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumIDbyCondition("ListTable", "ListId", 0, "Where ListType=@ListType", lstData);
                return maxId;
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
        public DataTable GetAllListTableByGroup(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ListTable", "Distinct ListType", " Where ListTable.IsActive = 1;", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllDataByListTypeId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ListTable", "*", " Where ListType=@ListType;", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllListTableByTypeAndPid(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ListTable", "*", "Where ListType = @ListType and ListId in (select color from ProductColor where ProductId = @ProductId);", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
