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
    public partial class ProformaInvoiceEntryList : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  Company _company;
        //private  bool isNewEntry = true;

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

        private void LoadPIInformation()
        {
            try
            {
                DataTable lstPiMaster = new PIMaster().GetPIFromViewList();
                if (lstPiMaster.Rows.Count <= 0)
                {
                    RadGridProformaInvoice.DataSource = new string[] { };
                    return;
                }


                RadGridProformaInvoice.DataSource = lstPiMaster;
                RadGridProformaInvoice.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridProformaInvoice.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load PI Information" + ex);
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
                this.LoadPIInformation();
            }
        }

        protected void RadGridProformaInvoice_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadPIInformation();
        }

        protected void RadGridProformaInvoice_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadPIInformation();
        }

        protected void RadGridProformaInvoice_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;

            if (e.CommandName == "btnDelete")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    int id = int.Parse(item["colId"].Text);
                    List<LCInformation> lstLcInformations=new LCInformation().GetLCByPIId(id);
                    if (lstLcInformations.Count != 0)
                    {
                        Alert.Show("P.I. Number Already used in LC . You can not Delete This.");
                        return;
                    }
                    else
                    {
                        PIMaster objPiMaster=new PIMaster();
                        PIDetails objPiDetails=new PIDetails();

                        int deleteDetails = objPiDetails.DeletePIDetailsByMasterId(id);
                        if (deleteDetails == 0)
                        {
                            Alert.Show("PI Details Is not delete.");
                            return;
                        }
                        else
                        {
                            delete = objPiMaster.DeletePIMasterById(id);
                            if (delete == 0)
                            {
                                Alert.Show("PI Is not delete.");
                                return;
                            }
                            else
                            {
                                Response.Redirect("ProformaInvoiceEntryList.aspx");
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Alert.Show("Data Is not Delete."+ex.Message);
                }
            }
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            LinkButton linkedit = (LinkButton)sender;
            string[] Id = linkedit.CommandArgument.ToString().Split(';');


            Response.Redirect("ProformaInvoiceEntryInfo.aspx?Id=" + Id[0]);
        }
    }
}