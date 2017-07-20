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
    public partial class CustomerProblem : System.Web.UI.Page
    {
        private UserRole _role;
        private Users _user;
        private Company _company;
        private string _department;
        private int _source;
        private AppPermission PermissionUser;

        private bool IsValidSession()
        {
            if (Session["user"] == null)
            {
                return false;
            }

            _user = (Users)Session["user"];
            _company = (Company)Session["company"];
            _department = Session["Department"].ToString();

            return _user.Id != 0;

        }

        private bool IsValidPageForUser()
        {
            //int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Data Export");
            int FunctionalId = lblsource.Text == string.Empty ? new AppFunctionality().GetAppFunctionalityId("Home") : new AppFunctionality().GetAppFunctionalityId("Home", int.Parse(lblsource.Text));
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
        private void ServiceLevel()
        {
            try
            {
                List<ListTable> lstListTables = new ListTable().GetAllListTableByType("SevereLevel");
                lstListTables.Insert(0, new ListTable());

                rdropServiceLevel.DataTextField = "ListValue";
                rdropServiceLevel.DataValueField = "ListId";
                rdropServiceLevel.DataSource = lstListTables;
                rdropServiceLevel.DataBind();

                rdropServiceLevel.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                Alert.Show(ex.Message);
            }
        }
        private void LoadCustomerProblem()
        {
            try
            {
                //List<Problems> lstProblemses = new Problems().GetAllProblems();
                DataTable dtCustomerProblem = new Problems().GetCustomerProblemFromViewListTable();
                if (dtCustomerProblem.Rows.Count == 0)
                {
                    RadGridCustomerProblem.DataSource = new string[] { };
                    RadGridCustomerProblem.DataBind();
                    return;
                }
                RadGridCustomerProblem.DataSource = dtCustomerProblem;
                RadGridCustomerProblem.DataBind();
            }
            catch (Exception ex)
            {

                Alert.Show("something is going wrong to load data." + ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblsource.Text = Request.QueryString["source"] ?? "0";
            if (!IsValidSession())
            {
                string str = Request.QueryString.ToString();
                if (str == string.Empty)
                    Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx");
                else
                    Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx?" + str);
            }
            else
                _department = "All";
            if (!IsPostBack)
            {
                lblisNewEntry.Text = "true";
                this.LoadCustomerProblem();
                this.ServiceLevel();
            }
            if (IsValidPageForUser())
            {

                PermissionUser = (AppPermission)Session["PermissionUser"];
                if (!PermissionUser.IsView)
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                if (bool.Parse(lblisNewEntry.Text))
                {
                    if (!PermissionUser.IsInsert)
                    {
                        Alert.Show("Sorry, You Don't Have permission to access this page.");
                        Response.Redirect("ErrorPage.aspx", false);
                    }
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {

                #region validation

                if (rtxtName.Text == string.Empty)
                {
                    Alert.Show("Please enter a problem name");
                    rtxtName.Focus();
                    return;
                }
                if (rdropServiceLevel.SelectedIndex == 0)
                {
                    Alert.Show("Please select a service level");
                    rdropServiceLevel.Focus();
                    return;
                }

                #endregion
                int success = 0;
                Problems objProblem = new Problems();
                objProblem.Name = rtxtName.Text;
                objProblem.Description = rtxtDescription.Value;
                objProblem.SeverLevel = int.Parse(rdropServiceLevel.SelectedValue);
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objProblem.InsertProblems();
                }
                else
                {
                    objProblem.Id = int.Parse(lblId.Text);
                    success = objProblem.UpdateProblems();
                }
                if (success == 0)
                {
                    Alert.Show("Data is not save succesfully");
                }
                else
                {
                    Alert.Show("Data save succesfully");
                    ClearAllInfo();
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is gong wrong to save data." + ex);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ClearAllInfo();
        }

        private void ClearAllInfo()
        {
            rtxtDescription.Value = null;
            rtxtName.Text = "";
            rdropServiceLevel.SelectedIndex = 0;
            this.LoadCustomerProblem();
        }

        protected void RadGridCustomerProblem_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadCustomerProblem();
        }

        protected void RadGridCustomerProblem_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadCustomerProblem();
        }

        protected void RadGridCustomerProblem_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int delete;



            if (e.CommandName == "btnDelete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                Problems objProblem = new Problems();
                int id = int.Parse(lblId.Text = item["colId"].Text);
                delete = objProblem.DeleteProblemsById(id);
                if (delete == 0)
                {
                    Alert.Show("Something is going wrong to delete data");
                }
                else
                {
                    Response.Redirect("CustomerProblem.aspx");
                }

            }
            if (e.CommandName == "btnSelect")
            {
                try
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblId.Text = item["colId"].Text;
                    rtxtName.Text = item["colName"].Text;
                    rtxtDescription.Value = item["colDescription"].Text;
                    rdropServiceLevel.SelectedValue = item["colSeverLevel"].Text;
                    lblisNewEntry.Text = "false";
                }
                catch (Exception ex)
                {
                    Alert.Show(ex.Message);
                }
            }
        }
    }
}