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
    public partial class DesignationInfo : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        //private  bool isNewEntry = true;


        private static Company _company;
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Designation");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Designation");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Designation");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Designation");
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
        private void LoadDesigGrid()
        {
            try
            {
                DataTable dtDesignation = new Designation().GetAllDesignationbyCompanyId(_company.Id);
                if (dtDesignation.Rows.Count == 0)
                    return;
                else
                {
                    RadGridDesig.DataSource = dtDesignation;
                    RadGridDesig.DataBind();
                }
                if (!IsValidUpdateForUser())
                {
                    RadGridDesig.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {

                Alert.Show("Something is going wrong to load designation data.");
            }

        }

        private void LoadDepartment()
        {
            try
            {
                List<Department> lstDept = new Department().GetAllDepartment(_company.Id);

                lstDept.Insert(0, new Department());

                rdropDept.DataTextField = "DepartmentName";
                rdropDept.DataValueField = "Id";

                rdropDept.DataSource = lstDept;
                rdropDept.DataBind();
                if (lstDept.Count == 2)
                    rdropDept.SelectedIndex = 1;
                else
                    rdropDept.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Parent Department DropdownList" + ex);
            }
        }

        private void LoadUpperHierKey()
        {
            DataTable dtUpperHireKey = new Designation().GetDesignationByLeveId(int.Parse(rdropLevel.SelectedValue));

            rdropUpperHier.DataTextField = "DsigInfo";
            rdropUpperHier.DataValueField = "Id";
            rdropUpperHier.DataSource = dtUpperHireKey;
            rdropUpperHier.DataBind();

            if (dtUpperHireKey.Rows.Count == 2)
            {
                rdropUpperHier.SelectedIndex = 1;
            }
            else
                rdropUpperHier.SelectedIndex = 0;

        }

        private void LoadLevelCombo()
        {
            ListTable objlisttable = new ListTable();
            List<ListTable> listtablelist = objlisttable.GetAllListTableByType("Level");

            listtablelist.Insert(0, new ListTable());

            rdropLevel.DataTextField = "ListValue";
            rdropLevel.DataValueField = "ListId";
            rdropLevel.DataSource = listtablelist;
            rdropLevel.DataBind();

            if (listtablelist.Count == 2)
                rdropLevel.SelectedIndex = 1;
            else
                rdropLevel.SelectedIndex = 0;

        }
        private void ClearDesignation()
        {
            rtxtDesigName.Text = "";
            rdropDept.SelectedIndex = 0;
            rtxtResponcibility.Text = "";

            chkIsActive.Checked = true;
            lblId.Text = "";
            rtxtDesigCode.Text = "";
            rdropLevel.SelectedIndex = 0;
            rdropUpperHier.SelectedIndex = 0;
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
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                this.LoadDepartment();
                this.LoadDesigGrid();
                this.LoadLevelCombo();

                lblisNewEntry.Text = "true";
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation

            if (rtxtDesigName.Text == string.Empty)
            {
                Alert.Show("Please enter designation name.");
                rtxtDesigName.Focus();
                return;
            }
            //if (rdropDept.SelectedIndex <= 0)
            //{
            //    Alert.Show("Please select depertment for designation.");
            //    rdropDept.Focus();
            //    return;
            //}
            if (rtxtDesigCode.Text == string.Empty)
            {
                Alert.Show("Please enter designation code.");
                rtxtDesigCode.Focus();
                return;
            }
            int Desigid = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);

            int DId = new Designation().CheckDesigExistance(Desigid, rtxtDesigName.Text, int.Parse(rdropDept.SelectedValue),bool.Parse(lblisNewEntry.Text));
            if (DId > 0)
            {
                Alert.Show("This Designation name is Already exits.");
                rtxtDesigName.Focus();
                return;
            }

            #endregion
            try
            {
                int success;
                Designation objDesignation = new Designation();

                objDesignation.DesignationName = rtxtDesigName.Text;
                objDesignation.DepartmentId = int.Parse(rdropDept.SelectedValue);
                objDesignation.Responsibility = rtxtResponcibility.Text;
                objDesignation.IsActive = chkIsActive.Checked;
                objDesignation.DesignationCode = rtxtDesigCode.Text;
                objDesignation.Level = rdropLevel.SelectedIndex <= 0 ? 0 : int.Parse(rdropLevel.SelectedValue);
                objDesignation.UpperHierarchy = rdropUpperHier.SelectedIndex <= 0 ? 0 : int.Parse(rdropUpperHier.SelectedValue);

                if (bool.Parse(lblisNewEntry.Text) == true)
                {
                    success = objDesignation.InsertDesignation();
                }
                else
                {
                    objDesignation.Id = int.Parse(lblId.Text);
                    success = objDesignation.UpdateDesignation();
                }
                if (success == 0)
                {
                    Alert.Show("Designation data is not save succesfully");
                }
                else
                {
                    //this.LoadDesigGrid();
                    //this.ClearDesignation();
                    //this.LoadDepartment();
                    Response.Redirect("DesignationInfo.aspx");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Error occured during designation save. " + ex.Message);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearDesignation();
        }

        protected void RadGridDesig_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;



            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                Designation objDesignation = new Designation();

                int id = int.Parse(item["colId"].Text);
                delete = objDesignation.DeleteDesignationById(id);

                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    this.LoadDesigGrid();
                    Response.Redirect("DesignationInfo.aspx");
                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    this.LoadUpperHierKey();
                    lblId.Text = item["colId"].Text;
                    rtxtDesigName.Text = item["colDesignationName"].Text;
                    rtxtResponcibility.Text = item["colResponsibility"].Text;
                    rdropDept.SelectedValue = item["colDepartmentId"].Text == "&nbsp;" ? "0" : item["colDepartmentId"].Text;
                    chkIsActive.Checked = bool.Parse(item["colIsActive"].Text);
                    rtxtDesigCode.Text = item["colDesignationCode"].Text;
                    rdropLevel.SelectedValue = item["colLevel"].Text == "" ? "0" : item["colLevel"].Text;
                    rdropUpperHier.SelectedValue = item["colUpperHierarchy"].Text;

                    lblisNewEntry.Text = "false";
                }
                catch (Exception ex)
                {
                    Alert.Show("Something is going wrong to select data." + ex);
                }
            }
        }

        protected void RadGridDesig_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadDesigGrid();
        }

        protected void RadGridDesig_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadDesigGrid();
        }

        protected void rdropUpperHier_OnDataBound(object sender, EventArgs e)
        {
            rdropUpperHier.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropLevel_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadUpperHierKey();
        }
    }
}