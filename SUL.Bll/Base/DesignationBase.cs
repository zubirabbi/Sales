using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
    public class DesignationBase
    {
        protected static SUL.Dal.DesignationDal dal = new SUL.Dal.DesignationDal();

        public System.Int32 Id { get; set; }

        public System.String DesignationName { get; set; }

        public System.Int32 DepartmentId { get; set; }

        public System.String DesignationCode { get; set; }

        public System.Int32 Level { get; set; }

        public System.Int32 UpperHierarchy { get; set; }

        public System.String Responsibility { get; set; }

        public System.Boolean IsActive { get; set; }

        public System.Int32 CompanyId { get; set; }


        public Int32 InsertDesignation()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@DesignationName", DesignationName);
            lstItems.Add("@DesignationCode", DesignationCode);
            lstItems.Add("@DepartmentId", DepartmentId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Level", Level.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UpperHierarchy", UpperHierarchy);
            lstItems.Add("@Responsibility", Responsibility);
            lstItems.Add("@IsActive", IsActive);
            lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));

            return dal.InsertDesignation(lstItems);
        }

        public Int32 UpdateDesignation()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@DesignationName", DesignationName);
            lstItems.Add("@DesignationCode", DesignationCode);
            lstItems.Add("@DepartmentId", DepartmentId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@Level", Level.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UpperHierarchy", UpperHierarchy);
            lstItems.Add("@Responsibility", Responsibility);
            lstItems.Add("@IsActive", IsActive);
            lstItems.Add("@CompanyId", CompanyId.ToString(CultureInfo.InvariantCulture));

            return dal.UpdateDesignation(lstItems);
        }

        public Int32 DeleteDesignationById(Int32 Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id);

            return dal.DeleteDesignationById(lstItems);
        }

        public List<Designation> GetAllDesignation()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllDesignation(lstItems);
            List<Designation> DesignationList = new List<Designation>();
            foreach (DataRow dr in dt.Rows)
            {
                DesignationList.Add(GetObject(dr));
            }
            return DesignationList;
        }

        public Designation GetDesignationById(Int32 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);

            DataTable dt = dal.GetDesignationById(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }

        protected Designation GetObject(DataRow dr)
        {

            Designation objDesignation = new Designation();
            objDesignation.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
            objDesignation.DesignationName = (dr["DesignationName"] == DBNull.Value) ? "" : (String)dr["DesignationName"];
            objDesignation.DepartmentId = (dr["DepartmentId"] == DBNull.Value) ? 0 : (Int32)dr["DepartmentId"];
            objDesignation.DesignationCode = (dr["DesignationCode"] == DBNull.Value) ? "" : (String)dr["DesignationCode"];
            objDesignation.Level = (dr["Level"] == DBNull.Value) ? 0 : (Int32)dr["Level"];
            objDesignation.UpperHierarchy = (dr["UpperHierarchy"] == DBNull.Value) ? 0 : (Int32)dr["UpperHierarchy"];
            objDesignation.Responsibility = (dr["Responsibility"] == DBNull.Value) ? "" : (String)dr["Responsibility"];
            objDesignation.IsActive = (dr["IsActive"] == DBNull.Value) ? false : (Boolean)dr["IsActive"];
            objDesignation.CompanyId = (dr["CompanyId"] == DBNull.Value) ? 0 : (Int32)dr["CompanyId"];

            return objDesignation;
        }
    }
}
