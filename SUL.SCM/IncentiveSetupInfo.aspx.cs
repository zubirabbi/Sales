using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;
using Telerik.Web.UI.ComboBox;

namespace SUL.SCM
{
    public partial class IncentiveSetupInfo : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        private Company _company;
        private List<TempIncentiveSetupInfo> tempIncentiveSetupInfo;

        private DataTable dtIncentiveSetup;


        private int _source;
        private int success;

        private class TempIncentiveSetupInfo
        {
            public int Id { get; set; }
            public int Slno { get; set; }
            public decimal StartValue { get; set; }
            public decimal EndValue { get; set; }
            public decimal Percentage { get; set; }
            public decimal Value { get; set; }

        }


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
            //int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Requisition Add");
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Insentive Setup") : new AppFunctionality().GetAppFunctionalityId("Insentive Setup", int.Parse(lblsource.Text));
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



        private void LoadType()
        {
            try
            {
                DataTable lstListTables = new ListTable().GetAllDataByListTypeId("Incentive");

                rdropType.DataTextField = "ListValue";
                rdropType.DataValueField = "ListId";
                rdropType.DataSource = lstListTables;
                rdropType.DataBind();

                rdropType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                Alert.Show("something is going wrong to load Type info." + ex);
            }
        }

        private void LoadProductInfo()
        {
            try
            {
                DataTable dtProductInfo = new Product().GetProductFromViewList();

                rdropProductName.DataTextField = "ProInfo";
                rdropProductName.DataValueField = "Id";
                rdropProductName.DataSource = dtProductInfo;
                rdropProductName.DataBind();

                rdropProductName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load product data.");
            }
        }

        private void LoadSalesPerson()
        {
            try
            {
                DataTable dtSalesPerson = new Designation().GetAllDesignationByDeptId(2028);

                rdropSalesPerson.DataTextField = "DesignationName";
                rdropSalesPerson.DataValueField = "Id";
                rdropSalesPerson.DataSource = dtSalesPerson;
                rdropSalesPerson.DataBind();
            }
            catch (Exception ex)
            {

                Alert.Show(ex.Message);
            }
        }

        private void LoadIncentiveSetup()
        {
            try
            {
                if (tempIncentiveSetupInfo.Count == 0)
                {
                    RadGridIncentiveSetupinfo.DataSource = new string[] { };
                    RadGridIncentiveSetupinfo.DataBind();
                    return;
                }

                RadGridIncentiveSetupinfo.DataSource = tempIncentiveSetupInfo;
                RadGridIncentiveSetupinfo.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load insentive setup grid." + ex);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblsource.Text = Request.QueryString["source"] != null ? Request.QueryString["source"] : "0";

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
            if (Session["dtIncentiveSetup"] != null)
            {
                dtIncentiveSetup = (DataTable)Session["dtIncentiveSetup"];
            }

            if (Session["tempIncentiveSetupInfo"] != null)
                tempIncentiveSetupInfo = (List<TempIncentiveSetupInfo>)Session["tempIncentiveSetupInfo"];
            else
                tempIncentiveSetupInfo = new List<TempIncentiveSetupInfo>();

            if (!IsPostBack)
            {
                lblisNewEntry.Text = "true";
                tempIncentiveSetupInfo = new List<TempIncentiveSetupInfo>();
                Session["tempIncentiveSetupInfo"] = null;

                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                this.LoadType();
                this.LoadSalesPerson();
                this.LoadIncentiveSetup();

            }

        }


        protected void RadGridIncentiveSetupInfo_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "btnDelete")
                {

                    GridDataItem item = (GridDataItem) e.Item;
                    lblDetails.Text = item["colId"].Text;
                    if (lblDetails.Text == string.Empty || lblDetails.Text == "0")
                    {
                        TempIncentiveSetupInfo objTempRequisitionDetailses =
                            tempIncentiveSetupInfo.Find(
                                x =>
                                    x.StartValue == int.Parse(item["colStartValue"].Text) &&
                                    x.Percentage == int.Parse(item["colPercentage"].Text));
                        tempIncentiveSetupInfo.Remove(objTempRequisitionDetailses);
                        Session["tempIncentiveSetupInfo"] = tempIncentiveSetupInfo;

                        RadGridIncentiveSetupinfo.DataSource = tempIncentiveSetupInfo;
                        RadGridIncentiveSetupinfo.DataBind();
                    }
                    else
                    {
                        IncentiveSetupDetails objIncentiveSetupDetails = new IncentiveSetupDetails();

                        int delete = 0;
                        delete = objIncentiveSetupDetails.DeleteIncentiveSetupDetailsById(int.Parse(lblDetails.Text));
                        if (delete == 0)
                            Alert.Show("data is not save succesfully");
                        else
                        {
                            if (rdropType.SelectedItem.Text == "Dealer")
                            {
                                List<IncentiveSetupDetails> details =
                                    new IncentiveSetupDetails().GetIncentiveSetupDetailsByMasterId(int.Parse(lblId.Text));
                                if (details.Count == 0)
                                {
                                    RadGridIncentiveSetupinfo.DataSource = new string[] {};
                                    RadGridIncentiveSetupinfo.DataBind();
                                    return;
                                }
                                RadGridIncentiveSetupinfo.DataSource = details;
                                RadGridIncentiveSetupinfo.DataBind();
                            }
                            else if (rdropType.SelectedItem.Text == "SalesPerson")
                            {
                                List<IncentiveSetupDetails> details =
                                    new IncentiveSetupDetails().GetIncentiveDetailsForDesignation(
                                        int.Parse(rdropSalesPerson.SelectedValue));
                                if (details.Count == 0)
                                {
                                    RadGridIncentiveSetupinfo.DataSource = new string[] {};
                                    RadGridIncentiveSetupinfo.DataBind();
                                    return;
                                }
                                RadGridIncentiveSetupinfo.DataSource = details;
                                RadGridIncentiveSetupinfo.DataBind();
                            }

                        }
                    }
                }

                else if (e.CommandName == "btnSelect")
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    lblDetails.Text = item["colId"].Text;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void rdropType_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (rdropType.SelectedItem.Text == "SalesPerson")
            {
                rdropSalesPerson.Enabled = true;
                tempIncentiveSetupInfo = new List<TempIncentiveSetupInfo>();
                Session["tempIncentiveSetupInfo"] = null;
                lblslno.Text = "";
                this.LoadIncentiveSetup();
            }
            else if (rdropType.SelectedItem.Text == "Dealer")
            {
                rdropSalesPerson.SelectedIndex = 0;
                rdropSalesPerson.Enabled = false;

                //load incentive slabs
                IncentiveSetup master = new IncentiveSetup().GetDealerMaster();
                if (master.Id == 0)
                {
                    lblId.Text = "";
                    lblslno.Text = "";
                    lblisNewEntry.Text = "true";
                }
                else
                {

                    lblId.Text = master.Id.ToString();
                    lblisNewEntry.Text = "false";

                    LoadDealerInsentiveFromDatabase();
                    this.LoadIncentiveSetup();
                }
            }
        }

        private void LoadDealerInsentiveFromDatabase()
        {
            List<IncentiveSetupDetails> details =
                new IncentiveSetupDetails().GetIncentiveSetupDetailsByMasterId(int.Parse(lblId.Text));
            tempIncentiveSetupInfo = new List<TempIncentiveSetupInfo>();
            Session["tempIncentiveSetupInfo"] = null;
            foreach (IncentiveSetupDetails detail in details)
            {
                TempIncentiveSetupInfo obj = new TempIncentiveSetupInfo()
                {
                    Id = detail.Id,
                    StartValue = detail.StartValue,
                    EndValue = detail.EndValue,
                    Percentage = detail.IncentivePcnt,
                    Value = detail.IncentiveValue,
                    Slno = detail.Slno
                };

                tempIncentiveSetupInfo.Add(obj);
                Session["tempIncentiveSetupInfo"] = tempIncentiveSetupInfo;
            }
        }

        protected void btnSave_OnClick_Click(object sender, EventArgs e)
        {
            try
            {
                #region validation

                if (rdropType.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Type");
                    rdropType.Focus();
                    return;
                }

                if (rdropType.SelectedItem.Text.ToLower() == "salesperson")
                {
                    if (rdropSalesPerson.SelectedIndex <= 0)
                    {
                        Alert.Show("Please select a designation");
                        rdropSalesPerson.Focus();
                        return;
                    }
                }
                if (rdropInsectiveOn.SelectedIndex <= 0)
                {

                    Alert.Show("Please select a Incentive type");
                    rdropInsectiveOn.Focus();
                    return;
                }
                if (rdropInsectiveOn.SelectedValue.ToLower() == "product")
                {
                    if (rdropApplyOn.SelectedIndex <= 0)
                    {
                        Alert.Show("Please select Apply on item");
                        rdropInsectiveOn.Focus();
                        return;
                    }
                    if (rdropProductName.SelectedIndex <= 0)
                    {
                        Alert.Show("Please select a product");
                        rdropProductName.Focus();
                        return;
                    }
                }

                #endregion

                IncentiveSetup objInsentiveSetup = new IncentiveSetup();
                objInsentiveSetup.Type = rdropType.SelectedValue;
                objInsentiveSetup.DesignationId = rdropSalesPerson.SelectedIndex <= 0 ? 0 : int.Parse(rdropSalesPerson.SelectedValue);
                objInsentiveSetup.SetupDate = DateTime.Now;
                objInsentiveSetup.UserId = _user.Id;
                objInsentiveSetup.IncentiveOn = rdropInsectiveOn.SelectedValue;
                objInsentiveSetup.IsActive = chkIsActive.Checked;
                objInsentiveSetup.ApplyOn = rdropApplyOn.SelectedValue.ToLower() == "selectone"
                    ? ""
                    : rdropApplyOn.SelectedValue;
                objInsentiveSetup.ProductId = rdropProductName.SelectedIndex <= 0
                    ? 0
                    : int.Parse(rdropProductName.SelectedValue);
                if (lblisNewEntry.Text == "true")
                {
                    success = objInsentiveSetup.InsertIncentiveSetup();
                    lblId.Text = objInsentiveSetup.GetIncentiveSetupMasterId().ToString();

                    lblisNewEntry.Text = "false";
                }
                else
                {
                    objInsentiveSetup.Id = int.Parse(lblId.Text);

                    success = objInsentiveSetup.UpdateIncentiveSetup();
                }


                if (success == 0)
                {
                    Alert.Show("Incentive Setup data is no save successfully");
                    return;
                }
                else
                {
                    if (tempIncentiveSetupInfo.Count != 0)
                    {
                        foreach (TempIncentiveSetupInfo tmpSetup in tempIncentiveSetupInfo)
                        {
                            IncentiveSetupDetails objIncentiveSetupDetails = new IncentiveSetupDetails();

                            objIncentiveSetupDetails.MasterId = int.Parse(lblId.Text);
                            objIncentiveSetupDetails.StartValue = tmpSetup.StartValue;
                            objIncentiveSetupDetails.EndValue = tmpSetup.EndValue;
                            objIncentiveSetupDetails.IncentivePcnt = tmpSetup.Percentage;
                            objIncentiveSetupDetails.IncentiveValue = tmpSetup.Value;
                            objIncentiveSetupDetails.Slno = tmpSetup.Slno;


                            if (tmpSetup.Id == 0)
                            {
                                success = objIncentiveSetupDetails.InsertIncentiveSetupDetails();
                            }
                            else
                            {
                                objIncentiveSetupDetails.Id = tmpSetup.Id;
                                success = objIncentiveSetupDetails.UpdateIncentiveSetupDetails();
                            }
                        }
                    }

                    Alert.Show("Data Save succesfully");
                    lblslno.Text = "";
                }

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save data." + ex);
            }

        }

        private void ClearAllInfo()
        {
            rdropSalesPerson.SelectedIndex = 0;
            rtxtEndValue.Text = "";
            rtxtPercentage.Text = "";
            rtxtStartValue.Text = "";
            rtxtValue.Text = "";
            lblisNewEntry.Text = "true";
            lblId.Text = "";
            lblslno.Text = "";
            tempIncentiveSetupInfo = new List<TempIncentiveSetupInfo>();

            Session["tempIncentiveSetupInfo"] = null;
        }

        protected void btnClear_OnClick_Click(object sender, EventArgs e)
        {
            this.ClearAllInfo();
        }

        protected void RadGridIncentiveSetupinfo_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }

        protected void RadGridIncentiveSetupinfo_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {

        }

        protected void rdropSalesPerson_DataBound(object sender, EventArgs e)
        {
            rdropSalesPerson.Items.Insert(0, new RadComboBoxItem());
        }


        protected void rdropType_DataBound(object sender, EventArgs e)
        {
            rdropType.Items.Insert(0, new RadComboBoxItem());
        }

        protected void btnAddIncentiveDetails_Click(object sender, EventArgs e)
        {
            try
            {
                Regex regexForQuentity = new Regex("^[0-9]*$");

                #region validation

                if (rtxtStartValue.Text == string.Empty || !regexForQuentity.IsMatch(rtxtStartValue.Text))
                {
                    Alert.Show("Please enter a valid Start Value");
                    rtxtStartValue.Focus();
                    return;
                }
                if (rtxtPercentage.Text == string.Empty || !regexForQuentity.IsMatch(rtxtPercentage.Text))
                {
                    Alert.Show("Please enter a valid Percentage");
                    rtxtPercentage.Focus();
                    return;
                }

                #endregion

                TempIncentiveSetupInfo objTempIncentiveSetupInfo = new TempIncentiveSetupInfo();

                objTempIncentiveSetupInfo.StartValue = int.Parse(rtxtStartValue.Text);
                objTempIncentiveSetupInfo.EndValue = rtxtEndValue.Text == string.Empty ? int.Parse(rtxtStartValue.Text) : int.Parse(rtxtEndValue.Text);
                objTempIncentiveSetupInfo.Percentage = (rtxtPercentage.Text == string.Empty)
                    ? 0
                    : int.Parse(rtxtPercentage.Text);

                objTempIncentiveSetupInfo.Value = (rtxtValue.Text == string.Empty) ? 0 : int.Parse(rtxtValue.Text);

                if (lblslno.Text == string.Empty)
                {
                    int mId = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);
                    lblslno.Text = new IncentiveSetupDetails().GetlastLastSlno(mId).ToString();
                }
                else
                {
                    int slNo = int.Parse(lblslno.Text) + 1;
                    lblslno.Text = slNo.ToString();
                }
                objTempIncentiveSetupInfo.Slno = int.Parse(lblslno.Text);


                tempIncentiveSetupInfo.Add(objTempIncentiveSetupInfo);

                Session["tempIncentiveSetupInfo"] = tempIncentiveSetupInfo;

                this.LoadIncentiveSetup();

                rtxtStartValue.Text = "";
                rtxtEndValue.Text = "";
                rtxtPercentage.Text = "";
                rtxtValue.Text = "";
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to add Incentive Setup." + ex);
            }

        }

        protected void RadGridIncentiveSetupinfo_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }

        protected void rdropSalesPerson_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (rdropSalesPerson.SelectedIndex > 0)
            {
                List<IncentiveSetupDetails> details = new IncentiveSetupDetails().GetIncentiveDetailsForDesignation(int.Parse(rdropSalesPerson.SelectedValue));
                tempIncentiveSetupInfo = new List<TempIncentiveSetupInfo>();



                foreach (IncentiveSetupDetails detail in details)
                {
                    lblId.Text = detail.MasterId.ToString();
                    TempIncentiveSetupInfo obj = new TempIncentiveSetupInfo()
                    {

                        Id = detail.Id,
                        StartValue = detail.StartValue,
                        EndValue = detail.EndValue,
                        Percentage = detail.IncentivePcnt,
                        Value = detail.IncentiveValue,
                        Slno = detail.Slno
                    };

                    tempIncentiveSetupInfo.Add(obj);
                }
                Session["tempIncentiveSetupInfo"] = tempIncentiveSetupInfo;

                this.LoadIncentiveSetup();
            }
            //else
            //{
            //    tempIncentiveSetupInfo = new List<TempIncentiveSetupInfo>();
            //    this.LoadIncentiveSetup();

            //}
        }

        protected void rdropInsectiveOn_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdropInsectiveOn.SelectedValue == "Product")
            {
                showApplyOn.Visible = true;
                this.LoadProductInfo();
            }
            else
            {
                showApplyOn.Visible = false;
                showQuantity.Visible = false;
                rtxtStartQuantity.Text = "";
                rtxtEndQuantity.Text = "";
                rdropApplyOn.SelectedIndex = 0;
                rdropProductName.SelectedIndex = 0;
            }
        }

        protected void rdropApplyOn_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdropApplyOn.SelectedValue == "Quantity")
            {
                showQuantity.Visible = true;
                showValue.Visible = false;
                rtxtStartValue.Text = "";
                rtxtEndValue.Text = "";

            }
            else
            {
                showQuantity.Visible = false;
                showValue.Visible = true;
                rtxtStartQuantity.Text = "";
                rtxtEndQuantity.Text = "";

            }
        }

        protected void rdropProductName_OnDataBound(object sender, EventArgs e)
        {
            rdropProductName.Items.Insert(0, new RadComboBoxItem());
        }
    }


}
