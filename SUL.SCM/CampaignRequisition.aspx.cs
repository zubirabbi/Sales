using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using System.Data;
using System.IO;
using hrms;
using Telerik.Web.UI;

namespace SUL.SCM
{

    public partial class CampaignRequisition : System.Web.UI.Page
    {
        private class TempCampaigns
        {
            public int Id { get; set; }
            public string CampaignCode { get; set; }
            public int DetailsId { get; set; }
            public string CampaignName { get; set; }
            public int Quantity { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal TotalDiscount { get; set; }

        }

        private class TempRequisitionDetailsC
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int CampaignId { get; set; }
            public string CampaignName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Price2 { get; set; }
            public int Unit { get; set; }
            public string UnitName { get; set; }
            public decimal Discount { get; set; }
            public decimal TotalProductPrice { get; set; }
            public decimal TotalProductPriceDiscount { get; set; }
        }


        private Users _user;
        private Company _company;
        private List<TempRequisitionDetailsC> _TempRequisitionDetailsCes;
        private List<TempCampaigns> _tempCampaigns;
        private string _department;
        private AppPermission _permissionUser;


        private bool IsValidSession()
        {
            if (Session["user"] == null)
            {
                return false;
            }

            _user = (Users)Session["user"];
            _company = (Company)Session["company"];
            _department = Session["Department"].ToString();

            return _user.Id != 0;

        }

        private bool IsValidPageForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Requisition Add");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            Session["PermissionUser"] = PermissionUser;

            return true;
        }


        private void LoadDealerInfo()
        {
            try
            {
                DataTable dtDealerInfo;
                if (showPanal.Visible == false)
                {
                    dtDealerInfo = new DealerInformation().GetAllDealerInformationView();
                }

                else
                {
                    if (rdropArea.SelectedIndex == 0 && rdropRegion.SelectedIndex == 0)
                    {
                        dtDealerInfo = new DealerInformation().GetAllDealerInformationView();
                    }
                    else if (rdropArea.SelectedIndex == 0)
                    {
                        dtDealerInfo =
                            new DealerInformation().GetAllDealerInformationViewByRegion(
                                int.Parse(rdropRegion.SelectedValue));
                    }
                    else
                    {
                        dtDealerInfo =
                            new DealerInformation().GetAllDealerInformationViewByArea(int.Parse(rdropArea.SelectedValue));
                    }

                }

                rdropPDealer.DataTextField = "DealerInfo";
                rdropPDealer.DataValueField = "Id";
                rdropPDealer.DataSource = dtDealerInfo;
                rdropPDealer.DataBind();

                rdropPDealer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
            }
        }
        private void LoadRegion()
        {
            try
            {
                DataTable lstRegions = new Region().GetAllRegionFromView();

                rdropRegion.DataTextField = "RECode";
                rdropRegion.DataValueField = "Id";
                rdropRegion.DataSource = lstRegions;
                rdropRegion.DataBind();
                rdropRegion.SelectedIndex = lstRegions.Rows.Count == 2 ? 1 : 0;


            }

            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load region." + ex);
            }
        }
        private void LoadArea(int region)
        {
            try
            {
                DataTable lstRegions = new Area().GetAllViewAreaByRegionId(region);

                rdropArea.DataTextField = "AreaInfo";
                rdropArea.DataValueField = "Id";
                rdropArea.DataSource = lstRegions;
                rdropArea.DataBind();
                rdropArea.SelectedIndex = lstRegions.Rows.Count == 2 ? 1 : 0;


            }

            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load region." + ex);
            }
        }

        private void LoadCampaignInfo()
        {
            try
            {
                DateTime date = (rdtRequisitionDate.SelectedDate == null)
                    ? DateTime.Today
                    : DateTime.Parse(rdtRequisitionDate.SelectedDate.ToString());

                List<CampaignMaster> campaignList = new CampaignMaster().GetAllCampaignByType("Product", true, date);

                campaignList.Insert(0, new CampaignMaster());

                rdropCode.DataValueField = "Id";
                rdropCode.DataTextField = "CampaignCode";
                rdropCode.DataSource = campaignList;
                rdropCode.DataBind();

                rdropCode.SelectedIndex = 0;

                Session["campaignList"] = campaignList;
            }
            catch (Exception ex)
            {
                Alert.Show("Error occured during load campaign Info" + ex);
            }
        }

        private void LoadCourierInfo()
        {
            try
            {
                //Get object list of courier information
                List<CourierInformation> lstCourierInformation = new CourierInformation().GetAllCourierInformation();
                DataTable dtTranstopt = new CourierInformation().GetTransportViewList();
                //insert bland object in the list

                rdropCourier.DataTextField = "Val";
                rdropCourier.DataValueField = "Val";
                rdropCourier.DataSource = dtTranstopt;
                rdropCourier.DataBind();

                if (lstCourierInformation.Count == 2)
                    rdropCourier.SelectedIndex = 1;
                else
                    rdropCourier.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load courier Info." + ex);
            }
        }

        private void LoadSetupChennalDesignation()
        {
            try
            {
                DealerInformation objDealerInformation = new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));

                DataTable dtRmployeeInfo =
                    new EmployeeInformation().GetEmpListFromViewListbyempId(objDealerInformation.CS, _company.Id);

                rdropCS.DataTextField = "empAllInfo";
                rdropCS.DataValueField = "Id";
                rdropCS.DataSource = dtRmployeeInfo;
                rdropCS.DataBind();
                rdropCS.SelectedIndex = dtRmployeeInfo.Rows.Count == 1 ? 1 : 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load data" + ex);
            }
        }

        private void LoadJrCS()
        {
            try
            {
                DealerInformation objDealerInformation = new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));

                DataTable dtJrcs = new EmployeeInformation().GetEmpListFromViewListbyempId(objDealerInformation.JrCS,
                    _company.Id);

                rdropJrCS.DataTextField = "empAllInfo";
                rdropJrCS.DataValueField = "Id";
                rdropJrCS.DataSource = dtJrcs;
                rdropJrCS.DataBind();
                if (dtJrcs.Rows.Count == 1)
                    rdropJrCS.SelectedIndex = 1;
                else
                    rdropJrCS.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to Load date." + ex);
            }
        }

        private void LoadCompanyBank()
        {
            try
            {
                DataTable lstbaBankInformations =
                    new BankInformation().GetAllViewbankInfoByCompany();

                rdropBankName.DataTextField = "BankInfo";
                rdropBankName.DataValueField = "Id";
                rdropBankName.DataSource = lstbaBankInformations;
                rdropBankName.DataBind();


            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Company bank." + ex);
            }
        }

        private void LoadPaymentType()
        {
            try
            {
                List<ListTable> lstTable = new ListTable().GetAllListTableByType("PaymentType");

                lstTable.Insert(0, new ListTable());

                rdropPaymentType.DataTextField = "ListValue";
                rdropPaymentType.DataValueField = "ListId";

                rdropPaymentType.DataSource = lstTable;
                rdropPaymentType.DataBind();
                if (lstTable.Count > 1)
                    rdropPaymentmode.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Payment Type DropdownList" + ex);
            }
        }

        private void LoadPaymentMode()
        {
            try
            {
                List<ListTable> lstTable = new ListTable().GetAllListTableByType("PaymentMode");

                lstTable.Insert(0, new ListTable());

                rdropPaymentmode.DataTextField = "ListValue";
                rdropPaymentmode.DataValueField = "ListId";

                rdropPaymentmode.DataSource = lstTable;
                rdropPaymentmode.DataBind();
                rdropPaymentmode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Payment Mode DropdownList" + ex);
            }
        }

        private void LoadRequisitionDetails()
        {
            try
            {
                if (_TempRequisitionDetailsCes.Count == 0)
                {
                    RadGridAddRequisitionDetails.DataSource = new string[] { };
                    return;
                }

                RadGridAddRequisitionDetails.DataSource = _TempRequisitionDetailsCes;
                RadGridAddRequisitionDetails.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load requisition details grid." + ex);
            }
        }

        private void LoadRequisitionDataFromDatabase(int id)
        {
            try
            {
                if (bool.Parse(lblnewEntry.Text) == false)
                {
                    List<RequisitionDetails> lstRequisitionDetailses =
                        new RequisitionDetails().GetAllRequisitionDetailsBymasterId(id);
                    if (lstRequisitionDetailses.Count > 0)
                    {
                        _TempRequisitionDetailsCes = new List<TempRequisitionDetailsC>();
                        foreach (RequisitionDetails lstReqDetails in lstRequisitionDetailses)
                        {
                            TempRequisitionDetailsC tmpRequisitionDetails = new TempRequisitionDetailsC();

                            tmpRequisitionDetails.Id = int.Parse(lstReqDetails.Id.ToString());
                            lblDetails.Text = lstReqDetails.Id.ToString();
                            //tmpRequisitionDetails.CategoryId = lstReqDetails.CategoryId;

                            //ProductCategory objProductCategory = new ProductCategory().GetProductCategoryById(lstReqDetails.CategoryId);
                            //tmpRequisitionDetails.ProductCategory = objProductCategory.CategoryCode;

                            Product objProduct = new Product().GetProductById(lstReqDetails.ProductId);
                            tmpRequisitionDetails.ProductName = objProduct.ProductCode + ";" + objProduct.ProductName;

                            tmpRequisitionDetails.ProductId = lstReqDetails.ProductId;
                            tmpRequisitionDetails.Quantity = lstReqDetails.Quantity;
                            tmpRequisitionDetails.Price = lstReqDetails.Price;
                            tmpRequisitionDetails.Price2 = lstReqDetails.Price2;
                            lblDP2.Text = lstReqDetails.Price2.ToString();
                            tmpRequisitionDetails.Unit = lstReqDetails.Unit;
                            //ProductUnit objProductUnit = new ProductUnit().GetProductUnitById(int.Parse(rdropProductUnit.SelectedValue));
                            //tmpRequisitionDetails.UnitName = objProductUnit.UnitCode;
                            //tmpRequisitionDetails.Color = lstReqDetails.Color;
                            tmpRequisitionDetails.Discount = lstReqDetails.Discount;
                            tmpRequisitionDetails.TotalProductPrice = lstReqDetails.Price * lstReqDetails.Quantity;
                            tmpRequisitionDetails.TotalProductPriceDiscount = lstReqDetails.Price * lstReqDetails.Quantity - lstReqDetails.Discount;
                            _TempRequisitionDetailsCes.Add(tmpRequisitionDetails);
                        }

                        Session["TempRequisitionDetailsCes"] = _TempRequisitionDetailsCes;

                        LoadRequisitionDetails();
                        decimal totalsum;

                        totalsum = _TempRequisitionDetailsCes.Sum(x => x.TotalProductPriceDiscount);
                        rtxtItemTotal.Text = totalsum.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Requisition details data." + ex);
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

            else
                _department = "All";


            if (Session["TempRequisitionDetailsCes"] != null)
                _TempRequisitionDetailsCes = (List<TempRequisitionDetailsC>)Session["TempRequisitionDetailsCes"];
            else
                _TempRequisitionDetailsCes = new List<TempRequisitionDetailsC>();

            if (Session["tempCampaigns"] != null)
                _tempCampaigns = (List<TempCampaigns>)Session["tempCampaigns"];
            else
                _tempCampaigns = new List<TempCampaigns>();

            if (!IsPostBack)
            {

                if (_user.EmployeeId != 0)
                {
                    Department objDepartment = new Department().GetEmployeeDepartment(_user.EmployeeId);
                    _department = objDepartment.DepartmentName;
                }

                if (IsValidPageForUser())
                {
                    _permissionUser = (AppPermission)Session["PermissionUser"];
                    if (!_permissionUser.IsView)
                    {
                        Alert.Show("Sorry, You Don't Have permission to access this page.");
                        Response.Redirect("ErrorPage.aspx", false);
                    }
                    if (_permissionUser.IsView && !_permissionUser.IsInsert && !_permissionUser.IsUpdate)
                    {
                        btnSave.Visible = false;
                        btnPrint.Visible = true;
                        btnClear.Visible = false;
                        btnAddRequisitionDetails.Visible = false;
                    }
                }


                this.ClearAllInfo();
                this.LoadDealerInfo();
                this.LoadCourierInfo();
                this.LoadPaymentType();
                this.LoadPaymentMode();
                this.LoadSetupChennalDesignation();
                this.LoadJrCS();
                this.LoadRegion();
                this.LoadCampaignInfo();

                List<Product> products = new Product().GetAllProductList();
                Session["products"] = products;
                Session["tempCampaigns"] = null;
                Session["TempRequisitionDetailsCes"] = null;

                if (Request.QueryString["Id"] != null)
                {
                    lblnewEntry.Text = "false";
                    string id = Request.QueryString["Id"];
                    GetRequisitionInformation(int.Parse(id));

                }

                if (bool.Parse(lblnewEntry.Text))
                {
                    if (!_permissionUser.IsInsert)
                    {
                        Alert.Show("Sorry, You Don't Have permission to access this page.");
                        Response.Redirect("ErrorPage.aspx", false);
                    }
                }
                else
                {
                    if (!_permissionUser.IsUpdate)
                    {
                        btnSave.Visible = false;
                        btnPrint.Visible = true;
                        btnClear.Visible = false;
                        btnAddRequisitionDetails.Visible = false;
                    }
                    if (!_permissionUser.IsDelete)
                    {
                        // hide delete button
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        private void GetRequisitionInformation(int id)
        {
            this.LoadCompanyBank();
            this.LoadPaymentType();
            this.LoadPaymentMode();

            #region load master data

            RequisitionMaster objRequisitionMaster = new RequisitionMaster().GetRequisitionMasterById(id);

            lblId.Text = objRequisitionMaster.Id.ToString();
            rdropPDealer.SelectedValue = objRequisitionMaster.DealerId.ToString();
            this.LoadSetupChennalDesignation();
            this.LoadJrCS();
            rdropCourier.SelectedValue = objRequisitionMaster.Courier;
            rdropCS.SelectedValue = objRequisitionMaster.CSId.ToString();
            rtxtAddress.Text = objRequisitionMaster.Address;
            rtxtRequisitionNo.Text = objRequisitionMaster.RequisitionCode;
            rdtRequisitionDate.SelectedDate = objRequisitionMaster.RequisitionDate;
            lblStatus.Text = objRequisitionMaster.Status;
            lblinvoiceCreate.Text = objRequisitionMaster.IsInvoiceCreated.ToString();
            rdropJrCS.SelectedValue = objRequisitionMaster.JrCSId.ToString();
            rtxtRemarks.Value = objRequisitionMaster.Remarks;
            rtxtItemTotal.Text = objRequisitionMaster.ItemTotal.ToString();
            rtxtReqDiscount.Text = objRequisitionMaster.Discount.ToString();
            rtxtReqTotal.Text = (objRequisitionMaster.ItemTotal - objRequisitionMaster.Discount).ToString();

            #endregion

            if (lblStatus.Text.ToLower() == "created" && _department.ToLower().Contains("account"))
            {
                new RequisitionMaster().ChangeRequisitionStatus(int.Parse(lblId.Text), _user.Id, "Seen");
            }

            #region load payment data

            Payment objPayment = new Payment().GetPaymentByRequisitionId(id);

            lblnewPaymentEntry.Text = "false";

            lblPaymentId.Text = objPayment.Id.ToString();
            rdropPaymentType.SelectedValue = objPayment.PaymentType.ToString();
            rdropPaymentmode.SelectedValue = objPayment.PaymentMode.ToString();
            rdropBankName.SelectedValue = objPayment.BankNameId.ToString();
            rtxtReferenceNo.Text = objPayment.ReferenceNo;
            rtxtAmount.Text = objPayment.Amount.ToString();
            rtxtBankCharge.Text = objPayment.BankCharge.ToString();
            lblpaymentStatus.Text = objPayment.Status;

            if (rdropBankName.SelectedIndex <= 0)
            {
                rtxtBranch.Text = "";
            }
            else
            {
                rtxtBranch.Text = objPayment.Branch;
            }
            if (objPayment.IsVarified == true)
            {
                rdropPaymentType.Enabled = false;
                rdropPaymentmode.Enabled = false;
                rdropBankName.Enabled = false;
                rtxtReferenceNo.Enabled = false;
                rtxtAmount.Enabled = false;
                rtxtBankCharge.Enabled = false;
                rdropBankName.Enabled = false;
                rtxtBranch.Enabled = false;
            }
            if (rdropPaymentType.SelectedItem.Text == "Deposit")
            {
                showDeposit.Visible = true;
                rtxtChequeBank.Text = objPayment.ChequeBank;
                rtxtChequeBranch.Text = objPayment.ChequeBranch;
                rdtCheckDate.SelectedDate = objPayment.ChequeDate;
            }
            else
            {
                showDeposit.Visible = false;
                rtxtChequeBank.Text = "";
                rtxtChequeBranch.Text = "";
                rdtCheckDate.SelectedDate = null;
            }
            #endregion

            //isNewEntry = false;
            lblnewEntry.Text = "false";
            this.LoadRequisitionDataFromDatabase(id);
            btnPrint.Visible = true;

            #region set buttons

            if (objRequisitionMaster.Status.ToLower() == "canceled" || objRequisitionMaster.Status.ToLower() == "rejected")
            {
                btnSave.Visible = false;
                btnPrint.Visible = false;
                btnClear.Visible = false;
                btnAddRequisitionDetails.Visible = false;
            }
            else if (objRequisitionMaster.Status.ToLower() == "approved" || objRequisitionMaster.Status.ToLower() == "delivered" ||
                     objRequisitionMaster.Status.ToLower() == "invoiced")
            {
                btnSave.Visible = false;
                btnPrint.Visible = true;
                btnClear.Visible = false;
                btnAddRequisitionDetails.Visible = false;
            }
            else if (objRequisitionMaster.Status.ToLower() == "unapproved" || objRequisitionMaster.Status.ToLower() == "seen")
            {
                btnSave.Visible = true;
                btnPrint.Visible = true;
                btnClear.Visible = true;
                btnAddRequisitionDetails.Visible = true;
            }
            if (_department != "All")
            {
                if (_department.ToLower().Contains("sales"))
                {
                    if (objRequisitionMaster.Status.ToLower() == "created" ||
                        objRequisitionMaster.Status.ToLower() == "unapproved" || objRequisitionMaster.Status.ToLower() == "seen")
                    {
                        btnSave.Visible = true;
                        btnPrint.Visible = true;
                        btnClear.Visible = true;
                        btnAddRequisitionDetails.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = false;
                        btnPrint.Visible = false;
                        btnClear.Visible = false;
                        btnAddRequisitionDetails.Visible = false;
                    }
                }
            }

            #endregion
        }

        protected void rdropCS_OnDataBound(object sender, EventArgs e)
        {
            rdropCS.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropProduct_OnDataBound(object sender, EventArgs e)
        {
            //rdropProduct.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadProductInfo(int.Parse(rdropCategory.SelectedValue));
        }

        protected void RadGridAddRequisitionDetails_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {




            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to when selecting the details." + ex);
            }
        }

        protected void btnAddRequisitionDetails_OnClick(object sender, EventArgs e)
        {
            try
            {
                Regex regexForQuentity = new Regex("^[0-9]*$");
                Regex regexForPriceUnite = new Regex(@"^[0-9]\d*(\.\d+)?$");

                #region validation
                if (rdropCode.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a campaign code first.");
                    rdropCode.Focus();
                    return;
                }
                if (rdropName.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a campaign first.");
                    rdropName.Focus();
                    return;
                }
                if (rtxtQuantity.Text == string.Empty || !regexForQuentity.IsMatch(rtxtQuantity.Text))
                {
                    Alert.Show("Please enter a valid quantity");
                    rtxtQuantity.Focus();
                    return;
                }
                #endregion

                int detailsId = int.Parse(rdropName.SelectedValue);
                List<CampaignDetails> objDetailsList = new List<CampaignDetails>();
                List<CampaignMaster> campaignList = (List<CampaignMaster>)Session["campaignList"];

                if (Session["campaignDetailsList"] != null)
                    objDetailsList = (List<CampaignDetails>)Session["campaignDetailsList"];
                CampaignDetails objDetails = objDetailsList.Find(x => x.Id == detailsId);

                //remove offer products
                if (_TempRequisitionDetailsCes.Count > 0)
                {
                    List<TempRequisitionDetailsC> objExistingProducts =
                        _TempRequisitionDetailsCes.FindAll(
                            x => x.CampaignId == detailsId);

                    if (objExistingProducts.Count > 0)
                    {
                        foreach (TempRequisitionDetailsC products in objExistingProducts)
                        {
                            _TempRequisitionDetailsCes.Remove(products);
                        }
                    }
                }
                else
                {
                    _TempRequisitionDetailsCes = new List<TempRequisitionDetailsC>();
                }

                #region region add campaign products

                List<Product> productList = (List<Product>)Session["products"];

                decimal campaignTotal = 0;
                List<CampaignProducts> objProducts = new CampaignProducts().GetProductsByDetailsId(detailsId);
                foreach (CampaignProducts products in objProducts)
                {
                    TempRequisitionDetailsC objTempRequisitionDetailsCes = new TempRequisitionDetailsC();

                    Product prod = productList.Find(x => x.Id == products.ProductId);
                    objTempRequisitionDetailsCes.CampaignId = detailsId;
                    objTempRequisitionDetailsCes.CampaignName = rdropName.SelectedItem.Text;
                    objTempRequisitionDetailsCes.ProductId = products.ProductId;
                    objTempRequisitionDetailsCes.Price = products.Price;
                    objTempRequisitionDetailsCes.Price2 = products.Price;
                    objTempRequisitionDetailsCes.Quantity = int.Parse(products.StartQuantity.ToString()) * int.Parse(rtxtQuantity.Text);
                    objTempRequisitionDetailsCes.Discount = 0;
                    objTempRequisitionDetailsCes.ProductName = prod.ProductName;
                    objTempRequisitionDetailsCes.UnitName = prod.Unit;
                    objTempRequisitionDetailsCes.Unit = prod.BaseUnit;
                    objTempRequisitionDetailsCes.TotalProductPrice = objTempRequisitionDetailsCes.Price * objTempRequisitionDetailsCes.Quantity;
                    objTempRequisitionDetailsCes.TotalProductPriceDiscount = objTempRequisitionDetailsCes.TotalProductPrice;
                    if (_TempRequisitionDetailsCes.Count == 0)
                        _TempRequisitionDetailsCes = new List<TempRequisitionDetailsC>();

                    _TempRequisitionDetailsCes.Add(objTempRequisitionDetailsCes);
                    campaignTotal += objTempRequisitionDetailsCes.TotalProductPrice;
                }


                #endregion

                #region region add free products

                List<CampaignOfferProducts> objOfferProducts = new CampaignOfferProducts().GetProductsByDetailsId(detailsId);
                foreach (CampaignOfferProducts products in objOfferProducts)
                {
                    TempRequisitionDetailsC objTempRequisitionDetailsCes = new TempRequisitionDetailsC();
                    Product prod = productList.Find(x => x.Id == products.ProductId);

                    objTempRequisitionDetailsCes.CampaignId = detailsId;
                    objTempRequisitionDetailsCes.CampaignName = rdropName.SelectedItem.Text;
                    objTempRequisitionDetailsCes.ProductId = products.ProductId;
                    objTempRequisitionDetailsCes.Price = 0;
                    objTempRequisitionDetailsCes.Price2 = 0;
                    objTempRequisitionDetailsCes.Quantity = products.Quantity * int.Parse(rtxtQuantity.Text);
                    objTempRequisitionDetailsCes.Discount = 0;
                    objTempRequisitionDetailsCes.ProductName = prod.ProductName;
                    objTempRequisitionDetailsCes.UnitName = prod.Unit;
                    objTempRequisitionDetailsCes.Unit = prod.BaseUnit;
                    objTempRequisitionDetailsCes.TotalProductPrice = objTempRequisitionDetailsCes.Price * objTempRequisitionDetailsCes.Quantity;
                    objTempRequisitionDetailsCes.TotalProductPriceDiscount = objTempRequisitionDetailsCes.TotalProductPrice;
                    if (_TempRequisitionDetailsCes.Count == 0)
                        _TempRequisitionDetailsCes = new List<TempRequisitionDetailsC>();

                    _TempRequisitionDetailsCes.Add(objTempRequisitionDetailsCes);
                }

                #endregion add free products

                Session["TempRequisitionDetailsCes"] = _TempRequisitionDetailsCes;
                this.LoadRequisitionDetails();



                CampaignMaster cMaster = campaignList.Find(x => x.Id == objDetails.CampaignId);
                decimal campaignDiscount = 0;
                if (cMaster != null)
                {
                    if (!cMaster.IsAdjustedAfterEnd)
                        if (objDetails.OfferAmount > 0)
                            campaignDiscount = objDetails.OfferAmount * int.Parse(rtxtQuantity.Text);
                        else
                        {
                            decimal pcnt = objDetails.DiscountPcnt;
                            campaignDiscount = (campaignTotal * pcnt) / 100;
                        }
                }

                TempCampaigns objCampaigns = new TempCampaigns()
                {
                    DetailsId = detailsId,
                    CampaignCode = rdropCode.SelectedItem.Text,
                    CampaignName = rdropName.SelectedItem.Text,
                    Quantity = int.Parse(rtxtQuantity.Text),
                    TotalAmount = campaignTotal,
                    TotalDiscount = campaignDiscount
                };


                if (Session["tempCampaigns"] == null)
                    _tempCampaigns = new List<TempCampaigns>();
                else
                {
                    _tempCampaigns = (List<TempCampaigns>)Session["tempCampaigns"];

                    if (_tempCampaigns.Count > 0)
                    {
                        TempCampaigns obj = _tempCampaigns.Find(x => x.DetailsId == detailsId);
                        _tempCampaigns.Remove(obj);
                    }
                }

                _tempCampaigns.Add(objCampaigns);
                Session["tempCampaigns"] = _tempCampaigns;

                decimal totalAmount = _tempCampaigns.Sum(x => x.TotalAmount);
                decimal totalDiscount = _tempCampaigns.Sum(x => x.TotalDiscount);

                rtxtItemTotal.Text = totalAmount.ToString("N");
                rtxtReqDiscount.Text = totalDiscount.ToString("N");
                rtxtReqTotal.Text = (totalAmount - totalDiscount).ToString("N");

                rdropName.SelectedIndex = -1;
                rtxtQuantity.Text = "";
                if (Request.QueryString["Id"] != null)
                {
                    string id = Request.QueryString["Id"];

                    Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx?id=" + id +
                                   "#reqDetaislInfo')</script>");
                }
                else
                    Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx#reqDetaislInfo')</script>");

                rdropName.Focus();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to add Requisition data." + ex);
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                #region validation

                if (rdropPDealer.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Dealer");
                    rdropPDealer.Focus();
                    return;
                }
                if (rdropCS.SelectedIndex <= 0 && rdropJrCS.SelectedIndex <= 0)
                {
                    Alert.Show("No CS/JSC was assigned for the selected dealer.");
                    rdropCS.Focus();
                    return;
                }
                if (rdropCourier.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Courier.");
                    rdropCourier.Focus();
                    return;
                }
                if (rdtRequisitionDate.SelectedDate == null)
                {
                    Alert.Show("Please select a Date");
                    rdtRequisitionDate.Focus();
                    return;
                }

                int isRequisitionExist = new RequisitionMaster().CheckCodeExistence(rtxtRequisitionNo.Text,
                    (bool.Parse(lblnewEntry.Text) ? 0 : int.Parse(lblId.Text)), bool.Parse(lblnewEntry.Text));

                if (isRequisitionExist > 0)
                {
                    Alert.Show("Requisition code already exist.");
                    return;
                }
                if (rdropPaymentType.SelectedIndex > 0)
                {
                    if (rdropPaymentmode.SelectedIndex == 0)
                    {
                        Alert.Show("Payment mode can not be empty.");
                        rdropPaymentmode.Focus();
                        return;
                    }
                    if (rdropPaymentmode.SelectedItem.Text.ToLower() == "bank" && rtxtReferenceNo.Text == string.Empty)
                    {
                        Alert.Show("Reference No can not be empty.");
                        rtxtRequisitionNo.Focus();
                        return;
                    }
                    if (rdropPaymentmode.SelectedItem.Text.ToLower() == "bank" && rdropBankName.SelectedIndex == 0)
                    {
                        Alert.Show("Please select a bank name.");
                        rdropBankName.Focus();
                        return;
                    }
                    if (rtxtAmount.Text == string.Empty)
                    {
                        Alert.Show("Amount can not be empty.");
                        rtxtAmount.Focus();
                        return;
                    }
                    try
                    {
                        decimal amount = decimal.Parse(rtxtAmount.Text);
                    }
                    catch (Exception ex)
                    {
                        Alert.Show("Not a valid amount.");
                        rtxtAmount.Focus();
                        return;
                    }
                    try
                    {
                        if (rtxtBankCharge.Text != string.Empty)
                        {
                            decimal charge = decimal.Parse(rtxtBankCharge.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        Alert.Show("Not a valid bank charge.");
                        rtxtBankCharge.Focus();
                        return;
                    }
                }
                if (_TempRequisitionDetailsCes.Count == 0)
                {
                    Alert.Show("No product details is found. Please enter product details to save data.");
                    return;
                }
                DealerInformation objdInformation = new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));
                if (objdInformation.Id == 0)
                {
                    Alert.Show("Please select a valid dealer.");
                    rdropPDealer.Focus();
                    return;
                }

                #endregion

                RequisitionMaster objRequisitionMaster = new RequisitionMaster();
                objRequisitionMaster.DealerId = int.Parse(rdropPDealer.SelectedValue);
                objRequisitionMaster.Address = rtxtAddress.Text;
                objRequisitionMaster.Courier = rdropCourier.SelectedValue;
                objRequisitionMaster.RequisitionDate = DateTime.Parse(rdtRequisitionDate.SelectedDate.ToString());
                objRequisitionMaster.CSId = rdropCS.SelectedIndex <= 0 ? 0 : int.Parse(rdropCS.SelectedValue);
                objRequisitionMaster.Status = lblStatus.Text;
                objRequisitionMaster.IsInvoiceCreated = bool.Parse(lblinvoiceCreate.Text);
                objRequisitionMaster.JrCSId = rdropJrCS.SelectedIndex <= 0 ? 0 : int.Parse(rdropJrCS.SelectedValue);
                objRequisitionMaster.UserId = _user.Id;
                objRequisitionMaster.CencelDate = PublicVariables.minDate;
                objRequisitionMaster.CencelBy = 0;
                objRequisitionMaster.CencelNote = "";
                objRequisitionMaster.LastBalance = objdInformation.Balance;
                objRequisitionMaster.Remarks = rtxtRemarks.Value;
                objRequisitionMaster.CampaignId = int.Parse(rdropCode.SelectedValue);

                int success;
                if (bool.Parse(lblnewEntry.Text))
                {
                    string rqCode = new RequisitionMaster().GetlastCampaignRequisitionCode();
                    rtxtRequisitionNo.Text = rqCode;
                    objRequisitionMaster.RequisitionCode = rtxtRequisitionNo.Text;
                    success = objRequisitionMaster.InsertRequisitionMaster();
                    lblnewEntry.Text = "false";
                    lblId.Text = new RequisitionMaster().GetMaxRequisitionMasterId().ToString();
                }
                else
                {
                    objRequisitionMaster.Id = int.Parse(lblId.Text);
                    objRequisitionMaster.RequisitionCode = rtxtRequisitionNo.Text;

                    success = objRequisitionMaster.UpdateRequisitionMaster();
                }
                if (success == 0)
                {
                    Alert.Show("Requsition Master data is not save successfully");
                    return;
                }

                //Add
                foreach (var campaigns in _tempCampaigns)
                {
                    RequisitionCampaigns campaign = new RequisitionCampaigns()
                    {
                        CampaignDetailsId = campaigns.DetailsId,
                        Quantity = campaigns.Quantity,
                        RequisitionId = int.Parse(lblId.Text)
                    };

                    campaign.InsertRequisitionCampaigns();
                }

                if (_TempRequisitionDetailsCes.Count != 0)
                {
                    decimal totalPrice = 0;
                    decimal totalPrice2 = 0;
                    decimal totalDiscount1 = 0;
                    decimal totalDiscount2 = 0;
                    int masterid = 0;
                    int successDetails = 0;

                    List<RequisitionDetails> objDetails = new RequisitionDetails().GetAllRequisitionDetailsBymasterId(masterid);

                    foreach (TempRequisitionDetailsC tmpDetails in _TempRequisitionDetailsCes)
                    {
                        RequisitionDetails objRequisitionDetails = new RequisitionDetails();

                        masterid = int.Parse(lblId.Text);
                        this.LoadRequisitionDataFromDatabase(int.Parse(lblId.Text));

                        lblDetails.Text = tmpDetails.Id.ToString();
                        objRequisitionDetails.MasterId = masterid;
                        objRequisitionDetails.CategoryId = 0;
                        objRequisitionDetails.ProductId = tmpDetails.ProductId;
                        objRequisitionDetails.Unit = tmpDetails.Unit;
                        objRequisitionDetails.Color = 0;
                        objRequisitionDetails.Quantity = tmpDetails.Quantity;
                        objRequisitionDetails.Price = tmpDetails.Price;
                        objRequisitionDetails.Price2 = tmpDetails.Price2;
                        objRequisitionDetails.Discount = ((tmpDetails.Price * tmpDetails.Quantity) * tmpDetails.Discount) / 100;
                        objRequisitionDetails.Discount2 = ((tmpDetails.Price2 * tmpDetails.Quantity) * tmpDetails.Discount) / 100;
                        objRequisitionDetails.CampaignDetailsId = tmpDetails.CampaignId;

                        RequisitionDetails lseRequisitionDetailses =
                            objDetails.Find(
                                x =>
                                    x.ProductId == objRequisitionDetails.ProductId &&
                                    x.Unit == objRequisitionDetails.Unit &&
                                    x.Color == objRequisitionDetails.Color && x.Price == objRequisitionDetails.Price);

                        totalPrice += (tmpDetails.Price * tmpDetails.Quantity);
                        totalPrice2 += (tmpDetails.Price2 * tmpDetails.Quantity);
                        totalDiscount1 += objRequisitionDetails.Discount;
                        totalDiscount2 += objRequisitionDetails.Discount2;

                        if (lseRequisitionDetailses == null || lseRequisitionDetailses.Id == 0)
                        {
                            successDetails = objRequisitionDetails.InsertRequisitionDetails();
                        }
                        else
                        {

                            objRequisitionDetails.Id = lseRequisitionDetailses.Id;
                            successDetails = objRequisitionDetails.UpdateRequisitionDetails();
                        }

                        if (successDetails == 0)
                            Alert.Show("Entry failed for item " + tmpDetails.ProductName);
                    }

                    this.LoadRequisitionDataFromDatabase(int.Parse(lblId.Text));
                    objRequisitionMaster.Id = masterid;
                    objRequisitionMaster.ItemTotal = totalPrice;
                    objRequisitionMaster.ItemTotal2 = totalPrice2;
                    objRequisitionMaster.Discount = (rtxtReqDiscount.Text == string.Empty) ? 0
                        : decimal.Parse(rtxtReqDiscount.Text);


                    objRequisitionMaster.UpdateRequisitionMasterByPrice();
                }

                if (rtxtAmount.Text != string.Empty && rtxtAmount.Text != "0")
                {
                    Payment objPayment = new Payment();

                    objPayment.Address = "";
                    objPayment.Amount = decimal.Parse(rtxtAmount.Text);
                    objPayment.BankNameId = rdropBankName.SelectedIndex == 0
                        ? 0
                        : int.Parse(rdropBankName.SelectedValue);
                    objPayment.ReferenceNo = rtxtReferenceNo.Text;
                    objPayment.PaymentType = int.Parse(rdropPaymentType.SelectedValue);
                    objPayment.PaymentMode = int.Parse(rdropPaymentmode.SelectedValue);
                    objPayment.DealerId = int.Parse(rdropPDealer.SelectedValue);
                    objPayment.MoneyReceiptNo = objPayment.GetlastMoneyReceiptCode();
                    objPayment.RequisitionId = int.Parse(lblId.Text);
                    objPayment.PaymentDate = DateTime.Parse(rdtRequisitionDate.SelectedDate.ToString());
                    objPayment.IsVarified = false;
                    objPayment.BankCharge = rtxtBankCharge.Text == string.Empty ? 0 : decimal.Parse(rtxtBankCharge.Text);
                    objPayment.Branch = rtxtBranch.Text;
                    objPayment.LastBalance = objdInformation.Balance;
                    objPayment.Status = lblpaymentStatus.Text;
                    objPayment.UpdateBy = _user.Id;
                    objPayment.CreatedBy = _user.Id;
                    objPayment.ChequeBank = rtxtChequeBank.Text == string.Empty ? "" : rtxtChequeBank.Text;
                    objPayment.ChequeBranch = rtxtChequeBranch.Text == string.Empty ? "" : rtxtChequeBranch.Text;
                    objPayment.ChequeDate = rdtCheckDate.SelectedDate == null
                        ? PublicVariables.minDate
                        : DateTime.Parse(rdtCheckDate.SelectedDate.ToString());
                    int successPayment = 0;
                    if (lblPaymentId.Text == string.Empty || lblPaymentId.Text == "0")
                    {
                        if (lblId.Text == string.Empty)
                            successPayment = objPayment.InsertPayment();
                        else
                        {
                            Payment objPaymentUp = new Payment().GetPaymentByRequisitionId(int.Parse(lblId.Text));
                            if (objPaymentUp.Id == 0)
                                successPayment = objPayment.InsertPayment();
                            else
                            {
                                objPayment.Id = objPaymentUp.Id;
                                successPayment = objPayment.UpdatePayment();
                            }
                        }
                    }
                    else
                    {
                        objPayment.Id = int.Parse(lblPaymentId.Text);
                        if (rtxtAmount.Text == "0")
                        {
                            objPayment.DeletePaymentById(int.Parse(lblPaymentId.Text));
                        }
                        else
                        {
                            successPayment = objPayment.UpdatePayment();
                        }
                    }
                    if (successPayment == 0)
                    {
                        Alert.Show("Payment information was not saved successfully.");
                    }
                }



                if (success != 0)
                {
                    //clearAllInfo();
                    string logoPath = Server.MapPath("Images") + @"\Elite Tec logo-01.png";

                    int requisitionId;
                    if (lblId.Text == string.Empty)
                    {
                        requisitionId = new RequisitionMaster().GetMaxRequisitionMasterId();
                    }
                    else
                    {
                        requisitionId = int.Parse(lblId.Text);
                    }

                    MemoryStream pdfData = PrintRequisition.Print(requisitionId, logoPath);

                    if (pdfData == null) return;

                    Session["StreamData"] = pdfData;
                    Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");


                    this.ClearAllInfo();
                }
                //Response.Redirect("CampaignRequisition.aspx");
                if (!bool.Parse(lblnewEntry.Text))
                {
                    btnPrint.Visible = true;
                }
                //else
                //{
                //    //Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx')</script>");
                //}
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save data." + ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearAllInfo()
        {
            rdropPDealer.SelectedIndex = 0;
            rdropCourier.SelectedIndex = 0;
            rdropCS.SelectedIndex = 0;
            rtxtAddress.Text = "";
            rdtRequisitionDate.SelectedDate = null;
            rdropPaymentType.SelectedIndex = 0;
            rdropPaymentmode.SelectedIndex = 0;
            rdropBankName.SelectedIndex = 0;
            rtxtReferenceNo.Text = "";
            rtxtAmount.Text = "";
            lblPaymentId.Text = "";
            lblId.Text = "";
            lblDetails.Text = "";
            rtxtBankCharge.Text = "";
            rdropCode.SelectedIndex = 0;
            rdropName.SelectedIndex = 0;
            lblDP2.Text = "";

            //string rqCode = new RequisitionMaster().GetlastRequisitionCode();
            lblStatus.Text = "Created";
            rdtRequisitionDate.SelectedDate = DateTime.Today;
            rtxtRequisitionNo.Text = "";
            RadGridAddRequisitionDetails.DataSource = new string[] { };
            RadGridAddRequisitionDetails.DataBind();
            btnPrint.Visible = false;

            lblnewEntry.Text = "true";
            lblnewPaymentEntry.Text = "true";
            lblinvoiceCreate.Text = "false";
            lblpaymentStatus.Text = "Created";
            _TempRequisitionDetailsCes = new List<TempRequisitionDetailsC>();
            Session["TempRequisitionDetailsCes"] = _TempRequisitionDetailsCes;
            Session["tempCampaigns"] = null;
            _tempCampaigns = new List<TempCampaigns>();

            rtxtChequeBank.Text = "";
            rtxtChequeBranch.Text = "";
            rdtCheckDate.SelectedDate = null;
            showDeposit.Visible = false;
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearAllInfo();
        }

        protected void rdropCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdropCode.SelectedIndex <= 0)
            {
                Alert.Show("Please select a campaign first.");
                rdropCode.Focus();
                return;
            }

            //load campaign names in dropdown
            int campaignId = int.Parse(rdropCode.SelectedValue);

            List<CampaignDetails> campaignDetailsList = new CampaignDetails().GetAllProductDetaislbyCampaignId(campaignId);
            campaignDetailsList.Insert(0, new CampaignDetails());

            rdropName.DataTextField = "CampaignName";
            rdropName.DataValueField = "Id";
            rdropName.DataSource = campaignDetailsList;
            rdropName.DataBind();

            rdropName.SelectedIndex = 0;

            Session["campaignDetailsList"] = campaignDetailsList;
            //move to div location
            if (Request.QueryString["Id"] != null)
            {
                string id = Request.QueryString["Id"];

                Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx?id=" + id +
                               "#reqDetaislInfo')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx#reqDetaislInfo')</script>");
        }

        protected void rdropPDealer_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DealerInformation objdealerInfo = new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));
            rtxtAddress.Text = objdealerInfo.Address;
            this.LoadSetupChennalDesignation();
            this.LoadJrCS();
        }

        protected void rdropJrCS_OnDataBound(object sender, EventArgs e)
        {
            rdropJrCS.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropPDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropPDealer.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropRegion_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            this.LoadArea(int.Parse(rdropRegion.SelectedValue));
            this.LoadDealerInfo();
            if (Request.QueryString["Id"] != null)
            {
                string id = Request.QueryString["Id"];

                Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx?id=" + id +
                               "#ReqInfo')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx#ReqInfo')</script>");
        }


        protected void btnShowPanal_OnClick(object sender, EventArgs e)
        {
            if (showPanal.Visible == false)
            {
                showPanal.Visible = true;
                btnShowPanal.Text = "Hide Search";
            }

            else
            {
                showPanal.Visible = false;
                btnShowPanal.Text = "Advance Search";
                rdropRegion.SelectedIndex = 0;
                rdropArea.SelectedIndex = 0;
                this.LoadDealerInfo();
            }

        }

        protected void rdropRegion_OnDataBound(object sender, EventArgs e)
        {
            rdropRegion.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropArea_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            this.LoadDealerInfo();

            if (Request.QueryString["Id"] != null)
            {
                string id = Request.QueryString["Id"];

                Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx?id=" + id +
                               "#ReqInfo')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx#ReqInfo')</script>");
        }

        //protected void rdropPaymentType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    if (Request.QueryString["Id"] != null)
        //    {
        //        string id = Request.QueryString["Id"];

        //        Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx?id=" + id +
        //                       "#PayInfo')</script>");
        //    }
        //    else
        //        Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx#PayInfo')</script>");

        //}

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                //clearAllInfo();
                string logoPath = Server.MapPath("Images") + @"\Elite Tec logo-01.png";

                //LinkButton linkedit = (LinkButton)sender;
                //string[] text = linkedit.CommandArgument.ToString().Split(';');

                int requisitionId;
                if (lblId.Text == string.Empty)
                {
                    requisitionId = new RequisitionMaster().GetMaxRequisitionMasterId();
                }
                else
                {
                    requisitionId = int.Parse(lblId.Text);
                }

                MemoryStream pdfData = PrintRequisition.Print(requisitionId, logoPath);

                if (pdfData == null) return;

                Session["StreamData"] = pdfData;
                Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void rdropBankName_OnDataBound(object sender, EventArgs e)
        {
            rdropBankName.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropPaymentmode_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            this.LoadCompanyBank();
            if (rdropPaymentmode.SelectedValue == "1")
            {
                rdropBankName.Enabled = false;
                rtxtBankCharge.Text = "";
                rtxtBranch.Text = "";
                rtxtBankCharge.Enabled = false;
                rtxtBranch.Enabled = false;
            }
            else
            {
                rdropBankName.Enabled = true;
                rtxtBankCharge.Enabled = true;
                rtxtBranch.Enabled = true;
            }
            if (Request.QueryString["Id"] != null)
            {
                string id = Request.QueryString["Id"];

                Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx?id=" + id +
                               "#PayInfo')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx#PayInfo')</script>");
        }

        protected void rdropCourier_OnDataBound(object sender, EventArgs e)
        {
            rdropCourier.Items.Insert(0, new RadComboBoxItem());
        }


        protected void rtxtReqDiscount_OnTextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtxtReqDiscount.Text))
                rtxtReqDiscount.Text = "0";
            if (string.IsNullOrEmpty(rtxtItemTotal.Text))
                rtxtItemTotal.Text = "0";

            rtxtReqTotal.Text = (decimal.Parse(rtxtItemTotal.Text) - decimal.Parse(rtxtReqDiscount.Text)).ToString("N");
        }
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {

        }

        protected void rdropPaymentType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (rdropPaymentType.SelectedItem.Text == "Deposit")
            {
                showDeposit.Visible = true;
            }
            else
            {
                showDeposit.Visible = false;
                rtxtChequeBank.Text = "";
                rtxtChequeBranch.Text = "";
                rdtCheckDate.SelectedDate = null;

            }
            if (Request.QueryString["Id"] != null)
            {
                string id = Request.QueryString["Id"];

                Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx?id=" + id +
                               "#PayInfo')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('CampaignRequisition.aspx#PayInfo')</script>");
        }
    }
}