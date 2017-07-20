using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class SetupChannelBase
	{
		protected static SUL.Dal.SetupChannelDal dal = new SUL.Dal.SetupChannelDal();

		public System.Int32 Id		{ get ; set; }

		public System.String ChannelPosition		{ get ; set; }

		public System.Int32 DesignationId		{ get ; set; }


		public  Int32 InsertSetupChannel()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@ChannelPosition", ChannelPosition);
			lstItems.Add("@DesignationId", DesignationId);

			return dal.InsertSetupChannel(lstItems);
		}

		public  Int32 UpdateSetupChannel()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@ChannelPosition", ChannelPosition);
			lstItems.Add("@DesignationId", DesignationId);

			return dal.UpdateSetupChannel(lstItems);
		}

		public  Int32 DeleteSetupChannelById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteSetupChannelById(lstItems);
		}

		public List<SetupChannel> GetAllSetupChannel()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllSetupChannel(lstItems);
			List<SetupChannel> SetupChannelList = new List<SetupChannel>();
			foreach (DataRow dr in dt.Rows)
			{
				SetupChannelList.Add(GetObject(dr));
			}
			return SetupChannelList;
		}

		public SetupChannel  GetSetupChannelById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetSetupChannelById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  SetupChannel GetObject(DataRow dr)
		{

			SetupChannel objSetupChannel = new SetupChannel();
			objSetupChannel.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objSetupChannel.ChannelPosition = (dr["ChannelPosition"] == DBNull.Value) ? "" : (String)dr["ChannelPosition"];
			objSetupChannel.DesignationId = (dr["DesignationId"] == DBNull.Value) ? 0 : (Int32)dr["DesignationId"];

			return objSetupChannel;
		}
	}
}
