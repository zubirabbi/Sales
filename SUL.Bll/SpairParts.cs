using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class SpairParts : SUL.Bll.Base.SpairPartsBase
	{
		private static SUL.Dal.SpairPartsDal Dal = new SUL.Dal.SpairPartsDal();
		public SpairParts() : base()
		{
		}
        public DataTable GetSpairPartsFromViewList( int mainProductId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId", mainProductId);

            DataTable dt = dal.GetSpairPartsFromViewList(lstItems);

            return dt;
        }
        public List<SpairParts> GetAllSpairPartsByProduct(int productId)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductId",productId);
            DataTable dt = dal.GetAllSpairPartsByProduct(lstItems);
            List<SpairParts> SpairPartsList = new List<SpairParts>();
            foreach (DataRow dr in dt.Rows)
            {
                SpairPartsList.Add(GetObject(dr));
            }
            return SpairPartsList;
        }
	}
}
