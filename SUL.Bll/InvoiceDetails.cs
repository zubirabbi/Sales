using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using SUL.Bll.Base;

namespace SUL.Bll
{
	public class InvoiceDetails : SUL.Bll.Base.InvoiceDetailsBase
	{
		private static SUL.Dal.InvoiceDetailsDal Dal = new SUL.Dal.InvoiceDetailsDal();
		public InvoiceDetails() : base()
		{
		}
        
	}
}
