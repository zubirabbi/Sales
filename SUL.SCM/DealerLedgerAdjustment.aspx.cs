using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class DealerLedgerAdjustment : System.Web.UI.Page
    {

        private  UserRoleInfo _role;
        private  Users _user;
       // private  bool isNewEntry;
        private  Company _company;

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
                DataTable dtDealerInfo;
                dtDealerInfo = new DealerInformation().GetAllDealerInformationView();

                rdropPDealer.DataTextField = "DealerInfo";
                rdropPDealer.DataValueField = "Id";
                rdropPDealer.DataSource = dtDealerInfo;
                rdropPDealer.DataBind();

                rdropPDealer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
            }
        }
        private void LoadAdjustment()
        {
            try
            {
                List<ListTable> lstTable = new ListTable().GetAllListTableByType("Adjustment");

                lstTable.Insert(0, new ListTable());

                rdropAdjustment.DataTextField = "ListValue";
                rdropAdjustment.DataValueField = "ListId";

                rdropAdjustment.DataSource = lstTable;
                rdropAdjustment.DataBind();
                if (lstTable.Count == 2)
                    rdropAdjustment.SelectedIndex = 1;
                else
                    rdropAdjustment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Nationality DropdownList" + ex);
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
                lblisNewEntry.Text = "true";
                this.LoadDealerInfo();
                this.LoadAdjustment();
            }
           
        }

        protected void rdropPDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropPDealer.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropPDealer_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string id = rdropPDealer.SelectedValue;
            DealerInformation objDealerInformation=new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));
            rtxtCurrentBalance.Text = objDealerInformation.Balance.ToString();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Regex regexForDecimal = new Regex(@"[\d]([.][\d])?");
            #region validation

            if (rdropPDealer.SelectedIndex == 0)
            {
                Alert.Show("Please Select a Dealer");
                rdropPDealer.Focus();
                return;
            }
            if (rdropAdjustment.SelectedIndex == 0)
            {
                Alert.Show("Please Select a Adjustment Type");
                rdropPDealer.Focus();
                return;
            }
            if (rtxtCradit.Text == string.Empty && rtxtDebit.Text == string.Empty)
            {
                Alert.Show("Please Enter Debit or Credit Amount");
                rtxtCradit.Focus();
                return;
            }
            if (rtxtCradit.Text != string.Empty)
            {
                if (!regexForDecimal.IsMatch(rtxtCradit.Text))
                {
                    Alert.Show("Please Enter Correct Amount.");
                    rtxtCradit.Focus();
                    return;
                }
            }
            if (rtxtDebit.Text != string.Empty)
            {
                if (!regexForDecimal.IsMatch(rtxtDebit.Text))
                {
                    Alert.Show("Please Enter Correct Amount.");
                    rtxtDebit.Focus();
                    return;
                }
            }

            #endregion
            try
            {
                DealerLedger objDealerLedger=new DealerLedger();

                objDealerLedger.DealerId = int.Parse(rdropPDealer.SelectedValue);
                objDealerLedger.TransactionType = "Adjustment";
                objDealerLedger.TransactionDate = DateTime.Now;
                objDealerLedger.SourceId ="";
                objDealerLedger.UserId = _user.Id;
                objDealerLedger.OpeningBalance = decimal.Parse(rtxtCurrentBalance.Text);
                objDealerLedger.Debit = rtxtDebit.Text==string.Empty?0:decimal.Parse(rtxtDebit.Text);
                objDealerLedger.Cradit = rtxtCradit.Text == string.Empty ? 0 : decimal.Parse(rtxtCradit.Text); ;
                //objDealerLedger.ClosingBalance = ;

                int dealersuccess = objDealerLedger.InsertDealerLedger();

                if (dealersuccess == 0)
                {
                    Alert.Show("Dealer Ledger information is not save succesfully.");
                }
                else
                {
                    decimal Debit = rtxtDebit.Text == string.Empty ? 0 : decimal.Parse(rtxtDebit.Text);
                    decimal Cradit=rtxtCradit.Text == string.Empty ? 0 : decimal.Parse(rtxtCradit.Text); ;
                    DealerInformation objdinfo = new DealerInformation();
                    DealerInformation objdealerDealerInformation=new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));
                    objdinfo.Id = int.Parse(rdropPDealer.SelectedValue);
                    objdinfo.TotalDebit =  Debit;
                    objdinfo.TotalCredit = Cradit;

                    int dInfosuccess = objdinfo.UpdateDealerInformationfordealerLedger();

                    if (dInfosuccess == 0)
                    {
                        Alert.Show("Dealer information is not save succesfully.");
                    }
                    else
                    {
                        Alert.Show("Data Save Successully.");
                        Response.Redirect("DealerLedgerAdjustment.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ClearAllInfo();
        }

        private void ClearAllInfo()
        {
            rdropPDealer.SelectedIndex = 0;
            rdropAdjustment.SelectedIndex = 0;
            rtxtCradit.Text = "";
            rtxtCradit.Text = "";
            rtxtCurrentBalance.Text = "";
            rtxtRemarks.Text = "";
            lblId.Text = "";
        }
    }
}