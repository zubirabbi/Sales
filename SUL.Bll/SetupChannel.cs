using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class SetupChannel : SUL.Bll.Base.SetupChannelBase
	{
		private static SUL.Dal.SetupChannelDal Dal = new SUL.Dal.SetupChannelDal();
		public SetupChannel() : base()
		{
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_channalPossition"></param>
        /// <returns></returns>
        public SetupChannel GetAllSetupChannelByChannels( string _channalPossition)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ChannelPosition", _channalPossition);
            DataTable dt = dal.GetAllSetupChannelByChannels(lstItems);
            SetupChannel setupChannel = new SetupChannel();
            if (dt.Rows.Count > 0)
            {
                setupChannel = GetObject(dt.Rows[0]);
            }
            
            return setupChannel;
        }
        public DataTable GetAllViewSetupChannel()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllViewSetupChannel(lstItems);

            return dt;
        }
	}
}
