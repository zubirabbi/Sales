using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class SupplierList : System.Web.UI.Page
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier List");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier List");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier List");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Supplier List");
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

        private void LoadSupplierList()
        {
            try
            {
                //List<Supplier> lstSuppliers = new Supplier().GetAllSupplier();
                DataTable dtSupplier = new Supplier().GetSupplierFromViewList();

                RadGridSupplier.DataSource = dtSupplier;
                RadGridSupplier.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridSupplier.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load Supplier grid" + ex);
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
                this.LoadSupplierList();
            }
        }
        protected void RadGridSupplier_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }

        protected void RadGridSupplier_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {

        }

        protected void RadGridSupplier_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;

            if (e.CommandName == "btnDelete")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    int id = int.Parse(item["colId"].Text);

                    List<PurchaseOrderMaster> lstPurchaseOrderMasters =
                        new PurchaseOrderMaster().GetPurchaseOrderByVandorId(id);
                    if (lstPurchaseOrderMasters.Count > 0)
                    {
                        Alert.Show("Supplier name is Used in PurchaseOrder.And It can not delete");
                        return;
                    }
                    List<PIMaster> lstPiMasters = new PIMaster().GetAllPIMasterbyVendorId(id);
                    if (lstPiMasters.Count > 0)
                    {
                        Alert.Show("Supplier name is Used in P.I.And It can not delete");
                        return;
                    }
                    List<LCInformation> lstLcInformations = new LCInformation().GetAllLCInformationbySupplierId(id);
                    if (lstLcInformations.Count > 0)
                    {
                        Alert.Show("Supplier name is Used in LC Information.And It can not delete");
                        return;
                    }
                    List<ReceiverMaster> lstReceiverMasters = new ReceiverMaster().GetReceiverMasterByVandorId(id);
                    if (lstReceiverMasters.Count > 0)
                    {
                        Alert.Show("Supplier name is Used in LC Information.And It can not delete");
                        return;
                    }


                    BankInformation objbanInformation = new BankInformation().GetBankInformationBySupplierId(id, "supplier");

                    int bankId = objbanInformation.Id;

                    int deleteBank = new BankInformation().DeleteBankInformationById(bankId);
                    int mapId;
                    int deletemap = 0;
                    List<ProductMapping> lstMappings = new ProductMapping().GetAllProductMappingBySupplierId(id);
                    foreach (ProductMapping lstProductMapping in lstMappings)
                    {
                        mapId = lstProductMapping.Id;

                        deletemap = new ProductMapping().DeleteProductMappingById(mapId);

                        if (deletemap == 0)
                        {
                            Alert.Show("Data is not delete");
                            return;
                        }
                    }
                    if (deletemap != 0)
                    {
                        delete = new Supplier().DeleteSupplierById(id);
                        if (delete == 0)
                        {
                            Alert.Show("Data is not delete");
                            return;
                        }
                        else
                        {
                            Alert.Show("Data delete succesfully.");
                            //this.LoadSupplierList();
                            Response.Redirect("SupplierList.aspx");

                        }

                    }
                    if (lstMappings.Count == 0)
                    {
                        delete = new Supplier().DeleteSupplierById(id);
                        if (delete == 0)
                        {
                            Alert.Show("Data is not delete");
                            return;
                        }
                        else
                        {
                            Alert.Show("Data delete succesfully.");
                            //this.LoadSupplierList();
                            Response.Redirect("SupplierList.aspx");

                        }
                    }

                }
                catch (Exception ex)
                {
                    Alert.Show("Something is going wrong to delete supplier Info");
                }

            }
            if (e.CommandName.Equals(RadGrid.FilterCommandName))
            {
                this.LoadSupplierList();
            }
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            LinkButton linkedit = (LinkButton)sender;
            string[] Id = linkedit.CommandArgument.ToString().Split();


            Response.Redirect("SupplierInfo.aspx?Id=" + Id[0]);
        }


    }
}