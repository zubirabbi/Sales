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

namespace SUL.SCM
{
    public partial class ServiceCenterInfo : System.Web.UI.Page
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

            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Service Center") : new AppFunctionality().GetAppFunctionalityId("Service Center", int.Parse(lblsource.Text));
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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
                    rdropArea.SelectedIndex = 1;

                else
                    rdropArea.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Area" + ex);
            }
        }
        private void LoadInCharge()
        {
            try
            {
                DataTable dtInCharge = new EmployeeInformation().GetEmpListFromViewList(_company.Id);
                rdropInCharge.DataTextField = "empAllinfo";
                rdropInCharge.DataValueField = "Id";
                rdropInCharge.DataSource = dtInCharge;
                rdropInCharge.DataBind();
                if (dtInCharge.Rows.Count == 2)
                    rdropInCharge.SelectedIndex = 1;
                else
                    rdropInCharge.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load InCharge" + ex);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblsource.Text = Request.QueryString["source"] ?? "0";
            if (!IsValidSession())
            {
                string str = Request.QueryString.ToString();
                if (str == string.Empty)
                    Response.Redirect("UserLogin.aspx?refPage=home.aspx");
                else
                    Response.Redirect("UserLogin.aspx?refPage=home.aspx?" + str);
            }
            if (!IsValidPageForUser())
            {
                Alert.Show("Sorry,You don't have permission to access this page.");
                Response.Redirect("UserLogin.aspx?refPage=home.aspx", false);
            }
            if (!IsPostBack)
            {
                lblisNewEntry.Text = "true";
                ClearAllInfo();
                this.LoadArea();
                this.LoadInCharge();
                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];
                    lblId.Text = id;

                    ServiceCenter objServiceCenter=new ServiceCenter().GetServiceCenterById(int.Parse(id));

                    rtxtAddress.Text = objServiceCenter.SCAddress;
                    rtxtCentreCode.Text = objServiceCenter.SCCode;
                    rtxtName.Text = objServiceCenter.SCName;

                    rdropArea.SelectedValue = objServiceCenter.AreaId.ToString();
                    rdropInCharge.SelectedValue = objServiceCenter.InChargeId.ToString();
                    rdtEstablishDate.SelectedDate = objServiceCenter.EstablishDate;

                    lblisNewEntry.Text = "false";
                }
            }

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                #region Validation
                if (rtxtCentreCode.Text == string.Empty)
                {
                    Alert.Show("Please enter Centre Code");
                    rtxtCentreCode.Focus();
                    return;
                }
                if (rtxtName.Text == string.Empty)
                {
                    Alert.Show("Please enter SC Name");
                    rtxtName.Focus();
                    return;
                }
                if (rtxtAddress.Text == string.Empty)
                {
                    Alert.Show("Please enter SC Address");
                    rtxtAddress.Focus();
                    return;
                }
                if (rdropArea.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Area");
                    rdropArea.Focus();
                    return;
                }
                if (rdropInCharge.SelectedIndex <= 0)
                {
                    Alert.Show("Please enter a Incharge");
                    rdropInCharge.Focus();
                    return;
                }
                if (rdtEstablishDate.SelectedDate == null)
                {
                    Alert.Show("Please enter Establish Date");
                    rdtEstablishDate.Focus();
                    return;
                }
                #endregion


                ServiceCenter objServiceCentre = new ServiceCenter();
                objServiceCentre.SCCode = rtxtCentreCode.Text;
                objServiceCentre.AreaId = int.Parse(rdropArea.SelectedValue);
                objServiceCentre.SCName = rtxtName.Text;
                objServiceCentre.SCAddress = rtxtAddress.Text;
                objServiceCentre.InChargeId = int.Parse(rdropInCharge.SelectedValue);
                DateTime Edate = DateTime.Parse(rdtEstablishDate.SelectedDate.ToString());
                objServiceCentre.EstablishDate = Edate;
                objServiceCentre.IsActive = chkIsActive.Checked;

                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objServiceCentre.InsertServiceCenter();
                }
                else
                {
                    if (lblId.Text == string.Empty || lblId.Text == "0")
                    {
                        success = objServiceCentre.InsertServiceCenter();
                    }
                    else
                    {
                        objServiceCentre.Id = int.Parse(lblId.Text);
                        success = objServiceCentre.UpdateServiceCenter();
                    }
                }
                if (success == 0)
                {
                    Alert.Show("ServiceCentre Data was not save successfully");
                }
                else
                {
                    this.ClearAllInfo();
                    Alert.Show("ServiceCentre Data save successfully");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to save service center data." + ex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllInfo();
        }

        private void ClearAllInfo()
        {
            rtxtCentreCode.Text = "";
            rtxtAddress.Text = "";
            rtxtName.Text = "";
            rdropInCharge.SelectedIndex = 0;
            rdropArea.SelectedIndex = 0;
            rdtEstablishDate.SelectedDate = null;

            lblisNewEntry.Text = "true";
            lblId.Text = "";
        }

        protected void rdropInCharge_DataBound(object sender, EventArgs e)
        {
            rdropInCharge.Items.Insert(0, new RadComboBoxItem());
        }
    }
}