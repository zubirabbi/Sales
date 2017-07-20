using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class DataExport : System.Web.UI.Page
    {
        private UserRole _role;
        private Users _user;
        private Company _company;
        private DataTable dtSalesVoucher;
        private DataTable dtReceiptVoucher;

        private string _department;
        private int _source;
        private AppPermission PermissionUser;

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
            //int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Data Export");
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Data Export") : new AppFunctionality().GetAppFunctionalityId("Data Export", int.Parse(lblsource.Text));
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
        private void LoadSalesReceipt()
        {
            try
            {
                StringBuilder whereCondition = new StringBuilder();
                DateTime startDate = DateTime.Parse(rdtStartDate.SelectedDate.ToString());
                DateTime endDate = DateTime.Parse(rdtEndDate.SelectedDate.ToString());
                whereCondition.Append(" Where Status = 'Invoiced'");
                if (rdtStartDate.SelectedDate != null)
                {
                    whereCondition.Append(" And  (cast(CONVERT(varchar(8), InvoiceDate, 112) AS datetime) >= '" + startDate.ToString("MMM dd,yyyy") + "')");

                    if (rdtEndDate.SelectedDate != null)
                    {
                        if (whereCondition.Length > 0)
                        {
                            whereCondition.Append(" And (cast(CONVERT(varchar(8), InvoiceDate, 112) AS datetime)  <= '" + endDate.ToString("MMM dd,yyyy") + "')");

                        }
                        else
                        {
                            whereCondition.Append(" Where (cast(CONVERT(varchar(8), InvoiceDate, 112) AS datetime)  <= '" + endDate.ToString("MMM dd,yyyy") + "')");
                        }
                    }
                }
                dtSalesVoucher = new RequisitionMaster().GetAllInfoFromViewList(whereCondition.ToString());


                DataTable dtSales = new DataTable();


                dtSales.Columns.Add("Sales_Voucher_Number ", typeof(string));
                dtSales.Columns.Add("Refference_Number", typeof(string));
                dtSales.Columns.Add("Invoice_Date", typeof(DateTime));
                dtSales.Columns.Add("Party_Name", typeof(string));
                dtSales.Columns.Add("Item_Name", typeof(string));
                dtSales.Columns.Add("Actual_Quantity", typeof(decimal));
                dtSales.Columns.Add("Billed_Quantity", typeof(string));
                dtSales.Columns.Add("Rate", typeof(decimal));
                dtSales.Columns.Add("Discount", typeof(decimal));
                dtSales.Columns.Add("Amount", typeof(decimal));
                dtSales.Columns.Add("Sale_Ledger", typeof(string));
                dtSales.Columns.Add("Narration", typeof(string));

                dtSales.Columns.Add("Godown_Name", typeof(string));

                foreach (DataRow dr in dtSalesVoucher.Rows)
                {
                    DataRow drw = dtSales.NewRow();
                    DateTime date = DateTime.Parse(dr["InvoiceDate"].ToString());
                    drw[0] = dr["InvoiceNo"].ToString();
                    drw[1] = dr["RequisitionCode"].ToString();
                    drw[2] = date.ToString("MMM dd, yyyy");
                    drw[3] = dr["DealerName"].ToString();
                    drw[4] = dr["ProductName"].ToString();
                    drw[5] = dr["Quantity"].ToString();
                    if (decimal.Parse(dr["Price"].ToString()) == 0)
                    {
                        drw[6] = "";
                    }
                    else
                    {
                        drw[6] = dr["Quantity"].ToString();
                    }
                    drw[7] = dr["Price"].ToString();
                    drw[8] = dr["Discount"].ToString();
                    drw[9] = dr["LineTotal2"].ToString();

                    drw[10] = "Sales";
                    drw[11] = "";
                    drw[12] = dr["Name"].ToString();


                    dtSales.Rows.Add(drw);
                }
                if (dtSales.Rows.Count == 0)
                {
                    RadGridSales.DataSource = new string[] { };
                    RadGridSales.DataBind();
                    Session["dtSalesVoucher"] = null;
                    return;
                }
                RadGridSales.DataSource = dtSales;
                RadGridSales.DataBind();
                Session["dtSalesVoucher"] = dtSalesVoucher;

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load data." + ex.Message);
            }
        }
        private void LoadVoucherReceipt()
        {
            try
            {
                StringBuilder whereCondition = new StringBuilder();
                DateTime startDate = DateTime.Parse(rdtStartDate.SelectedDate.ToString());
                DateTime endDate = DateTime.Parse(rdtEndDate.SelectedDate.ToString());
                whereCondition.Append(" Where IsVarified='true'");
                if (rdtStartDate.SelectedDate != null)
                {
                    whereCondition.Append(" And (cast(CONVERT(varchar(8), PaymentDate, 112) AS datetime) >= '" + startDate.ToString("MMM dd,yyyy") + "')");

                    if (rdtEndDate.SelectedDate != null)
                    {
                        if (whereCondition.Length > 0)
                        {
                            whereCondition.Append(" And (cast(CONVERT(varchar(8), PaymentDate, 112) AS datetime)  <= '" + endDate.ToString("MMM dd,yyyy") + "')");

                        }
                        else
                        {
                            whereCondition.Append(" Where (cast(CONVERT(varchar(8), PaymentDate, 112) AS datetime)  <= '" + endDate.ToString("MMM dd,yyyy") + "')");
                        }
                    }
                }
                dtReceiptVoucher = new Payment().GetAllViewpayment(whereCondition.ToString());

                DataTable dtReceipt = new DataTable();


                dtReceipt.Columns.Add("Voucher_No", typeof(string));
                dtReceipt.Columns.Add("Vocuher_Type", typeof(string));
                dtReceipt.Columns.Add("Voucher_Date", typeof(DateTime));
                dtReceipt.Columns.Add("Cr_LedName", typeof(string));
                dtReceipt.Columns.Add("Dr_LedName", typeof(string));
                dtReceipt.Columns.Add("Vch_Amt", typeof(decimal));
                dtReceipt.Columns.Add("Tranction_Type", typeof(string));
                dtReceipt.Columns.Add("Cheque_No_Instrument_Number", typeof(string));
                dtReceipt.Columns.Add("Favouring_Name", typeof(string));
                dtReceipt.Columns.Add("Bank_Name_deposit_chque_bank)", typeof(string));
                dtReceipt.Columns.Add("Branch_Name", typeof(string));
                dtReceipt.Columns.Add("Narrantion", typeof(string));
                foreach (DataRow dr in dtReceiptVoucher.Rows)
                {
                    DataRow drw = dtReceipt.NewRow();
                    drw[0] = dr["MoneyReceiptNo"];
                    if (dr["PaymentMode"].ToString() == "2")
                    {
                        drw[1] = "Recpt.-" + dr["ShortName"].ToString();
                    }
                    else if (dr["PaymentMode"].ToString() == "1")
                    {
                        drw[1] = "Recpt.-Cash".ToString();
                    }
                    DateTime date = DateTime.Parse(dr["PaymentDate"].ToString());
                    drw[2] = date.ToString("MMM dd, yyyy");
                    drw[3] = dr["DealerName"].ToString();
                    if (dr["LstPaymentMode"].ToString() == "Cash")
                        drw[4] = "Cash Sales";
                    else
                        drw[4] = dr["BankName"].ToString();
                    drw[5] = dr["Amount"].ToString();
                    drw[6] = dr["LstPaymentMode"].ToString();
                    drw[7] = dr["ReferenceNo"].ToString();
                    drw[8] = "ELITE Technologies";
                    if (dr["PaymentType"].ToString() == "Diposit")
                    {
                        drw[9] = dr["ChequeBank"].ToString();
                        drw[10] = dr["ChequeBranch"].ToString();
                    }
                    else
                    {
                        drw[9] = dr["BankName"].ToString();
                        drw[10] = dr["Branch"].ToString();
                    }
                    drw[11] = "";

                    dtReceipt.Rows.Add(drw);
                }
                if (dtReceipt.Rows.Count == 0)
                {
                    RadGridReceipt.DataSource = new string[] { };
                    RadGridSales.DataBind();
                    Session["dtSalesVoucher"] = null;
                }
                RadGridReceipt.DataSource = dtReceipt;
                RadGridReceipt.DataBind();
                Session["dtReceiptVoucher"] = dtReceiptVoucher;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load data." + ex.Message);
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

            if (Session["dtReceiptVoucher"] != null)
            {
                dtReceiptVoucher = (DataTable)Session["dtReceiptVoucher"];
            }
            else
            {
                dtReceiptVoucher = new DataTable();
            }
            if (Session["dtSalesVoucher"] != null)
            {
                dtSalesVoucher = (DataTable)Session["dtSalesVoucher"];
            }
            else
            {
                dtSalesVoucher = new DataTable();
            }
            if (!IsPostBack)
            {
                lblReceipt.Text = "0";
                lblSales.Text = "0";
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

        protected void btnSales_OnClick(object sender, EventArgs e)
        {
            lblSales.Text = "1";
            lblReceipt.Text = "0";
            showSales.Visible = true;
            showBank.Visible = false;
            this.LoadSalesReceipt();
        }

        protected void btnReceipt_OnClick(object sender, EventArgs e)
        {
            lblSales.Text = "0";
            lblReceipt.Text = "1";
            showBank.Visible = true;
            showSales.Visible = false;
            this.LoadVoucherReceipt();
        }

        protected void RadGridSales_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadSalesReceipt();
        }

        protected void RadGridSales_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadSalesReceipt();
        }

        protected void RadGridReceipt_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadVoucherReceipt();
        }

        protected void RadGridReceipt_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadVoucherReceipt();
        }

        protected void btnExccel_OnClick(object sender, EventArgs e)
        {
            DataTable dt;
            if (lblSales.Text == "1")
            {
                dt = dtSalesVoucher.Copy();
            }
            else
            {
                dt = dtReceiptVoucher.Copy();
            }
            ExportToExcel(dt);
        }

        public void ExportToExcel(DataTable dt)
        {


            if (lblReceipt.Text == "1")
            {
                string attachment = "attachment; filename=Receipt Voucher.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";

                string tab = "";

                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                Response.Write(sw.ToString());
                Response.Write(tab + "Voucher_No");
                tab = "\t";
                Response.Write(tab + "Vocuher Type");
                tab = "\t";
                Response.Write(tab + "Voucher_Date");
                tab = "\t";
                Response.Write(tab + "Cr_LedName (Cr. Side)");
                tab = "\t";
                Response.Write(tab + "Dr_LedName (Dr. Side)");
                tab = "\t";

                Response.Write(tab + "Vch_Amt");
                tab = "\t";
                Response.Write(tab + "Tranction Type");
                tab = "\t";
                Response.Write(tab + "Cheque_No/ Instrument Number");
                tab = "\t";
                Response.Write(tab + "Favouring Name");
                tab = "\t";
                Response.Write(tab + "Bank Name (deposit/ chque bank)");
                tab = "\t";
                Response.Write(tab + "Branch Name");
                tab = "\t";
                Response.Write(tab + "Narrantion");
                tab = "\t";

                Response.Write("\n");


                foreach (DataRow dr in dt.Rows)
                {

                    tab = "";
                    Response.Write(tab + dr["MoneyReceiptNo"]);
                    tab = "\t";
                    if (dr["PaymentMode"].ToString() == "2")
                    {
                        Response.Write(tab + "Recpt.-" + dr["ShortName"].ToString());
                        tab = "\t";
                    }
                    else if (dr["PaymentMode"].ToString() == "1")
                    {
                        Response.Write(tab + "Recpt.-Cash".ToString());
                        tab = "\t";
                    }
                    DateTime date = DateTime.Parse(dr["PaymentDate"].ToString());
                    Response.Write(tab + date.ToString("dd MMM,yyyy"));
                    tab = "\t";
                    Response.Write(tab + dr["DealerName"].ToString());
                    tab = "\t";
                    if (dr["LstPaymentMode"].ToString() == "Cash")
                    {
                        Response.Write(tab + "Cash Sales");
                        tab = "\t";
                    }
                    else
                    {
                        Response.Write(tab + dr["BankName"].ToString());
                        tab = "\t";
                    }



                    Response.Write(tab + dr["Amount"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["LstPaymentMode"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["ReferenceNo"].ToString());
                    tab = "\t";
                    Response.Write(tab + "ELITE Technologies");
                    tab = "\t";
                    if (dr["PaymentType"].ToString() == "Diposit")
                    {
                        Response.Write(tab + dr["ChequeBank"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["ChequeBranch"].ToString());
                        tab = "\t";
                    }
                    else
                    {
                        Response.Write(tab + dr["BankName"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Branch"].ToString());
                        tab = "\t";
                    }
                    Response.Write(tab + "");
                    tab = "\t";
                    Response.Write("\n");
                }
                Response.End();
                Response.Flush();
                sw.Close();
                sw.Dispose();
            }
            if (lblSales.Text == "1")
            {
                string attachment = "attachment; filename=Sales Voucher.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";

                string tab = "";

                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                Response.Write(sw.ToString());
                Response.Write(tab + "Sales Voucher Number");
                tab = "\t";
                Response.Write(tab + "Refference Number");
                tab = "\t";
                Response.Write(tab + "Invoice_Date");
                tab = "\t";
                Response.Write(tab + "Party_Name (Dr. Side)");
                tab = "\t";
                Response.Write(tab + "Item_Name");
                tab = "\t";
                Response.Write(tab + "Actual Quantity");
                tab = "\t";
                Response.Write(tab + "Billed Quantity");
                tab = "\t";
                Response.Write(tab + "Rate");
                tab = "\t";
                Response.Write(tab + "Discount");
                tab = "\t";
                Response.Write(tab + "Amount");
                tab = "\t";
                Response.Write(tab + "Sale_Ledger (Credit Side)");
                tab = "\t";

                Response.Write(tab + "Godown_Name");
                tab = "\t";
                Response.Write(tab + "Narration");
                tab = "\t";
                Response.Write("\n");


                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    DateTime date = DateTime.Parse(dr["InvoiceDate"].ToString());
                    Response.Write(tab + dr["InvoiceNo"]);
                    tab = "\t";
                    Response.Write(tab + dr["RequisitionCode"].ToString());
                    tab = "\t";
                    Response.Write(tab + date.ToString("dd MMM,yyyy"));
                    tab = "\t";

                    Response.Write(tab + dr["DealerName"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["ProductName"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["Quantity"].ToString());
                    tab = "\t";
                    if (decimal.Parse(dr["Price"].ToString()) == 0)
                    {
                        Response.Write(tab + "");
                        tab = "\t";
                    }
                    else
                    {
                        Response.Write(tab + dr["Quantity"].ToString());
                        tab = "\t";
                    }
                    Response.Write(tab + dr["Price"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["Discount"].ToString());
                    tab = "\t";
                    Response.Write(tab + dr["LineTotal2"].ToString());
                    tab = "\t";

                    Response.Write(tab + "Sales");
                    tab = "\t";

                    Response.Write(tab + dr["Name"].ToString());
                    tab = "\t";
                    Response.Write(tab + "");
                    tab = "\t";
                    Response.Write("\n");
                }
                Response.End();
                Response.Flush();
                sw.Close();
                sw.Dispose();
            }



        }

    }
}