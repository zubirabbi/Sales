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
    public partial class RequisitionHistory : System.Web.UI.Page
    {
        private UserRole _role;
        private Users _user;
        private Company _company;
        private DataTable dtRequisitionMaster;
        private AppPermission PermissionUser;
        private string _department;
        private int _source;
      
        private bool searchStatus;

        private bool IsValidSession()
        {
            if (Session["user"] == null)
            {
                return false;
            }

            _user = (Users)Session["user"];
            _company = (Company)Session["company"];
            _department = Session["Department"].ToString();
            _role = (UserRole)Session["Role"];

            return _user.Id != 0;
        }
        private bool IsValidPageForUser()
        {
           
            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Requisition History") : new AppFunctionality().GetAppFunctionalityId("Requisition History", int.Parse(lblsource.Text));
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
                rdropArea.SelectedIndex = 0;
            }

            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load region." + ex);
            }
        }
        private void LoadDealer()
        {
            try
            {
                DataTable dtDealerInfo = new DealerInformation().GetAllDealerInformationView();

                rdropPDealer.DataTextField = "DealerInfo";
                rdropPDealer.DataValueField = "Id";
                rdropPDealer.DataSource = dtDealerInfo;
                rdropPDealer.DataBind();

                if (dtDealerInfo.Rows.Count == 2)
                    rdropPDealer.SelectedIndex = 1;
                else
                    rdropPDealer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
            }
        }

        private void LoadRequisitionList()
        {
            try
            {
                int dealerId;
                int regionId;
                int areaId;

                DateTime prevDate = (DateTime)(dtfromDate.SelectedDate ?? DateTime.MinValue);
                DateTime todays = (DateTime)(dttoDate.SelectedDate ?? DateTime.MinValue);

               
                regionId = rdropRegion.SelectedIndex <= 0 ? 0 : int.Parse(rdropRegion.SelectedValue);
                areaId = rdropArea.SelectedIndex <= 0 ? 0 : int.Parse(rdropArea.SelectedValue);
                dealerId = rdropPDealer.SelectedIndex <= 0 ? 0 : int.Parse(rdropPDealer.SelectedValue);

                dtRequisitionMaster = new RequisitionMaster().SearchRequisition(regionId, areaId, dealerId, prevDate, todays, "");
                if (dtRequisitionMaster.Rows.Count == 0)
                {
                    RadGridRequisition.DataSource = new string[] { };
                    RadGridRequisition.DataBind();

                    return;
                }


                RadGridRequisition.DataSource = dtRequisitionMaster;
                RadGridRequisition.DataBind();
                lblsearchBtn.Text = "1";

                Session["RadGridRequisition"] = dtRequisitionMaster;

                Session["dtRequisitionMaster"] = dtRequisitionMaster;
                RadGridRequisition.DataSource = dtRequisitionMaster;
                RadGridRequisition.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Requisition data." + ex);
            }
        }

        private void LoadDealerInfo(int area)
        {
            try
            {
                DataTable dtDealerInfo = new DealerInformation().GetAllDealerInformationViewByArea(area);

                rdropPDealer.DataTextField = "DealerInfo";
                rdropPDealer.DataValueField = "Id";
                rdropPDealer.DataSource = dtDealerInfo;
                rdropPDealer.DataBind();

                if (dtDealerInfo.Rows.Count == 2)
                    rdropPDealer.SelectedIndex = 1;
                else
                    rdropPDealer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
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

            if (!IsValidPageForUser())
            {
                Alert.Show("Sorry, You Don't Have permission to access this page.");
                Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx", false);
            }

            if (_user.EmployeeId != 0)
            {
                Department objDepartment = new Department().GetEmployeeDepartment(_user.EmployeeId);
                _department = objDepartment.DepartmentName;
            }

            else
                _department = "All";

            if (Session["dtRequisitionMaster"] != null)
            {
                dtRequisitionMaster = (DataTable)Session["dtRequisitionMaster"];
            }

            if (!IsPostBack)
            {
                
                lblsearchBtn.Text = "0";
                this.LoadRegion();
                this.LoadDealer();
                this.LoadRequisitionList();
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

        protected void RadGridRequisition_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadRequisitionList();
        }

        protected void RadGridRequisition_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadRequisitionList();
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            this.LoadRequisitionList();
        }

        protected void rdropPDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropPDealer.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropRegion_OnDataBound(object sender, EventArgs e)
        {
            rdropRegion.Items.Insert(0, new ListItem());
        }

        protected void rdropRegion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadArea(int.Parse(rdropRegion.SelectedValue));
        }

        protected void rdropArea_OnDataBound(object sender, EventArgs e)
        {
            rdropArea.Items.Insert(0, new ListItem());
        }

        protected void rdropArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadDealerInfo(int.Parse(rdropArea.SelectedValue));
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            DataTable dt = dtRequisitionMaster.Copy();

            ExportToExcel(dt);
        }

        public void ExportToExcel(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {

                    string attachment = "attachment; filename=RequisitionHistory.xls";
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
                    if (rdropPDealer.SelectedIndex > 0)
                    {
                        Response.Write("\n");
                        Response.Write("To Date");
                        tab = "\t";
                        Response.Write(tab + rdropPDealer.SelectedItem.Text);
                        Response.Write("\n");
                    }
                    #endregion

                    #region ReportSummeryForDealer
                    decimal TQ = 0;
                    decimal T = 0;
                    decimal Dis = 0;

                    Response.Write("\n");
                    Response.Write("\n");
                    Response.Write("Requisition History");
                    Response.Write("\n");
                    //foreach (DataColumn dc in dt.Columns)
                    //{
                    Response.Write(sw.ToString());
                    Response.Write("Requisition Code");
                    tab = "\t";
                    Response.Write(tab + "Requisition Date");
                    tab = "\t";
                    Response.Write(tab + "Dealer Code");
                    tab = "\t";
                    Response.Write(tab + "Dealer Name");
                    tab = "\t";
                    Response.Write(tab + "Payment Amount");
                    tab = "\t";
                    Response.Write(tab + "Reference No");
                    tab = "\t";
                    Response.Write(tab + "Employee Name");
                    tab = "\t";
                    Response.Write(tab + "Update By");
                    tab = "\t";
                    Response.Write(tab + "Status");
                    tab = "\t";
                    Response.Write(tab + "Remarks");
                    tab = "\t";

                    Response.Write("\n");

                    foreach (DataRow dr in dt.Rows)
                    {
                        tab = "";

                        Response.Write(tab + dr["RequisitionCode"].ToString());
                        tab = "\t";
                        DateTime Rdt = DateTime.Parse(dr["RequisitionDate"].ToString());

                        Response.Write(tab + Rdt.ToString("dd/MM/yyyy"));
                        tab = "\t";
                        Response.Write(tab + dr["DealerCode"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["DealerName"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["PaymentAmount"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["ReferenceNo"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["EmployeeName"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["UpdateBy"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Status"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Remarks"].ToString());
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
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }

        }

        protected void btnPrintAll_OnClick(object sender, EventArgs e)
        {
            try
            {
                Company company = new Company().GetParentCompany();
                ReportHeader header = new ReportHeader()
                {
                    CompanyName = company.CompanyName,
                    Address = company.Address,
                    ReportTitle = "Requisition List Report",
                    LogoPath = ""
                };
                List<DetailStructure> detailDataList = new List<DetailStructure>()
                {
                    new DetailStructure
                    {
                        Slno = 1, FieldName = "DealerCode", DataType = "string", Caption = "Dealer Code",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 2, FieldName = "Address", DataType = "String", Caption = "Address",
                        FieldWidth = 110f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "Val", DataType = "string", Caption = "Courier Name",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 4, FieldName = "RequisitionDate", DataType = "DateTime", Caption = "Requisition Date",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 5, FieldName = "EmployeeName", DataType = "string", Caption = "Employee Name",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 6, FieldName = "Status", DataType = "string", Caption = "Status",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    }
                   
                };


                float[] widths = new float[] { 100f, 110f, 100f, 100f, 110f, 80f };

                ReportDetail reportDetail = new ReportDetail(detailDataList, dtRequisitionMaster, 9, "English", widths,
                    "Verdana", false);

                PdfDocument doc = new PdfDocument("Requisition List", PageSizes.A4, "Landscape", "English")
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