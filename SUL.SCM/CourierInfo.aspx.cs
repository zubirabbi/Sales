using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class CourierInfo : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        //private  bool isNewEntry;
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Courier");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsView)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsView;
                return permission;
            }
            else
                return true;
        }

        private bool IsValidInsertForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Courier");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsInsert)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsInsert;
                return permission;
            }
            else
                return true;
        }

        private bool IsValidUpdateForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Courier");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsUpdate)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsUpdate;
                return permission;
            }
            else
                return true;
        }

        private bool IsValidDeleteForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Courier");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsDelete)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsDelete;
                return permission;
            }
            else
                return true;
        }

        private void LoadCourierGrid()
        {
            try
            {
                List<CourierInformation> lstCourierInformations = new CourierInformation().GetAllCourierInformation();

                if (lstCourierInformations.Count <= 0)
                {
                    RadGridCourierList.DataSource = new string[] { };
                    return;
                }
                RadGridCourierList.DataSource = lstCourierInformations;
                RadGridCourierList.DataBind();
                if (!IsValidUpdateForUser())
                {
                    RadGridCourierList.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load courier Grid" + ex);
            }
        }

        private void clearAllInfo()
        {
            rtxtContactName.Text = "";
            rtxtContactNo.Text = "";
            rtxtCourierName.Text = "";
            rtxtCourierAddress.Text = "";
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
                this.LoadCourierGrid();
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region

            if (rtxtContactName.Text == string.Empty)
            {
                Alert.Show("Plesae enter Contact Name.");
                rtxtContactName.Focus();
                return;
            }
            if (rtxtContactNo.Text == string.Empty)
            {
                Alert.Show("Plesae enter Contact No.");
                rtxtContactNo.Focus();
                return;
            }
            if (rtxtCourierName.Text == string.Empty)
            {
                Alert.Show("Plesae enter Courier Name.");
                rtxtCourierName.Focus();
                return;
            }
            if (rtxtCourierAddress.Text == string.Empty)
            {
                Alert.Show("Plesae enter Courier Address.");
                rtxtCourierAddress.Focus();
                return;
            }
            #endregion
            try
            {
                CourierInformation objCourierInformation = new CourierInformation();
                objCourierInformation.Name = rtxtCourierName.Text;
                objCourierInformation.Address = rtxtCourierAddress.Text;
                objCourierInformation.ContactName = rtxtContactName.Text;
                objCourierInformation.ContactNo = rtxtContactNo.Text;

                int success;

                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objCourierInformation.InsertCourierInformation();
                }
                else
                {
                    objCourierInformation.Id = int.Parse(lblId.Text);
                    success = objCourierInformation.UpdateCourierInformation();
                }
                if (success == 0)
                {
                    Alert.Show("Data is not save succesfully.");
                   
                }
                else
                {
                   
                    //this.clearAllInfo();
                    //this.LoadCourierGrid();
                    Response.Redirect("CourierInfo.aspx");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save courier data" + ex);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            clearAllInfo();
        }

        protected void RadGridCourierList_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadCourierGrid();
        }

        protected void RadGridCourierList_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadCourierGrid();
        }

        protected void RadGridCourierList_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;



            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                CourierInformation objcCourierInformation = new CourierInformation();

                int id = int.Parse(item["colId"].Text);
                delete = objcCourierInformation.DeleteCourierInformationById(id);

                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    Alert.Show("Data Delete Succesfully.");
                    //this.LoadCourierGrid();
                    //this.LoadCourierGrid();
                    Response.Redirect("CourierInfo.aspx");
                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;

                    rtxtCourierName.Text = item["colName"].Text;
                    rtxtCourierAddress.Text = item["colAddress"].Text;
                    rtxtContactName.Text = item["colContactName"].Text;
                    rtxtContactNo.Text = item["colContactNo"].Text;
                    
                    lblisNewEntry.Text = "false";

                }
                catch (Exception ex)
                {
                    Alert.Show("Something is going wrong to select data." + ex);
                }
            }
            this.LoadCourierGrid();
        }
    }
}