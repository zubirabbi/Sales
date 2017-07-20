using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using hrms;
using SUL.Bll;
using System.Text.RegularExpressions;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class MainProductInfo : System.Web.UI.Page
    {
        private Users _user;
        private Company _company;
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

            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Monthly Sales Report") : new AppFunctionality().GetAppFunctionalityId("Monthly Sales Report", int.Parse(lblsource.Text));
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            Session["PermissionUser"] = PermissionUser;

            //if (!PermissionUser.IsView)
            //{
            //    AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
            //        _user.CompanyId);
            //    bool permission = Permission.IsView;
            //    return permission;
            //}
            //else
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

        private void LoadMainProduct()
        {
            try
            {
                DataTable dtProductInfo = new Product().GetProductFromViewList();

                rdropMainProduct.DataValueField = "Id";
                rdropMainProduct.DataTextField = "proInfo";
                rdropMainProduct.DataSource = dtProductInfo;
                rdropMainProduct.DataBind();

                rdropMainProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going on to load Product Info" + ex);
            }
        }

        private void LoadSpairProducts()
        {
            try
            {
                DataTable dtProductInfo = new Product().GetSpairProductFromViewList();

                rdropSpairParts.DataValueField = "Id";
                rdropSpairParts.DataTextField = "proInfo";
                rdropSpairParts.DataSource = dtProductInfo;
                rdropSpairParts.DataBind();

                rdropSpairParts.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going on to load Product Info" + ex);
            }
        }

        private void LoadSpairParts()
        {
            try
            {
                DataTable dtSpairParts = new SpairParts().GetSpairPartsFromViewList(int.Parse(rdropMainProduct.SelectedValue));
                if (dtSpairParts.Rows.Count <= 0)
                {
                    RadGridAddRequisitionDetails.DataSource = new string[]{};
                    RadGridAddRequisitionDetails.DataBind();
                }
                RadGridAddRequisitionDetails.DataSource = dtSpairParts;
                RadGridAddRequisitionDetails.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load data. "+ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>

        protected void Page_Load(object sender, EventArgs e)
        {
            lblsource.Text = Request.QueryString["source"] ?? "0";
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
                this.LoadMainProduct();
                this.LoadSpairProducts();
                //this.LoadSpairParts();
                this.ClearAllInfo();
            }
        }
        protected void rdropMainProduct_OnDataBound(object sender, EventArgs e)
        {
           rdropMainProduct.Items.Insert(0,new RadComboBoxItem());
        }

        protected void rdropSpairParts_OnDataBound(object sender, EventArgs e)
        {
            rdropSpairParts.Items.Insert(0,new RadComboBoxItem());
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                Regex regexForQuentity = new Regex("^[0-9]*$");
                #region validation
                if (rdropMainProduct.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Product.");
                    rdropMainProduct.Focus();
                    return;
                }
                if (rdropSpairParts.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Spairs Parts.");
                    rdropSpairParts.Focus();
                    return;
                }
                if (rtxtQuantity.Text == string.Empty || !regexForQuentity.IsMatch(rtxtQuantity.Text))
                {
                    Alert.Show("Please enter a valid quantity");
                    rtxtQuantity.Focus();
                    return;
                }
                #endregion

                SpairParts objSpairParts=new SpairParts();

                objSpairParts.ProductId = Convert.ToInt32(rdropMainProduct.SelectedValue);
                objSpairParts.SpairPartId = Convert.ToInt32(rdropSpairParts.SelectedValue);
                objSpairParts.Quentity = Convert.ToInt32(rtxtQuantity.Text);

                int success;

                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objSpairParts.InsertSpairParts();
                }
                else
                {
                    objSpairParts.Id = Convert.ToInt32(lblId.Text);

                    success = objSpairParts.UpdateSpairParts();
                }
                if (success == 0)
                {
                    Alert.Show("Data is not save succesfully");
                }
                else
                {
                    Alert.Show("Data Save Succesfully");
                    ClearAllInfo();
                }

            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        private void ClearAllInfo()
        {
            rdropMainProduct.SelectedIndex = 0;
            rdropSpairParts.SelectedIndex = 0;
            rtxtQuantity.Text = "";
            lblisNewEntry.Text = "true";
            lblId.Text = "";
            RadGridAddRequisitionDetails.DataSource = new string[] {};
            RadGridAddRequisitionDetails.DataBind();
            this.LoadSpairParts();

        }

        protected void RadGridAddRequisitionDetails_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;

            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                SpairParts objSpairParts = new SpairParts();
                int id = int.Parse(item["colId"].Text);
                delete = objSpairParts.DeleteSpairPartsById(id);
                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    this.LoadSpairParts();
                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {
                    GridDataItem item = (GridDataItem) e.Item;

                    lblId.Text = item["colId"].Text;
                    rtxtQuantity.Text = item["colQuantity"].Text;
                    rdropMainProduct.SelectedValue = item["colProductId"].Text;
                    rdropSpairParts.SelectedValue = item["colSpairPartId"].Text;
                    lblisNewEntry.Text = "false";
                }
                catch (Exception ex)
                {
                    Alert.Show("Something is going wrong to edit spair parts."+ex);
                }
            }
            }

        protected void RadGridAddRequisitionDetails_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadSpairParts();
        }

        protected void RadGridAddRequisitionDetails_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadSpairParts();
        }

        protected void rdropMainProduct_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            this.LoadSpairParts();
        }
    }
}