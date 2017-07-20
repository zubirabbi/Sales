using SUL.Bll;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class serviceCreationInfo : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        private Company _company;
        private Hashtable deletedItems;

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

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Serivce Info") : new AppFunctionality().GetAppFunctionalityId("Serivce Info", int.Parse(lblsource.Text));
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
        private void LoadServiceLevel()
        {
            try
            {
                DataTable dtListTables = new ListTable().GetAllDataByListTypeId("Service");
                rdropServiceLevel.DataTextField = "ListValue";
                rdropServiceLevel.DataValueField = "ListId";
                rdropServiceLevel.DataSource = dtListTables;
                rdropServiceLevel.DataBind();

                if (dtListTables.Rows.Count == 2)
                    rdropServiceLevel.SelectedIndex = 1;
                else
                    rdropServiceLevel.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                Alert.Show("Something is going wrong to Load Service Level" + ex);
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
                    Response.Redirect("UserLogin.aspx?rehPage=home.aspx?" + str);
            }
            if (!IsValidPageForUser())
            {
                Alert.Show("Sorry,You don't have permission to access this page.");
                Response.Redirect("UserLogin.aspx?refPage=home.aspx", false);
            }
            if (!IsPostBack)
            {
                lblisNewEntry.Text = "true";

                this.LoadServiceLevel();
                ClearAllInfo();
                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];
                    lblId.Text = id;

                    Service objseService=new Service().GetServiceById(int.Parse(id));

                    rtxtDesceiption.Text = objseService.ServiceDescription;
                    rtxtNonWarrentyCost.Text = objseService.NWCost.ToString();
                    rtxtServiceName.Text = objseService.ServiceName;
                    rtxtWarrentyCost.Text = objseService.WCost.ToString();
                    rdropServiceLevel.SelectedValue = objseService.ServiceLevel.ToString();
                    rdtServiceTime.SelectedDate = objseService.ServiceTime;
                    chkIsSparePartsRequired.Checked = objseService.IsSPReqired;

                    lblisNewEntry.Text = "false";


                }


            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                #region Validation
                if (rtxtServiceName.Text == string.Empty)
                {
                    Alert.Show("Please enter Service Name");
                    rtxtServiceName.Focus();
                    return;
                }
                if (rtxtDesceiption.Text == string.Empty)
                {
                    Alert.Show("Please enter Description");
                    rtxtDesceiption.Focus();
                    return;
                }
                if (rtxtNonWarrentyCost.Text == string.Empty)
                {
                    Alert.Show("Please enter Non Warrenty Cost.");
                    rtxtNonWarrentyCost.Focus();
                    return;
                }
                if (rtxtWarrentyCost.Text == string.Empty)
                {
                    Alert.Show("Please enter Warrenty cost");
                    rtxtWarrentyCost.Focus();
                    return;
                }
                if (rdropServiceLevel.SelectedIndex <= 0)
                {
                    Alert.Show("Please enter Service Level");
                    rdropServiceLevel.Focus();
                    return;
                }
                if (rdtServiceTime.SelectedDate == null)
                {
                    Alert.Show("Please enter Service Time");
                    rdtServiceTime.Focus();
                    return;
                }
                #endregion

                bool IsRequired = false || chkIsSparePartsRequired.Checked == true;

                Service objService = new Service();
                objService.ServiceName = rtxtServiceName.Text;
                objService.ServiceDescription = rtxtDesceiption.Text;
                objService.NWCost = Convert.ToDecimal(rtxtNonWarrentyCost.Text);
                objService.WCost = Convert.ToDecimal(rtxtWarrentyCost.Text);
                objService.ServiceLevel = int.Parse(rdropServiceLevel.SelectedValue);
                objService.ServiceTime = DateTime.Parse(rdtServiceTime.SelectedDate.ToString());
                objService.IsSPReqired = IsRequired;

                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objService.InsertService();
                }
                else
                {
                    if (lblId.Text == string.Empty || lblId.Text == "0")
                    {
                        success = objService.InsertService();
                    }
                    else
                    {
                        objService.Id = int.Parse(lblId.Text);
                        success = objService.UpdateService();
                    }
                }
                if (success == 0)
                {
                    Alert.Show("Service Information was not save successfully");
                }
                else
                {
                    ClearAllInfo();
                    Alert.Show("Service Information save successfully");
                }

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to save Service Information" + ex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllInfo();
        }

        private void ClearAllInfo()
        {
            rtxtServiceName.Text = "";
            rtxtDesceiption.Text = "";
            rtxtNonWarrentyCost.Text = "";
            rtxtWarrentyCost.Text = "";
            rdropServiceLevel.SelectedIndex = 0;
            rdtServiceTime.SelectedDate = null;
            chkIsSparePartsRequired.Checked = false;
            lblId.Text = "";
            lblisNewEntry.Text = "true";
        }

        protected void rdropServiceLevel_DataBound(object sender, EventArgs e)
        {
            rdropServiceLevel.Items.Insert(0,new RadComboBoxItem());
        }
    }
}