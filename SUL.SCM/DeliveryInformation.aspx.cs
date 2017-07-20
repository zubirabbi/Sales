using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class DeliveryInformation : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        //private  bool isNewEntry;
        //private  bool isnewPaymentEntry;
        private Company _company;
        private List<TempRequisitionDetails> tempDetails;
        //private  bool _isInvoiceCreate;

        private struct TempRequisitionDetails
        {
            public int Id { get; set; }
            public int CategoryId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Price2 { get; set; }
            public int Unit { get; set; }
            public int Discount { get; set; }
            public int Color { get; set; }
            public string ColorName { get; set; }
            public string ProductCategory { get; set; }
            public string ProductName { get; set; }
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
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsView)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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

        private bool IsValidDeleteForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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
                List<DealerInformation> lstDealerInfo = new DealerInformation().GetAllDealerInformation();
                lstDealerInfo.Insert(0, new DealerInformation());

                rdropPDealer.DataTextField = "DealerCode";
                rdropPDealer.DataValueField = "Id";
                rdropPDealer.DataSource = lstDealerInfo;
                rdropPDealer.DataBind();

                if (lstDealerInfo.Count == 2)
                    rdropPDealer.SelectedIndex = 1;
                else
                    rdropPDealer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
            }
        }


        private void LoadCourierInfo()
        {
            try
            {
                List<CourierInformation> lstCourierInformation = new CourierInformation().GetAllCourierInformation();
                lstCourierInformation.Insert(0, new CourierInformation());

                rdropCourier.DataTextField = "Name";
                rdropCourier.DataValueField = "Id";
                rdropCourier.DataSource = lstCourierInformation;
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
        private void LoadWareHouse()
        {
            try
            {
                List<WareHouse> lstWareHouses = new WareHouse().GetAllWareHousebyWareHouseID();
                lstWareHouses.Insert(0, new WareHouse());

                rdropWareHouse.DataTextField = "Name";
                rdropWareHouse.DataValueField = "Id";
                rdropWareHouse.DataSource = lstWareHouses;
                rdropWareHouse.DataBind();

                if (lstWareHouses.Count == 2)
                    rdropWareHouse.SelectedIndex = 1;
                else
                    rdropWareHouse.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load warehouse data." + ex);
            }
        }
        private void LoadSetupChennalDesignation()
        {
            try
            {
                SetupChannel objsetupChannel = new SetupChannel().GetAllSetupChannelByChannels("Channel Specialized");
                int desigId = objsetupChannel.DesignationId;

                DataTable dtRmployeeInfo =
                    new EmployeeInformation().GetAllEmployeeInformationByDesignationIdbyTable(desigId);

                rdropCS.DataTextField = "empAllInfo";
                rdropCS.DataValueField = "Id";
                rdropCS.DataSource = dtRmployeeInfo;
                rdropCS.DataBind();
                if (dtRmployeeInfo.Rows.Count == 2)
                    rdropCS.SelectedIndex = 1;
                else
                    rdropCS.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load data" + ex);
            }
        }

        private void LoadRequisitionDetails()
        {
            try
            {
                if (tempDetails.Count == 0)
                {
                    RadGridAddRequisitionDetails.DataSource = new string[] { };
                    return;
                }

                RadGridAddRequisitionDetails.DataSource = tempDetails;
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
                if (bool.Parse(lblisNewEntry.Text) == false)
                {
                    List<RequisitionDetails> lstRequisitionDetailses =
                        new RequisitionDetails().GetAllRequisitionDetailsBymasterId(id);
                    if (lstRequisitionDetailses.Count > 0)
                    {
                        tempDetails = new List<TempRequisitionDetails>();
                        foreach (RequisitionDetails lstReqDetails in lstRequisitionDetailses)
                        {
                            TempRequisitionDetails tmpRequisitionDetails = new TempRequisitionDetails();

                            tmpRequisitionDetails.Id = int.Parse(lstReqDetails.Id.ToString());
                            tmpRequisitionDetails.CategoryId = lstReqDetails.CategoryId;

                            //ProductCategory objProductCategory = new ProductCategory().GetProductCategoryById(lstReqDetails.CategoryId);
                            //tmpRequisitionDetails.ProductCategory = objProductCategory.CategoryCode;

                            Product objProduct = new Product().GetProductById(lstReqDetails.ProductId);
                            tmpRequisitionDetails.ProductName = objProduct.ProductCode + ";" + objProduct.ProductName;

                            tmpRequisitionDetails.ProductId = lstReqDetails.ProductId;
                            tmpRequisitionDetails.Quantity = lstReqDetails.Quantity;
                            tmpRequisitionDetails.Price = lstReqDetails.Price;
                            tmpRequisitionDetails.Price2 = lstReqDetails.Price2;
                            tmpRequisitionDetails.Unit = lstReqDetails.Unit;
                            tmpRequisitionDetails.Color = lstReqDetails.Color;
                            tempDetails.Add(tmpRequisitionDetails);
                        }
                        Session["tempDetails"] = tempDetails;
                        LoadRequisitionDetails();
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

            if (!IsValidPageForUser())
            {
                Alert.Show("Sorry, You Don't Have permission to access this page.");
                Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx", false);
            }
            if (Session["tempDetails"] != null)
                tempDetails = (List<TempRequisitionDetails>)Session["tempDetails"];
            else
                tempDetails = new List<TempRequisitionDetails>();

            if (!IsPostBack)
            {
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                lblisNewEntry.Text = "true";
                lblisnewPaymentEntry.Text = "true";
                lblisInvoiceCreate.Text = "false";
                tempDetails = new List<TempRequisitionDetails>();

                this.LoadDealerInfo();

                this.LoadSetupChennalDesignation();
                this.LoadCourierInfo();
                this.LoadWareHouse();
                string rqCode = new RequisitionMaster().GetlastRequisitionCode();
                InvoiceMaster invMaster = new InvoiceMaster();
                lblStatus.Text = "unapproved";
                rtxtInvoiceNo.Text = invMaster.GetlastInvoiceCode();
                rtxtRequisitionNo.Text = rqCode;

                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];
                    lblInvoiceId.Text = id;
                    InvoiceMaster invoiceMaster = new InvoiceMaster().GetInvoiceMasterByRequisitionId(int.Parse(id));
                    RequisitionMaster objRequisitionMaster = new RequisitionMaster().GetRequisitionMasterById(int.Parse(id));
                    rtxtDeliveryAddress.Text = objRequisitionMaster.Address;
                    lblId.Text = objRequisitionMaster.Id.ToString();
                    rtxtInvoiceNo.Text = invoiceMaster.InvoiceNo == string.Empty ? invMaster.GetlastInvoiceCode() : invoiceMaster.InvoiceNo;
                    rdropPDealer.SelectedValue = objRequisitionMaster.DealerId.ToString();
                    rdropCourier.SelectedValue = objRequisitionMaster.Courier;
                    rdropCS.SelectedValue = objRequisitionMaster.CSId.ToString();
                    rtxtAddress.Text = objRequisitionMaster.Address;
                    rtxtRequisitionNo.Text = objRequisitionMaster.RequisitionCode;
                    rdtRequisitionDate.SelectedDate = objRequisitionMaster.RequisitionDate;
                    lblStatus.Text = objRequisitionMaster.Status;
                    lblisInvoiceCreate.Text = objRequisitionMaster.IsInvoiceCreated.ToString();

                    Payment objPayment = new Payment().GetPaymentByRequisitionId(int.Parse(id));

                    lblisnewPaymentEntry.Text = "false";

                    lblPaymentId.Text = objPayment.Id.ToString();

                    lblisNewEntry.Text = "false";
                    this.LoadRequisitionDataFromDatabase(int.Parse(id));

                    if (objRequisitionMaster.IsInvoiceCreated)
                    {
                        btnSave.Visible = false;
                    }

                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                #region validation

                if (rdropWareHouse.SelectedIndex <= 0)
                {
                    Alert.Show("Select a Warehouse.");
                    rdropWareHouse.Focus();
                    return;

                }
                if (rtxtDeliveryAddress.Text == string.Empty)
                {
                    Alert.Show("Delivery Address is empty.");
                    rtxtDeliveryAddress.Focus();
                    return;
                }

                #endregion

                int success;

                DeliveryMaster objMaster = new DeliveryMaster();

                RequisitionMaster objRequisitionMaster =
                    new RequisitionMaster().GetRequisitionMasterById(int.Parse(lblInvoiceId.Text));
                if (tempDetails.Count != 0)
                {
                    if (objRequisitionMaster.Status == "Invoiced")
                    {
                        Alert.Show("This requisition is already invoiced");
                    }
                    else
                    {
                        objMaster.DeliveryNo = new DeliveryMaster().GetlastDeliveryCode();
                        objMaster.DeliveryDate = DateTime.Now;
                        objMaster.DealerId = int.Parse(objRequisitionMaster.DealerId.ToString());
                        objMaster.RequisitionId = int.Parse(lblInvoiceId.Text);
                        objMaster.WareHouseId = int.Parse(rdropWareHouse.SelectedValue);
                        objMaster.CourierId = int.Parse(rdropCourier.SelectedValue);
                        objMaster.DeliveryAddress = rtxtDeliveryAddress.Text;

                        success = objMaster.InsertDeliveryMaster();
                        if (success == 0)
                        {
                            Alert.Show("Delivery data is not save succesfully");
                        }
                        else
                        {
                            int IsinvoiceCreate;
                            InvoiceMaster invMaster = new InvoiceMaster();
                            invMaster.InvoiceNo = invMaster.GetlastInvoiceCode();
                            invMaster.InvoiceDate = DateTime.Now;
                            invMaster.DealerId = int.Parse(objRequisitionMaster.DealerId.ToString());
                            invMaster.RequisitionId = int.Parse(lblInvoiceId.Text);
                            invMaster.UserId = _user.Id;
                            IsinvoiceCreate =
                                new RequisitionMaster().SetIncoiceActiveStatus(int.Parse(lblInvoiceId.Text), _user.Id, true);



                            success = invMaster.InsertInvoiceMaster();

                            if (success == 0)
                            {
                                Alert.Show("Invoice data is not save Succesfully");
                                return;
                            }
                            else
                            {
                                decimal p1 = 0;
                                decimal p2 = 0;
                                decimal Dis = 0;

                                List<RequisitionDetails> lsRequisitionDetails =
                                    new RequisitionDetails().GetAllRequisitionDetailsBymasterId(
                                        int.Parse(lblInvoiceId.Text));
                                if (lsRequisitionDetails.Count != 0)
                                {
                                    foreach (RequisitionDetails requisitionDetails in lsRequisitionDetails)
                                    {
                                        InvoiceDetails objInvoiceDetails = new InvoiceDetails();

                                        objInvoiceDetails.MasterId = invMaster.GetMaxInvoiceMasterId();
                                        objInvoiceDetails.CategoryId = requisitionDetails.CategoryId;
                                        objInvoiceDetails.ProductId = requisitionDetails.ProductId;
                                        objInvoiceDetails.Quantity = requisitionDetails.Quantity;
                                        objInvoiceDetails.Price = requisitionDetails.Price;
                                        p1 += requisitionDetails.Price * requisitionDetails.Quantity;

                                        objInvoiceDetails.Unit = requisitionDetails.Unit;
                                        objInvoiceDetails.Discount = requisitionDetails.Discount;
                                        Dis += requisitionDetails.Discount;

                                        objInvoiceDetails.Color = requisitionDetails.Color;
                                        objInvoiceDetails.Price2 = requisitionDetails.Price2;
                                        p2 += requisitionDetails.Price2 * requisitionDetails.Quantity;
                                        success = objInvoiceDetails.InsertInvoiceDetails();
                                        if (success == 0)
                                        {
                                            Alert.Show("Invoice details Data is not save succesfully");
                                        }
                                        else
                                        {
                                            Alert.Show("Data save succesfully");
                                        }
                                    }

                                    int update;
                                    InvoiceMaster objIMaster = new InvoiceMaster();

                                    objIMaster.Id = invMaster.GetMaxInvoiceMasterId();
                                    objIMaster.ItemTotal = p1;
                                    objIMaster.ItemTotal2 = p2;
                                    objIMaster.Discount = Dis;

                                    update = objIMaster.UpdateInvoiceMasterInformation();

                                    if (update == 0)
                                    {
                                        Alert.Show("Invoice Master not update!!!");
                                    }
                                    else
                                    {
                                        int InvoiceId = invMaster.GetMaxInvoiceMasterId();

                                        invMaster = new InvoiceMaster().GetInvoiceMasterById(InvoiceId);
                                        DealerLedger dealerLedger = new DealerLedger();
                                        DealerInformation objDealerinfo =
                                            new DealerInformation().GetDealerInformationById(
                                                int.Parse(objRequisitionMaster.DealerId.ToString()));


                                        dealerLedger.DealerId = int.Parse(objRequisitionMaster.DealerId.ToString());
                                        dealerLedger.TransactionType = "Invoice";
                                        dealerLedger.TransactionDate = DateTime.Now;
                                        dealerLedger.SourceId = invMaster.Id.ToString();
                                        dealerLedger.UserId = _user.Id;
                                        dealerLedger.OpeningBalance = objDealerinfo.Balance;
                                        dealerLedger.Debit = invMaster.InvoiceTotal;
                                        dealerLedger.Cradit = 0;
                                        dealerLedger.ClosingBalance = 0;
                                        dealerLedger.SourceNo = invMaster.InvoiceNo;
                                        dealerLedger.Remarks = "";
                                        int dealersuccess = dealerLedger.InsertDealerLedger();

                                        if (dealersuccess == 0)
                                        {
                                            Alert.Show("Dealer Ledger information is not save succesfully.");
                                        }
                                        else
                                        {
                                            DealerInformation objdinfo = new DealerInformation();

                                            objdinfo.Id = int.Parse(objRequisitionMaster.DealerId.ToString());
                                            objdinfo.TotalDebit = invMaster.InvoiceTotal;
                                            objdinfo.TotalCredit = 0;

                                            int dInfosuccess = objdinfo.UpdateDealerInformationfordealerLedger();

                                            if (dInfosuccess == 0)
                                            {
                                                Alert.Show("Dealer information is not save succesfully.");
                                            }
                                        }

                                    }

                                }
                            }
                            ItemJournalMaster objDetailsJournal = new ItemJournalMaster();

                            int DeiveryId = new DeliveryMaster().GetMaxDeliveryMasterId();

                            DeliveryMaster objDeliveryMasters = new DeliveryMaster().GetDeliveryMasterById(DeiveryId);

                            objDetailsJournal.TransactionDate = DateTime.Now;
                            objDetailsJournal.TransactionType = "Invoice";
                            objDetailsJournal.SourceId = objDeliveryMasters.DeliveryNo;
                            objDetailsJournal.UserId = _user.Id;
                            objDetailsJournal.WareHouseId = int.Parse(rdropWareHouse.SelectedValue);

                            success = objDetailsJournal.InsertItemJournalMaster();
                            if (success == 0)
                            {
                                Alert.Show("something is going wrong to save Item Details Journal data.");
                            }
                            else
                            {
                                if (tempDetails.Count != 0)
                                {
                                    foreach (TempRequisitionDetails tmpDetails in tempDetails)
                                    {
                                        DeliveryDetails objDeliveryDetails = new DeliveryDetails();
                                        ItemJournalDetails objItemDetails = new ItemJournalDetails();
                                        List<ItemLedger> lstItemLedgers;
                                        ItemLedger objInsertItemLedger = new ItemLedger();

                                        int Openingbalance = 0;

                                        objDeliveryDetails.MasterId = new DeliveryMaster().GetMaxDeliveryMasterId();
                                        objDeliveryDetails.ProductId = tmpDetails.ProductId;
                                        objDeliveryDetails.Quantity = tmpDetails.Quantity;
                                        objDeliveryDetails.Unit = tmpDetails.Unit;
                                        objDeliveryDetails.Color = tmpDetails.Color;

                                        if (objDeliveryDetails.Color == 0)
                                        {
                                            lstItemLedgers =
                                                new ItemLedger().GetAllItemLedgersByItemIdUnit(tmpDetails.ProductId,
                                                    tmpDetails.Unit);
                                        }
                                        else
                                        {
                                            lstItemLedgers =
                                                new ItemLedger().GetAllItemLedgersByItemIdUnitColor(
                                                    tmpDetails.ProductId,
                                                    tmpDetails.Unit, tmpDetails.Color);
                                        }
                                        if (lstItemLedgers.Count != 0)
                                        {
                                            if (objDeliveryDetails.Color == 0)
                                            {
                                                ItemLedger objItemLedger =
                                                    new ItemLedger().GetItemLedgerByItemIdUnit(tmpDetails.ProductId,
                                                        tmpDetails.Unit);
                                                Openingbalance = objItemLedger.Balance;
                                            }
                                            else
                                            {
                                                ItemLedger objItemLedger =
                                                    new ItemLedger().GetItemLedgerByItemIdUnitColor(
                                                        tmpDetails.ProductId,
                                                        tmpDetails.Unit, tmpDetails.Color);
                                                Openingbalance = objItemLedger.Balance;
                                            }
                                        }

                                        //------Insert data in Item Details------//
                                        objItemDetails.MasterId =
                                            new ItemJournalMaster().GetMaxItemDetailsJournalMasterId();
                                        objItemDetails.ProductId = tmpDetails.ProductId;
                                        objItemDetails.ProductName = tmpDetails.ProductName;
                                        objItemDetails.Color = tmpDetails.Color;
                                        objItemDetails.Unit = tmpDetails.Unit;
                                        objItemDetails.OpeningBalance = Openingbalance;
                                        objItemDetails.QuantityIn = 0;
                                        objItemDetails.QuantityOut = tmpDetails.Quantity;
                                        objItemDetails.ClosingBalance = 0;
                                        objItemDetails.Rate = tmpDetails.Price;


                                        success = objDeliveryDetails.InsertDeliveryDetails();
                                        success = objItemDetails.InsertItemJournalDetails();

                                        if (lstItemLedgers.Count != 0)
                                        {
                                            if (objDeliveryDetails.Color == 0)
                                            {
                                                ItemLedger objItemLedger =
                                                    new ItemLedger().GetItemLedgerByItemIdUnit(tmpDetails.ProductId,
                                                        tmpDetails.Unit);
                                                this.UpdateItemLedger(tmpDetails, 0, tmpDetails.Quantity,
                                                    objItemLedger.Id);
                                            }
                                            else
                                            {
                                                ItemLedger objItemLedger =
                                                    new ItemLedger().GetItemLedgerByItemIdUnitColor(
                                                        tmpDetails.ProductId,
                                                        tmpDetails.Unit, tmpDetails.Color);
                                                this.UpdateItemLedger(tmpDetails, 0, tmpDetails.Quantity,
                                                    objItemLedger.Id);
                                            }
                                        }
                                        else
                                        {
                                            if (objDeliveryDetails.Color == 0)
                                            {
                                                this.UpdateItemLedger(tmpDetails, 0, tmpDetails.Quantity, 0);
                                            }
                                            else
                                            {
                                                this.UpdateItemLedger(tmpDetails, 0, tmpDetails.Quantity, 0);
                                            }
                                        }


                                        if (success == 0)
                                        {
                                            Alert.Show("Something is going wrong to Save data");
                                            return;
                                        }
                                        else
                                        {
                                            Alert.Show("Devilery Details Save succesfully.");
                                            //Response.Redirect("DeliveryInformation.aspx");
                                            btnPrintDelevary.Visible = true;
                                            btnPrintInvoice.Visible = true;
                                            btnSave.Visible = false;
                                            btnBack.Visible = true;

                                            //this.PrintInvoice();
                                            //this.PrintDeliveryChallan();
                                        }

                                    }
                                }
                            }


                        }
                    }
                }
                else
                    Alert.Show("There is no product details");
            }

            catch (Exception ex)
            {
                Alert.Show("something id going wrong to save data." + ex);
            }

        }

        private int UpdateItemLedger(TempRequisitionDetails tmpDetails, int totalIn, int totalOut, long ledgerId)
        {
            ItemLedger objItemLedger = new ItemLedger();
            //--------Insert data in Item Legder----------//
            objItemLedger.ItemId = tmpDetails.ProductId;
            objItemLedger.Unit = tmpDetails.Unit;
            objItemLedger.Color = tmpDetails.Color.ToString();
            objItemLedger.WareHouseId = int.Parse(rdropWareHouse.SelectedValue);
            objItemLedger.TotalIn = totalIn;
            objItemLedger.TotalOut = totalOut;

            int success = 0;
            if (ledgerId == 0)
                success = objItemLedger.InsertItemLedger();
            else
            {
                objItemLedger.Id = ledgerId;
                success = objItemLedger.UpdateItemLedger();
            }



            return success;
        }

        protected void btnPrintInvoice_OnClick(object sender, EventArgs e)
        {
            PrintInvoice();
        }
        protected void btnPrintDelevary_OnClick(object sender, EventArgs e)
        {
            PrintDeliveryChallan();
        }
        private void PrintInvoice()
        {
            try
            {
                string logoPath = Server.MapPath("Images") + @"\Elite Tec logo-01.png";


                int requisitionId = int.Parse(lblInvoiceId.Text);

                MemoryStream pdfData = SCM.PrintInvoice.Print(requisitionId, logoPath);

                if (pdfData == null) return;

                Session["StreamData"] = pdfData;
                Response.Write(
                    "<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }



        private void PrintDeliveryChallan()
        {
            try
            {
                string logoPath = Server.MapPath("Images") + @"\Elite Tec logo-01.png";


                int requisitionId = int.Parse(lblInvoiceId.Text);

                MemoryStream pdfData = PrintChannal.Print(requisitionId, logoPath);

                if (pdfData == null) return;

                Session["StreamData"] = pdfData;
                Response.Write(
                    "<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {


            if (Session["url"] == null)
                Response.Redirect("RequisitionInfoList.aspx");


            else
            {
                string url = Session["url"].ToString();
                Response.Redirect(url);
            }
        }
    }
}