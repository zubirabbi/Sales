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
    public partial class WareHouseInformation : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        //private bool isNewEntry;


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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("WareHouse Information");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("WareHouse Information");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("WareHouse Information");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("WareHouse Information");
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

        private void LoadWarehouseCategory()
        {
            try
            {
                List<WarehouseCategory> lstWarehouseCategories=new WarehouseCategory().GetAllWarehouseCategory();

                lstWarehouseCategories.Insert(0,new WarehouseCategory());

                rdropWarehouseCat.DataTextField = "CategoryName";
                rdropWarehouseCat.DataValueField = "Id";
                rdropWarehouseCat.DataSource = lstWarehouseCategories;
                rdropWarehouseCat.DataBind();
                if (lstWarehouseCategories.Count == 2)
                    rdropWarehouseCat.SelectedIndex = 1;
                else
                    rdropWarehouseCat.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
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

        private void LoadWareHouseInfo()
        {
            try
            {
                DataTable dtWareHouse = new WareHouse().GetAllWareHousebyCompanyId(_company.Id);
                if (dtWareHouse.Rows.Count == 0)
                {
                    RadGridWareHouse.DataSource = new string[] { };
                    return;
                }
                RadGridWareHouse.DataSource = dtWareHouse;
                RadGridWareHouse.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Warehouse Grid." + ex);
            }
        }

        private void ClearAllInfo()
        {
            rtxtCode.Text = "";
            rtxtLoaction.Text = "";
            rtxtName.Text = "";
            rdropInCharge.SelectedIndex = 0;
            chkIsActive.Checked = false;

            lblisNewEntry.Text = "true";
            rtxtCode.Text = new WareHouse().GetlastwareHouseCode();
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
                rtxtCode.Text = new WareHouse().GetlastwareHouseCode();
                this.Loadincharge();
                this.LoadWareHouseInfo();
                this.LoadWarehouseCategory();
                lblisNewEntry.Text = "true";

            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation

            if (rtxtName.Text == string.Empty)
            {
                Alert.Show("Please enter a Warehouse name.");
                rtxtName.Focus();
                return;
            }
           
            if (rtxtCode.Text == string.Empty)
            {
                Alert.Show("Please enter a Warehouse code.");
                rtxtCode.Focus();
                return;
            }
            #endregion
            try
            {
                WareHouse objWareHouse = new WareHouse();

                objWareHouse.Name = rtxtName.Text;
                objWareHouse.Code = rtxtCode.Text;
                objWareHouse.Location = rtxtLoaction.Text;
                objWareHouse.Incharge = rdropInCharge.SelectedValue;
                objWareHouse.IsActive = chkIsActive.Checked;
                objWareHouse.CompanyId = _company.Id;
                objWareHouse.CategoryId = int.Parse(rdropWarehouseCat.SelectedValue);

                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objWareHouse.InsertWareHouse();
                }
                else
                {
                    objWareHouse.Id = int.Parse(lblId.Text);
                    success = objWareHouse.UpdateWareHouse();
                }
                if (success == 0)
                {
                    Alert.Show("Warehouse data was not save succesfully");

                }
                else
                {
                    this.LoadWareHouseInfo();
                    this.ClearAllInfo();
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Somethig is going wrong to save data." + ex);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearAllInfo();
        }

        protected void RadGridWareHouse_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadWareHouseInfo();
        }

        protected void RadGridWareHouse_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadWareHouseInfo();
        }

        protected void RadGridWareHouse_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;



            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                WareHouse objWareHouse = new WareHouse();

                int id = int.Parse(item["colId"].Text);
                delete = objWareHouse.DeleteWareHouseById(id);

                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    
                    this.LoadWareHouseInfo();
                    Alert.Show("Data Delete succesfully");
                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;
                    rtxtCode.Text = item["colCode"].Text;
                    rtxtName.Text = item["colName"].Text;
                    rtxtLoaction.Text = item["colLocation"].Text;
                    rdropInCharge.SelectedValue = item["colInchargeId"].Text == "&nbsp;" ? "0" : item["colInchargeId"].Text;
                    rdropWarehouseCat.SelectedValue = item["colCategoryId"].Text == "&nbsp;" ? "0" : item["colCategoryId"].Text;
                    chkIsActive.Checked = bool.Parse(item["colIsActive"].Text);


                    lblisNewEntry.Text = "false";
                }
                catch (Exception ex)
                {
                    Alert.Show("Something is going wrong to select data." + ex);
                }
            }
        }
    }
}