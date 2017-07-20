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
    public partial class CampaingSetupList : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  bool isNewEntry;
        private  Company _company;

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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Campaign Setup List");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Campaign Setup List");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Campaign Setup List");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Campaign Setup List");
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

        private void LoadCampaignMaster()
        {
            DataTable dtCampaignMaster = new CampaignMaster().GetCampaignViewList();
            if (dtCampaignMaster.Rows.Count == 0)
            {
                RadGridCampaign.DataSource = new string[] {};
                return;
            }
            RadGridCampaign.DataSource = dtCampaignMaster;
            RadGridCampaign.DataBind();

            if (!IsValidUpdateForUser())
            {
                RadGridCampaign.MasterTableView.GetColumn("colEdit").Display = false;
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
                this.LoadCampaignMaster();
            }
        }

        protected void RadGridCampaign_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadCampaignMaster();
        }

        protected void RadGridCampaign_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadCampaignMaster();
        }

        protected void RadGridCampaign_OnItemCommand(object sender, GridCommandEventArgs e)
        {
             int delete;
            DealerInformation objDealerCategory = new DealerInformation();
            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem) e.Item;

                int id = int.Parse(item["colId"].Text);
                delete = objDealerCategory.DeleteDealerInformationById(id);
                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    //this.LoadDealerCategory();
                    //this.LoadDealerGrid();
                    Alert.Show("Data Delete succesfully");
                    Response.Redirect("DealerInfo.aspx");
                }
            }
            if (e.CommandName.Equals(RadGrid.FilterCommandName))
            {
                this.LoadCampaignMaster();
            }
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            LinkButton linkedit = (LinkButton)sender;
            string[] Id = linkedit.CommandArgument.ToString().Split(';');


            Response.Redirect("CampaignSetup.aspx?Id=" + Id[0]);
        }
    }
}