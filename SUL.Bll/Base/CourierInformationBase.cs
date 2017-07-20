using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class CourierInformationBase
	{
		protected static SUL.Dal.CourierInformationDal dal = new SUL.Dal.CourierInformationDal();

		public System.Int64 Id		{ get ; set; }

		public System.String Name		{ get ; set; }

		public System.String Address		{ get ; set; }

		public System.String ContactName		{ get ; set; }

		public System.String ContactNo		{ get ; set; }


		public  Int32 InsertCourierInformation()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Name", Name);
			lstItems.Add("@Address", Address);
			lstItems.Add("@ContactName", ContactName);
			lstItems.Add("@ContactNo", ContactNo.ToString(CultureInfo.InvariantCulture));

			return dal.InsertCourierInformation(lstItems);
		}

		public  Int32 UpdateCourierInformation()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@Name", Name);
			lstItems.Add("@Address", Address);
			lstItems.Add("@ContactName", ContactName);
			lstItems.Add("@ContactNo", ContactNo.ToString());

			return dal.UpdateCourierInformation(lstItems);
		}

		public  Int32 DeleteCourierInformationById(Int64 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteCourierInformationById(lstItems);
		}

		public List<CourierInformation> GetAllCourierInformation()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllCourierInformation(lstItems);
			List<CourierInformation> CourierInformationList = new List<CourierInformation>();
			foreach (DataRow dr in dt.Rows)
			{
				CourierInformationList.Add(GetObject(dr));
			}
			return CourierInformationList;
		}

	    public CourierInformation GetCourierInformationById(Int64 _Id)
	    {
	        Hashtable lstItems = new Hashtable();
	        lstItems.Add("@Id", _Id);

	        DataTable dt = dal.GetCourierInformationById(lstItems);
	        if (dt.Rows.Count == 0)
	            return new CourierInformation();
	        else
	        {
	            DataRow dr = dt.Rows[0];
	            return GetObject(dr);
	        }
	    }

	    protected  CourierInformation GetObject(DataRow dr)
		{

			CourierInformation objCourierInformation = new CourierInformation();
			objCourierInformation.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
			objCourierInformation.Name = (dr["Name"] == DBNull.Value) ? "" : (String)dr["Name"];
			objCourierInformation.Address = (dr["Address"] == DBNull.Value) ? "" : (String)dr["Address"];
			objCourierInformation.ContactName = (dr["ContactName"] == DBNull.Value) ? "" : (String)dr["ContactName"];
			objCourierInformation.ContactNo = (dr["ContactNo"] == DBNull.Value) ? "" : (String)dr["ContactNo"];

			return objCourierInformation;
		}
	}
}
