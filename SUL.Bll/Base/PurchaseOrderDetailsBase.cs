using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
    public class PurchaseOrderDetailsBase
    {
        protected static SUL.Dal.PurchaseOrderDetailsDal dal = new SUL.Dal.PurchaseOrderDetailsDal();

        public System.Int64 Id { get; set; }

        public System.Int32 CategoryId { get; set; }

        public System.Int32 ProductId { get; set; }

        public System.String ProductName { get; set; }

        public System.Int32 Quantity { get; set; }

        public System.Decimal UnitPrice { get; set; }

        public System.Int32 MasterId { get; set; }

        public System.Decimal LineTotal { get; set; }

        public System.String UnitName { get; set; }

        public System.Int32 UnitId { get; set; }


        public Int32 InsertPurchaseOrderDetails()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@CategoryId", CategoryId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ProductId", ProductId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@ProductName", ProductName);
            lstItems.Add("@Quantity", Quantity.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UnitPrice", UnitPrice.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@MasterId", MasterId.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@LineTotal", LineTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UnitName", UnitName);
            lstItems.Add("@UnitId", UnitId.ToString(CultureInfo.InvariantCulture));

            return dal.InsertPurchaseOrderDetails(lstItems);
        }

        public Int32 UpdatePurchaseOrderDetails()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@CategoryId", CategoryId.ToString());
            lstItems.Add("@ProductId", ProductId.ToString());
            lstItems.Add("@ProductName", ProductName);
            lstItems.Add("@Quantity", Quantity.ToString());
            lstItems.Add("@UnitPrice", UnitPrice.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@MasterId", MasterId.ToString());
            lstItems.Add("@LineTotal", LineTotal.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@UnitName", UnitName);
            lstItems.Add("@UnitId", UnitId.ToString(CultureInfo.InvariantCulture));

            return dal.UpdatePurchaseOrderDetails(lstItems);
        }

        public Int32 DeletePurchaseOrderDetailsById(Int64 Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id);

            return dal.DeletePurchaseOrderDetailsById(lstItems);
        }

        public List<PurchaseOrderDetails> GetAllPurchaseOrderDetails()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllPurchaseOrderDetails(lstItems);
            List<PurchaseOrderDetails> PurchaseOrderDetailsList = new List<PurchaseOrderDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                PurchaseOrderDetailsList.Add(GetObject(dr));
            }
            return PurchaseOrderDetailsList;
        }

        public PurchaseOrderDetails GetPurchaseOrderDetailsById(Int64 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);

            DataTable dt = dal.GetPurchaseOrderDetailsById(lstItems);
            DataRow dr = dt.Rows[0];
            return GetObject(dr);
        }

        protected PurchaseOrderDetails GetObject(DataRow dr)
        {

            PurchaseOrderDetails objPurchaseOrderDetails = new PurchaseOrderDetails();
            objPurchaseOrderDetails.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int64)dr["Id"];
            objPurchaseOrderDetails.CategoryId = (dr["CategoryId"] == DBNull.Value) ? 0 : (Int32)dr["CategoryId"];
            objPurchaseOrderDetails.ProductId = (dr["ProductId"] == DBNull.Value) ? 0 : (Int32)dr["ProductId"];
            objPurchaseOrderDetails.ProductName = (dr["ProductName"] == DBNull.Value) ? "" : (String)dr["ProductName"];
            objPurchaseOrderDetails.Quantity = (dr["Quantity"] == DBNull.Value) ? 0 : (Int32)dr["Quantity"];
            objPurchaseOrderDetails.UnitPrice = (dr["UnitPrice"] == DBNull.Value) ? 0 : (Decimal)dr["UnitPrice"];
            objPurchaseOrderDetails.MasterId = (dr["MasterId"] == DBNull.Value) ? 0 : (Int32)dr["MasterId"];
            objPurchaseOrderDetails.LineTotal = (dr["LineTotal"] == DBNull.Value) ? 0 : (Decimal)dr["LineTotal"];
            objPurchaseOrderDetails.UnitName = (dr["UnitName"] == DBNull.Value) ? "" : (String)dr["UnitName"];
            objPurchaseOrderDetails.UnitId = (dr["UnitId"] == DBNull.Value) ? 0 : (Int32)dr["UnitId"];

            return objPurchaseOrderDetails;
        }
    }
}
