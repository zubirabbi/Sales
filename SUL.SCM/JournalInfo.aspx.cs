using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hrms;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class JournalInfo : System.Web.UI.Page
    {
        private UserRole _role;
        private Users _user;
        private Company _company;
        private List<TempJournalDetails> tempJournalDetailses;
        private string _department;
        private int _source;
        private AppPermission PermissionUser;

        private class TempJournalDetails
        {
            public int Id { get; set; }
            public int DealerId { get; set; }
            public string DealerCode { get; set; }
            public string DealerName { get; set; }
            public decimal Balance { get; set; }
            public decimal Debit { get; set; }
            public decimal Credit { get; set; }
            public string Description { get; set; }


        }

        private void LoadDealerInfo()
        {
            try
            {
                DataTable dtDealerInfo = new DealerInformation().GetAllDealerInformationView();
                rdropDealer.DataTextField = "DealerInfo";
                rdropDealer.DataValueField = "Id";
                rdropDealer.DataSource = dtDealerInfo;
                rdropDealer.DataBind();

                rdropDealer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
            }
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
            //int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Data Export");
            int FunctionalId = lblsource.Text == string.Empty ? new AppFunctionality().GetAppFunctionalityId("Dealer Journal Info") : new AppFunctionality().GetAppFunctionalityId("Dealer Journal Info", int.Parse(lblsource.Text));
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
        private void LoadJournalType()
        {
            try
            {
                List<ListTable> lstTable = new ListTable().GetAllListTableByType("JournalType");

                lstTable.Insert(0, new ListTable());

                rdropType.DataTextField = "ListValue";
                rdropType.DataValueField = "ListId";

                rdropType.DataSource = lstTable;
                rdropType.DataBind();
                if (lstTable.Count > 1)
                    rdropType.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Payment Type DropdownList" + ex);
            }
        }
        private void LoadDealerJournalDetails()
        {
            try
            {
                if (tempJournalDetailses.Count == 0)
                {
                    RadGridJournalDetails.DataSource = new string[] { };
                    return;
                }

                RadGridJournalDetails.DataSource = tempJournalDetailses;
                RadGridJournalDetails.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load requisition details grid." + ex);
            }
        }
        private void LoadRequisitionDataFromDatabase(int id)
        {
            try
            {
                if (bool.Parse(lblisNewEntry.Text) == false)
                {
                    List<DealerJournalDetails> lstDealerJournalDetailses =
                       new DealerJournalDetails().GetAllDealerJournalDetailsBymasterId(id);
                    List<DealerInformation> lstListTables = new DealerInformation().GetAllDealerInformation();
                    if (lstDealerJournalDetailses.Count > 0)
                    {
                        tempJournalDetailses = new List<TempJournalDetails>();
                        foreach (DealerJournalDetails lstJournalDetails in lstDealerJournalDetailses)
                        {
                            TempJournalDetails tmpTempJournalDetails = new TempJournalDetails();

                            tmpTempJournalDetails.Id = int.Parse(lstJournalDetails.Id.ToString());
                            lblDetailsId.Text = lstJournalDetails.Id.ToString();
                            DealerInformation objDealerInformation = new DealerInformation().GetDealerInformationById(lstJournalDetails.DealerId);
                            tmpTempJournalDetails.DealerId = int.Parse(objDealerInformation.Id.ToString());
                            tmpTempJournalDetails.DealerName = objDealerInformation.DealerName;
                            tmpTempJournalDetails.DealerCode = objDealerInformation.DealerCode;
                            tmpTempJournalDetails.Balance = lstJournalDetails.CurrentBalance;
                            tmpTempJournalDetails.Debit = lstJournalDetails.Debit;
                            tmpTempJournalDetails.Credit = lstJournalDetails.Credit;
                            tmpTempJournalDetails.Description = lstJournalDetails.Description;
                            tempJournalDetailses.Add(tmpTempJournalDetails);
                        }
                        Session["tempJournalDetailses"] = tempJournalDetailses;

                        this.LoadDealerJournalDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblsource.Text = Request.QueryString["source"] ?? "";
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

            if (Session["tempJournalDetailses"] != null)
                tempJournalDetailses = (List<TempJournalDetails>)Session["tempJournalDetailses"];
            else
                tempJournalDetailses = new List<TempJournalDetails>();


            if (!IsPostBack)
            {
                lblisNewEntry.Text = "true";
                this.LoadDealerInfo();
                this.LoadJournalType();
                this.ClearInfo();
                tempJournalDetailses=new List<TempJournalDetails>();
                if (Request.QueryString["Id"] != null)
                {
                    string id = Request.QueryString["Id"];
                    LoadJournalData(id);
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
                if (bool.Parse(lblisNewEntry.Text))
                {
                    if (!PermissionUser.IsInsert)
                    {
                        Alert.Show("Sorry, You Don't Have permission to access this page.");
                        Response.Redirect("ErrorPage.aspx", false);
                    }
                }
            }
        }

        private void LoadJournalData(string id)
        {
            try
            {
                DealerJournalMaster objJournalMaster = new DealerJournalMaster().GetDealerJournalMasterById(int.Parse(id));

                lblId.Text = objJournalMaster.Id.ToString();
                rtxtJournalId.Text = objJournalMaster.JournalId;
                rtxtDescription.Value = objJournalMaster.Description;
                rtxtContraAccount.Text = objJournalMaster.ContraAccount;
                rdropType.SelectedValue = objJournalMaster.Type.ToString();
                rdtDate.SelectedDate = objJournalMaster.Date;
                lblisNewEntry.Text = "false";
                this.LoadRequisitionDataFromDatabase(int.Parse(id));

                if (objJournalMaster.Status == "Approved")
                {
                    btnSave.Visible = false;
                    btnAddJournalInfo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);

            }
        }

        protected void RadGridJournalDetails_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnSelect")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblDetailsId.Text = item["colId"].Text;
                    rdropDealer.SelectedValue = item["colDealerId"].Text;
                    rtxtCurrentBalance.Text = item["colBalance"].Text;
                    rtxtDebit.Text = item["colDebit"].Text;
                    rtxtCredit.Text = item["colCredit"].Text;
                    rtxtDescription2.Value = item["colDescription"].Text;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }

        }

        protected void btnAddJournalInfo_OnClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                Regex regexForPriceUnite = new Regex(@"^[0-9]\d*(\.\d+)?$");

                #region validation

                if (rdropDealer.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Dealer");
                    rdropDealer.Focus();
                    return;
                }
                if (rtxtCredit.Text == string.Empty && rtxtDebit.Text == string.Empty)
                {
                    Alert.Show("Plsease enter Debit or Credit");
                    rtxtCredit.Focus();
                    return;

                }
                if (rtxtCredit.Text != string.Empty)
                {
                    if (!regexForPriceUnite.IsMatch(rtxtCredit.Text))
                    {
                        Alert.Show("Please enter a valid credit");
                        rtxtCredit.Focus();
                        return;
                    }
                }
                if (rtxtDebit.Text != string.Empty)
                {
                    if (!regexForPriceUnite.IsMatch(rtxtDebit.Text))
                    {
                        Alert.Show("Please enter a valid Debit");
                        rtxtDebit.Focus();
                        return;
                    }
                }

                #endregion

                TempJournalDetails objTempJournalDetails =
                    tempJournalDetailses.Find(x => x.DealerId == int.Parse(rdropDealer.SelectedValue));
                if (objTempJournalDetails != null)
                {
                    tempJournalDetailses.Remove(objTempJournalDetails);
                }
                else
                    objTempJournalDetails = new TempJournalDetails();

                objTempJournalDetails.DealerId = int.Parse(rdropDealer.SelectedValue);
                DealerInformation objDealerInformation = new DealerInformation().GetDealerInformationById(int.Parse(rdropDealer.SelectedValue));
                objTempJournalDetails.DealerName = objDealerInformation.DealerName;
                objTempJournalDetails.DealerCode = objDealerInformation.DealerCode;
                objTempJournalDetails.Debit = rtxtDebit.Text == string.Empty ? 0 : decimal.Parse(rtxtDebit.Text);
                objTempJournalDetails.Credit = rtxtCredit.Text == string.Empty ? 0 : decimal.Parse(rtxtCredit.Text);
                objTempJournalDetails.Balance = rtxtCurrentBalance.Text == string.Empty ? 0 : decimal.Parse(rtxtCurrentBalance.Text);
                objTempJournalDetails.Description = rtxtDescription2.Value;
                if (tempJournalDetailses.Count == 0)
                    tempJournalDetailses = new List<TempJournalDetails>();
                tempJournalDetailses.Add(objTempJournalDetails);
                Session["tempJournalDetailses"] = tempJournalDetailses;
                this.LoadDealerJournalDetails();
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void rdropDealer_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DealerInformation objDealerInformation = new DealerInformation().GetDealerInformationById(int.Parse(rdropDealer.SelectedValue));
            rtxtCurrentBalance.Text = objDealerInformation.Balance.ToString();
            rtxtCredit.Text = "";
            rtxtDebit.Text = "";

        }

        protected void rdropDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropDealer.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropType_OnDataBound(object sender, EventArgs e)
        {
            rdropType.Items.Insert(0, new RadComboBoxItem());
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                #region validation

                //if (rtxtJournalId.Text == string.Empty)
                //{
                //    Alert.Show("Please enter a Journal Id");
                //    rtxtJournalId.Focus();
                //    return;
                //}
                if (rdtDate.SelectedDate == null)
                {
                    Alert.Show("Please select a date");
                    rdtDate.Focus();
                    return;
                }
                if (rdropType.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a JournalType");
                    rdropType.Focus();
                    return;
                }
               
                if (rtxtContraAccount.Text == string.Empty)
                {
                    Alert.Show("Please enter a Contra Account");
                    rtxtContraAccount.Focus();
                    return;
                }

                #endregion

                int success;
                DealerJournalMaster objDealerJournalMaster = new DealerJournalMaster();

                objDealerJournalMaster.JournalId = rtxtJournalId.Text;
                objDealerJournalMaster.Date = DateTime.Parse(rdtDate.SelectedDate.ToString());
                objDealerJournalMaster.Type = int.Parse(rdropType.SelectedValue);
                
                objDealerJournalMaster.Description = rtxtDescription.Value;
                objDealerJournalMaster.ContraAccount = rtxtContraAccount.Text;
                objDealerJournalMaster.CreatedBy = _user.Id;
                objDealerJournalMaster.CreatedDate = DateTime.Now;
                objDealerJournalMaster.UpdateBy = 0;
                objDealerJournalMaster.UpdateDate = PublicVariables.minDate;
                objDealerJournalMaster.ApproveBy = 0;
                objDealerJournalMaster.ApproveDate = PublicVariables.minDate;
                objDealerJournalMaster.Status = "Created";
                if (bool.Parse(lblisNewEntry.Text))
                {
                    rtxtJournalId.Text = new DealerJournalMaster().GetlastDealerJournalCode();
                    objDealerJournalMaster.JournalId = rtxtJournalId.Text;
                    success = objDealerJournalMaster.InsertDealerJournalMaster();
                    lblId.Text = new DealerJournalMaster().GetMaxDealerJournalnMasterId().ToString();
                }
                else
                {
                    objDealerJournalMaster.Id = int.Parse(lblId.Text);
                    objDealerJournalMaster.UpdateBy = _user.Id;
                    objDealerJournalMaster.UpdateDate = DateTime.Now;
                    
                    success = objDealerJournalMaster.UpdateDealerJournalMaster();
                }
                if (success == 0)
                {
                    Alert.Show("Dealer Journal dat ais not save succesfully");
                    return;
                }
                else
                {
                    if (tempJournalDetailses.Count != 0)
                    {
                        int update;
                        int masterid = 0;
                        foreach (TempJournalDetails tmpDetails in tempJournalDetailses)
                        {
                            DealerJournalDetails objJournalDetails = new DealerJournalDetails();

                            masterid = int.Parse(lblId.Text);
                            this.LoadRequisitionDataFromDatabase(int.Parse(lblId.Text));

                            lblDetailsId.Text = tmpDetails.Id.ToString();
                            objJournalDetails.MasterId = masterid;
                            objJournalDetails.DealerId = tmpDetails.DealerId;
                            objJournalDetails.Debit = tmpDetails.Debit;
                            objJournalDetails.Credit = tmpDetails.Credit;
                            objJournalDetails.CurrentBalance = tmpDetails.Balance;
                            objJournalDetails.Description = tmpDetails.Description == null ? "" : tmpDetails.Description;

                            List<DealerJournalDetails> lstJournalDetailses =
                                new DealerJournalDetails().GetAllBymasterIdDealerId(tmpDetails.DealerId,int.Parse(lblId.Text));

                            if (lstJournalDetailses.Count == 0)
                            {

                                success = objJournalDetails.InsertDealerJournalDetails();
                            }
                            else
                            {
                                objJournalDetails.Id = int.Parse(lblDetailsId.Text);
                                success = objJournalDetails.UpdateDealerJournalDetails();

                            }
                            if (success == 0)
                            {
                                Alert.Show(
                                    "Dealer Journal Master data save successfully but Details data not save succesfully");

                            }
                            else
                            {
                                Alert.Show("data save succesfully");
                               
                            }
                        }
                        if (success != 0)
                        {
                            this.ClearInfo();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load data." + ex);
            }
        }

        private void ClearInfo()
        {
            rtxtContraAccount.Text = "";
            rtxtCredit.Text = "";
            rtxtCurrentBalance.Text = "";
            rtxtDebit.Text = "";
            rtxtDescription.Value = "";
            rtxtDescription2.Value = "";
            rtxtJournalId.Text = "";
            rdropDealer.SelectedIndex = 0;
            rdropType.SelectedIndex = 0;
            rdtDate.SelectedDate = null;
            tempJournalDetailses = new List<TempJournalDetails>();
            Session["tempJournalDetailses"] = null;
            RadGridJournalDetails.DataSource = new string[] {};
            RadGridJournalDetails.DataBind();
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("JournalInfo.aspx");
        }
    }
}