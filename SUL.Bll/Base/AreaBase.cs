using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
    public class AreaBase
    {
        protected static SUL.Dal.AreaDal dal = new SUL.Dal.AreaDal();

        public System.Int32 Id { get; set; }

        public System.String AreaName { get; set; }

        public System.String AreaCode { get; set; }

        public System.String Description { get; set; }

        public System.Int32 RegionId { get; set; }

        public System.Int32 ChanelSpecialities { get; set; }

        public System.Int32 JrChanelSpecialities { get; set; }

        public System.Boolean IsActive { get; set; }


        public Int32 InsertArea()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@AreaName", AreaName);
            lstItems.Add("@AreaCode", AreaCode);
            lstItems.Add("@Description", Description);
            lstItems.Add("@RegionId", RegionId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ChanelSpecialities", ChanelSpecialities.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@JrChanelSpecialities", JrChanelSpecialities.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IsActive", IsActive);

            return dal.InsertArea(lstItems);
        }

        public Int32 UpdateArea()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@AreaName", AreaName);
            lstItems.Add("@AreaCode", AreaCode);
            lstItems.Add("@Description", Description);
            lstItems.Add("@RegionId", RegionId.ToString());
            lstItems.Add("@ChanelSpecialities", ChanelSpecialities.ToString());
            lstItems.Add("@JrChanelSpecialities", JrChanelSpecialities.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@IsActive", IsActive);

            
            return dal.UpdateArea(lstItems);
        }

        public Int32 DeleteAreaById(Int32 Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id);

            return dal.DeleteAreaById(lstItems);
        }

        public List<Area> GetAllArea()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllArea(lstItems);
            List<Area> AreaList = new List<Area>();
            foreach (DataRow dr in dt.Rows)
            {
                AreaList.Add(GetObject(dr));
            }
            return AreaList;
        }

        public Area GetAreaById(Int32 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);

            DataTable dt = dal.GetAreaById(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }

        protected Area GetObject(DataRow dr)
        {

            Area objArea = new Area();
            objArea.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
            objArea.AreaName = (dr["AreaName"] == DBNull.Value) ? "" : (String)dr["AreaName"];
            objArea.AreaCode = (dr["AreaCode"] == DBNull.Value) ? "" : (String)dr["AreaCode"];
            objArea.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];
            objArea.RegionId = (dr["RegionId"] == DBNull.Value) ? 0 : (Int32)dr["RegionId"];
            objArea.ChanelSpecialities = (dr["ChanelSpecialities"] == DBNull.Value) ? 0 : (Int32)dr["ChanelSpecialities"];
            objArea.JrChanelSpecialities = (dr["JrChanelSpecialities"] == DBNull.Value) ? 0 : (Int32)dr["JrChanelSpecialities"];
            objArea.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];

            return objArea;
        }
    }
}
