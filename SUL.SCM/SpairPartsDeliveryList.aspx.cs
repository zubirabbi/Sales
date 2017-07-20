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
    public partial class SpairPartsDeliveryList : System.Web.UI.Page
    {
        private Users _user;
        private Company _company;
        private string _department;

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

            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Spair Part Delivery List") : new AppFunctionality().GetAppFunctionalityId("Spair Part Delivery List", int.Parse(lblsource.Text));
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
        public static void GetActiveAndInvoice(DataTable SPDeliveryList)
        {
            try
            {
                DataColumn col1 = new DataColumn();
                DataColumn col2 = new DataColumn();
                col1.ColumnName = "ImageStatus";
                col2.ColumnName = "DevileryImage";
                SPDeliveryList.Columns.Add(col1);
                SPDeliveryList.Columns.Add(col2);

                foreach (DataRow dr in SPDeliveryList.Rows)
                {
                    if (dr["Status"].ToString().ToLower() == "created" || dr["Status"].ToString().ToLower() == "unapproved")
                    {
                        dr["ImageStatus"] = "Images/Inactive.png";
                        dr["DevileryImage"] = "Images/Delivery.png";

                    }
                    else if (dr["Status"].ToString().ToLower() == "seen" || dr["Status"].ToString().ToLower() == "unapproved")
                    {

                        dr["ImageStatus"] = "Images/Inactive.png";

                        dr["DevileryImage"] = "Images/Delivery.png";

                    }
                    else if ((dr["Status"].ToString().ToLower() == "approved"))
                    {

                        dr["ImageStatus"] = "Images/Active.png";

                        dr["DevileryImage"] = "Images/Delivery.png";
                    }
                    else if ((dr["Status"].ToString().ToLower() == "canceled"))
                    {

                        dr["DevileryImage"] = "Images/Delivery.png";

                    }
                    else if ((dr["Status"].ToString().ToLower() == "invoiced"))
                    {
                        dr["ImageStatus"] = "Images/Active.png";
                        dr["DevileryImage"] = "Images/Delivery.png";

                    }
                    //}

                    DataTable spDeliveryList = SPDeliveryList;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }


        }
        private void LoadSpDeliveryMaster()
        {
            try
            {
                DataTable dtspDeliveryMaster = new SPDeliveryMaster().GetSPDelliveryMasterList();

                if (dtspDeliveryMaster.Rows.Count == 0)
                {
                    RadgridSPDeliveryList.DataSource = new string[] { };
                    RadgridSPDeliveryList.DataBind();
                    return;
                }

                RadgridSPDeliveryList.DataSource = dtspDeliveryMaster;
                GetActiveAndInvoice(dtspDeliveryMaster);
                RadgridSPDeliveryList.DataBind();

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load data" + ex);
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
                LoadSpDeliveryMaster();
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

        protected void RadgridSPDeliveryList_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadSpDeliveryMaster();
        }

        protected void RadgridSPDeliveryList_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadSpDeliveryMaster();
        }

        protected void RadgridSPDeliveryList_OnItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            LinkButton linkedit = (LinkButton)sender;
            string[] Id = linkedit.CommandArgument.ToString().Split(';');

            Response.Redirect("SpairPartsDelivery.aspx?Id=" + Id[0]);

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
                        success = new SPDeliveryMaster().ChangeSPDeliveryApproveStatus(id, _user.Id, "Approved", DateTime.Now);
                        if (success == 0)
                        {
                            Alert.Show("Approval failed for the requisition.");
                        }
                        else
                        {
                            Alert.Show("Spair part is approved successfully.");
                            this.LoadSpDeliveryMaster();
                        }
                    }
                    else if (status.ToLower() == "approved")
                    {
                        success = new SPDeliveryMaster().ChangeSPDeliveryApproveStatus(id, _user.Id, "UnApproved", DateTime.Now);
                        if (success == 0)
                        {
                            Alert.Show("Up approval failed for the requisition.");
                        }
                        else
                        {
                            Alert.Show("Spair part  was un approved successfully.");
                            this.LoadSpDeliveryMaster();
                        }
                    }
                    else if (status.ToLower() == "received")
                    {
                        Alert.Show("Sorry you cannot change status of this Spair part now. This Spair part is already been received.");
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
        private int UpdateItemLedger(SPDeliveryDetails tmpDetails, int totalIn, int totalOut, long ledgerId)
        {
            ItemLedger objItemLedger = new ItemLedger();
            //--------Insert data in Item Legder----------//
            objItemLedger.ItemId = tmpDetails.SpairPartsId;
            objItemLedger.Unit = tmpDetails.Unit;
            objItemLedger.Color = tmpDetails.Color.ToString();
            objItemLedger.WareHouseId = 10003;
            objItemLedger.TotalIn = totalIn;
            objItemLedger.TotalOut = totalOut;

            int success = 0;
            if (ledgerId == 0)
                success = objItemLedger.InsertItemLedger();
            else
            {
                objItemLedger.Id = ledgerId;
                success = objItemLedger.UpdateItemLedger();
            }
            return success;
        }
        protected void btnDelivery_OnClick(object sender, EventArgs e)
        {
            try
            {

                LinkButton linkedit = (LinkButton)sender;
                string[] itemReturnId = linkedit.CommandArgument.ToString().Split(';');
                int returnId = int.Parse(itemReturnId[0]);

                SPDeliveryMaster objSpDeliveryMaster = new SPDeliveryMaster().GetSPDeliveryMasterById(returnId);
                List<SPDeliveryDetails> lstSpDeliveryDetailses =
                    new SPDeliveryDetails().GetAllSPDealerDetailsBymasterId(returnId);
                DealerLedger objDealerLedger = new DealerLedger();
                int dealerSuccess = 0;
                if (objSpDeliveryMaster.Status.ToLower() == "approved")
                {
                    ItemJournalMaster objDetailsJournal = new ItemJournalMaster();

                    objDetailsJournal.TransactionDate = DateTime.Now;
                    objDetailsJournal.TransactionType = "Product Return";
                    objDetailsJournal.SourceId = objSpDeliveryMaster.Id.ToString();
                    objDetailsJournal.UserId = _user.Id;
                    objDetailsJournal.WareHouseId = 1;
                    objDetailsJournal.WareHouseIdFrom = 10003;

                    int ItemJournalsuccess = objDetailsJournal.InsertItemJournalMaster();
                    if (ItemJournalsuccess == 0)
                    {
                        Alert.Show("something is going wrong to save Item Details Journal data.");
                    }
                    else
                    {
                        if (lstSpDeliveryDetailses.Count != 0)
                        {
                            foreach (SPDeliveryDetails deliveryDetails in lstSpDeliveryDetailses)
                            {
                                ItemJournalDetails objItemDetails = new ItemJournalDetails();
                                List<ItemLedger> lstItemLedgers = new List<ItemLedger>();
                                int Openingbalance = 0;
                                int success = 0;
                                if (deliveryDetails.Color == 0)
                                {
                                    lstItemLedgers =
                                        new ItemLedger().GetAllItemLedgersByItemIdUnit(deliveryDetails.SpairPartsId,
                                            deliveryDetails.Unit);
                                }
                                else
                                {
                                    lstItemLedgers =
                                        new ItemLedger().GetAllItemLedgersByItemIdUnitColor(deliveryDetails.SpairPartsId,
                                            deliveryDetails.Unit, deliveryDetails.Color);
                                }
                                if (lstItemLedgers.Count != 0)
                                {
                                    if (deliveryDetails.Color == 0)
                                    {
                                        ItemLedger objItemLedger =
                                            new ItemLedger().GetItemLedgerByItemIdUnit(deliveryDetails.SpairPartsId,
                                                deliveryDetails.Unit);
                                        Openingbalance = objItemLedger.Balance;
                                    }
                                    else
                                    {
                                        ItemLedger objItemLedger =
                                            new ItemLedger().GetItemLedgerByItemIdUnitColor(deliveryDetails.SpairPartsId,
                                                deliveryDetails.Unit, deliveryDetails.Color);
                                        Openingbalance = objItemLedger.Balance;
                                    }

                                }
                                List<Product> prod = new Product().GetAllProduct();
                                objItemDetails.MasterId = new ItemJournalMaster().GetMaxItemDetailsJournalMasterId();
                                objItemDetails.ProductId = deliveryDetails.SpairPartsId;
                                Product objp = prod.Find(x => x.Id == deliveryDetails.SpairPartsId);
                                objItemDetails.ProductName = objp.ProductName;
                                objItemDetails.Color = deliveryDetails.Color;
                                objItemDetails.Unit = deliveryDetails.Unit;
                                objItemDetails.OpeningBalance = Openingbalance;
                                objItemDetails.QuantityIn = 0;
                                objItemDetails.QuantityOut = int.Parse(deliveryDetails.Quantity.ToString());
                                objItemDetails.ClosingBalance = 0;
                                objItemDetails.Rate = deliveryDetails.TotalAmount;


                                //success = objDeliveryDetails.InsertDeliveryDetails();
                                success = objItemDetails.InsertItemJournalDetails();

                                if (lstItemLedgers.Count != 0)
                                {
                                    if (deliveryDetails.Color == 0)
                                    {
                                        ItemLedger objItemLedger =
                                            new ItemLedger().GetItemLedgerByItemIdUnit(deliveryDetails.SpairPartsId,
                                                deliveryDetails.Unit);
                                        this.UpdateItemLedger(deliveryDetails, 0, int.Parse(deliveryDetails.Quantity.ToString()), objItemLedger.Id);
                                    }
                                    else
                                    {
                                        ItemLedger objItemLedger =
                                            new ItemLedger().GetItemLedgerByItemIdUnitColor(deliveryDetails.SpairPartsId,
                                                deliveryDetails.Unit, deliveryDetails.Color);
                                        this.UpdateItemLedger(deliveryDetails, 0, int.Parse(deliveryDetails.Quantity.ToString()), objItemLedger.Id);
                                    }
                                }
                                else
                                {
                                    if (deliveryDetails.Color == 0)
                                    {
                                        this.UpdateItemLedger(deliveryDetails, 0, int.Parse(deliveryDetails.Quantity.ToString()), 0);
                                    }
                                    else
                                    {
                                        this.UpdateItemLedger(deliveryDetails, 0, int.Parse(deliveryDetails.Quantity.ToString()), 0);
                                    }
                                }


                                if (success == 0)
                                {
                                    Alert.Show("Something is going wrong to Save data");
                                    return;
                                }
                                else
                                {
                                    Alert.Show("Item  Details Save succesfully.");
                                }
                            }
                        }
                    }
                    int Itemsuccess = 0;
                    Itemsuccess = new ItemReturnMaster().ChangeItemReturnStatus(returnId, "Received");
                    if (Itemsuccess == 0)
                    {
                        Alert.Show("Item  Details Receive not Save succesfully.");
                    }
                    else
                    {
                        Alert.Show("Item  Details Receive Save succesfully.");
                        this.LoadSpDeliveryMaster();
                    }


                }
                else if (objSpDeliveryMaster.Status.ToLower() == "received")
                {
                    Alert.Show("Item retrun is already received ");

                }
                else
                {
                    Alert.Show("Item retrun is unapproved or created.To Receive this item please approve this return item");
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
    }
}