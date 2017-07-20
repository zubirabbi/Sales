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
    public partial class UserRoleInfo : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  bool isNewEntry;
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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

        private void LoadRoleGrid()
        {
            try
            {
                List<UserRole> lstUserRoles = new UserRole().GetAllUserRole(_company.Id);

                if (lstUserRoles.Count == 0)
                {
                    RadGridPRole.DataSource = new string[] {};
                    return;
                }
                RadGridPRole.DataSource = lstUserRoles;
                RadGridPRole.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridPRole.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load role data."+ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
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
                this.LoadRoleGrid();
                lblisNewEntry.Text = "true";

            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation

            if (rtxtRole.Text == string.Empty)
            {
                Alert.Show("Role is empty");
                rtxtRole.Focus();
                return;
            }

            #endregion

            try
            {
                UserRole objUserRole=new UserRole();

                objUserRole.Role = rtxtRole.Text;
                objUserRole.Description = rtxtDescription.Text;
                objUserRole.CompanyId = _company.Id;

                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objUserRole.InsertUserRole();

                }
                else
                {
                    objUserRole.Id = int.Parse(lblId.Text);
                    success = objUserRole.UpdateUserRole();
                }
                if (success == 0)
                {
                    Alert.Show("Data not save succesfully");
                }
                else
                {
                    this.LoadRoleGrid();
                    ClearInfo();
                }


            }
            catch (Exception ex)
            {
                Alert.Show("somethig is going wrong to save data."+ex);
            }
        }


        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ClearInfo();
        }

        private void ClearInfo()
        {
            rtxtRole.Text = "";
            rtxtDescription.Text = "";
            lblId.Text = "";
        }

        protected void RadGridPRole_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;



            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                UserRole objcUserRole = new UserRole();

                int id = int.Parse(item["colId"].Text);
                delete = objcUserRole.DeleteUserRoleById(id);

                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    this.LoadRoleGrid();
                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;

                    rtxtRole.Text = item["colRole"].Text;
                    rtxtDescription.Text = item["colDescription"].Text;

                    lblisNewEntry.Text = "false";

                }
                catch (Exception ex)
                {
                    Alert.Show("Something is going wrong to select data." + ex);
                }
            }
        }

        protected void RadGridPRole_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            
        }

        protected void RadGridPRole_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            
        }
    }
}