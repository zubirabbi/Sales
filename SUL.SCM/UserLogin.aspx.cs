using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using System.Resources;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Data;
using System.EnterpriseServices;
using System.Net;
using Telerik.Web.UI;
using System.Security.Policy;
using System.Text;
using hrms;
using SUL.Bll.Base;

namespace SUL.SCM
{
    public partial class UserLogin : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        private string DetermineCompName(string IP)
        {
            IPAddress myIP = IPAddress.Parse(IP);
            IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
            List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
            return compName.First();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string IP = Request.UserHostName;
                    string compName = DetermineCompName(IP);
                    lblText.Text = "IP: "+IP +" Name: "+compName +" SessionID: "+ Session.SessionID.ToString();
                    string _refPage = Request.QueryString["action"] != null
                                ? Request.QueryString["action"].ToString()
                                : string.Empty;

                    if (_refPage.ToLower() == "logout")
                    {
                        Users user = (Users)Session["user"];

                        Session["user"] = null;
                        Session.Clear();
                        Session.RemoveAll();
                        Session.Abandon();

                        UserLoginLog log = new UserLoginLog().GetUserLastLogin(user.Id);
                        if (log.Id != 0)
                        {
                            new UserLoginLog().UpdateStatus(log.Id, "Logged Out");
                            user = new Users();
                        }
                        return;
                    }

                    if (Session["user"] != null)
                    {
                        Users user = (Users)Session["user"];

                        if (user.Id != 0)
                        {
                            //Company company = ;
                            Response.Redirect("HomePage.aspx");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Error during page load. Error: " + ex.Message);
            }
        }

        protected void submit_OnServerClick(object sender, EventArgs e)
        {
            try
            {
                Users user = new Users().GetUserByUserName(txtUserName.Text);
                if (user.Id != 0)
                {
                    if (user.UserPass != txtPassword.Text)
                    {
                        Alert.Show("User and password didn't match. Please re-enter the correct password.");
                        txtPassword.Focus();
                        return;
                    }

                    string IP = Request.UserHostName;
                    string compName = DetermineCompName(IP);

                    UserLoginLog log = new UserLoginLog().GetUserLastLogin(user.Id);
                    if (log.Id != 0)
                    {
                        if (log.IpAddress != IP && log.Status == "Logged In")
                        {
                            Alert.Show("Sorry! This is user is already logged in from another PC.");
                            return;
                        }
                    }

                    Session["user"] = user;
                    UserRoleMapping userRole = new UserRoleMapping().GetUserRoleMappingByUserId(user.Id, user.CompanyId);
                    UserRole role = new UserRole().GetUserRoleById(userRole.RoleId, user.CompanyId);
                    Session["Role"] = role;


                    this.GenerateMenu(user);

                    log = new UserLoginLog();
                    log.UserId = user.Id;
                    log.SessionId = Session.SessionID;

                    

                    log.IpAddress = IP;
                    log.LoginPCName = compName;
                    log.LoginTime = DateTime.Now;
                    log.Status = "Logged In";
                    log.LogOutTime = PublicVariables.minDate;

                    log.InsertUserLoginLog();

                    UserRoleMapping userRoles = new UserRoleMapping().GetUserRoleMappingByUserId(user.Id, user.CompanyId);
                    if (userRoles.Id != 0 && user.Id == 1)
                        user.IsSuperUser = true;
                    else
                        user.IsSuperUser = false;

                    Company company = new Company();
                    int SingleUserCom = new AppConfiguration().GetSingleCompany();
                    if (SingleUserCom != 0)
                    {
                        company = new Company().GetParentCompany();
                    }
                    else
                    {
                        if (userRoles.Id != 0 && user.Id == 1)
                        {
                            company = new Company().GetParentCompany();
                        }
                        else
                        {
                            company = new Company().GetCompanyById(user.CompanyId);
                        }
                    }
                    Session["company"] = company;
                
                    if (user.CompanyId == 0 && !user.IsSuperUser)
                    {
                        Alert.Show("Sorry this user is not associated with any company. Contact your system administrator to fix this issue.");
                        return;
                    }

                    if (user.EmployeeId != 0)
                    {
                        Department objDepartment = new Department().GetEmployeeDepartment(user.EmployeeId);
                        Session["Department"] = objDepartment.DepartmentName;
                    }
                    else
                        Session["Department"] = "All";

                    string refPage = (Request.QueryString["refPage"] == null) ? string.Empty : Request.QueryString["refPage"].ToString();
                    Response.Redirect(((refPage == string.Empty || refPage.ToLower() == "logout") ? "HomePage.aspx" : refPage), false);
                }
                else
                {
                    Alert.Show("The user is not exist in the database. Please check the username.");
                    txtUserName.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Error during process user authentication. Error: " + ex.Message);
            }
        }

        private void GenerateMenu(Users _user)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<ul class='list-unstyled'>\n");
            int parentId = 0;
            bool parentUlOpen = false;
            List<AppPermission> permissionList = new AppPermission().GelAppFunctionalityForMenu(_user.CompanyId, _user.Id);
            List<AppModule> moduleList = new AppModule().GetAllAppModule(_user.CompanyId, _user.Id);

            foreach (AppModule module in moduleList)
            {
                List<AppPermission> modulePermission = permissionList.FindAll(x => x.ModuleName == module.Module);

                if (modulePermission.Count == 0) continue;

                if (modulePermission[0].IsView)
                {
                    foreach (AppPermission appPermission in modulePermission)
                    {

                        if (appPermission.Url == "#")
                        {
                            if (parentUlOpen)
                            {
                                str.Append("</ul>\n");
                                str.Append("</li>\n");
                            }


                            str.Append("<li class='has-sub'><a href='#' data-target='#menu-style_" + appPermission.FunctionalityId + "' data-toggle='collapse'><i class='icon-compose'></i><span>" + appPermission.FunctionalityName + "</span></a>\n");
                            str.Append("<ul class='collapse ' id='menu-style_" + appPermission.FunctionalityId + "'>\n");

                            parentId = appPermission.FunctionalityId;
                            parentUlOpen = true;
                        }
                        else
                        {

                            if (appPermission.ParentId != parentId)
                            {
                                if (parentUlOpen)
                                {
                                    str.Append("</ul>\n");
                                    str.Append("</li>\n");
                                }

                                if (appPermission.ParentId == 0)
                                    parentUlOpen = false;
                            }

                            str.Append("<li><a target=\"_self\" href=\"" + appPermission.Url + "\">" + appPermission.FunctionalityName + "</a> </li>\n");


                        }
                    }

                }
            }

            if (parentUlOpen)
            {
                str.Append("</ul>\n");
                str.Append("</li>\n");
            }

            str.Append("</ul>\n");

            Session["Menu"] = str.ToString();
        }
    }
}