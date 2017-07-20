using SUL.Bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hrms;
using Telerik.Web.UI;
using Telerik.Web.UI.ComboBox;

namespace SUL.SCM
{
    public partial class SpairPartsDelivery : System.Web.UI.Page
    {
        private Users _user;
        private Company _company;
        private List<TempSPDetails> tempSpDetailses;

        private string _department;

        private AppPermission PermissionUser;

        private class TempSPDetails
        {
            public int Id { get; set; }
            public int SpairPartsId { get; set; }
            public string SpairParts { get; set; }
            public decimal Quantity { get; set; }
            public decimal Rate { get; set; }
            public int Unit { get; set; }
            public string UnitName { get; set; }
            public int Color { get; set; }
            public string ColorName { get; set; }

            public decimal TotalRate { get; set; }
        }
        private bool IsValidSession()
        {
            if (Session["user"] == null)
            {
                return false;
            }

            _user = (Users)Session["user"];
            _company = (Company)Session["company"];
            _department = Session["Department"].ToString();

            return _user.Id != 0;

        }

        private bool IsValidPageForUser()
        {

            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Spair Part Delivery") : new AppFunctionality().GetAppFunctionalityId("Monthly Sales Report", int.Parse(lblsource.Text));
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Requisition Add");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsInsert)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsInsert;
                return permission;
            }
            else
                return true;
        }

        private bool IsValidUpdateForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Spair Part Delivery");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsUpdate)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsUpdate;
                return permission;
            }
            else
                return true;
        }
        private bool IsValidViewForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Spair Part Delivery");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsView)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsUpdate;
                return permission;
            }
            else
                return true;
        }

        private bool IsValidDeleteForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Spair Part Delivery");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsDelete)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsDelete;
                return permission;
            }
            else
                return true;
        }

        private void LoadServiceCenter()
        {
            try
            {
                List<ServiceCenter> lstServiceCenters = new ServiceCenter().GetAllServiceCenter();

                rdropServiceCenter.DataValueField = "Id";
                rdropServiceCenter.DataTextField = "SCName";
                rdropServiceCenter.DataSource = lstServiceCenters;
                rdropServiceCenter.DataBind();

                rdropServiceCenter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load service center" + ex);
            }
        }

        private void LoadProduct()
        {
            try
            {
                DataTable dtProductInfo = new Product().GetProductFromViewList();

                rdropProduct.DataValueField = "Id";
                rdropProduct.DataTextField = "proInfo";
                rdropProduct.DataSource = dtProductInfo;
                rdropProduct.DataBind();

                rdropProduct.SelectedIndex = 0;


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
                DataTable dtProductInfo = new Product().GetSpairProductFromViewList();

                rdropSpairParts.DataValueField = "Id";
                rdropSpairParts.DataTextField = "proInfo";
                rdropSpairParts.DataSource = dtProductInfo;
                rdropSpairParts.DataBind();

                rdropProduct.SelectedIndex = 0;


            }
            catch (Exception ex)
            {
                Alert.Show("something is going on to load Product Info" + ex);
            }
        }
        private void LoadColor()
        {
            try
            {
                List<ListTable> lstListTables = new ListTable().GetAllListTableByType("Color");
                lstListTables.Insert(0, new ListTable());

                rdropColor.DataTextField = "ListValue";
                rdropColor.DataValueField = "ListId";
                rdropColor.DataSource = lstListTables;
                rdropColor.DataBind();

                rdropColor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                Alert.Show(ex.Message);
            }
        }
        private void LoadDeliveryMethod()
        {
            try
            {
                //Get object list of courier information
                List<CourierInformation> lstCourierInformation = new CourierInformation().GetAllCourierInformation();
                DataTable dtTranstopt = new CourierInformation().GetTransportViewList();
                //insert bland object in the list

                rdropDeliveryMethod.DataTextField = "Val";
                rdropDeliveryMethod.DataValueField = "Val";
                rdropDeliveryMethod.DataSource = dtTranstopt;
                rdropDeliveryMethod.DataBind();

                if (lstCourierInformation.Count == 2)
                    rdropDeliveryMethod.SelectedIndex = 1;
                else
                    rdropDeliveryMethod.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load courier Info." + ex);
            }
        }
        private void LoadProductUnite()
        {
            try
            {
                List<ProductUnit> lstProductUnits = new ProductUnit().GetAllProductUnit();
                lstProductUnits.Insert(0, new ProductUnit());

                rdropProductUnit.DataTextField = "UnitCode";
                rdropProductUnit.DataValueField = "Id";
                rdropProductUnit.DataSource = lstProductUnits;
                rdropProductUnit.DataBind();
                if (lstProductUnits.Count == 2)
                {
                    rdropProductUnit.SelectedIndex = 1;
                }
                else
                {
                    rdropProductUnit.SelectedIndex = 0;
                }


            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load Product Unit dropdown" + ex);
            }
        }
        private void LoadSPDeliveryDetails()
        {
            try
            {
                if (tempSpDetailses.Count == 0)
                {
                    RadGridAddSPDetails.DataSource = new string[] { };
                    RadGridAddSPDetails.DataBind();
                    return;
                }

                RadGridAddSPDetails.DataSource = tempSpDetailses;
                RadGridAddSPDetails.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load requisition details grid." + ex);
            }
        }

        private void LoadSpairtPartsDataFromDatabase(int id)
        {
            try
            {
                if (bool.Parse(lblnewEntry.Text) == false)
                {
                    List<SPDeliveryDetails> lstSpDeliveryDetailses =
                        new SPDeliveryDetails().GetAllSPDealerDetailsBymasterId(id);
                    if (lstSpDeliveryDetailses.Count > 0)
                    {
                        tempSpDetailses = new List<TempSPDetails>();
                        foreach (SPDeliveryDetails lstSpDetails in lstSpDeliveryDetailses)
                        {
                            TempSPDetails tmpsSpDetails = new TempSPDetails();

                            tmpsSpDetails.Id = int.Parse(lstSpDetails.Id.ToString());
                            lblDetails.Text = lstSpDetails.Id.ToString();

                            Product objProduct = new Product().GetProductById(lstSpDetails.SpairPartsId);
                            tmpsSpDetails.SpairParts = objProduct.ProductCode + ";" + objProduct.ProductName;

                            this.LoadSpairParts();

                            tmpsSpDetails.SpairPartsId = lstSpDetails.SpairPartsId;
                            tmpsSpDetails.Quantity = lstSpDetails.Quantity;
                            tmpsSpDetails.Rate = lstSpDetails.Rate;

                            tmpsSpDetails.Unit = lstSpDetails.Unit;
                            ProductUnit objProductUnit = new ProductUnit().GetProductUnitById(int.Parse(rdropProductUnit.SelectedValue));
                            tmpsSpDetails.UnitName = objProductUnit.UnitCode;
                            tmpsSpDetails.Color = lstSpDetails.Color;
                            tmpsSpDetails.TotalRate = lstSpDetails.Rate * lstSpDetails.Quantity;

                            tempSpDetailses.Add(tmpsSpDetails);
                        }

                        Session["tempSpDetailses"] = tempSpDetailses;

                        LoadSPDeliveryDetails();
                        decimal totalsum;

                        totalsum = tempSpDetailses.Sum(x => x.TotalRate);
                        rtxtItemTotal.Text = totalsum.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Requisition details data." + ex);
            }
        }
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


            else
                _department = "All";

            if (Session["tempSpDetailses"] != null)
                tempSpDetailses = (List<TempSPDetails>)Session["tempSpDetailses"];
            else
                tempSpDetailses = new List<TempSPDetails>();

            if (!IsPostBack)
            {
                this.LoadColor();
                this.LoadDeliveryMethod();
                this.LoadProductUnite();
                this.LoadServiceCenter();
                this.LoadSpairParts();
                Session["tempSpDetailses"] = null;
                lblAdvenceSearch.Text = "2";
                lblnewEntry.Text = "true";
                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];

                    SPDeliveryMaster objSpDeliveryMaster=new SPDeliveryMaster().GetSPDeliveryMasterById(int.Parse(id));


                    lblId.Text = objSpDeliveryMaster.Id.ToString();
                    rdropDeliveryMethod.SelectedValue = objSpDeliveryMaster.DeliveryMethod;
                    rdropServiceCenter.SelectedValue = objSpDeliveryMaster.ServiceCenterId.ToString();
                    rdtCreateDate.SelectedDate = objSpDeliveryMaster.CreateDate;
                    rdtDeliveryDate.SelectedDate = objSpDeliveryMaster.DeliveryDate;
                    lblTransactionCode.Text = objSpDeliveryMaster.TransactionCode;
                    lblnewEntry.Text = "false";
                    this.LoadSpairtPartsDataFromDatabase(int.Parse(id));

                    if (objSpDeliveryMaster.Status.ToLower() != "created" &&
                        objSpDeliveryMaster.Status.ToLower() != "unapproved" &&
                        objSpDeliveryMaster.Status.ToLower() != "approved")
                    {
                        btnSave.Visible = false;
                        btnAddRequisitionDetails.Visible = false;
                    }

                }



            }
            if (IsValidPageForUser())
            {

                PermissionUser = (AppPermission)Session["PermissionUser"];
                if (!PermissionUser.IsView)
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }

            }
        }

        protected void btnShowPanal_OnClick(object sender, EventArgs e)
        {
            if (btnShowPanal.Text == "Advance Search")
            {
                showPanal.Visible = true;
                ShowNormal.Visible = false;
                btnShowPanal.Text = "Back Normal";
                rdropSpairParts.SelectedIndex = 0;
                rdropColor.SelectedIndex = 0;
                rtxtItemTotal.Text = "";
                rtxtRate.Text = "";
                lblAdvenceSearch.Text = "1";
                btnShowPanal.Visible = true;
                this.LoadProduct();
            }

        }

        protected void btnNormal_OnClick(object sender, EventArgs e)
        {
            if (btnShowPanal.Text == "Back Normal")
            {
                rdropProduct.SelectedIndex = 0;
                rtxtQuentity.Text = "";
                lblAdvenceSearch.Text = "2";
                showPanal.Visible = false;
                ShowNormal.Visible = true;
                btnShowPanal.Text = "Advance Search";

            }
        }

        protected void rdropSpairParts_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Product objProduct = new Product().GetProductById(int.Parse(rdropSpairParts.SelectedValue));

            rtxtRate.Text = objProduct.DP.ToString();
            lblDP2.Text = objProduct.DP2.ToString();
            rdropProductUnit.SelectedIndex = objProduct.BaseUnit;
        }

        protected void btnAddRequisitionDetails_OnClick(object sender, EventArgs e)
        {
            try
            {

                Regex regexForQuentity = new Regex("^[0-9]*$");
                Regex regexForPriceUnite = new Regex(@"^[0-9]\d*(\.\d+)?$");
                TempSPDetails objTempSpDetails = new TempSPDetails();
                #region Normal SpAdd
                if (lblAdvenceSearch.Text == "2")
                {
                    if (rdropProduct.SelectedIndex > 0)
                    {
                        List<SpairParts> lstSpairParts =
                            new SpairParts().GetAllSpairPartsByProduct(int.Parse(rdropProduct.SelectedValue));
                        if (lstSpairParts.Count != 0)
                        {

                        }
                    }
                    else
                    {
                        #region validation

                        if (rdropSpairParts.SelectedIndex <= 0)
                        {
                            Alert.Show("Please select a Spair parts.");
                            rdropProduct.Focus();
                            return;
                        }
                        if (rdropProductUnit.SelectedIndex <= 0)
                        {
                            Alert.Show("Please select a Product Unit.");
                            rdropProductUnit.Focus();
                            return;
                        }
                        if (rtxtQuentity.Text == string.Empty || !regexForQuentity.IsMatch(rtxtQuentity.Text))
                        {
                            Alert.Show("Please enter a valid quantity");
                            rtxtQuentity.Focus();
                            return;
                        }

                        #endregion


                        if (rdropColor.SelectedIndex == 0)
                        {
                            objTempSpDetails =
                            tempSpDetailses.Find(
                                x =>
                                    x.SpairPartsId == int.Parse(rdropSpairParts.SelectedValue));
                            if (objTempSpDetails != null)
                            {
                                if (objTempSpDetails.SpairPartsId == 0)
                                    objTempSpDetails = new TempSPDetails();
                                else
                                {
                                    tempSpDetailses.Remove(objTempSpDetails);
                                    objTempSpDetails.SpairPartsId = int.Parse(rdropSpairParts.SelectedValue);
                                    objTempSpDetails.Unit = int.Parse(rdropProductUnit.SelectedValue);
                                    objTempSpDetails.Quantity = int.Parse(rtxtQuentity.Text) + objTempSpDetails.Quantity;
                                    objTempSpDetails.SpairParts = rdropSpairParts.SelectedItem.Text;
                                    objTempSpDetails.UnitName = rdropProductUnit.SelectedItem.Text;
                                    objTempSpDetails.Rate = decimal.Parse(rtxtRate.Text);
                                    objTempSpDetails.Color = int.Parse(rdropColor.SelectedValue);
                                    if (rdropColor.SelectedIndex <= 0)
                                    {
                                        objTempSpDetails.ColorName = "";
                                    }
                                    else
                                    {
                                        objTempSpDetails.ColorName = rdropColor.SelectedItem.Text;

                                    }
                                    objTempSpDetails.TotalRate = decimal.Parse(rtxtTotalPrice.Text) + objTempSpDetails.TotalRate;

                                    if (tempSpDetailses.Count == 0)
                                        tempSpDetailses = new List<TempSPDetails>();
                                    tempSpDetailses.Add(objTempSpDetails);
                                    Session["tempSpDetailses"] = tempSpDetailses;
                                }

                            }
                            else
                            {
                                objTempSpDetails = new TempSPDetails();
                                objTempSpDetails.SpairPartsId = int.Parse(rdropSpairParts.SelectedValue);
                                objTempSpDetails.Unit = int.Parse(rdropProductUnit.SelectedValue);
                                objTempSpDetails.Quantity = int.Parse(rtxtQuentity.Text);
                                objTempSpDetails.SpairParts = rdropSpairParts.SelectedItem.Text;
                                objTempSpDetails.UnitName = rdropProductUnit.SelectedItem.Text;
                                objTempSpDetails.Rate = decimal.Parse(rtxtRate.Text);
                                objTempSpDetails.Color = int.Parse(rdropColor.SelectedValue);
                                if (rdropColor.SelectedIndex <= 0)
                                {
                                    objTempSpDetails.ColorName = "";
                                }
                                else
                                {
                                    objTempSpDetails.ColorName = rdropColor.SelectedItem.Text;

                                }
                                objTempSpDetails.TotalRate = decimal.Parse(rtxtTotalPrice.Text);

                                if (tempSpDetailses.Count == 0)
                                    tempSpDetailses = new List<TempSPDetails>();
                                tempSpDetailses.Add(objTempSpDetails);
                                Session["tempSpDetailses"] = tempSpDetailses;
                            }

                        }
                        else
                        {
                            objTempSpDetails =
                            tempSpDetailses.Find(
                                x =>
                                    x.SpairPartsId == int.Parse(rdropSpairParts.SelectedValue) &&
                                    x.Color == int.Parse(rdropColor.SelectedValue));
                            if (objTempSpDetails != null)
                            {
                                if (objTempSpDetails.SpairPartsId == 0)
                                    objTempSpDetails = new TempSPDetails();
                                else
                                {
                                    tempSpDetailses.Remove(objTempSpDetails);
                                    objTempSpDetails.SpairPartsId = int.Parse(rdropSpairParts.SelectedValue);
                                    objTempSpDetails.Unit = int.Parse(rdropProductUnit.SelectedValue);
                                    objTempSpDetails.Quantity = int.Parse(rtxtQuentity.Text) + objTempSpDetails.Quantity;
                                    objTempSpDetails.SpairParts = rdropSpairParts.SelectedItem.Text;
                                    objTempSpDetails.UnitName = rdropProductUnit.SelectedItem.Text;
                                    objTempSpDetails.Rate = decimal.Parse(rtxtRate.Text);
                                    objTempSpDetails.Color = int.Parse(rdropColor.SelectedValue);
                                    if (rdropColor.SelectedIndex <= 0)
                                    {
                                        objTempSpDetails.ColorName = "";
                                    }
                                    else
                                    {
                                        objTempSpDetails.ColorName = rdropColor.SelectedItem.Text;

                                    }
                                    objTempSpDetails.TotalRate = decimal.Parse(rtxtTotalPrice.Text) + objTempSpDetails.TotalRate;

                                    if (tempSpDetailses.Count == 0)
                                        tempSpDetailses = new List<TempSPDetails>();
                                    tempSpDetailses.Add(objTempSpDetails);
                                    Session["tempSpDetailses"] = tempSpDetailses;
                                }
                            }
                            else
                            {
                                objTempSpDetails = new TempSPDetails();
                                objTempSpDetails.SpairPartsId = int.Parse(rdropSpairParts.SelectedValue);
                                objTempSpDetails.Unit = int.Parse(rdropProductUnit.SelectedValue);
                                objTempSpDetails.Quantity = int.Parse(rtxtQuentity.Text);
                                objTempSpDetails.SpairParts = rdropSpairParts.SelectedItem.Text;
                                objTempSpDetails.UnitName = rdropProductUnit.SelectedItem.Text;
                                objTempSpDetails.Rate = decimal.Parse(rtxtRate.Text);
                                objTempSpDetails.Color = int.Parse(rdropColor.SelectedValue);
                                if (rdropColor.SelectedIndex <= 0)
                                {
                                    objTempSpDetails.ColorName = "";
                                }
                                else
                                {
                                    objTempSpDetails.ColorName = rdropColor.SelectedItem.Text;

                                }
                                objTempSpDetails.TotalRate = decimal.Parse(rtxtTotalPrice.Text);

                                if (tempSpDetailses.Count == 0)
                                    tempSpDetailses = new List<TempSPDetails>();
                                tempSpDetailses.Add(objTempSpDetails);
                                Session["tempSpDetailses"] = tempSpDetailses;
                            }

                        }

                        this.LoadSPDeliveryDetails();

                        decimal totalsum;

                        totalsum = tempSpDetailses.Sum(x => x.TotalRate);
                        rtxtItemTotal.Text = totalsum.ToString();
                        rtxtQuentity.Text = "";
                        rtxtTotalPrice.Text = "";
                    }
                }
                #endregion
                #region SpAdvAdd
                else if (lblAdvenceSearch.Text == "1")
                {
                    List<SpairParts> lstSpairParts =
                        new SpairParts().GetAllSpairPartsByProduct(int.Parse(rdropProduct.SelectedValue));
                    if (tempSpDetailses.Count == 0)
                        tempSpDetailses = new List<TempSPDetails>();
                    List<Product> lstProduct = new Product().GetAllProduct();

                    foreach (SpairParts sp in lstSpairParts)
                    {
                        Product objProduct = lstProduct.Find(x => x.Id == sp.SpairPartId && x.ProductCategory == 6);
                        if (rdropColor.SelectedIndex == 0)
                        {
                            objTempSpDetails =
                                tempSpDetailses.Find(
                                    x =>
                                        x.SpairPartsId == sp.SpairPartId && x.Color == int.Parse(rdropColor.SelectedValue));
                            if (objTempSpDetails != null)
                            {
                                if (objTempSpDetails.SpairPartsId != 0)
                                {
                                    tempSpDetailses.Remove(objTempSpDetails);
                                    objTempSpDetails.SpairPartsId = sp.SpairPartId;
                                    objTempSpDetails.Unit = 1;
                                    objTempSpDetails.Quantity = (int.Parse(rtxtpQuentityP.Text) *
                                                                 int.Parse(sp.Quentity.ToString())) +
                                                                objTempSpDetails.Quantity;
                                    objTempSpDetails.SpairParts = objProduct.ProductCode + ";" + objProduct.ProductName;
                                    objTempSpDetails.UnitName = "Price";
                                    objTempSpDetails.Rate = objProduct.DP;
                                    objTempSpDetails.Color = 0;
                                    objTempSpDetails.ColorName = "";
                                    objTempSpDetails.TotalRate = decimal.Parse(objTempSpDetails.Quantity.ToString()) *
                                                                 objProduct.DP;
                                    tempSpDetailses.Add(objTempSpDetails);

                                }
                            }
                            else
                            {
                                objTempSpDetails = new TempSPDetails();
                                objTempSpDetails.SpairPartsId = sp.SpairPartId;
                                objTempSpDetails.Unit = 1;
                                objTempSpDetails.Quantity = int.Parse(rtxtpQuentityP.Text) *
                                                            int.Parse(sp.Quentity.ToString());
                                objTempSpDetails.SpairParts = objProduct.ProductCode + ";" + objProduct.ProductName;
                                objTempSpDetails.UnitName = "Price";
                                objTempSpDetails.Rate = objProduct.DP;
                                objTempSpDetails.Color = 0;
                                objTempSpDetails.ColorName = "";
                                objTempSpDetails.TotalRate = decimal.Parse(rtxtpQuentityP.Text) * sp.Quentity *
                                                             objProduct.DP;
                                tempSpDetailses.Add(objTempSpDetails);
                            }

                        }
                        else
                        {
                            objTempSpDetails =
                               tempSpDetailses.Find(
                                   x =>
                                       x.SpairPartsId == sp.SpairPartId && x.Color == int.Parse(rdropColor.SelectedValue));

                            if (objTempSpDetails != null)
                            {
                                if (objTempSpDetails.SpairPartsId != 0)
                                {
                                    tempSpDetailses.Remove(objTempSpDetails);
                                    objTempSpDetails.SpairPartsId = sp.SpairPartId;
                                    objTempSpDetails.Unit = 1;
                                    objTempSpDetails.Quantity = (int.Parse(rtxtpQuentityP.Text) *
                                                                 int.Parse(sp.Quentity.ToString())) +
                                                                objTempSpDetails.Quantity;
                                    objTempSpDetails.SpairParts = objProduct.ProductCode + ";" + objProduct.ProductName;
                                    objTempSpDetails.UnitName = "Price";
                                    objTempSpDetails.Rate = objProduct.DP;
                                    objTempSpDetails.Color = int.Parse(rdropColor.SelectedValue);
                                    objTempSpDetails.ColorName = rdropColor.SelectedItem.Text;
                                    objTempSpDetails.TotalRate = decimal.Parse(objTempSpDetails.Quantity.ToString()) *
                                                                 objProduct.DP;
                                    tempSpDetailses.Add(objTempSpDetails);

                                }
                            }
                            else
                            {
                                objTempSpDetails = new TempSPDetails();
                                objTempSpDetails.SpairPartsId = sp.SpairPartId;
                                objTempSpDetails.Unit = 1;
                                objTempSpDetails.Quantity = int.Parse(rtxtpQuentityP.Text) *
                                                            int.Parse(sp.Quentity.ToString());
                                objTempSpDetails.SpairParts = objProduct.ProductCode + ";" + objProduct.ProductName;
                                objTempSpDetails.UnitName = "Price";
                                objTempSpDetails.Rate = objProduct.DP;
                                objTempSpDetails.Color = int.Parse(rdropColor.SelectedValue);
                                objTempSpDetails.ColorName = rdropColor.SelectedItem.Text;
                                objTempSpDetails.TotalRate = decimal.Parse(rtxtpQuentityP.Text) * sp.Quentity *
                                                             objProduct.DP;
                                tempSpDetailses.Add(objTempSpDetails);
                            }

                        }
                    }

                    Session["tempSpDetailses"] = tempSpDetailses;
                    this.LoadSPDeliveryDetails();

                    decimal totalsum;

                    totalsum = tempSpDetailses.Sum(x => x.TotalRate);
                    rtxtItemTotal.Text = totalsum.ToString();
                }

                #endregion SpAdvAdd
                #region SpEdit
                if (lblselect.Text == "3")
                {
                    if (rdropColor.SelectedIndex != 0)
                    {
                        objTempSpDetails =
                            tempSpDetailses.Find(
                                x =>
                                    x.SpairPartsId == int.Parse(rdropSpairParts.SelectedValue) &&
                                    x.Color == int.Parse(rdropColor.SelectedValue));
                        if (objTempSpDetails != null)
                            tempSpDetailses.Remove(objTempSpDetails);
                        else
                            objTempSpDetails = new TempSPDetails();
                        objTempSpDetails.SpairPartsId = int.Parse(rdropSpairParts.SelectedValue);
                        objTempSpDetails.Unit = int.Parse(rdropProductUnit.SelectedValue);
                        objTempSpDetails.Quantity = int.Parse(rtxtQuentity.Text);
                        objTempSpDetails.SpairParts = rdropSpairParts.SelectedItem.Text;
                        objTempSpDetails.UnitName = rdropProductUnit.SelectedItem.Text;
                        objTempSpDetails.Rate = decimal.Parse(rtxtRate.Text);
                        objTempSpDetails.Color = int.Parse(rdropColor.SelectedValue);
                        if (rdropColor.SelectedIndex <= 0)
                            objTempSpDetails.ColorName = "";
                        else
                            objTempSpDetails.ColorName = rdropColor.SelectedItem.Text;

                        objTempSpDetails.TotalRate = decimal.Parse(rtxtTotalPrice.Text);

                        if (tempSpDetailses.Count == 0)
                            tempSpDetailses = new List<TempSPDetails>();
                        tempSpDetailses.Add(objTempSpDetails);
                        Session["tempSpDetailses"] = tempSpDetailses;
                    }
                    else if (rdropColor.SelectedIndex == 0)
                    {
                        objTempSpDetails =
                           tempSpDetailses.Find(
                               x =>
                                   x.SpairPartsId == int.Parse(rdropSpairParts.SelectedValue));
                        if (objTempSpDetails != null)
                            tempSpDetailses.Remove(objTempSpDetails);
                        else
                            objTempSpDetails = new TempSPDetails();

                        objTempSpDetails.SpairPartsId = int.Parse(rdropSpairParts.SelectedValue);
                        objTempSpDetails.Unit = int.Parse(rdropProductUnit.SelectedValue);
                        objTempSpDetails.Quantity = int.Parse(rtxtQuentity.Text);
                        objTempSpDetails.SpairParts = rdropSpairParts.SelectedItem.Text;
                        objTempSpDetails.UnitName = rdropProductUnit.SelectedItem.Text;
                        objTempSpDetails.Rate = decimal.Parse(rtxtRate.Text);
                        objTempSpDetails.Color = int.Parse(rdropColor.SelectedValue);
                        if (rdropColor.SelectedIndex <= 0)
                            objTempSpDetails.ColorName = "";
                        else
                            objTempSpDetails.ColorName = rdropColor.SelectedItem.Text;

                        objTempSpDetails.TotalRate = decimal.Parse(rtxtTotalPrice.Text);

                        if (tempSpDetailses.Count == 0)
                            tempSpDetailses = new List<TempSPDetails>();
                        tempSpDetailses.Add(objTempSpDetails);
                        Session["tempSpDetailses"] = tempSpDetailses;

                    }
                    this.LoadSPDeliveryDetails();

                    decimal totalsum;

                    totalsum = tempSpDetailses.Sum(x => x.TotalRate);
                    rtxtItemTotal.Text = totalsum.ToString();
                }

                #endregion SpEdit

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to Load data." + ex.Message);
            }
        }

        protected void rdropDeliveryMethod_OnDataBound(object sender, EventArgs e)
        {
            rdropDeliveryMethod.Items.Insert(0, new RadComboBoxItem());
        }

        protected void RadGridAddSPDetails_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnSelect")
                {
                    ShowNormal.Visible = true;
                    btnShowPanal.Visible = false;

                    GridDataItem item = (GridDataItem)e.Item;
                    lblselect.Text = "3";
                    lblNormal.Text = "";
                    lblAdvenceSearch.Text = "";

                    lblDetails.Text = item["colId"].Text;
                    rdropSpairParts.SelectedValue = item["colSpairPartsId"].Text;
                    rdropProductUnit.SelectedValue = item["colUnit"].Text;
                    rtxtQuentity.Text = item["colQuantity"].Text;
                    rdropColor.SelectedValue = item["colColor"].Text;
                    rtxtRate.Text = item["colRate"].Text;
                    rtxtTotalPrice.Text = item["colTotalProductPrice"].Text;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void rdropProduct_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void rdropProduct_OnDataBound(object sender, EventArgs e)
        {
            rdropProduct.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropSpairParts_OnDataBound(object sender, EventArgs e)
        {
            rdropSpairParts.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropServiceCenter_OnDataBound(object sender, EventArgs e)
        {
            rdropServiceCenter.Items.Insert(0, new RadComboBoxItem());
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                #region validation

                if (rdropDeliveryMethod.SelectedIndex == 0)
                {
                    Alert.Show("Please select a Delivery Method.");
                    rdropDeliveryMethod.Focus();
                    return;
                }
                if (rdropServiceCenter.SelectedIndex == 0)
                {
                    Alert.Show("Please select a service center.");
                    rdropServiceCenter.Focus();
                    return;
                }
                if (rdtCreateDate.SelectedDate == null)
                {
                    Alert.Show("Please select a Create date.");
                    rdtCreateDate.Focus();
                    return;
                }
                if (rdtDeliveryDate.SelectedDate == null)
                {
                    Alert.Show("Please select a Create date.");
                    rdtDeliveryDate.Focus();
                    return;
                }
                #endregion

                SPDeliveryMaster objSpDeliveryMaster = new SPDeliveryMaster();

                DateTime cDate = DateTime.Parse(rdtCreateDate.SelectedDate.ToString());
                DateTime dDate = DateTime.Parse(rdtDeliveryDate.SelectedDate.ToString());

                objSpDeliveryMaster.CreateDate = DateTime.Parse(cDate.ToString("MMM dd,yyyy"));
                objSpDeliveryMaster.ServiceCenterId = int.Parse(rdropServiceCenter.SelectedValue);
                objSpDeliveryMaster.DeliveryMethod =rdropDeliveryMethod.SelectedValue;
                objSpDeliveryMaster.DeliveryDate = DateTime.Parse(dDate.ToString("MMM dd,yyyy"));
                objSpDeliveryMaster.UserId = _user.Id;
                objSpDeliveryMaster.ApproveBy = 0;
                objSpDeliveryMaster.ApproveDate = PublicVariables.minDate;
                objSpDeliveryMaster.Status = "Created";
                objSpDeliveryMaster.ReceiveDate = PublicVariables.minDate;
                objSpDeliveryMaster.ReceiveBy = 0;

                int success = 0;
                if (bool.Parse(lblnewEntry.Text))
                {
                    lblTransactionCode.Text = new SPDeliveryMaster().GetlastTransactionCode();
                    objSpDeliveryMaster.TransactionCode = lblTransactionCode.Text;
                    success = objSpDeliveryMaster.InsertSPDeliveryMaster();
                    lblnewEntry.Text = "false";
                    lblId.Text = new SPDeliveryMaster().GetMaxSPDeliveryMasterId().ToString();
                }
                else
                {
                    objSpDeliveryMaster.Id = int.Parse(lblId.Text);
                    objSpDeliveryMaster.TransactionCode = lblTransactionCode.Text;

                    success = objSpDeliveryMaster.UpdateSPDeliveryMaster();
                }
                if (success == 0)
                {
                    Alert.Show("Spair Parts Delivery master data is not save succesfully");
                    return;
                }
                if (tempSpDetailses.Count != 0)
                {
                    int update;
                    int masterid = 0;

                    foreach (TempSPDetails tmpSpDetails in tempSpDetailses)
                    {
                        SPDeliveryDetails objspDeliveryDetails=new SPDeliveryDetails();

                        masterid = int.Parse(lblId.Text);
                        this.LoadSpairtPartsDataFromDatabase(masterid);

                        lblDetails.Text = tmpSpDetails.Id.ToString();
                        objspDeliveryDetails.MasterId = masterid;
                        objspDeliveryDetails.SpairPartsId = tmpSpDetails.SpairPartsId;
                        objspDeliveryDetails.Color = tmpSpDetails.Color;
                        objspDeliveryDetails.Rate = tmpSpDetails.Rate;
                        objspDeliveryDetails.Quantity = tmpSpDetails.Quantity;
                        objspDeliveryDetails.TotalAmount = tmpSpDetails.TotalRate;
                        objspDeliveryDetails.Unit = tmpSpDetails.Unit;
                        List<SPDeliveryDetails> lstspDeliveryDetailses=new SPDeliveryDetails().GetAllSPDealerDetailsBymasterIdSpIdCateIdColorId(objspDeliveryDetails.SpairPartsId,objspDeliveryDetails.Unit,objspDeliveryDetails.Color,masterid);

                        if (lstspDeliveryDetailses.Count == 0)
                        {
                            success = objspDeliveryDetails.InsertSPDeliveryDetails();
                        }
                        else
                        {
                            objspDeliveryDetails.Id = int.Parse(lblDetails.Text);
                            success = objspDeliveryDetails.UpdateSPDeliveryDetails();
                        }
                        if (success == 0)
                        {
                            Alert.Show("Spair Parts Details Data isnot save succesfully..");
                        }
                        else
                        {
                            Alert.Show("Data save succesfully");
                            this.ClearAllInfo();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save data.error: " + ex);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearAllInfo();
        }

        private void ClearAllInfo()
        {
            rdropDeliveryMethod.SelectedIndex = 0;
            rdropServiceCenter.SelectedIndex = 0;
            rdtCreateDate.SelectedDate = null;
            rdtDeliveryDate.SelectedDate = null;
            showPanal.Visible = false;
            ShowNormal.Visible = true;
            btnShowPanal.Text = "Advance Search";
            rdropSpairParts.SelectedIndex = 0;
            rdropColor.SelectedIndex = 0;
            rtxtItemTotal.Text = "";
            rtxtRate.Text = "";
            lblAdvenceSearch.Text = "";
            lblselect.Text = "";
            lblNormal.Text = "";
            lblnewEntry.Text = "true";
            btnShowPanal.Visible = true;

            RadGridAddSPDetails.DataSource = new string[] {};
            RadGridAddSPDetails.DataBind();
            tempSpDetailses=new List<TempSPDetails>();
            Session["tempSpDetailses"] = null;
        }

    }
}