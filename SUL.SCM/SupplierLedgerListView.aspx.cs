using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PdfReport;
using PdfReport.Core;
using PdfReport.Templates;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class SupplierLedgerListView : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        private Company _company;
        private DataTable dtSupplierledger;
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier Ledger");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier Ledger");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier Ledger");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier Ledger");
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
        private void LoadSupplierLedger()
        {
            try
            {
                dtSupplierledger = new SupplierLedger().GetSupplierLedgerFromViewList();

                RadGridsupplierLedger.DataSource = dtSupplierledger;
                RadGridsupplierLedger.DataBind();
                
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
            if (Session["dtSupplierledger"] != null)
            {
                dtSupplierledger = (DataTable)Session["dtSupplierledger"];
            }
            if (!IsPostBack)
            {
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                this.LoadSupplierLedger();
            }
        }

        protected void RadGridsupplierLedger_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadSupplierLedger();
        }

        protected void RadGridsupplierLedger_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadSupplierLedger();
        }

        protected void RadGridsupplierLedger_OnItemCommand(object sender, GridCommandEventArgs e)
        {

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
                    ReportTitle = "Supplier Ledger List Report",
                    LogoPath = ""
                };
                List<DetailStructure> detailDataList = new List<DetailStructure>()
                {
                    new DetailStructure
                    {
                        Slno = 1, FieldName = "Name", DataType = "string", Caption = "Supplier Name",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 2, FieldName = "TransactionType", DataType = "String", Caption = "Transaction Type",
                        FieldWidth = 110f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "TransactionDate", DataType = "DateTime", Caption = "Transaction Date",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 4, FieldName = "SourceId", DataType = "string", Caption = "Source Id",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 5, FieldName = "OpeningBalance", DataType = "string", Caption = "Opening Balance",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 6, FieldName = "Debit", DataType = "string", Caption = "Debit",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 7, FieldName = "Cradit", DataType = "string", Caption = "Cradit",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 8, FieldName = "ClosingBalance", DataType = "string", Caption = "Closing Balance",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    }
                   
                };


                float[] widths = new float[] { 100f, 110f, 100f, 100f, 110f, 100f, 100f,100f };

                //DateTime startDate = DateTime.Parse("Jan 01, 2014");
                //DateTime endDate = DateTime.Parse("Jan 01, 2016");

                //dtRequisitionMaster = new RequisitionMaster().GetRequisitionListFromViewList();

                ReportDetail reportDetail = new ReportDetail(detailDataList, dtSupplierledger, 9, "English", widths,
                    "Verdana", false);

                PdfDocument doc = new PdfDocument("Supplier Ledger List", PageSizes.A4, "Potrait", "English")
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
    }
}