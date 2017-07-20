using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using PdfReport;
using PdfReport.Core;
using PdfReport.Templates;
using SUL.Bll;
using Telerik.Web.UI;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace SUL.SCM
{
    public partial class RequisitionInfoList : System.Web.UI.Page
    {
        private UserRole _role;
        private Users _user;
        private Company _company;
        private DataTable dtRequisitionMaster;

        private string _department;
        private int _source;
        private List<ListTable> statusList;
        private List<DataFilterSetup> setupList;
        private bool searchStatus;

        private bool IsValidSession()
        {
            if (Session["user"] == null)
            {
                return false;
            }

            _user = (Users)Session["user"];
            _company = (Company)Session["company"];
            _department = Session["Department"].ToString();
            _role = (UserRole)Session["Role"];

            return _user.Id != 0;
        }
        private bool IsValidPageForUser()
        {
            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Requisition List") : new AppFunctionality().GetAppFunctionalityId("Requisition List", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Requisition List") : new AppFunctionality().GetAppFunctionalityId("Requisition List", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Requisition List") : new AppFunctionality().GetAppFunctionalityId("Requisition List", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Requisition List") : new AppFunctionality().GetAppFunctionalityId("Requisition List", int.Parse(lblsource.Text));
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
        private void LoadRegion()
        {
            try
            {
                DataTable lstRegions = new Region().GetAllRegionFromView();



                rdropRegion.DataTextField = "RECode";
                rdropRegion.DataValueField = "Id";
                rdropRegion.DataSource = lstRegions;
                rdropRegion.DataBind();
                rdropRegion.SelectedIndex = lstRegions.Rows.Count == 2 ? 1 : 0;
            }

            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load region." + ex);
            }
        }
        private void LoadArea(int region)
        {
            try
            {
                DataTable lstRegions = new Area().GetAllViewAreaByRegionId(region);



                rdropArea.DataTextField = "AreaInfo";
                rdropArea.DataValueField = "Id";
                rdropArea.DataSource = lstRegions;
                rdropArea.DataBind();
                rdropArea.SelectedIndex = 0;
            }

            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load region." + ex);
            }
        }
        private void LoadDealer()
        {
            try
            {
                DataTable dtDealerInfo = new DealerInformation().GetAllDealerInformationView();

                rdropPDealer.DataTextField = "DealerInfo";
                rdropPDealer.DataValueField = "Id";
                rdropPDealer.DataSource = dtDealerInfo;
                rdropPDealer.DataBind();

                if (dtDealerInfo.Rows.Count == 2)
                    rdropPDealer.SelectedIndex = 1;
                else
                    rdropPDealer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
            }
        }

        /// <summary>
        /// Created By: Zobayer
        /// Created On: 18/05/2015
        /// Description: Load listbox to show all requisition status.
        /// </summary>
        private void LoadStatusListBox()
        {
            try
            {
                setupList = new DataFilterSetup().GetSetupByType(0, _role.Id, 1);
                statusList = new DataFilterSetup().GetApprovedStatusList(0, _role.Id, 1);
                if (statusList.Count == 0)
                {
                    setupList = new DataFilterSetup().GetSetupByType(_user.Id, 0, 1);
                    statusList = new DataFilterSetup().GetApprovedStatusList(_user.Id, 0, 1);
                }

                if (statusList.Count == 0)
                {
                    Alert.Show("Sorry this users requisition accessebility setup is done.");
                    return;
                }

                lstStatus.DataSource = statusList;
                lstStatus.DataTextField = "ListValue";
                lstStatus.DataValueField = "ListId";
                lstStatus.DataBind();

                foreach (RadListBoxItem item in lstStatus.Items)
                {
                    int id = int.Parse(item.Value);

                    DataFilterSetup setup = setupList.Find(x => x.StatusId == id);
                    if (setup.IsLoadInitially)
                        item.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        private void LoadRequisitionList()
        {
            try
            {

                StringBuilder statusCondition=new StringBuilder();
                foreach (RadListBoxItem item in lstStatus.CheckedItems)
                {
                    if (statusCondition.Length == 0)
                        statusCondition.Append(" where (status = '" + item.Text + "'");
                    else
                        statusCondition.Append(" OR status = '" + item.Text + "'"); 
                }
                statusCondition.Append(")");

                string strStatusCondition = statusCondition.ToString();

                int dealerId;
                int regionId;
                int areaId;

                DateTime prevDate = (DateTime)(dtfromDate.SelectedDate ?? DateTime.MinValue);
                DateTime todays = (DateTime)(dttoDate.SelectedDate ?? DateTime.MinValue);

                if (_department != "All")
                {
                    if (_department.ToLower().Contains("sales"))
                    {
                        RadGridRequisition.Columns[19].Visible = false; //aprpoval
                        RadGridRequisition.Columns[20].Visible = false; //delivery
                        RadGridRequisition.Columns[21].Visible = true;  //invoice print
                        RadGridRequisition.Columns[22].Visible = true;  //requisition print
                        RadGridRequisition.Columns[23].Visible = false; //challan print
                        RadGridRequisition.Columns[24].Visible = true;  //cancellation
                        RadGridRequisition.Columns[25].Visible = false;  //Rejection
                    }
                    else if (_department.ToLower().Contains("account"))
                    {
                        RadGridRequisition.Columns[19].Visible = true;
                        RadGridRequisition.Columns[20].Visible = false;
                        RadGridRequisition.Columns[21].Visible = true;
                        RadGridRequisition.Columns[22].Visible = true;
                        RadGridRequisition.Columns[23].Visible = false;
                        RadGridRequisition.Columns[24].Visible = false;
                        RadGridRequisition.Columns[25].Visible = true;  //Rejection
                    }
                    else if (_department.ToLower().Contains("logistic"))
                    {
                        RadGridRequisition.Columns[19].Visible = false;
                        RadGridRequisition.Columns[20].Visible = true;
                        RadGridRequisition.Columns[21].Visible = true;
                        RadGridRequisition.Columns[22].Visible = true;
                        RadGridRequisition.Columns[23].Visible = true;
                        RadGridRequisition.Columns[24].Visible = false;
                        RadGridRequisition.Columns[25].Visible = false;  //Rejection
                    }
                    else
                    {
                        RadGridRequisition.Columns[19].Visible = false;
                        RadGridRequisition.Columns[20].Visible = false;
                        RadGridRequisition.Columns[21].Visible = false;
                        RadGridRequisition.Columns[22].Visible = true;
                        RadGridRequisition.Columns[23].Visible = false;
                        RadGridRequisition.Columns[24].Visible = false;
                        RadGridRequisition.Columns[25].Visible = false;  //Rejection
                    }
                }

                regionId = rdropRegion.SelectedIndex <= 0 ? 0 : int.Parse(rdropRegion.SelectedValue);
                areaId = rdropArea.SelectedIndex <= 0 ? 0 : int.Parse(rdropArea.SelectedValue);
                dealerId = rdropPDealer.SelectedIndex <= 0 ? 0 : int.Parse(rdropPDealer.SelectedValue);

                dtRequisitionMaster = new RequisitionMaster().SearchRequisition(regionId, areaId, dealerId, prevDate, todays, strStatusCondition);
                if (dtRequisitionMaster.Rows.Count == 0)
                {
                    RadGridRequisition.DataSource = new string[] { };
                    RadGridRequisition.DataBind();

                    return;
                }

                GetActiveAndInvoice(dtRequisitionMaster);

                RadGridRequisition.DataSource = dtRequisitionMaster;
                RadGridRequisition.DataBind();
                lblsearchBtn.Text = "1";

                if (!IsValidUpdateForUser())
                {
                    RadGridRequisition.MasterTableView.GetColumn("colEdit").Display = false;
                }

                Session["RadGridRequisition"] = dtRequisitionMaster;





                Session["dtRequisitionMaster"] = dtRequisitionMaster;
                RadGridRequisition.DataSource = dtRequisitionMaster;
                RadGridRequisition.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to Load Requisition data." + ex);
            }
        }

        public static void GetActiveAndInvoice(DataTable RequisitionList)
        {
            try
            {
                DataColumn col = new DataColumn();
                DataColumn col1 = new DataColumn();
                DataColumn col3 = new DataColumn();
                DataColumn col4 = new DataColumn();
                DataColumn col5 = new DataColumn();
                col.ColumnName = "RequisitionStatus";
                col1.ColumnName = "ImageStatus";
                col3.ColumnName = "InvoiceOption";
                col4.ColumnName = "InvoiceImage";
                col5.ColumnName = "DevileryImage";
                RequisitionList.Columns.Add(col);
                RequisitionList.Columns.Add(col1);
                RequisitionList.Columns.Add(col3);
                RequisitionList.Columns.Add(col4);
                RequisitionList.Columns.Add(col5);

                foreach (DataRow dr in RequisitionList.Rows)
                {
                    //List<InvoiceMaster> lstInvoiceMasters =
                    //   new InvoiceMaster().GetInvoiceMasterByRequisitionId(int.Parse(dr["Id"].ToString()));

                    //if (bool.Parse(dr["IsInvoiceCreated"].ToString()) == false)
                    //{
                    //    if (dr["Status"].ToString().ToLower() == "unapproved")
                    //    {
                    //        dr["RequisitionStatus"] = "UnApproved";
                    //        dr["ImageStatus"] = "Images/Inactive.png";

                    //    }
                    //    else
                    //    {
                    //        dr["RequisitionStatus"] = "Approved";
                    //        dr["ImageStatus"] = "Images/Active.png";
                    //    }
                    //    dr["InvoiceOption"] = "CreateInvoice";
                    //    dr["InvoiceImage"] = "Images/Invoice.png";
                    //}
                    //else
                    //{
                    if (dr["Status"].ToString().ToLower() == "created" || dr["Status"].ToString().ToLower() == "unapproved")
                    {
                        dr["RequisitionStatus"] = "Created";
                        dr["ImageStatus"] = "Images/Inactive.png";
                        dr["InvoiceOption"] = "CreateInvoice";
                        dr["InvoiceImage"] = "Images/invoiceView.png";
                        dr["DevileryImage"] = "Images/Delivery.png";

                    }
                    else if (dr["Status"].ToString().ToLower() == "seen" || dr["Status"].ToString().ToLower() == "unapproved")
                    {
                        dr["RequisitionStatus"] = "Created";
                        dr["ImageStatus"] = "Images/Inactive.png";
                        dr["InvoiceOption"] = "CreateInvoice";
                        dr["InvoiceImage"] = "Images/invoiceView.png";
                        dr["DevileryImage"] = "Images/Delivery.png";

                    }
                    else if ((dr["Status"].ToString().ToLower() == "approved"))
                    {
                        dr["RequisitionStatus"] = "Approved";
                        dr["ImageStatus"] = "Images/Active.png";
                        dr["InvoiceOption"] = "CreateInvoice";
                        dr["InvoiceImage"] = "Images/invoiceView.png";
                        dr["DevileryImage"] = "Images/Delivery.png";
                    }
                    else if ((dr["Status"].ToString().ToLower() == "canceled"))
                    {
                        dr["InvoiceOption"] = "CreateInvoice";
                        dr["InvoiceImage"] = "Images/invoiceView.png";
                        dr["DevileryImage"] = "Images/Delivery.png";

                    }
                    else if ((dr["Status"].ToString().ToLower() == "invoiced"))
                    {
                        dr["RequisitionStatus"] = "Created";
                        dr["ImageStatus"] = "Images/Active.png";
                        dr["InvoiceOption"] = "CreateInvoice";
                        dr["InvoiceImage"] = "Images/invoiceView.png";
                        dr["DevileryImage"] = "Images/Delivery.png";

                    }
                    //}

                    DataTable requisitionList = RequisitionList;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }


        }
        private void LoadDealerInfo(int area)
        {
            try
            {
                DataTable dtDealerInfo = new DealerInformation().GetAllDealerInformationViewByArea(area);

                rdropPDealer.DataTextField = "DealerInfo";
                rdropPDealer.DataValueField = "Id";
                rdropPDealer.DataSource = dtDealerInfo;
                rdropPDealer.DataBind();

                if (dtDealerInfo.Rows.Count == 2)
                    rdropPDealer.SelectedIndex = 1;
                else
                    rdropPDealer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
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

            if (!IsValidPageForUser())
            {
                Alert.Show("Sorry, You Don't Have permission to access this page.");
                Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx", false);
            }

            if (_user.EmployeeId != 0)
            {
                Department objDepartment = new Department().GetEmployeeDepartment(_user.EmployeeId);
                _department = objDepartment.DepartmentName;
            }

            else
                _department = "All";

            if (Session["dtRequisitionMaster"] != null)
            {
                dtRequisitionMaster = (DataTable)Session["dtRequisitionMaster"];
            }

            if (!IsPostBack)
            {
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                lblsearchBtn.Text = "0";
                this.LoadRegion();
                this.LoadDealer();
                this.LoadStatusListBox();
                this.LoadRequisitionList();
            }
        }

        protected void RadGridRequisition_OnItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGridRequisition_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadRequisitionList();
        }

        protected void RadGridRequisition_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadRequisitionList();
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            LinkButton linkedit = (LinkButton) sender;
            string[] Id = linkedit.CommandArgument.ToString().Split(';');

            string campId = Id[1];
            if (campId == string.Empty || campId == "0")
                Response.Redirect("RequisitionInfo.aspx?Id=" + Id[0]);
            else
            {
                CampaignMaster campaign = new CampaignMaster().GetCampaignMasterById(int.Parse(campId));
                if (campaign == null || campaign.Id == 0)
                    Response.Redirect("RequisitionInfo.aspx?Id=" + Id[0]);                
                else
                {
                    if (campaign.CampaignOn.ToLower() == "value")
                        Response.Redirect("RequisitionInfo.aspx?Id=" + Id[0]);
                    else
                        Response.Redirect("CampaignRequisition.aspx?Id=" + Id[0]);
                }
            }
        }

        protected void btnDealer_OnClick(object sender, EventArgs e)
        {
            LinkButton linkedit = (LinkButton)sender;
            string[] Id = linkedit.CommandArgument.ToString().Split(';');


            Response.Redirect("DealerInfo.aspx?Id=" + Id[0]);
        }

        protected void btnInvoice_OnClick(object sender, EventArgs e)
        {
            int success;
            int IsinvoiceCreate;
            try
            {
                LinkButton linkedit = (LinkButton)sender;
                string[] requisitionId = linkedit.CommandArgument.ToString().Split(';');

                bool _isInvoiceActive = bool.Parse(requisitionId[1].ToString());
                RequisitionMaster objRequisitionMaster = new RequisitionMaster().GetRequisitionMasterById(int.Parse(requisitionId[0].ToString()));
                InvoiceMaster invMaster = new InvoiceMaster();
                if (_isInvoiceActive == false)
                {
                    invMaster.InvoiceNo = invMaster.GetlastInvoiceCode();
                    invMaster.InvoiceDate = DateTime.Now;
                    invMaster.DealerId = int.Parse(objRequisitionMaster.DealerId.ToString());
                    invMaster.RequisitionId = int.Parse(requisitionId[0].ToString());
                    invMaster.UserId = _user.Id;
                    IsinvoiceCreate = new RequisitionMaster().SetIncoiceActiveStatus(int.Parse(requisitionId[0].ToString()), _user.Id, true);

                    success = invMaster.InsertInvoiceMaster();
                }
                else
                {
                    InvoiceMaster objInvoiceMaster = new InvoiceMaster().invoiceMasterDetails(int.Parse(requisitionId[0].ToString()));
                    Response.Redirect("InvoiceView.aspx?Id=" + objInvoiceMaster.Id);
                    return;
                }
                if (success == 0)
                {
                    Alert.Show("Data is not save succesfully.");
                }
                else
                {
                    decimal p1 = 0;
                    decimal p2 = 0;
                    decimal Dis = 0;

                    List<RequisitionDetails> lsRequisitionDetails =
                        new RequisitionDetails().GetAllRequisitionDetailsBymasterId(int.Parse(requisitionId[0].ToString()));
                    if (lsRequisitionDetails.Count != 0)
                    {
                        foreach (RequisitionDetails requisitionDetails in lsRequisitionDetails)
                        {
                            InvoiceDetails objInvoiceDetails = new InvoiceDetails();

                            objInvoiceDetails.MasterId = invMaster.GetMaxInvoiceMasterId();
                            objInvoiceDetails.CategoryId = requisitionDetails.CategoryId;
                            objInvoiceDetails.ProductId = requisitionDetails.ProductId;
                            objInvoiceDetails.Quantity = requisitionDetails.Quantity;
                            objInvoiceDetails.Price = requisitionDetails.Price;
                            p1 += requisitionDetails.Price * requisitionDetails.Quantity;

                            objInvoiceDetails.Unit = requisitionDetails.Unit;
                            objInvoiceDetails.Discount = requisitionDetails.Discount;
                            Dis += requisitionDetails.Discount;

                            objInvoiceDetails.Color = requisitionDetails.Color;
                            objInvoiceDetails.Price2 = requisitionDetails.Price2;
                            p2 += requisitionDetails.Price2 * requisitionDetails.Quantity;
                            success = objInvoiceDetails.InsertInvoiceDetails();
                            if (success == 0)
                            {
                                Alert.Show("Invoice details Data is not save succesfully");
                            }
                            else
                            {
                                Alert.Show("Data save succesfully");
                            }
                        }

                        int update;
                        InvoiceMaster objIMaster = new InvoiceMaster();

                        objIMaster.Id = invMaster.GetMaxInvoiceMasterId();
                        objIMaster.ItemTotal = p1;
                        objIMaster.ItemTotal2 = p2;
                        objIMaster.Discount = Dis;

                        update = objIMaster.UpdateInvoiceMasterInformation();

                        if (update == 0)
                        {
                            Alert.Show("Invoice Master not update!!!");
                        }
                        else
                        {
                            int InvoiceId = invMaster.GetMaxInvoiceMasterId();

                            invMaster = new InvoiceMaster().GetInvoiceMasterById(InvoiceId);
                            DealerLedger dealerLedger = new DealerLedger();
                            DealerInformation objDealerinfo = new DealerInformation().GetDealerInformationById(int.Parse(objRequisitionMaster.DealerId.ToString()));


                            dealerLedger.DealerId = int.Parse(objRequisitionMaster.DealerId.ToString());
                            dealerLedger.TransactionType = "Dealer";
                            dealerLedger.TransactionDate = DateTime.Now;
                            dealerLedger.SourceId = invMaster.Id.ToString();
                            dealerLedger.UserId = _user.Id;
                            dealerLedger.OpeningBalance = objDealerinfo.Balance;
                            dealerLedger.Debit = invMaster.InvoiceTotal;
                            dealerLedger.Cradit = 0;
                            dealerLedger.SourceNo = invMaster.InvoiceNo;

                            int dealersuccess = dealerLedger.InsertDealerLedger();

                            if (dealersuccess == 0)
                            {
                                Alert.Show("Dealer Ledger information is not save succesfully.");
                            }
                            else
                            {
                                DealerInformation objdinfo = new DealerInformation();

                                objdinfo.Id = int.Parse(objRequisitionMaster.DealerId.ToString());
                                objdinfo.TotalDebit = invMaster.InvoiceTotal;
                                objdinfo.TotalCredit = 0;

                                int dInfosuccess = objdinfo.UpdateDealerInformationfordealerLedger();

                                if (dInfosuccess == 0)
                                {
                                    Alert.Show("Dealer information is not save succesfully.");
                                }
                            }

                            this.LoadRequisitionList();
                        }

                    }
                }
                this.LoadRequisitionList();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to create invoice" + ex);
            }
        }

        protected void lnkStatus_OnClick(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    LinkButton lnkStatusEdit = (LinkButton)sender;
                    string[] commandArguments = lnkStatusEdit.CommandArgument.ToString().Split(';');

                    int id = int.Parse(commandArguments[0].ToString());
                    string status = commandArguments[1].ToString();
                    int success = 0;
                    if (status.ToLower() == "unapproved" || status.ToLower() == "created" || status.ToLower() == "seen")
                    {
                        success = new RequisitionMaster().ChangeRequisitionStatus(id, _user.Id,"Approved");
                        if (success == 0)
                        {
                            Alert.Show("Approval failed for the requisition.");
                        }
                        else
                        {
                            Alert.Show("The requisition is approved successfully.");
                            this.LoadRequisitionList();
                        }
                    }
                    else if (status.ToLower() == "approved")
                    {
                        success = new RequisitionMaster().ChangeRequisitionStatus(id, _user.Id, "UnApproved");
                        if (success == 0)
                        {
                            Alert.Show("Up approval failed for the requisition.");
                        }
                        else
                        {
                            Alert.Show("The requisition was un approved successfully.");
                            this.LoadRequisitionList();
                        }
                    }
                    else if (status.ToLower() == "invoiced" || status.ToLower() == "delivered")
                    {
                        Alert.Show("Sorry you cannot change status of this requisition now. This requisition is already been invoiced and delivered.");
                    }
                    else
                    {
                        Alert.Show("This requisition is already canceled");
                    }

                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }


        protected void btnDelivery_OnClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton linkedit = (LinkButton)sender;
                string[] requisitionId = linkedit.CommandArgument.ToString().Split(';');
                int reqId = int.Parse(requisitionId[0]);

                RequisitionMaster objRequisition = new RequisitionMaster().GetRequisitionMasterById(reqId);
                if (objRequisition.Status.ToLower() == "approved")
                {
                    Session["url"] = HttpContext.Current.Request.Url.ToString();
                    Response.Redirect("DeliveryInformation.aspx?Id=" + requisitionId[0],true);
                    
                }
                else
                {
                    Alert.Show("Sorry the current status of this requistion is " + objRequisition.Status +
                               ". You cannot deliver this requisition now.");
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }

        }

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            try
            {
                string logoPath = Server.MapPath("Images") + @"\Elite Tec logo-01.png";

                LinkButton linkedit = (LinkButton)sender;
                string[] text = linkedit.CommandArgument.ToString().Split(';');

                int requisitionId = int.Parse(text[0].ToString());

                MemoryStream pdfData = PrintInvoice.Print(requisitionId, logoPath);

                if (pdfData == null) return;

                Session["StreamData"] = pdfData;
                Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnPrintAll_OnClick(object sender, EventArgs e)
        {
            try
            {
                Company company = new Company().GetParentCompany();
                ReportHeader header = new ReportHeader()
                {
                    CompanyName = company.CompanyName,
                    Address = company.Address,
                    ReportTitle = "Requisition List Report",
                    LogoPath = ""
                };
                List<DetailStructure> detailDataList = new List<DetailStructure>()
                {
                    new DetailStructure
                    {
                        Slno = 1, FieldName = "DealerCode", DataType = "string", Caption = "Dealer Code",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 2, FieldName = "Address", DataType = "String", Caption = "Address",
                        FieldWidth = 110f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "Val", DataType = "string", Caption = "Courier Name",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 4, FieldName = "RequisitionDate", DataType = "DateTime", Caption = "Requisition Date",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 5, FieldName = "EmployeeName", DataType = "string", Caption = "Employee Name",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 6, FieldName = "Status", DataType = "string", Caption = "Status",
                        FieldWidth = 80f, Align = Alignments.ALIGN_LEFT
                    }
                   
                };


                float[] widths = new float[] { 100f, 110f, 100f, 100f, 110f, 80f };

                ReportDetail reportDetail = new ReportDetail(detailDataList, dtRequisitionMaster, 9, "English", widths,
                    "Verdana", false);

                PdfDocument doc = new PdfDocument("Requisition List", PageSizes.A4, "Landscape", "English")
                {
                    Header = header,
                    Details = reportDetail,
                    ReportTemplate = PdfDocument.ReportType.Basic
                };
                doc.PageHeader = reportDetail.GetPageHeader(doc.ReportWidth);
                doc.ReportTemplate = PdfDocument.ReportType.Basic;
                MemoryStream pdfData = doc.WriteToStream();

                Session["StreamData"] = pdfData;
                Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }

        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            this.LoadRequisitionList();
        }

        protected void rdropPDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropPDealer.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropRegion_OnDataBound(object sender, EventArgs e)
        {
            rdropRegion.Items.Insert(0, new ListItem());
        }

        protected void rdropRegion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadArea(int.Parse(rdropRegion.SelectedValue));
        }

        protected void rdropArea_OnDataBound(object sender, EventArgs e)
        {
            rdropArea.Items.Insert(0, new ListItem());
        }

        protected void rdropArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadDealerInfo(int.Parse(rdropArea.SelectedValue));
        }

        protected void btnPrintChallan_OnClick(object sender, EventArgs e)
        {
            try
            {
                string logoPath = Server.MapPath("Images") + @"\Elite Tec logo-01.png";

                LinkButton linkedit = (LinkButton)sender;
                string[] text = linkedit.CommandArgument.ToString().Split(';');

                int requisitionId = int.Parse(text[0].ToString());

                MemoryStream pdfData = PrintChannal.Print(requisitionId, logoPath);

                if (pdfData == null) return;

                Session["StreamData"] = pdfData;
                Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnPrintRequsition_OnClick(object sender, EventArgs e)
        {
            try
            {
                string logoPath = Server.MapPath("Images") + @"\Elite Tec logo-01.png";

                LinkButton linkedit = (LinkButton)sender;
                string[] text = linkedit.CommandArgument.ToString().Split(';');

                int requisitionId = int.Parse(text[0].ToString());

                MemoryStream pdfData = PrintRequisition.Print(requisitionId, logoPath);

                if (pdfData == null) return;

                Session["StreamData"] = pdfData;
                Response.Write("<script type='text/javascript'> window.open('ReportViewer.aspx','_blank', 'height=' + screen.height + ',width=' + screen.width + ',resizable=yes,scrollbars=yes,toolbar=yes,menubar=yes,location=yes'); </script>");
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    LinkButton linkedit = (LinkButton)sender;
                    string[] Id = linkedit.CommandArgument.ToString().Split(';');
                    RequisitionMaster objMaster = new RequisitionMaster().GetRequisitionMasterById(int.Parse(Id[0]));

                    if (objMaster.Status.ToLower() == "created" || objMaster.Status.ToLower() == "seen" || objMaster.Status.ToLower() == "unapproved")
                    {
                        Response.Redirect("RequisitionCancel.aspx?Id=" + Id[0]); 
                    }
                    else
                    {
                        Alert.Show("Sorry the current status of this requistion is " + objMaster.Status +
                               ". You cannot cancel this requisition now.");
                    }
                }
                else
                {
                    Alert.Show("Your Action Is Not Done !!!");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Error Occur " + ex.Message);
            }


        }

        protected void btnReject_OnClick(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    LinkButton linkedit = (LinkButton)sender;
                    string[] Id = linkedit.CommandArgument.ToString().Split(';');
                    RequisitionMaster objMaster = new RequisitionMaster().GetRequisitionMasterById(int.Parse(Id[0]));

                    if (objMaster.Status.ToLower() == "created" || objMaster.Status.ToLower() == "seen" || objMaster.Status.ToLower() == "unapproved")
                    {
                        Response.Redirect("RequisitionCancel.aspx?Id=" + Id[0]);
                    }
                    else
                    {
                        Alert.Show("Sorry the current status of this requistion is" + objMaster.Status +
                               ". You cannot reject this requisition now.");
                    }
                }
                else
                {
                    Alert.Show("Your Action Is Not Done !!!");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Error Occur " + ex.Message);
            }


        }
        protected void RadGridRequisition_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    string status = item["colIsActive"].Text;

                    if (status.ToLower() == "created")
                    {
                        item["colDelivery"].Enabled = false;
                        item["colPrintInvoice"].Enabled = false;
                        item["colPrintChallan"].Enabled = false;
                        item["colCancel"].Enabled = true;
                        item["colEdit"].Enabled = true;
                    }
                    else if (status.ToLower() == "seen")
                    {
                        item["colDelivery"].Enabled = false;
                        item["colPrintInvoice"].Enabled = false;
                        item["colPrintChallan"].Enabled = false;
                        item["colCancel"].Enabled = true;
                        item["colEdit"].Enabled = true;
                    }
                    else if (status.ToLower() == "approved")
                    {
                        item["colDelivery"].Enabled = true;
                        item["colPrintInvoice"].Enabled = false;
                        item["colPrintChallan"].Enabled = false;
                        item["colCancel"].Enabled = false;
                        item["colEdit"].Enabled = true;
                    }
                    else if (status.ToLower() == "cancelled" || status.ToLower() == "rejected")
                    {
                        item["colDelivery"].Enabled = false;
                        item["colPrintInvoice"].Enabled = false;
                        item["colPrintChallan"].Enabled = false;
                        item["colCancel"].Enabled = false;
                        item["colEdit"].Enabled = true;
                    }
                    else if (status.ToLower() == "delivered" || status.ToLower() == "invoiced")
                    {
                        item["colDelivery"].Enabled = true;
                        item["colPrintInvoice"].Enabled = true;
                        item["colPrintChallan"].Enabled = true;
                        item["colCancel"].Enabled = false;
                        item["colEdit"].Enabled = true;

                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }

        protected void btntest_OnClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            DataTable dt = dtRequisitionMaster.Copy();

            ExportToExcel(dt);
        }

        public void ExportToExcel(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {

                    string attachment = "attachment; filename=RequisitionHistory.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";

                    string tab = "";

                    Response.ContentEncoding = System.Text.Encoding.Unicode;
                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


                    StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    #region ReportHeader

                    if (dtfromDate.SelectedDate != null)
                    {
                        Response.Write("From Date");
                        tab = "\t";
                        Response.Write(tab + dtfromDate.SelectedDate);
                        Response.Write("\n");
                    }
                    if (dttoDate.SelectedDate != null)
                    {
                        Response.Write("From Date");
                        tab = "\t";
                        Response.Write(tab + dttoDate.SelectedDate);
                        Response.Write("\n");
                    }
                    if (rdropPDealer.SelectedIndex > 0)
                    {
                        Response.Write("\n");
                        Response.Write("To Date");
                        tab = "\t";
                        Response.Write(tab + rdropPDealer.SelectedItem.Text);
                        Response.Write("\n");
                    }
                    #endregion

                    #region ReportSummeryForDealer
                    decimal TQ = 0;
                    decimal T = 0;
                    decimal Dis = 0;

                    Response.Write("\n");
                    Response.Write("\n");
                    Response.Write("Requisition History");
                    Response.Write("\n");
                    //foreach (DataColumn dc in dt.Columns)
                    //{
                    Response.Write(sw.ToString());
                    Response.Write("Requisition Code");
                    tab = "\t";
                    Response.Write(tab + "Requisition Date");
                    tab = "\t";
                    Response.Write(tab + "Dealer Code");
                    tab = "\t";
                    Response.Write(tab + "Dealer Name");
                    tab = "\t";
                    Response.Write(tab + "Total Amount");
                    tab = "\t";
                    Response.Write(tab + "Payment Amount");
                    tab = "\t";
                    Response.Write(tab + "Reference No");
                    tab = "\t";
                    Response.Write(tab + "Employee Name");
                    tab = "\t";
                    Response.Write(tab + "Update By");
                    tab = "\t";
                    Response.Write(tab + "Status");
                    tab = "\t";
                    Response.Write(tab + "Remarks");
                    tab = "\t";

                    Response.Write("\n");

                    foreach (DataRow dr in dt.Rows)
                    {
                        tab = "";

                        Response.Write(tab + dr["RequisitionCode"].ToString());
                        tab = "\t";
                        DateTime Rdt = DateTime.Parse(dr["RequisitionDate"].ToString());

                        Response.Write(tab + Rdt.ToString("dd/MM/yyyy"));
                        tab = "\t";
                        Response.Write(tab + dr["DealerCode"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["DealerName"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["RequistionTotal"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["PaymentAmount"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["ReferenceNo"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["EmployeeName"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["UpdateBy"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Status"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Remarks"].ToString());
                        tab = "\t";
                        

                        Response.Write("\n");
                    }
                    Response.End();
                    Response.Flush();
                    sw.Close();
                    sw.Dispose();
                    
                }

                    #endregion

            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }

        }
    }
}