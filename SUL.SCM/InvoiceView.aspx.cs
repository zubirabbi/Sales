using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;

namespace SUL.SCM
{
    public partial class InvoiceView : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  Company _company;
        private  List<TempRequisitionDetails> tempRequisitionDetailses;

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
                if (bool.Parse(lblisNewEntry.Text) == false)
                {
                    List<RequisitionDetails> lstRequisitionDetailses =
                        new RequisitionDetails().GetAllRequisitionDetailsBymasterId(id);
                    if (lstRequisitionDetailses.Count > 0)
                    {
                        tempRequisitionDetailses = new List<TempRequisitionDetails>();
                        foreach (RequisitionDetails lstReqDetails in lstRequisitionDetailses)
                        {
                           TempRequisitionDetails tmpRequisitionDetails = new TempRequisitionDetails();

                            tmpRequisitionDetails.Id = int.Parse(lstReqDetails.Id.ToString());
                            tmpRequisitionDetails.CategoryId = lstReqDetails.CategoryId;

                            ProductCategory objProductCategory = new ProductCategory().GetProductCategoryById(lstReqDetails.CategoryId);
                            tmpRequisitionDetails.ProductCategory = objProductCategory.CategoryCode;

                            Product objProduct = new Product().GetProductById(lstReqDetails.ProductId);
                            tmpRequisitionDetails.ProductName = objProduct.ProductCode + ";" + objProduct.ProductName;

                            tmpRequisitionDetails.ProductId = lstReqDetails.ProductId;
                            tmpRequisitionDetails.Quantity = lstReqDetails.Quantity;
                            tmpRequisitionDetails.Price = lstReqDetails.Price;
                            tmpRequisitionDetails.Price2 = lstReqDetails.Price2;
                            tmpRequisitionDetails.Unit = lstReqDetails.Unit;
                            tmpRequisitionDetails.Color = lstReqDetails.Color;
                            tempRequisitionDetailses.Add(tmpRequisitionDetails);
                        }
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
            if (Session["tempRequisitionDetailses"] != null)
                tempRequisitionDetailses = (List<TempRequisitionDetails>)Session["tempRequisitionDetailses"];
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
                tempRequisitionDetailses = new List<TempRequisitionDetails>();
              
                this.LoadDealerInfo();
              
                this.LoadSetupChennalDesignation();
                this.LoadCourierInfo();
               
                string rqCode = new RequisitionMaster().GetlastRequisitionCode();

                lblStatus.Text = "unapproved";

                rtxtRequisitionNo.Text = rqCode;

                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];

                    InvoiceMaster invoiceMaster = new InvoiceMaster().GetInvoiceMasterById(int.Parse(id));
                    RequisitionMaster objRequisitionMaster = new RequisitionMaster().GetRequisitionMasterById(invoiceMaster.RequisitionId);
                    lblId.Text = objRequisitionMaster.Id.ToString();
                    rtxtInvoiceNo.Text = invoiceMaster.InvoiceNo;
                    rdropPDealer.SelectedValue = objRequisitionMaster.DealerId.ToString();
                    rdropCourier.SelectedValue = objRequisitionMaster.Courier;
                    rdropCS.SelectedValue = objRequisitionMaster.CSId.ToString();
                    rtxtAddress.Text = objRequisitionMaster.Address;
                    rtxtRequisitionNo.Text = objRequisitionMaster.RequisitionCode;
                    rdtRequisitionDate.SelectedDate = objRequisitionMaster.RequisitionDate;
                    lblStatus.Text = objRequisitionMaster.Status;
                    lblisInvoiceCreate.Text = objRequisitionMaster.IsInvoiceCreated.ToString();

                    Payment objPayment = new Payment().GetPaymentByRequisitionId(invoiceMaster.RequisitionId);

                    lblisnewPaymentEntry.Text = "false";

                    lblPaymentId.Text = objPayment.Id.ToString();
                   
                    lblisNewEntry.Text = "false";
                    this.LoadRequisitionDataFromDatabase(invoiceMaster.RequisitionId);

                }
            }
        }

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            try
            {
                string logoPath = Server.MapPath("Images") + @"\Elite Tec logo-01.png";

                int requisitionId = int.Parse(lblId.Text);

                MemoryStream pdfData = PrintInvoice.Print(requisitionId, logoPath);

                if (pdfData == null) return;

                Session["StreamData"] = pdfData;
                Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
    }
}