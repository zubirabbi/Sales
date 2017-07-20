using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class EmployeeRoleAssignment : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  Company _company;
        //private  bool isNewEntry ;

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
        private void LoadRoleListCombo()
        {

            List<UserRole> lstUserRole = new UserRole().GetAllUserRole(_company.Id);

            lstUserRole.Insert(0, new UserRole());

            rdropList.DataTextField = "Role";
            rdropList.DataValueField = "Id";
            rdropList.DataSource = lstUserRole;
            rdropList.DataBind();


            rdropList.SelectedIndex = 0;
        }
        private void LoadUserListCombo()
        {

            List<Users> lstUser = new Users().GetAllUsers(_company.Id);

            lstUser.Insert(0, new Users());
            rdropList.DataTextField = "UserName";
            rdropList.DataValueField = "Id";
            rdropList.DataSource = lstUser;
            rdropList.DataBind();


            rdropList.SelectedIndex = 0;
        }

        private DataTable GetDatatable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("PermitionId", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Id", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Functionality", typeof(System.String)));
            dt.Columns.Add(new DataColumn("IsInsert", typeof(System.Boolean)));
            dt.Columns.Add(new DataColumn("IsUpdate", typeof(System.Boolean)));
            dt.Columns.Add(new DataColumn("IsDelete", typeof(System.Boolean)));
            dt.Columns.Add(new DataColumn("IsView", typeof(System.Boolean)));
            dt.Columns.Add(new DataColumn("IsApprove", typeof(System.Boolean)));

            return dt;
        }

        private void LoadAppfunctionality()
        {
            List<AppFunctionality> objAppFunctionlity = new AppFunctionality().GetAllAppFunctionality(_company.Id);
            DataTable dtFunc = GetDatatable();

            foreach (AppFunctionality func in objAppFunctionlity)
            {
                DataRow row = dtFunc.NewRow();
                row["PermitionId"] = 0;
                row["Id"] = func.Id;
                row["Functionality"] = func.Functionality;
                row["IsInsert"] = false;
                row["IsUpdate"] = false;
                row["IsDelete"] = false;
                row["IsView"] = false;
                row["IsApprove"] = false;

                dtFunc.Rows.Add(row);
            }

            RadGridAppFunction.DataSource = dtFunc;
            RadGridAppFunction.DataBind();

        }

        private DataTable GetDatatablePermition()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("PermitionId", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Id", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Functionality", typeof(System.String)));
            dt.Columns.Add(new DataColumn("IsInsert", typeof(System.Boolean)));
            dt.Columns.Add(new DataColumn("IsUpdate", typeof(System.Boolean)));
            dt.Columns.Add(new DataColumn("IsDelete", typeof(System.Boolean)));
            dt.Columns.Add(new DataColumn("IsView", typeof(System.Boolean)));
            dt.Columns.Add(new DataColumn("IsApprove", typeof(System.Boolean)));

            return dt;
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
                if (!IsValidUpdateForUser())
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                }
                lblisNewEntry.Text = "true";
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                foreach (GridDataItem item in RadGridAppFunction.Items)
                {
                    //GridDataItem Item1 = (GridDataItem)item;

                    int PerId = int.Parse(item["colPerId"].Text);
                    int Id = int.Parse(item["colId"].Text);

                    CheckBox isview = (CheckBox)item.FindControl("chkIsView");
                    bool IsViewList = isview.Checked;

                    CheckBox isInsert = (CheckBox)item.FindControl("chkIsInsert");
                    bool IsInsertList = isInsert.Checked;

                    CheckBox isupdate = (CheckBox)item.FindControl("chkIsUpdate");
                    bool IsUpadteList = isupdate.Checked;

                    CheckBox isdelete = (CheckBox)item.FindControl("chkIsDelete");
                    bool IsDeleteList = isdelete.Checked;

                    CheckBox isApprove = (CheckBox)item.FindControl("chkIsApprove");
                    bool IsApproveList = isApprove.Checked;

                    AppPermission objAppPermission = new AppPermission();

                    if (rdeopType.SelectedValue == "Role")
                    {
                        objAppPermission.RoleId = int.Parse(rdropList.SelectedValue);
                        objAppPermission.UserId = 0;
                    }
                    else
                    {
                        objAppPermission.UserId = int.Parse(rdropList.SelectedValue);
                        objAppPermission.RoleId = 0;
                    }

                    objAppPermission.CompanyId = _company.Id;
                    objAppPermission.FunctionalityId = Id;
                    objAppPermission.IsView = IsViewList;
                    objAppPermission.IsInsert = IsInsertList;
                    objAppPermission.IsUpdate = IsUpadteList;
                    objAppPermission.IsDelete = IsDeleteList;
                    objAppPermission.IsApprove = IsApproveList;

                    int success = 0;
                    if (bool.Parse(lblisNewEntry.Text))
                        success = objAppPermission.InsertAppPermission();
                    else
                    {
                        objAppPermission.Id = PerId;
                        success = objAppPermission.UpdateAppPermission();
                    }
                    if (success == 0)
                    {
                        Alert.Show("AppPermition was not saved successfully. Please retry.");
                    }
                    else
                    {
                        Alert.Show("AppPermition saved successfully. .");
                    }
                }
            }
            catch (Exception exp)
            {
                Alert.Show("Something is Going Wrong!!!" + exp);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {

        }

        protected void rdropList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            List<AppPermission> listR = new List<AppPermission>();
            if (rdeopType.SelectedValue == "Role")
            {
                listR =
                   new AppPermission().GetAllAppPermission(_company.Id)
                       .Where(ulist => ulist.RoleId == int.Parse(rdropList.SelectedValue))
                       .ToList();
            }
            else
            {
                listR =
                    new AppPermission().GetViewAllAppPermission(_company.Id)
                        .Where(ulist => ulist.UserId == int.Parse(rdropList.SelectedValue))
                        .ToList();
            }

            if (listR.Count == 0)
            {
                this.LoadAppfunctionality();
                lblisNewEntry.Text = "true";
            }
            else
            {
                lblisNewEntry.Text = "false";

                List<AppPermission> objApppermision = new List<AppPermission>();
                if (rdeopType.SelectedValue == "Role")
                {
                    objApppermision =
                        new AppPermission().GetViewAllAppPermission(_company.Id)
                            .Where(ulist => ulist.RoleId == int.Parse(rdropList.SelectedValue))
                            .ToList();
                }
                else
                {
                    objApppermision =
                        new AppPermission().GetViewAllAppPermission(_company.Id)
                            .Where(ulist => ulist.UserId == int.Parse(rdropList.SelectedValue))
                            .ToList();
                }

                DataTable dtpermition = GetDatatablePermition();

                foreach (AppPermission func in objApppermision)
                {
                    DataRow row = dtpermition.NewRow();
                    row["PermitionId"] = func.Id;
                    row["Id"] = func.FunctionalityId;
                    row["Functionality"] = func.FunctionalityName;
                    row["IsInsert"] = func.IsInsert;
                    row["IsUpdate"] = func.IsUpdate;
                    row["IsDelete"] = func.IsDelete;
                    row["IsView"] = func.IsView;
                    row["IsApprove"] = func.IsApprove;

                    dtpermition.Rows.Add(row);
                }
                RadGridAppFunction.DataSource = dtpermition;
                RadGridAppFunction.DataBind();
            }
        }

        protected void rdeopType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdeopType.SelectedValue == "Users")
            {
                this.LoadUserListCombo();

            }
            else
            {
                this.LoadRoleListCombo();
            }
        }
        protected void RadWages_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem hItem = (GridHeaderItem)e.Item;
                CheckBox chk1 = (CheckBox)hItem.FindControl("chkall");
                HiddenField1.Value = chk1.ClientID.ToString();
            }
        }
    }
}