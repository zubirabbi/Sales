using System;
using System.Text;
using System.Data;
using System.Collections;
using SUL.Dal.Base;

namespace SUL.Dal
{
	public class ItemReturnDetailsDal : SUL.Dal.Base.ItemReturnDetailsDalBase
	{
		public ItemReturnDetailsDal() : base()
		{
		}
        public DataTable GetAllItemReturnDetailsByProductId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ItemReturnDetails", "*", " Where ProductId = @ProductId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable GetAllItemReturnDetailsByMasterId(Hashtable lstData)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetDataTable("ItemReturnDetails", "*", " Where MasterId = @MasterId", lstData);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
	}
}
