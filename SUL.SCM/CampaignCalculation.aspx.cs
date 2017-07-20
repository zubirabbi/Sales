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
    public partial class CampaignCalculation : System.Web.UI.Page
    {
        private Users _user;
        private Company _company;

        private string _department;

        private AppPermission PermissionUser;

        private DataTable dtDealerInfo;
        private List<TempCampaignCal> tempCampaignCal;


        public class TempCampaignCal
        {
            public int DealerId { get; set; }
            public string DealerName { get; set; }
            public decimal TotalAmount { get; set; }
            public int CampaignId { get; set; }
            public string CampaignName { get; set; }
            public decimal Discount { get; set; }
            public decimal Amount { get; set; }
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
            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Dealer Balance Report") : new AppFunctionality().GetAppFunctionalityId("Dealer Balance Report", int.Parse(lblsource.Text));
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
        private void LoadCampaignCode()
        {
            try
            {
                DataTable dtCampaignCode = new CampaignMaster().GetAllCampaignInfoByAdjust();

                rdropCampaignCode.DataTextField = "CampaignCode";
                rdropCampaignCode.DataValueField = "Id";
                rdropCampaignCode.DataSource = dtCampaignCode;
                rdropCampaignCode.DataBind();

                rdropCampaignCode.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load data" + ex);
            }
        }

        private void LoadCampaignCalculation()
        {
            try
            {
                DataTable dtCampaignCalculation =
                    new RequisitionMaster().GetInformationByCampaignId(int.Parse(rdropCampaignCode.SelectedValue));
                if (dtCampaignCalculation.Rows.Count == 0)
                {
                    RadGridCampaignCal.DataSource = new string[] { };
                    RadGridCampaignCal.DataBind();
                    return;
                }

                RadGridCampaignCal.DataSource = dtCampaignCalculation;
                RadGridCampaignCal.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load data." + ex);
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

            if (Session["dtDealerInfo"] != null)
            {
                dtDealerInfo = (DataTable)Session["dtDealerInfo"];
            }
            else
            {
                dtDealerInfo = new DataTable();
            }
            if (Session["tempCampaignCal"] != null)
                tempCampaignCal = (List<TempCampaignCal>)Session["tempCampaignCal"];
            else
                tempCampaignCal = new List<TempCampaignCal>();

            if (!IsPostBack)
            {

                if (_user.EmployeeId != 0)
                {
                    Department objDepartment = new Department().GetEmployeeDepartment(_user.EmployeeId);
                    _department = objDepartment.DepartmentName;
                }
                this.LoadCampaignCode();

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

        protected void rdropCampaignCode_OnDataBound(object sender, EventArgs e)
        {
            rdropCampaignCode.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropCampaignCode_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            tempCampaignCal = new List<TempCampaignCal>();
            Session["tempCampaignCal"] = null;
            if (rdropCampaignCode.SelectedIndex <= 0)
            {
                Alert.Show("Please select CampaignCode");
                rdropCampaignCode.Focus();
                return;
            }
            GetCampaignCalculationData();
        }

        private void GetCampaignCalculationData()
        {
            try
            {
                CampaignMaster objCapCampaignMaster =
                    new CampaignMaster().GetCampaignMasterById(int.Parse(rdropCampaignCode.SelectedValue));

                lblStartDate.Text = objCapCampaignMaster.StartDate.ToString("MMM dd,yyyy");
                lblEndDate.Text = objCapCampaignMaster.EndDate.ToString("MMM dd,yyyy");


                List<CampaignDetails> lstCampaignDetailses =
                    new CampaignDetails().GetAllProductDetaislbyCampaignId(int.Parse(rdropCampaignCode.SelectedValue));

                DataTable dtInvoice = new InvoiceMaster().SearchInvoiceDateItem(DateTime.Parse(lblStartDate.Text),
                    DateTime.Parse(lblEndDate.Text));

                foreach (DataRow dr in dtInvoice.Rows)
                {
                    decimal Amount = decimal.Parse(dr["Total"].ToString());



                    List<CampaignDetails> objCampaignDetails =
                        lstCampaignDetailses.Where(x => Amount >= x.StartValue && Amount <= x.EndValue).ToList();
                    if (objCampaignDetails != null)
                    {
                        foreach (CampaignDetails cmp in objCampaignDetails)
                        {
                            List<CalculationCampaign> lstCalculationCampaigns =
                                new CalculationCampaign().GetAllCalculationCampaignByCampaignId(
                              int.Parse(cmp.CampaignId.ToString()), cmp.CampaignName, int.Parse(dr["DealerId"].ToString()));

                            if (lstCalculationCampaigns.Count == 0)
                            {
                                TempCampaignCal objCampaignCal = new TempCampaignCal();
                                objCampaignCal.DealerId = int.Parse(dr["DealerId"].ToString());
                                objCampaignCal.DealerName = dr["Name"].ToString();
                                objCampaignCal.TotalAmount = decimal.Parse(dr["Total"].ToString());
                                objCampaignCal.CampaignName = cmp.CampaignName;
                                objCampaignCal.CampaignId = int.Parse(cmp.CampaignId.ToString());
                                decimal Dis = (cmp.DiscountPcnt * Amount) / 100;
                                objCampaignCal.Discount = Dis;
                                objCampaignCal.Amount = cmp.OfferAmount;
                                tempCampaignCal.Add(objCampaignCal);
                                break;
                            }
                        }
                    }
                }
                Session["tempCampaignCal"] = tempCampaignCal;

                if (tempCampaignCal.Count == 0)
                {
                    RadGridCampaignCal.DataSource = new string[] { };
                    RadGridCampaignCal.DataBind();
                    return;
                }

                RadGridCampaignCal.DataSource = tempCampaignCal;
                RadGridCampaignCal.DataBind();
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load data." + ex);
            }
        }

        protected void RadGridCampaignCal_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            this.LoadCampaignCalculation();
        }

        protected void RadGridCampaignCal_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            this.LoadCampaignCalculation();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (tempCampaignCal.Count != 0)
                {
                    foreach (GridDataItem item in RadGridCampaignCal.Items)
                    {
                        //GridDataItem Item1 = (GridDataItem)item;
                        CheckBox isCheck = (CheckBox)item.FindControl("chkIsUpdate");
                        bool CheckView = isCheck.Checked;
                        int success = 0;
                        if (CheckView)
                        {
                            CalculationCampaign objcaCalculationCampaign = new CalculationCampaign();

                            objcaCalculationCampaign.CampaignId = int.Parse(item["colCampaignId"].Text);
                            objcaCalculationCampaign.DealerId = int.Parse(item["colDealerId"].Text);
                            objcaCalculationCampaign.DealerName = item["colDealerName"].Text;
                            objcaCalculationCampaign.CampaignName = item["colCampaignCode"].Text;
                            objcaCalculationCampaign.Amount = decimal.Parse(item["colAmount"].Text);
                            objcaCalculationCampaign.Discount = decimal.Parse(item["colDiscount"].Text);
                            objcaCalculationCampaign.ApproveBy = _user.Id;
                            objcaCalculationCampaign.IsAppiled = true;

                            success = objcaCalculationCampaign.InsertCalculationCampaign();

                            if (success == 0)
                                Alert.Show("data is not save succesfully");
                            else
                                Alert.Show("data save succesfully");
                        }
                        if (success != 0)
                            this.ClearAllInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to save data. " + ex);
            }
        }

        private void ClearAllInfo()
        {
            rdropCampaignCode.SelectedIndex = 0;
            tempCampaignCal = new List<TempCampaignCal>();
            Session["tempCampaignCal"] = null;

            RadGridCampaignCal.DataSource = new string[] { };
            RadGridCampaignCal.DataBind();

        }
    }
}