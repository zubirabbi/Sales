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
using System.Text;

namespace SUL.SCM
{
    public partial class DealerLedgerListView : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        private Company _company;
        private DataTable dtDealerledger;


        private static int _source;

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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer Ledger") : new AppFunctionality().GetAppFunctionalityId("Dealer Ledger", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer Ledger") : new AppFunctionality().GetAppFunctionalityId("Dealer Ledger", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer Ledger") : new AppFunctionality().GetAppFunctionalityId("Dealer Ledger", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer Ledger") : new AppFunctionality().GetAppFunctionalityId("Dealer Ledger", int.Parse(lblsource.Text));
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
        private void LoadDealerLedger()
        {
            try
            {
                if (int.Parse(lblsearchBtn.Text) == 1)
                {
                    RadGridDealerLedger.DataSource = dtDealerledger;
                    RadGridDealerLedger.DataBind();

                    Session["dtDealerledger"] = dtDealerledger;
                }
                else
                {
                    dtDealerledger = new DealerLedger().GetDealerLedgerFromViewList();

                    RadGridDealerLedger.DataSource = dtDealerledger;
                    RadGridDealerLedger.DataBind();

                    Session["dtDealerledger"] = dtDealerledger;
                }


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
            lblsource.Text = Request.QueryString["source"] != null ? Request.QueryString["source"] : "0";


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
            if (Session["dtDealerledger"] != null)
            {
                dtDealerledger = (DataTable)Session["dtDealerledger"];
            }
            if (!IsPostBack)
            {
                lblsearchBtn.Text = "0";
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                this.LoadDealerLedger();
                this.LoadDealer();
            }
        }

        protected void RadGridDealerLedger_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadDealerLedger();
        }

        protected void RadGridDealerLedger_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadDealerLedger();
        }

        protected void RadGridDealerLedger_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            //throw new NotImplementedException();
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
                    ReportTitle = "Dealer Ledger List Report",
                    LogoPath = ""
                };
                List<DetailStructure> detailDataList = new List<DetailStructure>()
                {
                    new DetailStructure
                    {
                        Slno = 1, FieldName = "DealerName", DataType = "string", Caption = "Supplier Name",
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
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, NeedTotal = true
                    },
                    new DetailStructure
                    {
                        Slno = 6, FieldName = "Debit", DataType = "string", Caption = "Debit",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, NeedTotal = true
                    },
                    new DetailStructure
                    {
                        Slno = 7, FieldName = "Cradit", DataType = "string", Caption = "Cradit",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, NeedTotal = true
                    },
                    new DetailStructure
                    {
                        Slno = 8, FieldName = "ClosingBalance", DataType = "string", Caption = "Closing Balance",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT, NeedTotal = true
                    },
                     new DetailStructure
                    {
                        Slno = 8, FieldName = "SourceId", DataType = "string", Caption = "Source Id",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    }
                   
                };


                float[] widths = new float[] { 100f, 110f, 100f, 100f, 110f, 100f, 100f, 100f, 80f };

                //DateTime startDate = DateTime.Parse("Jan 01, 2014");
                //DateTime endDate = DateTime.Parse("Jan 01, 2016");

                //dtRequisitionMaster = new RequisitionMaster().GetRequisitionListFromViewList();

                ReportDetail reportDetail = new ReportDetail(detailDataList, dtDealerledger, 9, "English", widths,
                    "Verdana", true);

                PdfDocument doc = new PdfDocument("Dealer Ledger List", PageSizes.A4, "Potrait", "English")
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
                if (rdropDealer.SelectedIndex <= 0 && dtfromDate.SelectedDate == null)
                {
                    Alert.Show("Please Select one of the option.");
                    return;
                }
                DateTime startDate = (DateTime)(dtfromDate.SelectedDate ?? DateTime.MinValue);
                DateTime endDate = (DateTime)(dttoDate.SelectedDate ?? DateTime.MinValue);

                dealerId = rdropDealer.SelectedIndex <= 0 ? 0 : int.Parse(rdropDealer.SelectedValue);


                dtDealerledger = new DealerLedger().SearchDealerItem(dealerId, startDate, endDate);

                if (rdropDealer.SelectedIndex > 0)
                {
                    showSummary.Visible = true;
                    decimal closingBalance = decimal.Parse(dtDealerledger.Rows[0]["ClosingBalance"].ToString());
                    decimal openingBalance = decimal.Parse(dtDealerledger.Rows[dtDealerledger.Rows.Count - 1]["OpeningBalance"].ToString());

                    decimal totalDebit = decimal.Parse(dtDealerledger.Compute("sum(Debit)", "").ToString());
                    decimal totalCredit = decimal.Parse(dtDealerledger.Compute("sum(Cradit)", "").ToString());

                    lblOpening.InnerText = openingBalance.ToString();
                    lblClosing.InnerText = closingBalance.ToString();
                    lblcredit.InnerText = totalCredit.ToString();
                    lbldebit.InnerText = totalDebit.ToString();


                }

                RadGridDealerLedger.DataSource = dtDealerledger;
                RadGridDealerLedger.DataBind();
                Session["dtDealerledger"] = dtDealerledger;
                lblsearchBtn.Text = "1";
                //this.LoadDealerLedger();
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

            DataTable dt = dtDealerledger.Copy();
            if (rdropDealer.SelectedIndex <= 0)
                ExportToExcelAllData(dt);
            else
                ExportToExcel(dt);

        }
        public void ExportToExcel(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                string attachment = "attachment; filename=Dealer Ledger.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";

                string tab = "";

                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);


                Response.Write(sw.ToString());


                Response.Write("Dealer Name");
                tab = "\t";
                Response.Write(tab + dt.Rows[0]["DealerName"].ToString());
                Response.Write("\n");

                Response.Write("Opening Balance");
                tab = "\t";
                Response.Write(tab + lblOpening.InnerText);
                Response.Write("\n");

                Response.Write("Debit");
                Response.Write(tab + lbldebit.InnerText);
                Response.Write("\n");

                Response.Write("Credit");
                tab = "\t";
                Response.Write(tab + lblcredit.InnerText);
                Response.Write("\n");

                Response.Write("Closing Balance");
                tab = "\t";
                Response.Write(tab + lblClosing.InnerText);
                Response.Write("\n");


                Response.Write("\n");
                //foreach (DataColumn dc in dt.Columns)
                //{
                Response.Write(sw.ToString());
                Response.Write("Transaction Date");
                tab = "\t";
                Response.Write(tab + "Transaction Type");
                tab = "\t";
                Response.Write(tab + "Opening Balance");
                tab = "\t";
                Response.Write(tab + "Debit");
                tab = "\t";
                Response.Write(tab + "Credit");
                tab = "\t";
                Response.Write(tab + "Closing Balance");
                tab = "\t";
                Response.Write(tab + "Source No");
                tab = "\t";

                Response.Write("\n");


                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";

                    Response.Write(tab + dr["TransactionDate"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["TransactionType"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["OpeningBalance"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["Debit"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["Cradit"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["ClosingBalance"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["SourceNo"].ToString());
                    tab = "\t";


                    Response.Write("\n");
                }

                Response.End();

            }
        }

        public void ExportToExcelAllData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                string attachment = "attachment; filename=DealerLedger.xls";
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

                Response.Write("Dealer Ledger List");
                Response.Write("\n");
                //foreach (DataColumn dc in dt.Columns)
                //{
                Response.Write(sw.ToString());
                Response.Write("Dealer Name");
                tab = "\t";
                Response.Write(tab + "Transaction Type");
                tab = "\t";
                Response.Write(tab + "Transaction Date");
                tab = "\t";
                Response.Write(tab + "Opening Balance");
                tab = "\t";
                Response.Write(tab + "Debit");
                tab = "\t";
                Response.Write(tab + "Credit");
                tab = "\t";
                Response.Write(tab + "Closing Balance");
                tab = "\t";
                Response.Write(tab + "Source No");
                tab = "\t";

                Response.Write("\n");

                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";

                    Response.Write(tab + dr["DealerName"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["TransactionType"].ToString());
                    tab = "\t";
                    DateTime Rdt = DateTime.Parse(dr["TransactionDate"].ToString());
                    Response.Write(tab + Rdt.ToString("dd/MM/yyyy"));
                    tab = "\t";
                    Response.Write(tab + dr["OpeningBalance"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["Debit"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["Cradit"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["ClosingBalance"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["SourceNo"].ToString());
                    tab = "\t";



                    Response.Write("\n");
                }
                Response.End();
                Response.Flush();
                sw.Close();
                sw.Dispose();

            }

                #endregion
        }

        protected void rdropDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropDealer.Items.Insert(0, new RadComboBoxItem());
        }
    }
}