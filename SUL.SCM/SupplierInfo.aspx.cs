using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;

namespace SUL.SCM
{
    public partial class SupplierInfo : System.Web.UI.Page
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier Add");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier Add");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier Add");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier Add");
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

        private void LoadCountry()
        {
            try
            {
                List<Country> lstCountries = new Country().GetAllCountry();
                lstCountries.Insert(0, new Country());

                rdropCountry.DataTextField = "Name";
                rdropCountry.DataValueField = "Id";
                rdropCountry.DataSource = lstCountries;
                rdropCountry.DataBind();

                if (lstCountries.Count == 2)
                    rdropCountry.SelectedIndex = 1;
                else
                    rdropCountry.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load country" + ex);
            }
        }

        private void LoadProductCategory()
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

        private void LoadProduct(int supplierid)
        {
            if (rdropProductCat.SelectedIndex <= 0)
            {
                List<Product> lstProducts = new Product().GetAllProductbyProduct(supplierid);

                RadListProduct.DataTextField = "ProductName";
                RadListProduct.DataValueField = "Id";
                RadListProduct.DataSource = lstProducts;
                RadListProduct.DataBind();
            }
            else
            {
                List<Product> lstProducts = new Product().GetAllProductbyProductCatId(int.Parse(rdropProductCat.SelectedValue), supplierid);

                RadListProduct.DataTextField = "ProductName";
                RadListProduct.DataValueField = "Id";
                RadListProduct.DataSource = lstProducts;
                RadListProduct.DataBind();
            }
        }

        private void LoadProductMapping(int supplierid)
        {
            List<Product> lstProducts = new Product().GetAllProductbyProductForList(supplierid);

            RadListBoxDestination.DataTextField = "ProductName";
            RadListBoxDestination.DataValueField = "Id";
            RadListBoxDestination.DataSource = lstProducts;
            RadListBoxDestination.DataBind();
        }

        //=========clear Supplier=========\
        private void ClearSupplieInfo(bool allClear)
        {
            rtxtName.Text = "";
            rtxtComAddress.Text = "";
            rtxtPhone.Text = "";
            rtxtCode.Text = "";
            rtxtFactory.Text = "";
            rtxtMobile.Text = "";
            lblId.Text = "";

            if (allClear == true)
                lblisNewEntry.Text = "true";
            else
                lblisNewEntry.Text = "false";


        }


        //===========clear Product tagging==========\\
        private void clearProductTagging()
        {
            rdropProductCat.SelectedIndex = 0;
            lblisNewEntryForProductTagging.Text = "true";
            lbldeletedItemCount.Text = "0";
            deletedItems = new Hashtable();
        }
        //========Clear SupplierBank======\\
        private void cleanSupplierbankBank()
        {
            rtxtBankName.Text = "";
            rtxtAccOn.Text = "";
            rtxtAccTitle.Text = "";
            rtxtSWIFTCode.Text = "";
            chkIsDefault.Checked = false;

            lblisNewEntryForSupplierBank.Text = "true";
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
                deletedItems=new Hashtable();
            }
            if (!IsPostBack)
            {
                lblisNewEntry.Text = "true";
                lblisNewEntryForProductTagging.Text = "true";
                lblisNewEntryForSupplierBank.Text = "true";

                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }


                this.LoadCountry();
                lbldeletedItemCount.Text = "0";
                deletedItems = new Hashtable();

                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];

                    Supplier objsupplier = new Supplier().GetSupplierById(int.Parse(id));

                    rtxtName.Text = objsupplier.Name;
                    rtxtCode.Text = objsupplier.Code.ToUpper();
                    rtxtComAddress.Text = objsupplier.CompanyAddress;
                    rtxtFactory.Text = objsupplier.FactoryAddress;
                    rtxtPhone.Text = objsupplier.Phone.ToString();
                    rtxtMobile.Text = objsupplier.Mobile.ToString();
                    rtxtContractPerson.Text = objsupplier.ContactPerson;
                    rtxtDesignation.Text = objsupplier.Designation;

                    lblId.Text = id;

                    lblisNewEntry.Text = "false";
                    this.LoadProductCategory();
                    this.LoadProduct(int.Parse(lblId.Text));
                    this.LoadProductMapping(int.Parse(lblId.Text));

                    lblisNewEntryForProductTagging.Text = "false";

                    BankInformation objBankInfo = new BankInformation().GetBankInformationBySupplierId(int.Parse(id), "Supplier");

                    rtxtBankName.Text = objBankInfo.BankName;
                    rtxtAccOn.Text = objBankInfo.AccountNo;
                    rtxtAccTitle.Text = objBankInfo.AccountTitle;
                    rtxtSWIFTCode.Text = objBankInfo.SWIFTCode;
                    rdropCountry.SelectedValue = objBankInfo.Country.ToString();
                    lblbankId.Text = objBankInfo.Id.ToString();
                    chkIsDefault.Checked = objBankInfo.IsDefault;
                    lblisNewEntryForSupplierBank.Text = "false";

                }
            }
        }
        //------------------Save Supplier-----------------\\
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                #region validation

                if (rtxtName.Text == string.Empty)
                {
                    Alert.Show("Please enter Supplier name.");
                    rtxtName.Focus();
                    return;
                }
                if (rtxtCode.Text == string.Empty)
                {
                    Alert.Show("please enter supplier Code");
                    rtxtCode.Focus();
                    return;
                }
                if (rtxtComAddress.Text == string.Empty)
                {
                    Alert.Show("Please enter Company Address");
                    rtxtComAddress.Focus();
                    return;
                }
                int id = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);

                int suppId = new Supplier().CheckSupplierExistance(id, rtxtCode.Text, bool.Parse(lblisNewEntry.Text));
                if (suppId > 0)
                {
                    Alert.Show("This code is Already exits.");
                    rtxtCode.Focus();
                    return;
                }
                int sid = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);
                int codeExist = new Supplier().CheckForCodeExist(rtxtCode.Text, bool.Parse(lblisNewEntry.Text), sid);
                if (codeExist > 0)
                {
                    Alert.Show("This Supplier code is Already Exist.");
                    return;
                }
                #endregion

                Supplier objSupplier = new Supplier();
                objSupplier.Name = rtxtName.Text;
                objSupplier.Code = rtxtCode.Text.ToUpper();
                objSupplier.CompanyAddress = rtxtComAddress.Text;
                objSupplier.FactoryAddress = rtxtFactory.Text == string.Empty ? "" : rtxtFactory.Text;
                objSupplier.Phone = rtxtPhone.Text == string.Empty ? "0" : rtxtPhone.Text;
                objSupplier.Mobile = rtxtMobile.Text == string.Empty ? "0" : rtxtMobile.Text;
                objSupplier.Email = rtxtEmail.Text == string.Empty ? "" : rtxtEmail.Text;
                objSupplier.ContactPerson = rtxtContractPerson.Text == string.Empty ? "" : rtxtContractPerson.Text;
                objSupplier.Designation = rtxtDesignation.Text == string.Empty ? "" : rtxtDesignation.Text;

                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objSupplier.InsertSupplier();

                }
                else
                {
                    objSupplier.Id = int.Parse(lblId.Text);
                    success = objSupplier.UpdateSupplier();
                }
                if (success == 0)
                {
                    Alert.Show("Supplier data was not save succesfully");
                }
                else
                {
                    if (lblId.Text == string.Empty)
                    {
                        int lastId = new Supplier().GetlastSupplier();
                        lblId.Text = lastId.ToString();
                    }
                    this.LoadProductCategory();

                    this.LoadProduct(int.Parse(lblId.Text));
                    this.LoadProductMapping(int.Parse(lblId.Text));

                    RadTabStrip1.SelectedIndex = 1;
                    rmpProductTagging.Selected = true;
                    Alert.Show("Supplier Infornation save succesfully");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save supplier info.");
            }
        }
        //------------------Clear Supplier-----------------\\
        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearSupplieInfo(false);
        }


        //------------------Save Product tagging-----------------\\
        protected void btnsaveProductTag_OnClick(object sender, EventArgs e)
        {
            int supplierId = int.Parse(lblId.Text);
            int success = 0;
            if (bool.Parse(lblisNewEntryForProductTagging.Text) == false)
            {
                if (lbldeletedItemCount.Text!=string.Empty && int.Parse(lbldeletedItemCount.Text) > 0)
                {
                    string deleteIds = string.Empty;
                    foreach (DictionaryEntry dict in deletedItems)
                    {
                        if (deleteIds == string.Empty)
                            deleteIds = " ProductId = " + dict.Value.ToString();
                        else
                            deleteIds = deleteIds + " Or ProductId =" + dict.Value.ToString() + "";
                    }
                    Session["deletedItems"] = null;
                    deletedItems=new Hashtable();

                    int delete = new ProductMapping().DeleteProductMappingBySupplierIdAndProductId(int.Parse(lblId.Text), deleteIds);

                    if (delete == 0)
                        Alert.Show("somethings is going wrong to delete product mapping");
                    else
                    {
                        this.LoadProduct(int.Parse(lblId.Text));
                        this.LoadProductMapping(int.Parse(lblId.Text));
                        success = 1;
                    }
                }
            }

            foreach (RadListBoxItem item in RadListBoxDestination.Items)
            {
                int productId = int.Parse(item.Value);

                int productIdExist = new ProductMapping().CheckProductIdExistance(productId, int.Parse(lblId.Text));
                if (productIdExist == 0)
                {
                    ProductMapping objProductMapping = new ProductMapping();

                    objProductMapping.ProductId = productId;
                    objProductMapping.SupplierId = supplierId;



                    success = objProductMapping.InsertProductMapping();


                    if (success == 0)
                    {
                        Alert.Show("Product mapping data is not save succesfully");
                        return;
                    }
                    else
                    {
                        Alert.Show("Product mapping data save succesfully");
                    }

                }
                else
                {

                }
            }
            if (success == 0)
            {
                Alert.Show("Product mapping data is not save succesfully");
            }
            else
            {
                RadTabStrip1.SelectedIndex = 2;
                rmpSupplierBankInfo.Selected = true;
            }


        }
        //------------------Clear Product tagging-----------------\\
        protected void btnClearProductTag_OnClick(object sender, EventArgs e)
        {
            this.clearProductTagging();
        }
        //------------------Save Supplier Bank-----------------\\
        protected void btnSaveSuppBank_OnClick(object sender, EventArgs e)
        {
            #region validation

            if (rtxtBankName.Text == string.Empty)
            {
                Alert.Show("Please enter bank name");
                rtxtBankName.Focus();
                return;
            }
            if (rtxtAccOn.Text == string.Empty)
            {
                Alert.Show("Please enter account number.");
                rtxtAccOn.Focus();
                return;
            }
            if (rtxtAccTitle.Text == string.Empty)
            {
                Alert.Show("Please enter account title.");
                rtxtAccTitle.Focus();
                return;
            }
            if (rtxtSWIFTCode.Text == string.Empty)
            {
                Alert.Show("Please enter SWIFT code.");
                rtxtSWIFTCode.Focus();
                return;
            }

            #endregion

            try
            {
                BankInformation objBankInfo = new BankInformation();
                objBankInfo.BankName = rtxtBankName.Text;
                objBankInfo.AccountNo = rtxtAccOn.Text;
                objBankInfo.AccountTitle = rtxtAccTitle.Text;
                objBankInfo.BranchName = "";
                objBankInfo.SWIFTCode = rtxtSWIFTCode.Text;
                objBankInfo.Country = 1;
                objBankInfo.Type = "Supplier";
                objBankInfo.TypeId = int.Parse(lblId.Text);
                objBankInfo.IsDefault = chkIsDefault.Checked;
                objBankInfo.ShortName = "";
                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objBankInfo.InsertBankInformation();
                }
                else
                {
                    if (lblbankId.Text == string.Empty || lblbankId.Text == "0")
                    {
                        success = objBankInfo.InsertBankInformation();
                    }
                    else
                    {
                        objBankInfo.Id = int.Parse(lblbankId.Text);
                        success = objBankInfo.UpdateBankInformation();
                    }

                }
                if (success == 0)
                {
                    Alert.Show("Bank data was not save successfully ");
                    return;
                }
                else
                {

                    this.cleanSupplierbankBank();
                    lblId.Text = "";
                    Alert.Show("Supplier All Information save successfully ");
                }

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save bank information.");
            }
        }
        //------------------Clear Supplier Bank-----------------\\
        protected void btnClearSuppBank_OnClick(object sender, EventArgs e)
        {
            this.cleanSupplierbankBank();
        }

        protected void RadListBoxDestination_OnDeleted(object sender, RadListBoxEventArgs e)
        {
            var items = e.Items;
            int slno = (lbldeletedItemCount.Text == string.Empty) ? 1 : int.Parse(lbldeletedItemCount.Text) + 1;

            lbldeletedItemCount.Text = slno.ToString();
            deletedItems.Add(slno, items[0].Value);

            Session["deletedItems"] = deletedItems;
        }

        protected void rdropProductCat_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}