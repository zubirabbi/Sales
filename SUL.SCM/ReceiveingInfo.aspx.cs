using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUL.Bll;
using Telerik.Web.UI;

namespace SUL.SCM
{
    public partial class ReceiveingInfo : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        //private bool isNewEntry;
        private List<TempReceiverInfo> receiverInfo;
        private Company _company;
        private Hashtable ItemBalance;

        private int _source;
        private string _department;
        private class TempReceiverInfo
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public int PIquantity { get; set; }
            public int receivedQuantity { get; set; }
            public string color { get; set; }
            public string colorName { get; set; }
            public int ReceiveQuantity { get; set; }
            public int ProductUnit { get; set; }
            public string ProductUnitName { get; set; }
            public decimal amount { get; set; }

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
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Receiveing Add") : new AppFunctionality().GetAppFunctionalityId("Receiveing Add", int.Parse(lblsource.Text));

            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsView)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsView;
                return permission;
            }
            else
                return true;
        }

        private bool IsValidInsertForUser()
        {
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Receiveing Add") : new AppFunctionality().GetAppFunctionalityId("Receiveing Add", int.Parse(lblsource.Text));
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsInsert)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsInsert;
                return permission;
            }
            else
                return true;
        }

        private bool IsValidUpdateForUser()
        {
            int FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Receiveing Add") : new AppFunctionality().GetAppFunctionalityId("Receiveing Add", int.Parse(lblsource.Text));
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsUpdate)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsUpdate;
                return permission;
            }
            else
                return true;
        }

        private bool IsValidDeleteForUser()
        {
            int FunctionalId = new AppFunctionality().GetAppFunctionalityId("Receiveing Add");
            int RoleId = new UserRoleMapping().GetUserRoleMappingByUserId(_user.Id, _user.CompanyId).RoleId;
            AppPermission PermissionUser = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                _user.CompanyId);

            if (!PermissionUser.IsDelete)
            {
                AppPermission Permission = new AppPermission().GetAppPermissionId(FunctionalId, _user.Id, RoleId,
                    _user.CompanyId);
                bool permission = Permission.IsDelete;
                return permission;
            }
            else
                return true;
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

                if (lstSuppliers.Count == 2)
                {
                    rdropVandorName.SelectedIndex = 1;
                    this.LoadLCNo(int.Parse(rdropVandorName.SelectedValue));
                }
                else
                    rdropVandorName.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load vendor name.");
            }
        }

        private void LoadColor(int ProductId)
        {
            try
            {
                List<ListTable> lstListTables = new ListTable().GetAllListTableByTypeAndPid("Color", ProductId);
                lstListTables.Insert(0, new ListTable());

                rdropColor.DataTextField = "ListValue";
                rdropColor.DataValueField = "ListId";
                rdropColor.DataSource = lstListTables;
                rdropColor.DataBind();

                rdropColor.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                Alert.Show(ex.Message);
            }
        }

        private void LoadLCNo(int supId)
        {
            try
            {
                List<LCInformation> lstLcInformations = new LCInformation().GetAllLCInformationbySupplierId(supId);
                lstLcInformations.Insert(0, new LCInformation());

                rdropLCNo.DataTextField = "LCNumber";
                rdropLCNo.DataValueField = "Id";
                rdropLCNo.DataSource = lstLcInformations;
                rdropLCNo.DataBind();

                if (lstLcInformations.Count == 2)
                {
                    rdropLCNo.SelectedIndex = 1;

                    LCInformation objLcInformation = new LCInformation().GetLCInformationById(int.Parse(rdropLCNo.SelectedValue));
                    lblSupId.Text = objLcInformation.Id.ToString();
                    string PiId = objLcInformation.PINo.ToString();

                    PIMaster objPiMaster = new PIMaster().GetPIMasterById(int.Parse(PiId));
                    rtxtPINumber.Text = objPiMaster.PINo;
                    lblPIId.Text = PiId;


                    this.LoadProductInfo(int.Parse(lblPIId.Text));
                }
                else
                    rdropLCNo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Lc Information." + ex);
            }
        }

        private void LoadWareHouse()
        {
            try
            {
                List<WareHouse> lstWareHouses = new WareHouse().GetAllWareHousebyWareHouseID();
                lstWareHouses.Insert(0, new WareHouse());

                rdropWareHouse.DataTextField = "Name";
                rdropWareHouse.DataValueField = "Id";
                rdropWareHouse.DataSource = lstWareHouses;
                rdropWareHouse.DataBind();

                if (lstWareHouses.Count == 2)
                {
                    rdropWareHouse.SelectedIndex = 1;

                    WareHouse objWareHouse = new WareHouse().GetWareHouseById(int.Parse(rdropWareHouse.SelectedValue), _company.Id);
                    EmployeeInformation objEmployeeInformation =
                        new EmployeeInformation().GetEmployeeInformationById(int.Parse(objWareHouse.Incharge),
                            _company.Id);
                    rtxtReceivedBy.Text = objEmployeeInformation.EmployeeName;

                }
                else
                    rdropWareHouse.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load warehouse data." + ex);
            }
        }

        private void LoadRecciverDetailsGrid()
        {
            try
            {
                //DataTable dtReceiveDetails = new ReceiverDetails().GetPIDetailsTable(piNo);
                if (receiverInfo.Count <= 0)
                {
                    RadGridAddReceivedDetails.DataSource = new string[] { };
                    return;

                }
                RadGridAddReceivedDetails.DataSource = receiverInfo;
                RadGridAddReceivedDetails.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load Receive Details." + ex);
            }
        }

        private void LoadReceiverDetails(int masterId)
        {
            try
            {

                DataTable dtReceiveDetails = new ReceiverDetails().GetReceiverDetailsTable(masterId);
                if (dtReceiveDetails.Rows.Count > 0)
                {
                    receiverInfo = new List<TempReceiverInfo>();
                    foreach (DataRow dr in dtReceiveDetails.Rows)
                    {
                        TempReceiverInfo tempReceiverInfo = new TempReceiverInfo();

                        tempReceiverInfo.Id = int.Parse(dr["Id"].ToString());
                        tempReceiverInfo.PIquantity = int.Parse(dr["PIquantity"].ToString());
                        tempReceiverInfo.ProductCode = dr["ProductCode"].ToString();
                        tempReceiverInfo.ProductId = int.Parse(dr["ProductId"].ToString());
                        tempReceiverInfo.ProductName = dr["ProductName"].ToString();
                        tempReceiverInfo.ProductUnit = int.Parse(dr["ProductUnit"].ToString());
                        ProductUnit objProductUnit = new ProductUnit().GetProductUnitById(int.Parse(dr["ProductUnit"].ToString()));
                        tempReceiverInfo.ProductUnitName = objProductUnit.UnitCode;
                        tempReceiverInfo.ReceiveQuantity = int.Parse(dr["ReceiveQuantity"].ToString());
                        tempReceiverInfo.color = dr["color"].ToString();
                        ListTable objlistTable = new ListTable().GetListTableByTypeIdAndValue(Convert.ToInt32(tempReceiverInfo.color),"Color");
                        tempReceiverInfo.colorName = objlistTable.ListValue;
                        tempReceiverInfo.receivedQuantity = int.Parse(dr["receivedQuantity"].ToString());

                        receiverInfo.Add(tempReceiverInfo);
                    }
                    Session["receiverInfo"] = receiverInfo;
                    this.LoadRecciverDetailsGrid();
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to Load data." + ex);
            }
        }

        private void LoadChequeDetailsFromDB(string id)
        {
            try
            {
                DataTable dtReceiveDetails = new ReceiverDetails().GetPIDetailsTable(id);
                if (dtReceiveDetails.Rows.Count > 0)
                {
                    receiverInfo = new List<TempReceiverInfo>();
                    foreach (DataRow dr in dtReceiveDetails.Rows)
                    {
                        TempReceiverInfo tempReceiverInfo = new TempReceiverInfo();

                        tempReceiverInfo.Id = int.Parse(dr["Id"].ToString());
                        tempReceiverInfo.PIquantity = int.Parse(dr["PIquantity"].ToString());
                        tempReceiverInfo.ProductCode = dr["ProductCode"].ToString();
                        tempReceiverInfo.ProductId = int.Parse(dr["ProductId"].ToString());
                        tempReceiverInfo.ProductName = dr["ProductName"].ToString();
                        tempReceiverInfo.ProductUnit = int.Parse(dr["ProductUnit"].ToString());
                        ProductUnit objProductUnit = new ProductUnit().GetProductUnitById(int.Parse(dr["ProductUnit"].ToString()));
                        tempReceiverInfo.ProductUnitName = objProductUnit.UnitCode;
                        tempReceiverInfo.ReceiveQuantity = int.Parse(dr["ReceiveQuantity"].ToString());
                        tempReceiverInfo.color = dr["color"].ToString();
                        tempReceiverInfo.colorName = rdropColor.SelectedItem.Text;
                        tempReceiverInfo.receivedQuantity = int.Parse(dr["receivedQuantity"].ToString());

                        receiverInfo.Add(tempReceiverInfo);
                    }
                    this.LoadRecciverDetailsGrid();
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to Load data." + ex);
            }
        }

        private void LoadProductInfo(int masterId)
        {
            try
            {
                DataTable dtProductInfo = new Product().GetProductByMasterIdFromViewList(masterId);

                rdropProductCode.DataValueField = "Id";
                rdropProductCode.DataTextField = "ProInfo";
                rdropProductCode.DataSource = dtProductInfo;
                rdropProductCode.DataBind();

                if (dtProductInfo.Rows.Count == 1)
                {
                    rdropProductCode.SelectedIndex = 1;

                    this.LoadColor(int.Parse(rdropProductCode.SelectedValue));


                    try
                    {
                        DataTable dtreceiverDetils = new ReceiverDetails().GetPIDetailsTableByProductId(rtxtPINumber.Text,
                       int.Parse(rdropProductCode.SelectedValue));
                        PIMaster objPIM = new PIMaster().GetAllPImasterbyPIno(rtxtPINumber.Text);
                        long pId = objPIM.Id;
                        PIDetails objPID = new PIDetails().GetAllPIDetailsOrderDetailsByIdProductId(int.Parse(pId.ToString()), int.Parse(rdropProductCode.SelectedValue));


                        if (dtreceiverDetils.Rows.Count != 0)
                        {
                            lblProductId.Text = dtreceiverDetils.Rows[0]["ProductId"].ToString();
                            lblProductCode.Text = dtreceiverDetils.Rows[0]["ProductCode"].ToString();
                            lblProductName.Text = dtreceiverDetils.Rows[0]["ProductName"].ToString();
                            lblLCQuantity.Text = dtreceiverDetils.Rows[0]["PIquantity"].ToString();
                            lblReceivedQuantity.Text = dtreceiverDetils.Rows[0]["ReceivedQuantity"].ToString();
                            rdropColor.SelectedValue = dtreceiverDetils.Rows[0]["color"].ToString() == "" ? "0" : dtreceiverDetils.Rows[0]["color"].ToString();
                            rtxtReceiveQuantity.Text = dtreceiverDetils.Rows[0]["ReceiveQuantity"].ToString();
                            lblUnite.Text = dtreceiverDetils.Rows[0]["ProductUnit"].ToString() == "" ? "0" : dtreceiverDetils.Rows[0]["ProductUnit"].ToString();
                            lblAmount.Text = objPID.PIUnitePrice.ToString();
                            int balance = 0;
                            if (ItemBalance.Contains(lblProductId.Text))
                            {
                                balance = int.Parse(ItemBalance[lblProductId.Text].ToString());
                            }
                            else
                            {
                                balance = int.Parse(lblLCQuantity.Text) - int.Parse(lblReceivedQuantity.Text);
                            }
                            lblRemaining.Text = balance.ToString();
                        }
                        else
                        {
                            Alert.Show("There is no P.I. created for this product .Please create a P.I.");
                        }

                    }
                    catch (Exception ex)
                    {

                        Alert.Show("Something is going wrong to load data." + ex);
                    }


                }
                else
                    rdropProductCode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("something is going on to load Product Info" + ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblsource.Text = Request.QueryString["source"] != null ? (Request.QueryString["source"]) : "0";


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

            if (!IsValidPageForUser())
            {
                Alert.Show("Sorry, You Don't Have permission to access this page.");
                Response.Redirect("UserLogin.aspx?refPage=HomePage.aspx", false);
            }
            if (Session["receiverInfo"] != null)
                receiverInfo = (List<TempReceiverInfo>)Session["receiverInfo"];
            else
                receiverInfo = new List<TempReceiverInfo>();

            if (Session["ItemBalance"] != null)
                ItemBalance = (Hashtable)Session["ItemBalance"];
            else
                ItemBalance = new Hashtable();

            if (!IsPostBack)
            {
                ItemBalance = new Hashtable();
                if (!IsValidInsertForUser())
                {
                    Alert.Show("Sorry, You Don't Have permission to access this page.");
                    Response.Redirect("ErrorPage.aspx", false);
                }
                receiverInfo = new List<TempReceiverInfo>();


                this.LoadVendorName();
                this.LoadWareHouse();
                lblisNewEntry.Text = "true";
                lblStatus.Text = "Created";
                this.ClearAllInfo();
                if (Request.QueryString["Id"] != null)
                {
                    string id = "";
                    id = Request.QueryString["Id"];
                    lblId.Text = id;
                    ReceiverMaster objReceiverMaster = new ReceiverMaster().GetReceiverMasterById(int.Parse(id), _company.Id);

                    rtxtReceiveCode.Text = objReceiverMaster.ReceivingCode;
                    rdtPIDate.SelectedDate = objReceiverMaster.ReceivingDate;
                    rdropVandorName.SelectedValue = objReceiverMaster.VendorId.ToString();

                    this.LoadLCNo(int.Parse(rdropVandorName.SelectedValue));

                    rdropLCNo.SelectedValue = objReceiverMaster.LCId.ToString();

                    lblPIId.Text = objReceiverMaster.PIId.ToString();
                    PIMaster objPiMaster = new PIMaster().GetPIMasterById(int.Parse(lblPIId.Text));
                    rtxtPINumber.Text = objPiMaster.PINo;

                    LoadReceiverDetails(int.Parse(id));
                    this.LoadProductInfo(int.Parse(lblPIId.Text));



                    rdropWareHouse.SelectedValue = objReceiverMaster.WareHouseId.ToString();
                    rtxtIMEIFileName.Text = objReceiverMaster.IMEIName;
                    rtxtReceivedBy.Text = objReceiverMaster.ReceivedBy;
                    rtxtInvoiceNo.Text = objReceiverMaster.InvoiceNo;
                    lblStatus.Text = objReceiverMaster.Status;
                    //this.LoadColor(int.Parse(rdropProductCode.SelectedValue));
                    lblisNewEntry.Text = "false";

                    //if (_department != "All")
                    //{
                    //    if (_department.ToLower().Contains("logistics"))
                    //    {
                    //        if (objReceiverMaster.Status.ToLower() != "approved" && objReceiverMaster.Status.ToLower() != "invoiced")
                    //            btnApprove.Visible = true;
                    //    }
                    //}
                    //else
                    //{
                    //    if (objReceiverMaster.Status.ToLower() != "approved" && objReceiverMaster.Status.ToLower() != "invoiced")
                    //        btnApprove.Visible = true;
                    //}
                }

            }
        }

        protected void rdropVandorName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadLCNo(int.Parse(rdropVandorName.SelectedValue));

        }

        protected void rdropLCNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LCInformation objLcInformation = new LCInformation().GetLCInformationById(int.Parse(rdropLCNo.SelectedValue));
            lblSupId.Text = objLcInformation.Id.ToString();
            string PiId = objLcInformation.PINo.ToString();

            PIMaster objPiMaster = new PIMaster().GetPIMasterById(int.Parse(PiId));
            rtxtPINumber.Text = objPiMaster.PINo;
            lblPIId.Text = PiId;


            this.LoadProductInfo(int.Parse(lblPIId.Text));


        }

        protected void RadGridAddReceivedDetails_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnSelect")
                {


                    this.LoadProductInfo(int.Parse(lblPIId.Text));
                    //this.LoadColor(int.Parse(rdropProductCode.SelectedValue));
                    GridDataItem item = (GridDataItem)e.Item;

                    lblDetailsId.Text = item["colId"].Text;
                    lblProductId.Text = item["colProductId"].Text;
                    lblProductCode.Text = item["colProductCode"].Text;
                    rdropProductCode.SelectedValue = item["colProductId"].Text;
                    lblProductName.Text = item["colProductName"].Text;
                    lblLCQuantity.Text = item["colPIquantity"].Text;
                    lblReceivedQuantity.Text = item["colreceivedQuantity"].Text;
                    this.LoadColor(int.Parse(rdropProductCode.SelectedValue));
                    rdropColor.SelectedValue = item["colcolor"].Text == "&nbsp;" ? "0" : item["colcolor"].Text;
                    rtxtReceiveQuantity.Text = item["colReceiveQuantity"].Text;
                    lblUnite.Text = item["colUnit"].Text == "" ? "0" : item["colUnit"].Text;
                    
                    PIMaster objPIM = new PIMaster().GetAllPImasterbyPIno(rtxtPINumber.Text);
                    long pId = objPIM.Id;
                    PIDetails objPID = new PIDetails().GetAllPIDetailsOrderDetailsByIdProductId(int.Parse(pId.ToString()), int.Parse(rdropProductCode.SelectedValue));
                    lblAmount.Text = objPID.PIUnitePrice.ToString();

                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to select Receive details grid." + ex);
            }
        }

        protected void btnAddReceiveProduct_OnClick(object sender, EventArgs e)
        {
            try
            {

                Regex regexForQuentity = new Regex("^[0-9]*$");
                #region validation

                //if (rdropColor.SelectedIndex <= 0)
                //{
                //    Alert.Show("Please enter a Color");
                //    rdropColor.Focus();
                //    return;
                //}
                if (rtxtReceiveQuantity.Text == string.Empty || !regexForQuentity.IsMatch(rtxtReceiveQuantity.Text))
                {
                    Alert.Show("Please enter a receive quentity");
                    rtxtReceiveQuantity.Focus();
                    return;
                }
                int recQuentity = int.Parse(rtxtReceiveQuantity.Text);
                if (recQuentity > int.Parse(lblLCQuantity.Text))
                {

                    Alert.Show("Invoice quantity can not be greater then order quentity");
                    rtxtReceiveQuantity.Focus();
                    return;
                }

                int balance = 0;
                if (ItemBalance.Contains(lblProductId.Text))
                {
                    balance = int.Parse(ItemBalance[lblProductId.Text].ToString());
                }
                else
                {
                    balance = int.Parse(lblLCQuantity.Text) - int.Parse(lblReceivedQuantity.Text);
                }
                #endregion

                TempReceiverInfo objTempReceiverInfo =
                    receiverInfo.Find(x => x.ProductId == int.Parse(lblProductId.Text) && x.color == rdropColor.SelectedValue);
                if (objTempReceiverInfo != null)
                {
                    if (objTempReceiverInfo.color == "0" || objTempReceiverInfo.color == null)
                    {
                        objTempReceiverInfo = new TempReceiverInfo();
                        balance = balance - objTempReceiverInfo.ReceiveQuantity;

                        if (balance < int.Parse(rtxtReceiveQuantity.Text))
                        {
                            Alert.Show("This Amount of product is not available");
                            return;
                        }

                    }
                    else if (objTempReceiverInfo.color == rdropColor.SelectedValue)
                    {
                        balance = balance + objTempReceiverInfo.ReceiveQuantity;

                        if (balance < int.Parse(rtxtReceiveQuantity.Text))
                        {
                            Alert.Show("This Amount of product is not available");
                            return;
                        }
                        //int receiveBalance = objTempReceiverInfo.ReceiveQuantity;

                        receiverInfo.Remove(objTempReceiverInfo);
                    }
                }
                else
                {
                    objTempReceiverInfo = new TempReceiverInfo();
                }

                balance = balance - int.Parse(rtxtReceiveQuantity.Text);
                if (ItemBalance.Contains(lblProductId.Text))
                {
                    ItemBalance.Remove(lblProductId.Text);
                }
                ItemBalance.Add(lblProductId.Text, balance);
                Session["ItemBalance"] = ItemBalance;

                objTempReceiverInfo.Id = lblDetailsId.Text == string.Empty ? 0 : int.Parse(lblDetailsId.Text);
                objTempReceiverInfo.PIquantity = int.Parse(lblLCQuantity.Text);
                objTempReceiverInfo.ProductCode = lblProductCode.Text;
                objTempReceiverInfo.ProductId = int.Parse(lblProductId.Text);
                objTempReceiverInfo.ProductName = lblProductName.Text;
                objTempReceiverInfo.ProductUnit = int.Parse(lblUnite.Text);
                ProductUnit objProductUnit = new ProductUnit().GetProductUnitById(int.Parse(lblUnite.Text));
                objTempReceiverInfo.ProductUnitName = objProductUnit.UnitCode;
                objTempReceiverInfo.ReceiveQuantity = int.Parse(rtxtReceiveQuantity.Text);
                objTempReceiverInfo.color = rdropColor.SelectedValue;
                objTempReceiverInfo.colorName = rdropColor.SelectedItem.Text;
                objTempReceiverInfo.amount = decimal.Parse(lblAmount.Text) * int.Parse(rtxtReceiveQuantity.Text);


                if (receiverInfo.Count == 0)
                    receiverInfo = new List<TempReceiverInfo>();

                Session["receiverInfo"] = receiverInfo;

                receiverInfo.Add(objTempReceiverInfo);

                this.LoadRecciverDetailsGrid();


            }
            catch (Exception ex)
            {
                Alert.Show("Data was not add succesfully" + ex);
            }
        }

        protected void rdropProductCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadColor(int.Parse(rdropProductCode.SelectedValue));


            try
            {
                DataTable dtreceiverDetils = new ReceiverDetails().GetPIDetailsTableByProductId(rtxtPINumber.Text,
               int.Parse(rdropProductCode.SelectedValue));
                PIMaster objPIM = new PIMaster().GetAllPImasterbyPIno(rtxtPINumber.Text);
                long pId = objPIM.Id;
                PIDetails objPID = new PIDetails().GetAllPIDetailsOrderDetailsByIdProductId(int.Parse(pId.ToString()), int.Parse(rdropProductCode.SelectedValue));


                if (dtreceiverDetils.Rows.Count != 0)
                {
                    lblProductId.Text = dtreceiverDetils.Rows[0]["ProductId"].ToString();
                    lblProductCode.Text = dtreceiverDetils.Rows[0]["ProductCode"].ToString();
                    lblProductName.Text = dtreceiverDetils.Rows[0]["ProductName"].ToString();
                    lblLCQuantity.Text = dtreceiverDetils.Rows[0]["PIquantity"].ToString();
                    lblReceivedQuantity.Text = dtreceiverDetils.Rows[0]["ReceivedQuantity"].ToString();
                    rdropColor.SelectedValue = dtreceiverDetils.Rows[0]["color"].ToString() == "" ? "0" : dtreceiverDetils.Rows[0]["color"].ToString();
                    rtxtReceiveQuantity.Text = dtreceiverDetils.Rows[0]["ReceiveQuantity"].ToString();
                    lblUnite.Text = dtreceiverDetils.Rows[0]["ProductUnit"].ToString() == "" ? "0" : dtreceiverDetils.Rows[0]["ProductUnit"].ToString();
                    lblAmount.Text = objPID.PIUnitePrice.ToString();
                    int balance = 0;
                    if (ItemBalance.Contains(lblProductId.Text))
                    {
                        balance = int.Parse(ItemBalance[lblProductId.Text].ToString());
                    }
                    else
                    {
                        balance = int.Parse(lblLCQuantity.Text) - int.Parse(lblReceivedQuantity.Text);
                    }
                    lblRemaining.Text = balance.ToString();
                }
                else
                {
                    Alert.Show("There is no P.I. created for this product .Please create a P.I.");
                }

            }
            catch (Exception ex)
            {

                Alert.Show("Something is going wrong to load data." + ex);
            }

        }

        protected void rdropProductCode_OnDataBound(object sender, EventArgs e)
        {
            rdropProductCode.Items.Insert(0, new RadComboBoxItem());
            rdropProductCode.SelectedIndex = 0;


        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                #region validation

                if (rdtPIDate.SelectedDate == null)
                {
                    Alert.Show("Please enter a Date.");
                    rdtPIDate.Focus();
                    return;
                }
                if (rdropVandorName.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Vendor name.");
                    rdropVandorName.Focus();
                    return;
                }
                if (rdropLCNo.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a LC no.");
                    rdropLCNo.Focus();
                    return;
                }
                if (rtxtPINumber.Text == string.Empty)
                {
                    Alert.Show("Please enter a PI number.");
                    rtxtPINumber.Focus();
                    return;
                }
                if (rdropWareHouse.SelectedIndex <= 0)
                {
                    Alert.Show("Please select a Ware house.");
                    rdropWareHouse.Focus();
                    return;
                }

                //if (rtxtInvoiceNo.Text == string.Empty)
                //{
                //    Alert.Show("Please enter a Invoice No.");
                //    rtxtInvoiceNo.Focus();
                //    return;
                //}

                #endregion

                DataTable itemJournalDt = new DataTable();
                if (!bool.Parse(lblisNewEntry.Text))
                    itemJournalDt = new ItemJournalMaster().GetItemJournalMasterByRefId("Receiving",
                        rtxtReceiveCode.Text);

                if (IMEIFileUpload.HasFile)
                {

                    //string PINo = rtxtReceiveCode.Text == string.Empty ? "Empty" : rtxtReceiveCode.Text;
                    string extension = Path.GetExtension(IMEIFileUpload.FileName);
                    string fileName = "_IMEI" + extension;
                    lblImageName.Text = fileName;
                    if (extension == ".csv" || extension == ".xlsx" || extension == ".xls")
                    {
                        DirectoryInfo directory = new DirectoryInfo(Server.MapPath("images/IMEI_File"));
                        if (!directory.Exists)
                            directory.Create();

                        string UploadFilePath = Path.Combine(Server.MapPath("images/P.I._File"), fileName);
                        FileInfo file = new FileInfo(UploadFilePath);

                        if (!file.Exists)
                            file.Delete();

                        IMEIFileUpload.SaveAs(UploadFilePath);
                    }
                    else
                    {
                        Alert.Show("Please upload 'CSV' or 'EXCEL' type file.");
                        return;
                    }
                }


                ReceiverMaster objReceiverMaster = new ReceiverMaster();

                objReceiverMaster.CompanyId = _company.Id;

                objReceiverMaster.VendorId = int.Parse(rdropVandorName.SelectedValue);
                objReceiverMaster.LCId = int.Parse(rdropLCNo.SelectedValue);
                objReceiverMaster.PIId = int.Parse(lblPIId.Text);
                objReceiverMaster.ReceivedBy = rtxtReceivedBy.Text == string.Empty ? "" : rtxtReceivedBy.Text;
                objReceiverMaster.InvoiceNo = rtxtInvoiceNo.Text;
                objReceiverMaster.WareHouseId = int.Parse(rdropWareHouse.SelectedValue);
                objReceiverMaster.IMEIName = rtxtIMEIFileName.Text;
                objReceiverMaster.IMEIUplodePath = lblImageName.Text;
                objReceiverMaster.ReceivingDate = DateTime.Parse(rdtPIDate.SelectedDate.ToString());
                objReceiverMaster.IsInvoiceCreated = false;
                objReceiverMaster.TotalAmount = 0;
                objReceiverMaster.UserId = _user.Id;
                objReceiverMaster.Status = lblStatus.Text;


                int success;
                if (bool.Parse(lblisNewEntry.Text))
                {
                    rtxtReceiveCode.Text = new ReceiverMaster().GetlastReceiverCode();
                    objReceiverMaster.ReceivingCode = rtxtReceiveCode.Text;
                    success = objReceiverMaster.InsertReceiverMaster();
                    lblisNewEntry.Text = "false";
                }
                else
                {
                    objReceiverMaster.Id = int.Parse(lblId.Text);
                    objReceiverMaster.ReceivingCode = rtxtReceiveCode.Text;
                    success = objReceiverMaster.UpdateReceiverMaster();
                }
                if (success == 0)
                {
                    Alert.Show("Data is not save succesfully");
                }
                else
                {
                    //------Start Insert Item Journal Master Data------//
                    int RecevingId = 0;
                    decimal totalAmount = 0;
                    if (receiverInfo.Count != 0)
                    {
                        if (lblId.Text == string.Empty)
                        {
                            RecevingId = new ReceiverMaster().GetMaxReciverMasterId();
                        }
                        else
                        {
                            RecevingId = int.Parse(lblId.Text);
                        }

                        ReceiverMaster objReceivermasterinfo = new ReceiverMaster().GetReceiverMasterById(RecevingId, _company.Id);

                        int journalid = 0;
                        if (itemJournalDt.Rows.Count > 0)
                            journalid = int.Parse(itemJournalDt.Rows[0][0].ToString());

                        ItemJournalMaster objDetailsJournal = new ItemJournalMaster();



                        objDetailsJournal.TransactionDate = DateTime.Now;
                        objDetailsJournal.TransactionType = "Receiving";
                        objDetailsJournal.SourceId = objReceivermasterinfo.ReceivingCode;
                        objDetailsJournal.UserId = _user.Id;
                        objDetailsJournal.WareHouseId = int.Parse(rdropWareHouse.SelectedValue);

                        InventorySetup objInventorySetup = new InventorySetup().GetInventorySetupById(1);

                        objDetailsJournal.WareHouseIdFrom = objInventorySetup.PurchaseLocationId;

                        if (journalid > 0)
                        {
                            objDetailsJournal.Id = journalid;
                            success = objDetailsJournal.UpdateItemJournalMaster();
                        }
                        else
                            success = objDetailsJournal.InsertItemJournalMaster();

                    //------End Insert Item Journal Master Data------//
                    if (lblId.Text != string.Empty && lblId.Text != "0")
                        LoadReceiverDetails(int.Parse(lblId.Text));

                    foreach (TempReceiverInfo tempReceiverInfo in receiverInfo)
                    {
                        ReceiverDetails objReceiverDetails = new ReceiverDetails();

                        ItemJournalDetails objItemDetails = new ItemJournalDetails();
                        List<ItemLedger> lstItemLedgers;

                        int Openingbalance = 0;

                        lblDetailsId.Text = tempReceiverInfo.Id.ToString();
                        if (lblId.Text == string.Empty)
                        {
                            objReceiverDetails.MasterId = new ReceiverMaster().GetMaxReciverMasterId();
                            lblId.Text = new ReceiverMaster().GetMaxReciverMasterId().ToString();
                        }
                        else
                            objReceiverDetails.MasterId = int.Parse(lblId.Text);

                        objReceiverDetails.ProductCode = tempReceiverInfo.ProductCode;
                        objReceiverDetails.ProductId = tempReceiverInfo.ProductId;
                        objReceiverDetails.LCQuantity = tempReceiverInfo.PIquantity;
                        objReceiverDetails.ReceiveQuantity = tempReceiverInfo.ReceiveQuantity;
                        objReceiverDetails.Color = tempReceiverInfo.color;
                        objReceiverDetails.ReceivedQuantity = tempReceiverInfo.receivedQuantity;
                        objReceiverDetails.Unit = tempReceiverInfo.ProductUnit;
                        totalAmount += tempReceiverInfo.amount;

                        if (lblDetailsId.Text == string.Empty || lblDetailsId.Text == "0")
                        {
                            success = objReceiverDetails.InsertReceiverDetails();
                        }
                        else
                        {
                            objReceiverDetails.Id = int.Parse(lblDetailsId.Text);
                            success = objReceiverDetails.UpdateReceiverDetails();
                        }
                        //------Start Get Item Ledger Data------//
                        if (int.Parse(objReceiverDetails.Color) == 0)
                        {
                            lstItemLedgers =
                                new ItemLedger().GetAllItemLedgersByItemIdUnit(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit);
                        }
                        else
                        {
                            lstItemLedgers = new ItemLedger().GetAllItemLedgersByItemIdUnitColor(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit, int.Parse(tempReceiverInfo.color));
                        }
                        if (lstItemLedgers.Count != 0)
                        {
                            if (int.Parse(objReceiverDetails.Color) == 0)
                            {
                                ItemLedger objItemLedger =
                                    new ItemLedger().GetItemLedgerByItemIdUnit(tempReceiverInfo.ProductId,
                                        tempReceiverInfo.ProductUnit);
                                Openingbalance = objItemLedger.Balance;
                            }
                            else
                            {
                                ItemLedger objItemLedger = new ItemLedger().GetItemLedgerByItemIdUnitColor(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit, int.Parse(tempReceiverInfo.color));
                                Openingbalance = objItemLedger.Balance;
                            }
                        }
                        //------End Get Item Ledger Data------//
                        PIDetails objPiDetails = new PIDetails().GetAllPIDetailsOrderDetailsBymasterIdProductId(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit);
                        //------Start Insert data in Item Details------//
                        if (journalid > 0)
                            objItemDetails.MasterId = journalid;
                        else
                            objItemDetails.MasterId = new ItemJournalMaster().GetMaxItemDetailsJournalMasterId();

                        objItemDetails.ProductId = tempReceiverInfo.ProductId;
                        objItemDetails.ProductName = tempReceiverInfo.ProductName;
                        objItemDetails.Color = int.Parse(tempReceiverInfo.color);
                        objItemDetails.Unit = tempReceiverInfo.ProductUnit;
                        objItemDetails.OpeningBalance = Openingbalance;
                        objItemDetails.QuantityIn = tempReceiverInfo.ReceiveQuantity;
                        objItemDetails.QuantityOut = 0;
                        objItemDetails.ClosingBalance = 0;
                        objItemDetails.Rate = objPiDetails.PIUnitePrice;

                        Int64 journalDetailsId = 0;
                        ItemJournalDetails journalDetails =
                            new ItemJournalDetails().GetItemJournalDetailsByItem(objItemDetails.MasterId, objItemDetails.ProductId, objItemDetails.Color);
                        journalDetailsId = journalDetails.Id;

                        if (journalDetailsId == 0)
                            success = objItemDetails.InsertItemJournalDetails();
                        else
                        {
                            objItemDetails.Id = journalDetailsId;
                            success = objItemDetails.UpdateItemJournalDetails();
                        }
                        //------End Insert data in Item Details------//

                        //------Start Insert data in Item Ledger------//
                        if (lstItemLedgers.Count != 0)
                        {
                            if (int.Parse(objReceiverDetails.Color) == 0)
                            {
                                ItemLedger objItemLedger = new ItemLedger().GetItemLedgerByItemIdUnit(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit);
                                this.UpdateItemLedger(tempReceiverInfo, tempReceiverInfo.ReceiveQuantity, 0, objItemLedger.Id);
                            }
                            else
                            {
                                ItemLedger objItemLedger = new ItemLedger().GetItemLedgerByItemIdUnitColor(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit, int.Parse(tempReceiverInfo.color));
                                this.UpdateItemLedger(tempReceiverInfo, tempReceiverInfo.ReceiveQuantity, 0, objItemLedger.Id);
                            }
                        }
                        else
                        {
                            if (int.Parse(objReceiverDetails.Color) == 0)
                            {
                                this.UpdateItemLedger(tempReceiverInfo, tempReceiverInfo.ReceiveQuantity, 0, 0);
                            }
                            else
                            {
                                this.UpdateItemLedger(tempReceiverInfo, tempReceiverInfo.ReceiveQuantity, 0, 0);
                            }
                        }
                        //------End Insert data in Item Ledger------//
                        if (success == 0)
                        {
                            Alert.Show("Receiver Master data save successfully but Receiver details data not save succesfully");
                        }
                        else
                        {
                            Alert.Show("Data save succesfully");
                            
                        }

                    }
                    LoadReceiverDetails(int.Parse(lblId.Text));
                    ReceiverMaster resMaster = new ReceiverMaster();

                    resMaster.Id = new ReceiverMaster().GetMaxReciverMasterId();
                    resMaster.TotalAmount = totalAmount;
                    int updatesuccess = resMaster.UpdateReceiverMasterByAmount();
                    if (updatesuccess == 0)
                    {
                        Alert.Show("Recever Update data is not save succesfully");
                    }
                    else
                    {
                        if (_department != "All")
                        {
                            if (_department.ToLower().Contains("logistics"))
                            btnApprove.Visible = true;    
                        }
                        else
                            btnApprove.Visible = true;

                        this.ClearAllInfo();
                    }
                    }
                }

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save Reveiver info " + ex);
            }
        }

        private void ClearAllInfo()
        {
            RadGridAddReceivedDetails.DataSource = new string[] { };
            RadGridAddReceivedDetails.Rebind();


            rtxtReceiveCode.Text = "";
            rdropVandorName.SelectedIndex = 0;
            rdropLCNo.SelectedIndex = 0;
            lblPIId.Text = "";
            rtxtReceivedBy.Text = "";
            rtxtInvoiceNo.Text = "";
            rdropWareHouse.SelectedIndex = 0;
            rtxtIMEIFileName.Text = "";            
            lblImageName.Text = "";
            rdtPIDate.SelectedDate = null;
            lblisNewEntry.Text = "true";
            lblId.Text = "";
            lblStatus.Text = "Created";
            rtxtPINumber.Text = "";
            rdropProductCode.SelectedIndex = 0;
            lblProductName.Text = "";
            rtxtReceiveQuantity.Text = "";
            btnApprove.Visible = false;
            RadGridAddReceivedDetails.DataSource = new string[] { };
            RadGridAddReceivedDetails.DataBind();
            receiverInfo = new List<TempReceiverInfo>();
            Session["receiverInfo"] = receiverInfo;
            ItemBalance=new Hashtable();
            Session["ItemBalance"] = ItemBalance;
        }

        private void clearDetailsInfo()
        {

        }

        private int UpdateItemLedger(TempReceiverInfo tmpDetails, int totalIn, int totalOut, long ledgerId)
        {
            ItemLedger objItemLedger = new ItemLedger();
            //--------Insert data in Item Legder----------//
            objItemLedger.ItemId = tmpDetails.ProductId;
            objItemLedger.Unit = tmpDetails.ProductUnit;
            objItemLedger.Color = tmpDetails.color.ToString();
            objItemLedger.WareHouseId = int.Parse(rdropWareHouse.SelectedValue);
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

        protected void rdropWareHouse_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            WareHouse objWareHouse = new WareHouse().GetWareHouseById(int.Parse(rdropWareHouse.SelectedValue), _company.Id);
            EmployeeInformation objEmployeeInformation =
                new EmployeeInformation().GetEmployeeInformationById(int.Parse(objWareHouse.Incharge),
                    _company.Id);
            rtxtReceivedBy.Text = objEmployeeInformation.EmployeeName;
        }

        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            this.ClearAllInfo();
        }

        protected void btnApprove_OnClick(object sender, EventArgs e)
        {
        //    //------Start Insert Item Journal Master Data------//
        //    DataTable itemJournalDt = new DataTable();
        //    int success = 0;
        //    int RecevingId = 0;
        //    if (receiverInfo.Count != 0)
        //    {
        //        if (lblId.Text == string.Empty)
        //        {
        //            RecevingId = new ReceiverMaster().GetMaxReciverMasterId();
        //        }
        //        else
        //        {
        //            RecevingId = int.Parse(lblId.Text);
        //        }

        //        ReceiverMaster objReceivermasterinfo = new ReceiverMaster().GetReceiverMasterById(RecevingId,
        //            _company.Id);

        //        int journalid = 0;
        //        if (itemJournalDt.Rows.Count > 0)
        //            journalid = int.Parse(itemJournalDt.Rows[0][0].ToString());

        //        ItemJournalMaster objDetailsJournal = new ItemJournalMaster();


        //        decimal totalAmount = 0;
        //        objDetailsJournal.TransactionDate = DateTime.Now;
        //        objDetailsJournal.TransactionType = "Receiving";
        //        objDetailsJournal.SourceId = objReceivermasterinfo.ReceivingCode;
        //        objDetailsJournal.UserId = _user.Id;
        //        objDetailsJournal.WareHouseId = int.Parse(rdropWareHouse.SelectedValue);

        //        InventorySetup objInventorySetup = new InventorySetup().GetInventorySetupById(1);

        //        objDetailsJournal.WareHouseIdFrom = objInventorySetup.PurchaseLocationId;

        //        if (journalid > 0)
        //        {
        //            objDetailsJournal.Id = journalid;
        //            success = objDetailsJournal.UpdateItemJournalMaster();
        //        }
        //        else
        //            success = objDetailsJournal.InsertItemJournalMaster();

        //        //------End Insert Item Journal Master Data------//

        //        foreach (TempReceiverInfo tempReceiverInfo in receiverInfo)
        //        {
        //            ReceiverDetails objReceiverDetails = new ReceiverDetails();

        //            ItemJournalDetails objItemDetails = new ItemJournalDetails();
        //            List<ItemLedger> lstItemLedgers;

        //            int Openingbalance = 0;

        //            lblDetailsId.Text = tempReceiverInfo.Id.ToString();


        //            if (int.Parse(tempReceiverInfo.color) == 0)
        //            {
        //                lstItemLedgers =
        //                    new ItemLedger().GetAllItemLedgersByItemIdUnit(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit);
        //            }
        //            else
        //            {
        //                lstItemLedgers = new ItemLedger().GetAllItemLedgersByItemIdUnitColor(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit, int.Parse(tempReceiverInfo.color));
        //            }
        //            if (lstItemLedgers.Count != 0)
        //            {
        //                if (int.Parse(tempReceiverInfo.color) == 0)
        //                {
        //                    ItemLedger objItemLedger =
        //                        new ItemLedger().GetItemLedgerByItemIdUnit(tempReceiverInfo.ProductId,
        //                            tempReceiverInfo.ProductUnit);
        //                    Openingbalance = objItemLedger.Balance;
        //                }
        //                else
        //                {
        //                    ItemLedger objItemLedger = new ItemLedger().GetItemLedgerByItemIdUnitColor(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit, int.Parse(tempReceiverInfo.color));
        //                    Openingbalance = objItemLedger.Balance;
        //                }
        //            }
        //            //------End Get Item Ledger Data------//
        //            PIDetails objPiDetails = new PIDetails().GetAllPIDetailsOrderDetailsBymasterIdProductId(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit);
        //            //------Start Insert data in Item Details------//
        //            if (journalid > 0)
        //                objItemDetails.MasterId = journalid;
        //            else
        //                objItemDetails.MasterId = new ItemJournalMaster().GetMaxItemDetailsJournalMasterId();

        //            objItemDetails.ProductId = tempReceiverInfo.ProductId;
        //            objItemDetails.ProductName = tempReceiverInfo.ProductName;
        //            objItemDetails.Color = int.Parse(tempReceiverInfo.color);
        //            objItemDetails.Unit = tempReceiverInfo.ProductUnit;
        //            objItemDetails.OpeningBalance = Openingbalance;
        //            objItemDetails.QuantityIn = tempReceiverInfo.ReceiveQuantity;
        //            objItemDetails.QuantityOut = 0;
        //            objItemDetails.ClosingBalance = 0;
        //            objItemDetails.Rate = objPiDetails.PIUnitePrice;

        //            Int64 journalDetailsId = 0;
        //            ItemJournalDetails journalDetails =
        //                new ItemJournalDetails().GetItemJournalDetailsByItem(objItemDetails.MasterId, objItemDetails.ProductId, objItemDetails.Color);
        //            journalDetailsId = journalDetails.Id;

        //            if (journalDetailsId == 0)
        //                success = objItemDetails.InsertItemJournalDetails();
        //            else
        //            {
        //                objItemDetails.Id = journalDetailsId;
        //                success = objItemDetails.UpdateItemJournalDetails();
        //            }
        //            //------End Insert data in Item Details------//

        //            //------Start Insert data in Item Ledger------//
        //            if (lstItemLedgers.Count != 0)
        //            {
        //                if (int.Parse(tempReceiverInfo.color) == 0)
        //                {
        //                    ItemLedger objItemLedger = new ItemLedger().GetItemLedgerByItemIdUnit(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit);
        //                    this.UpdateItemLedger(tempReceiverInfo, tempReceiverInfo.ReceiveQuantity, 0, objItemLedger.Id);
        //                }
        //                else
        //                {
        //                    ItemLedger objItemLedger = new ItemLedger().GetItemLedgerByItemIdUnitColor(tempReceiverInfo.ProductId, tempReceiverInfo.ProductUnit, int.Parse(tempReceiverInfo.color));
        //                    this.UpdateItemLedger(tempReceiverInfo, tempReceiverInfo.ReceiveQuantity, 0, objItemLedger.Id);
        //                }
        //            }
        //            else
        //            {
        //                if (int.Parse(tempReceiverInfo.color) == 0)
        //                {
        //                    this.UpdateItemLedger(tempReceiverInfo, tempReceiverInfo.ReceiveQuantity, 0, 0);
        //                }
        //                else
        //                {
        //                    this.UpdateItemLedger(tempReceiverInfo, tempReceiverInfo.ReceiveQuantity, 0, 0);
        //                }
        //            }
        //            if (success == 0)
        //            {
        //                Alert.Show("data not Approved");
        //            }
        //            else
        //            {
        //                Alert.Show("Data Approved succesfully");

        //            }
        //            //------End Insert data in Item Ledger------//
        //        }

        //        if (success != 0)
        //        {
        //            int id = int.Parse(lblId.Text);
        //            string status = "Approved";

        //            new ReceiverMaster().ChangeReceivingStatus(id, status);

        //            this.ClearAllInfo();
        //        }
        //    }
        }
    }
}