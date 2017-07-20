using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;

namespace SUL.SCM
{
    public partial class JournalList : System.Web.UI.Page
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

        private void LoadJournalMaster()
        {
            try
            {
                DataTable dtDeralerJournalMaster = new DealerJournalMaster().GetDealerJournalMasterFromViewList();

                if (dtDeralerJournalMaster.Rows.Count == 0)
                {
                    RadGridJournalLIst.DataSource = new string[] { };
                    RadGridJournalLIst.DataBind();
                    return;
                }
                RadGridJournalLIst.DataSource = dtDeralerJournalMaster;
                GetActiveAndInvoice(dtDeralerJournalMaster);
                RadGridJournalLIst.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load data." + ex);
            }
        }
        public static void GetActiveAndInvoice(DataTable JournalMaster)
        {
            try
            {
                DataColumn col = new DataColumn();
                DataColumn col1 = new DataColumn();

                col.ColumnName = "JournalStatus";
                col1.ColumnName = "ImageStatus";

                JournalMaster.Columns.Add(col);
                JournalMaster.Columns.Add(col1);

                foreach (DataRow dr in JournalMaster.Rows)
                {
                    if (dr["Status"].ToString().ToLower() == "created" || dr["Status"].ToString().ToLower() == "unapproved")
                    {
                        dr["JournalStatus"] = "Created";
                        dr["ImageStatus"] = "Images/Inactive.png";


                    }
                    else if (dr["Status"].ToString().ToLower() == "seen" || dr["Status"].ToString().ToLower() == "unapproved")
                    {
                        dr["JournalStatus"] = "Created";
                        dr["ImageStatus"] = "Images/Inactive.png";


                    }
                    else if ((dr["Status"].ToString().ToLower() == "approved"))
                    {
                        dr["JournalStatus"] = "Approved";
                        dr["ImageStatus"] = "Images/Active.png";

                    }

                    else if ((dr["Status"].ToString().ToLower() == "invoiced"))
                    {
                        dr["JournalStatus"] = "Created";
                        dr["ImageStatus"] = "Images/Active.png";
                        dr["InvoiceOption"] = "CreateInvoice";
                        dr["InvoiceImage"] = "Images/invoiceView.png";
                        dr["DevileryImage"] = "Images/Delivery.png";

                    }
                    //}

                    DataTable requisitionList = JournalMaster;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }


        }
        private bool IsValidPageForUser()
        {
            //int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Data Export");
            int FunctionalId = lblsource.Text == string.Empty ? new AppFunctionality().GetAppFunctionalityId("Dealer Journal List") : new AppFunctionality().GetAppFunctionalityId("Dealer Journal List", int.Parse(lblsource.Text));
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
            else
                _department = "All";
            if (!IsPostBack)
            {
                this.LoadJournalMaster();
            }
            if (IsValidPageForUser())
            {

                PermissionUser = (AppPermission)Session["PermissionUser"];
                if (!PermissionUser.IsView)
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
            }
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton linkedit = (LinkButton)sender;
                string[] Id = linkedit.CommandArgument.ToString().Split(';');
                Response.Redirect("JournalInfo.aspx?Id=" + Id[0]);
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong." + ex);
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
                        success = new DealerJournalMaster().ChangeDealerJournalStatus(id, _user.Id, "Approved");
                        if (success == 0)
                        {
                            Alert.Show("Approval failed for the Dealer Journal.");
                        }
                        else
                        {
                            DealerJournalMaster objDealerJournalMaster =
                                new DealerJournalMaster().GetDealerJournalMasterById(id);
                            List<DealerJournalDetails> lstJournalDetailses =
                                new DealerJournalDetails().GetAllDealerJournalDetailsBymasterId(id);
                            int dealerSuccess = 0;
                            if (lstJournalDetailses.Count != 0)
                            {
                                DealerLedger objDealerLedger = new DealerLedger();


                                foreach (DealerJournalDetails journalDetails in lstJournalDetailses)
                                {
                                    DealerInformation objDealerinfo =
                                  new DealerInformation().GetDealerInformationById(journalDetails.DealerId);
                                    objDealerLedger.DealerId = journalDetails.DealerId;
                                    objDealerLedger.TransactionType = "Dealer Journal";
                                    objDealerLedger.TransactionDate = DateTime.Now;
                                    objDealerLedger.SourceId = objDealerJournalMaster.Id.ToString();
                                    objDealerLedger.UserId = _user.Id;
                                    objDealerLedger.OpeningBalance = objDealerinfo.Balance;
                                    objDealerLedger.Debit = journalDetails.Debit;
                                    objDealerLedger.Cradit = journalDetails.Credit;
                                    //objDealerLedger.ClosingBalance = objDealerinfo.Balance;
                                    objDealerLedger.SourceNo = objDealerJournalMaster.JournalId;
                                    objDealerLedger.Remarks = "";

                                    dealerSuccess = objDealerLedger.InsertDealerLedger();
                                    if (dealerSuccess == 0)
                                    {
                                        Alert.Show("Dealer Ledger information is not save succesfully.");
                                    }
                                    else
                                    {
                                        DealerInformation objdinfo = new DealerInformation();

                                        objdinfo.Id = int.Parse(journalDetails.DealerId.ToString());
                                        objdinfo.TotalDebit = journalDetails.Debit;
                                        objdinfo.TotalCredit = journalDetails.Credit;

                                        int dInfosuccess = objdinfo.UpdateDealerInformationfordealerLedger();

                                        if (dInfosuccess == 0)
                                        {
                                            Alert.Show("Dealer information is not save succesfully.");
                                        }



                                        Alert.Show("The Dealer Journal is approved successfully.");
                                        this.LoadJournalMaster();
                                    }
                                }

                            }

                        }
                    }
                    else
                    {
                        Alert.Show("Dealer journal is already aproved");
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
    }
}