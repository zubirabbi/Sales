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
    public partial class AreaInfo : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  Company _company;
        //private  bool isNewEntry;


        private  int companyId;
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Area");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Area");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Area");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Area");
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
                SetupChannel objsetupChannel = new SetupChannel().GetAllSetupChannelByChannels("Channel Specialized");
                int desigId = objsetupChannel.DesignationId;

                DataTable dtRmployeeInfo =
                    new EmployeeInformation().GetAllEmployeeInformationByDesignationIdbyTable(desigId);

                rdropCS.DataTextField = "empAllInfo";
                rdropCS.DataValueField = "Id";
                rdropCS.DataSource = dtRmployeeInfo;
                rdropCS.DataBind();
                if (dtRmployeeInfo.Rows.Count == 2)
                    rdropCS.SelectedIndex = 1;
                else
                    rdropCS.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load data" + ex);
            }
        }

        private void LoadSetupJrChennalDesignation()
        {
            try
            {
                SetupChannel objsetupChannel = new SetupChannel().GetAllSetupChannelByChannels("Jr. Channel Specialized");
                int desigId = objsetupChannel.DesignationId;

                DataTable dtRmployeeInfo =
                    new EmployeeInformation().GetAllEmployeeInformationByDesignationIdbyTable(desigId);

                rdropJrCS.DataTextField = "empAllInfo";
                rdropJrCS.DataValueField = "Id";
                rdropJrCS.DataSource = dtRmployeeInfo;
                //if (dtRmployeeInfo.Rows.Count == 2)
                //    rdropJrCS.SelectedIndex = 1;
                //else
                //    rdropJrCS.SelectedIndex = 0;
                rdropJrCS.DataBind();
               
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load data" + ex);
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
                if (lstRegions.Rows.Count == 2)
                {
                    rdropRegion.SelectedIndex = 1;
                    this.ExecuteAreaCode();
                }
                else
                    rdropRegion.SelectedIndex = 0;
            }

            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load region." + ex);
            }
        }

        private void LoadAreaGrid()
        {
            try
            {
                DataTable dtAreaTable = new Area().GetAllViewArea();

                RadGridArea.DataSource = dtAreaTable;
                RadGridArea.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridArea.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load region data" + ex);
            }
        }
        private void ExecuteAreaCode()
        {
            string areaCode = new Area().GetlastAreaCode(int.Parse(rdropRegion.SelectedValue));
            rtxtAreaCode.ReadOnly = true;
            rtxtAreaCode.Text = areaCode;
        }
        private void ClearArea()
        {
            rtxtDesc.Text = "";
            rtxtAreaName.Text = "";
            rdropRegion.SelectedIndex = 0;
            rdropCS.SelectedIndex = 0;
            chkIsActive.Checked = true;
            lblisNewEntry.Text = "true";
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
                this.LoadSetupChennalDesignation();
                this.LoadAreaGrid();
                this.LoadSetupJrChennalDesignation();
                this.LoadRegion();
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                #region validation

                if (rtxtAreaCode.Text == string.Empty)
                {
                    Alert.Show("Please enter a Area code");
                    rtxtAreaCode.Focus();
                    return;
                }
                if (rtxtAreaName.Text == string.Empty)
                {
                    Alert.Show("Please enter a Area name");
                    rtxtAreaName.Focus();
                    return;
                }
                if (rdropRegion.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Reagion!!");
                    rdropRegion.Focus();
                    return;
                }
                //if (rdropCS.SelectedIndex <= 0 && rdropJrCS.SelectedIndex <= 0)
                //{
                //    Alert.Show("Please select a Channel Specialized");
                //    rdropCS.Focus();
                //    return;
                //}

                int id = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);
                int codeExist = new Area().CheckForCodeExist(rtxtAreaCode.Text, bool.Parse(lblisNewEntry.Text), id);
                if (codeExist > 0)
                {
                    Alert.Show("This Area code is Already Exist.");
                    return;
                }

                #endregion

                Area objArea = new Area();

                objArea.AreaName = rtxtAreaName.Text;
                objArea.AreaCode = rtxtAreaCode.Text;
                objArea.Description = rtxtDesc.Text;
                objArea.RegionId = int.Parse(rdropRegion.SelectedValue);
                objArea.ChanelSpecialities = rdropCS.SelectedIndex <= 0 ? 0 : int.Parse(rdropCS.SelectedValue);
                objArea.JrChanelSpecialities = rdropJrCS.SelectedIndex <= 0 ? 0 : int.Parse(rdropJrCS.SelectedValue);
                objArea.IsActive = chkIsActive.Checked;

                int success;

                if (bool.Parse(lblisNewEntry.Text))
                {

                    success = objArea.InsertArea();

                }
                else
                {
                    objArea.Id = int.Parse(lblId.Text);
                    success = objArea.UpdateArea();
                    int successUpdate = new DealerInformation().UpdateDealerCSJCS(objArea.Id, objArea.ChanelSpecialities,
                        objArea.JrChanelSpecialities);
                }
                if (success == 0)
                {
                    Alert.Show("Data was not save Succesfully");
                }
                else
                {
                    //this.LoadAreaGrid();
                    //this.ExecuteAreaCode();
                    //this.ClearArea();
                    RadWindowMessage.RadAlert("Data save succesfully", 330, 180,"Alert Message","");
                    Response.Redirect("AreaInfo.aspx");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save area data." + ex);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearArea();
            this.ExecuteAreaCode();
        }

        protected void RadGridArea_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;



            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                Area objArea = new Area();

                int id = int.Parse(item["colId"].Text);
                delete = objArea.DeleteAreaById(id);

                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    //this.LoadAreaGrid();
                    Response.Redirect("AreaInfo.aspx");

                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {

                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;
                    rtxtAreaName.Text = item["colAreaName"].Text;
                    rtxtAreaCode.Text = item["colAreaCode"].Text;
                    rdropRegion.SelectedValue = item["colRegionId"].Text == string.Empty
                        ? "0"
                        : item["colRegionId"].Text;
                    if (item["colChanelSpecialities"].Text == "0")
                    {
                        rdropCS.SelectedIndex = 0;
                    }
                    else
                    {
                        rdropCS.SelectedValue = item["colChanelSpecialities"].Text;    
                    }
                    if (item["colJrChanelSpecialities"].Text == "0")
                    {
                        rdropJrCS.SelectedIndex = 0;
                    }
                    else
                    {
                        rdropJrCS.SelectedValue = item["colJrChanelSpecialities"].Text;
                    }

                    rtxtDesc.Text = item["colDescription"].Text;
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
                this.LoadAreaGrid();
            }
        }

        protected void RadGridArea_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadAreaGrid();
        }

        protected void RadGridArea_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadAreaGrid();
        }


        protected void rdropRegion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.ExecuteAreaCode();
        }

        protected void rdropRegion_OnDataBound(object sender, EventArgs e)
        {
            rdropRegion.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropCS_OnDataBound(object sender, EventArgs e)
        {
            rdropCS.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropJrCS_OnDataBound(object sender, EventArgs e)
        {
            rdropJrCS.Items.Insert(0, new RadComboBoxItem());
        }
    }
}