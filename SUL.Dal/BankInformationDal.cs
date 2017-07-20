using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class BankInformationDal : SUL.Dal.Base.BankInformationDalBase
	{
		public BankInformationDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetBankInformationBySupplierId(Hashtable lstData)
        {
            string whereCondition = " where BankInformation.Type = @Type And BankInformation.TypeId = @TypeId ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("BankInformation", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllViewbankInfo(Hashtable lstData)
        {
            string whereCondition = " ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewBankInformation", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllViewbankInfoByCompany(Hashtable lstData)
        {
            string whereCondition = " where Type='Company' ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewBankInformation", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
