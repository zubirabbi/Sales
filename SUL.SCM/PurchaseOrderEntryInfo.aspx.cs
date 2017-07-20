using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using SUL.Bll.Base;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class PurchaseOrderEntryInfo : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private List<TempProductInfo> POproductInfo;
        private Users _user;
        private Company _company;
        //private  bool isNewEntry;
        //private  bool isNewDetailEntry;


        private class TempProductInfo
        {
            public int Id { get; set; }
            public int POCategory { get; set; }
            public int POProduct { get; set; }
            public string POProductName { get; set; }
            public int POQuentity { get; set; }
            public decimal POUnitePrice { get; set; }
            public int POUniteId { get; set; }
            public string POUniteName { get; set; }
            public decimal TotalProductPrice { get; set; }

        }

        private bool IsValidSession()
        {
            if (Session["user"] == null)
            {
                return false;
            }

            _user = (Users)Session["user"];
            _company = (Company)Session["company"];

            return _user.Id != 0;

        }
        private bool IsValidPageForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);

            if (!PermissionUser.IsView)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);
                bool permission = Permission.IsView;
                return permission;
            }
            else
                return true;
        }
        private bool IsValidInsertForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);

            if (!PermissionUser.IsInsert)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);
                bool permission = Permission.IsInsert;
                return permission;
            }
            else
                return true;
        }
        private bool IsValidUpdateForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);

            if (!PermissionUser.IsUpdate)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);
                bool permission = Permission.IsUpdate;
                return permission;
            }
            else
                return true;
        }
        private bool IsValidDeleteForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);

            if (!PermissionUser.IsDelete)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId, _user.CompanyId);
                bool permission = Permission.IsDelete;
                return permission;
            }
            else
                return true;
        }

        private void LoadProductUnite()
        {
            try
            {
                if (rdropPOProduct.SelectedIndex <= 0)
                {
                    List<ProductUnit> lstProductUnits = new ProductUnit().GetAllProductUnit();
                    lstProductUnits.Insert(0, new ProductUnit());

                    rdropProductUnit.DataTextField = "UnitCode";
                    rdropProductUnit.DataValueField = "Id";
                    rdropProductUnit.DataSource = lstProductUnits;
                    rdropProductUnit.DataBind();
                    rdropProductUnit.SelectedIndex = 0;
                }
                else
                {
                    int productId = int.Parse(rdropPOProduct.SelectedValue);

                    Product objProduct = new Product().GetProductById(productId);

                    int baseUniteId = objProduct.BaseUnit;

                    List<ProductUnit> lstProductUnits = new ProductUnit().GetProductUnitByBaseId(baseUniteId);
                    lstProductUnits.Insert(0, new ProductUnit());

                    rdropProductUnit.DataTextField = "UnitCode";
                    rdropProductUnit.DataValueField = "Id";
                    rdropProductUnit.DataSource = lstProductUnits;
                    rdropProductUnit.DataBind();
                    rdropProductUnit.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load Product Unit dropdown" + ex);
            }
        }

        private void LoadVendorName()
        {
            try
            {
                List<Supplier> lstSuppliers = new Supplier().GetAllSupplier();
                lstSuppliers.Insert(0, new Supplier());

                rdropVandorName.DataTextField = "Name";
                rdropVandorName.DataValueField = "Id";
                rdropVandorName.DataSource = lstSuppliers;
                rdropVandorName.DataBind();

                rdropVandorName.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load vendor name.");
            }
        }

        private void LoadProductNameBySupplier(int SupId)
        {
            try
            {
                List<Product> lstProducts = new Product().GetAllProductbySupplierId(SupId);
                lstProducts.Insert(0, new Product());


                rdropPOProduct.DataTextField = "ProductName";
                rdropPOProduct.DataValueField = "Id";
                rdropPOProduct.DataSource = lstProducts;
                rdropPOProduct.DataBind();

                rdropPOProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going erong to load product dropdownlist");
            }
        }

        private void LoadAllProduct()
        {
            try
            {
                List<Product> lstProducts = new Product().GetAllProduct();
                lstProducts.Insert(0, new Product());


                rdropPOProduct.DataTextField = "ProductName";
                rdropPOProduct.DataValueField = "Id";
                rdropPOProduct.DataSource = lstProducts;
                rdropPOProduct.DataBind();

                rdropPOProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load product name.");
            }
        }



        private void LoadProductDetailsGrid()
        {
            if (POproductInfo.Count == 0)
            {
                RadGridAddProduct.DataSource = new string[] { };
                return;
            }


            RadGridAddProduct.DataSource = POproductInfo;
            RadGridAddProduct.DataBind();
        }

        private void LoadChequeDetailsFromDataBase(int id)
        {
            try
            {
                POproductInfo = new List<TempProductInfo>();
                List<PurchaseOrderDetails> lstOrderDetailses =
                            new PurchaseOrderDetails().GetAllPurchaseOrderDetailsBymasterId(id);

                if (lstOrderDetailses.Count > 0)
                {

                    foreach (PurchaseOrderDetails lstPurchaseOrderDetails in lstOrderDetailses)
                    {
                        TempProductInfo tmpProductInfo = new TempProductInfo();

                        tmpProductInfo.Id = int.Parse(lstPurchaseOrderDetails.Id.ToString());
                        lblDetailsId.Text = lstPurchaseOrderDetails.Id.ToString();
                        tmpProductInfo.POCategory = lstPurchaseOrderDetails.CategoryId;
                        tmpProductInfo.POProduct = lstPurchaseOrderDetails.ProductId;
                        tmpProductInfo.POQuentity = lstPurchaseOrderDetails.Quantity;
                        tmpProductInfo.POUnitePrice = lstPurchaseOrderDetails.UnitPrice;
                        tmpProductInfo.POProductName = lstPurchaseOrderDetails.ProductName;
                        tmpProductInfo.POUniteId = lstPurchaseOrderDetails.UnitId;
                        tmpProductInfo.POUniteName = lstPurchaseOrderDetails.UnitName;
                        tmpProductInfo.TotalProductPrice = lstPurchaseOrderDetails.LineTotal;
                        POproductInfo.Add(tmpProductInfo);
                    }
                }

                Session["POproductInfo"] = POproductInfo;
                LoadProductDetailsGrid();
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsValidSession())
            {
                string str = Request.QueryString.ToString();
                if (str == string.Empty)
                    Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx");
                else
                    Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx?" + str);

            }

            if (!IsValidPageForUser())
            {
                Alert.Show("Sorry, You Don't Have permission to access this page.");
                Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx", false);
            }

            if (Session["POproductInfo"] != null)
                POproductInfo = (List<TempProductInfo>)Session["POproductInfo"];
            else
                POproductInfo = new List<TempProductInfo>();

            if (!IsPostBack)
            {
                lblisNewEntry.Text = "true";
                lblisNewDetailEntry.Text = "true";
                POproductInfo = new List<TempProductInfo>();
                Session["POproductInfo"] = null;
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                this.LoadVendorName();
                this.LoadAllProduct();
                this.LoadProductUnite();

                if (Request.QueryString["Id"] != null)
                {

                    string id = "";
                    id = Request.QueryString["Id"];

                    PurchaseOrderMaster objOrderMaster = new PurchaseOrderMaster().GetPurchaseOrderMasterById(int.Parse(id));
                    lblId.Text = objOrderMaster.Id.ToString();
                    rdropVandorName.SelectedValue = objOrderMaster.VendorId.ToString();
                    lblSupId.Text = objOrderMaster.VendorId.ToString();
                    LoadProductNameBySupplier(int.Parse(objOrderMaster.VendorId.ToString()));

                    rtxtVendorAddress.Text = objOrderMaster.VandorAddress;
                    rtxtProductOrderNumber.Text = objOrderMaster.OrderNo.ToString();
                    rdtProductOrderDate.SelectedDate = objOrderMaster.OrderDate;

                    rtxtTotalPrice.Text = objOrderMaster.TotalPrice.ToString();

                    lblisNewEntry.Text = "false";
                    lblisNewDetailEntry.Text = "false";
                    LoadChequeDetailsFromDataBase(int.Parse(id));


                }
            }
        }


        protected void btnAddProductInPO_OnClick(object sender, EventArgs e)
        {
            try
            {
                Regex regexForQuentity = new Regex("^[0-9]*$");
                Regex regexForPriceUnite = new Regex(@"^[1-9]\d*(\.\d+)?$");


                #region validation

                if (rdropPOProduct.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a product first.");
                    rdropPOProduct.Focus();
                    return;
                }
                if (rtxtPOQuentity.Text == string.Empty || !regexForQuentity.IsMatch(rtxtPOQuentity.Text))
                {
                    Alert.Show("Please enter the product quantity");
                    rtxtPOQuentity.Focus();
                    return;
                }
                if (rtxtPOunitPrice.Text == string.Empty || !regexForPriceUnite.IsMatch(rtxtPOunitPrice.Text))
                {
                    Alert.Show("Please enter a valid unit price.");
                    rtxtPOunitPrice.Focus();
                    return;
                }
                Product product = new Product().GetProductById(int.Parse(rdropPOProduct.SelectedValue));
                if (product.Id == 0)
                {
                    Alert.Show("Not a valid product.");
                    return;
                }

                #endregion


                TempProductInfo objTempProductInfo =
                    POproductInfo.Find(x => x.POProduct == int.Parse(rdropPOProduct.SelectedValue));
                if (objTempProductInfo != null)
                {
                    if (objTempProductInfo.POProduct == 0)
                        objTempProductInfo = new TempProductInfo();
                    else
                    {
                        POproductInfo.Remove(objTempProductInfo);
                    }
                }
                else
                {
                    objTempProductInfo = new TempProductInfo();
                }


                decimal totaluPrice = decimal.Parse(rtxtPOQuentity.Text) * decimal.Parse(rtxtPOunitPrice.Text);
                lblpriceosProduct.Text = totaluPrice.ToString();

                objTempProductInfo.POCategory = product.ProductCategory;
                objTempProductInfo.POProduct = int.Parse(rdropPOProduct.SelectedValue);
                objTempProductInfo.POQuentity = int.Parse(rtxtPOQuentity.Text);
                objTempProductInfo.POUnitePrice = decimal.Parse(rtxtPOunitPrice.Text);
                objTempProductInfo.TotalProductPrice = totaluPrice;
                objTempProductInfo.POProductName = rdropPOProduct.SelectedItem.Text;
                objTempProductInfo.POUniteId = int.Parse(rdropProductUnit.SelectedValue);
                objTempProductInfo.POUniteName = rdropProductUnit.SelectedItem.Text;

                if (POproductInfo.Count == 0)
                    POproductInfo = new List<TempProductInfo>();

                POproductInfo.Add(objTempProductInfo);
                Session["POproductInfo"] = POproductInfo;

                this.LoadProductDetailsGrid();
                decimal totalsum;

                totalsum = POproductInfo.Sum(x => x.TotalProductPrice);

                rtxtTotalPrice.Text = totalsum.ToString();

                lblisNewDetailEntry.Text = "true";
            }
            catch (Exception ex)
            {
                Alert.Show("Something Is going wrong to save Purchase Details." + ex);
            }
        }

        protected void RadGridAddProduct_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnSelect")
                {


                    GridDataItem item = (GridDataItem)e.Item;

                    lblDetailsId.Text = item["colId"].Text;

                    rdropPOProduct.SelectedValue = item["colPOProduct"].Text;
                    rtxtPOQuentity.Text = item["colPOQuentity"].Text;
                    rtxtPOunitPrice.Text = item["colPOUnitePrice"].Text;
                    rtxtTotalUnitePrice.Text = item["colTotalProductPrice"].Text;
                    rdropProductUnit.SelectedValue = item["colPOUniteId"].Text;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to select Add product grid." + ex);
            }

        }

        protected void rdropPOProduct_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdropPOProduct.SelectedIndex <= 0)
                {
                    return;
                }

                Product objProduct = new Product().GetProductById(int.Parse(rdropPOProduct.SelectedValue));
                if (objProduct.Id != 0)
                    rdropProductUnit.SelectedValue = objProduct.BaseUnit.ToString();
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation

            if (rdropVandorName.SelectedIndex <= 0)
            {
                Alert.Show("please select a vendor name.");
                rdropVandorName.Focus();
                return;
            }
            if (rtxtVendorAddress.Text == String.Empty)
            {
                Alert.Show("please enert a Vendor address.");
                rtxtVendorAddress.Focus();
                return;
            }
            if (rtxtProductOrderNumber.Text == string.Empty)
            {
                Alert.Show("please enter a order number");
                rtxtProductOrderNumber.Focus();
                return;
            }
            if (rdtProductOrderDate.SelectedDate == null)
            {
                Alert.Show("please select order date");
                rdtProductOrderDate.Focus();
                return;
            }

            if (POproductInfo.Count == 0)
            {
                Alert.Show("No product information was given for the order.");
                return;
            }

            #endregion

            try
            {

                PurchaseOrderMaster objPurchaseOrderMaster = new PurchaseOrderMaster();

                objPurchaseOrderMaster.VendorId = int.Parse(rdropVandorName.SelectedValue);
                objPurchaseOrderMaster.VandorAddress = rtxtVendorAddress.Text;
                objPurchaseOrderMaster.OrderNo = rtxtProductOrderNumber.Text;
                objPurchaseOrderMaster.TotalPrice = decimal.Parse(rtxtTotalPrice.Text);
                objPurchaseOrderMaster.OrderDate = DateTime.Parse(rdtProductOrderDate.SelectedDate.ToString());
                objPurchaseOrderMaster.VendorName = rdropVandorName.SelectedItem.Text;
                objPurchaseOrderMaster.CreatedBy = _user.Id;
                objPurchaseOrderMaster.Status = "Order Created";

                int success = 0;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objPurchaseOrderMaster.InsertPurchaseOrderMaster();
                    if (success > 0)
                    {
                        lblId.Text = new PurchaseOrderMaster().GetMaxPurchaseMasterId().ToString();
                        lblisNewEntry.Text = "false";
                    }
                    else
                    {
                        Alert.Show("Data was not saved successfully.");
                        return;
                    }
                }
                else
                {
                    objPurchaseOrderMaster.Id = int.Parse(lblId.Text);
                    success = objPurchaseOrderMaster.UpdatePurchaseOrderMaster();
                }
                if (success == 1)
                {
                    if (POproductInfo.Count != 0)
                    {
                        foreach (TempProductInfo tempProductInfo in POproductInfo)
                        {
                            PurchaseOrderDetails objPurchaseOrderDetails = new PurchaseOrderDetails();

                            objPurchaseOrderDetails.MasterId = int.Parse(lblId.Text);
                            objPurchaseOrderDetails.CategoryId = tempProductInfo.POCategory;
                            objPurchaseOrderDetails.ProductId = tempProductInfo.POProduct;
                            objPurchaseOrderDetails.Quantity = tempProductInfo.POQuentity;
                            objPurchaseOrderDetails.UnitPrice = tempProductInfo.POUnitePrice;
                            objPurchaseOrderDetails.ProductName = tempProductInfo.POProductName;
                            objPurchaseOrderDetails.UnitId = tempProductInfo.POUniteId;
                            objPurchaseOrderDetails.UnitName = tempProductInfo.POUniteName;
                            objPurchaseOrderDetails.LineTotal = tempProductInfo.TotalProductPrice;

                            List<PurchaseOrderDetails> countOrderDetails = new PurchaseOrderDetails().GetAllPurchaseOrderDetailsBymasterIdProductIdCateId(objPurchaseOrderDetails.ProductId, objPurchaseOrderDetails.UnitId, objPurchaseOrderDetails.MasterId);

                            if (countOrderDetails.Count == 0)
                            {
                                success = objPurchaseOrderDetails.InsertPurchaseOrderDetails();
                            }
                            else
                            {
                                objPurchaseOrderDetails.Id = tempProductInfo.Id;
                                success = objPurchaseOrderDetails.UpdatePurchaseOrderDetails();
                            }
                            if (success == 0)
                            {
                                Alert.Show("The Purchase Master saved successfully, but failed to save Purchase Details.");
                                return;
                            }
                            else
                            {
                                Alert.Show("Data save succesfully");
                            }

                        }
                        if (success != 0)
                        {
                            //Response.Redirect("PurchaseOrderEntryInfo.aspx");
                            ClearAllInfo();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save Purchase order" + ex);
            }
        }


        protected void rdropVandorName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProductNameBySupplier(int.Parse(rdropVandorName.SelectedValue));

            Supplier objSupplier = new Supplier().GetSupplierById(int.Parse(rdropVandorName.SelectedValue));

            rtxtVendorAddress.Text = objSupplier.CompanyAddress;

            string OrderCode = new PurchaseOrderMaster().GetlastSupplierCode(int.Parse(rdropVandorName.SelectedValue));

            //rtxtProductOrderNumber.Text = OrderCode;

        }


        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ClearAllInfo();
        }

        private void ClearAllInfo()
        {
            rtxtPOQuentity.Text = "";
            rtxtPOunitPrice.Text = "";
            rtxtProductOrderNumber.Text = "";
            rtxtTotalPrice.Text = "";
            rtxtTotalUnitePrice.Text = "";
            rtxtVendorAddress.Text = "";
            rdtProductOrderDate.SelectedDate = null;
            rdropPOProduct.SelectedIndex = 0;
            rdropProductUnit.SelectedIndex = 0;
            rdropVandorName.SelectedIndex = 0;
            RadGridAddProduct.DataSource = new string[] { };
            RadGridAddProduct.Rebind();
            this.LoadAllProduct();
            POproductInfo = new List<TempProductInfo>();
            Session["POproductInfo"] = POproductInfo;

            Response.Redirect("PurchaseOrderEntryInfo.aspx");
        }
    }
}