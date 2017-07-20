using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using SUL.Dal;

namespace SUL.Bll.Base
{
    public class ProductBase
    {
        protected static SUL.Dal.ProductDal dal = new SUL.Dal.ProductDal();

        public System.Int32 Id { get; set; }

        public System.String ProductCode { get; set; }

        public System.String ProductName { get; set; }

        public System.String ModelNo { get; set; }

        public System.Int32 ProductCategory { get; set; }

        public System.Int32 BaseUnit { get; set; }

        public System.Decimal MRP { get; set; }

        public System.Decimal DP { get; set; }

        public System.Decimal RP { get; set; }

        public System.Decimal CostPrice { get; set; }

        public System.Decimal CurrentBalance { get; set; }

        public System.Decimal DP2 { get; set; }


        public Int32 InsertProduct()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@ProductCode", ProductCode);
            lstItems.Add("@ProductName", ProductName);
            lstItems.Add("@ModelNo", ModelNo);
            lstItems.Add("@ProductCategory", ProductCategory.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@BaseUnit", BaseUnit.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@MRP", MRP.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@DP", DP.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@RP", RP.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CostPrice", CostPrice.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CurrentBalance", CurrentBalance.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@DP2", DP2.ToString(CultureInfo.InvariantCulture));

            return dal.InsertProduct(lstItems);
        }

        public Int32 UpdateProduct()
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id.ToString());
            lstItems.Add("@ProductCode", ProductCode);
            lstItems.Add("@ProductName", ProductName);
            lstItems.Add("@ModelNo", ModelNo);
            lstItems.Add("@ProductCategory", ProductCategory.ToString());
            lstItems.Add("@BaseUnit", BaseUnit.ToString());
            lstItems.Add("@MRP", MRP.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@DP", DP.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@RP", RP.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CostPrice", CostPrice.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@CurrentBalance", CurrentBalance.ToString(CultureInfo.InvariantCulture));
            lstItems.Add("@DP2", DP2.ToString(CultureInfo.InvariantCulture));

            return dal.UpdateProduct(lstItems);
        }

        public Int32 DeleteProductById(Int32 Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", Id);

            return dal.DeleteProductById(lstItems);
        }

        public List<Product> GetAllProduct()
        {
            Hashtable lstItems = new Hashtable();
            DataTable dt = dal.GetAllProduct(lstItems);
            List<Product> ProductList = new List<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductList.Add(GetObject(dr));
            }
            return ProductList;
        }

        public Product GetProductById(Int32 _Id)
        {
            Hashtable lstItems = new Hashtable();
            lstItems.Add("@Id", _Id);

            DataTable dt = dal.GetProductById(lstItems);
            if (dt.Rows.Count == 0)
            {
                return  new Product();
            }
            else
            {
                DataRow dr = dt.Rows[0];
                return GetObject(dr);
            }
        }

        protected Product GetObject(DataRow dr)
        {

            Product objProduct = new Product();
            objProduct.Id = (dr["Id"] == DBNull.Value) ? 0 : (Int32)dr["Id"];
            objProduct.ProductCode = (dr["ProductCode"] == DBNull.Value) ? "" : (String)dr["ProductCode"];
            objProduct.ProductName = (dr["ProductName"] == DBNull.Value) ? "" : (String)dr["ProductName"];
            objProduct.ModelNo = (dr["ModelNo"] == DBNull.Value) ? "" : (String)dr["ModelNo"];
            objProduct.ProductCategory = (dr["ProductCategory"] == DBNull.Value) ? 0 : (Int32)dr["ProductCategory"];
            objProduct.BaseUnit = (dr["BaseUnit"] == DBNull.Value) ? 0 : (Int32)dr["BaseUnit"];
            objProduct.MRP = (dr["MRP"] == DBNull.Value) ? 0 : (Decimal)dr["MRP"];
            objProduct.DP = (dr["DP"] == DBNull.Value) ? 0 : (Decimal)dr["DP"];
            objProduct.RP = (dr["RP"] == DBNull.Value) ? 0 : (Decimal)dr["RP"];
            objProduct.CostPrice = (dr["CostPrice"] == DBNull.Value) ? 0 : (Decimal)dr["CostPrice"];
            objProduct.CurrentBalance = (dr["CurrentBalance"] == DBNull.Value) ? 0 : (Decimal)dr["CurrentBalance"];
            objProduct.DP2 = (dr["DP2"] == DBNull.Value) ? 0 : (Decimal)dr["DP2"];

            return objProduct;
        }
    }
}
