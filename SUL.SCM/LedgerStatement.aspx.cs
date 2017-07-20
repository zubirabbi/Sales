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
    public partial class LedgerStatement : System.Web.UI.Page
    {
        private Users _user;
        private Company _company;

        private string _department;

        private AppPermission PermissionUser;

        private DataTable dtLedgerStatement;


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

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Ledger Statement") : new AppFunctionality().GetAppFunctionalityId("Ledger Statement", int.Parse(lblsource.Text));
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
        private void LoadLedgerStatement()
        {
            try
            {
                int dealerId;
                if (rdropDealer.SelectedIndex <= 0 && dtfromDate.SelectedDate == null)
                {
                    Alert.Show("Please Select one of the option.");
                    return;
                }
                DateTime startDate = dtfromDate.SelectedDate==null?DateTime.MinValue:DateTime.Parse(dtfromDate.SelectedDate.ToString());
                DateTime endDate = dttoDate.SelectedDate == null ? DateTime.MinValue : DateTime.Parse(dttoDate.SelectedDate.ToString());

                dealerId = rdropDealer.SelectedIndex <= 0 ? 0 : int.Parse(rdropDealer.SelectedValue);


                dtLedgerStatement = new DealerInformation().SearchLedgerStatement(dealerId, startDate, endDate);


                RadGridLedgerStatement.DataSource = dtLedgerStatement;
                RadGridLedgerStatement.DataBind();
                Session["dtLedgerStatement"] = dtLedgerStatement;
                lblsearchBtn.Text = "1";
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
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

            if (Session["dtLedgerStatement"] != null)
            {
                dtLedgerStatement = (DataTable)Session["dtLedgerStatement"];
            }
            else
            {
                dtLedgerStatement = new DataTable();
            }
            if (!IsPostBack)
            {

                if (_user.EmployeeId != 0)
                {
                    Department objDepartment = new Department().GetEmployeeDepartment(_user.EmployeeId);
                    _department = objDepartment.DepartmentName;
                }

                this.LoadDealer();

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

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            LoadLedgerStatement();
        }
        protected void btnExportToExcel_OnClick(object sender, EventArgs e)
        {
            DataTable dt = dtLedgerStatement.Copy();

            ExportToExcel(dt);
        }

        public void ExportToExcel(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {

                    string attachment = "attachment; filename=LedgerStatement.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";

                    string tab = "";

                    Response.ContentEncoding = System.Text.Encoding.Unicode;
                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


                    StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    #region ReportHeader

                    if (dtfromDate.SelectedDate != null)
                    {
                        Response.Write("From Date");
                        tab = "\t";
                        Response.Write(tab + dtfromDate.SelectedDate);
                        Response.Write("\n");
                    }
                    if (dttoDate.SelectedDate != null)
                    {
                        Response.Write("From Date");
                        tab = "\t";
                        Response.Write(tab + dttoDate.SelectedDate);
                        Response.Write("\n");
                    }
                    if (rdropDealer.SelectedIndex > 0)
                    {
                        Response.Write("\n");
                        Response.Write("To Date");
                        tab = "\t";
                        Response.Write(tab + rdropDealer.SelectedItem.Text);
                        Response.Write("\n");
                    }
                    #endregion

                    #region ReportSummeryForDealer
                    decimal TQ = 0;
                    decimal T = 0;
                    decimal Dis = 0;

                    Response.Write("\n");
                    Response.Write("\n");
                    Response.Write("All Information");
                    Response.Write("\n");
                    //foreach (DataColumn dc in dt.Columns)
                    //{
                    Response.Write(sw.ToString());
                    Response.Write("Dealer Name");
                    tab = "\t";
                    Response.Write(tab + "Transaction Date");
                    tab = "\t";
                    Response.Write(tab + "Enter By");
                    tab = "\t";
                    Response.Write(tab + "Transaction Type");
                    tab = "\t";
                    Response.Write(tab + "Invoice No");
                    tab = "\t";
                    Response.Write(tab + "Model No");
                    tab = "\t";
                    Response.Write(tab + "Product Name");
                    tab = "\t";
                    Response.Write(tab + "Quantity");
                    tab = "\t";
                    Response.Write(tab + "Bank Name");
                    tab = "\t";
                    Response.Write(tab + "Branch Name");
                    tab = "\t";
                    Response.Write(tab + "Opening Balance");
                    tab = "\t";
                    Response.Write(tab + "Debit");
                    tab = "\t";
                    Response.Write(tab + "Cradit");
                    tab = "\t";
                    Response.Write(tab + "Closing Balance");
                    tab = "\t";
                    Response.Write(tab + "Remarks");
                    tab = "\t";

                    Response.Write("\n");

                    foreach (DataRow dr in dt.Rows)
                    {
                        tab = "";

                        Response.Write(tab + dr["DealerName"].ToString());
                        tab = "\t";
                        DateTime Rdt = DateTime.Parse(dr["TransactionDate"].ToString());

                        Response.Write(tab + Rdt.ToString("dd/MM/yyyy"));
                        tab = "\t";
                        Response.Write(tab + dr["EnterBy"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["TransactionType"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["InvoiceNo"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["ModelNo"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["ProductName"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Quantity"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["BankName"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Branch"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["OpeningBalance"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Debit"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Cradit"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["ClosingBalance"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Remarks"].ToString());
                        tab = "\t";

                        TQ += dr["Quantity"].ToString() == string.Empty ? 0 : decimal.Parse(dr["Quantity"].ToString());
                        T += dr["Debit"].ToString() == string.Empty ? 0 : decimal.Parse(dr["Debit"].ToString());
                        Dis += dr["Cradit"].ToString() == string.Empty ? 0 : decimal.Parse(dr["Cradit"].ToString());

                        Response.Write("\n");
                    }
                    Response.Write("\n");
                    Response.Write("Total Quantity");
                    tab = "\t";

                    Response.Write(tab + TQ);
                    tab = "\t";
                    Response.Write("\n");
                    Response.Write("Total Debit");
                    tab = "\t";
                    Response.Write(tab + Dis);
                    tab = "\t";
                    Response.Write("\n");
                    Response.Write("Total Credit");
                    tab = "\t";
                    Response.Write(tab + T);
                    tab = "\t";

                    Response.End();
                    Response.Flush();
                    sw.Close();
                    sw.Dispose();
                }

                    #endregion

            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
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
                    ReportTitle = "Ledger Statement Report",
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
                        Slno = 2, FieldName = "TransactionDate", DataType = "Datetime", Caption = "Transaction Date",
                        FieldWidth = 110f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "EnterBy", DataType = "string", Caption = "Enter By",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 4, FieldName = "TransactionType", DataType = "string", Caption = "Transaction Type",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 5, FieldName = "InvoiceNo", DataType = "string", Caption = "Invoice No",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 6, FieldName = "ModelNo", DataType = "string", Caption = "Model No",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 7, FieldName = "ProductName", DataType = "string", Caption = "Product Name",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 8, FieldName = "Quantity", DataType = "int", Caption = "Quantity",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 9, FieldName = "BankName", DataType = "string", Caption = "Bank Name",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 10, FieldName = "Branch", DataType = "string", Caption = "Branch",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 11, FieldName = "OpeningBalance", DataType = "int", Caption = "OpeningBalance",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 12, FieldName = "Debit", DataType = "int", Caption = "Debit",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 13, FieldName = "Cradit", DataType = "int", Caption = "Cradit",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 14, FieldName = "ClosingBalance", DataType = "int", Caption = "ClosingBalance",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 15, FieldName = "Remarks", DataType = "string", Caption = "Remarks",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    }

                   
                };


                float[] widths = new float[] { 70f, 70f, 70f, 70f, 70f, 70f, 70f, 70f, 70f, 70f, 70f, 70f,70f,70f,70f };

                ReportDetail reportDetail = new ReportDetail(detailDataList, dtLedgerStatement, 9, "English", widths,
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
        protected void RadGridLedgerStatement_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadLedgerStatement();
        }

        protected void RadGridLedgerStatement_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadLedgerStatement();
        }

        protected void rdropDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropDealer.Items.Insert(0, new RadComboBoxItem());
        }




    }
}