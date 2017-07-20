using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using SUL.Bll.Base;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class ItemReturnList : System.Web.UI.Page
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
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Requisition Add");
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

        private void LoadItemReturn()
        {
            try
            {
                DataTable dtItemReturnMaster = new ItemReturnMaster().GetItemReturnListFromViewList();

                if (dtItemReturnMaster.Rows.Count == 0)
                {
                    RadGridItemReturn.DataSource = new string[] { };
                    RadGridItemReturn.DataBind();
                }
                GetActiveAndInvoice(dtItemReturnMaster);
                RadGridItemReturn.DataSource = dtItemReturnMaster;
                RadGridItemReturn.DataBind();

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load data ." + ex);
            }
        }
        public static void GetActiveAndInvoice(DataTable ItemReturnList)
        {
            try
            {
                DataColumn col = new DataColumn();
                DataColumn col1 = new DataColumn();
                DataColumn col2 = new DataColumn();
                col.ColumnName = "ItemReturnStatus";
                col1.ColumnName = "ImageStatus";
                col2.ColumnName = "ReceiveImage";
                ItemReturnList.Columns.Add(col);
                ItemReturnList.Columns.Add(col1);
                ItemReturnList.Columns.Add(col2);

                foreach (DataRow dr in ItemReturnList.Rows)
                {


                    if (dr["Status"].ToString().ToLower() == "created" || dr["Status"].ToString().ToLower() == "unapproved")
                    {
                        dr["ItemReturnStatus"] = "Created";
                        dr["ImageStatus"] = "Images/Inactive.png";
                        dr["ReceiveImage"] = "Images/Delivery.png";

                    }
                    else if (dr["Status"].ToString().ToLower() == "seen" || dr["Status"].ToString().ToLower() == "unapproved")
                    {
                        dr["ItemReturnStatus"] = "Created";
                        dr["ImageStatus"] = "Images/Inactive.png";
                        dr["ReceiveImage"] = "Images/Delivery.png";

                    }
                    else if ((dr["Status"].ToString().ToLower() == "approved"))
                    {
                        dr["ItemReturnStatus"] = "Approved";
                        dr["ImageStatus"] = "Images/Active.png";
                        dr["ReceiveImage"] = "Images/Delivery.png";
                    }
                    else if ((dr["Status"].ToString().ToLower() == "received"))
                    {
                        dr["ItemReturnStatus"] = "Approved";
                        dr["ImageStatus"] = "Images/Active.png";
                        dr["ReceiveImage"] = "Images/Delivery.png";
                    }

                    //}

                    DataTable ItemList = ItemReturnList;
                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
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
            else
                _department = "All";
            if (!IsPostBack)
            {

                if (_user.EmployeeId != 0)
                {
                    Department objDepartment = new Department().GetEmployeeDepartment(_user.EmployeeId);
                    _department = objDepartment.DepartmentName;
                }

                this.LoadItemReturn();
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

        protected void RadGridItemReturn_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadItemReturn();
        }

        protected void RadGridItemReturn_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadItemReturn();
        }

        protected void RadGridItemReturn_OnItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void btnEdit_OnClick(object sender, EventArgs e)
        {

            LinkButton linkedit = (LinkButton)sender;
            string[] Id = linkedit.CommandArgument.ToString().Split(';');


            Response.Redirect("ItemReturnsInfo.aspx?Id=" + Id[0]);
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
                        success = new ItemReturnMaster().ChangeItemReturnStatus(id, "Approved");
                        if (success == 0)
                        {
                            Alert.Show("Approval failed for the Item return .");
                        }
                        else
                        {
                            Alert.Show("The Item return is approved successfully.");
                            this.LoadItemReturn();
                        }
                    }
                    else if (status.ToLower() == "approved")
                    {
                        success = new ItemReturnMaster().ChangeItemReturnStatus(id, "UnApproved");
                        if (success == 0)
                        {
                            Alert.Show("Up approval failed for the Item return .");
                        }
                        else
                        {
                            Alert.Show("The Item return was un approved successfully.");
                            this.LoadItemReturn();
                        }
                    }
                    else if (status.ToLower() == "received")
                    {
                        Alert.Show("Sorry you cannot change status of this Item return now. This Item return is already been received .");
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

        private int UpdateItemLedger(ItemReturnDetails tmpDetails, int totalIn, int totalOut, long ledgerId)
        {
            ItemLedger objItemLedger = new ItemLedger();
            //--------Insert data in Item Legder----------//
            objItemLedger.ItemId = tmpDetails.ProductId;
            objItemLedger.Unit = tmpDetails.UnitId;
            objItemLedger.Color = tmpDetails.ColorId.ToString();
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
        protected void btnReceive_OnClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton linkedit = (LinkButton)sender;
                string[] itemReturnId = linkedit.CommandArgument.ToString().Split(';');
                int returnId = int.Parse(itemReturnId[0]);

                ItemReturnMaster objItemReturnMaster = new ItemReturnMaster().GetItemReturnMasterById(returnId);
                List<ItemReturnDetails> lstItemReturnDetailses =
                    new ItemReturnDetails().GetAllItemReturnDetailsByMasterId(returnId);
                DealerLedger objDealerLedger = new DealerLedger();
                DealerInformation objDealerinfo =
                    new DealerInformation().GetDealerInformationById(objItemReturnMaster.DealerId);

                int dealerSuccess = 0;
                if (objItemReturnMaster.Status.ToLower() == "approved")
                {
                    objDealerLedger.DealerId = objItemReturnMaster.DealerId;
                    objDealerLedger.TransactionType = "Product Return";
                    objDealerLedger.TransactionDate = DateTime.Now;
                    objDealerLedger.SourceId = objItemReturnMaster.Id.ToString();
                    objDealerLedger.UserId = _user.Id;
                    objDealerLedger.OpeningBalance = objDealerinfo.Balance;
                    objDealerLedger.Debit = 0;
                    objDealerLedger.Cradit = objItemReturnMaster.ReturnTotal;
                    //objDealerLedger.ClosingBalance = objDealerinfo.Balance;
                    objDealerLedger.SourceNo = objItemReturnMaster.ReturnCode;
                    objDealerLedger.Remarks = "";

                    dealerSuccess = objDealerLedger.InsertDealerLedger();
                    if (dealerSuccess == 0)
                    {
                        Alert.Show("Dealer Ledger information is not save succesfully.");
                    }
                    else
                    {
                        DealerInformation objdinfo = new DealerInformation();

                        objdinfo.Id = int.Parse(objItemReturnMaster.DealerId.ToString());
                        objdinfo.TotalDebit = 0;
                        objdinfo.TotalCredit = objItemReturnMaster.ReturnTotal;

                        int dInfosuccess = objdinfo.UpdateDealerInformationfordealerLedger();

                        if (dInfosuccess == 0)
                        {
                            Alert.Show("Dealer information is not save succesfully.");
                        }
                    }

                    ItemJournalMaster objDetailsJournal = new ItemJournalMaster();

                    objDetailsJournal.TransactionDate = DateTime.Now;
                    objDetailsJournal.TransactionType = "Product Return";
                    objDetailsJournal.SourceId = objItemReturnMaster.Id.ToString();
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
                        if (lstItemReturnDetailses.Count != 0)
                        {
                            foreach (ItemReturnDetails itemReturn in lstItemReturnDetailses)
                            {
                                ItemJournalDetails objItemDetails = new ItemJournalDetails();
                                List<ItemLedger> lstItemLedgers = new List<ItemLedger>();
                                int Openingbalance = 0;
                                int success = 0;
                                if (itemReturn.ColorId == 0)
                                {
                                    lstItemLedgers =
                                        new ItemLedger().GetAllItemLedgersByItemIdUnit(itemReturn.ProductId,
                                            itemReturn.UnitId);
                                }
                                else
                                {
                                    lstItemLedgers =
                                        new ItemLedger().GetAllItemLedgersByItemIdUnitColor(itemReturn.ProductId,
                                            itemReturn.UnitId, itemReturn.ColorId);
                                }
                                if (lstItemLedgers.Count != 0)
                                {
                                    if (itemReturn.ColorId == 0)
                                    {
                                        ItemLedger objItemLedger =
                                            new ItemLedger().GetItemLedgerByItemIdUnit(itemReturn.ProductId,
                                                itemReturn.UnitId);
                                        Openingbalance = objItemLedger.Balance;
                                    }
                                    else
                                    {
                                        ItemLedger objItemLedger =
                                            new ItemLedger().GetItemLedgerByItemIdUnitColor(itemReturn.ProductId,
                                                itemReturn.UnitId, itemReturn.ColorId);
                                        Openingbalance = objItemLedger.Balance;
                                    }

                                }
                                List<Product> prod = new Product().GetAllProduct();
                                objItemDetails.MasterId = new ItemJournalMaster().GetMaxItemDetailsJournalMasterId();
                                objItemDetails.ProductId = itemReturn.ProductId;
                                Product objp = prod.Find(x => x.Id == itemReturn.ProductId);
                                objItemDetails.ProductName = objp.ProductName;
                                objItemDetails.Color = itemReturn.ColorId;
                                objItemDetails.Unit = itemReturn.UnitId;
                                objItemDetails.OpeningBalance = Openingbalance;
                                objItemDetails.QuantityIn = 0;
                                objItemDetails.QuantityOut = itemReturn.ReturnQuantity;
                                objItemDetails.ClosingBalance = 0;
                                objItemDetails.Rate = itemReturn.ReturnRate;


                                //success = objDeliveryDetails.InsertDeliveryDetails();
                                success = objItemDetails.InsertItemJournalDetails();

                                if (lstItemLedgers.Count != 0)
                                {
                                    if (itemReturn.ColorId == 0)
                                    {
                                        ItemLedger objItemLedger =
                                            new ItemLedger().GetItemLedgerByItemIdUnit(itemReturn.ProductId,
                                                itemReturn.UnitId);
                                        this.UpdateItemLedger(itemReturn, 0, itemReturn.ReturnQuantity, objItemLedger.Id);
                                    }
                                    else
                                    {
                                        ItemLedger objItemLedger =
                                            new ItemLedger().GetItemLedgerByItemIdUnitColor(itemReturn.ProductId,
                                                itemReturn.UnitId, itemReturn.ColorId);
                                        this.UpdateItemLedger(itemReturn, 0, itemReturn.ReturnQuantity, objItemLedger.Id);
                                    }
                                }
                                else
                                {
                                    if (itemReturn.ColorId == 0)
                                    {
                                        this.UpdateItemLedger(itemReturn, 0, itemReturn.ReturnQuantity, 0);
                                    }
                                    else
                                    {
                                        this.UpdateItemLedger(itemReturn, 0, itemReturn.ReturnQuantity, 0);
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
                        this.LoadItemReturn();
                    }


                }
                else if (objItemReturnMaster.Status.ToLower() == "received")
                {
                    Alert.Show("Item retrun is already received ");

                }
                else
                {
                    Alert.Show("Item retrun is unapproved or created.To Receive this item please approve this return item");
                }
            }
            catch
                (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }
    }
}