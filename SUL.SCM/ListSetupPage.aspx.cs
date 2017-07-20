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
    public partial class ListSetupPage : System.Web.UI.Page
    {

        private UserRoleInfo _role;
        private Users _user;
        //private bool isNewEntry;
        private Company _company;
        private int company = 0;

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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("List Setup");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("List Setup");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("List Setup");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("List Setup");
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
        private void LoadListType()
        {
            ListTable lsttable = new ListTable();
            DataTable lstType = lsttable.GetAllListTableByGroup(company);


            rdropListType.DataTextField = "ListType";
            rdropListType.DataValueField = "ListType";
            rdropListType.DataSource = lstType;

            rdropListType.DataBind();
            rdropListType.SelectedIndex = 0;
        }
        private void LoadListTable()
        {
            try
            {
                List<ListTable> lstList = new ListTable().GetAllListTable(0);
                if (lstList.Count == 0)
                {
                    lstList.Add(new ListTable());
                    RadGridListType.Visible = false;
                }
                else
                {
                    RadGridListType.Visible = true;
                    RadGridListType.DataSource = lstList;
                    RadGridListType.DataBind();

                }
            }
            catch (Exception ex)
            {
                Alert.Show("Have Some Problem TO Load ListTable Grid " + ex);
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
                this.LoadListType();
                lblisNewEntry.Text = "true";

            }
        }

        private void LoadListTableGrid()
        {
            try
            {
                List<ListTable> lstList = new ListTable().GetAllListTable(0).Where(l => l.ListType == rdropListType.Text).ToList();
                if (lstList.Count == 0)
                {
                    lstList.Add(new ListTable());
                    RadGridListType.Visible = false;
                }
                else
                {
                    RadGridListType.Visible = true;
                    RadGridListType.DataSource = lstList;
                    RadGridListType.DataBind();

                }
            }
            catch (Exception ex)
            {
                Alert.Show("Have Some Problem TO Load ListTable Grid " + ex);
            }
        }

        protected void rdropListType_OnDataBound(object sender, EventArgs e)
        {
            var combo = (RadComboBox)sender;
            combo.Items.Insert(0, new RadComboBoxItem(" ", string.Empty));
        }

        protected void rdropListType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            this.LoadListTableGrid();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region Validation
           
            if (rtxtValue.Text == string.Empty)
            {
                Alert.Show("Please Enter The Value");
                rtxtValue.Focus();
            }
            #endregion

            try
            {
                ListTable lsttable = new ListTable();
                lsttable.Id = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);
                lsttable.IsActive = true;
                lsttable.ListType = rdropListType.Text;
                lsttable.ListValue = rtxtValue.Text;

                lsttable.CompanyId = 0;
                lsttable.ListId = lbllstId.Text == string.Empty ? 0 : int.Parse(lbllstId.Text);

                int success = 0;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    int lstid = lsttable.GetlastListTypeId(rdropListType.Text);
                    lsttable.ListId = lstid+1;
                    success = lsttable.InsertListTable();
                }
                else
                    success = lsttable.UpdateListTable();

                if (success == 0)
                {
                    Alert.Show("Create List Data Setup was not successfull.");
                    return;
                }
                else
                {
                    this.LoadListTableGrid();
                    Alert.Show("List Data Setup created succssfully.");
                }
            }
            catch (Exception ex)
            {
               Alert.Show("something is Going Wrong."+ex);
            }
            
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            
        }

        protected void RadGridListType_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnSelect")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;
                    rdropListType.SelectedValue = item["colListType"].Text.Trim();
                    lbllstId.Text = item["colListId"].Text.Trim();
                    rtxtValue.Text = item["collistvalue"].Text;
                    lblisNewEntry.Text = "false";
                }
                else if (e.CommandName == "btnDelete")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;

                    int delete = new ListTable().DeleteListTableById(int.Parse(lblId.Text));
                    if (delete == 0)
                    {
                        Alert.Show("Data is not Delete");
                    }
                    else
                    {
                        this.LoadListTableGrid();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
            
        }

        protected void RadGridListType_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadListTableGrid();
        }

        protected void RadGridListType_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadListTableGrid();
        }
    }
}