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
    public partial class DepartmentInfo : System.Web.UI.Page
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Department");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Department");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Department");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Department");
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

        private void LoadDeptGrid()
        {
            try
            {
                DataTable dtDepartment = new Department().GetAllDepartmentbyCompanyId(_company.Id);
                if (dtDepartment.Rows.Count == 0)
                {
                    RadGridDept.DataSource = new string[] { };
                    return;
                }
                else
                {
                    RadGridDept.DataSource = dtDepartment;
                    RadGridDept.DataBind();
                }
                if (!IsValidUpdateForUser())
                {
                    RadGridDept.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {

                Alert.Show("Something is going wrong to load department data.");
            }

        }

        private void ClearDepartment()
        {
            rtxtDeptName.Text = "";
            rdropInCharge.SelectedIndex = 0;
            rtxtLocation.Text = "";
            rdropPerentDept.SelectedIndex = 0;
            chkIsActive.Checked = false;

            lblisNewEntry.Text = "true";
        }

        private void LoadPerentDepartment()
        {
            try
            {
                List<Department> lstDept = new Department().GetAllDepartment(_company.Id);

                lstDept.Insert(0, new Department());

                rdropPerentDept.DataTextField = "DepartmentName";
                rdropPerentDept.DataValueField = "Id";

                rdropPerentDept.DataSource = lstDept;
                rdropPerentDept.DataBind();
                if (lstDept.Count == 2)
                    rdropPerentDept.SelectedIndex = 1;
                else
                    rdropPerentDept.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Parent Department DropdownList" + ex);
            }
        }

        private void Loadincharge()
        {
            try
            {
                List<EmployeeInformation> lstEmployeeInformations =
                    new EmployeeInformation().GetAllEmployeeInformation(_company.Id);

                lstEmployeeInformations.Insert(0, new EmployeeInformation());

                rdropInCharge.DataTextField = "EmployeeName";
                rdropInCharge.DataValueField = "Id";

                rdropInCharge.DataSource = lstEmployeeInformations;
                rdropInCharge.DataBind();
                if (lstEmployeeInformations.Count == 2)
                    rdropInCharge.SelectedIndex = 1;
                else
                    rdropInCharge.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Incharge DropdownList" + ex);
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
                this.Loadincharge();
                this.LoadPerentDepartment();
                this.LoadDeptGrid();
                lblisNewEntry.Text = "true";

            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation

            if (rtxtDeptName.Text == string.Empty)
            {
                Alert.Show("Please enter department name.");
                rtxtDeptName.Focus();
                return;
            }
            int Did = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);

            int DeptId = new Department().ChecDepartmentExistance(Did, rtxtDeptName.Text, bool.Parse(lblisNewEntry.Text));
            if (DeptId > 0)
            {
                Alert.Show("This Department name is Already exits.");
                rtxtDeptName.Focus();
                return;
            }

            #endregion
            try
            {
                int success;
                Department objDepartment = new Department();

                objDepartment.DepartmentName = rtxtDeptName.Text;
                if (rdropPerentDept.SelectedIndex == null)
                {
                    objDepartment.PerentDepartmentId = 0;
                }
                else
                {
                    objDepartment.PerentDepartmentId = int.Parse(rdropPerentDept.SelectedValue);
                }
               
                objDepartment.Location = rtxtLocation.Text;
                objDepartment.InchargeId = (rdropInCharge.SelectedIndex <= 0) ? 0 : int.Parse(rdropInCharge.SelectedValue);
                objDepartment.IsActive = chkIsActive.Checked;
                objDepartment.CompanyId = _company.Id;

                if (lblisNewEntry.Text == "true")
                {
                    success = objDepartment.InsertDepartment();
                }
                else
                {
                    int id = int.Parse(lblId.Text);
                    objDepartment.Id = id;
                    success = objDepartment.UpdateDepartment();
                }
                if (success == 0)
                {
                    Alert.Show("Department data is not save succesfully");
                }
                else
                {
                    this.LoadDeptGrid();
                    this.ClearDepartment();
                    this.Loadincharge();
                    this.LoadPerentDepartment();
                    Response.Redirect("DepartmentInfo.aspx");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save department data");
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearDepartment();
        }

        protected void RadGridDept_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;



            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                Department objDepartment = new Department();

                int id = int.Parse(item["colId"].Text);
                delete = objDepartment.DeleteDepartmentById(id);

                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    this.LoadDeptGrid();
                    Response.Redirect("DepartmentInfo.aspx");
                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;
                    rtxtDeptName.Text = item["colDepartmentName"].Text;
                    rtxtLocation.Text = item["colLocation"].Text;
                    rdropInCharge.SelectedValue = item["colInchargeId"].Text == "&nbsp;" ? "0" : item["colInchargeId"].Text;
                    rdropPerentDept.SelectedValue = item["colPerentDepartmentId"].Text == "&nbsp;" ? "0" : item["colPerentDepartmentId"].Text;
                    chkIsActive.Checked = bool.Parse(item["colIsActive"].Text);


                    lblisNewEntry.Text = "false";
                }
                catch (Exception ex)
                {
                    Alert.Show("Something is going wrong to select data." + ex);
                }
            }
        }

        protected void RadGridDept_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadDeptGrid();
        }

        protected void RadGridDept_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadDeptGrid();
        }

    }
}