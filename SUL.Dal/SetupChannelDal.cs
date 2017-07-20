using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class SetupChannelDal : SUL.Dal.Base.SetupChannelDalBase
	{
		public SetupChannelDal() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public DataTable GetAllSetupChannelByChannels(Hashtable lstData)
        {
            string whereCondition = " where SetupChannel.ChannelPosition = @ChannelPosition ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("SetupChannel", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllViewSetupChannel(Hashtable lstData)
        {
            string whereCondition = "  ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewSetUpChannel", "*", whereCondition, lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
