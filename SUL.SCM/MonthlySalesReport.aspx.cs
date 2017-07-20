using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Crypto.Tls;
using PdfReport;
using PdfReport.Core;
using PdfReport.Templates;
using SUL.Bll;
using Telerik.Web.UI;
using Region = SUL.Bll.Region;


namespace SUL.SCM
{
    public partial class MonthlySalesReport : System.Web.UI.Page
    {
        private UserRoleInfo _role;
        private Users _user;
        private Company _company;
        private string _department;

        private DataTable dtdealerSummary;
        private DataTable dtSummary;
        private DataTable dtProductSummary;

        private AppPermission PermissionUser;
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
            
            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Sales Report") : new AppFunctionality().GetAppFunctionalityId("Sales Report", int.Parse(lblsource.Text));
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
        private bool IsValidInsertForUser()
        {
            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Sales Report") : new AppFunctionality().GetAppFunctionalityId("Sales Report", int.Parse(lblsource.Text));
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
            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Monthly Sales Report") : new AppFunctionality().GetAppFunctionalityId("Monthly Sales Report", int.Parse(lblsource.Text));
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
            int FunctionalId = 0;

            FunctionalId = int.Parse(lblsource.Text) == 0 ? new AppFunctionality().GetAppFunctionalityId("Monthly Sales Report") : new AppFunctionality().GetAppFunctionalityId("Monthly Sales Report", int.Parse(lblsource.Text));
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

                rdropDealer.DataTextField = "DealerInfo";
                rdropDealer.DataValueField = "Id";
                rdropDealer.DataSource = dtDealerInfo;
                rdropDealer.DataBind();

                rdropDealer.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
            }
        }
        private void LoadDealerInfo(int area)
        {
            try
            {
                DataTable dtDealerInfo = new DealerInformation().GetAllDealerInformationViewByArea(area);

                rdropDealer.DataTextField = "DealerInfo";
                rdropDealer.DataValueField = "Id";
                rdropDealer.DataSource = dtDealerInfo;
                rdropDealer.DataBind();

                rdropDealer.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("something is going wrong to load dealer info." + ex);
            }
        }
        private void LoadProductInfo()
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
        private void LoadColor()
        {
            try
            {
                List<ListTable> lstListTables = new ListTable().GetAllListTableByType("Color");
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
        private void LoadSetupChennalDesignation()
        {
            try
            {
                SetupChannel objsetupChannel = new SetupChannel().GetAllSetupChannelByChannels("Channel Specialized");
                int desigId = objsetupChannel.DesignationId;

                DataTable dtRmployeeInfo =
                    new EmployeeInformation().GetAllEmployeeInformationByDesignationIdbyTable(desigId);

                rdropCs.DataTextField = "empAllInfo";
                rdropCs.DataValueField = "Id";
                rdropCs.DataSource = dtRmployeeInfo;
                rdropCs.DataBind();
                if (dtRmployeeInfo.Rows.Count == 2)
                    rdropCs.SelectedIndex = 1;
                else
                    rdropCs.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load data" + ex);
            }
        }

        private void LoadSetupJrChennalDesignation()
        {
            try
            {
                SetupChannel objsetupChannel = new SetupChannel().GetAllSetupChannelByChannels("Jr. Channel Specialized");
                int desigId = objsetupChannel.DesignationId;

                DataTable dtRmployeeInfo =
                    new EmployeeInformation().GetAllEmployeeInformationByDesignationIdbyTable(desigId);

                rdropJcs.DataTextField = "empAllInfo";
                rdropJcs.DataValueField = "Id";
                rdropJcs.DataSource = dtRmployeeInfo;
                //if (dtRmployeeInfo.Rows.Count == 2)
                //    rdropJrCS.SelectedIndex = 1;
                //else
                //    rdropJrCS.SelectedIndex = 0;
                rdropJcs.DataBind();

            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load data" + ex);
            }
        }
        private void LoadSetupChennalManager()
        {
            try
            {
                SetupChannel objsetupChannel = new SetupChannel().GetAllSetupChannelByChannels("Channel Manager");
                int desigId = objsetupChannel.DesignationId;

                DataTable dtRmployeeInfo =
                     new EmployeeInformation().GetAllEmployeeInformationByDesignationIdbyTable(desigId);

                rdropCM.DataTextField = "empAllInfo";
                rdropCM.DataValueField = "Id";
                rdropCM.DataSource = dtRmployeeInfo;
                rdropCM.DataBind();
                if (dtRmployeeInfo.Rows.Count == 2)
                    rdropCM.SelectedIndex = 1;
                else
                    rdropCM.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load data" + ex);
            }
        }

        private void LoadSetupAssistantChanelManager()
        {
            try
            {
                SetupChannel objsetupChannel = new SetupChannel().GetAllSetupChannelByChannels("Assistant Channel Manager");
                int desigId = objsetupChannel.DesignationId;
                DataTable dtRmployeeInfo =
                                  new EmployeeInformation().GetAllEmployeeInformationByDesignationIdbyTable(desigId);

                rdropAcm.DataTextField = "empAllInfo";
                rdropAcm.DataValueField = "Id";
                rdropAcm.DataSource = dtRmployeeInfo;
                rdropAcm.DataBind();
                if (dtRmployeeInfo.Rows.Count == 2)
                    rdropAcm.SelectedIndex = 1;
                else
                    rdropAcm.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Alert.Show("Something is going wrong to load data" + ex);
            }
        }

        private void GetSummary(string groupElement)
        {
            try
            {
                StringBuilder whereCondition = new StringBuilder();
                StringBuilder whereCondition2 = new StringBuilder();
                StringBuilder havingCondition2 = new StringBuilder();

                if (rdtStartDate.SelectedDate != null)
                {
                    DateTime sDate = DateTime.Parse(rdtStartDate.SelectedDate.ToString());

                    whereCondition.Append(" Where RequisitionDate >= '" + sDate.ToString("MM/dd/yyyy") + "'");
                    whereCondition2.Append(" Where RequisitionDate >= '" + sDate.ToString("MM/dd/yyyy") + "'");
                    if (rdtEndDate.SelectedDate != null)
                    {
                        DateTime eDate = DateTime.Parse(rdtEndDate.SelectedDate.ToString());
                        if (whereCondition.Length > 0)
                        {
                            whereCondition.Append(" And RequisitionDate <= '" + eDate.ToString("MM/dd/yyyy") + "'");
                            whereCondition2.Append(" And RequisitionDate <= '" + eDate.ToString("MM/dd/yyyy") + "'");
                        }
                        else
                        {
                            whereCondition.Append(" Where RequisitionDate <= '" + eDate.ToString("MM/dd/yyyy") + "'");
                            whereCondition2.Append(" And RequisitionDate <= '" + eDate.ToString("MM/dd/yyyy") + "'");
                        }
                    }
                }

                if (chkDealer.Checked)
                {
                    if (whereCondition.Length <= 0)
                    {
                        if (rdropRegion.SelectedIndex > 0)
                        {
                            whereCondition.Append(" Where RegeonId = " + Convert.ToInt32(rdropRegion.SelectedValue));
                            whereCondition2.Append(" Where RegeonId = " + Convert.ToInt32(rdropRegion.SelectedValue));
                        }
                        else if (rdropArea.SelectedIndex > 0)
                        {
                            whereCondition.Append(" Where AId = " + Convert.ToInt32(rdropArea.SelectedValue));
                            whereCondition2.Append(" Where AId = " + Convert.ToInt32(rdropArea.SelectedValue));
                        }
                    }
                    else
                    {
                        if (rdropRegion.SelectedIndex > 0)
                        {
                            whereCondition.Append(" And  RegeonId = " + int.Parse(rdropRegion.SelectedValue));
                            whereCondition2.Append(" And  RegeonId = " + int.Parse(rdropRegion.SelectedValue));
                        }
                        else if (rdropArea.SelectedIndex > 0)
                        {
                            whereCondition.Append(" And AId = " + Convert.ToInt32(rdropArea.SelectedValue));
                            whereCondition2.Append(" And AId = " + Convert.ToInt32(rdropArea.SelectedValue));
                        }
                    }
                }
                if (rdropDealer.SelectedIndex > 0)
                {
                    if (whereCondition.Length <= 0)
                    {
                        whereCondition.Append(" Where DealerId =" + Convert.ToInt32(rdropDealer.SelectedValue));
                        whereCondition2.Append(" Where DealerId =" + Convert.ToInt32(rdropDealer.SelectedValue));
                    }
                    else
                    {
                        whereCondition.Append(" And DealerId =" + Convert.ToInt32(rdropDealer.SelectedValue));
                        whereCondition2.Append(" And DealerId =" + Convert.ToInt32(rdropDealer.SelectedValue));
                    }
                }
                if (chkProduct.Checked)
                {

                    if (rdropProduct.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where ProductId =" + Convert.ToInt32(rdropProduct.SelectedValue));
                            whereCondition2.Append(" Where ProductId =" + Convert.ToInt32(rdropProduct.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(" And ProductId =" + Convert.ToInt32(rdropProduct.SelectedValue));
                            whereCondition2.Append(" And ProductId =" + Convert.ToInt32(rdropProduct.SelectedValue));
                        }
                    }
                    if (rdropColor.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where ColorId =" + Convert.ToInt32(rdropColor.SelectedValue));
                            whereCondition2.Append(" Where ColorId =" + Convert.ToInt32(rdropColor.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(" And ColorId =" + Convert.ToInt32(rdropColor.SelectedValue));
                            whereCondition2.Append(" And ColorId =" + Convert.ToInt32(rdropColor.SelectedValue));
                        }
                    }
                }
                if (ckhEmployee.Checked)
                {

                    if (rdropCM.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where CMId =" + Convert.ToInt32(rdropCM.SelectedValue));
                            whereCondition2.Append(" Where CMId =" + Convert.ToInt32(rdropCM.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(@" And CMId =" + Convert.ToInt32(rdropCM.SelectedValue));
                            whereCondition2.Append(@" And CMId =" + Convert.ToInt32(rdropCM.SelectedValue));
                        }
                    }
                    if (rdropAcm.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where ACM =" + Convert.ToInt32(rdropAcm.SelectedValue));
                            whereCondition2.Append(" Where ACM =" + Convert.ToInt32(rdropAcm.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(" And ACM =" + Convert.ToInt32(rdropAcm.SelectedValue));
                            whereCondition2.Append(" And ACM =" + Convert.ToInt32(rdropAcm.SelectedValue));
                        }
                    }
                    if (rdropCs.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where CSId =" + Convert.ToInt32(rdropCs.SelectedValue));
                            whereCondition2.Append(" Where CSId =" + Convert.ToInt32(rdropCs.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(" And CSId =" + Convert.ToInt32(rdropCs.SelectedValue));
                            whereCondition2.Append(" And CSId =" + Convert.ToInt32(rdropCs.SelectedValue));
                        }
                    }
                    if (rdropJcs.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where JrCSId =" + Convert.ToInt32(rdropJcs.SelectedValue));
                            whereCondition2.Append(" Where JrCSId =" + Convert.ToInt32(rdropJcs.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(" And JrCSId =" + Convert.ToInt32(rdropJcs.SelectedValue));
                            whereCondition2.Append(" And JrCSId =" + Convert.ToInt32(rdropJcs.SelectedValue));
                        }
                    }
                }
                if (rdropStatus.SelectedValue != "SelectOne" && rdropStatus.SelectedIndex > 0)
                {

                    if (whereCondition.Length <= 0)
                    {
                        whereCondition.Append(" Where Status ='" + rdropStatus.SelectedValue + "'");
                        whereCondition2.Append(" Where Status ='" + rdropStatus.SelectedValue + "'");
                    }
                    else
                    {
                        whereCondition.Append(" And Status ='" + rdropStatus.SelectedValue + "'");
                        whereCondition2.Append(" And Status ='" + rdropStatus.SelectedValue + "'");
                    }
                }
                if (chkQuentity.Checked)
                {
                    if (rdropQuentity.SelectedValue != "SelectOne" && rdropQuentity.SelectedIndex > 0)
                    {
                        if (rdropQuentity.SelectedValue == "equal")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Quantity =" + rtxtQEqual.Text);
                                whereCondition2.Append(" Where Quantity =" + rtxtQEqual.Text);
                            }
                            else
                            {
                                whereCondition.Append(" And Quantity =" + rtxtQEqual.Text);
                                whereCondition2.Append(" And Quantity =" + rtxtQEqual.Text);
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Quantity) =" + rtxtQEqual.Text);


                            //else
                            //    havingCondition2.Append(" And sum(Quantity) =" + rtxtQEqual.Text);


                        }
                        if (rdropQuentity.SelectedValue == "greater")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Quantity >=" + rtxtQEqual.Text);
                                whereCondition2.Append(" Where Quantity >=" + rtxtQEqual.Text);
                            }
                            else
                            {
                                whereCondition.Append(" And Quantity >=" + rtxtQEqual.Text);
                                whereCondition2.Append(" And Quantity >=" + rtxtQEqual.Text);
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Quantity) >=" + rtxtQEqual.Text);
                            //else
                            //    havingCondition2.Append(" And sum(Quantity) >=" + rtxtQEqual.Text);


                        }
                        if (rdropQuentity.SelectedValue == "less")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Quantity <=" + rtxtQEqual.Text);
                                whereCondition2.Append(" Where Quantity <=" + rtxtQEqual.Text);
                            }
                            else
                            {
                                whereCondition.Append(" And Quantity <=" + rtxtQEqual.Text);
                                whereCondition2.Append(" And Quantity <=" + rtxtQEqual.Text);
                            }


                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Quantity) <=" + rtxtQEqual.Text);
                            //else
                            //    havingCondition2.Append(" And sum(Quantity) <=" + rtxtQEqual.Text);


                        }
                        if (rdropQuentity.SelectedValue == "between")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Quantity >=" + rtxtQEqual.Text + "And Quantity <=" + rtxtQGeter.Text);
                                whereCondition2.Append(" Where Quantity >=" + rtxtQEqual.Text + "And Quantity <=" + rtxtQGeter.Text);
                            }
                            else
                            {
                                whereCondition.Append(" And Quantity >=" + rtxtQEqual + "And Quantity <=" + rtxtQGeter);
                                whereCondition2.Append(" And Quantity >=" + rtxtQEqual + "And Quantity <=" + rtxtQGeter);
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Quantity) >=" + rtxtQEqual.Text + "And sum(Quantity) <=" + rtxtQGeter.Text);
                            //else
                            //    havingCondition2.Append(" And sum(Quantity) >=" + rtxtQEqual + "And sum(Quantity) <=" + rtxtQGeter);


                        }

                    }
                }
                if (ChkTotal.Checked)
                {
                    if (rdropTotal.SelectedValue != "SelectOne" && rdropTotal.SelectedIndex > 0)
                    {
                        if (rdropTotal.SelectedValue == "equal")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where LineTotal =" + rtxtTEqual.Text);
                                whereCondition2.Append(" Where LineTotal =" + rtxtTEqual.Text);
                            }

                            else
                            {
                                whereCondition.Append(" And LineTotal =" + rtxtTEqual.Text);
                                whereCondition2.Append(" And LineTotal =" + rtxtTEqual.Text);
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(ItemTotal) =" + rtxtTEqual.Text);

                            //else
                            //    havingCondition2.Append(" And sum(ItemTotal) =" + rtxtTEqual.Text);


                        }
                        if (rdropTotal.SelectedValue == "greater")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where LineTotal >=" + rtxtTEqual.Text);
                                whereCondition2.Append(" Where LineTotal >=" + rtxtTEqual.Text);
                            }

                            else
                            {
                                whereCondition.Append(" And LineTotal >=" + rtxtTEqual.Text);
                                whereCondition2.Append(" And LineTotal >=" + rtxtTEqual.Text);
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(ItemTotal) >=" + rtxtTEqual.Text);

                            //else
                            //    havingCondition2.Append(" And sum(ItemTotal) >=" + rtxtTEqual.Text);

                        }
                        if (rdropTotal.SelectedValue == "less")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where LineTotal <=" + rtxtTEqual.Text);
                                whereCondition2.Append(" Where LineTotal <=" + rtxtTEqual.Text);
                            }

                            else
                            {
                                whereCondition.Append(" And LineTotal <=" + rtxtTEqual);
                                whereCondition2.Append(" And LineTotal <=" + rtxtTEqual);
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(ItemTotal)<=" + rtxtTEqual.Text);


                            //else
                            //    havingCondition2.Append(" And sum(ItemTotal) <=" + rtxtTEqual);


                        }
                        if (rdropTotal.SelectedValue == "between")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where LineTotal >=" + rtxtTEqual.Text + "And LineTotal <=" + rtxtTGeter.Text);
                                whereCondition2.Append(" Where LineTotal >=" + rtxtTEqual.Text + "And LineTotal <=" + rtxtTGeter.Text);
                            }
                            else
                            {
                                whereCondition.Append(" And LineTotal >=" + rtxtTEqual.Text + "And LineTotal <=" + rtxtTGeter.Text);
                                whereCondition.Append(" And LineTotal >=" + rtxtTEqual.Text + "And LineTotal <=" + rtxtTGeter.Text);
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(ItemTotal )>=" + rtxtTEqual.Text + "And sum(ItemTotal) <=" + rtxtTGeter.Text);
                            //else
                            //    havingCondition2.Append(" And sum(ItemTotal )>=" + rtxtTEqual.Text + "And sum(ItemTotal) <=" + rtxtTGeter.Text);


                        }
                    }
                }
                if (chkDiscount.Checked)
                {
                    if (rdropDiscount.SelectedValue != "SelectOne" && rdropDiscount.SelectedIndex > 0)
                    {
                        if (rdropDiscount.SelectedValue == "equal")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Discount =" + rtxtTEqual);
                                whereCondition2.Append(" Where Discount =" + rtxtTEqual);
                            }
                            else
                            {
                                whereCondition.Append(" And Discount =" + rtxtTEqual);
                                whereCondition2.Append(" And Discount =" + rtxtTEqual);
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Discount) =" + rtxtTEqual);
                            //else
                            //    havingCondition2.Append(" And sum(Discount) =" + rtxtTEqual);


                        }
                        if (rdropDiscount.SelectedValue == "greater")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Discount >=" + rtxtDEqual);
                                whereCondition2.Append(" Where Discount >=" + rtxtDEqual);
                            }
                            else
                            {
                                whereCondition.Append(" And Discount >=" + rtxtDEqual);
                                whereCondition2.Append(" And Discount >=" + rtxtDEqual);
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Discount) >=" + rtxtDEqual);
                            //else
                            //    havingCondition2.Append(" And sum(Discount) >=" + rtxtDEqual);

                        }
                        if (rdropDiscount.SelectedValue == "less")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Discount <=" + rtxtDEqual);
                                whereCondition2.Append(" Where Discount <=" + rtxtDEqual);
                            }
                            else
                            {
                                whereCondition.Append(" And Discount <=" + rtxtDEqual);
                                whereCondition2.Append(" And Discount <=" + rtxtDEqual);
                            }


                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Discount) <=" + rtxtDEqual);
                            //else
                            //    havingCondition2.Append(" And sum(Discount) <=" + rtxtDEqual);


                        }
                        if (rdropDiscount.SelectedValue == "between")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Discount >=" + rtxtDEqual + "And Discount <=" + rtxtDTotal);
                                whereCondition2.Append(" Where Discount >=" + rtxtDEqual + "And Discount <=" + rtxtDTotal);
                            }
                            else
                            {
                                whereCondition.Append(" And Discount >=" + rtxtDEqual + "And Discount <=" + rtxtDTotal);
                                whereCondition2.Append(" And Discount >=" + rtxtDEqual + "And Discount <=" + rtxtDTotal);

                            }
                            //if (havingCondition2.Length <= 0)
                            //{
                            //    havingCondition2.Append(" having sum(Discount) >=" + rtxtDEqual + "And sum(Discount) <=" + rtxtDTotal);

                            //}
                            //else
                            //{
                            //    havingCondition2.Append(" And sum(Discount) >=" + rtxtDEqual + "And sum(Discount) <=" + rtxtDTotal);
                            //}
                        }
                    }
                }
                dtdealerSummary = new RequisitionMaster().GetAllInfoFromViewList(whereCondition.ToString());

                //if (whereCondition2.Length > 0)
                if (groupElement == string.Empty)
                {
                    dtSummary = new RequisitionMaster().GetAllInfoFromViewListByGroup(whereCondition2.ToString(),
                        havingCondition2.ToString());
                }
                else
                {
                    dtSummary = new RequisitionMaster().GetAllInfoFromViewListByDealer(whereCondition2.ToString(),
                        havingCondition2.ToString(), groupElement);
                }
                if (dtdealerSummary.Rows.Count == 0)
                {
                    RadGridMonthlySalesReport.DataSource = new string[] { };
                    RadGridMonthlySalesReport.DataBind();
                    Session["dtdealerSummary"] = null;
                }
                else
                {
                    RadGridMonthlySalesReport.DataSource = dtdealerSummary;
                    RadGridMonthlySalesReport.DataBind();
                    Session["dtdealerSummary"] = dtdealerSummary;

                }
                if (dtSummary.Rows.Count == 0)
                {
                    RadGridSummary.DataSource = new string[] { };
                    RadGridSummary.DataBind();
                    Session["dtSummary"] = null;
                }
                else
                {
                    RadGridSummary.DataSource = dtSummary;
                    RadGridSummary.DataBind();
                    Session["dtSummary"] = dtSummary;
                }



                btnPrintAll.Visible = true;
                btnExcel.Visible = true;
            }
            catch (Exception ex)
            {
                Alert.Show("Error Occurs. Error: " + ex);
            }
        }

        private void GetProductSummary()
        {
            try
            {
                StringBuilder whereCondition = new StringBuilder();
                StringBuilder whereCondition2 = new StringBuilder();
                StringBuilder havingCondition2 = new StringBuilder();
                if (rdtStartDate.SelectedDate != null)
                {
                    DateTime sDate = DateTime.Parse(rdtStartDate.SelectedDate.ToString());

                    whereCondition.Append(" Where RequisitionDate >= '" + sDate.ToString("MM/dd/yyyy") + "'");
                    whereCondition2.Append(" Where RequisitionDate >= '" + sDate.ToString("MM/dd/yyyy") + "'");
                    if (rdtEndDate.SelectedDate != null)
                    {
                        DateTime eDate = DateTime.Parse(rdtEndDate.SelectedDate.ToString());
                        if (whereCondition.Length > 0)
                        {
                            whereCondition.Append(" And RequisitionDate <= '" + eDate.ToString("MM/dd/yyyy") + "'");
                            whereCondition2.Append(" And RequisitionDate <= '" + eDate.ToString("MM/dd/yyyy") + "'");
                        }
                        else
                        {
                            whereCondition.Append(" Where RequisitionDate <= '" + eDate.ToString("MM/dd/yyyy") + "'");
                            whereCondition2.Append(" And RequisitionDate <= '" + eDate.ToString("MM/dd/yyyy") + "'");
                        }
                    }
                }

                if (chkDealer.Checked)
                {
                    if (whereCondition.Length <= 0)
                    {
                        if (rdropRegion.SelectedIndex > 0)
                        {
                            whereCondition.Append(" Where RegeonId = " + Convert.ToInt32(rdropRegion.SelectedValue));
                            whereCondition2.Append(" Where RegeonId = " + Convert.ToInt32(rdropRegion.SelectedValue));
                        }
                        else if (rdropArea.SelectedIndex > 0)
                        {
                            whereCondition.Append(" Where AId = " + Convert.ToInt32(rdropArea.SelectedValue));
                            whereCondition2.Append(" Where AId = " + Convert.ToInt32(rdropArea.SelectedValue));
                        }
                    }
                    else
                    {
                        if (rdropRegion.SelectedIndex > 0)
                        {
                            whereCondition.Append(" And  RegeonId = " + int.Parse(rdropRegion.SelectedValue));
                            whereCondition2.Append(" And  RegeonId = " + int.Parse(rdropRegion.SelectedValue));
                        }
                        else if (rdropArea.SelectedIndex > 0)
                        {
                            whereCondition.Append(" And AId = " + Convert.ToInt32(rdropArea.SelectedValue));
                            whereCondition2.Append(" And AId = " + Convert.ToInt32(rdropArea.SelectedValue));
                        }
                    }
                }
                if (rdropDealer.SelectedIndex > 0)
                {
                    if (whereCondition.Length <= 0)
                    {
                        whereCondition.Append(" Where DealerId =" + Convert.ToInt32(rdropDealer.SelectedValue));
                        whereCondition2.Append(" Where DealerId =" + Convert.ToInt32(rdropDealer.SelectedValue));
                    }
                    else
                    {
                        whereCondition.Append(" And DealerId =" + Convert.ToInt32(rdropDealer.SelectedValue));
                        whereCondition2.Append(" And DealerId =" + Convert.ToInt32(rdropDealer.SelectedValue));
                    }
                }
                if (chkProduct.Checked)
                {

                    if (rdropProduct.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where ProductId =" + Convert.ToInt32(rdropProduct.SelectedValue));
                            whereCondition2.Append(" Where ProductId =" + Convert.ToInt32(rdropProduct.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(" And ProductId =" + Convert.ToInt32(rdropProduct.SelectedValue));
                            whereCondition2.Append(" And ProductId =" + Convert.ToInt32(rdropProduct.SelectedValue));
                        }
                    }
                    if (rdropColor.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where ColorId =" + Convert.ToInt32(rdropColor.SelectedValue));
                            whereCondition2.Append(" Where ColorId =" + Convert.ToInt32(rdropColor.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(" And ColorId =" + Convert.ToInt32(rdropColor.SelectedValue));
                            whereCondition2.Append(" And ColorId =" + Convert.ToInt32(rdropColor.SelectedValue));
                        }
                    }
                }
                if (ckhEmployee.Checked)
                {

                    if (rdropCM.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where CMId =" + Convert.ToInt32(rdropCM.SelectedValue));
                            whereCondition2.Append(" Where CMId =" + Convert.ToInt32(rdropCM.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(@" And CMId =" + Convert.ToInt32(rdropCM.SelectedValue));
                            whereCondition2.Append(@" And CMId =" + Convert.ToInt32(rdropCM.SelectedValue));
                        }
                    }
                    if (rdropAcm.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where ACM =" + Convert.ToInt32(rdropAcm.SelectedValue));
                            whereCondition2.Append(" Where ACM =" + Convert.ToInt32(rdropAcm.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(" And ACM =" + Convert.ToInt32(rdropAcm.SelectedValue));
                            whereCondition2.Append(" And ACM =" + Convert.ToInt32(rdropAcm.SelectedValue));
                        }
                    }
                    if (rdropCs.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where CSId =" + Convert.ToInt32(rdropCs.SelectedValue));
                            whereCondition2.Append(" Where CSId =" + Convert.ToInt32(rdropCs.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(" And CSId =" + Convert.ToInt32(rdropCs.SelectedValue));
                            whereCondition2.Append(" And CSId =" + Convert.ToInt32(rdropCs.SelectedValue));
                        }
                    }
                    if (rdropJcs.SelectedIndex > 0)
                    {
                        if (whereCondition.Length <= 0)
                        {
                            whereCondition.Append(" Where JrCSId =" + Convert.ToInt32(rdropJcs.SelectedValue));
                            whereCondition2.Append(" Where JrCSId =" + Convert.ToInt32(rdropJcs.SelectedValue));
                        }
                        else
                        {
                            whereCondition.Append(" And JrCSId =" + Convert.ToInt32(rdropJcs.SelectedValue));
                            whereCondition2.Append(" And JrCSId =" + Convert.ToInt32(rdropJcs.SelectedValue));
                        }
                    }
                }
                if (rdropStatus.SelectedValue != "SelectOne" && rdropStatus.SelectedIndex > 0)
                {

                    if (whereCondition.Length <= 0)
                    {
                        whereCondition.Append(" Where Status ='" + rdropStatus.SelectedValue + "'");
                        whereCondition2.Append(" Where Status ='" + rdropStatus.SelectedValue + "'");
                    }
                    else
                    {
                        whereCondition.Append(" And Status ='" + rdropStatus.SelectedValue + "'");
                        whereCondition2.Append(" And Status ='" + rdropStatus.SelectedValue + "'");
                    }
                }
                if (chkQuentity.Checked)
                {
                    if (rdropQuentity.SelectedValue != "SelectOne" && rdropQuentity.SelectedIndex > 0)
                    {
                        if (rdropQuentity.SelectedValue == "equal")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Quantity =" + rtxtQEqual.Text);
                                whereCondition2.Append(" Where Quantity =" + rtxtQEqual.Text);

                            }
                            else
                            {
                                whereCondition.Append(" And Quantity =" + rtxtQEqual.Text);
                                whereCondition2.Append(" And Quantity =" + rtxtQEqual.Text);

                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Quantity) =" + rtxtQEqual.Text);


                            //else
                            //    havingCondition2.Append(" And sum(Quantity) =" + rtxtQEqual.Text);


                        }
                        if (rdropQuentity.SelectedValue == "greater")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Quantity >=" + rtxtQEqual.Text);
                                whereCondition2.Append(" Where Quantity >=" + rtxtQEqual.Text);

                            }
                            else
                            {
                                whereCondition.Append(" And Quantity >=" + rtxtQEqual.Text);
                                whereCondition2.Append(" And Quantity >=" + rtxtQEqual.Text);

                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Quantity) >=" + rtxtQEqual.Text);
                            //else
                            //    havingCondition2.Append(" And sum(Quantity) >=" + rtxtQEqual.Text);


                        }
                        if (rdropQuentity.SelectedValue == "less")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Quantity <=" + rtxtQEqual.Text);
                                whereCondition2.Append(" Where Quantity <=" + rtxtQEqual.Text);

                            }
                            else
                            {
                                whereCondition.Append(" And Quantity <=" + rtxtQEqual.Text);
                                whereCondition2.Append(" And Quantity <=" + rtxtQEqual.Text);

                            }


                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Quantity) <=" + rtxtQEqual.Text);
                            //else
                            //    havingCondition2.Append(" And sum(Quantity) <=" + rtxtQEqual.Text);


                        }
                        if (rdropQuentity.SelectedValue == "between")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Quantity >=" + rtxtQEqual.Text + "And Quantity <=" +rtxtQGeter.Text);
                                whereCondition2.Append(" Where Quantity >=" + rtxtQEqual.Text + "And Quantity <=" + rtxtQGeter.Text);

                            }
                            else
                            {
                                whereCondition.Append(" And Quantity >=" + rtxtQEqual + "And Quantity <=" + rtxtQGeter);
                                whereCondition2.Append(" And Quantity >=" + rtxtQEqual + "And Quantity <=" + rtxtQGeter);

                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Quantity) >=" + rtxtQEqual.Text + "And sum(Quantity) <=" + rtxtQGeter.Text);
                            //else
                            //    havingCondition2.Append(" And sum(Quantity) >=" + rtxtQEqual + "And sum(Quantity) <=" + rtxtQGeter);


                        }

                    }
                }
                if (ChkTotal.Checked)
                {
                    if (rdropTotal.SelectedValue != "SelectOne" && rdropTotal.SelectedIndex > 0)
                    {
                        if (rdropTotal.SelectedValue == "equal")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where LineTotal =" + rtxtTEqual.Text);
                                whereCondition2.Append(" Where LineTotal =" + rtxtTEqual.Text);

                            }

                            else
                            {
                                whereCondition.Append(" And LineTotal =" + rtxtTEqual.Text);
                                whereCondition2.Append(" And LineTotal =" + rtxtTEqual.Text);

                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(ItemTotal) =" + rtxtTEqual.Text);

                            //else
                            //    havingCondition2.Append(" And sum(ItemTotal) =" + rtxtTEqual.Text);


                        }
                        if (rdropTotal.SelectedValue == "greater")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where LineTotal >=" + rtxtTEqual.Text);
                                whereCondition2.Append(" Where LineTotal >=" + rtxtTEqual.Text);

                            }

                            else
                            {
                                whereCondition.Append(" And LineTotal >=" + rtxtTEqual.Text);
                                whereCondition2.Append(" And LineTotal >=" + rtxtTEqual.Text);

                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(ItemTotal) >=" + rtxtTEqual.Text);

                            //else
                            //    havingCondition2.Append(" And sum(ItemTotal) >=" + rtxtTEqual.Text);

                        }
                        if (rdropTotal.SelectedValue == "less")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where LineTotal <=" + rtxtTEqual.Text);
                                whereCondition2.Append(" Where LineTotal <=" + rtxtTEqual.Text);

                            }

                            else
                            {
                                whereCondition.Append(" And LineTotal <=" + rtxtTEqual);
                                whereCondition2.Append(" And LineTotal <=" + rtxtTEqual);

                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(ItemTotal)<=" + rtxtTEqual.Text);


                            //else
                            //    havingCondition2.Append(" And sum(ItemTotal) <=" + rtxtTEqual);


                        }
                        if (rdropTotal.SelectedValue == "between")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where LineTotal >=" + rtxtTEqual.Text + "And LineTotal <=" + rtxtTGeter.Text);
                                whereCondition2.Append(" Where LineTotal >=" + rtxtTEqual.Text + "And LineTotal <=" + rtxtTGeter.Text);

                            }
                            else
                            {
                                whereCondition.Append(" And LineTotal >=" + rtxtTEqual.Text + "And LineTotal <=" + rtxtTGeter.Text);
                                whereCondition2.Append(" And LineTotal >=" + rtxtTEqual.Text + "And LineTotal <=" + rtxtTGeter.Text);
                                
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(ItemTotal )>=" + rtxtTEqual.Text + "And sum(ItemTotal) <=" + rtxtTGeter.Text);
                            //else
                            //    havingCondition2.Append(" And sum(ItemTotal )>=" + rtxtTEqual.Text + "And sum(ItemTotal) <=" + rtxtTGeter.Text);


                        }
                    }
                }
                if (chkDiscount.Checked)
                {
                    if (rdropDiscount.SelectedValue != "SelectOne" && rdropDiscount.SelectedIndex > 0)
                    {
                        if (rdropDiscount.SelectedValue == "equal")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Discount =" + rtxtTEqual);
                                whereCondition2.Append(" Where Discount =" + rtxtTEqual);
                                
                            }
                            else
                            {
                                whereCondition.Append(" And Discount =" + rtxtTEqual);
                                whereCondition2.Append(" And Discount =" + rtxtTEqual);
                                
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Discount) =" + rtxtTEqual);
                            //else
                            //    havingCondition2.Append(" And sum(Discount) =" + rtxtTEqual);


                        }
                        if (rdropDiscount.SelectedValue == "greater")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Discount >=" + rtxtDEqual);
                                whereCondition2.Append(" Where Discount >=" + rtxtDEqual);
                                
                            }
                            else
                            {
                                whereCondition.Append(" And Discount >=" + rtxtDEqual);
                                whereCondition2.Append(" And Discount >=" + rtxtDEqual);
                                
                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Discount) >=" + rtxtDEqual);
                            //else
                            //    havingCondition2.Append(" And sum(Discount) >=" + rtxtDEqual);

                        }
                        if (rdropDiscount.SelectedValue == "less")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Discount <=" + rtxtDEqual);
                                whereCondition2.Append(" Where Discount <=" + rtxtDEqual);
                                
                            }
                            else
                            {
                                whereCondition.Append(" And Discount <=" + rtxtDEqual);
                                whereCondition2.Append(" And Discount <=" + rtxtDEqual);

                            }

                            //if (havingCondition2.Length <= 0)
                            //    havingCondition2.Append(" having sum(Discount) <=" + rtxtDEqual);
                            //else
                            //    havingCondition2.Append(" And sum(Discount) <=" + rtxtDEqual);


                        }
                        if (rdropDiscount.SelectedValue == "between")
                        {
                            if (whereCondition.Length <= 0)
                            {
                                whereCondition.Append(" Where Discount >=" + rtxtDEqual + "And Discount <=" + rtxtDTotal);
                                whereCondition2.Append(" Where Discount >=" + rtxtDEqual + "And Discount <=" + rtxtDTotal);
                            }
                            else
                            {
                                whereCondition.Append(" And Discount >=" + rtxtDEqual + "And Discount <=" + rtxtDTotal);
                                whereCondition2.Append(" And Discount >=" + rtxtDEqual + "And Discount <=" + rtxtDTotal);

                            }
                            //if (havingCondition2.Length <= 0)
                            //{
                            //    havingCondition2.Append(" having sum(Discount) >=" + rtxtDEqual + "And sum(Discount) <=" + rtxtDTotal);

                            //}
                            //else
                            //{
                            //    havingCondition2.Append(" And sum(Discount) >=" + rtxtDEqual + "And sum(Discount) <=" + rtxtDTotal);
                            //}
                        }
                    }
                }
                dtdealerSummary = new RequisitionMaster().GetAllInfoFromViewList(whereCondition.ToString());
                dtProductSummary = new RequisitionMaster().GetAllInfoFromViewListByProduct(whereCondition2.ToString(), havingCondition2.ToString());

                if (dtdealerSummary.Rows.Count == 0)
                {
                    RadGridMonthlySalesReport.DataSource = new string[] { };
                    RadGridMonthlySalesReport.DataBind();
                    Session["dtProductSummary"] = null;

                }
                else
                {
                    RadGridMonthlySalesReport.DataSource = dtdealerSummary;
                    RadGridMonthlySalesReport.DataBind();
                    Session["dtdealerSummary"] = dtdealerSummary;
                }
                if (dtProductSummary.Rows.Count == 0)
                {
                    RadgridProduct.DataSource = new string[] { };
                    RadgridProduct.DataBind();
                    Session["dtdealerSummary"] = null;

                }
                else
                {
                    RadgridProduct.DataSource = dtProductSummary;
                    RadgridProduct.DataBind();
                    Session["dtProductSummary"] = dtProductSummary;
                }




                btnPrintAll.Visible = true;
                btnExcel.Visible = true;
            }
            catch (Exception ex)
            {
                Alert.Show("Error Occurs. Error: " + ex);
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

            if (Session["dtSummary"] != null)
            {
                dtSummary = (DataTable)Session["dtSummary"];
            }
            else
            {
                dtSummary = new DataTable();
            }
            if (Session["dtProductSummary"] != null)
            {
                dtProductSummary = (DataTable)Session["dtProductSummary"];
            }
            else
            {
                dtProductSummary = new DataTable();
            }
            if (Session["dtdealerSummary"] != null)
            {
                dtdealerSummary = (DataTable)Session["dtdealerSummary"];
            }
            else
            {
                dtdealerSummary = new DataTable();
            }
            if (!IsPostBack)
            {
                if (_user.EmployeeId != 0)
                {
                    Department objDepartment = new Department().GetEmployeeDepartment(_user.EmployeeId);
                    _department = objDepartment.DepartmentName;
                }

                lblCount.Text = "0";
                //this.LoadRegion();
                lblproduct.Text = "0";
                lblsummary.Text = "0";
                lblgroupElement.Text = "";
                this.LoadDealer();
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

        protected void RadGridMonthlySalesReport_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            GetSummary(lblgroupElement.Text);
        }

        protected void RadGridMonthlySalesReport_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            GetSummary(lblgroupElement.Text);
        }

        protected void RadGridMonthlySalesReport_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GetSummary(lblgroupElement.Text);
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
                    ReportTitle = lblCount.Text + " Summary Report",
                    LogoPath = ""
                };
                List<DetailStructure> detailDataList = new List<DetailStructure>()
                {
                    new DetailStructure
                    {
                        Slno = 1, FieldName = "DealerName", DataType = "string", Caption = "Name",
                        FieldWidth = 100f, Align = Alignments.ALIGN_LEFT
                    },
                    new DetailStructure
                    {
                        Slno = 2, FieldName = "RequisitionDate", DataType = "DateTime", Caption = "Requisition Date",
                        FieldWidth = 110f, Align = Alignments.ALIGN_RIGHT
                    },
                    new DetailStructure
                    {
                        Slno = 3, FieldName = "RequisitionCode", DataType = "string", Caption = "Requisition Code",
                        FieldWidth = 100f, Align = Alignments.ALIGN_RIGHT
                    },
                    new DetailStructure
                    {
                        Slno = 4, FieldName = "Status", DataType = "string", Caption = "Status",
                        FieldWidth = 100f, Align = Alignments.ALIGN_RIGHT
                    },
                    new DetailStructure
                    {
                        Slno = 5, FieldName = "CourierName", DataType = "string", Caption = "Courier Name",
                        FieldWidth = 80f, Align = Alignments.ALIGN_RIGHT
                    },
                    new DetailStructure
                    {
                        Slno = 6, FieldName = "ProductName", DataType = "string", Caption = "Product Name",
                        FieldWidth = 110f, Align = Alignments.ALIGN_RIGHT
                    },
                    new DetailStructure
                    {
                        Slno = 7, FieldName = "Quantity", DataType = "int", Caption = "Quantity",
                        FieldWidth = 100f, Align = Alignments.ALIGN_RIGHT
                    },
                    
                    new DetailStructure
                    {
                        Slno = 8, FieldName = "Color", DataType = "string", Caption = "Color",
                        FieldWidth = 80f, Align = Alignments.ALIGN_RIGHT
                    },
                    new DetailStructure
                    {
                        Slno = 9, FieldName = "Discount", DataType = "int", Caption = "Discount",
                        FieldWidth = 100f, Align = Alignments.ALIGN_RIGHT
                    },
                     new DetailStructure
                    {
                        Slno = 10, FieldName = "Price", DataType = "int", Caption = "Price",
                        FieldWidth = 100f, Align = Alignments.ALIGN_RIGHT
                    },
                    new DetailStructure
                    {
                        Slno = 11, FieldName = "ItemTotal", DataType = "int", Caption = "Total",
                        FieldWidth = 80f, Align = Alignments.ALIGN_RIGHT
                    }

                };


                float[] widths = new float[] { 110f, 80f, 100f, 80f, 60f, 100f, 80f, 80f, 80, 110, 100f };

                ReportDetail reportDetail = new ReportDetail(detailDataList, dtdealerSummary, 9, "English", widths,
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

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            DataTable dt = dtdealerSummary.Copy();
            DataTable dts;
            if (lblsummary.Text == "1")
            {
                dts = dtSummary.Copy();
            }
            else
            {
                dts = dtProductSummary.Copy();
            }
            ExportToExcel(dt, dts);
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (rdropSarch.SelectedValue.ToLower() != "selectone")
            {

                if (rdropSarch.SelectedValue.ToLower() == "region")
                {
                    lblgroupElement.Text = "RegionName";
                    showSalseGrid.Visible = true;
                    showSummery.Visible = true;
                    showProductSummary.Visible = false;
                    lblproduct.Text = "0";
                    lblsummary.Text = "1";
                    lblGroupCaption.Text = "Region";
                    GetSummary(lblgroupElement.Text);
                }
                if (rdropSarch.SelectedValue.ToLower() == "area")
                {
                    lblgroupElement.Text = "AreaName";
                    showSalseGrid.Visible = true;
                    showSummery.Visible = true;
                    showProductSummary.Visible = false;
                    lblproduct.Text = "0";
                    lblsummary.Text = "1";
                    GetSummary(lblgroupElement.Text);
                    lblGroupCaption.Text = "Area";
                }
                if (rdropSarch.SelectedValue.ToLower() == "cm")
                {
                    lblgroupElement.Text = "CM";
                    showSalseGrid.Visible = true;
                    showSummery.Visible = true;
                    showProductSummary.Visible = false;
                    lblproduct.Text = "0";
                    lblsummary.Text = "1";
                    lblGroupCaption.Text = "CM";
                    GetSummary(lblgroupElement.Text);
                }
                if (rdropSarch.SelectedValue.ToLower() == "acm")
                {
                    lblgroupElement.Text = "AreaCM";
                    showSalseGrid.Visible = true;
                    showSummery.Visible = true;
                    showProductSummary.Visible = false;
                    lblproduct.Text = "0";
                    lblsummary.Text = "1";
                    lblGroupCaption.Text = "ACM";
                    GetSummary(lblgroupElement.Text);
                }
                if (rdropSarch.SelectedValue.ToLower() == "cs")
                {
                    lblgroupElement.Text = "Cs";
                    showSalseGrid.Visible = true;
                    showSummery.Visible = true;
                    showProductSummary.Visible = false;
                    lblproduct.Text = "0";
                    lblsummary.Text = "1";
                    lblGroupCaption.Text = "CS";
                    GetSummary(lblgroupElement.Text);
                }
                if (rdropSarch.SelectedValue.ToLower() == "jcs")
                {
                    lblgroupElement.Text = "JrCs";
                    showSalseGrid.Visible = true;
                    showSummery.Visible = true;
                    showProductSummary.Visible = false;
                    lblproduct.Text = "0";
                    lblsummary.Text = "1";
                    lblGroupCaption.Text = "Jr.CS";
                    GetSummary(lblgroupElement.Text);
                }
                if (rdropSarch.SelectedValue.ToLower() == "product")
                {
                    showSalseGrid.Visible = false;
                    showProductSummary.Visible = true;
                    showSummery.Visible = true;
                    lblproduct.Text = "1";
                    lblsummary.Text = "0";
                    this.GetProductSummary();
                }
            }
        }


        protected void btnProduct_OnClick(object sender, EventArgs e)
        {
            showSalseGrid.Visible = false;
            showProductSummary.Visible = true;
            showSummery.Visible = true;
            lblproduct.Text = "1";
            lblsummary.Text = "0";
            this.GetProductSummary();
        }

        protected void rdropRegion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdropRegion.SelectedIndex != 0)
                this.LoadArea(int.Parse(rdropRegion.SelectedValue));


        }
        protected void rdropArea_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdropRegion.SelectedIndex != 0)
                this.LoadDealerInfo(int.Parse(rdropArea.SelectedValue));
        }
        protected void rdropRegion_OnDataBound(object sender, EventArgs e)
        {
            rdropRegion.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropArea_OnDataBound(object sender, EventArgs e)
        {
            rdropArea.Items.Insert(0, new RadComboBoxItem());
        }



        protected void rdropProduct_OnDataBound(object sender, EventArgs e)
        {
            rdropProduct.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropCM_OnDataBound(object sender, EventArgs e)
        {
            rdropCM.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropAcm_OnDataBound(object sender, EventArgs e)
        {
            rdropAcm.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropCs_OnDataBound(object sender, EventArgs e)
        {
            rdropCs.Items.Insert(0, new RadComboBoxItem());
        }

        protected void rdropJcs_OnDataBound(object sender, EventArgs e)
        {
            rdropJcs.Items.Insert(0, new RadComboBoxItem());
        }

        protected void chkProduct_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkProduct.Checked)
            {
                ShowProductPanel.Visible = true;
                this.LoadProductInfo();
                this.LoadColor();
            }
            else
            {
                ShowProductPanel.Visible = false;
                rdropProduct.SelectedIndex = 0;
                rdropColor.SelectedIndex = 0;

            }
        }

        protected void ckhEmployee_OnCheckedChanged(object sender, EventArgs e)
        {
            if (ckhEmployee.Checked)
            {
                showEmployeePanel.Visible = true;
                this.LoadSetupChennalDesignation();
                this.LoadSetupJrChennalDesignation();
                this.LoadSetupChennalManager();
                this.LoadSetupAssistantChanelManager();
            }
            else
            {
                showEmployeePanel.Visible = false;
                rdropCM.SelectedIndex = 0;
                rdropAcm.SelectedIndex = 0;
                rdropCs.SelectedIndex = 0;
                rdropJcs.SelectedIndex = 0;
            }
        }

        protected void chkDealer_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkDealer.Checked)
            {
                this.LoadRegion();
                showDealerPanel.Visible = true;
            }
            else
            {
                showDealerPanel.Visible = false;
                rdropArea.SelectedIndex = 0;
                rdropRegion.SelectedIndex = 0;
                rdropDealer.SelectedIndex = 0;
            }
        }

        protected void ChkTotal_OnCheckedChanged(object sender, EventArgs e)
        {
            rdropTotal.Visible = ChkTotal.Checked;
        }

        protected void chkDiscount_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkDiscount.Checked)
            {
                rdropDiscount.Visible = true;
            }
            else
            {
                rdropDiscount.Visible = false;
            }
        }

        protected void chkQuentity_OnCheckedChanged(object sender, EventArgs e)
        {
            rdropQuentity.Visible = chkQuentity.Checked;
        }

        protected void rdropQuentity_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdropQuentity.SelectedValue == "between")
            {
                rtxtQGeter.Visible = true;
                rtxtQEqual.Visible = true;

            }
            else
            {
                rtxtQGeter.Visible = false;
                rtxtQEqual.Visible = true;
            }
        }

        protected void rdropDiscount_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdropDiscount.SelectedValue == "between")
            {
                rtxtDEqual.Visible = true;
                rtxtDTotal.Visible = true;

            }
            else
            {
                rtxtDTotal.Visible = false;
                rtxtDEqual.Visible = true;
            }
        }

        protected void rdropTotal_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (rdropTotal.SelectedValue == "between")
            {
                rtxtTEqual.Visible = true;
                rtxtTGeter.Visible = true;

            }
            else
            {
                rtxtTGeter.Visible = false;
                rtxtTEqual.Visible = true;
            }
        }
        public void ExportToExcel(DataTable dt, DataTable dts)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string attachment = "attachment; filename=SalesReport.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";

                    string tab = "";

                    Response.ContentEncoding = System.Text.Encoding.Unicode;
                    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


                    StringWriter sw = new StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    #region ReportHeader

                    if (rdropRegion.SelectedIndex > 0)
                    {
                        Response.Write("Region");
                        tab = "\t";
                        Response.Write(tab + rdropRegion.SelectedItem.Text);
                        Response.Write("\n");
                    }
                    if (rdropArea.SelectedIndex > 0)
                    {
                        Response.Write("\n");
                        Response.Write("Area");
                        tab = "\t";
                        Response.Write(tab + rdropArea.SelectedItem.Text);
                        Response.Write("\n");
                    }

                    if (rdropDealer.SelectedIndex > 0)
                    {
                        Response.Write("\n");
                        Response.Write("Dealer");
                        tab = "\t";
                        Response.Write(tab + rdropDealer.SelectedItem.Text);
                        Response.Write("\n");
                    }

                    if (rdropProduct.SelectedIndex > 0)
                    {
                        Response.Write("\n");
                        Response.Write("Product Name");
                        tab = "\t";
                        Response.Write(tab + rdropProduct.SelectedItem.Text);
                        Response.Write("\n");
                    }
                    if (rdropCM.SelectedIndex > 0)
                    {
                        Response.Write("\n");
                        Response.Write("Channel Manager");
                        tab = "\t";
                        Response.Write(tab + rdropCM.SelectedItem.Text);
                        Response.Write("\n");
                    }
                    if (rdropAcm.SelectedIndex > 0)
                    {
                        Response.Write("\n");
                        Response.Write("Assi. channel manager");
                        tab = "\t";
                        Response.Write(tab + rdropRegion.SelectedItem.Text);
                        Response.Write("\n");
                    }

                    if (rdropCs.SelectedIndex > 0)
                    {
                        Response.Write("\n");
                        Response.Write(tab + "Channel Specialist");
                        tab = "\t";
                        Response.Write(tab + rdropCs.SelectedItem.Text);
                        Response.Write("\n");
                    }
                    if (rdropJcs.SelectedIndex > 0)
                    {
                        Response.Write("\n");
                        Response.Write(tab + "Jr Channel Specialist");
                        tab = "\t";
                        Response.Write(tab + rdropJcs.SelectedItem.Text);
                        Response.Write("\n");
                    }

                    #endregion

                    Response.Write(sw.ToString());

                    #region ReportSummeryForDealer

                    if (lblsummary.Text == "1")
                    {
                        decimal TQ = 0;
                        decimal T = 0;
                        decimal Dis = 0;

                        string groupElement = string.Empty;

                        foreach (DataRow dr in dts.Rows)
                        {
                            if (groupElement != dr["GroupElement"].ToString())
                            {
                                if (groupElement != string.Empty)
                                {
                                    Response.Write("Total");
                                    tab = "\t";
                                    Response.Write(tab + TQ);
                                    tab = "\t";
                                    Response.Write(tab + T);
                                    Response.Write("\n");
                                    Response.Write("\n");
                                }
                                Response.Write(lblGroupCaption.Text);
                                tab = "\t";
                                Response.Write(tab + dr["GroupElement"].ToString());
                                Response.Write("\n");
                                Response.Write("Product Name");
                                tab = "\t";
                                Response.Write(tab + "Total Quantity");
                                tab = "\t";
                                Response.Write(tab + "Total Amount");
                                tab = "\t";
                                Response.Write(tab + "Requisition Code");
                                tab = "\t";
                                Response.Write("\n");
                                T = 0;
                                TQ = 0;
                            }

                            Response.Write(dr["ProductName"].ToString());
                            tab = "\t";
                            Response.Write(tab + dr["totalQuantity"].ToString());
                            tab = "\t";
                            Response.Write(tab + dr["TotalPrice"].ToString());
                            tab = "\t";
                            Response.Write(tab + dr["ReqCode"].ToString());
                            tab = "\t";
                            groupElement = dr["GroupElement"].ToString();
                            TQ += decimal.Parse(dr["totalQuantity"].ToString());
                            T += decimal.Parse(dr["TotalPrice"].ToString());
                            Response.Write("\n");
                        }
                        Response.Write("Total");
                        tab = "\t";
                        Response.Write(tab + TQ);
                        tab = "\t";
                        Response.Write(tab + T);
                        Response.Write("\n");
                        Response.Write("\n");
                    }

                    #endregion

                    #region ReportSummeryForProduct

                    if (lblproduct.Text == "1")
                    {
                        decimal TQ = 0;
                        decimal T = 0;
                        decimal Dis = 0;
                        foreach (DataRow drtotal in dts.Rows)
                        {
                            TQ += drtotal["totalQuantity"] == DBNull.Value
                                ? 0
                                : Convert.ToDecimal(drtotal["totalQuantity"].ToString());
                        }

                        Response.Write(sw.ToString());

                        Response.Write("Product Report Summery");
                        Response.Write("\n");
                        Response.Write("\n");
                        Response.Write("Total Quantity");
                        tab = "\t";
                        Response.Write(tab + TQ);

                        Response.Write("\n");
                        Response.Write("\n");
                        Response.Write("Product Name");
                        tab = "\t";
                        Response.Write(tab + "Total Quantity");
                        tab = "\t";
                        Response.Write(tab + "Total Price");
                        tab = "\t";
                        Response.Write(tab + "Requisition Code");
                        tab = "\t";

                        Response.Write("\n");

                        foreach (DataRow drD in dts.Rows)
                        {
                            tab = "";

                            Response.Write(tab + drD["ProductName"].ToString());
                            tab = "\t";
                            Response.Write(tab + drD["totalQuantity"].ToString());
                            tab = "\t";
                            Response.Write(tab + drD["TotalPrice"].ToString());
                            tab = "\t";
                            Response.Write(tab + drD["ReqCode"].ToString());
                            tab = "\t";

                            Response.Write("\n");
                        }
                    }

                    #endregion

                    #region Allsummery

                    decimal tq = 0;
                    decimal tp = 0;
                    decimal td = 0;
                    Response.Write("\n");
                    Response.Write("\n");
                    Response.Write("All Information");
                    Response.Write("\n");
                    //foreach (DataColumn dc in dt.Columns)
                    //{
                    Response.Write(sw.ToString());
                    Response.Write("Dealer Name");
                    tab = "\t";
                    Response.Write(tab + "Date");
                    tab = "\t";
                    Response.Write(tab + "Requisition Code");
                    tab = "\t";
                    Response.Write(tab + "Statue");
                    tab = "\t";
                    Response.Write(tab + "Courier Name");
                    tab = "\t";
                    Response.Write(tab + "Product Name");
                    tab = "\t";
                    Response.Write(tab + "Color");
                    tab = "\t";
                    Response.Write(tab + "Quantity");
                    tab = "\t";
                    Response.Write(tab + "Price");
                    tab = "\t";
                    Response.Write(tab + "Discount");
                    tab = "\t";
                    Response.Write(tab + "Total Price");
                    tab = "\t";
                    Response.Write(tab + "CS");
                    tab = "\t";
                    Response.Write(tab + "Jr.CS");
                    tab = "\t";
                    Response.Write(tab + "CM");
                    tab = "\t";
                    Response.Write(tab + "ACM");
                    tab = "\t";
                    Response.Write(tab + "Area");
                    tab = "\t";
                    Response.Write(tab + "Region");
                    tab = "\t";

                    Response.Write("\n");



                    foreach (DataRow dr in dt.Rows)
                    {
                        tab = "";

                        Response.Write(tab + dr["DealerName"].ToString());
                        tab = "\t";
                        DateTime Rdt = DateTime.Parse(dr["RequisitionDate"].ToString());

                        Response.Write(tab + Rdt.ToString("dd/MM/yyyy"));
                        tab = "\t";
                        Response.Write(tab + dr["RequisitionCode"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Status"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["CourierName"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["ProductName"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Color"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Quantity"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Price"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Discount"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["LineTotal2"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["Cs"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["JrCs"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["CM"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["AreaCM"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["AreaName"].ToString());
                        tab = "\t";
                        Response.Write(tab + dr["RegionName"].ToString());
                        tab = "\t";
                        tq += dr["Quantity"].ToString() == string.Empty ? 0 : decimal.Parse(dr["Quantity"].ToString());
                        tp += dr["ItemTotal"].ToString() == string.Empty ? 0 : decimal.Parse(dr["ItemTotal"].ToString());
                        td += dr["Discount"].ToString() == string.Empty ? 0 : decimal.Parse(dr["Discount"].ToString());

                        Response.Write("\n");
                    }
                    Response.Write("\n");
                    Response.Write("Total Quantity");
                    tab = "\t";

                    Response.Write(tab + tq);
                    tab = "\t";
                    Response.Write("\n");
                    Response.Write("Total Discount");
                    tab = "\t";
                    Response.Write(tab + td);
                    tab = "\t";
                    Response.Write("\n");
                    Response.Write("Total Amount");
                    tab = "\t";
                    Response.Write(tab + tp);
                    tab = "\t";

                    #endregion

                    Response.End();
                    Response.Flush();
                    sw.Close();
                    sw.Dispose();

                }
            }
            catch (Exception ex)
            {
                Alert.Show(ex.Message);
            }
        }


        protected void rdropDealer_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected void rdropDealer_OnDataBound(object sender, EventArgs e)
        {
            rdropDealer.Items.Insert(0, new RadComboBoxItem());
        }

        protected void RadGridSummary_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            GetSummary(lblgroupElement.Text);
        }

        protected void RadGridSummary_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            GetSummary(lblgroupElement.Text);
        }

        protected void RadGridSummary_OnGroupsChanging(object sender, GridGroupsChangingEventArgs e)
        {
            GetSummary(lblgroupElement.Text);
        }

        protected void RadgridProduct_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {


            this.GetProductSummary();
        }

        protected void RadgridProduct_OnPageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {

            this.GetProductSummary();
        }

        //protected void btnArea_OnClick(object sender, EventArgs e)
        //{
        //    lblgroupElement.Text = "AreaName";
        //    showSalseGrid.Visible = true;
        //    showSummery.Visible = true;
        //    showProductSummary.Visible = false;
        //    lblproduct.Text = "0";
        //    lblsummary.Text = "1";
        //    GetSummary(lblgroupElement.Text);
        //    lblGroupCaption.Text = "Area";
        //}

        //protected void btnRegion_OnClick(object sender, EventArgs e)
        //{
        //    lblgroupElement.Text = "RegionName";
        //    showSalseGrid.Visible = true;
        //    showSummery.Visible = true;
        //    showProductSummary.Visible = false;
        //    lblproduct.Text = "0";
        //    lblsummary.Text = "1";
        //    lblGroupCaption.Text = "Region";
        //    GetSummary(lblgroupElement.Text);
        //}

        //protected void btnCM_OnClick(object sender, EventArgs e)
        //{
        //    lblgroupElement.Text = "CM";
        //    showSalseGrid.Visible = true;
        //    showSummery.Visible = true;
        //    showProductSummary.Visible = false;
        //    lblproduct.Text = "0";
        //    lblsummary.Text = "1";
        //    lblGroupCaption.Text = "CM";
        //    GetSummary(lblgroupElement.Text);
        //}

        //protected void btnAcm_OnClick(object sender, EventArgs e)
        //{
        //    lblgroupElement.Text = "AreaCM";
        //    showSalseGrid.Visible = true;
        //    showSummery.Visible = true;
        //    showProductSummary.Visible = false;
        //    lblproduct.Text = "0";
        //    lblsummary.Text = "1";
        //    lblGroupCaption.Text = "ACM";
        //    GetSummary(lblgroupElement.Text);
        //}

        //protected void btnCs_OnClick(object sender, EventArgs e)
        //{
        //    lblgroupElement.Text = "Cs";
        //    showSalseGrid.Visible = true;
        //    showSummery.Visible = true;
        //    showProductSummary.Visible = false;
        //    lblproduct.Text = "0";
        //    lblsummary.Text = "1";
        //    lblGroupCaption.Text = "CS";
        //    GetSummary(lblgroupElement.Text);
        //}

        //protected void btnJcs_OnClick(object sender, EventArgs e)
        //{
        //    lblgroupElement.Text = "JrCs";
        //    showSalseGrid.Visible = true;
        //    showSummery.Visible = true;
        //    showProductSummary.Visible = false;
        //    lblproduct.Text = "0";
        //    lblsummary.Text = "1";
        //    lblGroupCaption.Text = "Jr.CS";
        //    GetSummary(lblgroupElement.Text);
        //}

        protected void RadGridSummary_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridGroupHeaderItem)
            {
                GridGroupHeaderItem item = (GridGroupHeaderItem)e.Item;
                DataRowView groupDataRow = (DataRowView)e.Item.DataItem;
                string groupText = item.DataCell.Text;
                groupText = groupText.Substring(groupText.IndexOf(": ") + 2, groupText.Length - groupText.IndexOf(":") - 2);
                item.DataCell.Text = lblGroupCaption.Text + ": " + groupText;
            }
        }
    }
}