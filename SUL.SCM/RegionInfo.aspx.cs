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
    public partial class RegionInfo : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  Company _company;
       // private  bool isNewEntry;


        private static int companyId;
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home");
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

        private void LoadSetupChennalDesignation()
        {
            try
            {
                SetupChannel objsetupChannel = new SetupChannel().GetAllSetupChannelByChannels("Channel Manager");
                int desigId = objsetupChannel.DesignationId;

                List<EmployeeInformation> lstEmployeeInformations =
                    new EmployeeInformation().GetAllEmployeeInformationByDesignationId(desigId);

                lstEmployeeInformations.Insert(0, new EmployeeInformation());

                rdropCM.DataTextField = "EmployeeName";
                rdropCM.DataValueField = "Id";
                rdropCM.DataSource = lstEmployeeInformations;
                rdropCM.DataBind();
                if (lstEmployeeInformations.Count == 2)
                    rdropCM.SelectedIndex = 1;
                else
                    rdropCM.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load data" + ex);
            }
        }

        private void LoadSetupAssistantChanelManager()
        {
            try
            {
                SetupChannel objsetupChannel = new SetupChannel().GetAllSetupChannelByChannels("Assistant Channel Manager");
                int desigId = objsetupChannel.DesignationId;

                List<EmployeeInformation> lstEmployeeInformations =
                    new EmployeeInformation().GetAllEmployeeInformationByDesignationId(desigId);

                lstEmployeeInformations.Insert(0, new EmployeeInformation());

                rdropACM.DataTextField = "EmployeeName";
                rdropACM.DataValueField = "Id";
                rdropACM.DataSource = lstEmployeeInformations;
                rdropACM.DataBind();
                if (lstEmployeeInformations.Count == 2)
                    rdropACM.SelectedIndex = 1;
                else
                    rdropACM.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load data" + ex);
            }
        }

        private void LoadRegionGrid()
        {
            try
            {
                DataTable dtRegion = new Region().GetAllViewRegion();

                RadGridRegion.DataSource = dtRegion;
                RadGridRegion.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridRegion.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load region data" + ex);
            }
        }

        private void ClearRegion()
        {
            rtxtDesc.Text = "";
            rtxtRegionCode.Text = "";
            rtxtRegionName.Text = "";
            rdropCM.SelectedIndex = 0;
            chkIsActive.Checked = true;
            lblisNewEntry.Text = "true";
        }
        private void ExecuteEmpCode()
        {
            string RegionCode = new Region().GetlastEmpCode();
            rtxtRegionCode.ReadOnly = true;
            rtxtRegionCode.Text = RegionCode;
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
                lblisNewEntry.Text = "true";

                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                this.ExecuteEmpCode();
                this.LoadSetupChennalDesignation();
                this.LoadSetupAssistantChanelManager();
                this.LoadRegionGrid();
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                #region validation

                if (rtxtRegionName.Text == string.Empty)
                {
                    Alert.Show("Please enter region name");
                    rtxtRegionName.Focus();
                    return;

                }
                if (rtxtRegionCode.Text == string.Empty)
                {
                    Alert.Show("please enter region code.");
                    rtxtRegionCode.Focus();
                    return;
                }
                //if (rdropCM.SelectedIndex <= 0 && rdropACM.SelectedIndex <= 0)
                //{
                //    Alert.Show("please select an employee");
                //    rdropCM.Focus();
                //    return;
                //}
                int id = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);
                int codeExist = new Region().CheckForCodeExist(rtxtRegionCode.Text, bool.Parse(lblisNewEntry.Text), id);
                if (codeExist > 0)
                {
                    Alert.Show("This Region code is Already Exist.");
                    return;
                }
                #endregion

                Region objRegion = new Region();

                objRegion.RegionName = rtxtRegionName.Text;
                objRegion.RegionCode = rtxtRegionCode.Text;
                objRegion.ChanelManager = rdropCM.SelectedIndex <= 0 ? 0 : int.Parse(rdropCM.SelectedValue);
                objRegion.AssistantChanelManager = rdropACM.SelectedIndex <= 0 ? 0 : int.Parse(rdropACM.SelectedValue);
                objRegion.Description = rtxtDesc.Text == string.Empty ? "" : rtxtDesc.Text;
                objRegion.IsActive = chkIsActive.Checked;

                int success;

                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objRegion.InsertRegion();
                }
                else
                {
                    objRegion.Id = int.Parse(lblId.Text);
                    success = objRegion.UpdateRegion();
                }
                if (success == 0)
                {
                    Alert.Show("Data was not save succesfully");
                }
                else
                {
                    //this.LoadRegionGrid();
                    //this.ClearRegion();
                    //this.ExecuteEmpCode();
                    Response.Redirect("RegionInfo.aspx");
                }

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to save Region data");
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearRegion();
        }

        protected void RadGridRegion_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;



            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                Region objRegion = new Region();

                int id = int.Parse(item["colId"].Text);
                delete = objRegion.DeleteRegionById(id);

                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    this.LoadRegionGrid();
                    Response.Redirect("RegionInfo.aspx");
                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;
                    rtxtRegionName.Text = item["colRegionName"].Text;
                    rtxtRegionCode.Text = item["colRegionCode"].Text;
                    rdropCM.SelectedValue = item["colChanelManager"].Text == string.Empty || item["colChanelManager"].Text == "&nbsp" ? "0" : item["colChanelManager"].Text;
                    rdropACM.SelectedValue = item["colAssChanelManager"].Text == string.Empty || item["colAssChanelManager"].Text == "&nbsp" ? "0" : item["colAssChanelManager"].Text;
                    rtxtDesc.Text = item["colDescription"].Text == "&nbsp;" ? "" : item["colDescription"].Text;
                    chkIsActive.Checked = bool.Parse(item["colIsActive"].Text);


                    lblisNewEntry.Text = "false";

                }
                catch (Exception ex)
                {
                    Alert.Show("Something is going wrong to select data." + ex);
                }
            }
            if (e.CommandName.Equals(RadGrid.FilterCommandName))
            {
                this.LoadRegionGrid();
            }
        }

        protected void RadGridRegion_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadRegionGrid();
        }

        protected void RadGridRegion_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadRegionGrid();
        }

    }
}