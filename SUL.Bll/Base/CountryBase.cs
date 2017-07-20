using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class CountryBase
	{
		protected static SUL.Dal.CountryDal dal = new SUL.Dal.CountryDal();

		public System.Int32 Id		{ get ; set; }

		public System.String Name		{ get ; set; }

		public System.String Description		{ get ; set; }


		public  Int32 InsertCountry()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Name", Name);
			lstItems.Add("@Description", Description);

			return dal.InsertCountry(lstItems);
		}

		public  Int32 UpdateCountry()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@Name", Name);
			lstItems.Add("@Description", Description);

			return dal.UpdateCountry(lstItems);
		}

		public  Int32 DeleteCountryById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteCountryById(lstItems);
		}

		public List<Country> GetAllCountry()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllCountry(lstItems);
			List<Country> CountryList = new List<Country>();
			foreach (DataRow dr in dt.Rows)
			{
				CountryList.Add(GetObject(dr));
			}
			return CountryList;
		}

		public Country  GetCountryById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetCountryById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  Country GetObject(DataRow dr)
		{

			Country objCountry = new Country();
			objCountry.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objCountry.Name = (dr["Name"] == DBNull.Value) ? "" : (String)dr["Name"];
			objCountry.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];

			return objCountry;
		}
	}
}
