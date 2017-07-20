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
    public partial class DealerCategoryInfo : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        //private  bool isNewEntry;
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Dealer Category");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Dealer Category");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Dealer Category");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Dealer Category");
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
        private void LoadDealerCategoryGrid()
        {
            try
            {
                List<DealerCategory> lstDealerCategories = new DealerCategory().GetAllDealerCategory();
                if (lstDealerCategories.Count == 0)
                {
                    RadGridDealerCate.DataSource = new string[] { };
                    return;
                }
                else
                {
                    RadGridDealerCate.DataSource = lstDealerCategories;
                    RadGridDealerCate.DataBind();
                }
                if (!IsValidUpdateForUser())
                {
                    RadGridDealerCate.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {

                Alert.Show("Something is going wrong to load grid" + ex);
            }
        }

        private void clearDetails()
        {
            rtxtCatCode.Text = "";
            txtAreaDes.Value = null;
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
                this.LoadDealerCategoryGrid();
                lblisNewEntry.Text = "true";
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            int success;
            #region validation

            if (rtxtCatCode.Text == string.Empty)
            {
                Alert.Show("Please Enter The Category Code!!!");
                rtxtCatCode.Focus();
                return;
            }
            if (txtAreaDes.Value == null)
            {
                Alert.Show("Please Enter The Category Description!!!");
                txtAreaDes.Focus();
                return;
            }

            int cid = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);
            int codeExist = new DealerCategory().CheckForCodeExist(rtxtCatCode.Text, bool.Parse(lblisNewEntry.Text), cid);
            if (codeExist > 0)
            {
                Alert.Show("This Category code is Already Exist.");
                return;
            }
            #endregion

            try
            {
                DealerCategory objDealerCategory = new DealerCategory();

                objDealerCategory.CategoryCode = rtxtCatCode.Text;
                objDealerCategory.Description = txtAreaDes.Value;

                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objDealerCategory.InsertDealerCategory();
                }
                else
                {
                    int id = int.Parse(lblId.Text);
                    objDealerCategory.Id = id;
                    success = objDealerCategory.UpdateDealerCategory();
                }
                if (success == 0)
                {
                    Alert.Show("Dealer Category data was not save succesfully");
                }
                else
                {
                    //this.LoadDealerCategoryGrid();
                    //this.clearDetails();
                    Response.Redirect("DealerCategoryInfo.aspx");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to save data." + ex);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.clearDetails();
        }

        protected void RadGridDealerCate_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadDealerCategoryGrid();
        }

        protected void RadGridDealerCate_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadDealerCategoryGrid();

        }

        protected void RadGridDealerCate_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;
            DealerCategory objDealerCategory = new DealerCategory();
            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;

                int id = int.Parse(item["colId"].Text);
                List<DealerInformation> lstDealerInformations =
                    new DealerInformation().GetAllDealerCategoryInformation(id);
                if (lstDealerInformations.Count != 0)
                {
                    Alert.Show("This dealer code used in Dealer. you cannot delete this");
                    return;
                }
                delete = objDealerCategory.DeleteDealerCategoryById(id);
                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    //this.LoadDealerCategoryGrid();

                    Response.Redirect("DealerCategoryInfo.aspx");
                }
               
            }
            if (e.CommandName == "btnSelect")
            {
                GridDataItem item = (GridDataItem)e.Item;
                try
                {
                    lblId.Text = item["colId"].Text;
                    rtxtCatCode.Text = item["colCategoryCode"].Text;
                    txtAreaDes.Value = item["colCategoryDescription"].Text;

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