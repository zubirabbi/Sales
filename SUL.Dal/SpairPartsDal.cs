using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class SpairPartsDal : SUL.Dal.Base.SpairPartsDalBase
	{
		public SpairPartsDal() : base()
		{
		}
        public DataTable GetSpairPartsFromViewList(Hashtable lstData)
        {

            string whereCondition = " Where ProductId = @ProductId";
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("vewSpairPartsInfo", "*", whereCondition, lstData);
                return dt;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllSpairPartsByProduct(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("SpairParts", "*", " Where ProductId = @ProductId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
