using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class ProductInfo : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        private Company _company; 
        private Hashtable deletedItems;

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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Product Info");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Product Info");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Product Info");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Product Info");
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

        private void LoadProductCategory()
        {
            try
            {
                List<ProductCategory> lstProductCategories = new ProductCategory().GetAllProductCategory();

                lstProductCategories.Insert(0, new ProductCategory());

                rdropProductCat.DataTextField = "CategoryCode";
                rdropProductCat.DataValueField = "Id";
                rdropProductCat.DataSource = lstProductCategories;
                rdropProductCat.DataBind();

                if (lstProductCategories.Count == 2)
                    rdropProductCat.SelectedIndex = 1;
                else
                    rdropProductCat.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Product Category Info." + ex);
            }

        }
        private void LoadColorList()
        {
            try
            {
                List<ListTable> lstTable = new ListTable().GetAllListTableByType("Color");

                RadListColor.DataTextField = "ListValue";
                RadListColor.DataValueField = "ListId";

                RadListColor.DataSource = lstTable;
                RadListColor.DataBind();

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Nationality DropdownList" + ex);
            }
        }
        private void LoadProductBaseUnit()
        {
            try
            {
                List<ProductUnit> lstProductUnits = new ProductUnit().GetAllProductUnit();
                lstProductUnits.Insert(0, new ProductUnit());

                rdropBaseunite.DataTextField = "UnitCode";
                rdropBaseunite.DataValueField = "Id";
                rdropBaseunite.DataSource = lstProductUnits;
                rdropBaseunite.DataBind();
                if (lstProductUnits.Count == 2)
                    rdropBaseunite.SelectedIndex = 1;
                else
                    rdropBaseunite.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Product Base unit." + ex);
            }
        }

        private void LoadColorMapping(int productId)
        {
            try
            {
                List<ListTable> lstTable = new ListTable().GetAllListTableByTypeAndPid("Color", productId);

                RadListBoxDestination.DataTextField = "ListValue";
                RadListBoxDestination.DataValueField = "ListId";

                RadListBoxDestination.DataSource = lstTable;
                RadListBoxDestination.DataBind();

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Nationality DropdownList" + ex);
            }
        }

        private void ClearProductInfo()
        {
            rtxtProductCode.Text = "";
            rtxtProductName.Text = "";
            rdropProductCat.SelectedIndex = 0;
            rdropBaseunite.SelectedIndex = 0;
            rtxtModelNo.Text = "";
            rtxtDP.Text = "";
            rtxtMRP.Text = "";
            rtxtCostPrice.Text = "";
            rtxtRP.Text = "";

            lblisNewEntry.Text = "true";
        }

        private void LoadProductInfoGrid()
        {
            try
            {
                DataTable dtProducts = new Product().GetAllProductFromViewList();

                RadGridProducr.DataSource = dtProducts;
                RadGridProducr.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridProducr.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going  wrong to load product grid." + ex);
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
            if (Session["deletedItems"] != null)
            {
                deletedItems = (Hashtable)Session["deletedItems"];
            }
            else
            {
                deletedItems = new Hashtable();
            }
            if (!IsPostBack)
            {
                lblisNewEntry.Text = "true";
                deletedItems = new Hashtable();
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                if (_user.UserName.ToLower() == "super")
                {
                    dp2Panel.Visible = true;
                }
                else
                {
                    dp2Panel.Visible = false;
                }
                this.LoadProductBaseUnit();
                this.LoadProductCategory();
                this.LoadColorList();
                this.LoadProductInfoGrid();
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {


            try
            {
                int id;
                if (lblId.Text == string.Empty)
                    id = 0;
                else
                    id = int.Parse(lblId.Text);
                #region validation

                if (rtxtProductCode.Text == string.Empty)
                {
                    Alert.Show("Please enter Product code");
                    rtxtProductCode.Focus();
                    return;
                }
                int productCode = new Product().CheckProductCodeExistance(id, rtxtProductCode.Text, bool.Parse(lblisNewEntry.Text));

                if (productCode > 0)
                {
                    Alert.Show("Prdouct code is already exist. Please enter a new Product code");
                    rtxtProductCode.Focus();
                    return;
                }
                if (rtxtProductName.Text == string.Empty)
                {
                    Alert.Show("Please enter Product Name.");
                    rtxtProductName.Focus();
                    return;
                }
                int productName = new Product().CheckProductNameExistance(id, rtxtProductName.Text, bool.Parse(lblisNewEntry.Text));

                if (productName > 0)
                {
                    Alert.Show("Prdouct name is already exist. Please enter a new Product name.");
                    rtxtProductName.Focus();
                    return;
                }

                if (rtxtModelNo.Text == string.Empty)
                {
                    Alert.Show("Please enter Product Model no.");
                    rtxtModelNo.Focus();
                    return;
                }

                //int modelno = new Product().CheckProductModelExistance(id, rtxtModelNo.Text, isNewEntry);
                //if (modelno > 0)
                //{
                //    Alert.Show("model no is already exist. Please enter a new model no.");
                //    rtxtModelNo.Focus();
                //    return;
                //}
                if (rdropBaseunite.SelectedIndex <= 0)
                {
                    Alert.Show("Please Select Base Unit.");
                    rdropBaseunite.Focus();
                    return;
                }
                if (rdropProductCat.SelectedIndex <= 0)
                {
                    Alert.Show("Please Select Product Category.");
                    rdropProductCat.Focus();
                    return;
                }

                int pid = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);
                int codeExist = new Product().CheckForCodeExist(rtxtProductCode.Text, bool.Parse(lblisNewEntry.Text), pid);
                if (codeExist > 0)
                {
                    Alert.Show("This Product code is Already Exist.");
                    return;
                }

                #endregion

                Product objproduct = new Product();

                objproduct.ProductCode = rtxtProductCode.Text;
                objproduct.ProductName = rtxtProductName.Text;
                objproduct.ModelNo = rtxtModelNo.Text;
                objproduct.ProductCategory = int.Parse(rdropProductCat.SelectedValue);
                objproduct.BaseUnit = int.Parse(rdropBaseunite.SelectedValue);
                objproduct.ModelNo = rtxtModelNo.Text;

                objproduct.MRP = rtxtMRP.Text == string.Empty ? 0 : decimal.Parse(rtxtMRP.Text);
                objproduct.DP = rtxtDP.Text == string.Empty ? 0 : decimal.Parse(rtxtDP.Text);
                objproduct.DP2 = rtxtDP2.Text == string.Empty ? 0 : decimal.Parse(rtxtDP2.Text);
                objproduct.RP = rtxtRP.Text == string.Empty ? 0 : decimal.Parse(rtxtRP.Text);
                objproduct.CostPrice = rtxtCostPrice.Text == string.Empty ? 0 : decimal.Parse(rtxtCostPrice.Text);

                objproduct.CurrentBalance = decimal.Parse(rtxtCurrentPrice.Text);

                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objproduct.InsertProduct();
                }
                else
                {
                    objproduct.Id = id;
                    success = objproduct.UpdateProduct();
                }
                if (success == 0)
                    Alert.Show("something is going wrong to save product data.");
                else
                {
                    //this.LoadProductInfoGrid();
                    //this.ClearProductInfo();
                    if (bool.Parse(lblisNewEntry.Text) == false)
                    {
                        if (lbldeletedItemCount.Text!=string.Empty && int.Parse(lbldeletedItemCount.Text) > 0)
                        {
                            string deleteIds = string.Empty;
                            foreach (DictionaryEntry dict in deletedItems)
                            {
                                if (deleteIds == string.Empty)
                                    deleteIds = " Color = " + dict.Value.ToString();
                                else
                                    deleteIds = deleteIds + " Or Color =" + dict.Value.ToString() + "";
                            }

                            Session["deletedItems"] = null;
                            deletedItems = new Hashtable();

                            int delete = new ProductColor().DeleteProductMappingByColorIdAndProductId(int.Parse(lblId.Text), deleteIds);

                            if (delete == 0)
                                Alert.Show("somethings is going wrong to delete product Color");
                            else
                            {
                            }
                        }
                    }
                    foreach (RadListBoxItem item in RadListBoxDestination.Items)
                    {
                        int colorId= int.Parse(item.Value);

                        if (lblId.Text == string.Empty)
                        {
                            lblId.Text = "0";
                        }

                        int productIdExist = new ProductColor().CheckColorIdExistance(colorId, int.Parse(lblId.Text));
                        if (productIdExist == 0)
                        {
                            ProductColor objProductColor = new ProductColor();

                            //objProductColor.ProductId = new Product().GetlastProductId();
                            objProductColor.ProductId = int.Parse(lblId.Text);
                            objProductColor.Color = colorId;



                            success = objProductColor.InsertProductColor();


                            if (success == 0)
                            {
                                Alert.Show("Product color data is not save succesfully");
                                return;
                            }
                            else
                            {
                                Alert.Show("Product color data save succesfully");
                            }

                        }
                        else
                        {

                        }
                    }

                    Response.Redirect("ProductInfo.aspx");

                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save product data." + ex);
            }

        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearProductInfo();
        }

        protected void RadGridProducr_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;



            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                Product objproduct = new Product();
                ProductColor objcolor=new ProductColor();
                int id = int.Parse(item["colId"].Text);
                delete = objproduct.DeleteProductById(id);
                int deletecolor = objcolor.DeleteProductColorByProductIdId(id);

                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    this.LoadProductInfoGrid();
                    Response.Redirect("ProductInfo.aspx");
                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;
                    rtxtProductCode.Text = item["colProductCode"].Text;
                    rtxtProductName.Text = item["colProductName"].Text;
                    rtxtModelNo.Text = item["colModelNo"].Text;
                    rdropBaseunite.SelectedValue = item["colBaseUnit"].Text;
                    rdropProductCat.SelectedValue = item["colProductCategory"].Text;
                    rtxtMRP.Text = item["colMRP"].Text;
                    rtxtDP.Text = item["colDP"].Text;
                    rtxtDP2.Text = item["colDP2"].Text;
                    rtxtCostPrice.Text = item["colCostPrice"].Text;
                    rtxtRP.Text = item["colRP"].Text;

                    LoadColorMapping(int.Parse(lblId.Text));

                    lblisNewEntry.Text = "false";
                }
                catch (Exception ex)
                {
                    Alert.Show("Something is going wrong to select data." + ex);
                }
                if (e.CommandName.Equals(RadGrid.FilterCommandName))
                {
                    this.LoadProductInfoGrid(); ;
                }
            }
        }

        protected void RadGridProducr_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            //this.LoadProductInfoGrid();
        }

        protected void RadGridProducr_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            //this.LoadProductInfoGrid();
        }

        protected void RadListBoxDestination_OnDeleted(object sender, RadListBoxEventArgs e)
        {
            //throw new NotImplementedException();
            var items = e.Items;
            int slno = (lbldeletedItemCount.Text == string.Empty) ? 1 : int.Parse(lbldeletedItemCount.Text) + 1;

            lbldeletedItemCount.Text = slno.ToString();
            deletedItems.Add(slno, items[0].Value);

            Session["deletedItems"] = deletedItems;
        }
    }
}