using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
	public class ProblemsBase
	{
		protected static SUL.Dal.ProblemsDal dal = new SUL.Dal.ProblemsDal();

		public System.Int32 Id		{ get ; set; }

		public System.String Name		{ get ; set; }

		public System.String Description		{ get ; set; }

		public System.Int32 SeverLevel		{ get ; set; }


		public  Int32 InsertProblems()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Name", Name);
			lstItems.Add("@Description", Description);
			lstItems.Add("@SeverLevel", SeverLevel.ToString(CultureInfo.InvariantCulture));

			return dal.InsertProblems(lstItems);
		}

		public  Int32 UpdateProblems()
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id.ToString());
			lstItems.Add("@Name", Name);
			lstItems.Add("@Description", Description);
			lstItems.Add("@SeverLevel", SeverLevel.ToString());

			return dal.UpdateProblems(lstItems);
		}

		public  Int32 DeleteProblemsById(Int32 Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", Id);

			return dal.DeleteProblemsById(lstItems);
		}

		public List<Problems> GetAllProblems()
		{
			Hashtable lstItems = new Hashtable();
			DataTable dt = dal.GetAllProblems(lstItems);
			List<Problems> ProblemsList = new List<Problems>();
			foreach (DataRow dr in dt.Rows)
			{
				ProblemsList.Add(GetObject(dr));
			}
			return ProblemsList;
		}

		public Problems  GetProblemsById(Int32 _Id)
		{
			Hashtable lstItems = new Hashtable();
			lstItems.Add("@Id", _Id);

			DataTable dt = dal.GetProblemsById(lstItems);
			DataRow dr = dt.Rows[0];
			return GetObject(dr);
		}

		protected  Problems GetObject(DataRow dr)
		{

			Problems objProblems = new Problems();
			objProblems.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
			objProblems.Name = (dr["Name"] == DBNull.Value) ? "" : (String)dr["Name"];
			objProblems.Description = (dr["Description"] == DBNull.Value) ? "" : (String)dr["Description"];
			objProblems.SeverLevel = (dr["SeverLevel"] == DBNull.Value) ? 0 : (Int32)dr["SeverLevel"];

			return objProblems;
		}
	}
}
