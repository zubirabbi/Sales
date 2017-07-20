using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hrms;
using SUL.Bll;
using SUL.Dal;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class DealerInfo : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        //private  bool isNewEntry;
        private Company _company;
        //private  int _source;

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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer Add") : new AppFunctionality().GetAppFunctionalityId("Dealer Add", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer Add") : new AppFunctionality().GetAppFunctionalityId("Dealer Add", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer Add") : new AppFunctionality().GetAppFunctionalityId("Dealer Add", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer Add") : new AppFunctionality().GetAppFunctionalityId("Dealer Add", int.Parse(lblsource.Text));
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

        private void LoadArea()
        {
            try
            {
                List<Area> lstAreas = new Area().GetAllArea();
                lstAreas.Insert(0, new Area());

                rdropArea.DataTextField = "AreaName";
                rdropArea.DataValueField = "Id";
                rdropArea.DataSource = lstAreas;
                rdropArea.DataBind();

                if (lstAreas.Count == 2)
                {
                    rdropArea.SelectedIndex = 1;
                    try
                    {
                        Area objArea = new Area().GetAreaById(int.Parse(rdropArea.SelectedValue));

                        int csId = objArea.ChanelSpecialities;
                        int Jrcs = objArea.JrChanelSpecialities;

                        EmployeeInformation objEmployeeInformationcs = new EmployeeInformation().GetEmployeeInformationById(csId, _company.Id);
                        EmployeeInformation objEmployeeInformationjrcs = new EmployeeInformation().GetEmployeeInformationById(Jrcs, _company.Id);

                        rtxtCS.Text = objEmployeeInformationcs.EmployeeName;
                        lblEmpId.Text = objEmployeeInformationcs.Id.ToString();

                        rtxtJrCS.Text = objEmployeeInformationjrcs.EmployeeName;
                        lblEmpJrId.Text = objEmployeeInformationjrcs.Id.ToString();

                        string dealerCode = new DealerInformation().GetlastDealerInfoCode(int.Parse(rdropArea.SelectedValue));
                        rtxtDealerCode.Text = dealerCode;
                    }
                    catch (Exception ex)
                    {
                        Alert.Show(ex.Message);
                    }

                }
                else
                    rdropArea.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load Area." + ex);
            }
        }

        private void LoadDealerCategory()
        {
            try
            {
                List<DealerCategory> lstDealerCategories = new DealerCategory().GetAllDealerCategory();
                lstDealerCategories.Insert(0, new DealerCategory());

                rdropDealerCate.DataTextField = "CategoryCode";
                rdropDealerCate.DataValueField = "Id";
                rdropDealerCate.DataSource = lstDealerCategories;
                rdropDealerCate.DataBind();

                rdropDealerCate.DataBind();

                if (lstDealerCategories.Count == 2)
                    rdropDealerCate.SelectedIndex = 1;
                else
                    rdropDealerCate.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load Delear Category." + ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblsource.Text = Request.QueryString["source"] != null ? Request.QueryString["source"] : "0";


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
                this.LoadArea();
                this.LoadDealerCategory();
                lblisNewEntry.Text = "true";

                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];

                    DealerInformation objDealerInformation = new DealerInformation().GetDealerInformationById(int.Parse(id));

                    lblId.Text = objDealerInformation.Id.ToString();
                    rtxtDealerCode.Text = objDealerInformation.DealerCode;
                    rtxtDealername.Text = objDealerInformation.DealerName;
                    rdropArea.SelectedValue = objDealerInformation.Area.ToString();
                    rtxtAddress.Text = objDealerInformation.Address;
                    lblEmpId.Text = objDealerInformation.CS.ToString();

                    EmployeeInformation objemoInfo = new EmployeeInformation().GetEmployeeInformationById(int.Parse(lblEmpId.Text), _company.Id);
                    rtxtCS.Text = objemoInfo.EmployeeName;

                    rtxtProprietorName.Text = objDealerInformation.ProprietorName;
                    rtxtPhone.Text = objDealerInformation.Phone.ToString();
                    rtxtMobile.Text = objDealerInformation.Mobile.ToString();
                    rtxtEmail.Text = objDealerInformation.Email;
                    rdropDealerCate.SelectedValue = objDealerInformation.DealerCategory.ToString();
                    rdtStartDate.SelectedDate = objDealerInformation.StartDate == null ? DateTime.Now : objDealerInformation.StartDate;
                    chkIsActive.Checked = objDealerInformation.IsActive;
                    rtxtCreditLimit.Text = objDealerInformation.CreditLimit.ToString();

                    lblisNewEntry.Text = "false";

                }


            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation

            if (rdropArea.SelectedIndex <= 0)
            {
                Alert.Show("Please select Area.");
                rdropArea.Focus();
                return;
            }
            if (rtxtDealerCode.Text == String.Empty)
            {
                Alert.Show("Please enter a Dealer code.");
                rtxtDealerCode.Focus();
                return;
            }
            if (rtxtDealername.Text == string.Empty)
            {
                Alert.Show("Please enter a Dealer Name.");
                rtxtDealername.Focus();
                return;
            }
            if (rtxtProprietorName.Text == string.Empty)
            {
                Alert.Show("Please enter a Proprietor Name.");
                rtxtProprietorName.Focus();
                return;
            }
            if (rdropDealerCate.SelectedIndex <= 0)
            {
                Alert.Show("Please select a Dealer Category.");
                rdropDealerCate.Focus();
                return;
            }
            if (rdtStartDate.SelectedDate == null)
            {
                Alert.Show("Please select a start date.");
                rdtStartDate.Focus();
                return;
            }

            int id = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);
            int codeExist = new DealerInformation().CheckForCodeExist(rtxtDealerCode.Text, bool.Parse(lblisNewEntry.Text), id);
            if (codeExist > 0)
            {
                Alert.Show("This Dealer code is Already Exist.");
                return;
            }
            #endregion
            try
            {
                DealerInformation objDealerInformation = new DealerInformation();
                objDealerInformation.DealerCode = rtxtDealerCode.Text;
                objDealerInformation.DealerName = rtxtDealername.Text;
                objDealerInformation.Area = int.Parse(rdropArea.SelectedValue);
                objDealerInformation.Address = rtxtAddress.Text.Trim();
                objDealerInformation.CS = lblEmpId.Text == string.Empty ? 0 : int.Parse(lblEmpId.Text);
                objDealerInformation.JrCS = lblEmpJrId.Text == string.Empty ? 0 : int.Parse(lblEmpJrId.Text);

                objDealerInformation.ProprietorName = rtxtProprietorName.Text;
                objDealerInformation.Phone = rtxtPhone.Text.Trim();
                objDealerInformation.Mobile = rtxtMobile.Text.Trim();
                objDealerInformation.Email = rtxtEmail.Text.Trim();
                objDealerInformation.DealerCategory = int.Parse(rdropDealerCate.SelectedValue);
                objDealerInformation.StartDate = rdtStartDate.SelectedDate == null ? DateTime.Now : DateTime.Parse(rdtStartDate.SelectedDate.ToString());
                objDealerInformation.IsActive = chkIsActive.Checked;
                objDealerInformation.TotalCredit = 0;
                objDealerInformation.TotalDebit = 0;
                objDealerInformation.CreditLimit = rtxtCreditLimit.Text == string.Empty ? 0 : decimal.Parse(rtxtCreditLimit.Text);

                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    string WareHouseCode = new WareHouse().GetlastwareHouseCode();
                    WareHouse objWareHouse = new WareHouse();
                    objWareHouse.Code = WareHouseCode;
                    objWareHouse.Name = rtxtDealerCode.Text + "/" + WareHouseCode;
                    objWareHouse.CompanyId = _company.Id;

                    objDealerInformation.WareHouseCode = rtxtDealerCode.Text + "/" + WareHouseCode;
                    success = objDealerInformation.InsertDealerInformation();

                    objWareHouse.Code = WareHouseCode;
                    objWareHouse.Name = rtxtDealerCode.Text + "/" + WareHouseCode;
                    objWareHouse.CompanyId = _company.Id;
                    objWareHouse.Incharge = "";
                    objWareHouse.IsActive = true;
                    objWareHouse.Location = "";

                    int succWarehouse = objWareHouse.InsertWareHouse();
                }
                else
                {
                    objDealerInformation.Id = int.Parse(lblId.Text);
                    objDealerInformation.WareHouseCode = lblWareHouseId.Text;
                    success = objDealerInformation.UpdateDealerInformation();
                }
                if (success == 0)
                {
                    Alert.Show("Data was not save succesfully");
                }
                else
                {
                    Alert.Show("Data save succesfully");
                    //this.LoadDealerGrid();
                    this.ClearAllInfo();
                   

                }

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to save Dealer Info." + ex);
            }
        }



        private void ClearAllInfo()
        {
            rtxtDealerCode.Text = "";
            rtxtDealername.Text = "";
            rdropArea.SelectedIndex = 0;
            rtxtAddress.Text = "";
            lblEmpId.Text = "";
            rtxtProprietorName.Text = "";
            rtxtPhone.Text = "";
            rtxtMobile.Text = "";
            rtxtEmail.Text = "";
            rdropDealerCate.SelectedIndex = 0;
            rdtStartDate.SelectedDate = null;
            chkIsActive.Checked = true;
            rtxtCreditLimit.Text = "";
            lblId.Text = "";
            lblisNewEntry.Text = "true";
            rtxtCS.Text = "";
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ClearAllInfo();
        }

        protected void rdropArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Area objArea = new Area().GetAreaById(int.Parse(rdropArea.SelectedValue));

                int csId = objArea.ChanelSpecialities;
                int Jrcs = objArea.JrChanelSpecialities;

                EmployeeInformation objEmployeeInformationcs = new EmployeeInformation().GetEmployeeInformationById(csId, _company.Id);
                EmployeeInformation objEmployeeInformationjrcs = new EmployeeInformation().GetEmployeeInformationById(Jrcs, _company.Id);

                rtxtCS.Text = objEmployeeInformationcs.EmployeeName;
                lblEmpId.Text = objEmployeeInformationcs.Id.ToString();

                rtxtJrCS.Text = objEmployeeInformationjrcs.EmployeeName;
                lblEmpJrId.Text = objEmployeeInformationjrcs.Id.ToString();

                string dealerCode = new DealerInformation().GetlastDealerInfoCode(int.Parse(rdropArea.SelectedValue));
                rtxtDealerCode.Text = dealerCode;
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }

        }
    }
}