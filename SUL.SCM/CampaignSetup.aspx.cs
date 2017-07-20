using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Crypto.Tls;
using SUL.Bll;
using SUL.Bll.Base;
using Telerik.Charting;
using Telerik.Web.UI;
using Product = SUL.Bll.Product;

namespace SUL.SCM
{
    public partial class CampaignSetup : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        private Company _company;
        //private bool isNewEntry;
        private List<TempProducts> tempProduct;
        private List<TempOfferProducts> tempOfferProductList;
        private int companyId;

        private List<CampaignDetails> _campaignDetailsList;
        private AppPermission PermissionUser;

        public class TempProducts
        {
            public int Id { get; set; }
            public decimal StartQuantity { get; set; }
            public decimal EndQuantity { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public decimal Amount { get; set; }

        }

        public class TempOfferProducts
        {
            public int Id { get; set; }
            public string ProductType { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public Int32 Quantity { get; set; }
        }

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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Requisition Add");
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

        private void LoadRegion()
        {
            try
            {
                DataTable lstRegions = new Region().GetAllRegionFromView();



                rdropRegion.DataTextField = "RECode";
                rdropRegion.DataValueField = "Id";
                rdropRegion.DataSource = lstRegions;
                rdropRegion.DataBind();
                if (lstRegions.Rows.Count == 2)
                {
                    rdropRegion.SelectedIndex = 1;
                }
                else
                    rdropRegion.SelectedIndex = 0;
            }

            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load region." + ex);
            }
        }
        private void LoadProducts()
        {
            try
            {
                DataTable dtProductInfo = new Product().GetProductFromViewList();
                Session["dtProductInfo"] = dtProductInfo;

                //load products dropdown
                rdropProduct.DataValueField = "Id";
                rdropProduct.DataTextField = "proInfo";
                rdropProduct.DataSource = dtProductInfo;
                rdropProduct.DataBind();

                rdropProduct.SelectedIndex = 0;

                //load free products dropdown
                rdropFreeProduct.DataValueField = "Id";
                rdropFreeProduct.DataTextField = "proInfo";
                rdropFreeProduct.DataSource = dtProductInfo;
                rdropFreeProduct.DataBind();

                rdropFreeProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going on to load Product Info" + ex);
            }
        }
        private void LoadGiftProductInfo()
        {
            try
            {
                DataTable dtProductInfo = new Product().GetProductsByCategory("Gift Product");
                rdropGiftProduct.DataValueField = "Id";
                rdropGiftProduct.DataTextField = "proInfo";
                rdropGiftProduct.DataSource = dtProductInfo;
                rdropGiftProduct.DataBind();

                rdropGiftProduct.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going on to load Product Info" + ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadOfferProduct()
        {
            try
            {
                if (tempOfferProductList == null || tempOfferProductList.Count == 0)
                {
                    radgridCampnigeDetails.Visible = false;
                    return;
                }
                radgridCampnigeDetails.DataSource = tempOfferProductList;
                radgridCampnigeDetails.DataBind();
                radgridCampnigeDetails.Visible = true;
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
        private void LoadCampaignProducts()
        {
            try
            {
                if (tempProduct == null || tempProduct.Count == 0)
                {
                    RadGridAddProductCampaign.Visible = false;
                    return;
                }
                RadGridAddProductCampaign.DataSource = tempProduct;
                RadGridAddProductCampaign.DataBind();
                RadGridAddProductCampaign.Visible = true;
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
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

            if (IsValidPageForUser())
            {
                PermissionUser = (AppPermission)Session["PermissionUser"];
                if (!PermissionUser.IsView)
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }

                if (PermissionUser.IsView && !PermissionUser.IsInsert && !PermissionUser.IsUpdate)
                {
                    btnSave.Visible = false;
                    //btnPrint.Visible = true;
                    btnClear.Visible = false;
                    //btnAddRequisitionDetails.Visible = false;
                }
            }

            if (Session["tempProduct"] != null)
                tempProduct = (List<TempProducts>)Session["tempProduct"];
            else
                tempProduct = new List<TempProducts>();
            if (Session["tempOfferProductList"] != null)
                tempOfferProductList = (List<TempOfferProducts>)Session["tempOfferProductList"];
            else
                tempOfferProductList = new List<TempOfferProducts>();

            if (!IsPostBack)
            {
                this.LoadProducts();
                this.LoadGiftProductInfo();
                this.LoadRegion();

                lblisNewEntry.Text = "true";
                rpvDetails.Enabled = false;

                rtxtCampaignName.Visible = true;
                //cmbCampaignOption.Visible = false;
                tempProduct = new List<TempProducts>();
                tempOfferProductList = new List<TempOfferProducts>();
                
                Session["tempOfferProductList"] = null;
                Session["tempProduct"] = null;

                rdropCampaignType.Enabled = true;
                if (Request.QueryString["Id"] != null)
                {
                    string campaignId = Request.QueryString["Id"].ToString();
                    LoadData(campaignId);
                }
            }
        }

        private void LoadCampaignDetails(int id)
        {
            List<CampaignDetails> objDetailList = new CampaignDetails().GetAllProductDetaislbyCampaignId(id);

            rgdCampaignDetails.DataSource = objDetailList;
            rgdCampaignDetails.DataBind();

            rdropCampaignType.Enabled = objDetailList.Count <= 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadData(string id)
        {
            try
            {
                lblId.Text = id;

                //get campaign master information
                CampaignMaster objCampaignMaster = new CampaignMaster().GetCampaignMasterById(int.Parse(id));
                rtxtCampaignCode.Text = objCampaignMaster.CampaignCode;
                rdtStartDate.SelectedDate = DateTime.Parse(objCampaignMaster.StartDate.ToString());
                rdtEndDate.SelectedDate = DateTime.Parse(objCampaignMaster.EndDate.ToString());
                rdropRegion.SelectedValue = objCampaignMaster.RegionId.ToString();
                rtxtDescription.Value = objCampaignMaster.Description;
                chkIsActive.Checked = objCampaignMaster.IsActive;
                chkIsAdjusted.Checked = objCampaignMaster.IsAdjustedAfterEnd;
                chkIsExclude.Checked = objCampaignMaster.IsExcludedfromIncentive;
                rdropCampaignType.SelectedValue = objCampaignMaster.CampaignOn;

                rdropCampaignType.Enabled = false;

                lblisNewEntry.Text = "false";

                divDetailsGrid.Visible = true;
                btnNext.Visible = true;
                rpvDetails.Enabled = true;
                this.SelectCompaignType();

                this.LoadCampaignDetails(int.Parse(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnAddMultiProduct_OnClick(object sender, EventArgs e)
        {
            #region validation

            if (rdropProduct.SelectedIndex == 0)
            {
                Alert.Show("Please select a Product");
                rdropProduct.Focus();
                return;
            }
            if (rtxtStartQuantity.Text == string.Empty)
            {
                Alert.Show("Please Enter a Start quantity");
                rtxtStartQuantity.Focus();
                return;
            }
           
            if (rtxtPrice.Text == string.Empty)
            {
                Alert.Show("Please Enter the price of the product");
                rtxtPrice.Focus();
                return;
            }
            Regex regexForQuentity = new Regex("^[0-9]*$");
            if (!regexForQuentity.IsMatch(rtxtStartQuantity.Text))
            {
                Alert.Show("Please enter a valid number");
                rtxtStartQuantity.Focus();
                return;
            }
            

            Regex regexForPriceUnite = new Regex(@"^[0-9]\d*(\.\d+)?$");

            if (!regexForPriceUnite.IsMatch(rtxtPrice.Text))
            {
                Alert.Show("Please enter a valid price for the product.");
                rtxtPrice.Focus();
                return;
            }
            #endregion


            try
            {
                TempProducts objTempProducts =
                    tempProduct.Find(x => x.ProductId == int.Parse(rdropProduct.SelectedValue));
                if (objTempProducts == null)
                    objTempProducts = new TempProducts();
                else
                {
                    tempProduct.Remove(objTempProducts);
                }
                objTempProducts.ProductId = int.Parse(rdropProduct.SelectedValue);
                objTempProducts.StartQuantity = decimal.Parse(rtxtStartQuantity.Text);
                objTempProducts.EndQuantity = objTempProducts.StartQuantity;
                objTempProducts.ProductName = rdropProduct.SelectedItem.Text;
                objTempProducts.Price = decimal.Parse(rtxtPrice.Text);
                objTempProducts.Amount = (objTempProducts.StartQuantity*objTempProducts.Price);

                if (tempProduct.Count == 0)
                    tempProduct = new List<TempProducts>();
                tempProduct.Add(objTempProducts);

                Session["tempProduct"] = tempProduct;
                this.LoadCampaignProducts();

                rdropProduct.SelectedIndex = 0;
                rtxtStartQuantity.Text = "";
                rtxtPrice.Text = "";
                rdropProduct.Focus();
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnAddFreeProduct_OnClick(object sender, EventArgs e)
        {

            #region validation
            Regex regexForQuantity = new Regex(@"[\d]([.][\d])?");
            if (rdropFreeProduct.SelectedIndex == 0)
            {
                Alert.Show("Please select a product first.");
                rdropFreeProduct.Focus();
                return;
            }
            if (rtxtFreeQuantity.Text != string.Empty)
            {
                if (!regexForQuantity.IsMatch(rtxtFreeQuantity.Text))
                {
                    Alert.Show("Please enter correct value in Free Quantity");
                    rtxtFreeQuantity.Focus();
                    return;
                }
            }
            else
            {
                Alert.Show("Please enter free product quantity");
                rtxtFreeQuantity.Focus();
                return;
            }
            #endregion

            try
            {
                AddOfferProduct("Free Product", int.Parse(rdropFreeProduct.SelectedValue),
                    rdropFreeProduct.SelectedItem.Text, int.Parse(rtxtFreeQuantity.Text));

                rdropFreeProduct.SelectedIndex = 0;
                rtxtFreeQuantity.Text = "";
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        private void AddOfferProduct(string productType, int productId, string productName, int quantity)
        {
            TempOfferProducts objTempOfferProducts =
                tempOfferProductList.Find(x => x.ProductId == productId);
            if (objTempOfferProducts == null)
                objTempOfferProducts = new TempOfferProducts();
            else
            {
                tempOfferProductList.Remove(objTempOfferProducts);
            }

            objTempOfferProducts.ProductType = productType;
            objTempOfferProducts.ProductId = productId;
            objTempOfferProducts.ProductName = productName;
            objTempOfferProducts.Quantity = quantity;

            if (tempOfferProductList.Count == 0)
                tempOfferProductList = new List<TempOfferProducts>();
            tempOfferProductList.Add(objTempOfferProducts);

            Session["tempOfferProductList"] = tempOfferProductList;
            this.LoadOfferProduct();

            if (Request.QueryString["Id"] != null)
            {
                string id = Request.QueryString["Id"];

                Response.Write("<script type='text/javascript'> location.replace('CampaignSetup.aspx?id=" + id +
                               "#campOffers')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('CampaignSetup.aspx#campOffers')</script>");
        }

        protected void rdropCampaignType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //throw new NotImplementedException();
            this.SelectCompaignType();
        }

        private void SelectCompaignType()
        {
            if (rdropCampaignType.SelectedValue.ToLower() == "product")
            {
                ShowProductPanel.Visible = true;
                ShowValuePanel.Visible = false;

                rtxtStartValue.Text = "";
                rtxtEndvalue.Text = "";
                rtxtPercentage.Text = "";
                rtxtDiscountValue.Text = "";
            }
            else if (rdropCampaignType.SelectedValue.ToLower() == "value")
            {
                if (tempProduct.Count > 0)
                {
                    Alert.Show("Please delete all the products before you change  campaign type.");
                    rdropCampaignType.SelectedValue = "Product";
                    return;
                }

                tempProduct = new List<TempProducts>();
                Session["tempProduct"] = null;

                ShowValuePanel.Visible = true;
                ShowProductPanel.Visible = false;
            }
        }

        protected void RadGridAddProductCampaign_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnSelect")
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    rdropProduct.SelectedValue = item["colProductId"].Text;
                    rtxtStartQuantity.Text = item["colStartQuantity"].Text;
                }
                else if (e.CommandName == "btnDelete")
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    string id = item["colId"].Text;
                    string productId = item["colProductId"].Text;

                    if (tempProduct.Exists(x => x.ProductId == int.Parse(productId)))
                    {
                        TempProducts product = tempProduct.Find(x => x.ProductId == int.Parse(productId));
                        tempProduct.Remove(product);

                        Session["tempProduct"] = tempProduct;
                        this.LoadCampaignProducts();
                    }

                    if (id != string.Empty)
                    {
                        int delete = new CampaignProducts().DeleteCampaignProductsById(int.Parse(id));
                    }

                    Alert.Show("Seleted product deleted from campaign");
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void radgridCampnigeDetails_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnSelect")
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    string type = item["colProdType"].Text;
                    if (type.ToLower() == "free product")
                    {
                        chkFreeProduct.Checked = true;
                        rdropFreeProduct.SelectedValue = item["colProductId"].Text;
                        rtxtFreeQuantity.Text = item["colQuantity"].Text;
                    }
                    else
                    {
                        chkGift.Checked = true;
                        rdropGiftProduct.SelectedValue = item["colProductId"].Text;
                        rtxtGiftQuantity.Text = item["colQuantity"].Text;
                    }
                }
                else if (e.CommandName == "btnDelete")
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    string id = item["colId"].Text;
                    string productId = item["colProductId"].Text;
                    string productType = item["colProdType"].Text;


                    if (tempOfferProductList.Exists(x => x.ProductId == int.Parse(productId) && x.ProductType == productType))
                    {
                        TempOfferProducts product = tempOfferProductList.Find(x => x.ProductId == int.Parse(productId) && x.ProductType == productType);
                        tempOfferProductList.Remove(product);

                        Session["tempOfferProductList"] = tempOfferProductList;
                        this.LoadOfferProduct();
                    }

                    if (id != string.Empty)
                    {
                        int delete = new CampaignOfferProducts().DeleteCampaignOfferProductsById(int.Parse(id));
                    }

                    Alert.Show("Seleted product deleted from campaign");
                }

            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation
            if (rtxtCampaignCode.Text == string.Empty)
            {
                Alert.Show("Please enter a campaign code");
                rtxtCampaignCode.Focus();
                return;
            }
            if (rdtStartDate.SelectedDate == null)
            {
                Alert.Show("Please Select a start Date");
                rdtStartDate.Focus();
                return;
            }
            if (rdtEndDate.SelectedDate == null)
            {
                Alert.Show("Please Select a End Date");
                rdtEndDate.Focus();
                return;
            }
            if (rdropCampaignType.SelectedIndex <= -1)
            {
                Alert.Show("Please select a campaign on option.");
                rdropCampaignType.Focus();
                return;
            }

            //check for duplicate campaign name
            bool isNewEntry = bool.Parse(lblisNewEntry.Text);

            int isExist = (isNewEntry)
                ? new CampaignMaster().CheckCodeExistance(rtxtCampaignCode.Text, 0, true)
                : new CampaignMaster().CheckCodeExistance(rtxtCampaignCode.Text, int.Parse(lblId.Text), false);

            if (isExist > 0)
            {
                Alert.Show("The code '" + rtxtCampaignCode.Text + "' is already exist.");
                rtxtCampaignCode.Focus();
                return;
            }

            #endregion

            try
            {
                CampaignMaster objCampaignMaster = new CampaignMaster();
                objCampaignMaster.StartDate = DateTime.Parse(rdtStartDate.SelectedDate.ToString());
                objCampaignMaster.EndDate = DateTime.Parse(rdtEndDate.SelectedDate.ToString());
                objCampaignMaster.RegionId = (rdropRegion.SelectedIndex == 0) ? 0 : int.Parse(rdropRegion.SelectedValue);
                objCampaignMaster.CampaignCode = rtxtCampaignCode.Text;
                objCampaignMaster.Description = rtxtDescription.Value;
                objCampaignMaster.IsAdjustedAfterEnd = chkIsAdjusted.Checked;
                objCampaignMaster.IsActive = chkIsActive.Checked;
                objCampaignMaster.IsExcludedfromIncentive = chkIsExclude.Checked;
                objCampaignMaster.CreateDate = DateTime.Now;
                objCampaignMaster.CreatedBy = _user.Id;
                objCampaignMaster.CampaignOn = rdropCampaignType.SelectedItem.Text;

                int success = 0;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objCampaignMaster.InsertCampaignMaster();
                    lblId.Text = new CampaignMaster().GetMaxCampaignMasterId().ToString();
                }
                else
                {
                    objCampaignMaster.Id = int.Parse(lblId.Text);
                    success = objCampaignMaster.UpdateCampaignMaster();
                }
                if (success == 0)
                {
                    Alert.Show("Campaign information is not save succesfully");
                    return;
                }

                rpvDetails.Enabled = true;
                btnNext.Visible = true;
                Alert.Show("Campaign information save succesfully");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void rdropProduct_OnDataBound(object sender, EventArgs e)
        {
            rdropProduct.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropGiftProduct_OnDataBound(object sender, EventArgs e)
        {
            rdropGiftProduct.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropFreeProduct_OnDataBound(object sender, EventArgs e)
        {
            rdropFreeProduct.Items.Insert(0, new RadComboBoxItem());
        }


        protected void rdropRegion_OnDataBound(object sender, EventArgs e)
        {
            rdropRegion.Items.Insert(0, new RadComboBoxItem());
        }

        protected void chkDiscount_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkDiscount.Checked)
            {
                rtxtPercentage.Visible = true;
            }
            else
            {
                rtxtPercentage.Visible = false;
            }
            if (Request.QueryString["Id"] != null)
            {
                string id = Request.QueryString["Id"];

                Response.Write("<script type='text/javascript'> location.replace('CampaignSetup.aspx?id=" + id +
                               "#campOffers')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('CampaignSetup.aspx#campOffers')</script>");
        }

        protected void chkAmount_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkAmount.Checked)
            {
                rtxtDiscountValue.Visible = true;
            }
            else
            {
                rtxtDiscountValue.Visible = false;
            }
            if (Request.QueryString["Id"] != null)
            {
                string id = Request.QueryString["Id"];

                Response.Write("<script type='text/javascript'> location.replace('CampaignSetup.aspx?id=" + id +
                               "#campOffers')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('CampaignSetup.aspx#campOffers')</script>");
        }

        protected void chkGift_OnCheckedChanged(object sender, EventArgs e)
        {
            //if (chkGift.Checked)
            //{
            //    ShoWGiftPanel.Visible = true;
            //}
            //else
            //{
            //    ShoWGiftPanel.Visible = false;
            //}
            if (Request.QueryString["Id"] != null)
            {
                string id = Request.QueryString["Id"];

                Response.Write("<script type='text/javascript'> location.replace('CampaignSetup.aspx?id=" + id +
                               "#campOffers')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('CampaignSetup.aspx#campOffers')</script>");
        }

        protected void chkFreeProduct_OnCheckedChanged(object sender, EventArgs e)
        {
            //if (chkFreeProduct.Checked)
            //{
            //    ShowFreeProduct.Visible = true;
            //}
            //else
            //{
            //    ShowFreeProduct.Visible = false;
            //}
            if (Request.QueryString["Id"] != null)
            {
                string id = Request.QueryString["Id"];

                Response.Write("<script type='text/javascript'> location.replace('CampaignSetup.aspx?id=" + id +
                               "#campOffers')</script>");
            }
            else
                Response.Write("<script type='text/javascript'> location.replace('CampaignSetup.aspx#campOffers')</script>");
        }

       

        protected void btnAddGiftProduct_OnClick(object sender, EventArgs e)
        {
            #region validation
            Regex regexForQuantity = new Regex(@"[\d]([.][\d])?");
            if (rdropGiftProduct.SelectedIndex == 0)
            {
                Alert.Show("Please select a product first.");
                rdropGiftProduct.Focus();
                return;
            }
            if (rtxtGiftQuantity.Text != string.Empty)
            {
                if (!regexForQuantity.IsMatch(rtxtGiftQuantity.Text))
                {
                    Alert.Show("Please enter correct value in Free Quantity");
                    rtxtGiftQuantity.Focus();
                    return;
                }
            }
            else
            {
                Alert.Show("Please enter free product quantity");
                rtxtGiftQuantity.Focus();
                return;
            }
            #endregion

            try
            {
                AddOfferProduct("Gift Product", int.Parse(rdropGiftProduct.SelectedValue),
                    rdropGiftProduct.SelectedItem.Text, int.Parse(rtxtGiftQuantity.Text));

                rdropGiftProduct.SelectedIndex = -1;
                rtxtGiftQuantity.Text = "";
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        private void ClearDetails()
        {
            rtxtStartValue.Text = "";
            rtxtEndvalue.Text = "";
            rtxtPercentage.Text = "";
            rtxtDiscountValue.Text = "";
            lblCampaignDetails.Text = "";
            rtxtCampaignName.Text = "";
            rtxtGiftQuantity.Text = "";
            rtxtFreeQuantity.Text = "";

            rtxtPercentage.Visible = false;
            rtxtDiscountValue.Visible = false;

            tempOfferProductList = null;
            tempProduct = null;
            Session["tempOfferProductList"] = null;
            Session["tempProduct"] = null;

            chkAmount.Checked = false;
            chkDiscount.Checked = false;
            chkFreeProduct.Checked = false;
            chkGift.Checked = false;

            this.LoadCampaignProducts();
            this.LoadOfferProduct();
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearDetails();
        }

        protected void btnSaveDetails_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (lblId.Text == string.Empty)
                {
                    Alert.Show("Please save campaign information before you save details.");
                    return;
                }
                if (rdropCampaignType.SelectedValue.ToLower() == "product" && tempProduct.Count == 0)
                {
                    Alert.Show("Please enter the campaign type first.");
                    return;
                }
                if (chkDiscount.Checked && rtxtPercentage.Text==string.Empty) 
                {
                    Alert.Show("Please enter the discount percentage.");
                    rtxtPercentage.Focus();
                    return;
                }

                if (chkAmount.Checked && rtxtDiscountValue.Text == string.Empty)
                {
                    Alert.Show("Please enter the discount value.");
                    rtxtDiscountValue.Focus();
                    return;
                }

                int campId = int.Parse(lblId.Text);
                //check for duplicate campaign name
                bool isNewEntry = (lblCampaignDetails.Text == string.Empty);

                int isExist = (isNewEntry)
                    ? new CampaignDetails().CheckNameExistance(rtxtCampaignName.Text, 0, true, campId)
                    : new CampaignDetails().CheckNameExistance(rtxtCampaignName.Text, int.Parse(lblCampaignDetails.Text), false, campId);

                if (isExist > 0)
                {
                    Alert.Show("Campaign name '" + rtxtCampaignName.Text + "' is already exist.");
                    rtxtCampaignName.Focus();
                    return;
                }
                int success = 0;

                CampaignDetails objCampaignDetails = new CampaignDetails
                {
                    CampaignId = campId,
                    StartValue = (rtxtStartValue.Text == string.Empty) ? 0 : decimal.Parse(rtxtStartValue.Text),
                    EndValue = (rtxtEndvalue.Text == string.Empty) ? 0 : decimal.Parse(rtxtEndvalue.Text),
                    DiscountPcnt = (rtxtPercentage.Text == string.Empty) ? 0 : decimal.Parse(rtxtPercentage.Text),
                    OfferAmount = (rtxtDiscountValue.Text == string.Empty) ? 0 : decimal.Parse(rtxtDiscountValue.Text),
                    CampaignName = rtxtCampaignName.Text,
                };

                if (isNewEntry)
                {
                    success = objCampaignDetails.InsertCampaignDetails();
                    if (success > 0)
                        lblCampaignDetails.Text = new CampaignDetails().GetLastId().ToString();
                    else
                    {
                        Alert.Show("Error occurd during save campaign details.");
                        return;
                    }
                }
                else
                {
                    objCampaignDetails.Id = int.Parse(lblCampaignDetails.Text);
                    success = objCampaignDetails.UpdateCampaignDetails();

                    if (success <= 0)
                    {
                        Alert.Show("Error occurd during update campaign details.");
                        return;
                    }
                }

                int detailsId = int.Parse(lblCampaignDetails.Text);
                if (tempProduct.Count != 0)
                {
                    List<CampaignProducts> listCampaignProducts = (isNewEntry) ? new List<CampaignProducts>() : new CampaignProducts().GetProductsByDetailsId(detailsId);

                    foreach (TempProducts tmpProducts in tempProduct)
                    {
                        CampaignProducts objCampaignProducts = new CampaignProducts()
                        {
                            ProductId = tmpProducts.ProductId,
                            StartQuantity = tmpProducts.StartQuantity,
                            EndQuantity = tmpProducts.EndQuantity,
                            CampaignId = campId,
                            CampaignDetailsId = detailsId,
                            Price = tmpProducts.Price
                        };

                        CampaignProducts obj = listCampaignProducts.Find(x => x.ProductId == tmpProducts.ProductId);
                        if (obj == null)
                            success = objCampaignProducts.InsertCampaignProducts();
                        else
                        {
                            objCampaignProducts.Id = obj.Id;
                            success = objCampaignProducts.UpdateCampaignProducts();
                        }
                    }
                }

                if (tempOfferProductList.Count != 0)
                {
                    List<CampaignOfferProducts> listOfferProducts = (isNewEntry) ? new List<CampaignOfferProducts>() : new CampaignOfferProducts().GetProductsByDetailsId(detailsId);

                    foreach (TempOfferProducts offerProducts in tempOfferProductList)
                    {
                        CampaignOfferProducts offer = new CampaignOfferProducts()
                        {
                            CampaignId = campId,
                            CampaignDetailsId = detailsId,
                            ProductId = offerProducts.ProductId,
                            ProductType = offerProducts.ProductType,
                            Quantity = offerProducts.Quantity
                        };

                        CampaignOfferProducts obj = listOfferProducts.Find(x => x.ProductId == offerProducts.ProductId);
                        if (obj == null)
                            success = offer.InsertCampaignOfferProducts();
                        else
                        {
                            offer.Id = obj.Id;
                            success = offer.UpdateCampaignOfferProducts();
                        }
                    }
                }

                if (success == 0)
                {
                    Alert.Show("Data Is not Save succesfully");
                }
                else
                {
                    Alert.Show(rtxtCampaignName.Text+" Details Data Save succesfully");
                    this.ClearDetails();

                    this.LoadCampaignDetails(campId);
                }
            }
            catch (Exception ex)
            {

                Alert.Show(ex.Message);
            }
        }

        protected void btnNext_OnClick(object sender, EventArgs e)
        {
            rpvDetails.Selected = true;
            RadTabStrip1.Tabs[1].Selected = true;
        }

        protected void rgdCampaignDetails_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton linkedit = (LinkButton)sender;
                string[] Id = linkedit.CommandArgument.ToString().Split(';');
                lblCampaignDetails.Text = Id[0];

                int id = int.Parse(lblCampaignDetails.Text);
                this.LoadDetails(id);

                int requisitionExist = new RequisitionDetails().CheckExistanceByCampaignDetailsId(id);
                rpvDetails.Enabled = requisitionExist <= 0;
                rpvDetails.Selected = true;
                RadTabStrip1.Tabs[1].Selected = true;
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        private void LoadDetails(int id)
        {
            try
            {
                CampaignDetails objDetails = new CampaignDetails().GetCampaignDetailsById(id);

                rtxtCampaignName.Text = objDetails.CampaignName;
                if (objDetails.DiscountPcnt > 0)
                {
                    chkDiscount.Checked = true;
                  
                    rtxtPercentage.Visible = true;
                    //rtxtPercentage.EmptyMessage = "";
                    rtxtPercentage.Text = objDetails.DiscountPcnt.ToString();
                }
                if (objDetails.OfferAmount > 0)
                {
                    chkAmount.Checked = true;
                    rtxtDiscountValue.Text = objDetails.OfferAmount.ToString();
                    rtxtDiscountValue.Visible = true;
                }

                List<Product> productList = new Product().GetAllProduct();

                if (rdropCampaignType.SelectedItem.Text.ToLower() == "product")
                {
                    tempProduct = new List<TempProducts>();

                    List<CampaignProducts> listCampaignProducts = new CampaignProducts().GetProductsByDetailsId(id);
                    if (listCampaignProducts.Count > 0)
                    {
                        foreach (CampaignProducts obj in listCampaignProducts)
                        {
                            TempProducts prod = new TempProducts()
                            {
                                Id = obj.Id,
                                ProductId = obj.ProductId,
                                ProductName = productList.Find(x => x.Id == obj.ProductId).ProductName,
                                StartQuantity = obj.StartQuantity,
                                EndQuantity = obj.EndQuantity,
                                Price = obj.Price
                            };
                            tempProduct.Add(prod);
                        }

                        
                    }

                    Session["tempProduct"] = tempProduct;
                    this.LoadCampaignProducts();
                }
                else
                {
                    rtxtStartValue.Text = objDetails.StartValue.ToString();
                    rtxtEndvalue.Text = objDetails.EndValue.ToString();
                    rtxtPercentage.Text = objDetails.DiscountPcnt.ToString();
                    rtxtDiscountValue.Text = objDetails.OfferAmount.ToString();
                }


                //load offer products
                tempOfferProductList = new List<TempOfferProducts>();

                List<CampaignOfferProducts> listOfferProducts = new CampaignOfferProducts().GetProductsByDetailsId(id);
                if (listOfferProducts.Count > 0)
                {
                    foreach (CampaignOfferProducts obj in listOfferProducts)
                    {
                        TempOfferProducts prod = new TempOfferProducts()
                        {
                            Id = obj.Id,
                            ProductId = obj.ProductId,
                            ProductName = productList.Find(x => x.Id == obj.ProductId).ProductName,
                            ProductType = obj.ProductType,
                            Quantity = obj.Quantity
                        };
                        tempOfferProductList.Add(prod);

                        if (obj.ProductType.Contains("Gift"))
                            chkGift.Checked = true;
                        else
                            chkFreeProduct.Checked = true;
                    }
                }

                Session["tempOfferProductList"] = tempOfferProductList;

                this.LoadOfferProduct();
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void rdropProduct_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (rdropProduct.SelectedIndex > 0)
                {
                    int id = int.Parse(rdropProduct.SelectedValue);

                    DataTable dtProduct = new DataTable();
                    if (Session["dtProductInfo"] == null)
                        dtProduct = new Product().GetProductFromViewList();
                    else
                        dtProduct = (DataTable)Session["dtProductInfo"];


                    DataRow row = dtProduct.AsEnumerable().SingleOrDefault(x => x.Field<int>("Id") == id);

                    decimal price = decimal.Parse(row["DP"].ToString());
                    rtxtPrice.Text = price.ToString();
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
    }
}
