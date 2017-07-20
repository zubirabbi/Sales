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
    public partial class ListTableInfo : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  bool isNewEntry;
        private int companyId=1;

        private bool IsValidSession()
        {
            if (Session["user"] == null)
            {
                return false;
            }

            _user = (Users)Session["user"];

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
        private void LoadListTable()
        {
            try
            {
                List<ListTable> lstList = new ListTable().GetAllListTable(companyId);
                if (lstList.Count == 0)
                {
                    lstList.Add(new ListTable());
                    RadGridListTable.Visible = false;
                }
                else
                {
                    RadGridListTable.Visible = true;
                    RadGridListTable.DataSource = lstList;
                    RadGridListTable.DataBind();

                }
            }
            catch (Exception ex)
            {
                Alert.Show("Have Some Problem TO Load ListTable Grid " + ex);
            }
        }

        private void LoadListdropDown()
        {
            try
            {
                ListTable lsttable = new ListTable();
                DataTable lstType = lsttable.GetAllListTableByGroup(companyId);


                rdropListType.DataTextField = "ListType";
                rdropListType.DataValueField = "ListType";
                rdropListType.DataSource = lstType;

                rdropListType.DataBind();
                if (lstType.Rows.Count == 2)
                    rdropListType.SelectedIndex = 1;
                else
                    rdropListType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load listdropdown.");
            }
        }

        private void LoadClear()
        {
            rdropListType.SelectedIndex = 0;
            rtxtListId.Text = "";
            rtxtListValue.Text = "";
            chkIsActive.Checked = false;

            lblisNewEntry.Text = "true";
        }

        private void LoadListGridByListType()
        {
            try
            {
                DataTable dtLisTable = new ListTable().GetAllDataByListTypeId(rdropListType.Text);

                RadGridListTable.DataSource = dtLisTable;
                RadGridListTable.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going rong to load Grid."+ex);
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
                this.LoadListdropDown();
                lblisNewEntry.Text = "true";
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            int success;
            try
            {
                ListTable objlistTable = new ListTable();

                objlistTable.ListType = rdropListType.SelectedValue;
                objlistTable.ListId = int.Parse(rtxtListId.Text);
                objlistTable.ListValue = rtxtListValue.Text;
                objlistTable.CompanyId = companyId;
                objlistTable.IsActive = chkIsActive.Checked;

                if (bool.Parse(lblisNewEntry.Text))
                {
                    
                    success = objlistTable.InsertListTable();
                }
                else
                {
                    objlistTable.Id = int.Parse(lblId.Text);
                    success = objlistTable.UpdateListTable();
                }
                if (success == 0)
                {
                    Alert.Show("List table Data was not save.");
                }
                else
                {
                    
                    LoadListdropDown();
                    LoadListTable();
                    this.LoadClear();
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save data.");
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.LoadClear();
        }

        protected void RadGridListTable_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadListTable();
        }

        protected void RadGridListTable_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadListTable();
        }

        protected void RadGridListTable_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "btnSelect")
            {
                GridDataItem item = (GridDataItem)e.Item;

                lblId.Text = item["colId"].Text;
                rdropListType.SelectedValue = item["colListType"].Text.Trim();
                rtxtListId.Text = item["colListId"].Text.Trim();
                rtxtListValue.Text = item["colListValue"].Text;

                chkIsActive.Checked = bool.Parse(item["colIsActive"].Text);
                lblisNewEntry.Text = "false";
            }
            else if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;

                lblId.Text = item["colId"].Text;

                int delete = new ListTable().DeleteListTableById(int.Parse(lblId.Text));

                if (delete == 0)
                {
                    Alert.Show("Data was not delete..");
                }
                else
                    LoadListTable();
            }
        }

        protected void rdropListType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadListGridByListType();
            int lsitId = new ListTable().GetlastListTypeId(rdropListType.Text);
            lsitId += 1;
            rtxtListId.Text = lsitId.ToString();
        }

        protected void rdropListType_OnDataBound(object sender, EventArgs e)
        {
            rdropListType.Items.Insert(0, new ListItem(string.Empty, string.Empty));
        }
    }
}