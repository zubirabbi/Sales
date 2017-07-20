using SUL.Bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PdfReport;
using PdfReport.Core;
using PdfReport.Templates;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class DealerItemTransaction : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  Company _company;
        //private  bool isNewEntry;
        private  DataTable dtDealerItemTransaction;
        //public  int searchBtn;

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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Dealer Transaction");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Dealer Transaction");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Dealer Transaction");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Dealer Transaction");
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

        private void LoadDealerTransactionListList()
        {
            try
            {
                if (lblsearchBtn.Text == "1")
                {
                    RadGridDealerTransaction.DataSource = dtDealerItemTransaction;
                    RadGridDealerTransaction.DataBind();
                }
                else
                {


                    dtDealerItemTransaction = new InvoiceMaster().GetInvoiceFromViewList();

                    RadGridDealerTransaction.DataSource = dtDealerItemTransaction;
                    RadGridDealerTransaction.DataBind();
                    
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load Item Ledger grid" + ex);
            }

        }
        private void LoadProduct()
        {
            try
            {
                DataTable dtproduct = new Product().GetProductFromViewList();

                rdropProduct.DataTextField = "ProInfo";
                rdropProduct.DataValueField = "Id";
                rdropProduct.DataSource = dtproduct;
                rdropProduct.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        private void LoadDealer()
        {
            try
            {
                DataTable dtDealer = new DealerInformation().GetAllDealerInformationView();

                rdropDealer.DataTextField = "DealerInfo";
                rdropDealer.DataValueField = "Id";
                rdropDealer.DataSource = dtDealer;
                rdropDealer.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
                throw;
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
            if (Session["dtDealerItemTransaction"] != null)
            {
                dtDealerItemTransaction = (DataTable)Session["dtDealerItemTransaction"];
            }
            if (!IsPostBack)
            {
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                lblsearchBtn.Text = "0";
                this.LoadProduct();
                this.LoadDealer();
                this.LoadDealerTransactionListList();

            }
        }

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            try
            {
                Company company = new Company().GetParentCompany();
                ReportHeader header = new ReportHeader()
                {
                    CompanyName = company.CompanyName,
                    Address = company.Address,
                    ReportTitle = "Dealer Item Transaction List Report",
                    LogoPath = ""
                };
                List<DetailStructure> detailDataList = new List<DetailStructure>()
                {
                    new DetailStructure
                    {
                        Slno = 1, FieldName = "InvoiceDate", DataType = "Datetime", Caption = "Invoice Date",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 2, FieldName = "InvoiceNo", DataType = "String", Caption = "Invoice No",
                        FieldWidth = 110f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "DealerName", DataType = "string", Caption = "Dealer Name",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 4, FieldName = "ProductName", DataType = "string", Caption = "Product Name",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 5, FieldName = "Quantity", DataType = "string", Caption = "Quantity",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 6, FieldName = "Color", DataType = "string", Caption = "Color",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 7, FieldName = "Price", DataType = "string", Caption = "Price",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 8, FieldName = "ItemTotal", DataType = "string", Caption = "Item Total",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, NeedTotal = true
                    },
                    new DetailStructure
                    {
                        Slno = 9, FieldName = "Discount", DataType = "string", Caption = "Discount",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, NeedTotal = true
                    },
                    new DetailStructure
                    {
                        Slno = 10, FieldName = "Total", DataType = "string", Caption = "Total",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, NeedTotal = true
                    }
                   
                };


                float[] widths = new float[] { 100f, 110f, 120f, 100f, 110f, 80f, 100f, 100f, 100f, 100f };

                //DateTime startDate = DateTime.Parse("Jan 01, 2014");
                //DateTime endDate = DateTime.Parse("Jan 01, 2016");

                //dtRequisitionMaster = new RequisitionMaster().GetRequisitionListFromViewList();

                ReportDetail reportDetail = new ReportDetail(detailDataList, dtDealerItemTransaction, 9, "English", widths,
                    "Verdana", true);

                PdfDocument doc = new PdfDocument("Dealer Item Transaction List Report", PageSizes.A4, "landscape", "English")
                {
                    Header = header,
                    Details = reportDetail,
                    ReportTemplate = PdfDocument.ReportType.Basic
                };
                doc.PageHeader = reportDetail.GetPageHeader(doc.ReportWidth);
                doc.ReportTemplate = PdfDocument.ReportType.Basic;
                MemoryStream pdfData = doc.WriteToStream();

                Session["StreamData"] = pdfData;
                Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            try
            {
                int dealerId;
                int productId;
                if (rdropDealer.SelectedIndex <= 0 && rdropProduct.SelectedIndex <= 0 && dtfromDate.SelectedDate == null)
                {
                    Alert.Show("Please Select one of the option.");
                    return;
                }
                else
                {
                    DateTime startDate = (DateTime)(dtfromDate.SelectedDate ?? DateTime.MinValue);
                    DateTime endDate = (DateTime)(dttoDate.SelectedDate ?? DateTime.MinValue);

                    dealerId = rdropDealer.SelectedIndex <= 0 ? 0 : int.Parse(rdropDealer.SelectedValue);
                    productId = rdropProduct.SelectedIndex <= 0 ? 0 : int.Parse(rdropProduct.SelectedValue);


                    dtDealerItemTransaction = new InvoiceMaster().SearchDealerItem(dealerId, productId, startDate,
                        endDate);

                    RadGridDealerTransaction.DataSource = dtDealerItemTransaction;
                    RadGridDealerTransaction.DataBind();

                    Session["dtDealerItemTransaction"] = dtDealerItemTransaction;


                    lblsearchBtn.Text = "1";
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void rdropProduct_OnDataBound(object sender, EventArgs e)
        {
            rdropProduct.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropDealer.Items.Insert(0, new RadComboBoxItem());
        }

        protected void RadGridDealerTransaction_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadDealerTransactionListList();
        }

        protected void RadGridDealerTransaction_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadDealerTransactionListList();
        }

        protected void RadGridDealerTransaction_OnItemCommand(object sender, GridCommandEventArgs e)
        {

        }
    }
}