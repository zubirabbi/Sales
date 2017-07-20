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
    public partial class ProductConvertion : System.Web.UI.Page
    {
        private Users _user;
        private Company _company;
        private string _department;
        private List<TempSPDetails> tempSpDetailses;
        private AppPermission PermissionUser;

        private class TempSPDetails
        {
            public int Id { get; set; }
            public int SpairPartsId { get; set; }
            public string SpairParts { get; set; }
            public decimal Quantity { get; set; }
            public decimal Rate { get; set; }
            public int Unit { get; set; }
            public string UnitName { get; set; }
            public int Color { get; set; }
            public string ColorName { get; set; }

            public decimal TotalRate { get; set; }
        }

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

        private void LoadProduct()
        {
            try
            {
                DataTable dtProductInfo = new Product().GetProductFromViewList();

                rdropProduct.DataValueField = "Id";
                rdropProduct.DataTextField = "proInfo";
                rdropProduct.DataSource = dtProductInfo;
                rdropProduct.DataBind();

                rdropProduct.SelectedIndex = 0;


            }
            catch (Exception ex)
            {
                Alert.Show("something is going on to load Product Info" + ex);
            }
        }
        private void LoadSPDeliveryDetails()
        {
            try
            {
                if (tempSpDetailses.Count == 0)
                {
                    RadGridAddSPDetails.DataSource = new string[] { };
                    RadGridAddSPDetails.DataBind();
                    return;
                }

                RadGridAddSPDetails.DataSource = tempSpDetailses;
                RadGridAddSPDetails.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load requisition details grid." + ex);
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

            if (Session["tempSpDetailses"] != null)
                tempSpDetailses = (List<TempSPDetails>)Session["tempSpDetailses"];
            else
                tempSpDetailses = new List<TempSPDetails>();

            if (!IsPostBack)
            {
                this.LoadProduct();
                Session["tempSpDetailses"] = null;
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

        protected void rdropProduct_OnDataBound(object sender, EventArgs e)
        {
            rdropProduct.Items.Insert(0, new RadComboBoxItem());
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            TempSPDetails objTempSpDetails = new TempSPDetails();
            #region SpAdvAdd

            List<SpairParts> lstSpairParts =
                new SpairParts().GetAllSpairPartsByProduct(int.Parse(rdropProduct.SelectedValue));
            if (tempSpDetailses.Count == 0)
                tempSpDetailses = new List<TempSPDetails>();
            List<Product> lstProduct = new Product().GetAllProduct();

            foreach (SpairParts sp in lstSpairParts)
            {
                Product objProduct = lstProduct.Find(x => x.Id == sp.SpairPartId && x.ProductCategory == 6);
                objTempSpDetails =
                    tempSpDetailses.Find(
                        x =>
                            x.SpairPartsId == sp.SpairPartId);
                if (objTempSpDetails != null)
                {
                    if (objTempSpDetails.SpairPartsId != 0)
                    {
                        tempSpDetailses.Remove(objTempSpDetails);
                        objTempSpDetails.SpairPartsId = sp.SpairPartId;
                        objTempSpDetails.Unit = 1;
                        objTempSpDetails.Quantity = (int.Parse(rtxtpQuentityP.Text) *
                                                     int.Parse(sp.Quentity.ToString()));
                        objTempSpDetails.SpairParts = objProduct.ProductCode + ";" + objProduct.ProductName;
                        objTempSpDetails.UnitName = "Price";
                        objTempSpDetails.Rate = objProduct.DP;
                        objTempSpDetails.Color = 0;
                        objTempSpDetails.ColorName = "";
                        objTempSpDetails.TotalRate = decimal.Parse(objTempSpDetails.Quantity.ToString()) *
                                                     objProduct.DP;
                        tempSpDetailses.Add(objTempSpDetails);

                    }
                }
                else
                {
                    objTempSpDetails = new TempSPDetails();
                    objTempSpDetails.SpairPartsId = sp.SpairPartId;
                    objTempSpDetails.Unit = 1;
                    objTempSpDetails.Quantity = int.Parse(rtxtpQuentityP.Text) *
                                                int.Parse(sp.Quentity.ToString());
                    objTempSpDetails.SpairParts = objProduct.ProductCode + ";" + objProduct.ProductName;
                    objTempSpDetails.UnitName = "Price";
                    objTempSpDetails.Rate = objProduct.DP;
                    objTempSpDetails.Color = 0;
                    objTempSpDetails.ColorName = "";
                    objTempSpDetails.TotalRate = decimal.Parse(rtxtpQuentityP.Text) * sp.Quentity *
                                                 objProduct.DP;
                    tempSpDetailses.Add(objTempSpDetails);
                }
            }

            Session["tempSpDetailses"] = tempSpDetailses;
            this.LoadSPDeliveryDetails();

            decimal totalsum;

            totalsum = tempSpDetailses.Sum(x => x.TotalRate);
            rtxtItemTotal.Text = totalsum.ToString();
            #endregion SpAdvAdd
        }
        private int UpdateItemLedger(TempSPDetails tmpDetails, int totalIn, int totalOut, long ledgerId)
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
        protected void btnConvert_OnClick(object sender, EventArgs e)
        {
            try
            {
                #region validation

                if (rtxtpQuentityP.Text == string.Empty)
                {
                    Alert.Show("Please enter a price");
                    rtxtpQuentityP.Focus();
                    return;
                }

                #endregion
                if (tempSpDetailses.Count != 0)
                {
                    #region SpairPartsItemJournal
                    ItemJournalMaster objDetailsJournal = new ItemJournalMaster();

                    objDetailsJournal.TransactionDate = DateTime.Now;
                    objDetailsJournal.TransactionType = "Receive";
                    objDetailsJournal.SourceId = "0";
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
                        foreach (TempSPDetails tmpSpDetails in tempSpDetailses)
                        {
                            ItemJournalDetails objItemDetails = new ItemJournalDetails();
                            List<ItemLedger> lstItemLedgers = new List<ItemLedger>();
                            int Openingbalance = 0;
                            int success = 0;
                            if (tmpSpDetails.Color == 0)
                            {
                                lstItemLedgers =
                                    new ItemLedger().GetAllItemLedgersByItemIdUnit(tmpSpDetails.SpairPartsId,
                                        tmpSpDetails.Unit);
                            }
                            else
                            {
                                lstItemLedgers =
                                    new ItemLedger().GetAllItemLedgersByItemIdUnitColor(tmpSpDetails.SpairPartsId,
                                        tmpSpDetails.Unit, tmpSpDetails.Color);
                            }
                            {
                                if (tmpSpDetails.Color == 0)
                                {
                                    ItemLedger objItemLedger =
                                        new ItemLedger().GetItemLedgerByItemIdUnit(tmpSpDetails.SpairPartsId,
                                            tmpSpDetails.Unit);

                                    Openingbalance = objItemLedger.Id == 0 ? 0 : objItemLedger.Balance;
                                }
                                else
                                {
                                    ItemLedger objItemLedger =
                                        new ItemLedger().GetItemLedgerByItemIdUnitColor(tmpSpDetails.SpairPartsId,
                                            tmpSpDetails.Unit, tmpSpDetails.Color);
                                    Openingbalance = objItemLedger.Id == 0 ? 0 : objItemLedger.Balance;
                                }

                            }
                            List<Product> prod = new Product().GetAllProduct();
                            objItemDetails.MasterId = new ItemJournalMaster().GetMaxItemDetailsJournalMasterId();
                            objItemDetails.ProductId = tmpSpDetails.SpairPartsId;
                            Product objp = prod.Find(x => x.Id == tmpSpDetails.SpairPartsId);
                            objItemDetails.ProductName = objp.ProductName;
                            objItemDetails.Color = tmpSpDetails.Color;
                            objItemDetails.Unit = tmpSpDetails.Unit;
                            objItemDetails.OpeningBalance = Openingbalance;
                            objItemDetails.QuantityIn = int.Parse(tmpSpDetails.Quantity.ToString());
                            objItemDetails.QuantityOut = 0;
                            objItemDetails.ClosingBalance = 0;
                            objItemDetails.Rate = tmpSpDetails.Rate;


                            //success = objDeliveryDetails.InsertDeliveryDetails();
                            success = objItemDetails.InsertItemJournalDetails();

                            if (lstItemLedgers.Count != 0)
                            {
                                if (tmpSpDetails.Color == 0)
                                {
                                    ItemLedger objItemLedger =
                                        new ItemLedger().GetItemLedgerByItemIdUnit(tmpSpDetails.SpairPartsId,
                                            tmpSpDetails.Unit);
                                    this.UpdateItemLedger(tmpSpDetails, 0, int.Parse(tmpSpDetails.Quantity.ToString()), objItemLedger.Id);
                                }
                                else
                                {
                                    ItemLedger objItemLedger =
                                        new ItemLedger().GetItemLedgerByItemIdUnitColor(tmpSpDetails.SpairPartsId,
                                            tmpSpDetails.Unit, tmpSpDetails.Color);
                                    this.UpdateItemLedger(tmpSpDetails, 0, int.Parse(tmpSpDetails.Quantity.ToString()), objItemLedger.Id);
                                }
                            }
                            else
                            {
                                if (tmpSpDetails.Color == 0)
                                {
                                    this.UpdateItemLedger(tmpSpDetails,int.Parse(tmpSpDetails.Quantity.ToString()), 0,0);
                                }
                                else
                                {
                                    this.UpdateItemLedger(tmpSpDetails, int.Parse(tmpSpDetails.Quantity.ToString()), 0, 0);
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
                    #endregion
                    #region ProductItemJournal
                    ItemJournalMaster objDetailsJournalForProduct = new ItemJournalMaster();

                    objDetailsJournalForProduct.TransactionDate = DateTime.Now;
                    objDetailsJournalForProduct.TransactionType = "Receive";
                    objDetailsJournalForProduct.SourceId = "0";
                    objDetailsJournalForProduct.UserId = _user.Id;
                    objDetailsJournalForProduct.WareHouseId = 1;
                    objDetailsJournalForProduct.WareHouseIdFrom = 10003;


                    int ItemJournalProductsuccess = objDetailsJournalForProduct.InsertItemJournalMaster();
                    if (ItemJournalProductsuccess == 0)
                    {
                        Alert.Show("something is going wrong to save Item Details Journal data.");
                    }
                    else
                    {
                        ItemJournalDetails objItemDetails = new ItemJournalDetails();
                        List<ItemLedger> lstItemLedgers = new List<ItemLedger>();
                        int Openingbalance = 0;
                        int success = 0;

                        lstItemLedgers =
                            new ItemLedger().GetAllItemLedgersByItemIdUnit(int.Parse(rdropProduct.SelectedValue), 1);

                        if (lstItemLedgers.Count != 0)
                        {

                            ItemLedger objItemLedger =
                                new ItemLedger().GetItemLedgerByItemIdUnit(int.Parse(rdropProduct.SelectedValue), 1);
                            Openingbalance = objItemLedger.Id == 0 ? 0 : objItemLedger.Balance;

                        }
                        List<Product> prod = new Product().GetAllProduct();
                        objItemDetails.MasterId = new ItemJournalMaster().GetMaxItemDetailsJournalMasterId();
                        objItemDetails.ProductId = int.Parse(rdropProduct.SelectedValue);
                        Product objp = prod.Find(x => x.Id == int.Parse(rdropProduct.SelectedValue));
                        objItemDetails.ProductName = objp.ProductName;
                        objItemDetails.Color = 0;
                        objItemDetails.Unit = 1;
                        objItemDetails.OpeningBalance = Openingbalance;
                        objItemDetails.QuantityIn = 0;
                        objItemDetails.QuantityOut = int.Parse(rtxtpQuentityP.Text);
                        objItemDetails.ClosingBalance = 0;
                        objItemDetails.Rate = objp.CostPrice;


                        //success = objDeliveryDetails.InsertDeliveryDetails();
                        success = objItemDetails.InsertItemJournalDetails();
                        if (success == 1)
                        {
                            if (lstItemLedgers.Count != 0)
                            {

                                ItemLedger objItemLedger =
                                    new ItemLedger().GetItemLedgerByItemIdUnit(int.Parse(rdropProduct.SelectedValue), 1);

                                ItemLedger objItemLedgerProduct = new ItemLedger();
                                //--------Insert data in Item Legder----------//
                                objItemLedgerProduct.ItemId = int.Parse(rdropProduct.SelectedValue);
                                objItemLedgerProduct.Unit = 1;
                                objItemLedgerProduct.Color = "0";
                                objItemLedgerProduct.WareHouseId = 10003;
                                objItemLedgerProduct.TotalIn = 0;
                                objItemLedgerProduct.TotalOut = int.Parse(rtxtpQuentityP.Text);

                                objItemLedgerProduct.Id = objItemLedger.Id;
                                success = objItemLedger.UpdateItemLedger();
                                if (success == 0)
                                    return;

                            }
                            else
                            {
                                ItemLedger objItemLedger = new ItemLedger();
                                //--------Insert data in Item Legder----------//
                                objItemLedger.ItemId = int.Parse(rdropProduct.SelectedValue);
                                objItemLedger.Unit = 1;
                                objItemLedger.Color = "0";
                                objItemLedger.WareHouseId = 10003;
                                objItemLedger.TotalIn = 0;
                                objItemLedger.TotalOut = int.Parse(rtxtpQuentityP.Text);

                                int successProduct = 0;
                                successProduct = objItemLedger.InsertItemLedger();
                                if (successProduct == 0)
                                    return;
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

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save data.");
            }
        }
    }
}