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
    public partial class SetupChannelDesignation : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Setup Channel");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Setup Channel");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Setup Channel");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Setup Channel");
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
        private void LoadDesignation()
        {
            try
            {
                List<Designation> lstDesig = new Designation().GetAllDesignation();

                lstDesig.Insert(0, new Designation());

                rdropCM.DataTextField = "DesignationName";
                rdropCM.DataValueField = "Id";

                rdropCM.DataSource = lstDesig;
                rdropCM.DataBind();
                if (lstDesig.Count == 2)
                    rdropCM.SelectedIndex = 1;
                else
                    rdropCM.SelectedIndex = 0;

                rdropCS.DataTextField = "DesignationName";
                rdropCS.DataValueField = "Id";

                rdropCS.DataSource = lstDesig;
                rdropCS.DataBind();
                if (lstDesig.Count == 2)
                    rdropCS.SelectedIndex = 1;
                else
                    rdropCS.SelectedIndex = 0;

                rdropACM.DataTextField = "DesignationName";
                rdropACM.DataValueField = "Id";

                rdropACM.DataSource = lstDesig;
                rdropACM.DataBind();
                if (lstDesig.Count == 2)
                    rdropACM.SelectedIndex = 1;
                else
                    rdropACM.SelectedIndex = 0;

                rdropJrCS.DataTextField = "DesignationName";
                rdropJrCS.DataValueField = "Id";

                rdropJrCS.DataSource = lstDesig;
                rdropJrCS.DataBind();
                if (lstDesig.Count == 2)
                    rdropJrCS.SelectedIndex = 1;
                else
                    rdropJrCS.SelectedIndex = 0;



            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Designation DropdownList" + ex);
            }
        }

        private void LoadSetupChannel()
        {
            try
            {
                DataTable dtSetupchannel = new SetupChannel().GetAllViewSetupChannel();
                RadGridSetUpChennal.DataSource = dtSetupchannel;
                RadGridSetUpChennal.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridSetUpChennal.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                
                Alert.Show("Something is going wrong to Load data");
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
                lblisNewEntry.Text = "true";

                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }



                this.LoadDesignation();
                this.LoadSetupChannel();
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                SetupChannel objsSetupChannel=new SetupChannel();
                int success;
                if (rdropCM.SelectedIndex > 0)
                {
                    int cmvalue = int.Parse(rdropCM.SelectedValue);
                    string cmName = "Channel Manager";

                    objsSetupChannel.ChannelPosition = cmName;
                    objsSetupChannel.DesignationId = cmvalue;

                    SetupChannel setupChannel = new SetupChannel().GetAllSetupChannelByChannels(cmName);
                    if (setupChannel.Id != 0)
                    {
                        objsSetupChannel.Id = setupChannel.Id;
                        success = objsSetupChannel.UpdateSetupChannel();
                        
                    }
                    else
                    {
                        success = objsSetupChannel.InsertSetupChannel();
                    }
                    if (success == 0)
                    {
                        Alert.Show("Data Is not save succesfully");
                        return;
                    }
                    else
                    {
                        Alert.Show("Data save succesfully");
                    }


                }
                if (rdropCS.SelectedIndex > 0)
                {
                    int csvalue = int.Parse(rdropCS.SelectedValue);
                    string csName = "Channel Specialized";

                    objsSetupChannel.ChannelPosition = csName;
                    objsSetupChannel.DesignationId = csvalue;

                    SetupChannel setupChannel = new SetupChannel().GetAllSetupChannelByChannels(csName);
                    if (setupChannel.Id != 0)
                    {
                        objsSetupChannel.Id = setupChannel.Id;
                        success = objsSetupChannel.UpdateSetupChannel();
                    }
                    else
                    {
                        success = objsSetupChannel.InsertSetupChannel();
                    }
                    if (success == 0)
                    {
                        Alert.Show("Data Is not save succesfully");
                        return;
                    }
                    else
                    {
                        Alert.Show("Data save succesfully");
                    }
                }


                if (rdropACM.SelectedIndex > 0)
                {
                    int acmvalue = int.Parse(rdropACM.SelectedValue);
                    string acmName = "Assistant Channel Manager";

                    objsSetupChannel.ChannelPosition = acmName;
                    objsSetupChannel.DesignationId = acmvalue;

                    SetupChannel setupChannel = new SetupChannel().GetAllSetupChannelByChannels(acmName);
                    if (setupChannel.Id != 0)
                    {
                        objsSetupChannel.Id = setupChannel.Id;
                        success = objsSetupChannel.UpdateSetupChannel();
                    }
                    else
                    {
                        success = objsSetupChannel.InsertSetupChannel();
                    }
                    if (success == 0)
                    {
                        Alert.Show("Data Is not save succesfully");
                        return;
                    }
                    else
                    {
                        Alert.Show("Data save succesfully");
                    }
                }
                if (rdropJrCS.SelectedIndex > 0)
                {
                    int jrcsvalue = int.Parse(rdropJrCS.SelectedValue);
                    string jrcsName = "Jr. Channel Specialized";

                    objsSetupChannel.ChannelPosition = jrcsName;
                    objsSetupChannel.DesignationId = jrcsvalue;

                    SetupChannel setupChannel = new SetupChannel().GetAllSetupChannelByChannels(jrcsName);
                    if (setupChannel.Id != 0)
                    {
                        objsSetupChannel.Id = setupChannel.Id;
                        success = objsSetupChannel.UpdateSetupChannel();
                    }
                    else
                    {
                        success = objsSetupChannel.InsertSetupChannel();
                    }
                    if (success == 0)
                    {
                        Alert.Show("Data Is not save succesfully");
                        return;
                    }
                    else
                    {
                        Alert.Show("Data save succesfully");
                    }
                }
                this.LoadSetupChannel();
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to save setup channel desigation");
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            
        }

        protected void RadGridSetUpChennal_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            
        }

        protected void rdropCS_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string CMValue = rdropCM.Text;
            string CSValue = rdropCS.Text;

            if (CMValue == CSValue)
            {
                Alert.Show("Please Select a different Designation For Channel specialized");
                rdropCS.SelectedIndex = 0;
                rdropCS.Focus();
                return;
            }
        }

        protected void rdropACM_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string CMValue = rdropCM.Text;
            string CSValue = rdropCS.Text;
            string ACM = rdropACM.Text;
            if (ACM == CMValue || ACM == CSValue)
            {
                Alert.Show("Please Select a different Designation For  Assistant Channel Manager");
                rdropACM.SelectedIndex = 0;
                rdropACM.Focus();
                return;
            }
        }

        protected void rdropJrCS_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string CMValue = rdropCM.Text;
            string CSValue = rdropCS.Text;;
            string jrCS = rdropJrCS.Text;
            string ACM = rdropACM.Text;
            if (jrCS == CMValue || jrCS == CSValue || jrCS == ACM)
            {
                Alert.Show("Please Select a different Designation For Jr. Channel specialized");
                rdropJrCS.SelectedIndex = 0;
                rdropJrCS.Focus();
                return;
            }
        }
    }
}