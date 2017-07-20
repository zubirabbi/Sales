using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class IncentiveSetupDetailsDal : SUL.Dal.Base.IncentiveSetupDetailsDalBase
	{
		public IncentiveSetupDetailsDal() : base()
		{
		}
        public DataTable GetIncentiveSetupDetailsByMasterId(Hashtable lstData)
        {
            string whereCondition = " where IncentiveSetupDetails.MasterId = @Id ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("IncentiveSetupDetails", "*", whereCondition, lstData);
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
        /// <returns></returns>
        public DataTable GetIncentiveDetailsForDealer()
        {
            try
            {
                string whereCondition = " where MasterId =(select Id from IncentiveSetup where Type=2 and IsActive = 1)";
                
                DataTable dt = GetDataTable("IncentiveSetupDetails", "*", whereCondition, new Hashtable());
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
        /// <param name="lstItems"></param>
        /// <returns></returns>
        public DataTable GetIncentiveDetailsForDesignation(Hashtable lstItems)
        {
            try
            {
                string whereCondition = " where MasterId in (select Id from IncentiveSetup where Type=1 and DesignationId = @DesignationId)";

                DataTable dt = GetDataTable("IncentiveSetupDetails", "*", whereCondition, lstItems);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      
        public int GetlastLastSlno(Hashtable lstData)
        {

            try
            {
                int maxId = GetMaximumIDbyCondition("IncentiveSetupDetails", "cast(Slno as int)", 0, " Where MasterId = @MasterId ", lstData);

                return maxId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}

}
