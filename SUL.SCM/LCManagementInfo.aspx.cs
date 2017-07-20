using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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
    public partial class LCManagementInfo : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
        private  Company _company;
        //private  bool isNewEntry;
        private  List<TempLCAmendment> tempLcAmendment;

        private class TempLCAmendment
        {
            public int Id { get; set; }
            public string AmendmentNumber { get; set; }
            public DateTime AmendementDate { get; set; }
            public string AmendmentDescription { get; set; }
        }

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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("LC Management Add");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("LC Management Add");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("LC Management Add");
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("LC Management Add");
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
        private void LoadLCAmendment()
        {
            try
            {
                if (tempLcAmendment.Count == 0)
                {
                    RadGridAddRequisitionDetails.DataSource = new string[] { };
                    return;
                }

                RadGridAddRequisitionDetails.DataSource = tempLcAmendment;
                RadGridAddRequisitionDetails.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load requisition details grid." + ex);
            }
        }
        private void LoadRequisitionDataFromDatabase(int id)
        {
            try
            {

                List<LCAmendment> lstAmendments =
                    new LCAmendment().GetAllLCAmendmentbyLCId(id);

                if (lstAmendments.Count > 0)
                {
                    tempLcAmendment = new List<TempLCAmendment>();
                    foreach (LCAmendment lstLCAme in lstAmendments)
                    {
                        TempLCAmendment tmpLcAmendment = new TempLCAmendment();

                        tmpLcAmendment.Id = int.Parse(lstLCAme.Id.ToString());
                        tmpLcAmendment.AmendmentNumber = lstLCAme.AmendmentNumber;


                        tmpLcAmendment.AmendementDate = lstLCAme.AmendementDate;

                        tmpLcAmendment.AmendmentDescription = lstLCAme.AmendmentDescription;
                        tempLcAmendment.Add(tmpLcAmendment);
                    }

                    Session["tempLcAmendment"] = tempLcAmendment;

                    LoadLCAmendment();
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Requisition details data." + ex);
            }
        }
        private void LoadVendorName()
        {
            try
            {
                List<Supplier> lstSuppliers = new Supplier().GetAllSupplier();
                lstSuppliers.Insert(0, new Supplier());

                rdropVandorName.DataTextField = "Name";
                rdropVandorName.DataValueField = "Id";
                rdropVandorName.DataSource = lstSuppliers;
                rdropVandorName.DataBind();

                rdropVandorName.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load vendor name.");
            }
        }
        private void LoadVandorbank(int supId)
        {
            try
            {
                List<BankInformation> lstbaBankInformations =
                    new BankInformation().GetBankInformationBySupplier(supId,
                        "Supplier");

                lstbaBankInformations.Insert(0, new BankInformation());

                rdropNegotiatingBank.DataTextField = "BankName";
                rdropNegotiatingBank.DataValueField = "Id";
                rdropNegotiatingBank.DataSource = lstbaBankInformations;
                rdropNegotiatingBank.DataBind();


            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to Load Vendor bank." + ex);
            }
        }

        private void LoadCompanyBank()
        {
            try
            {
                List<BankInformation> lstbaBankInformations =
                    new BankInformation().GetBankInformationBySupplier(_company.Id,
                        "Company");

                lstbaBankInformations.Insert(0, new BankInformation());

                rdropIssuingBank.DataTextField = "BankName";
                rdropIssuingBank.DataValueField = "Id";
                rdropIssuingBank.DataSource = lstbaBankInformations;
                rdropIssuingBank.DataBind();

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Company bank." + ex);
            }
        }

        private void LoadPIInfo(int supId)
        {
            try
            {
                List<PIMaster> lstPIMasters =
                    new PIMaster().GetAllPIMasterbyVendorId(supId);
                lstPIMasters.Insert(0, new PIMaster());

                rdropPINo.DataTextField = "PINo";
                rdropPINo.DataValueField = "Id";
                rdropPINo.DataSource = lstPIMasters;
                rdropPINo.DataBind();

                rdropPINo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to Load P.I. info . " + ex);
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

            if (Session["tempLcAmendment"] != null)
                tempLcAmendment = (List<TempLCAmendment>) Session["tempLcAmendment"];
            else
                tempLcAmendment = new List<TempLCAmendment>();
            if (!IsPostBack)
            {
                lblisNewEntry.Text = "true";

                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                if (!IsValidUpdateForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                this.LoadVendorName();
                this.LoadCompanyBank();
                tempLcAmendment = new List<TempLCAmendment>();
                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];

                    LCInformation objLcInformation = new LCInformation().GetLCInformationById(int.Parse(id));

                    lblId.Text = objLcInformation.Id.ToString();
                    rtxtVendorAddress.Text = objLcInformation.VendorAddress;
                    rdropVandorName.SelectedValue = objLcInformation.VendorId.ToString();

                    this.LoadPIInfo(int.Parse(rdropVandorName.SelectedValue));
                    this.LoadVandorbank(int.Parse(rdropVandorName.SelectedValue));

                    rdropPINo.SelectedValue = objLcInformation.PINo.ToString();
                    rtxtLcNumber.Text = objLcInformation.LCNumber.ToString();
                    rdtLcDate.SelectedDate = objLcInformation.LCDate;
                    rdtLcExpDate.SelectedDate = objLcInformation.LCExpiryDate;
                    rtxtLCValue.Text = objLcInformation.LCValue.ToString();
                    rdropLCStatus.SelectedValue = objLcInformation.LCStatus;
                    rdropIssuingBank.SelectedValue = objLcInformation.IssusingBank.ToString();
                    rdropNegotiatingBank.SelectedValue = objLcInformation.NegotiatingBank.ToString();
                    rtxtFileName.Text = objLcInformation.FileName;
                    this.LoadRequisitionDataFromDatabase(int.Parse(id));
                    lblisNewEntry.Text = "false";

                }

            }
        }

        protected void rdropVandorName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadPIInfo(int.Parse(rdropVandorName.SelectedValue));
            this.LoadVandorbank(int.Parse(rdropVandorName.SelectedValue));
            this.LoadCompanyBank();

            Supplier objsuoSupplier = new Supplier().GetSupplierById(int.Parse(rdropVandorName.SelectedValue));
            rtxtVendorAddress.Text = objsuoSupplier.CompanyAddress;

            if (bool.Parse(lblisNewEntry.Text))
            {
                rdropLCStatus.SelectedValue = "lcopen";
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            #region validation

            if (rdropVandorName.SelectedIndex <= 0)
            {
                Alert.Show("Please select Vendor name.");
                rdropVandorName.Focus();
                return;
            }
            if (rtxtVendorAddress.Text == string.Empty)
            {
                Alert.Show("Enter a Vendor address");
                rtxtVendorAddress.Focus();
                return;
            }
            if (rdropPINo.SelectedIndex <= 0)
            {
                Alert.Show("Please select PI No.");
                rdropPINo.Focus();
                return;
            }
            if (rtxtLcNumber.Text == string.Empty)
            {
                Alert.Show("Enter a Lc Number.");
                rtxtLcNumber.Focus();
                return;
            }
            if (rdtLcDate.SelectedDate == null)
            {
                Alert.Show("Please select a L/C Date.");
                rdtLcDate.Focus();
                return;
            }

            if (rdtLcExpDate.SelectedDate == null)
            {
                Alert.Show("Please select a L/C Expiry Date.");
                rdtLcExpDate.Focus();
                return;
            }
            if (rdropLCStatus.SelectedIndex <= 0)
            {
                Alert.Show("Please select a L/C status.");
                rdropLCStatus.Focus();
                return;

            }

            //check if lc number already exist.
            int id = (lblId.Text == string.Empty) ? 0 : int.Parse(lblId.Text);
            int count = new LCInformation().IsLCNoExist(rtxtLcNumber.Text, id);
            
            if (count > 0)
            {
                Alert.Show("LC number already exist.");
                rtxtLcNumber.Focus();
                return;
            }

            #endregion

            try
            {
                if (ScanedCopy.HasFile)
                {
                    string LCNo = rtxtLcNumber.Text == string.Empty ? "Empty" : rtxtLcNumber.Text;
                    string extension = Path.GetExtension(ScanedCopy.FileName);
                    string fileName = LCNo + "_Image" + extension;
                    lblImageName.Text = fileName;

                    DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/images/L.CFile") + LCNo);
                    if (!directory.Exists)
                        directory.Create();

                    string UploadFilePath = Server.MapPath("~/images/L.CFile") + LCNo + "/" +
                                            fileName;
                    FileInfo file = new FileInfo(UploadFilePath);

                    if (!file.Exists)
                        file.Delete();

                    ScanedCopy.SaveAs(UploadFilePath);

                }
                LCInformation objLcInformation = new LCInformation();

                objLcInformation.VendorId = int.Parse(rdropVandorName.SelectedValue);
                objLcInformation.VendorName = rdropVandorName.SelectedItem.Text;
                objLcInformation.VendorAddress = rtxtVendorAddress.Text;
                objLcInformation.PINo = int.Parse(rdropPINo.SelectedValue);
                objLcInformation.LCNumber = rtxtLcNumber.Text;
                objLcInformation.LCDate = DateTime.Parse(rdtLcDate.SelectedDate.ToString());
                objLcInformation.LCExpiryDate = DateTime.Parse(rdtLcExpDate.SelectedDate.ToString());
                objLcInformation.LCValue = Decimal.Parse(rtxtLCValue.Text);
                objLcInformation.LCStatus = rdropLCStatus.SelectedValue;
                objLcInformation.IssusingBank = int.Parse(rdropIssuingBank.SelectedValue);
                objLcInformation.NegotiatingBank = rdropNegotiatingBank.SelectedIndex == 0 ? 0 : int.Parse(rdropNegotiatingBank.SelectedValue);
                objLcInformation.FileName = rtxtFileName.Text;
                objLcInformation.FileLocation = lblImageName.Text;

                int success;

                if (bool.Parse(lblisNewEntry.Text))
                {
                    success = objLcInformation.InsertLCInformation();
                }
                else
                {
                    objLcInformation.Id = int.Parse(lblId.Text);
                    success = objLcInformation.UpdateLCInformation();
                }

                if (success == 0)
                    Alert.Show("Data is not save succesfully");
                else
                {
                    if (tempLcAmendment.Count != 0)
                    {

                        int lcid = 0;

                        foreach (TempLCAmendment tmpAmendment in tempLcAmendment)
                        {
                            LCAmendment objLcAmendment = new LCAmendment();

                            if (lblId.Text == string.Empty)
                            {
                                lcid = new LCInformation().GetMaxLCMasterId();
                            }
                            else
                            {
                                lcid = int.Parse(lblId.Text);
                            }
                            lblAmmId.Text = tmpAmendment.Id.ToString();
                            objLcAmendment.AmendementDate = tmpAmendment.AmendementDate;
                            objLcAmendment.AmendmentNumber = tmpAmendment.AmendmentNumber;
                            objLcAmendment.AmendmentDescription = tmpAmendment.AmendmentDescription;
                            objLcAmendment.LCId = lcid;

                            List<LCAmendment> lstLcAmendments = new LCAmendment().GetAllLCAmendmentbyLCIdAndNumber(lcid, objLcAmendment.AmendmentNumber);
                            if (lstLcAmendments.Count == 0)
                            {
                                success = objLcAmendment.InsertLCAmendment();
                            }
                            else
                            {
                                objLcAmendment.Id = tmpAmendment.Id;
                                success = objLcAmendment.UpdateLCAmendment();
                            }

                            if (success == 0)
                            {
                                Alert.Show("LC Data Data is not save succesfully..");
                            }
                        }
                        this.LoadRequisitionDataFromDatabase(int.Parse(lblId.Text));
                    }
                    Alert.Show("L/C Data Saved succesfully");
                    ClearAllInfo();
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save Lc Information" + ex);
            }
        }

        private void ClearAllInfo()
        {
            rdropVandorName.SelectedIndex = 0;

            rtxtVendorAddress.Text = "";
            rdropPINo.SelectedIndex = 0;
            rtxtLcNumber.Text = "";
            rdtLcDate.SelectedDate = null;
            rdtLcExpDate.SelectedDate = null;
            rtxtLCValue.Text = "";
            rdropLCStatus.SelectedIndex = 0;
            rdropIssuingBank.SelectedIndex = 0;
            rdropNegotiatingBank.SelectedIndex = 0;
            rtxtFileName.Text = "";
            lblImageName.Text = "";
            rtxtAmmendmentNo.Text = "";
            rdtAmmendementDate.SelectedDate = null;
            rtxtAmmendmentDesc.Value = null;
            
            lblisNewEntry.Text = "true";

            Session["tempLcAmendment"] = new List<TempLCAmendment>();
            Response.Redirect("LCManagementInfo.aspx");
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            ClearAllInfo();
        }

        protected void btnAddLCAmendment_OnClick(object sender, EventArgs e)
        {
            try
            {
                TempLCAmendment objLcAmendment = tempLcAmendment.Find(x => x.AmendmentNumber == rtxtAmmendmentNo.Text);
                if (objLcAmendment != null)
                {
                    if (objLcAmendment.AmendmentNumber == string.Empty)
                        objLcAmendment = new TempLCAmendment();
                    else
                    {
                        tempLcAmendment.Remove(objLcAmendment);
                    }
                }
                else
                {
                    objLcAmendment=new TempLCAmendment();
                }
                objLcAmendment.AmendmentNumber = rtxtAmmendmentNo.Text;
                objLcAmendment.AmendementDate = DateTime.Parse(rdtAmmendementDate.SelectedDate.ToString());
                objLcAmendment.AmendmentDescription = rtxtAmmendmentDesc.Value;

                if (tempLcAmendment.Count == 0)
                    tempLcAmendment = new List<TempLCAmendment>();
  
                tempLcAmendment.Add(objLcAmendment);
                Session["tempLcAmendment"] = tempLcAmendment;
                this.LoadLCAmendment();
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
                throw;
            }
        }

        protected void RadGridAddRequisitionDetails_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnSelect")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    lblDetails.Text = item["colId"].Text;
                    rtxtAmmendmentNo.Text = item["colAmendmentNumber"].Text;
                    rtxtAmmendmentDesc.Value = item["colAmendmentDescription"].Text;
                    rdtAmmendementDate.SelectedDate = DateTime.Parse(item["colAmendementDate"].Text);
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to when selecting the details." + ex);
            }
        }
    }
}