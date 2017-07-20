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
    public partial class DealerInfoList : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        private bool isNewEntry;
        private Company _company;

        private static int _source;

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

            int FunctionalId = _source == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer List") : new AppFunctionality().GetAppFunctionalityId("Dealer List", _source);
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
            int FunctionalId = _source == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer List") : new AppFunctionality().GetAppFunctionalityId("Dealer List", _source);
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
            int FunctionalId = _source == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer List") : new AppFunctionality().GetAppFunctionalityId("Dealer List", _source);
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
            int FunctionalId = _source == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer List") : new AppFunctionality().GetAppFunctionalityId("Dealer List", _source);
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
        private void LoadDealerGrid()
        {
            try
            {
                DataTable lstDealerInformations = new DealerInformation().GetAllDealerInformationView();

                if (lstDealerInformations.Rows.Count == 0)
                {
                    RadGridDealer.DataSource = new string[] { };
                    RadGridDealer.DataBind();
                    return;
                }
                RadGridDealer.DataSource = lstDealerInformations;
                RadGridDealer.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridDealer.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("somrthing is going wrong to Load Dealer grid." + ex);
            }
        }
        private void LoadDealer()
        {
            try
            {
                DataTable dtDealerInfo = new DealerInformation().GetAllDealerInformationView();

                rdropPDealer.DataTextField = "DealerInfo";
                rdropPDealer.DataValueField = "Id";
                rdropPDealer.DataSource = dtDealerInfo;
                rdropPDealer.DataBind();

                if (dtDealerInfo.Rows.Count == 2)
                    rdropPDealer.SelectedIndex = 1;
                else
                    rdropPDealer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _source = Request.QueryString["source"] != null ? int.Parse(Request.QueryString["source"].ToString()) : 0;


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
                this.LoadDealerGrid();
                this.LoadDealer();
            }
        }
        protected void RadGridDealer_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadDealerGrid();
        }

        protected void RadGridDealer_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadDealerGrid();
        }

        protected void RadGridDealer_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;
            DealerInformation objDealerCategory = new DealerInformation();
            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;

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

                this.LoadDealerGrid();
            }
            if (e.CommandName.Equals(RadGrid.FilterCommandName))
            {
                this.LoadDealerGrid();
            }
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
             LinkButton linkedit = (LinkButton)sender;
            string[] Id = linkedit.CommandArgument.ToString().Split(';');


            Response.Redirect("DealerInfo.aspx?Id=" + Id[0]);
        }

        protected void rdropPDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropPDealer.Items.Insert(0,new RadComboBoxItem());
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int dealerId = Convert.ToInt32(rdropPDealer.SelectedValue);
            try
            {
                DataTable lstDealerInformations = new DealerInformation().GetAllDealerInformationViewById(dealerId);

                if (lstDealerInformations.Rows.Count == 0)
                {
                    RadGridDealer.DataSource = new string[] { };
                    RadGridDealer.DataBind();
                    return;
                }
                RadGridDealer.DataSource = lstDealerInformations;
                RadGridDealer.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridDealer.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("somrthing is going wrong to Load Dealer grid." + ex);
            }
            

        }
    }
}