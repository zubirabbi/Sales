using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hrms;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class EmployeeInfo : System.Web.UI.Page
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Employee Add");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Employee Add");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Employee Add");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Employee Add");
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

        private void LoadDepartment()
        {
            try
            {
                List<Department> lstDept = new Department().GetAllDepartment(_company.Id);

                lstDept.Insert(0, new Department());

                rdropDept.DataTextField = "DepartmentName";
                rdropDept.DataValueField = "Id";

                rdropDept.DataSource = lstDept;
                rdropDept.DataBind();
                if (lstDept.Count == 2)
                {
                    rdropDept.SelectedIndex = 1;
                    this.LoadDesignation(int.Parse(rdropDept.SelectedValue));
                }
                else
                    rdropDept.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Parent Department DropdownList" + ex);
            }
        }

        private void LoadDesignation( int deptId)
        {
            try
            {
                DataTable lstDesig = new Designation().GetAllDesignationByDeptId(deptId);

                //lstDesig.Insert(0, new Designation());

                rdropDesignation.DataTextField = "DesignationName";
                rdropDesignation.DataValueField = "Id";

                rdropDesignation.DataSource = lstDesig;
                rdropDesignation.DataBind();
                if (lstDesig.Rows.Count == 2)
                    rdropDesignation.SelectedIndex = 1;
                else
                    rdropDesignation.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Parent Designation DropdownList" + ex);
            }
        }

        private void LoadSupervisor()
        {
            try
            {
                DataTable dtsupervisor = new EmployeeInformation().GetEmpListFromViewList(_company.Id);

                rdropSupervisor.DataTextField = "empAllInfo";
                rdropSupervisor.DataValueField = "Id";

                rdropSupervisor.DataSource = dtsupervisor;
                rdropSupervisor.DataBind();
                if (dtsupervisor.Rows.Count == 2)
                    rdropSupervisor.SelectedIndex = 1;
                else
                    rdropSupervisor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is goingwrong to load supervisor dropdownlist."+ex);
            }
        }

        private void LoadNationality()
        {
            try
            {
                List<ListTable> lstTable = new ListTable().GetAllListTableByType("Nationality");

                lstTable.Insert(0, new ListTable());

                rdropNationality.DataTextField = "ListValue";
                rdropNationality.DataValueField = "ListId";

                rdropNationality.DataSource = lstTable;
                rdropNationality.DataBind();
                if (lstTable.Count == 2)
                    rdropNationality.SelectedIndex = 1;
                else
                    rdropNationality.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Nationality DropdownList" + ex);
            }
        }
        private void LoadIdType()
        {
            try
            {
                List<ListTable> lstTable = new ListTable().GetAllListTableByType("IdType");

                lstTable.Insert(0, new ListTable());

                rdropIdType.DataTextField = "ListValue";
                rdropIdType.DataValueField = "ListId";

                rdropIdType.DataSource = lstTable;
                rdropIdType.DataBind();
                if (lstTable.Count == 2)
                    rdropIdType.SelectedIndex = 1;
                else
                    rdropIdType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load IdType DropdownList" + ex);
            }
        }

        private void LoadGender()
        {
            try
            {
                List<ListTable> lstTable = new ListTable().GetAllListTableByType("Gender");

                lstTable.Insert(0, new ListTable());

                rdropGender.DataTextField = "ListValue";
                rdropGender.DataValueField = "ListId";

                rdropGender.DataSource = lstTable;
                rdropGender.DataBind();
                if ( lstTable.Count == 2)
                    rdropGender.SelectedIndex = 1;
                else
                    rdropGender.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Gender DropdownList" + ex);
            }
        }

        private void LoadCountry()
        {
            try
            {
                List<Country> lstCountry = new Country().GetAllCountry();

                lstCountry.Insert(0, new Country());

                rdropCountry.DataTextField = "Name";
                rdropCountry.DataValueField = "Id";

                rdropCountry.DataSource = lstCountry;
                rdropCountry.DataBind();
                if (lstCountry.Count == 2)
                    rdropCountry.SelectedIndex = 1;
                else
                    rdropCountry.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Gender DropdownList" + ex);
            }
        }

        private void ClearEmpInfo()
        {
            rtxtEmployeeName.Text = "";
            rdtDOB.SelectedDate = null;
            rtxtIdNo.Text = "";
            rdropIdType.SelectedIndex = 0;
            rdropGender.SelectedIndex = 0;
            rdropBloodGroup.SelectedIndex = 0;
            rtxtAddress.Text = "";
            rtxtPostalCode.Text = "";
            rtxtPhone.Text = "";
            rtxtMobile.Text = "";
            rdropCountry.SelectedIndex = 0;
            rdropNationality.SelectedIndex = 0;
            rdropDept.SelectedIndex = 0;
            rdropDesignation.SelectedIndex = 0;
            chkIsActive.Checked = false;
            rdtJoinDate.SelectedDate = null;
            Image1.ImageUrl = null;

            ExecuteEmpCode();
            lblisNewEntry.Text = "true";
        }

        private void ExecuteEmpCode()
        {
            string EmpCode = new EmployeeInformation().GetlastEmpCode(_company.Id);
            rtxtEmpCode.ReadOnly = true;
            rtxtEmpCode.Text = EmpCode;
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



                this.ExecuteEmpCode();
                this.LoadIdType();
                this.LoadGender();
                this.LoadNationality();
                this.LoadCountry();
                this.LoadDepartment();
                this.LoadSupervisor();


                if (Request.QueryString["Id"] != null && Request.QueryString["CompanyId"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];
                    string comId = Request.QueryString["CompanyId"];

                   

                    EmployeeInformation empInfo=new EmployeeInformation().GetEmployeeInformationById(int.Parse(id),int.Parse(comId));
                    lblId.Text = empInfo.Id.ToString();
                    rtxtEmpCode.Text = empInfo.Code;
                    rtxtEmployeeName.Text = empInfo.EmployeeName;
                    rtxtIdNo.Text = empInfo.IdNo;
                    rdropIdType.SelectedValue = empInfo.IdType.ToString();
                    rdropGender.SelectedValue = empInfo.Gender.ToString();
                    rdtDOB.SelectedDate = empInfo.DateOfBirth;
                    rdropBloodGroup.SelectedValue = empInfo.BloodGroup;
                    RtxtEmail.Text = empInfo.Email;
                    rdtJoinDate.SelectedDate = empInfo.JoinDate;
                    rdropDept.SelectedValue = empInfo.DepartmentId.ToString();

                    this.LoadDesignation(empInfo.DepartmentId);
                    rdropDesignation.SelectedValue = empInfo.DesignationId.ToString();
                    rdropSupervisor.SelectedValue = empInfo.SupervisorId.ToString();
                    chkIsActive.Checked = empInfo.IsActive;
                    rtxtAddress.Text = empInfo.Address;
                    rtxtPostalCode.Text = empInfo.PostCode;
                    rdropNationality.SelectedValue = empInfo.Nationality.ToString();
                    rdropCountry.SelectedValue = empInfo.Country.ToString();
                    Image1.ImageUrl = @"~/images/emp_image" + empInfo.Code + "/" + empInfo.Photo;
                    lblImageName.Text = empInfo.Photo;
                    rtxtPhone.Text = empInfo.Phone;
                    rtxtMobile.Text = empInfo.Mobile;
                    if (empInfo.ResignationDate != DateTime.MinValue && empInfo.ResignationDate != PublicVariables.minDate)
                        rdtResignation.SelectedDate = empInfo.ResignationDate;
                    else
                        rdtResignation.SelectedDate = null;
                    lblisNewEntry.Text = "false";

                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (EmpPhoto.HasFile)
                {
                    string empCode = rtxtEmpCode.Text == string.Empty ? "Empty" : rtxtEmpCode.Text;
                    string extension = Path.GetExtension(EmpPhoto.FileName);
                    string fileName = empCode + "_Image" + extension;
                    lblImageName.Text = fileName;

                    DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/images/emp_image") + empCode);
                    if (!directory.Exists)
                        directory.Create();

                    string UploadFilePath = Server.MapPath("~/images/emp_image") + empCode + "/" +
                                            fileName;
                    FileInfo file = new FileInfo(UploadFilePath);

                    if (!file.Exists)
                        file.Delete();

                    EmpPhoto.SaveAs(UploadFilePath);
                    Image1.ImageUrl = @"~/images/emp_image" + empCode + "/" + fileName;

                }
                #region validation

                if (rtxtEmployeeName.Text == string.Empty)
                {
                    Alert.Show("Please input employee name.");
                    rtxtEmployeeName.Focus();
                    return;
                }
                if (rtxtEmpCode.Text == string.Empty)
                {
                    Alert.Show("Please input employee code.");
                    rtxtEmpCode.Focus();
                    return;

                }
                if (rdtJoinDate.SelectedDate == null)
                {
                    Alert.Show("please a select join date");
                    rdtJoinDate.Focus();
                    return;
                }
                if (rdropDept.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a depertment.");
                    rdropDept.Focus();
                    return;
                }
                if (rdropDesignation.SelectedIndex <= 0)
                {
                    Alert.Show("please select a designation.");
                    rdropDesignation.Focus();
                    return;
                }
                int id = lblId.Text == string.Empty ? 0 : int.Parse(lblId.Text);
                int codeExist = new EmployeeInformation().CheckForCodeExist(rtxtEmpCode.Text, bool.Parse(lblisNewEntry.Text), id);
                if (codeExist > 0)
                {
                    Alert.Show("This Employee code is Already Exist.");
                    return;
                }
                #endregion

                EmployeeInformation empinfo = new EmployeeInformation();

                string lstEmpCode = empinfo.GetMaxEmpCode(_company.Id);
                if (lstEmpCode.Trim() == rtxtEmpCode.Text)
                {
                    Alert.Show("This" + rtxtEmpCode.Text +
                               "Employee Code Name Already Taken.Now We Generate Another Employee Code.");
                }

                empinfo.EmployeeName = rtxtEmployeeName.Text;
                empinfo.Code = rtxtEmpCode.Text;
                empinfo.DateOfBirth = rdtDOB.SelectedDate == null ? PublicVariables.minDate : DateTime.Parse(rdtDOB.SelectedDate.ToString());
                empinfo.IdType = rdropIdType.SelectedIndex <= 0 ? 0 : int.Parse(rdropIdType.SelectedValue);
                empinfo.IdNo = rtxtIdNo.Text == string.Empty ? "0" : rtxtIdNo.Text;
                empinfo.BloodGroup = rdropBloodGroup.SelectedValue == "SelectOne" ? "B+" : rdropBloodGroup.Text;
                empinfo.Email = RtxtEmail.Text == string.Empty ? "" : RtxtEmail.Text;
                empinfo.Gender = rdropGender.SelectedIndex <= 0 ? 0 : int.Parse(rdropGender.SelectedValue);

                empinfo.Country = rdropCountry.SelectedIndex <= 0 ? 0 : int.Parse(rdropCountry.SelectedValue);
                empinfo.Nationality = rdropNationality.SelectedIndex <= 0 ? 0 : int.Parse(rdropNationality.SelectedValue);
                empinfo.Address = rtxtAddress.Text == string.Empty ? "" : rtxtAddress.Text;
                empinfo.PostCode = rtxtPostalCode.Text == string.Empty ? "" : rtxtPostalCode.Text;
                empinfo.Phone = rtxtPhone.Text == string.Empty ? "" : rtxtPhone.Text;
                empinfo.Mobile = rtxtMobile.Text == string.Empty ? "" : rtxtMobile.Text;
                empinfo.Photo = lblImageName.Text == string.Empty ? "" : lblImageName.Text;

                empinfo.JoinDate = rdtJoinDate.SelectedDate == null
                    ? PublicVariables.minDate
                    : DateTime.Parse(rdtJoinDate.SelectedDate.ToString());
                empinfo.DepartmentId = int.Parse(rdropDept.SelectedValue);
                empinfo.DesignationId = int.Parse(rdropDesignation.SelectedValue);
                if (rdropSupervisor.SelectedIndex != 0)
                {
                    empinfo.SupervisorId = int.Parse(rdropSupervisor.SelectedValue);
                }
                else
                {
                    empinfo.SupervisorId = 0;
                }

                empinfo.ResignationDate = rdtResignation.SelectedDate == null
                    ? PublicVariables.minDate
                    : DateTime.Parse(rdtResignation.SelectedDate.ToString());
                empinfo.CompanyId = _company.Id;
                empinfo.IsActive = chkIsActive.Checked;

                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = empinfo.InsertEmployeeInformation();

                }
                else
                {
                    empinfo.Id = int.Parse(lblId.Text);
                    success = empinfo.UpdateEmployeeInformation();
                }

                if (success == 0)
                {
                    Alert.Show("Data was not save succesfully");
                }
                else
                {
                    
                    Response.Redirect("EmployeeInfo.aspx");
                    Alert.Show("Data save succesfully");
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearEmpInfo();
        }

        protected void rdropDept_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadDesignation(int.Parse(rdropDept.SelectedValue));
        }


        protected void rdropDesignation_OnDataBound(object sender, EventArgs e)
        {
            rdropDesignation.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropSupervisor_OnDataBound(object sender, EventArgs e)
        {
            rdropSupervisor.Items.Insert(0,new RadComboBoxItem());
        }
    }
}