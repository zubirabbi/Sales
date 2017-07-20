using SUL.Bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class SalesIncentiveInfo : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        private Company _company;
        private int companyId;


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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Area");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Area");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Area");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Area");
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
        private void LoadDegignation()
        {
            try
            {
                List<Designation> lstDesig = new Designation().GetAllDesignation();
                //lstDesig.Insert(0,new Designation());

                rdropDesignation.DataTextField = "DesignationName";
                rdropDesignation.DataValueField = "Id";

                rdropDesignation.DataSource = lstDesig;
                rdropDesignation.DataBind();
                if (lstDesig.Count == 2)
                    rdropDesignation.SelectedIndex = 1;
                else
                    rdropDesignation.SelectedIndex = 0;

            }
            catch (Exception ex) {
                Alert.Show(ex.Message);
            }
 
        }
        private void LoadGiftProductInfo()
        {
            try
            {
                DataTable dtProductInfo = new Product().GetProductFromViewList();
                rdropGiftProduct.DataValueField = "Id";
                rdropGiftProduct.DataTextField = "ProInfo";
                rdropGiftProduct.DataSource = dtProductInfo;
                rdropGiftProduct.DataBind();
                rdropGiftProduct.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("Someting is going into LoadProductInfo"+ex);
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
                lblisNewEntry.Text = "true";

                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                LoadDegignation();
                this.LoadGiftProductInfo();
 
            }
        }

        protected void radgridSalesIncentivesDetails_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                SalesIncentiveSetup objSalesIncentive=new SalesIncentiveSetup();
                objSalesIncentive.Designation = int.Parse(rdropDesignation.SelectedValue);
                objSalesIncentive.Startvalue =decimal.Parse( rtxtStartValue.Text);
                objSalesIncentive.EndValue =decimal.Parse( rtxtEndValue.Text);
                objSalesIncentive.Discount = decimal.Parse(rtxtPercentage.Text);
                objSalesIncentive.Amount =decimal.Parse( rtxtDiscountValue.Text);
                objSalesIncentive.GiftProduct = int.Parse(rdropGiftProduct.SelectedValue);
                objSalesIncentive.GiftQuantity = decimal.Parse (rtxtGiftQuantity.Text);

                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {

                    success = objSalesIncentive.InsertSalesIncentiveSetup();

                }
                else
                {

                    objSalesIncentive.Id = int.Parse(lblId.Text);
                    success = objSalesIncentive.UpdateSalesIncentiveSetup();

                }

                if (success == 0)
                {

                    Alert.Show("Data was not Save succesfully");

                }

                else
                {

                    Response.Redirect("SalesIncentiveSetup.aspx");

                }

            }

            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to save SalesIncentive" + ex);

            }



        }

        protected void rdropGiftProduct_DataBound(object sender, EventArgs e)
        {
            rdropGiftProduct.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropDesignation_DataBound(object sender, EventArgs e)
        {
           rdropDesignation.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropDesignation_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            
        }

    }
}