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
    public partial class PurchaseOrderEntryList : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  Company _company;
       // private static bool isNewEntry = true;

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

        private void LoadPurchaseOrderList()
        {
            try
            {
                DataTable dtPurchaseOrderList = new PurchaseOrderMaster().GetPurchaseOrderListFromViewList();

                RadGridPurchaseOrder.DataSource = dtPurchaseOrderList;
                RadGridPurchaseOrder.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridPurchaseOrder.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load purchase order list.");
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
                this.LoadPurchaseOrderList();
            }
        }

        protected void RadGridPurchaseOrder_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            
            int delete;

            if (e.CommandName == "btnDelete")
            {
                try
                {
                    GridDataItem item = (GridDataItem) e.Item;
                    int id = int.Parse(item["colId"].Text);


                    List<PIMaster> lstMasters = new PIMaster().GetPIByPOId(id);
                    if (lstMasters.Count != 0)
                    {
                        Alert.Show("P.O. Number Already used in P.I. You can not Delete This.");
                        return;
                    }
                    else
                    {
                        PurchaseOrderMaster objPurchaseOrderMaster=new PurchaseOrderMaster();
                        PurchaseOrderDetails objPurchaseOrderDetails=new PurchaseOrderDetails();

                        int deleteDetails = objPurchaseOrderDetails.DeletePurchaseOrderDetailsByPOMasterId(id);
                        if (deleteDetails == 0)
                        {
                            Alert.Show("P.O. Details Data not Deleted.");
                            return;
                        }
                        else
                        {
                            delete = objPurchaseOrderMaster.DeletePurchaseOrderMasterById(id);
                            if (delete == 0)
                            {
                                Alert.Show("P.O. Master Data not Deleted.");
                                return;
                            }
                            else
                            {
                                Response.Redirect("PurchaseOrderEntryList.aspx");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Alert.Show(ex.Message);
                }
            }
            }

        protected void RadGridPurchaseOrder_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadPurchaseOrderList();
        }

        protected void RadGridPurchaseOrder_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadPurchaseOrderList();
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            LinkButton linkedit = (LinkButton)sender;
            string[] Id = linkedit.CommandArgument.ToString().Split(';');


            Response.Redirect("PurchaseOrderEntryInfo.aspx?Id=" + Id[0]);
        }
    }
}