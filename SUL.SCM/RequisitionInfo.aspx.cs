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
    public partial class RequisitionInfo : System.Web.UI.Page
    {
        private Users _user;
        private Company _company;
        private List<TempRequisitionDetails> tempRequisitionDetailses;

        private string _department;

        private AppPermission PermissionUser;

        private class TempRequisitionDetails
        {
            public int Id { get; set; }
            public int CategoryId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Price2 { get; set; }
            public int Unit { get; set; }
            public string UnitName { get; set; }
            public decimal Discount { get; set; }
            public int Color { get; set; }
            public string ColorName { get; set; }
            public string ProductName { get; set; }
            public decimal TotalProductPrice { get; set; }
            public decimal TotalProductPriceDiscount { get; set; }

        }

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

            //if (!PermissionUser.IsView)
            //{
            //    AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
            //        _user.CompanyId);
            //    bool permission = Permission.IsView;
            //    return permission;
            //}
            //else
            return true;
        }

        private bool IsValidInsertForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Requisition Add");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsInsert)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsInsert;
                return permission;
            }
            else
                return true;
        }

        private bool IsValidUpdateForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Requisition Add");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsUpdate)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsUpdate;
                return permission;
            }
            else
                return true;
        }
        private bool IsValidViewForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Requisition Add");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsView)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsUpdate;
                return permission;
            }
            else
                return true;
        }

        private bool IsValidDeleteForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Requisition Add");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsDelete)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsDelete;
                return permission;
            }
            else
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
        //private void LoadProductCategory()
        //{
        //    try
        //    {
        //        List<ProductCategory> lstProductCategories = new ProductCategory().GetAllProductCategory();

        //        lstProductCategories.Insert(0, new ProductCategory());

        //        rdropCategory.DataTextField = "CategoryCode";
        //        rdropCategory.DataValueField = "Id";
        //        rdropCategory.DataSource = lstProductCategories;
        //        rdropCategory.DataBind();

        //        rdropCategory.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Alert.Show("something is going wrong to load Product Category Info." + ex);
        //    }

        //}

        //private void LoadProductInfo(int catId)
        //{
        //    try
        //    {
        //        DataTable dtProductInfo = new Product().GetProductFromViewListByCategoryId(catId);

        //        rdropProduct.DataValueField = "Id";
        //        rdropProduct.DataTextField = "proInfo";
        //        rdropProduct.DataSource = dtProductInfo;
        //        rdropProduct.DataBind();

        //        rdropProduct.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Alert.Show("something is going on to load Product Info" + ex);
        //    }
        //}
        private void LoadProductInfo()
        {
            try
            {
                DataTable dtProductInfo = new Product().GetProductFromViewList();

                rdropProduct.DataValueField = "Id";
                rdropProduct.DataTextField = "proInfo";
                rdropProduct.DataSource = dtProductInfo;
                rdropProduct.DataBind();

                rdropProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going on to load Product Info" + ex);
            }
        }

        private void LoadProductUnite()
        {
            try
            {
                if (rdropProduct.SelectedIndex <= 0)
                {
                    List<ProductUnit> lstProductUnits = new ProductUnit().GetAllProductUnit();
                    lstProductUnits.Insert(0, new ProductUnit());

                    rdropProductUnit.DataTextField = "UnitCode";
                    rdropProductUnit.DataValueField = "Id";
                    rdropProductUnit.DataSource = lstProductUnits;
                    rdropProductUnit.DataBind();
                    if (lstProductUnits.Count == 2)
                    {
                        rdropProductUnit.SelectedIndex = 1;
                    }
                    else
                    {
                        rdropProductUnit.SelectedIndex = 0;
                    }

                }
                else
                {
                    int productId = int.Parse(rdropProduct.SelectedValue);

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

        private void LoadColor(int ProductId)
        {
            try
            {
                List<ListTable> lstListTables = new ListTable().GetAllListTableByTypeAndPid("Color", ProductId);
                lstListTables.Insert(0, new ListTable());

                rdropColor.DataTextField = "ListValue";
                rdropColor.DataValueField = "ListId";
                rdropColor.DataSource = lstListTables;
                rdropColor.DataBind();

                rdropColor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                Alert.Show(ex.Message);
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
                if (dtRmployeeInfo.Rows.Count == 1)
                {
                    rdropCS.SelectedIndex = 1;
                }
                else
                    rdropCS.SelectedIndex = 0;
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
                if (tempRequisitionDetailses.Count == 0)
                {
                    RadGridAddRequisitionDetails.DataSource = new string[] { };
                    return;
                }

                RadGridAddRequisitionDetails.DataSource = tempRequisitionDetailses;
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
                    List<ListTable> lstListTables = new ListTable().GetAllListTableByType("Color");
                    if (lstRequisitionDetailses.Count > 0)
                    {
                        tempRequisitionDetailses = new List<TempRequisitionDetails>();
                        foreach (RequisitionDetails lstReqDetails in lstRequisitionDetailses)
                        {
                            TempRequisitionDetails tmpRequisitionDetails = new TempRequisitionDetails();

                            tmpRequisitionDetails.Id = int.Parse(lstReqDetails.Id.ToString());
                            lblDetails.Text = lstReqDetails.Id.ToString();
                            //tmpRequisitionDetails.CategoryId = lstReqDetails.CategoryId;

                            //ProductCategory objProductCategory = new ProductCategory().GetProductCategoryById(lstReqDetails.CategoryId);
                            //tmpRequisitionDetails.ProductCategory = objProductCategory.CategoryCode;

                            Product objProduct = new Product().GetProductById(lstReqDetails.ProductId);
                            tmpRequisitionDetails.ProductName = objProduct.ProductCode + ";" + objProduct.ProductName;

                            //this.LoadProductInfo(lstReqDetails.CategoryId);
                            this.LoadProductInfo();

                            tmpRequisitionDetails.ProductId = lstReqDetails.ProductId;
                            tmpRequisitionDetails.Quantity = lstReqDetails.Quantity;
                            tmpRequisitionDetails.Price = lstReqDetails.Price;
                            tmpRequisitionDetails.Price2 = lstReqDetails.Price2;
                            lblDP2.Text = lstReqDetails.Price2.ToString();
                            tmpRequisitionDetails.Unit = lstReqDetails.Unit;
                            ProductUnit objProductUnit = new ProductUnit().GetProductUnitById(int.Parse(rdropProductUnit.SelectedValue));
                            tmpRequisitionDetails.UnitName = objProductUnit.UnitCode;
                            tmpRequisitionDetails.Color = lstReqDetails.Color;
                            if (lstReqDetails.Color != 0)
                            {
                                ListTable objlisttable = lstListTables.Find(x => x.ListId == lstReqDetails.Color);
                                tmpRequisitionDetails.ColorName = objlisttable.ListValue;
                            }
                            tmpRequisitionDetails.Discount = lstReqDetails.Discount;
                            tmpRequisitionDetails.TotalProductPrice = lstReqDetails.Price * lstReqDetails.Quantity;
                            tmpRequisitionDetails.TotalProductPriceDiscount = lstReqDetails.Price * lstReqDetails.Quantity - lstReqDetails.Discount;
                            tempRequisitionDetailses.Add(tmpRequisitionDetails);
                        }

                        Session["tempRequisitionDetailses"] = tempRequisitionDetailses;

                        LoadRequisitionDetails();
                        decimal totalsum;

                        totalsum = tempRequisitionDetailses.Sum(x => x.TotalProductPriceDiscount);
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
            //if (!IsValidPageForUser())
            //{
            //    Alert.Show("Sorry, You Don't Have permission to access this page.");
            //    Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx", false);
            //}

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

            if (Session["tempRequisitionDetailses"] != null)
                tempRequisitionDetailses = (List<TempRequisitionDetails>)Session["tempRequisitionDetailses"];
            else
                tempRequisitionDetailses = new List<TempRequisitionDetails>();


            if (!IsPostBack)
            {
                if (_user.EmployeeId != 0)
                {
                    Department objDepartment = new Department().GetEmployeeDepartment(_user.EmployeeId);
                    _department = objDepartment.DepartmentName;
                }

                this.ClearAllInfo();

                //region load all combo
                #region region load all combo

                this.LoadDealerInfo();
                //this.LoadProductCategory();
                this.LoadProductUnite();
                this.LoadProductInfo();
                this.LoadCourierInfo();
                //this.LoadCompanyBank();
                this.LoadPaymentType();
                this.LoadPaymentMode();
                this.LoadSetupChennalDesignation();
                this.LoadJrCS();
                this.LoadRegion();

                #endregion

                // start load campaigns on value
                DateTime date = (rdtRequisitionDate.SelectedDate == null)
                    ? DateTime.Today
                    : DateTime.Parse(rdtRequisitionDate.SelectedDate.ToString());

                List<CampaignDetails> objCampaignDetails = new CampaignDetails().GetActiveCampaigns("Value", true, date);
                if (objCampaignDetails.Count > 0)
                    Session["objCampaignDetails"] = objCampaignDetails;

                //end load campaigns on vlaue

                if (Request.QueryString["Id"] != null)
                {
                    this.LoadCompanyBank();
                    this.LoadPaymentType();
                    this.LoadPaymentMode();
                    //this.LoadSetupChennalDesignation();
                    //this.LoadJrCS();

                    string id = "";
                    id = Request.QueryString["Id"];

                    LoadRequisitionData(id);
                }

                if (IsValidPageForUser())
                {

                    PermissionUser = (AppPermission)Session["PermissionUser"];
                    if (!PermissionUser.IsView)
                    {
                        Alert.Show("Sorry, You Don't Have permission to access this page.");
                        Response.Redirect("ErrorPage.aspx", false);
                    }
                    if (bool.Parse(lblnewEntry.Text))
                    {
                        if (!PermissionUser.IsInsert)
                        {
                            Alert.Show("Sorry, You Don't Have permission to access this page.");
                            Response.Redirect("ErrorPage.aspx", false);
                        }
                    }
                    else
                    {
                        if (!PermissionUser.IsUpdate)
                        {
                            btnSave.Visible = false;
                            btnPrint.Visible = true;
                            btnClear.Visible = false;
                            btnAddRequisitionDetails.Visible = false;
                        }
                        if (!PermissionUser.IsDelete)
                        {
                            // hide delete button
                        }
                    }

                    if (PermissionUser.IsView && !PermissionUser.IsInsert && !PermissionUser.IsUpdate)
                    {
                        btnSave.Visible = false;
                        btnPrint.Visible = true;
                        btnClear.Visible = false;
                        btnAddRequisitionDetails.Visible = false;
                    }
                }
            }
        }

        private void LoadRequisitionData(string id)
        {
            RequisitionMaster objRequisitionMaster = new RequisitionMaster().GetRequisitionMasterById(int.Parse(id));

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
            lblUpdateBy.Text = objRequisitionMaster.UpdateBy.ToString();
            lblCampaignId.Text = (objRequisitionMaster.CampaignId == null)
                ? "0"
                : objRequisitionMaster.CampaignId.ToString();
            if (lblCampaignId.Text != "0")
            {
                CampaignMaster campaign = new CampaignMaster().GetCampaignMasterById(objRequisitionMaster.CampaignId);
                lblCampaignCode.Text = campaign.CampaignCode;
                lblCampaignCode.Visible = true;
            }

            rtxtItemTotal.Text = objRequisitionMaster.ItemTotal.ToString();
            rtxtReqDiscount.Text = objRequisitionMaster.Discount.ToString();
            rtxtReqTotal.Text = (objRequisitionMaster.ItemTotal - objRequisitionMaster.Discount).ToString();


            Payment objPayment = new Payment().GetPaymentByRequisitionId(int.Parse(id));

            lblnewPaymentEntry.Text = "false";

            lblPaymentId.Text = objPayment.Id.ToString();
            rdropPaymentType.SelectedValue = objPayment.PaymentType.ToString();

            rdropPaymentmode.SelectedValue = objPayment.PaymentMode.ToString();
            rdropBankName.SelectedValue = objPayment.BankNameId.ToString();
            rtxtReferenceNo.Text = objPayment.ReferenceNo;
            rtxtAmount.Text = objPayment.Amount.ToString();
            rtxtBankCharge.Text = objPayment.BankCharge.ToString();
            lblpaymentStatus.Text = objPayment.Status;
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
          
            if (lblStatus.Text.ToLower() == "created" && _department.ToLower().Contains("account"))
            {
                new RequisitionMaster().ChangeRequisitionStatus(int.Parse(lblId.Text), _user.Id, "Seen");
            }

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

            //isNewEntry = false;
            lblnewEntry.Text = "false";
            this.LoadRequisitionDataFromDatabase(int.Parse(id));
            btnPrint.Visible = true;

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
        }

        protected void rdropCS_OnDataBound(object sender, EventArgs e)
        {
            rdropCS.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropProduct_OnDataBound(object sender, EventArgs e)
        {
            rdropProduct.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadProductInfo(int.Parse(rdropCategory.SelectedValue));
        }

        protected void RadGridAddRequisitionDetails_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnSelect")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblDetails.Text = item["colId"].Text;
                    //rdropCategory.SelectedValue = item["colCategoryId"].Text;
                    rdropProduct.SelectedValue = item["colProductId"].Text;
                    rdropProductUnit.SelectedValue = item["colUnit"].Text;
                    rdropColor.SelectedValue = item["colColor"].Text;
                    rtxtPrice.Text = item["colPrice"].Text;
                    //lblDP2.Text = item["colPrice2"].Text;
                   
                    rtxtQuentity.Text = item["colQuantity"].Text;
                    decimal dis = decimal.Parse(item["colDiscount"].Text)/decimal.Parse(item["colQuantity"].Text);
                    rtxtDiscount.Text = dis.ToString();

                    decimal totalPrice = decimal.Parse(rtxtPrice.Text) * decimal.Parse(rtxtQuentity.Text);
                    decimal totalPriceDiscount = totalPrice - decimal.Parse(item["colDiscount"].Text);

                    rtxtTotalPrice.Text = totalPrice.ToString();
                    rtxtTotalPriceAfterDiscount.Text = totalPriceDiscount.ToString();



                    if (Request.QueryString["Id"] != null)
                    {
                        string id = Request.QueryString["Id"];

                        Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx?id=" + id +
                                       "#reqDetaislInfo')</script>");
                    }
                    else
                        Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx#reqDetaislInfo')</script>");
                }
                if (e.CommandName == "btnDelete")
                {

                    GridDataItem item = (GridDataItem)e.Item;
                    lblDetails.Text = item["colId"].Text;
                    if (lblDetails.Text == string.Empty || lblDetails.Text == "0")
                    {
                        TempRequisitionDetails objTempRequisitionDetailses =
                            tempRequisitionDetailses.Find(
                                x =>
                                    x.ProductId == int.Parse(item["colProductId"].Text) &&
                                    x.Color == int.Parse(item["colColor"].Text));
                        tempRequisitionDetailses.Remove(objTempRequisitionDetailses);
                        Session["tempRequisitionDetailses"] = tempRequisitionDetailses;

                        RadGridAddRequisitionDetails.DataSource = tempRequisitionDetailses;
                        RadGridAddRequisitionDetails.DataBind();

                        var itemTotal = tempRequisitionDetailses.Sum(x => x.TotalProductPriceDiscount);
                        rtxtItemTotal.Text = itemTotal.ToString("N");
                        this.GetCampaign(decimal.Parse(itemTotal.ToString()));
                        this.CalculateTotal();

                        if (Request.QueryString["Id"] != null)
                        {
                            string id = Request.QueryString["Id"];

                            Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx?id=" + id +
                                           "#divReqDetails')</script>");
                        }
                        else
                            Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx#divReqDetails')</script>");
                    }
                    else
                    {
                        RequisitionMaster objRequisitionMaster = new RequisitionMaster().GetRequisitionMasterById(int.Parse(lblId.Text));
                        if (objRequisitionMaster.Status.ToLower() == "created")
                        {
                            int delete =
                                new RequisitionDetails().DeleteRequisitionDetailsById(int.Parse(lblDetails.Text));

                            TempRequisitionDetails objTempRequisitionDetailses =
                                tempRequisitionDetailses.Find(
                                    x => x.Id == int.Parse(lblDetails.Text));
                            tempRequisitionDetailses.Remove(objTempRequisitionDetailses);
                            Session["tempRequisitionDetailses"] = tempRequisitionDetailses;

                            this.LoadRequisitionDataFromDatabase(int.Parse(lblId.Text));

                            if (delete == 0)
                            {
                                Alert.Show("Cannot delete Data");
                            }
                            else
                            {
                                if (Request.QueryString["Id"] != null)
                                {
                                    string id = Request.QueryString["Id"];

                                    Response.Write(
                                        "<script type='text/javascript'> location.replace('RequisitionInfo.aspx?id=" +
                                        id +
                                        "#divReqDetails')</script>");
                                }
                                else
                                    Response.Write(
                                        "<script type='text/javascript'> location.replace('RequisitionInfo.aspx#divReqDetails')</script>");
                            }

                        }
                        else
                        {
                            Alert.Show("You can not delete this Product .because its already " + objRequisitionMaster.Status);
                        }

                    }


                }
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


                if (rdropProduct.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Product.");
                    rdropProduct.Focus();
                    return;
                }
                if (rdropProductUnit.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Product Unit.");
                    rdropProductUnit.Focus();
                    return;
                }

                if (rtxtQuentity.Text == string.Empty || !regexForQuentity.IsMatch(rtxtQuentity.Text))
                {
                    Alert.Show("Please enter a valid quantity");
                    rtxtQuentity.Focus();
                    return;
                }
                if (rtxtDiscount.Text != string.Empty)
                {
                    if (!regexForPriceUnite.IsMatch(rtxtDiscount.Text))
                    {
                        Alert.Show("Please enter a valid discount");
                        rtxtQuentity.Focus();
                        return;
                    }
                }
                else
                    rtxtDiscount.Text = "0";

                #endregion


                #region region add requisition details into temp

                TempRequisitionDetails objTempRequisitionDetailses =
                   tempRequisitionDetailses.Find(x => x.ProductId == int.Parse(rdropProduct.SelectedValue) && x.Color == int.Parse(rdropColor.SelectedValue));
                if (objTempRequisitionDetailses != null)
                {
                    if (objTempRequisitionDetailses.ProductId == 0)
                        objTempRequisitionDetailses = new TempRequisitionDetails();
                    else
                    {
                        tempRequisitionDetailses.Remove(objTempRequisitionDetailses);
                    }
                }
                else
                    objTempRequisitionDetailses = new TempRequisitionDetails();

                objTempRequisitionDetailses.ProductId = int.Parse(rdropProduct.SelectedValue);
                //objTempRequisitionDetailses.CategoryId = int.Parse(rdropCategory.SelectedValue);
                objTempRequisitionDetailses.Unit = int.Parse(rdropProductUnit.SelectedValue);
                ProductUnit objProductUnit = new ProductUnit().GetProductUnitById(int.Parse(rdropProductUnit.SelectedValue));
                objTempRequisitionDetailses.UnitName = objProductUnit.UnitCode;
                objTempRequisitionDetailses.Color = int.Parse(rdropColor.SelectedValue);
                objTempRequisitionDetailses.Price = decimal.Parse(rtxtPrice.Text);
                objTempRequisitionDetailses.Price2 = lblDP2.Text == string.Empty ? 0 : decimal.Parse(lblDP2.Text);
                objTempRequisitionDetailses.Quantity = int.Parse(rtxtQuentity.Text);
                objTempRequisitionDetailses.Discount = rtxtDiscount.Text == string.Empty ? 0 : decimal.Parse(rtxtDiscount.Text)*decimal.Parse(rtxtQuentity.Text);
                //objTempRequisitionDetailses.ProductCategory = rdropCategory.SelectedItem.Text;
                objTempRequisitionDetailses.ProductName = rdropProduct.SelectedItem.Text;


                objTempRequisitionDetailses.TotalProductPrice = objTempRequisitionDetailses.Price * objTempRequisitionDetailses.Quantity;
                objTempRequisitionDetailses.TotalProductPriceDiscount = objTempRequisitionDetailses.TotalProductPrice - objTempRequisitionDetailses.Discount;
                if (rdropColor.SelectedIndex <= 0)
                {
                    objTempRequisitionDetailses.ColorName = "";
                }
                else
                {
                    objTempRequisitionDetailses.ColorName = rdropColor.SelectedItem.Text;
                }

                #endregion


                if (tempRequisitionDetailses.Count == 0)
                    tempRequisitionDetailses = new List<TempRequisitionDetails>();
                tempRequisitionDetailses.Add(objTempRequisitionDetailses);

                Session["tempRequisitionDetailses"] = tempRequisitionDetailses;
                this.LoadRequisitionDetails();

                decimal totalsum = tempRequisitionDetailses.Sum(x => x.TotalProductPriceDiscount);
                rtxtItemTotal.Text = totalsum.ToString();

                decimal Subprice = tempRequisitionDetailses.Sum(x => x.TotalProductPrice);
                rtxtSubTotal.Text = Subprice.ToString();

                decimal ReqDiscount = tempRequisitionDetailses.Sum(x => x.Discount);
                rtxtItemDiscount.Text = ReqDiscount.ToString();

                // check if any value campaign is active and the amount fits in any slab
                if (Session["objCampaignDetails"] != null)
                {
                    this.GetCampaign(totalsum);
                }
                else
                {
                    lblCampaignCode.Text = "";
                    lblCampaignId.Text = "";
                    lblCampaignCode.Visible = false;
                }

                if (string.IsNullOrEmpty(rtxtReqDiscount.Text))
                    rtxtReqDiscount.Text = "0";

                rtxtReqTotal.Text = (totalsum - decimal.Parse(rtxtReqDiscount.Text)).ToString("N");

                // end of checking

                Session["tempRequisitionDetailses"] = tempRequisitionDetailses;
                this.LoadRequisitionDetails();

                rtxtQuentity.Text = "";
                rtxtTotalPrice.Text = "";
                rtxtTotalPriceAfterDiscount.Text = "";
                rtxtDiscount.Text = "";

                if (Request.QueryString["Id"] != null)
                {
                    string id = Request.QueryString["Id"];

                    Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx?id=" + id +
                                   "#reqDetaislInfo')</script>");
                }
                else
                    Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx#reqDetaislInfo')</script>");

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to add Requisition data." + ex);
            }
        }

        private void GetCampaign(decimal totalsum)
        {
            TempRequisitionDetails objTempRequisitionDetailses;
            ProductUnit objProductUnit;
            tempRequisitionDetailses.RemoveAll(x => x.Price == 0);

            List<CampaignDetails> objCampaignDetails = (List<CampaignDetails>)Session["objCampaignDetails"];

            CampaignDetails campaignDetails =
                objCampaignDetails.Find(x => totalsum >= x.StartValue && totalsum <= x.EndValue);
            if (campaignDetails != null)
            {
                lblCampaignId.Text = campaignDetails.CampaignId.ToString();
                CampaignMaster campMaster = new CampaignMaster().GetCampaignMasterById(campaignDetails.CampaignId);
                lblCampaignCode.Text = campMaster.CampaignCode;
                lblCampaignCode.Visible = true;
                rtxtReqDiscount.Text = "0";

                if (!campMaster.IsAdjustedAfterEnd)
                {
                    if (campaignDetails.OfferAmount > 0)
                        rtxtReqDiscount.Text = campaignDetails.OfferAmount.ToString();
                    else
                    {
                        decimal pcnt = campaignDetails.DiscountPcnt;
                        decimal value = (totalsum * pcnt) / 100;
                        rtxtReqDiscount.Text = value.ToString("N");
                    }
                }

                List<CampaignOfferProducts> offerProducts =
                    new CampaignOfferProducts().GetProductsByDetailsId(campaignDetails.Id);

                if (offerProducts.Count > 0)
                {
                    foreach (CampaignOfferProducts offerProduct in offerProducts)
                    {
                        objTempRequisitionDetailses =
                            tempRequisitionDetailses.Find(
                                x => x.ProductId == offerProduct.ProductId && x.Price == 0);
                        if (objTempRequisitionDetailses != null)
                        {
                            if (objTempRequisitionDetailses.ProductId == 0)
                                objTempRequisitionDetailses = new TempRequisitionDetails();
                            else
                            {
                                tempRequisitionDetailses.Remove(objTempRequisitionDetailses);
                            }
                        }
                        else
                            objTempRequisitionDetailses = new TempRequisitionDetails();

                        Product prod = new Product().GetProductById(offerProduct.ProductId);

                        objTempRequisitionDetailses.ProductId = offerProduct.ProductId;
                        objTempRequisitionDetailses.Unit = prod.BaseUnit;
                        objProductUnit =
                            new ProductUnit().GetProductUnitById(prod.BaseUnit);
                        objTempRequisitionDetailses.UnitName = objProductUnit.UnitCode;
                        objTempRequisitionDetailses.Color = 0;
                        objTempRequisitionDetailses.Price = 0;
                        objTempRequisitionDetailses.Price2 = 0;
                        objTempRequisitionDetailses.Quantity = offerProduct.Quantity;
                        objTempRequisitionDetailses.Discount = 0;

                        objTempRequisitionDetailses.ProductName = prod.ProductName;

                        objTempRequisitionDetailses.TotalProductPrice = 0;
                        objTempRequisitionDetailses.TotalProductPriceDiscount = 0;

                        objTempRequisitionDetailses.ColorName = "";

                        if (tempRequisitionDetailses.Count == 0)
                            tempRequisitionDetailses = new List<TempRequisitionDetails>();
                        tempRequisitionDetailses.Add(objTempRequisitionDetailses);
                    }
                }
                // end free product
            }
            else
            {
                lblCampaignId.Text = "0";
                lblCampaignCode.Text = "";
                lblCampaignCode.Visible = false;
                rtxtReqDiscount.Text = "0";
            }

            this.CalculateTotal();
            this.LoadRequisitionDetails();
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
                if (tempRequisitionDetailses.Count == 0)
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
                objRequisitionMaster.Status = lblStatus.Text == string.Empty ? "Created" : lblStatus.Text;
                objRequisitionMaster.IsInvoiceCreated = bool.Parse(lblinvoiceCreate.Text);
                objRequisitionMaster.JrCSId = rdropJrCS.SelectedIndex <= 0 ? 0 : int.Parse(rdropJrCS.SelectedValue);
                objRequisitionMaster.UserId = _user.Id;
                objRequisitionMaster.CencelDate = PublicVariables.minDate;
                objRequisitionMaster.CencelBy = 0;
                objRequisitionMaster.CencelNote = "";
                objRequisitionMaster.LastBalance = objdInformation.Balance;
                objRequisitionMaster.Remarks = rtxtRemarks.Value;
                objRequisitionMaster.CampaignId = (string.IsNullOrEmpty(lblCampaignId.Text)) ? 0 : int.Parse(lblCampaignId.Text);
                if (lblUpdateBy.Text == string.Empty || lblUpdateBy.Text == "0")
                {
                    objRequisitionMaster.UpdateBy = 0;
                }
                


                int success;
                int delete;
                if (bool.Parse(lblnewEntry.Text))
                {
                    string rqCode = (string.IsNullOrEmpty(lblCampaignId.Text))
                        ? new RequisitionMaster().GetlastRequisitionCode()
                        : new RequisitionMaster().GetlastCampaignRequisitionCode();

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
                    objRequisitionMaster.UpdateBy = _user.Id;
                    success = objRequisitionMaster.UpdateRequisitionMaster();
                }
                if (success == 0)
                {
                    Alert.Show("Requsition Master data is not save successfully");
                    return;
                }

                if (tempRequisitionDetailses.Count != 0)
                {
                    decimal totalPrice = 0;
                    decimal totalPrice2 = 0;
                    decimal totalDiscount1 = 0;
                    decimal totalDiscount2 = 0;

                    int update;
                    int masterid = 0;
                    foreach (TempRequisitionDetails tmpDetails in tempRequisitionDetailses)
                    {
                        RequisitionDetails objRequisitionDetails = new RequisitionDetails();

                        masterid = int.Parse(lblId.Text);
                        this.LoadRequisitionDataFromDatabase(int.Parse(lblId.Text));

                        lblDetails.Text = tmpDetails.Id.ToString();
                        objRequisitionDetails.MasterId = masterid;
                        objRequisitionDetails.CategoryId = tmpDetails.CategoryId;
                        objRequisitionDetails.ProductId = tmpDetails.ProductId;
                        objRequisitionDetails.Unit = tmpDetails.Unit;
                        objRequisitionDetails.Color = tmpDetails.Color;
                        objRequisitionDetails.Quantity = tmpDetails.Quantity;
                        objRequisitionDetails.Price = tmpDetails.Price;
                        objRequisitionDetails.Price2 = tmpDetails.Price2;

                        // LINE DISCOUNT FOR PERCENTAGE

                        //objRequisitionDetails.Discount = ((tmpDetails.Price * tmpDetails.Quantity) * tmpDetails.Discount) / 100;
                        //objRequisitionDetails.Discount2 = ((tmpDetails.Price2 * tmpDetails.Quantity) * tmpDetails.Discount) / 100;

                        // LINE DISCOUNT FOR NORMAL

                        objRequisitionDetails.Discount = tmpDetails.Discount;
                        objRequisitionDetails.Discount2 = tmpDetails.Discount;

                        List<RequisitionDetails> lseRequisitionDetailses =
                            new RequisitionDetails().GetAllRequisitionDetailsBymasterIdProductIdCateIdColorId(
                                objRequisitionDetails.ProductId, objRequisitionDetails.Unit,
                                objRequisitionDetails.Color, int.Parse(objRequisitionDetails.MasterId.ToString()));

                        totalPrice += (tmpDetails.Price * tmpDetails.Quantity);
                        totalPrice2 += (tmpDetails.Price2 * tmpDetails.Quantity);
                        totalDiscount1 += objRequisitionDetails.Discount;
                        totalDiscount2 += objRequisitionDetails.Discount2;

                        if (lseRequisitionDetailses.Count == 0)
                        {

                            success = objRequisitionDetails.InsertRequisitionDetails();
                        }
                        else
                        {
                            objRequisitionDetails.Id = int.Parse(lblDetails.Text);
                            success = objRequisitionDetails.UpdateRequisitionDetails();

                        }
                        if (success == 0)
                        {
                            Alert.Show("Requisition Data isnot save succesfully..");
                        }
                    }

                    this.LoadRequisitionDataFromDatabase(int.Parse(lblId.Text));
                    objRequisitionMaster.Id = masterid;
                    objRequisitionMaster.ItemTotal = totalPrice - totalDiscount1;
                    objRequisitionMaster.ItemTotal2 = totalPrice2 - totalDiscount2;
                    objRequisitionMaster.Discount = (rtxtReqDiscount.Text == string.Empty)
                        ? 0
                        : decimal.Parse(rtxtReqDiscount.Text);

                    update = objRequisitionMaster.UpdateRequisitionMasterByPrice();
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
                    objPayment.Status = lblpaymentStatus.Text == string.Empty ? "Created" : lblpaymentStatus.Text;
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
                                objPayment.CreatedBy = objPaymentUp.CreatedBy;
                                successPayment = objPayment.UpdatePayment();
                            }
                        }
                    }
                    else
                    {
                        objPayment.Id = int.Parse(lblPaymentId.Text);
                        if (rtxtAmount.Text == "0")
                        {
                            delete = objPayment.DeletePaymentById(int.Parse(lblPaymentId.Text));
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
                //Response.Redirect("RequisitionInfo.aspx");
                if (!bool.Parse(lblnewEntry.Text))
                {
                    btnPrint.Visible = true;
                }
                //else
                //{
                //    //Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx')</script>");
                //}
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save data." + ex);
            }
        }

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
            // rdropCategory.SelectedIndex = 0;
            rdropProduct.SelectedIndex = 0;
            rdropProductUnit.SelectedIndex = 0;
            rdropColor.SelectedIndex = 0;
            rtxtDiscount.Text = "0";
            rtxtQuentity.Text = "";
            rtxtPrice.Text = "";
            rtxtDiscount.Text = "";
            rtxtReqTotal.Text = "";
            rtxtBranch.Text = "";
            rtxtChequeBranch.Text = "";
            rtxtChequeBank.Text = "";
            rdtCheckDate.SelectedDate = null;

            lblDP2.Text = "";
            lblCampaignId.Text = "";
            lblCampaignCode.Text = "";
            lblCampaignCode.Visible = false;
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
            tempRequisitionDetailses = new List<TempRequisitionDetails>();
            Session["tempRequisitionDetailses"] = tempRequisitionDetailses;
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearAllInfo();
        }

        protected void rdropProduct_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Product objProduct = new Product().GetProductById(int.Parse(rdropProduct.SelectedValue));

            rtxtPrice.Text = objProduct.DP.ToString();
            lblDP2.Text = objProduct.DP2.ToString();
            rdropProductUnit.SelectedIndex = objProduct.BaseUnit;
            this.LoadColor(int.Parse(rdropProduct.SelectedValue));

            int quantity = rtxtQuentity.Text == string.Empty ? 0 : int.Parse(rtxtQuentity.Text);

            decimal TotalPrice = decimal.Parse(rtxtPrice.Text) * quantity;

            rtxtTotalPrice.Text = TotalPrice.ToString();


            if (Request.QueryString["Id"] != null)
            {
                string id = Request.QueryString["Id"];

                Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx?id=" + id +
                               "#reqDetaislInfo')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx#reqDetaislInfo')</script>");
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

                Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx?id=" + id +
                               "#ReqInfo')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx#ReqInfo')</script>");
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

                Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx?id=" + id +
                               "#ReqInfo')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx#ReqInfo')</script>");
        }

        //protected void rdropPaymentType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    if (Request.QueryString["Id"] != null)
        //    {
        //        string id = Request.QueryString["Id"];

        //        Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx?id=" + id +
        //                       "#PayInfo')</script>");
        //    }
        //    else
        //        Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx#PayInfo')</script>");

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

                Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx?id=" + id +
                               "#PayInfo')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx#PayInfo')</script>");
        }

        protected void rdropCourier_OnDataBound(object sender, EventArgs e)
        {
            rdropCourier.Items.Insert(0, new RadComboBoxItem());
        }



        protected void btnDelete_OnClick(object sender, EventArgs e)
        {

        }

        protected void rtxtReqDiscount_OnTextChanged(object sender, EventArgs e)
        {

        }

        private void CalculateTotal()
        {
            if (string.IsNullOrEmpty(rtxtReqDiscount.Text))
                rtxtReqDiscount.Text = "0";
            if (string.IsNullOrEmpty(rtxtItemTotal.Text))
                rtxtItemTotal.Text = "0";

            rtxtReqTotal.Text = (decimal.Parse(rtxtItemTotal.Text) - decimal.Parse(rtxtReqDiscount.Text)).ToString("N");
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

                Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx?id=" + id +
                               "#PayInfo')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('RequisitionInfo.aspx#PayInfo')</script>");
        }
    }
}