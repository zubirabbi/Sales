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
    public partial class UserInformation : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        //private bool isNewEntry;
        private Company _company;

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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("User Info");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("User Info");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("User Info");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("User Info");
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
        private void loadRoleListBox()
        {
            List<UserRole> roleList = new UserRole().GetAllUserRole(_company.Id);

            lbRole.DataSource = roleList;
            lbRole.DataTextField = "Role";
            lbRole.DataValueField = "Id";
            lbRole.DataBind();
        }

        private void LoadEmployeeCombo()
        {
            try
            {
                List<EmployeeInformation> lstEmpInfo = new EmployeeInformation().GetAllEmployeeInformation(_company.Id);
                lstEmpInfo.Insert(0, new EmployeeInformation());

                rdropEmpName.DataTextField = "EmployeeName";
                rdropEmpName.DataValueField = "Id";

                rdropEmpName.DataSource = lstEmpInfo;
                rdropEmpName.DataBind();

                if (lstEmpInfo.Count == 2)
                    rdropEmpName.SelectedIndex = 1;
                else
                    rdropEmpName.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Employee Data." + ex);
            }
        }

        private void ClearAllinfo()
        {
            lblId.Text = "";
            rtxtPassword.Text = "";
            rtxtUserName.Text = "";
            chkIsActive.Checked = true;
            rdropEmpName.SelectedIndex = 0;
            lblisNewEntry.Text = "true";

            //lbRole.ClearSelection();
        }

        private void LoadUserGrid()
        {
            try
            {

                DataTable dtUserlist = new Users().GetUserListFromViewList();
                if (dtUserlist.Rows.Count == 0)
                {
                    dgvUser.DataSource = new string[] { };
                    dgvUser.DataBind();
                    return;
                }

                dgvUser.DataSource = dtUserlist;
                dgvUser.DataBind();
                //if (!IsValidUpdateForUser())
                //{
                //    dgvUser.MasterTableView.GetColumn("colEdit").Display = false;
                //}
            }
            catch (Exception ex)
            {
                Alert.Show("Error in method 'LoadLeaveDetailsGrid'. Error: " + ex.Message);
            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidSession())
                {
                    string str = Request.QueryString.ToString();
                    if (str == string.Empty)
                        Response.Redirect("LogIn.aspx?refPage=HomePage.aspx");
                    else
                        Response.Redirect("LogIn.aspx?refPage=HomePage.aspx?" + str);

                }
                if (!IsValidPageForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("LogIn.aspx?refPage=HomePage.aspx", false);
                }
                if (!IsPostBack)
                {
                    if (!IsValidInsertForUser())
                    {
                        Alert.Show("Sorry, You Don't Have permission to access this page.");
                        Response.Redirect("ErrorPage.aspx", false);
                    }

                    lblisNewEntry.Text = "true";
                    this.LoadEmployeeCombo();
                    this.loadRoleListBox();
                    this.LoadUserGrid();

                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }



        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (rtxtUserName.Text == string.Empty)
                {
                    Alert.Show("Please enter a user name.");
                    rtxtPassword.Focus();
                    return;
                }
                if (Regex.IsMatch(rtxtUserName.Text, @"^[a-zA-Z0-9_]{5,20}$") != true)
                {
                    Alert.Show(
                        "User name must be between 5 to 20 Characters Or Lowercase and Uppercase characters Or Alpha-Numeric And No Space And special character allowed");
                    rtxtUserName.Focus();
                    return;
                }

                if (rtxtPassword.Text == string.Empty)
                {
                    Alert.Show("Please enter a password for the user.");
                    rtxtPassword.Focus();
                    return;
                }
                if (rdropEmpName.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a employee first..");
                    rdropEmpName.Focus();
                    return;
                }

                int count = 0;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    count = _user.CheckUserExistance(int.Parse(rdropEmpName.SelectedValue));
                    if (count > 0)
                    {
                        Alert.Show("This employee already has a user account.");
                        return;
                    }
                }

                count = _user.CheckUserNameExistance((lblId.Text == string.Empty) ? 0 : int.Parse(lblId.Text),
                    rtxtUserName.Text, bool.Parse(lblisNewEntry.Text));

                if (count > 0)
                {
                    Alert.Show("User name already exists. ");
                    return;
                }


                _user = new Users();
                _user.Id = (lblId.Text == string.Empty) ? 0 : int.Parse(lblId.Text);
                _user.UserName = rtxtUserName.Text;
                _user.UserPass = rtxtPassword.Text;
                _user.EmployeeId = (rdropEmpName.SelectedValue == string.Empty)
                    ? 0
                    : int.Parse(rdropEmpName.SelectedValue);
                _user.CompanyId = _company.Id;
                _user.IsActive = (bool)chkIsActive.Checked;

                int success = 0;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = _user.InsertUsers();
                    _user.Id = new Users().GetLastId(_user.CompanyId);
                }
                else
                    success = _user.UpdateUsers();

                if (success == 0)
                {

                    Alert.Show("Create user information was not successfull.");
                    return;
                }

                //delete all roles from userrole mapping table
                success = new UserRoleMapping().DeleteUserRoleMappingByUserId(_user.Id);
                //get roles and update db
                foreach (RadListBoxItem item in lbRole.CheckedItems)
                {
                    if (item.Checked)
                    {
                        int roleId = int.Parse(item.Value);
                        UserRoleMapping role = new UserRoleMapping();

                        role.UserId = _user.Id;
                        role.RoleId = roleId;
                        role.CompanyId = _user.CompanyId;

                        role.InsertUserRoleMapping();
                    }
                }

                Alert.Show("User information created succssfully.");
                //Response.Redirect("UserInformation.aspx");
                this.ClearAllinfo();
                this.LoadUserGrid();
                //lbRole.ClearSelection();
            }
            catch (Exception ex)
            {
                Alert.Show("Error during user information save. Error: " + ex.Message);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearAllinfo();
        }

       

        protected void dgvUser_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "btnSelect")
            {
                GridDataItem item = (GridDataItem)e.Item;

                lblId.Text = item["colId"].Text;
                rtxtUserName.Text = item["colName"].Text.Trim();
                rtxtPassword.Text = (item["colPass"].Text == "&nbsp;") ? "" : item["colPass"].Text.Trim();
                rdropEmpName.SelectedValue = (item["colEmpId"].Text == "&nbsp;") ? "" : item["colEmpId"].Text.Trim();
                lblisNewEntry.Text = "false";

                List<UserRoleMapping> lstRoleMappings=new UserRoleMapping().GetAllUserRoleMappingbyUserId(int.Parse(lblId.Text),_company.Id);


                foreach (RadListBoxItem items in lbRole.Items)
                {
                    int id = int.Parse(items.Value.ToString());
                    if (lstRoleMappings.Exists(x => x.RoleId == id))
                        items.Checked = true;
                }

            }
            else if (e.CommandName == "btnDelete")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    lblId.Text = item["colId"].Text;
                    int id = int.Parse(lblId.Text);
                    Users userDelete = new Users();
                    int success = userDelete.DeleteUsersById(id);
                    if (success == 0)
                    {
                        Alert.Show("Something is going Wrong!!!!");
                    }
                    else
                    {
                        Alert.Show("Successfully Deleted!!");
                        this.LoadUserGrid();
                    }
                }
                catch (Exception ex)
                {
                    Alert.Show("Error happen during delete attendance data. Error: " + ex.Message);
                }
            }
        }

        protected void dgvUser_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadUserGrid();
        }

        protected void dgvUser_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadUserGrid();
        }


    }
}