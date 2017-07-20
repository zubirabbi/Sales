using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hrms;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class PaymentInfo : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        //private bool isNewEntry;
        private Company _company;

        private string _department;
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Payment Information");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Payment Information");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Payment Information");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Payment Information");
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
        private void LoadPaymentType()
        {
            try
            {
                List<ListTable> lstTable = new ListTable().GetAllListTableByType("PaymentType");

                lstTable.Insert(0, new ListTable());

                rdropPaymentType.DataTextField = "ListValue";
                rdropPaymentType.DataValueField = "ListId";

                rdropPaymentType.DataSource = lstTable;
                rdropPaymentType.DataBind();
                if (lstTable.Count == 2)
                    rdropPaymentType.SelectedIndex = 1;
                else
                    rdropPaymentType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Payment Type DropdownList" + ex);
            }
        }

        private void LoadPaymentMode()
        {
            try
            {
                List<ListTable> lstTable = new ListTable().GetAllListTableByType("PaymentMode");

                lstTable.Insert(0, new ListTable());

                rdropPaymentmode.DataTextField = "ListValue";
                rdropPaymentmode.DataValueField = "ListId";

                rdropPaymentmode.DataSource = lstTable;
                rdropPaymentmode.DataBind();
                if (lstTable.Count == 2)
                    rdropPaymentmode.SelectedIndex = 1;
                else
                    rdropPaymentmode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Payment Mode DropdownList" + ex);
            }
        }

        private void LoadDealerInfo()
        {
            try
            {
                DataTable dtDealerInfo = new DealerInformation().GetAllDealerInformationView();

                DataRow row = dtDealerInfo.NewRow();
                dtDealerInfo.Rows.InsertAt(row,0);
                rdropPDealer.DataTextField = "DealerInfo";
                rdropPDealer.DataValueField = "Id";
                rdropPDealer.DataSource = dtDealerInfo;
                rdropPDealer.DataBind();

                if (dtDealerInfo.Rows.Count == 2)
                {
                    rdropPDealer.SelectedIndex = 1;
                    DealerInformation objdDealerInformation = new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));
                    rtxtAddress.Text = objdDealerInformation.Address;
                }
                else
                    rdropPDealer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("someting is going wrong to Load Dealer Info." + ex);
            }
        }

        private void LoadBankInformation()
        {
            try
            {
                DataTable lstbaBankInformations =
                    new BankInformation().GetAllViewbankInfoByCompany();

                rdropBankName.DataTextField = "BankInfo";
                rdropBankName.DataValueField = "Id";
                rdropBankName.DataSource = lstbaBankInformations;
                rdropBankName.DataBind();

                if (lstbaBankInformations.Rows.Count == 2)
                    rdropBankName.SelectedIndex = 1;
                else
                    rdropBankName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load bank info." + ex);
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

            if (!IsPostBack)
            {
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                if (!IsValidUpdateForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                lblisNewEntry.Text = "true";
                lblStatus.Text = "Created";

                this.LoadPaymentMode();
                this.LoadPaymentType();
                this.LoadBankInformation();
                this.LoadDealerInfo();
                rdtpaymentDate.SelectedDate = DateTime.Now;

                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];
                    Payment objPayment = new Payment().GetPaymentById(int.Parse(id));

                    lblId.Text = objPayment.Id.ToString();

                   
                    rtxtMoneyReceiptNo.Text = objPayment.MoneyReceiptNo;
                    rdtpaymentDate.SelectedDate = objPayment.PaymentDate;
                    rdropPDealer.SelectedValue = objPayment.DealerId.ToString();
                    DealerInformation objDealerInformation = new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));
                    rtxtAddress.Text = objDealerInformation.Address;
                    rdropPaymentType.SelectedValue = objPayment.PaymentType.ToString();
                    rtxtAmount.Text = objPayment.Amount.ToString();
                    rdropPaymentmode.SelectedValue = objPayment.PaymentMode.ToString();
                    rdropBankName.SelectedValue = objPayment.BankNameId.ToString();
                    rtxtRefNo.Text = objPayment.ReferenceNo;
                    lblRequisitionId.Text = objPayment.RequisitionId.ToString();
                    rtxtBankCharge.Text = objPayment.BankCharge.ToString();
                    rtxtBranch.Text = objPayment.Branch;
                    lblisNewEntry.Text = "false";
                    lblStatus.Text = objPayment.Status;
                    if (rdropPaymentType.SelectedItem.Text == "Deposit")
                    {
                        showDeposit.Visible = true;
                        rtxtChequeBank.Text = objPayment.ChequeBank;
                        rtxtChequeBranch.Text = objPayment.ChequeBranch;
                        rdtCheckDate.SelectedDate = objPayment.ChequeDate;
                    }
                    else
                    {
                        showDeposit.Visible = false;
                        rtxtChequeBank.Text = "";
                        rtxtChequeBranch.Text = "";
                        rdtCheckDate.SelectedDate = null;
                    }
                    if (lblStatus.Text.ToLower() == "created" && _department.ToLower().Contains("account"))
                    {
                        new Payment().ChangePaymentStatus(int.Parse(lblId.Text), "Seen",_user.Id);
                    }

                    if (_department != "All")
                    {
                        if (_department.ToLower().Contains("sales"))
                        {
                            if (objPayment.IsVarified == true)
                            {
                                btnSave.Visible = false;
                                btnClear.Visible = false;

                            }
                        }
                    }

                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation

            if (rdropPDealer.SelectedIndex <= 0)
            {
                Alert.Show("Please select a Dealer infornation");
                rdropPDealer.Focus();
                return;
            }
            if (rdropPaymentmode.SelectedIndex <= 0)
            {
                Alert.Show("Please select a payment mode");
                rdropPaymentmode.Focus();
                return;
            }
            if (rdropPaymentType.SelectedIndex <= 0)
            {
                Alert.Show("Please select a Payment type");
                rdropPaymentType.Focus();
                return;
            }
            if (rdtpaymentDate.SelectedDate == null)
            {
                Alert.Show("Please select a payment date.");
                rdtpaymentDate.Focus();
                return;
            }
            if (rdropPaymentmode.SelectedItem.Text.ToLower() == "bank")
            {
                if (rdropBankName.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a BankName");
                    rdropBankName.Focus();
                    return;
                }
                if (rtxtRefNo.Text == string.Empty)
                {
                    Alert.Show("Please enter a reference no.");
                    rtxtRefNo.Focus();
                    return;
                }
            }
          
            if (rtxtAmount.Text == string.Empty)
            {
                Alert.Show("Please enter a amount");
                rtxtAmount.Focus();
                return;
            }
            
            try
            {
                decimal amount = decimal.Parse(rtxtAmount.Text);
            }
            catch (Exception ex)
            {
                Alert.Show("Not a valid amount.");
                rtxtAmount.Focus();
                return;
            }
            try
            {
                if (rtxtBankCharge.Text != string.Empty)
                {
                    decimal charge = decimal.Parse(rtxtBankCharge.Text);
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Not a valid bank charge.");
                rtxtBankCharge.Focus();
                return;
            }
            #endregion


            try
            {
                Payment objPayment = new Payment();
                objPayment.MoneyReceiptNo = rtxtMoneyReceiptNo.Text;
                objPayment.PaymentDate = DateTime.Parse(rdtpaymentDate.SelectedDate.ToString());
                objPayment.DealerId = int.Parse(rdropPDealer.SelectedValue);
                objPayment.Address = rtxtAddress.Text;
                objPayment.PaymentType = int.Parse(rdropPaymentType.SelectedValue);
                objPayment.Amount = decimal.Parse(rtxtAmount.Text);
                objPayment.PaymentMode = int.Parse(rdropPaymentmode.SelectedValue);
                objPayment.BankNameId = rdropBankName.SelectedIndex <= 0 ? 0 : int.Parse(rdropBankName.SelectedValue);
                objPayment.ReferenceNo = rtxtRefNo.Text == string.Empty ? "" : rtxtRefNo.Text;
                objPayment.IsVarified = false;
                objPayment.BankCharge = rtxtBankCharge.Text == string.Empty ? 0 : decimal.Parse(rtxtBankCharge.Text);
                objPayment.Branch = rtxtBranch.Text;
                objPayment.Status = lblStatus.Text;
                DealerInformation objdInformation = new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));
                objPayment.LastBalance = objdInformation.Balance;
                objPayment.Status = lblStatus.Text == string.Empty ? "unverified" : lblStatus.Text;
                objPayment.UpdateBy = _user.Id;
                objPayment.CreatedBy = _user.Id;
                objPayment.ChequeBank = rtxtChequeBank.Text == string.Empty ? "" : rtxtChequeBank.Text;
                objPayment.ChequeBranch = rtxtChequeBranch.Text == string.Empty ? "" : rtxtChequeBranch.Text;
                objPayment.ChequeDate = rdtCheckDate.SelectedDate == null
                    ? PublicVariables.minDate
                    : DateTime.Parse(rdtCheckDate.SelectedDate.ToString());

                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    objPayment.RequisitionId = 0;
                    string Moneycode = new Payment().GetlastMoneyReceiptCode();
                    rtxtMoneyReceiptNo.Text = Moneycode;
                    objPayment.MoneyReceiptNo = rtxtMoneyReceiptNo.Text;
                    success = objPayment.InsertPayment();
                    lblisNewEntry.Text = "false";
                }
                else
                {
                    if (lblRequisitionId.Text == string.Empty)
                    {
                        objPayment.RequisitionId = 0;
                    }
                    else
                    {
                        objPayment.RequisitionId = int.Parse(lblRequisitionId.Text);
                    }
                    if (lblId.Text == string.Empty || lblId.Text == "0")
                    {
                        lblId.Text = new Payment().GetMaxPaymentId().ToString();
                    }
                    Payment objPay=new Payment().GetPaymentById(int.Parse(lblId.Text));
                    objPayment.CreatedBy = objPay.CreatedBy;
                    objPayment.Id = int.Parse(lblId.Text);
                    success = objPayment.UpdatePayment();

                }
                if (success == 0)
                {
                    Alert.Show("Data is not save succesfully");
                }
                else
                {
                    Alert.Show("Data saved succesfully");

                    this.ClearAllInfo();
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to  save data");
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ClearAllInfo();
        }

        private void ClearAllInfo()
        {
            rtxtMoneyReceiptNo.Text = "";
            rdtpaymentDate.SelectedDate = null;
            rdropPDealer.SelectedIndex = 0;
            rtxtAddress.Text = "";
            rdropPaymentType.SelectedIndex = 0;
            rtxtAmount.Text = "";
            rdropPaymentmode.SelectedIndex = 0;
            rdropBankName.SelectedIndex = 0;
            rtxtRefNo.Text = "";
            rtxtBankCharge.Text = "";
            rtxtBranch.Text = "";
            lblId.Text = "";
            lblStatus.Text = "Created";
            lblisNewEntry.Text = "true";
            rtxtChequeBranch.Text = "";
            rtxtChequeBank.Text = "";
            rdtCheckDate.SelectedDate = null;
        }

        protected void rdropPDealer_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DealerInformation objdDealerInformation = new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));
            rtxtAddress.Text = objdDealerInformation.Address;
        }

        protected void rdropPaymentmode_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //throw new NotImplementedException();

            if (rdropPaymentmode.SelectedValue == "1")
            {
                rdropBankName.Enabled = false;
                rtxtBankCharge.Enabled = false;
                rtxtBranch.Enabled = false;

                rdropBankName.SelectedIndex = 0;
                rtxtBranch.Text = "";
                rtxtBankCharge.Text = "";
            }
            else
            {
                rdropBankName.Enabled = true;
                rtxtBankCharge.Enabled = true;
                rtxtBranch.Enabled = true;
            }
        }

        protected void rdropBankName_OnDataBound(object sender, EventArgs e)
        {
            rdropBankName.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropPaymentType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (rdropPaymentType.SelectedItem.Text == "Deposit")
            {
                showDeposit.Visible = true;
            }
            else
            {
                showDeposit.Visible = false;
                rtxtChequeBank.Text = "";
                rtxtChequeBranch.Text = "";
                rdtCheckDate.SelectedDate = null;

            }
        }
    }
}