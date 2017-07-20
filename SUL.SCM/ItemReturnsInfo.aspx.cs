using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;
using Telerik.Web.UI.ComboBox;

namespace SUL.SCM
{
    public partial class ItemReturnsInfo : System.Web.UI.Page
    {
        private Users _user;
        private Company _company;
        private List<TempReturnsDetails> tempReturnsDetailses;

        private string _department;

        private AppPermission PermissionUser;

        private class TempReturnsDetails
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public int ProductId { get; set; }
            public int ReturnQuentity { get; set; }
            public decimal ReturnRate { get; set; }

            public decimal LineTotal { get; set; }

            public int ColorId { get; set; }
            public int UnitId { get; set; }

            public string Color { get; set; }

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
            //int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Item Returns");
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Item Returns") : new AppFunctionality().GetAppFunctionalityId("Item Returns", int.Parse(lblsource.Text));
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

        private void LoadDealerInfo()
        {
            try
            {
                DataTable dtDealerInfo;
                if (showPanal.Visible == false)
                {
                    dtDealerInfo = new DealerInformation().GetAllDealerInformationView();
                }

                else
                {
                    if (rdropArea.SelectedIndex == 0 && rdropRegion.SelectedIndex == 0)
                    {
                        dtDealerInfo = new DealerInformation().GetAllDealerInformationView();
                    }
                    else if (rdropArea.SelectedIndex == 0)
                    {
                        dtDealerInfo =
                            new DealerInformation().GetAllDealerInformationViewByRegion(
                                int.Parse(rdropRegion.SelectedValue));
                    }
                    else
                    {
                        dtDealerInfo =
                            new DealerInformation().GetAllDealerInformationViewByArea(int.Parse(rdropArea.SelectedValue));
                    }

                }

                rdropPDealer.DataTextField = "DealerInfo";
                rdropPDealer.DataValueField = "Id";
                rdropPDealer.DataSource = dtDealerInfo;
                rdropPDealer.DataBind();

                rdropPDealer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
            }
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
                rdropRegion.SelectedIndex = lstRegions.Rows.Count == 2 ? 1 : 0;


            }

            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load region." + ex);
            }
        }
        private void LoadArea(int region)
        {
            try
            {
                DataTable lstRegions = new Area().GetAllViewAreaByRegionId(region);

                rdropArea.DataTextField = "AreaInfo";
                rdropArea.DataValueField = "Id";
                rdropArea.DataSource = lstRegions;
                rdropArea.DataBind();
                rdropArea.SelectedIndex = lstRegions.Rows.Count == 2 ? 1 : 0;


            }

            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load region." + ex);
            }
        }

        private void LoadProduct(int DealerId)
        {
            try
            {
                string whereCondition = " Where DealerId = " + DealerId + " And Status = 'Invoiced'";
                DataTable dtRequisitionInfo = new InvoiceMaster().GetInvoiceFromViewListByDealerId(DealerId);

                rdropProduct.DataTextField = "ProductName";
                rdropProduct.DataValueField = "ProductId";
                rdropProduct.DataSource = dtRequisitionInfo;
                rdropProduct.DataBind();

                rdropProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to Load RequisitionCode." + ex);
            }
        }
        private void LoadReturnDetails()
        {
            try
            {
                if (tempReturnsDetailses.Count == 0)
                {
                    RadGridAddItemReturnsDetails.DataSource = new string[] { };
                    return;
                }

                RadGridAddItemReturnsDetails.DataSource = tempReturnsDetailses;
                RadGridAddItemReturnsDetails.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load requisition details grid." + ex);
            }
        }

        private void LLoadReturnDetailsFromDatabase(int id)
        {
            try
            {
                if (bool.Parse(lblnewEntry.Text) == false)
                {
                    int ReturnQ = 0;
                    tempReturnsDetailses = new List<TempReturnsDetails>();

                    List<ItemReturnDetails> lstReturnDetailses =
                        new ItemReturnDetails().GetAllItemReturnDetails();
                    List<ItemReturnDetails> lstItemReturnDetails =
                        new ItemReturnDetails().GetAllItemReturnDetailsByMasterId(id);
                    List<Product> productList = new Product().GetAllProduct();
                    List<ListTable> lstListTables = new ListTable().GetAllListTableByType("Color");
                    List<ProductUnit> lstProductUnits = new ProductUnit().GetAllProductUnit();
                    if (lstItemReturnDetails.Count > 0)
                    {
                        tempReturnsDetailses = new List<TempReturnsDetails>();
                        foreach (ItemReturnDetails lstDetails in lstItemReturnDetails)
                        {
                            TempReturnsDetails objReturnsDetails = new TempReturnsDetails();

                            if (lstReturnDetailses.Count != 0)
                            {
                                ReturnQ =
                                   lstReturnDetailses.Find(x => x.ProductId == lstDetails.ProductId)
                                       .ReturnQuantity;
                            }
                            Product prod = productList.Find(x => x.Id == lstDetails.ProductId);
                            objReturnsDetails.Id = lstDetails.Id;
                            
                            objReturnsDetails.ProductId = lstDetails.ProductId;
                            objReturnsDetails.ProductName = prod.ProductName;
                            objReturnsDetails.ReturnQuentity = lstDetails.ReturnQuantity;
                            objReturnsDetails.ReturnRate = lstDetails.ReturnRate;
                            objReturnsDetails.LineTotal = lstDetails.LineTotal;
                            objReturnsDetails.ColorId = lstDetails.ColorId;
                            objReturnsDetails.UnitId = lstDetails.UnitId;
                            ListTable lst = lstListTables.Find(x => x.ListId == lstDetails.ColorId);
                            if (lst == null)
                                objReturnsDetails.Color = "";
                            else
                                objReturnsDetails.Color = lst.ListValue;

                            tempReturnsDetailses.Add(objReturnsDetails);
                        }
                        Session["tempReturnsDetailses"] = tempReturnsDetailses;
                        this.LoadReturnDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Requisition details data." + ex);
            }
        }

        private void LoadColorByProduct(int productid)
        {
            try
            {
                DataTable dtProductColor = new ProductColor().GetAllVewProductColorbyProductid(productid);

                rdropColor.DataTextField = "ColorName";
                rdropColor.DataValueField = "Color";
                rdropColor.DataSource = dtProductColor;
                rdropColor.DataBind();
                rdropColor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
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

            if (Session["tempReturnsDetailses"] != null)
                tempReturnsDetailses = (List<TempReturnsDetails>)Session["tempReturnsDetailses"];
            else
                tempReturnsDetailses = new List<TempReturnsDetails>();

            if (!IsPostBack)
            {

                if (_user.EmployeeId != 0)
                {
                    Department objDepartment = new Department().GetEmployeeDepartment(_user.EmployeeId);
                    _department = objDepartment.DepartmentName;
                }

                lblnewEntry.Text = "true";
                this.ClearAllInfo();
                this.LoadDealerInfo();
                this.LoadRegion();

                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];

                    ItemReturnMaster objItemReturnMaster = new ItemReturnMaster().GetItemReturnMasterById(int.Parse(id));
                    if (objItemReturnMaster.Status.ToLower() == "received")
                    {
                        btnSave.Visible = false;
                        btnClear.Visible = false;
                        btnAddReturnDetails.Visible = false;
                    }
                    lblId.Text = objItemReturnMaster.Id.ToString();
                    rdropPDealer.SelectedValue = objItemReturnMaster.DealerId.ToString();
                    //this.(objItemReturnMaster.DealerId);
                    // rdropRequisitionCode.SelectedValue = objItemReturnMaster.RequisitionId.ToString();
                    this.LoadProduct(objItemReturnMaster.DealerId);
                    rdtReturnsDate.SelectedDate = objItemReturnMaster.ReturnDate;
                    lblReturnCode.Text = objItemReturnMaster.ReturnCode;
                    rtxtRemarks.Value = objItemReturnMaster.Remarks;
                    rtxtCharges.Text = objItemReturnMaster.Charges.ToString();
                    rtxtItemTotal.Text = objItemReturnMaster.ItemTotal.ToString();
                    lblnewEntry.Text = "false";

                    this.LLoadReturnDetailsFromDatabase(int.Parse(id));
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
                if (bool.Parse(lblnewEntry.Text))
                {
                    if (!PermissionUser.IsInsert)
                    {
                        Alert.Show("Sorry, You Don't Have permission to access this page.");
                        Response.Redirect("ErrorPage.aspx", false);
                    }
                }
                else
                {
                    if (!PermissionUser.IsUpdate)
                    {
                        btnSave.Visible = false;
                        btnClear.Visible = false;
                    }
                    if (!PermissionUser.IsDelete)
                    {
                        // hide delete button
                    }
                }

                if (PermissionUser.IsView && !PermissionUser.IsInsert && !PermissionUser.IsUpdate)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                Regex regexForPriceUnite = new Regex(@"^[0-9]\d*(\.\d+)?$");
                #region validation

                if (rdropPDealer.SelectedIndex <= 0)
                {
                    Alert.Show("Please Select a Dealer.");
                    rdropPDealer.Focus();
                    return;
                }
                if (rdtReturnsDate.SelectedDate == null)
                {
                        Alert.Show("Please enter a return date");
                        rdtReturnsDate.Focus();
                        return;
                }
                if (rdropProduct.SelectedIndex <= 0)
                {
                    Alert.Show("Please Select a Product Code.");
                    rdropProduct.Focus();
                    return;
                }
                if (rtxtCharges.Text != string.Empty)
                {
                    if (!regexForPriceUnite.IsMatch(rtxtCharges.Text))
                    {
                        Alert.Show("Please enter a valid Amount");
                        rtxtCharges.Focus();
                        return;
                    }
                }

                #endregion

                ItemReturnMaster objItemReturnMaster = new ItemReturnMaster();
                objItemReturnMaster.DealerId = int.Parse(rdropPDealer.SelectedValue);
                objItemReturnMaster.ReturnDate = DateTime.Parse(rdtReturnsDate.SelectedDate.ToString());
                objItemReturnMaster.Remarks = rtxtRemarks.Value;
                objItemReturnMaster.UserId = _user.Id;
                objItemReturnMaster.ReceiveBy = 0;
                objItemReturnMaster.Status = "Created";
                objItemReturnMaster.ItemTotal = decimal.Parse(rtxtItemTotal.Text);
                objItemReturnMaster.Charges = rtxtCharges.Text == string.Empty ? 0 : decimal.Parse(rtxtCharges.Text);

                int success;

                if (bool.Parse(lblnewEntry.Text))
                {
                    string ReCode = new ItemReturnMaster().GetlastItemReturnCode();
                    objItemReturnMaster.ReturnCode = ReCode;

                    success = objItemReturnMaster.InsertItemReturnMaster();
                    lblnewEntry.Text = "false";
                    lblId.Text = new ItemReturnMaster().GetMaxItemReturnMasterId().ToString();
                }
                else
                {
                    objItemReturnMaster.Id = int.Parse(lblId.Text);
                    objItemReturnMaster.ReturnCode = lblReturnCode.Text;
                    success = objItemReturnMaster.UpdateItemReturnMaster();
                }
                if (success == 0)
                {
                    Alert.Show("ItemReturn Mastre data is not save succesfully");

                }
                else
                {
                    int masterid = 0;
                    foreach (TempReturnsDetails tmpReturnsDetails in tempReturnsDetailses)
                    {
                        ItemReturnDetails objReturnDetails = new ItemReturnDetails();

                        masterid = int.Parse(lblId.Text);
                        this.LLoadReturnDetailsFromDatabase(masterid);

                        lblDetails.Text = tmpReturnsDetails.Id.ToString();
                        objReturnDetails.MasterId = masterid;
                        objReturnDetails.ProductId = tmpReturnsDetails.ProductId;
                        objReturnDetails.ReturnQuantity = tmpReturnsDetails.ReturnQuentity;
                        objReturnDetails.ReturnRate = tmpReturnsDetails.ReturnRate;
                        objReturnDetails.LineTotal = tmpReturnsDetails.LineTotal;
                        objReturnDetails.ColorId = tmpReturnsDetails.ColorId;
                        objReturnDetails.UnitId = tmpReturnsDetails.UnitId;

                        if (objReturnDetails.ReturnQuantity != 0)
                        {

                            if (lblDetails.Text == string.Empty || lblDetails.Text == "0")
                            {
                                success = objReturnDetails.InsertItemReturnDetails();
                            }
                            else
                            {
                                objReturnDetails.Id = int.Parse(lblDetails.Text);
                                success = objReturnDetails.UpdateItemReturnDetails();
                            }
                            if (success == 0)
                            {
                                Alert.Show("Requisition Data isnot save succesfully..");
                            }
                            else
                            {
                                Alert.Show("Data save succesfully");

                            }
                            
                        }

                    }
                    if (success != 0)
                    {
                        this.ClearAllInfo();
                    }
                    else
                    {
                        Alert.Show("Data is not save succesfully");
                    }

                }

            }
            catch (Exception ex)
            {
                Alert.Show("Something is goingwron to save data. " + ex);
            }
        }



        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearAllInfo();
        }

        protected void btnAddReturnDetails_OnClick(object sender, EventArgs e)
        {
            try
            {
                Regex regexForQuentity = new Regex("^[0-9]*$");
                Regex regexForPriceUnite = new Regex(@"^[0-9]\d*(\.\d+)?$");

                #region validation
                if (rtxtReturnQuentity.Text == string.Empty || !regexForQuentity.IsMatch(rtxtReturnQuentity.Text))
                {
                    Alert.Show("Please enter a valid quantity");
                    rtxtReturnQuentity.Focus();
                    return;
                }
                
                if (rtxtReturnRate.Text == string.Empty || !regexForPriceUnite.IsMatch(rtxtReturnRate.Text))
                {
                    Alert.Show("Please enter a valid quantity");
                    rtxtReturnQuentity.Focus();
                    return;
                }
                
                #endregion
                TempReturnsDetails objTempReturnsDetails =
                    tempReturnsDetailses.Find(x => x.ProductId == int.Parse(rdropProduct.SelectedValue) && x.ColorId == int.Parse(rdropColor.SelectedValue));
                if (objTempReturnsDetails != null)
                {
                    if (objTempReturnsDetails.ProductId == 0)
                        objTempReturnsDetails = new TempReturnsDetails();
                    else
                    {
                        tempReturnsDetailses.Remove(objTempReturnsDetails);
                    }
                }
                else
                    objTempReturnsDetails = new TempReturnsDetails();

                objTempReturnsDetails.ProductId = int.Parse(rdropProduct.SelectedValue);
                objTempReturnsDetails.ProductName = rdropProduct.SelectedItem.Text;
                objTempReturnsDetails.ReturnQuentity = int.Parse(rtxtReturnQuentity.Text);
                objTempReturnsDetails.ReturnRate = decimal.Parse(rtxtReturnRate.Text);
                objTempReturnsDetails.LineTotal = decimal.Parse(rtxtReturnRate.Text) * int.Parse(rtxtReturnQuentity.Text);
                objTempReturnsDetails.ColorId = rdropColor.SelectedIndex == 0 ? 0 : int.Parse(rdropColor.SelectedValue);
                objTempReturnsDetails.UnitId = lblUnitId.Text == string.Empty ? 0 : int.Parse(lblUnitId.Text);
                objTempReturnsDetails.Color =  rdropColor.SelectedIndex == 0 ? "" :rdropColor.SelectedItem.Text ;
                if (tempReturnsDetailses.Count == 0)
                    tempReturnsDetailses = new List<TempReturnsDetails>();
                tempReturnsDetailses.Add(objTempReturnsDetails);
                Session["tempReturnsDetailses"] = tempReturnsDetailses;
                this.LoadReturnDetails();

                decimal totalsum;

                totalsum = tempReturnsDetailses.Sum(x => x.LineTotal);
                rtxtItemTotal.Text = totalsum.ToString();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to add details. " + ex);
            }
        }

        private void ClearAllInfo()
        {
            rdropPDealer.SelectedIndex = 0;
            rdropProduct.SelectedIndex = 0;   
            rdtReturnsDate.SelectedDate = null;
            rtxtRemarks.Value = "";
            rtxtCharges.Text = "";
            rtxtReturnQuentity.Text = "";
            rtxtReturnRate.Text = "";
            rtxtItemTotal.Text = "";
            lblId.Text = "";
            lblProductId.Text = "";
            lblReturnCode.Text = "";
            lblDetails.Text = "";
            lblColor.Text = "";
            lblColorId.Text = "";
            lblUnit.Text = "";
            lblUnitId.Text = "";
            lblnewEntry.Text = "true";
            RadGridAddItemReturnsDetails.DataSource = new string[] { };
            RadGridAddItemReturnsDetails.DataBind();
            tempReturnsDetailses = new List<TempReturnsDetails>();
            Session["tempReturnsDetailses"] = tempReturnsDetailses;

        }

        protected void RadGridAddItemReturnsDetails_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnSelect")
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    lblDetails.Text = item["colId"].Text;
      
                    rtxtReturnQuentity.Text = item["colReturnQuentity"].Text == "&nbsp;" ? "0" : item["colReturnQuentity"].Text;
                    rtxtReturnRate.Text = item["colReturnRate"].Text == "&nbsp;" ? "0" : item["colReturnRate"].Text;
                    rdropProduct.SelectedValue = item["colProductId"].Text;
                    lblColor.Text = item["colColor"].Text;
                    lblColorId.Text = item["colColorId"].Text;
                    lblUnitId.Text = item["colUnitId"].Text;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong " + ex);
            }
        }


        protected void rdropPDealer_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            this.LoadProduct(int.Parse(rdropPDealer.SelectedValue));
        }

        protected void rdropArea_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            this.LoadDealerInfo();
        }

        protected void rdropRegion_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            this.LoadArea(int.Parse(rdropRegion.SelectedValue));
            this.LoadDealerInfo();
        }

        protected void rdropRegion_OnDataBound(object sender, EventArgs e)
        {
            rdropRegion.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropArea_OnDataBound(object sender, EventArgs e)
        {
            rdropArea.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropPDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropPDealer.Items.Insert(0, new RadComboBoxItem());
        }


        protected void btnShowPanal_OnClick(object sender, EventArgs e)
        {
            if (showPanal.Visible == false)
            {
                showPanal.Visible = true;
                btnShowPanal.Text = "Hide Search";
            }

            else
            {
                showPanal.Visible = false;
                btnShowPanal.Text = "Advance Search";
                rdropRegion.SelectedIndex = 0;
                rdropArea.SelectedIndex = 0;
                this.LoadDealerInfo();
            }
        }

        protected void rdropProduct_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblQuantity.Text =
                new InvoiceMaster().GetInvoiceFromViewListByDealerIdByProductId(int.Parse(rdropPDealer.SelectedValue),
                    int.Parse(rdropProduct.SelectedValue));
            LoadColorByProduct(int.Parse(rdropProduct.SelectedValue));

        }

        protected void rdropProduct_OnDataBound(object sender, EventArgs e)
        {
            rdropProduct.Items.Insert(0,new RadComboBoxItem());
        }

        protected void rdropColor_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected void rdropColor_OnDataBound(object sender, EventArgs e)
        {
            rdropColor.Items.Insert(0, new RadComboBoxItem());
        }
    }
}