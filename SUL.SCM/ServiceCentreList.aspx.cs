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
    public partial class ServiceCentreList : System.Web.UI.Page
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

            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Service Center List") : new AppFunctionality().GetAppFunctionalityId("Service Center List", int.Parse(lblsource.Text));
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home ");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Home ");
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
        private void LoadServiceCentre()
        {
            try
            {
                DataTable dtServiceCentre = new ServiceCenter().GetServiceCenterFromViewList();
                RadGridServiceCentre.DataSource = dtServiceCentre;
                RadGridServiceCentre.DataBind();
                if (!IsValidUpdateForUser())
                {
                    RadGridServiceCentre.MasterTableView.GetColumn("colId").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load Service Centre Grid." + ex);
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
                    Response.Redirect("UserLogin.aspx?refPage=home.aspx?"+str);
            }
            if (!IsValidPageForUser())
            {
                Alert.Show("Sorry,You don't have permission to access this page.");
                Response.Redirect("UserLogin.aspx?refPage=home.aspx", false);
            }
            if (!IsPostBack)
            {
                lblisNewEntry.Text = "true";
                this.LoadServiceCentre(); 
            }
        }

        protected void RadGridServiceCentre_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            int delete;

            if (e.CommandName == "btnDelete") 
            {
                try 
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    int id = int.Parse(item["colId"].Text);

                    delete = new  ServiceCenter().DeleteServiceCenterById(id);
                    if (delete == 0) {
                        Alert.Show("Service is deleted");
                        return;
                    } else {
                        Response.Redirect("ServiceCentreList.aspx");
                    }
                }
                catch (Exception ex) 
                {
                    Alert.Show(ex.Message);
                }

            }

        }

        protected void RadGridServiceCentre_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

            this.LoadServiceCentre();
        }

        protected void RadGridServiceCentre_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadServiceCentre();
        }

       

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton editLinkBtn = (LinkButton)sender;
            string[] Id = editLinkBtn.CommandArgument.ToString().Split(';');
            Response.Redirect("ServiceCenterInfo.aspx?Id=" + Id[0]);

        }


    }
}