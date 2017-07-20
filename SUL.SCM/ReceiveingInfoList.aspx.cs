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
    public partial class ReceiveingInfoList : System.Web.UI.Page
    {
        private  UserRoleInfo _role;
        private  Users _user;
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

            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Receiveing List") : new AppFunctionality().GetAppFunctionalityId("Receiveing List", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Receiveing List") : new AppFunctionality().GetAppFunctionalityId("Receiveing List", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Receiveing List") : new AppFunctionality().GetAppFunctionalityId("Receiveing List", int.Parse(lblsource.Text));
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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Receiveing List") : new AppFunctionality().GetAppFunctionalityId("Receiveing List", int.Parse(lblsource.Text));
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

        private void LoadReceiverGrid()
        {
            try
            {
                DataTable dtReceiverMaster = new ReceiverMaster().GetReceiverFromViewList(_company.Id);
                if (dtReceiverMaster.Rows.Count == 0)
                {
                    RadGridReceiverMaster.DataSource = new string[] {};
                    return;
                }
                RadGridReceiverMaster.DataSource = dtReceiverMaster;
                RadGridReceiverMaster.DataBind();

                if (!IsValidUpdateForUser())
                {
                    RadGridReceiverMaster.MasterTableView.GetColumn("colEdit").Display = false;
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Receiver grid"+ex);
            }
        }

        public static void GetActiveInvoice(DataTable ReceivingList)
        {
            try
            {
                DataColumn col1 = new DataColumn();
                DataColumn col3 = new DataColumn();
                col3.ColumnName = "InvoiceOption";
                col1.ColumnName = "InvoiceImage";
                ReceivingList.Columns.Add(col1);
                ReceivingList.Columns.Add(col3);

                foreach (DataRow dr in ReceivingList.Rows)
                {
                    if (bool.Parse(dr["IsInvoiceCreate"].ToString()) == false)
                    {
                        dr["InvoiceOption"] = "CreateInvoice";
                        dr["InvoiceImage"] = "Images/Invoice.png";

                    }
                    else
                    {
                        dr["InvoiceOption"] = "CreatedInvoice";
                        dr["InvoiceImage"] = "Images/invoiceView.png";
                    }

                    DataTable receivingList = ReceivingList;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            } 
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblsource.Text = Request.QueryString["source"] != null ? Request.QueryString["source"] : "0";


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
                this.LoadReceiverGrid();
            }
        }

        protected void RadGridReceiverMaster_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadReceiverGrid();
        }

        protected void RadGridReceiverMaster_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadReceiverGrid();
        }

        protected void RadGridReceiverMaster_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            
        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            LinkButton linkedit = (LinkButton)sender;
            string[] Id = linkedit.CommandArgument.ToString().Split(';');


            Response.Redirect("ReceiveingInfo.aspx?Id=" + Id[0]);
        }

        protected void btnInvoice_OnClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton linkedit = (LinkButton)sender;
                string[] Id = linkedit.CommandArgument.ToString().Split(';');
                int id = int.Parse(Id[0]);
                bool _isInvoiceActive = bool.Parse(Id[1].ToString());

                if (_isInvoiceActive == false)
                {
                    ReceiverMaster objReceiverMaster = new ReceiverMaster().GetReceiverMasterById(id, _company.Id);
                    Supplier obSupplier = new Supplier().GetSupplierById(objReceiverMaster.VendorId);
                    SupplierLedger objSupplierLedger = new SupplierLedger();

                    objSupplierLedger.SupplierId = objReceiverMaster.VendorId;
                    objSupplierLedger.TransactionType = "Receiving";
                    objSupplierLedger.TransactionDate = DateTime.Now;
                    objSupplierLedger.SourceId = objReceiverMaster.ReceivingCode;
                    objSupplierLedger.UserId = _user.Id;
                    objSupplierLedger.OpeningBalance = obSupplier.Balance;
                    objSupplierLedger.Debit = 0;
                    objSupplierLedger.Cradit = objReceiverMaster.TotalAmount;

                    int supSuccess;
                    int IsinvoiceCreate = new ReceiverMaster().SetIncoiceActiveStatus(id, true);
                    supSuccess = objSupplierLedger.InsertSupplierLedger();
                    if (supSuccess == 0)
                    {
                        Alert.Show("Something is going wrong to save supplier ledger.");
                    }
                    else
                    {
                        Supplier objSupplier = new Supplier();

                        objSupplier.Id = objReceiverMaster.VendorId;
                        objSupplier.TotalDebit = 0;
                        objSupplier.TotalCredit = objReceiverMaster.TotalAmount;
                        

                        int sInfosuccess = objSupplier.UpdateSupplierInformationfordealerLedger();

                        if (sInfosuccess == 0)
                        {
                            Alert.Show("Supplier information is not save succesfully.");
                        }
                        else
                        {
                            new ReceiverMaster().ChangeReceivingStatus(id, "Invoiced");

                            Alert.Show("Invoice Create succesfully.");


                        }
                    }
                    this.LoadReceiverGrid();
                }
                else
                {
                    Alert.Show("Already Invoice Cerated");
                }
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to create invoice");
            }
            
        }
    }
}