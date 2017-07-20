using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class BankInfo : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  Company _company;

        private bool IsValidSession()
        {
            if (Session["user"] == null)
            {
                return false;
            }

            _user = (Users)Session["user"];
            _company = (Company) Session["company"];
            return _user.Id != 0;

        }
        private bool IsValidPageForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Bank");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Bank");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Bank");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Bank");
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

        private void LoadBankInfo()
        {
            try
            {
                List<BankInformation> lstBankInfo = new BankInformation().GetAllBankInformation();
                RadGridPBank.DataSource = lstBankInfo;
                RadGridPBank.DataBind();
                if (!IsValidUpdateForUser())
                {
                    RadGridPBank.MasterTableView.GetColumn("colEdit").Display = false;
                }

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load bank information data.");
            }
        }

        private void CleanBank()
        {
            rtxtAccOn.Text = "";
            rtxtAccTitle.Text = "";
            rtxtBankName.Text = "";
            rtxtBranchName.Text = "";
            rtxtSWIFTCode.Text = "";
            chkIsDefault.Checked = false;
            lblisNewEntry.Text = "true";
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
                lblisNewEntry.Text = "true";
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                this.LoadBankInfo();

            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation

            if (rtxtBankName.Text == string.Empty)
            {
                Alert.Show("Please enter bank name");
                rtxtBankName.Focus();
                return;
            }
            if (rtxtAccOn.Text == string.Empty)
            {
                Alert.Show("Please enter account number.");
                rtxtAccOn.Focus();
                return;
            }
            if (rtxtAccTitle.Text == string.Empty)
            {
                Alert.Show("Please enter account title.");
                rtxtAccTitle.Focus();
                return;
            }
            if (rtxtSWIFTCode.Text == string.Empty)
            {
                Alert.Show("Please enter SWIFT code.");
                rtxtSWIFTCode.Focus();
                return;
            }

            #endregion

            try
            {
                BankInformation objBankInfo = new BankInformation();
                objBankInfo.BankName = rtxtBankName.Text;
                objBankInfo.AccountNo = rtxtAccOn.Text;
                objBankInfo.AccountTitle = rtxtAccTitle.Text;
                objBankInfo.BranchName = rtxtBranchName.Text == string.Empty ? "" : rtxtBranchName.Text;
                objBankInfo.SWIFTCode = rtxtSWIFTCode.Text;
                objBankInfo.Country = 1;
                objBankInfo.Type = "Company";
                objBankInfo.TypeId = _company.Id;
                objBankInfo.IsDefault = chkIsDefault.Checked;
                objBankInfo.ShortName = rtxtBankShortName.Text;

                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objBankInfo.InsertBankInformation();
                }
                else
                {
                    objBankInfo.Id = int.Parse(lblId.Text);
                    success = objBankInfo.UpdateBankInformation();
                }
                if (success == 0)
                {
                    Alert.Show("Bank data was not save successfully ");
                    return;
                }
                else
                {
                    //this.LoadBankInfo();
                    //this.CleanBank();
                    Response.Redirect("BankInfo.aspx");
                }

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save bank information.");
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.CleanBank();
        }

        protected void RadGridPBank_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;



            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                BankInformation objBankInfo = new BankInformation();

                int id = int.Parse(item["colId"].Text);
                delete = objBankInfo.DeleteBankInformationById(id);

                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    //this.LoadBankInfo();
                    Response.Redirect("BankInfo.aspx");
                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;

                    rtxtAccOn.Text = item["colAccountNo"].Text;
                    rtxtAccTitle.Text = item["colAccountTitle"].Text;
                    rtxtBankName.Text = item["colBankName"].Text;
                    rtxtBranchName.Text = item["colBranchName"].Text;
                    rtxtSWIFTCode.Text = item["colSWIFTCode"].Text;
                    chkIsDefault.Checked = bool.Parse(item["colIsDefault"].Text);
                    rtxtBankShortName.Text = item["colShortName"].Text;
                    lblisNewEntry.Text = "false";
                }
                catch (Exception ex)
                {
                    Alert.Show("Something is going wrong to select data." + ex);
                }
            }
        }

        protected void RadGridPBank_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadBankInfo();
        }

        protected void RadGridPBank_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadBankInfo();
        }
    }
}