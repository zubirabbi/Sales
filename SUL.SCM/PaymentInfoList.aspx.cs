using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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
    public partial class PaymentInfoList : System.Web.UI.Page
    {
        private UserRole _role;
        private Users _user;
        private bool isNewEntry;
        private static Company _company;
        public int searchBtn;
        public DataTable dtPayment;
        private string _department;
        private int _source;

        private List<DataFilterSetup> setupList;
        private List<ListTable> statusList;

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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Payment List") : new AppFunctionality().GetAppFunctionalityId("Payment List", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Payment List") : new AppFunctionality().GetAppFunctionalityId("Payment List", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Payment List") : new AppFunctionality().GetAppFunctionalityId("Payment List", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Payment List") : new AppFunctionality().GetAppFunctionalityId("Payment List", int.Parse(lblsource.Text));
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
        private void LoadStatusListBox()
        {
            try
            {
                setupList = new DataFilterSetup().GetSetupByType(0, _role.Id, 2);
                statusList = new DataFilterSetup().GetPaymetStatusList(0, _role.Id, 2);
                if (statusList.Count == 0)
                {
                    setupList = new DataFilterSetup().GetSetupByType(_user.Id, 0, 2);
                    statusList = new DataFilterSetup().GetPaymetStatusList(_user.Id, 0, 2);
                }

                if (statusList.Count == 0)
                {
                    Alert.Show("Sorry this users payment accessebility setup is not done.");
                    return;
                }

                lstStatus.DataSource = statusList;
                lstStatus.DataTextField = "ListValue";
                lstStatus.DataValueField = "ListId";
                lstStatus.DataBind();

                foreach (RadListBoxItem item in lstStatus.Items)
                {
                    int id = int.Parse(item.Value);

                    DataFilterSetup setup = setupList.Find(x => x.StatusId == id);
                    if (setup.IsLoadInitially)
                        item.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
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
        private void LoadpaymentGride()
        {
            try
            {
                StringBuilder statusCondition = new StringBuilder();
                foreach (RadListBoxItem item in lstStatus.CheckedItems)
                {
                    if (statusCondition.Length == 0)
                        statusCondition.Append(" where (status = '" + item.Text + "'");
                    else
                        statusCondition.Append(" OR status = '" + item.Text + "'");
                }
                statusCondition.Append(")");

                string strStatusCondition = statusCondition.ToString();


                int dealerId;
                int regionId;
                int areaId;

                DateTime prevDate = (DateTime)(dtfromDate.SelectedDate ?? DateTime.MinValue);
                DateTime todays = (DateTime)(dttoDate.SelectedDate ?? DateTime.MinValue);
                regionId = rdropRegion.SelectedIndex <= 0 ? 0 : int.Parse(rdropRegion.SelectedValue);
                areaId = rdropArea.SelectedIndex <= 0 ? 0 : int.Parse(rdropArea.SelectedValue);
                dealerId = rdropPDealer.SelectedIndex <= 0 ? 0 : int.Parse(rdropPDealer.SelectedValue);


                string whereCondition = string.Empty;

                if (_department != "All")
                {
                    //payment Verification
                    if (_department.ToLower().Contains("sales"))
                    {
                        RadGridPayment.Columns[20].Visible = false;
                    }
                    else if (_department.ToLower().Contains("account"))
                    {
                        RadGridPayment.Columns[20].Visible = true;
                        //whereCondition += " Where IsVarified='false'";
                    }
                    else if (_department.ToLower().Contains("logistic"))
                    {
                        RadGridPayment.Columns[20].Visible = false;
                    }
                }
                dtPayment = new Payment().SearchPayment(regionId, areaId, dealerId, prevDate, todays, strStatusCondition);
                if (dtPayment.Rows.Count == 0)
                {
                    RadGridPayment.DataSource = new string[] { };
                    RadGridPayment.DataBind();

                    return;
                }
                

                RadGridPayment.DataSource = dtPayment;
                GetVarifiedPayment(dtPayment);
                RadGridPayment.DataBind();
                Session["dtPayment"] = dtPayment;
                //}
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        public static void GetVarifiedPayment(DataTable PaymentList)
        {
            DataColumn col = new DataColumn();
            col.ColumnName = "varifiedStatus";

            PaymentList.Columns.Add(col);
            foreach (DataRow dr in PaymentList.Rows)
            {

                if (bool.Parse(dr["IsVarified"].ToString()) == false)
                {
                    dr["varifiedStatus"] = "Images/NotVatified.png";
                }
                else
                {
                    dr["varifiedStatus"] = "Images/verified.png";
                }
                DataTable paymentList = PaymentList;
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

            if (Session["dtPayment"] != null)
            {
                dtPayment = (DataTable)Session["dtPayment"];
            }
            if (!IsPostBack)
            {
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                lblsearchBtn.Text = "0";
                this.LoadRegion();
                this.LoadDealer();
                this.LoadStatusListBox();
                this.LoadpaymentGride();
            }
        }
        protected void RadGridPayment_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadpaymentGride();
        }

        protected void RadGridPayment_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadpaymentGride();
        }
        protected void RadGridPayment_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;
            Payment objPayment = new Payment();
            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;

                int id = int.Parse(item["colId"].Text);
                delete = objPayment.DeletePaymentById(id);
                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    this.LoadpaymentGride();
                }

                this.LoadpaymentGride();
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    LinkButton linkedit = (LinkButton)sender;
                    string[] Id = linkedit.CommandArgument.ToString().Split(';');
                    int paymentId = int.Parse(Id[0]);
                    Payment payment = new Payment().GetPaymentById(paymentId);
                    if (payment.Id == 0)
                    {
                        Alert.Show("Not a valid payment information.");
                        return;
                    }

                    if (payment.Status.ToLower() == "verified")
                    {
                        Alert.Show("This payment is already verified, you cannot cancel it now.");
                        return;
                    }
                    if (payment.Status.ToLower() == "cancelled")
                    {
                        Alert.Show("This payment is already cancelled.");
                        return;
                    }

                    int success = new Payment().ChangeStatus(paymentId, "Cancelled", _user.Id);
                    if (success == 0)
                    {
                        Alert.Show("Payment cancelation failed.");
                        return;
                    }

                    Alert.Show("Successfully cancelled the payment.");
                    this.LoadpaymentGride();

                }
            }
            catch (Exception ex)
            {
                Alert.Show("Error Occur " + ex.Message);
            }
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            LinkButton linkedit = (LinkButton)sender;
            string[] Id = linkedit.CommandArgument.ToString().Split(';');

            int id = int.Parse(Id[0]);
            Payment objPayment = new Payment().GetPaymentById(id);

            if (objPayment.Status.ToLower() == "cancelled")
            {
                Alert.Show("Payment is already cancelled.");
            }
            else
            {

                if (objPayment.IsVarified == false)
                {
                    Response.Redirect("Paymentinfo.aspx?Id=" + id);
                }
                else
                {
                    Alert.Show("Payment is already varified.");
                }
            }


        }

        protected void btnVerified_OnClick(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    LinkButton linkedit = (LinkButton)sender;
                    string[] Id = linkedit.CommandArgument.ToString().Split(';');

                    int id = int.Parse(Id[0]);

                    Payment objPayment = new Payment().GetPaymentById(id);
                    long DealerId = objPayment.DealerId;
                    DealerInformation objInformation = new DealerInformation().GetDealerInformationById(DealerId);

                    decimal openingBalance = objInformation.Balance;
                    string sourceId = objPayment.MoneyReceiptNo;
                    decimal cradit = objPayment.Amount;

                    if (objPayment.Status.ToLower() == "cancelled")
                    {
                        Alert.Show("Payment information was already cancelled.");
                        return;
                    }

                    if (objPayment.IsVarified == false)
                    {
                        int update;
                        update = new Payment().SetvarifiedPayment(id, _user.Id, true);

                        if (update == 0)
                        {
                            Alert.Show("Payment information was not verified.");
                        }
                        else
                        {
                            DealerLedger objdealerLedger = new DealerLedger();

                            objdealerLedger.DealerId = int.Parse(DealerId.ToString());
                            objdealerLedger.TransactionType = "Payment";
                            objdealerLedger.TransactionDate = DateTime.Now;
                            objdealerLedger.SourceId = id.ToString();
                            objdealerLedger.UserId = _user.Id;
                            objdealerLedger.OpeningBalance = openingBalance;
                            objdealerLedger.Debit = 0;
                            objdealerLedger.Cradit = cradit;
                            objdealerLedger.SourceNo = sourceId;
                            objdealerLedger.Remarks = "";

                            int success;
                            success = objdealerLedger.InsertDealerLedger();
                            if (success == 0)
                            {
                                Alert.Show("Dealer ledger update failed for the payment.");


                            }
                            else
                            {
                                DealerInformation objdinfo = new DealerInformation();

                                objdinfo.Id = int.Parse(DealerId.ToString());
                                objdinfo.TotalDebit = 0;
                                objdinfo.TotalCredit = cradit;

                                int dInfosuccess = objdinfo.UpdateDealerInformationfordealerLedger();

                                if (dInfosuccess == 0)
                                {
                                    Alert.Show("Payment is not verified .");
                                }

                                Alert.Show("Payment is verified succesfully.");

                                this.LoadpaymentGride();
                            }


                            //this.LoadpaymentGride();
                        }
                    }
                    else
                    {
                        Alert.Show("Payment was already verified...");
                    }
                }
                else
                {
                    Alert.Show("Your Action Is Not Done !!!");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Error Occur " + ex.Message);
            }

        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {

            this.LoadpaymentGride();

            lblsearchBtn.Text = "1";

        }

        protected void rdropPDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropPDealer.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropArea_OnDataBound(object sender, EventArgs e)
        {
            rdropArea.Items.Insert(0, new ListItem());
        }

        protected void rdropRegion_OnDataBound(object sender, EventArgs e)
        {

            rdropRegion.Items.Insert(0, new ListItem());
        }

        protected void rdropRegion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadArea(int.Parse(rdropRegion.SelectedValue));
        }

        protected void rdropArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            this.LoadDealerInfo(int.Parse(rdropArea.SelectedValue));
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            DataTable dt = dtPayment.Copy();
            ExportToExcel(dt);
        }

        private void ExportToExcel(DataTable dt)
        {
            try
            {
                string attachment = "attachment; filename=PaymentListReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";

                string tab = "";

                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                decimal tq = 0;
                decimal tp = 0;
                decimal td = 0;
                Response.Write("All Payment Information Information");
                Response.Write("\n");
                //foreach (DataColumn dc in dt.Columns)
                //{
                Response.Write(sw.ToString());
                Response.Write("Money Receipt No");
                tab = "\t";
                Response.Write(tab + "Payment Date");
                tab = "\t";
                Response.Write(tab + "Dealer Name");
                tab = "\t";
                Response.Write(tab + "Requisition Code");
                tab = "\t";
                Response.Write(tab + "Payment Type");
                tab = "\t";
                Response.Write(tab + "Payment Mode");
                tab = "\t";
                Response.Write(tab + "Bank Name");
                tab = "\t";
                Response.Write(tab + "Branch");
                tab = "\t";
                Response.Write(tab + "Amount");
                tab = "\t";
                Response.Write(tab + "Reference No");
                tab = "\t"; 
                Response.Write(tab + "Status");
                tab = "\t";
                Response.Write(tab + "Created By");
                tab = "\t";
               
                Response.Write("\n");



                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";

                    Response.Write(tab + dr["MoneyReceiptNo"].ToString());
                    tab = "\t";
                    DateTime Rdt = DateTime.Parse(dr["PaymentDate"].ToString());

                    Response.Write(tab + Rdt.ToString("dd/MM/yyyy"));
                    tab = "\t";
                    Response.Write(tab + dr["DealerName"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["RequisitionCode"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["LstPaymentType"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["LstPaymentMode"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["BankName"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["Branch"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["Amount"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["ReferenceNo"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["Status"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["CreatedBy"].ToString());
                    tab = "\t";
                   
                    Response.Write("\n");
                }
                Response.Write("\n");
                
                Response.End();
                Response.Flush();
                sw.Close();
                sw.Dispose();
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going to load excel data." + ex);
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
                    ReportTitle = "Payment List Report",
                    LogoPath = ""
                };
                List<DetailStructure> detailDataList = new List<DetailStructure>()
                {
                    new DetailStructure
                    {
                        Slno = 1, FieldName = "MoneyReceiptNo", DataType = "string", Caption = "Money Receipt No",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 2, FieldName = "PaymentDate", DataType = "datetime", Caption = "Payment Date",
                        FieldWidth = 110f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "DealerName", DataType = "string", Caption = "Dealer Name",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 4, FieldName = "LstPaymentType", DataType = "string", Caption = "Payment Type",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 5, FieldName = "LstPaymentMode", DataType = "string", Caption = "Payment Mode",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 6, FieldName = "RequisitionCode", DataType = "string", Caption = "Requisition Code",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 7, FieldName = "Status", DataType = "string", Caption = "Status",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 8, FieldName = "CreatedBy", DataType = "string", Caption = "Created By",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    }
                   
                };


                float[] widths = new float[] { 100f, 110f, 100f, 100f, 110f, 80f, 110f, 80f };

                ReportDetail reportDetail = new ReportDetail(detailDataList, dtPayment, 9, "English", widths,
                    "Verdana", false);

                PdfDocument doc = new PdfDocument("Payment List", PageSizes.A4, "Landscape", "English")
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
                Alert.Show("something is going wrong to print data." +
                           ""+ex);
            }
        }
    }
}