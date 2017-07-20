using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class RequisitionCencel : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        //private bool isNewEntry;
        //private bool isnewPaymentEntry;
        private Company _company;

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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsView)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsView;
                return permission;
            }
            else
                return true;
        }

        private bool IsValidInsertForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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

        private bool IsValidDeleteForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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

        private void LoadDealerInfo()
        {
            try
            {
                DataTable dtDealerInfo;

                dtDealerInfo = new DealerInformation().GetAllDealerInformationView();


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



        private void LoadSetupChennalDesignation()
        {
            try
            {
                DealerInformation objDealerInformation =
                    new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));

                DataTable dtRmployeeInfo =
                    new EmployeeInformation().GetEmpListFromViewListbyempId(objDealerInformation.CS, _company.Id);

                rdropCS.DataTextField = "empAllInfo";
                rdropCS.DataValueField = "Id";
                rdropCS.DataSource = dtRmployeeInfo;
                rdropCS.DataBind();
                if (dtRmployeeInfo.Rows.Count == 1)
                {
                    rdropCS.SelectedIndex = 1;
                }
                else
                    rdropCS.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load data" + ex);
            }
        }

        private void LoadJrCS()
        {
            try
            {
                DealerInformation objDealerInformation =
                    new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));

                DataTable dtJrcs = new EmployeeInformation().GetEmpListFromViewListbyempId(objDealerInformation.JrCS,
                    _company.Id);

                rdropJrCS.DataTextField = "empAllInfo";
                rdropJrCS.DataValueField = "Id";
                rdropJrCS.DataSource = dtJrcs;
                rdropJrCS.DataBind();
                if (dtJrcs.Rows.Count == 1)
                    rdropJrCS.SelectedIndex = 1;
                else
                    rdropJrCS.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to Load date." + ex);
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
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                lblisNewEntry.Text = "true";
                lblisnewPaymentEntry.Text = "true";


                this.LoadDealerInfo();

                this.LoadSetupChennalDesignation();
                this.LoadJrCS();
                string rqCode = new RequisitionMaster().GetlastRequisitionCode();

                rdtRequisitionDate.SelectedDate = DateTime.Today;

                rtxtRequisitionNo.Text = rqCode;

                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];

                    RequisitionMaster objRequisitionMaster =
                        new RequisitionMaster().GetRequisitionMasterById(int.Parse(id));
                    Payment objPayment=new Payment().GetPaymentByRequisitionId(int.Parse(id));
                    lblId.Text = objRequisitionMaster.Id.ToString();
                    rdropPDealer.SelectedValue = objRequisitionMaster.DealerId.ToString();
                    this.LoadSetupChennalDesignation();
                    this.LoadJrCS();
                    rdropCS.SelectedValue = objRequisitionMaster.CSId.ToString();
                    rtxtAddress.Text = objRequisitionMaster.Address;
                    rtxtRequisitionNo.Text = objRequisitionMaster.RequisitionCode;
                    rdtRequisitionDate.SelectedDate = objRequisitionMaster.RequisitionDate;
                    lblStatus.Text = objRequisitionMaster.Status;
                    rdropJrCS.SelectedValue = objRequisitionMaster.JrCSId.ToString();
                    rtxtRequisitionTotal.Text = objRequisitionMaster.RequistionTotal.ToString();
                    rtxtPayment.Text = objPayment.Amount.ToString();

                }
            }
        }

        protected void rdropArea_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            this.LoadDealerInfo();
        }

        protected void rdropPDealer_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DealerInformation objdealerInfo = new DealerInformation().GetDealerInformationById(int.Parse(rdropPDealer.SelectedValue));
            rtxtAddress.Text = objdealerInfo.Address;
            this.LoadSetupChennalDesignation();
            this.LoadJrCS();
        }

        protected void rdropCS_OnDataBound(object sender, EventArgs e)
        {
            rdropCS.Items.Insert(0, new RadComboBoxItem());
        }
        protected void rdropJrCS_OnDataBound(object sender, EventArgs e)
        {
            rdropJrCS.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropPDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropPDealer.Items.Insert(0, new RadComboBoxItem());
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            try
            {
                RequisitionMaster objRequisitionMaster=new RequisitionMaster();
                objRequisitionMaster.Id = int.Parse(lblId.Text);
                objRequisitionMaster.Status = "Rejected";
                objRequisitionMaster.CencelDate = DateTime.Now;
                objRequisitionMaster.CencelBy = _user.Id;
                objRequisitionMaster.CencelNote = rtxtNote.Value;

                int success;
                success = objRequisitionMaster.UpdateRequisitionMasterByCancel();
                if (success == 0)
                {
                    Alert.Show("Data is not save succesfully");
                }
                else
                {
                    Response.Redirect("RequisitionInfoList.aspx");
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
    }
}