using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class ProformaInvoiceEntryInfo : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private List<TempProductInfo> PIproductInfo;
        private Users _user;
        private Company _company;
        //private bool isNewEntry;
        //private bool isNewDetailEntry;


        private class TempProductInfo
        {
            public int Id { get; set; }
            public int PICategory { get; set; }
            public int PIProduct { get; set; }
            public string PIProductName { get; set; }
            public int POQuantity { get; set; }
            public int PIQuantity { get; set; }
            public decimal PIUnitePrice { get; set; }
            public int PIUniteId { get; set; }
            public string PIUniteName { get; set; }
            public decimal TotalProductPIPrice { get; set; }
            public int OrderDetailsId { get; set; }

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

                if (lstSuppliers.Count == 2)
                {
                    rdropVandorName.SelectedIndex = 1;
                    try
                    {
                        Supplier objSupplier = new Supplier().GetSupplierById(int.Parse(rdropVandorName.SelectedValue));
                        rtxtVendorAddress.Text = objSupplier.CompanyAddress;
                        lblSupId.Text = rdropVandorName.SelectedValue;
                        this.LoadOrderNumber(int.Parse(rdropVandorName.SelectedValue));
                        this.LoadVandorbank(int.Parse(rdropVandorName.SelectedValue));

                    }
                    catch (Exception ex)
                    {
                        Alert.Show("something is going wron to load Purcher Order information." + ex);
                    }
                }
                else
                    rdropVandorName.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load vendor name.");
            }
        }

        private void LoadVandorbank(int vendorId)
        {
            try
            {
                List<BankInformation> lstbaBankInformations =
                    new BankInformation().GetBankInformationBySupplier(vendorId,
                        "Supplier");

                lstbaBankInformations.Insert(0, new BankInformation());

                rdropbankInfo.DataTextField = "BankName";
                rdropbankInfo.DataValueField = "Id";
                rdropbankInfo.DataSource = lstbaBankInformations;
                rdropbankInfo.DataBind();
                if (lstbaBankInformations.Count == 2)
                    rdropbankInfo.SelectedIndex = 1;
                else
                    rdropbankInfo.SelectedIndex = 0;


            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to Load Vendor bank." + ex);
            }
        }

        private void LoadOrderNumber(int vandorId)
        {
            try
            {
                List<PurchaseOrderMaster> lstOrderMasters = new PurchaseOrderMaster().GetPurchaseOrderMasterByVandorId(vandorId);
                lstOrderMasters.Insert(0, new PurchaseOrderMaster());

                rdropPONumber.DataTextField = "OrderNo";
                rdropPONumber.DataValueField = "Id";
                rdropPONumber.DataSource = lstOrderMasters;
                rdropPONumber.DataBind();
                rdropPONumber.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load order number dropdown." + ex);
            }
        }

        private void LoadOrderNumberInPI(int vandorId)
        {
            try
            {
                List<PurchaseOrderMaster> lstOrderMasters = new PurchaseOrderMaster().GetOrderNoForPI(vandorId);
                lstOrderMasters.Insert(0, new PurchaseOrderMaster());

                rdropPONumber.DataTextField = "OrderNo";
                rdropPONumber.DataValueField = "Id";
                rdropPONumber.DataSource = lstOrderMasters;
                rdropPONumber.DataBind();
                if (lstOrderMasters.Count == 2)
                    rdropPONumber.SelectedIndex = 1;
                else
                    rdropPONumber.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load order number dropdown." + ex);
            }
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
                    if (lstProductUnits.Count == 2)
                        rdropProductUnit.SelectedIndex = 1;
                    else
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
                    if (lstProductUnits.Count == 2)
                        rdropProductUnit.SelectedIndex = 1;
                    else
                        rdropProductUnit.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load Product Unit dropdown" + ex);
            }
        }


        private void LoadProductNameByCategory(int catid, int supId)
        {
            try
            {
                List<Product> lstProducts = new Product().GetAllProductbyProductCategoryId(catid, supId);
                lstProducts.Insert(0, new Product());

                rdropPOProduct.DataTextField = "ProductName";
                rdropPOProduct.DataValueField = "Id";
                rdropPOProduct.DataSource = lstProducts;
                rdropPOProduct.DataBind();

                if (lstProducts.Count == 2)
                    rdropPOProduct.SelectedIndex = 1;
                else
                    rdropPOProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load product name.");
            }
        }

        private void LoadProductCategory()
        {
            try
            {
                List<ProductCategory> lstProductCategories = new ProductCategory().GetAllProductCategory();

                lstProductCategories.Insert(0, new ProductCategory());

                rdropPOCategory.DataTextField = "CategoryCode";
                rdropPOCategory.DataValueField = "Id";
                rdropPOCategory.DataSource = lstProductCategories;
                rdropPOCategory.DataBind();

                if (lstProductCategories.Count == 2)
                    rdropPOCategory.SelectedIndex = 1;
                else
                    rdropPOCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Product Category Info." + ex);
            }

        }

        private void LoadProductDetailsGrid()
        {
            try
            {
                if (PIproductInfo.Count == 0)
                {
                    RadGridAddProforma.DataSource = new string[] { };
                    return;
                }


                RadGridAddProforma.DataSource = PIproductInfo;
                RadGridAddProforma.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to Load grid" + ex);
            }

        }

        private void LoadChequeDetailsFromDataBase(int id)
        {
            try
            {
                if (bool.Parse(lblisNewEntry.Text) == false)
                {
                    List<PIDetails> lstDetailses = new PIDetails().GetAllPIDetailsBymasterId(id);
                    if (lstDetailses.Count > 0)
                    {
                        PIproductInfo = new List<TempProductInfo>();
                        foreach (PIDetails lstPiDetails in lstDetailses)
                        {
                            TempProductInfo tmpProductInfo = new TempProductInfo();
                            tmpProductInfo.Id = int.Parse(lstPiDetails.Id.ToString());
                            tmpProductInfo.PICategory = lstPiDetails.PICategory;
                            tmpProductInfo.PIProduct = lstPiDetails.ProductId;
                            tmpProductInfo.PIQuantity = lstPiDetails.PIQuantity;
                            tmpProductInfo.POQuantity = lstPiDetails.OrderQuantity;
                            tmpProductInfo.PIUnitePrice = lstPiDetails.PIUnitePrice;
                            tmpProductInfo.PIProductName = lstPiDetails.ProductName;
                            tmpProductInfo.PIUniteId = lstPiDetails.ProductUnit;
                            tmpProductInfo.PIUniteName = lstPiDetails.UnitName;
                            tmpProductInfo.TotalProductPIPrice = lstPiDetails.LineTotal;
                            tmpProductInfo.OrderDetailsId = int.Parse(lstPiDetails.Id.ToString());
                            PIproductInfo.Add(tmpProductInfo);    
                        }
                        Session["PIproductInfo"] = PIproductInfo;
                        LoadProductDetailsGrid();
                    }
                }
                else
                {
                    List<PurchaseOrderDetails> lstOrderDetailses =
                        new PurchaseOrderDetails().GetAllPurchaseOrderDetailsBymasterId(id);

                    if (lstOrderDetailses.Count > 0)
                    {
                        PIproductInfo = new List<TempProductInfo>();
                        foreach (PurchaseOrderDetails lstPurchaseOrderDetails in lstOrderDetailses)
                        {
                            TempProductInfo tmpProductInfo = new TempProductInfo();

                            tmpProductInfo.PICategory = lstPurchaseOrderDetails.CategoryId;
                            tmpProductInfo.PIProduct = lstPurchaseOrderDetails.ProductId;
                            tmpProductInfo.PIQuantity = lstPurchaseOrderDetails.Quantity;
                            tmpProductInfo.POQuantity = lstPurchaseOrderDetails.Quantity;
                            tmpProductInfo.PIUnitePrice = lstPurchaseOrderDetails.UnitPrice;
                            tmpProductInfo.PIProductName = lstPurchaseOrderDetails.ProductName;
                            tmpProductInfo.PIUniteId = lstPurchaseOrderDetails.UnitId;
                            tmpProductInfo.PIUniteName = lstPurchaseOrderDetails.UnitName;
                            tmpProductInfo.TotalProductPIPrice = lstPurchaseOrderDetails.LineTotal;
                            tmpProductInfo.OrderDetailsId = int.Parse(lstPurchaseOrderDetails.Id.ToString());
                            PIproductInfo.Add(tmpProductInfo);
                        }
                        Session["PIproductInfo"] = PIproductInfo;
                        LoadProductDetailsGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong." + ex);
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


            if (Session["PIproductInfo"] != null)
                PIproductInfo = (List<TempProductInfo>)Session["PIproductInfo"];
            else
                PIproductInfo = new List<TempProductInfo>();

            if (!IsPostBack)
            {
                lblisNewEntry.Text = "true";

                PIproductInfo = new List<TempProductInfo>();

                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }

                this.LoadProductCategory();
                this.LoadProductUnite();
                this.LoadProductDetailsGrid();
                this.LoadVendorName();

                

                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];

                    PIMaster objPIMaster = new PIMaster().GetPIMasterById(int.Parse(id));

                    
                    lblId.Text = objPIMaster.Id.ToString();
                    rtxtVendorAddress.Text = objPIMaster.VendorAddress;
                    rdropVandorName.SelectedValue = objPIMaster.VendorId.ToString();
                    lblSupId.Text = objPIMaster.VendorId.ToString();
                    this.LoadVandorbank(int.Parse(lblSupId.Text));
                    this.LoadOrderNumberInPI(int.Parse(rdropVandorName.SelectedValue));

                    rdropPONumber.SelectedValue = objPIMaster.OrderId.ToString();
                    rtxtPINumber.Text = objPIMaster.PINo;
                    rdtPIDate.SelectedDate = objPIMaster.PIDate;
                    rtxtfileName.Text = objPIMaster.DocName;

                    lblisNewEntry.Text = "false";
                    LoadChequeDetailsFromDataBase(int.Parse(id));
                }
            }
        }

        protected void rdropPONumber_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PurchaseOrderMaster objPurchaseOrderMaster = new PurchaseOrderMaster().GetPurchaseOrderMasterById(int.Parse(rdropPONumber.SelectedValue));
            lblPIId.Text = objPurchaseOrderMaster.Id.ToString();
            this.LoadChequeDetailsFromDataBase(int.Parse(lblPIId.Text));
        }

        protected void rdropProductUnit_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void rdropPOProduct_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdropPOProduct.SelectedIndex <= 0)
            {
                return;
            }
            else
            {
                Product objProduct = new Product().GetProductById(int.Parse(rdropPOProduct.SelectedValue));
                rtxtPOunitPrice.Text = objProduct.MRP.ToString();
            }
        }

        protected void rdropPOCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadProductNameByCategory(int.Parse(rdropPOCategory.SelectedValue), int.Parse(lblSupId.Text));
        }

        protected void RadGridAddProforma_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnSelect")
                {

                    showPI.Visible = true;

                    GridDataItem item = (GridDataItem)e.Item;

                    lblDetailsId.Text = item["colId"].Text;
                    rdropPOCategory.SelectedValue = item["colPICategory"].Text;
                    this.LoadProductNameByCategory(int.Parse(item["colPICategory"].Text), int.Parse(lblSupId.Text));
                    rdropPOProduct.SelectedValue = item["colPIProduct"].Text;
                    rtxtPOQuantity.Text = item["colPIQuantity"].Text;
                    rtxtPOunitPrice.Text = item["colPIUnitPrice"].Text;
                    rtxtTotalUnitPrice.Text = item["colTotalProductPIPrice"].Text;
                    rdropProductUnit.SelectedValue = item["colPIUniteId"].Text;
                    lblPIQuantity.Text = item["colPOQuantity"].Text;

                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to select Add product grid." + ex);
            }
        }


        protected void rbtnAddNew_OnClick(object sender, EventArgs e)
        {
            if (rdropPONumber.SelectedIndex <= 0)
            {
                Alert.Show("Please select a Order number");
                return;
            }
            if (rbtnAddNew.Text == "Add New Item")
            {
                showPI.Visible = true;
                rtxtPOQuantity.Text = "";
                rdropPOCategory.SelectedIndex = 0;
                rtxtPOunitPrice.Text = "";
                rtxtTotalUnitPrice.Text = "";
                rdropProductUnit.SelectedIndex = 0;
                rbtnAddNew.Text = "Cancel";
            }
            else
            {
                showPI.Visible = false;
                rbtnAddNew.Text = "Add New Item";
            }
        }

        protected void btnAddProductInPO_OnClick(object sender, EventArgs e)
        {
            try
            {
                Regex regexForQuentity = new Regex("^[0-9]*$");
                Regex regexForPriceUnite = new Regex(@"^[1-9]\d*(\.\d+)?$");


                #region validation

                if (rdropPOCategory.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Product Category");
                    rdropPOCategory.Focus();
                    return;
                }
                if (rdropPOProduct.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Product name");
                    rdropPOProduct.Focus();
                    return;
                }
                if (rtxtPOQuantity.Text == string.Empty || !regexForQuentity.IsMatch(rtxtPOQuantity.Text))
                {
                    Alert.Show("Please Enter the Quentity");
                    rtxtPOQuantity.Focus();
                    return;
                }
                if (rtxtPOunitPrice.Text == string.Empty || !regexForPriceUnite.IsMatch(rtxtPOunitPrice.Text))
                {
                    Alert.Show("Please Enter the Unite Price");
                    rtxtPOunitPrice.Focus();
                    return;
                }
                if (rdropProductUnit.SelectedIndex <= 0)
                {
                    Alert.Show("Please select Product unit");
                    rdropProductUnit.Focus();
                    return;
                }
                #endregion
                TempProductInfo objTempProductInfo =
                   PIproductInfo.Find(x => x.PIProduct == int.Parse(rdropPOProduct.SelectedValue));
                if (objTempProductInfo != null)
                {
                    if (objTempProductInfo.PIProduct == 0)
                        objTempProductInfo = new TempProductInfo();
                    else
                    {
                        PIproductInfo.Remove(objTempProductInfo);
                    }
                }
                else
                {
                    objTempProductInfo=new TempProductInfo();
                }
                decimal totaluPrice = decimal.Parse(rtxtPOQuantity.Text) * decimal.Parse(rtxtPOunitPrice.Text);
                lblpriceosProduct.Text = totaluPrice.ToString();

                objTempProductInfo.PICategory = int.Parse(rdropPOCategory.SelectedValue);
                objTempProductInfo.PIProduct = int.Parse(rdropPOProduct.SelectedValue);
                objTempProductInfo.PIQuantity = int.Parse(rtxtPOQuantity.Text);
                objTempProductInfo.PIUnitePrice = decimal.Parse(rtxtPOunitPrice.Text);
                objTempProductInfo.TotalProductPIPrice = totaluPrice;
                objTempProductInfo.PIProductName = rdropPOProduct.SelectedItem.Text;
                objTempProductInfo.PIUniteId = int.Parse(rdropProductUnit.SelectedValue);
                objTempProductInfo.PIUniteName = rdropProductUnit.SelectedItem.Text;

                if (PIproductInfo.Count == 0)
                    PIproductInfo = new List<TempProductInfo>();

                PIproductInfo.Add(objTempProductInfo);
                Session["PIproductInfo"] = PIproductInfo;

                this.LoadProductDetailsGrid();
                decimal totalsum;

                totalsum = PIproductInfo.Sum(x => x.TotalProductPIPrice);

                rtxtTotalPrice.Text = totalsum.ToString();

                lblisNewDetailEntry.Text = "true";
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to add P.I. Items" + ex);
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation
            if (rdropPONumber.SelectedIndex <= 0)
            {
                Alert.Show("please select a Order number.");
                rdropPONumber.Focus();
                return;
            }
            if (rtxtPINumber.Text == string.Empty)
            {
                Alert.Show("Please enter P.I. number.");
                rtxtPINumber.Focus();
                return;
            }
            if (rdtPIDate.SelectedDate == null)
            {
                Alert.Show("Please select a P.I date");
                rdtPIDate.Focus();
                return;
            }
            #endregion
            try
            {
                if (PIFileUp.HasFile)
                {
                    string PINo = rtxtPINumber.Text == string.Empty ? "Empty" : rtxtPINumber.Text;
                    string extension = Path.GetExtension(PIFileUp.FileName);
                    string fileName = PINo + "_Image" + extension;
                    lblImageName.Text = fileName;

                    DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/images/P.I._File") + PINo);
                    if (!directory.Exists)
                        directory.Create();

                    string UploadFilePath = Server.MapPath("~/images/P.I._File") + PINo + "/" +
                                            fileName;
                    FileInfo file = new FileInfo(UploadFilePath);

                    if (!file.Exists)
                        file.Delete();

                    PIFileUp.SaveAs(UploadFilePath);

                }
                PIMaster objPiMaster = new PIMaster();

                objPiMaster.PINo = rtxtPINumber.Text;
                objPiMaster.PIDate = DateTime.Parse(rdtPIDate.SelectedDate.ToString());
                objPiMaster.OrderId = int.Parse(rdropPONumber.SelectedValue);
                objPiMaster.VendorId = int.Parse(lblSupId.Text);
                objPiMaster.VendorName = rdropVandorName.Text;
                objPiMaster.VendorAddress = rtxtVendorAddress.Text;
                objPiMaster.DocName = rtxtfileName.Text;
                objPiMaster.DocLocation = lblImageName.Text;
                objPiMaster.Status = "P.I. Created";
                objPiMaster.CreatedBy = _user.Id;

                int success = 0;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objPiMaster.InsertPIMaster();
                }
                else
                {
                    objPiMaster.Id = int.Parse(lblId.Text);
                    success = objPiMaster.UpdatePIMaster();
                }
                if (success == 0)
                {
                    Alert.Show("PI master data was no save succesfully");
                    return;

                }
                else
                {
                    if (PIproductInfo.Count != 0)
                    {
                        foreach (TempProductInfo tempProductInfo in PIproductInfo)
                        {
                            PIDetails objPiDetails = new PIDetails();

                            lblDetailsId.Text = tempProductInfo.Id.ToString();
                            if (lblId.Text == string.Empty)
                            {
                                objPiDetails.MasterId = new PIMaster().GetMaxPIMasterId();
                            }
                            else
                            {
                                objPiDetails.MasterId = int.Parse(lblId.Text);
                            }
                            
                            objPiDetails.OrderDetailsId = tempProductInfo.OrderDetailsId;
                            objPiDetails.ProductId = tempProductInfo.PIProduct;
                            objPiDetails.ProductName = tempProductInfo.PIProductName;
                            objPiDetails.OrderQuantity = tempProductInfo.POQuantity;
                            objPiDetails.PIQuantity = tempProductInfo.PIQuantity;
                            objPiDetails.ProductUnit = tempProductInfo.PIUniteId;
                            objPiDetails.UnitName = tempProductInfo.PIUniteName;
                            objPiDetails.LineTotal = tempProductInfo.TotalProductPIPrice;
                            objPiDetails.PICategory = tempProductInfo.PICategory;
                            objPiDetails.PIUnitePrice = tempProductInfo.PIUnitePrice;

                            if (tempProductInfo.POQuantity > 0)
                            {
                                List<PIDetails> countPiDetailses =
                                    new PIDetails().GetAllPIDetailsOrderDetailsBymasterIdProductIdCateId(
                                        objPiDetails.ProductId, objPiDetails.ProductUnit, int.Parse(objPiDetails.MasterId.ToString()));

                                if (countPiDetailses.Count == 0)
                                {
                                    success = objPiDetails.InsertPIDetails();
                                }
                                else
                                {
                                    objPiDetails.Id = tempProductInfo.Id;
                                    success = objPiDetails.UpdatePIDetails();
                                }
                                if (success == 0)
                                {
                                    Alert.Show(
                                        "The Purchase Master saved successfully, but failed to save Purchase Details.");
                                    return;
                                }
                                else
                                {
                                    ClearAllInfo();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save Purchase order" + ex);
            }
        }

        private void ClearAllInfo()
        {
            rdropVandorName.SelectedIndex = 0;
            rtxtVendorAddress.Text = "";
            rtxtfileName.Text = "";

            rtxtPOunitPrice.Text = "";
            rtxtPINumber.Text = "";
            rtxtTotalPrice.Text = "";
            rtxtTotalUnitPrice.Text = "";
            rtxtVendorAddress.Text = "";
            rdtPIDate.SelectedDate = null;
            rdropPOCategory.SelectedIndex = 0;
            rdropPOProduct.SelectedIndex = -1;
            rdropProductUnit.SelectedIndex = 0;
            rdropPONumber.SelectedIndex = 0;
            rtxtPOQuantity.Text = "";
            RadGridAddProforma.DataSource = new string[] { };
            RadGridAddProforma.Rebind();
            PIproductInfo = new List<TempProductInfo>();
            Session["PIproductInfo"] = PIproductInfo;
            this.LoadOrderNumber(rdropVandorName.SelectedIndex);

        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearAllInfo();
        }

        protected void rdropVandorName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Supplier objSupplier = new Supplier().GetSupplierById(int.Parse(rdropVandorName.SelectedValue));
                rtxtVendorAddress.Text = objSupplier.CompanyAddress;
                lblSupId.Text = rdropVandorName.SelectedValue;
                this.LoadOrderNumber(int.Parse(rdropVandorName.SelectedValue));
                this.LoadVandorbank(int.Parse(rdropVandorName.SelectedValue));

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wron to load Purcher Order information." + ex);
            }
        }
    }
}