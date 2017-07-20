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
    public partial class LCManagementList : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        private Company _company;
        //private bool isNewEntry;

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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("LC Management List");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("LC Management List");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("LC Management List");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("LC Management List");
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

        private void LoadLCInfoList()
        {
            try
            {
                DataTable lstLcInformations = new LCInformation().GetLCFromViewList();
                if (lstLcInformations.Rows.Count <= 0)
                {
                    RadGridLCInfo.DataSource = new string[] {};
                    return;
                }
                RadGridLCInfo.DataSource = lstLcInformations;
                RadGridLCInfo.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridLCInfo.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load LC Information Grid."+ex);
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
                this.LoadLCInfoList();

            }
        }

        protected void RadGridLCInfo_OnItemCommand(object sender, GridCommandEventArgs e)
        {
             int delete;

            if (e.CommandName == "btnDelete")
            {
                try
                {
                    GridDataItem item = (GridDataItem) e.Item;
                    int id = int.Parse(item["colId"].Text);

                    List<ReceiverMaster> lstReceiverMasters=new ReceiverMaster().GetReceiverMasterByLCId(id);

                    if (lstReceiverMasters.Count != 0)
                    {
                        Alert.Show("P.I. Number Already used in Receiving Information  . You can not Delete This.");
                        return;
                    }
                    else
                    {
                        LCInformation objLcInformation=new LCInformation();
                        delete = objLcInformation.DeleteLCInformationById(id);
                        if (delete == 0)
                        {
                            Alert.Show("Data is not Delete");
                            return;
                        }
                        else
                        {
                            Response.Redirect("LCManagementList.aspx");
                        }

                    }

                }
                catch (Exception ex)
                {
                    Alert.Show(ex.Message);
                }
            }
            }

        protected void RadGridLCInfo_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadLCInfoList();
        }
        
        protected void RadGridLCInfo_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadLCInfoList();
        }
        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            LinkButton linkedit = (LinkButton)sender;
            string[] Id = linkedit.CommandArgument.ToString().Split(';');


            Response.Redirect("LCManagementInfo.aspx?Id=" + Id[0]);
        }

       
    }
}