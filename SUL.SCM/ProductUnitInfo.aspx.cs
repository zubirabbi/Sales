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
    public partial class ProductUnitInfo : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        //private  bool isNewEntry;

        private bool IsValidSession()
        {
            if (Session["user"] == null)
            {
                return false;
            }

            _user = (Users)Session["user"];

            return _user.Id != 0;

        }
        private bool IsValidPageForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Product Unit");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Product Unit");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Product Unit");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Product Unit");
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

        private void LoadProductUniteGrid()
        {
            try
            {
                List<ProductUnit> lstProductUnite = new ProductUnit().GetAllProductUnit();

                if (lstProductUnite.Count == 0)
                    return;
                else
                {
                    RadGridProductUnite.DataSource = lstProductUnite;
                    RadGridProductUnite.DataBind();
                }
                if (!IsValidUpdateForUser())
                {
                    RadGridProductUnite.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {

                Alert.Show("Something is going wrong to load grid" + ex);
            }
        }

        private void clearDetails()
        {
            rtxtUnitCode.Text = "";
            txtUniteDes.Value = null;

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
                this.LoadProductUniteGrid();
                lblisNewEntry.Text = "true";
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            int success;
            #region validation

            if (rtxtUnitCode.Text == string.Empty)
            {
                Alert.Show("Please Enter The Unite Code!!!");
                rtxtUnitCode.Focus();
                return;
            }
            if (txtUniteDes.Value == null)
            {
                Alert.Show("Please Enter The Unite Description!!!");
                txtUniteDes.Focus();
                return;
            }
            #endregion

            try
            {
                ProductUnit objProductUnit = new ProductUnit();

                objProductUnit.UnitCode = rtxtUnitCode.Text;
                objProductUnit.UnitDescription = txtUniteDes.Value;

                if (lblisNewEntry.Text == "true")
                {
                    success = objProductUnit.InsertProductUnit();
                }
                else
                {
                    int id = int.Parse(lblId.Text);
                    objProductUnit.Id = id;
                    success = objProductUnit.UpdateProductUnit();
                }

                if (success == 0)
                {
                    Alert.Show("Product Category data was not save succesfully");
                }
                else
                {
                    //this.LoadProductUniteGrid();
                    //clearDetails();
                    Response.Redirect("ProductUnitInfo.aspx");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to save product category Data !!!!");
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.clearDetails();
        }

        protected void RadGridProductUnite_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;
           

           
            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                ProductUnit objProductUnit=new ProductUnit();

                int id = int.Parse(item["colId"].Text);
                delete = objProductUnit.DeleteProductUnitById(id);
                
                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                   // this.LoadProductUniteGrid();
                    Response.Redirect("ProductUnitInfo.aspx");
                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;
                    rtxtUnitCode.Text = item["colUnitCode"].Text;
                    txtUniteDes.Value = item["colUnitDescription"].Text;

                    lblisNewEntry.Text = "false";
                }
                catch (Exception ex)
                {
                    Alert.Show("Something is going wrong to select data." + ex);
                }
            }
        }

        protected void RadGridProductUnite_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadProductUniteGrid();
        }

        protected void RadGridProductUnite_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadProductUniteGrid();
        }
    }
}