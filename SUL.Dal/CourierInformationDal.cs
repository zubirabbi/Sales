using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class CourierInformationDal : SUL.Dal.Base.CourierInformationDalBase
	{
		public CourierInformationDal() : base()
		{
		}
        public DataTable GetTransportViewList(Hashtable lstData)
        {

            string whereCondition = " ";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewTransport", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
