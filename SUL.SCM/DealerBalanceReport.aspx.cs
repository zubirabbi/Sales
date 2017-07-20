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
    public partial class DealerBalanceReport : System.Web.UI.Page
    {

        private Users _user;
        private Company _company;

        private string _department;

        private AppPermission PermissionUser;

        private DataTable dtDealerInfo;


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
            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer Balance Report") : new AppFunctionality().GetAppFunctionalityId("Dealer Balance Report", int.Parse(lblsource.Text));
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

        private void loadDealerInformation()
        {
            try
            {
               dtDealerInfo = new DealerInformation().GetAllDealerInformationView();

               if (dtDealerInfo.Rows.Count == 0)
                {
                    RadGridDealerBalence.DataSource = new string[] { };
                    RadGridDealerBalence.DataBind();
                    Session["dtDealerInfo"] = null;
                    return;
                }
               RadGridDealerBalence.DataSource = dtDealerInfo;
                RadGridDealerBalence.DataBind();

                Session["dtDealerInfo"] = dtDealerInfo;
            }
            catch (Exception ex)
            {
                Alert.Show("somrthing is going wrong to Load Dealer grid." + ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblsource.Text = Request.QueryString["source"] ?? "0";
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

            if (Session["dtDealerInfo"] != null)
            {
                dtDealerInfo = (DataTable)Session["dtDealerInfo"];
            }
            else
            {
                dtDealerInfo = new DataTable();
            }
            if (!IsPostBack)
            {

                if (_user.EmployeeId != 0)
                {
                    Department objDepartment = new Department().GetEmployeeDepartment(_user.EmployeeId);
                    _department = objDepartment.DepartmentName;
                }
                this.loadDealerInformation();

            }
            if (IsValidPageForUser())
            {

                PermissionUser = (AppPermission)Session["PermissionUser"];
                if (!PermissionUser.IsView)
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }

            }
        }

        protected void RadGridDealer_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.loadDealerInformation();
        }

        protected void RadGridDealer_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.loadDealerInformation();
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
                    ReportTitle = "Dealer Information Report on "+DateTime.Now.ToString("MMM dd,yyyy+"),
                    LogoPath = ""
                };
                List<DetailStructure> detailDataList = new List<DetailStructure>()
                {
                    new DetailStructure
                    {
                        Slno = 1, FieldName = "DealerName", DataType = "string", Caption = "Dealer Name",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 2, FieldName = "DealerCode", DataType = "string", Caption = "Dealer Code",
                        FieldWidth = 110f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "TotalDebit", DataType = "int", Caption = "Total Debit",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 4, FieldName = "TotalCredit", DataType = "int", Caption = "Total Credit",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 5, FieldName = "Balance", DataType = "int", Caption = "Balance",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    }
                   
                };


                float[] widths = new float[] { 120f, 110f, 110f, 120f, 120f };

                ReportDetail reportDetail = new ReportDetail(detailDataList, dtDealerInfo, 9, "English", widths,
                    "Verdana", false);

                PdfDocument doc = new PdfDocument("Ledger statement", PageSizes.A4, "Landscape", "English")
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

        protected void btnExportToExcel_OnClick(object sender, EventArgs e)
        {
            DataTable dt = dtDealerInfo.Copy();

            ExportToExcel(dt);
        }

        public void ExportToExcel(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {

                    string attachment = "attachment; filename=DealerBalanceReport.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";

                    string tab = "";

                    Response.ContentEncoding = System.Text.Encoding.Unicode;
                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


                    StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    DateTime dttime = DateTime.Now;
                     
                    #region ReportHeader
                    Response.Write("Date");
                    tab = "\t";
                    Response.Write(tab + dttime.ToString("MMM dd,yyyy"));
                    Response.Write("\n");
                    #endregion

                     Response.Write("\n");
                    Response.Write("\n");
                    Response.Write("All Information");
                    Response.Write("\n");
                    //foreach (DataColumn dc in dt.Columns)
                    //{
                    Response.Write(sw.ToString());
                    Response.Write("Dealer Name");
                    tab = "\t";
                    Response.Write(tab + "Dealer Code");
                    tab = "\t";
                    Response.Write(tab + "Total Debit");
                    tab = "\t";
                    Response.Write(tab + "Total Credit");
                    tab = "\t";
                    Response.Write(tab + "Balance");
                    tab = "\t";

                    Response.Write("\n");

                    foreach (DataRow dr in dt.Rows)
                    {
                        tab = "";

                        Response.Write(tab + dr["DealerName"].ToString());
                        tab = "\t";
                        Response.Write(tab +  dr["DealerCode"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["TotalDebit"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["TotalCredit"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Balance"].ToString());
                        tab = "\t";
                        
                        Response.Write("\n");
                    }
                    Response.End();
                    Response.Flush();
                    sw.Close();
                    sw.Dispose();
                }

               
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
    }
}