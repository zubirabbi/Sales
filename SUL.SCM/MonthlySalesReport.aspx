<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MonthlySalesReport.aspx.cs" Inherits="SUL.SCM.MonthlySalesReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .RadComboBox_Metro table td.rcbInputCellLeft {
            height: 30px;
        }
    </style>
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div id="content">

        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Monthly Sales Report </h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Start Date</label>
                                    <div class="col-md-8">
                                        <telerik:RadDatePicker ID="rdtStartDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">End Date</label>
                                    <div class="col-md-8">
                                        <telerik:RadDatePicker ID="rdtEndDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                                    <div class="col-md-8">
                                        <asp:CheckBox runat="server" ID="chkDealer" Text="Area Wise Search" AutoPostBack="True" OnCheckedChanged="chkDealer_OnCheckedChanged" />
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <asp:Panel ID="showDealerPanel" runat="server" Visible="False">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Region</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropRegion" runat="server" Skin="Metro" Width="220px" AutoPostBack="True" OnSelectedIndexChanged="rdropRegion_OnSelectedIndexChanged" Font-Names="" OnDataBound="rdropRegion_OnDataBound">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Area</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropArea" runat="server" Skin="Metro" Width="220px" Font-Names="" AutoPostBack="True" OnSelectedIndexChanged="rdropArea_OnSelectedIndexChanged" OnDataBound="rdropArea_OnDataBound">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropDealer" runat="server" Skin="Metro" Width="220px" Font-Names="" AutoPostBack="True" OnSelectedIndexChanged="rdropDealer_OnSelectedIndexChanged" OnDataBound="rdropDealer_OnDataBound" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>

                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                                    <div class="col-md-8">
                                        <asp:CheckBox runat="server" ID="chkProduct" AutoPostBack="True" Text="Product Wise Search" OnCheckedChanged="chkProduct_OnCheckedChanged" />
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <asp:Panel ID="ShowProductPanel" runat="server" Visible="False">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropProduct" runat="server" Skin="Metro" Width="220px" Font-Names="" OnDataBound="rdropProduct_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Color</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropColor" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </asp:Panel>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                                    <div class="col-md-8">
                                        <asp:CheckBox runat="server" ID="ckhEmployee" AutoPostBack="True" Text="Employee Wise Search" OnCheckedChanged="ckhEmployee_OnCheckedChanged" />
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <asp:Panel ID="showEmployeePanel" runat="server" Visible="False">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">C.M.</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropCM" runat="server" Skin="Metro" Width="220px" Font-Names="" OnDataBound="rdropCM_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">A C.M.</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropAcm" runat="server" Skin="Metro" Width="220px" Font-Names="" OnDataBound="rdropAcm_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">C.S.</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropCs" runat="server" Skin="Metro" Width="220px" Font-Names="" OnDataBound="rdropCs_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Jr. C.S</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropJcs" runat="server" Skin="Metro" Width="220px" Font-Names="" OnDataBound="rdropJcs_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Status</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="rdropStatus" runat="server" CssClass="form-control" Skin="Metro" Width="220px" Font-Names="">
                                            <asp:ListItem Value="SelectOne">---Select One---</asp:ListItem>
                                            <asp:ListItem Value="Created">Created</asp:ListItem>
                                            <asp:ListItem Value="Approved">Approve</asp:ListItem>
                                            <asp:ListItem Value="UnApproved">UnApprove</asp:ListItem>
                                            <asp:ListItem Value="Invoiced">invoice</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <asp:CheckBox ID="chkQuentity" runat="server" Text="Quantity" AutoPostBack="True" OnCheckedChanged="chkQuentity_OnCheckedChanged" />
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="rdropQuentity" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="rdropQuentity_OnSelectedIndexChanged" Skin="Metro" Width="120px" Font-Names="" Visible="False">
                                        <asp:ListItem Value="SelectOne">-Select One-</asp:ListItem>
                                        <asp:ListItem Value="equal"> = </asp:ListItem>
                                        <asp:ListItem Value="greater"> => </asp:ListItem>
                                        <asp:ListItem Value="less"> =< </asp:ListItem>
                                        <asp:ListItem Value="between">Between</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="rtxtQEqual" CssClass="form-control" Skin="Metro" Visible="False" EmptyMessage="Value" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="rtxtQGeter" CssClass="form-control" Skin="Metro" Visible="False" EmptyMessage="Value" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <asp:CheckBox ID="chkDiscount" runat="server" Text="Discount" AutoPostBack="True" OnCheckedChanged="chkDiscount_OnCheckedChanged" />
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="rdropDiscount" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="rdropDiscount_OnSelectedIndexChanged" Skin="Metro" Width="120px" Font-Names="" Visible="False">
                                        <asp:ListItem Value="SelectOne">---Select One---</asp:ListItem>
                                        <asp:ListItem Value="equal"> = </asp:ListItem>
                                        <asp:ListItem Value="greater"> => </asp:ListItem>
                                        <asp:ListItem Value="less"> =< </asp:ListItem>
                                        <asp:ListItem Value="between">Between</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="rtxtDEqual" CssClass="form-control" Skin="Metro" Visible="False" EmptyMessage="Value" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="rtxtDTotal" CssClass="form-control" Skin="Metro" Visible="False" EmptyMessage="Value" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <asp:CheckBox ID="ChkTotal" runat="server" Text="Total Price" AutoPostBack="True" OnCheckedChanged="ChkTotal_OnCheckedChanged" />
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="rdropTotal" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="rdropTotal_OnSelectedIndexChanged" Skin="Metro" Width="120px" Font-Names="" Visible="False">
                                        <asp:ListItem Value="SelectOne">---Select One---</asp:ListItem>
                                        <asp:ListItem Value="equal"> = </asp:ListItem>
                                        <asp:ListItem Value="greater"> => </asp:ListItem>
                                        <asp:ListItem Value="less"> =< </asp:ListItem>
                                        <asp:ListItem Value="between">Between</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="rtxtTEqual" CssClass="form-control" Skin="Metro" Visible="False" EmptyMessage="Value" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadTextBox ID="rtxtTGeter" CssClass="form-control" Skin="Metro" Visible="False" EmptyMessage="Value" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-md-12 pull-left">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Search Item</label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="rdropSarch" runat="server" CssClass="form-control" Skin="Metro" Width="220px" Font-Names="">
                                                <asp:ListItem Value="SelectOne">---Select One---</asp:ListItem>
                                                <asp:ListItem Value="product">Product Wise Search</asp:ListItem>
                                                <asp:ListItem Value="Region">Region Wise Search</asp:ListItem>
                                                <asp:ListItem Value="Area">Area Wise Search</asp:ListItem>
                                                <asp:ListItem Value="CM">CM Wise Search</asp:ListItem>
                                                <asp:ListItem Value="ACM">ACM Wise Search</asp:ListItem>
                                                <asp:ListItem Value="CS">CS Wise Search</asp:ListItem>
                                                <asp:ListItem Value="JCS">Jr.Cs Wise Search</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3 pull-left">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_OnClick" CssClass="btn btn-inverse pull-left" Width="150px" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                </div>
                                <%--<div class="col-md-3 pull-left">
                                    <asp:Button ID="btnProduct" runat="server" Text="Product Wise Search" OnClick="btnProduct_OnClick" CssClass="btn btn-inverse pull-left" Width="150px" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                </div>
                                <div class="col-md-3 pull-left">
                                    <asp:Button ID="btnRegion" runat="server" Text="Region Wise Search" OnClick="btnRegion_OnClick" CssClass="btn btn-inverse pull-left" Width="150px" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                </div>
                                <div class="col-md-3 pull-left">
                                    <asp:Button ID="btnArea" runat="server" Text="Area Wise Search" OnClick="btnArea_OnClick" CssClass="btn btn-inverse pull-left" Width="150px" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                </div>
                                <div class="col-md-3 pull-left">
                                    <asp:Button ID="btnCM" runat="server" Text="CM Wise Search" OnClick="btnCM_OnClick" CssClass="btn btn-inverse pull-left" Width="150px" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                </div>
                            </div>
                            <div class="col-md-12 pull-left">

                                <div class="col-md-3 pull-left">
                                    <asp:Button ID="btnAcm" runat="server" Text="ACM Wise Search" OnClick="btnAcm_OnClick" CssClass="btn btn-inverse pull-left" Width="150px" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                </div>
                                <div class="col-md-3 pull-left">
                                    <asp:Button ID="btnCs" runat="server" Text="CS Wise Search" OnClick="btnCs_OnClick" CssClass="btn btn-inverse pull-left" Width="150px" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                </div>
                                <div class="col-md-3 pull-left">
                                    <asp:Button ID="btnJcs" runat="server" Text="Jr.Cs Wise Search" OnClick="btnJcs_OnClick" CssClass="btn btn-inverse pull-left" Width="150px" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                </div>
                            </div>--%>
                            </div>
                            <asp:Label runat="server" Visible="false" ID="lblCount" />
                        </div>
                        <asp:Label runat="server" Visible="false" ID="lblAss" />
                        <asp:Label runat="server" Visible="false" ID="lblproduct" />
                        <asp:Label runat="server" Visible="false" ID="lblsummary" />
                        <asp:Label runat="server" Visible="false" ID="lblgroupElement" />
                        <asp:Label runat="server" Visible="false" ID="lblGroupCaption" />
                        <asp:Label runat="server" ID="lblsource" Visible="False"></asp:Label>
                        <asp:Panel ID="showSalseGrid" runat="server" Visible="False">
                            <div class="widget widget-inverse">
                                <div class="widget-head">
                                    <h3 class="heading">Sales Report Summery</h3>
                                </div>
                                <div class="widget-body">
                                    <div class="row">
                                        <telerik:RadGrid ID="RadGridSummary" runat="server" AutoGenerateColumns="False" CellSpacing="0" Height="500px" AllowPaging="True"
                                            OnPageIndexChanged="RadGridSummary_OnPageIndexChanged" PageSize="100" OnPageSizeChanged="RadGridSummary_OnPageSizeChanged"
                                            OnGroupsChanging="RadGridSummary_OnGroupsChanging" OnItemDataBound="RadGridSummary_OnItemDataBound"
                                            Skin="Metro" Width="100%" GridLines="None"
                                            ShowFooter="True">
                                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" AllowDragToGroup="True">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                <Selecting AllowRowSelect="True"></Selecting>
                                                <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                                    ResizeGridOnColumnResize="False"></Resizing>
                                            </ClientSettings>

                                            <MasterTableView>
                                                <GroupByExpressions>
                                                    <telerik:GridGroupByExpression>
                                                        <SelectFields>
                                                            <telerik:GridGroupByField FieldAlias="GroupElement" FieldName="GroupElement"></telerik:GridGroupByField>
                                                        </SelectFields>
                                                        <GroupByFields>
                                                            <telerik:GridGroupByField FieldName="GroupElement"></telerik:GridGroupByField>
                                                        </GroupByFields>
                                                    </telerik:GridGroupByExpression>
                                                </GroupByExpressions>
                                                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="#" UniqueName="RowNumber">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblRowNumber" Text="<%# Container.DataSetIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Name" UniqueName="colDealerName" Visible="true">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="totalQuantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total Quantity" UniqueName="colRequisitionDate" Aggregate="Sum" FooterText="Total Quantity: ">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TotalPrice" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total Price" UniqueName="colRequisitionCode" Aggregate="Sum" FooterText="Total Price: ">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ReqCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="No of Requisition" UniqueName="colStatus" Visible="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                    </EditColumn>
                                                </EditFormSettings>
                                                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                            </MasterTableView>
                                            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                            <FilterMenu EnableImageSprites="False">
                                            </FilterMenu>
                                            <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="showProductSummary" runat="server" Visible="False">
                            <div class="widget widget-inverse">
                                <div class="widget-head">
                                    <h3 class="heading">Product Report Summery</h3>
                                </div>
                                <div class="widget-body">
                                    <div class="row">
                                        <telerik:RadGrid ID="RadgridProduct" runat="server" AutoGenerateColumns="False" CellSpacing="0" Height="300px" AllowPaging="True"
                                            OnPageIndexChanged="RadgridProduct_OnPageIndexChanged" PageSize="20" OnPageSizeChanged="RadgridProduct_OnPageSizeChanged"
                                            Skin="Metro" Width="100%" GridLines="None" ShowFooter="True">
                                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                            </ClientSettings>
                                            <MasterTableView>

                                                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="#" UniqueName="RowNumber">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblRowNumber" Text="<%# Container.DataSetIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Name" UniqueName="colDealerName" Visible="true">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="totalQuantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total Quantity" UniqueName="colRequisitionDate" Aggregate="Sum" FooterText="Total Quantity: ">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TotalPrice" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total Price" UniqueName="colRequisitionCode" Aggregate="Sum" FooterText="Total Price: ">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ReqCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="No of Requisition" UniqueName="colStatus" Visible="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                    </EditColumn>
                                                </EditFormSettings>
                                                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                            </MasterTableView>
                                            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                            <FilterMenu EnableImageSprites="False">
                                            </FilterMenu>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="showSummery" runat="server" Visible="False">
                            <div class="widget widget-inverse">
                                <div class="widget-head">
                                    <h3 class="heading">All Information Report Summery</h3>
                                </div>
                                <div class="widget-body">
                                    <div class="row">
                                        <telerik:RadGrid ID="RadGridMonthlySalesReport" runat="server" AutoGenerateColumns="False" CellSpacing="0" Height="500px" AllowPaging="True" OnPageIndexChanged="RadGridMonthlySalesReport_PageIndexChanged" PageSize="30" OnPageSizeChanged="RadGridMonthlySalesReport_PageSizeChanged" Skin="Metro" Width="100%" GridLines="None">
                                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                            </ClientSettings>
                                            <MasterTableView>
                                                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                                    <HeaderStyle Width="20px"></HeaderStyle>
                                                </ExpandCollapseColumn>
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="#" UniqueName="RowNumber">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblRowNumber" Text="<%# Container.DataSetIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="DealerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Name" UniqueName="colDealerName" Visible="true">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="RequisitionDate" FilterControlAltText="Filter column column" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderText="Date" UniqueName="colRequisitionDate">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="RequisitionCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Requisition Code" UniqueName="colRequisitionCode">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Status" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Status" UniqueName="colStatus" Visible="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CourierName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Courier Name" UniqueName="colCourierName" Visible="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Name" UniqueName="colProductName" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Quantity" UniqueName="colQuantity" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Discount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Discount" UniqueName="colDiscount" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Color" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Color" UniqueName="colColor" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Price" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Price" UniqueName="colPrice" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="LineTotal2" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total" UniqueName="colItemTotal" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Cs" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Cs" UniqueName="colCs" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="JrCs" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Jr, Cs" UniqueName="colJrCs" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CM" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="C.M." UniqueName="colCM" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AreaCM" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Assistant CM" UniqueName="colAreaCM" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AreaName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Area Name" UniqueName="colAreaName" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="RegionName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Region Name" UniqueName="colRegionName" Display="True">
                                                        <ColumnValidationSettings>
                                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                                        </ColumnValidationSettings>
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings>
                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                    </EditColumn>
                                                </EditFormSettings>
                                                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                            </MasterTableView>
                                            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                            <FilterMenu EnableImageSprites="False">
                                            </FilterMenu>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>

                    <div class="row">
                        <div class="col-md-6 pull-left">
                            <asp:Button ID="btnPrintAll" runat="server" Text="Print" OnClick="btnPrintAll_OnClick" Visible="False" CssClass="btn btn-primary pull-left" />
                            <div style="float: left; height: 3px; width: 10px;"></div>
                            <%--<asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left" />--%>
                            <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" OnClick="btnExcel_OnClick" Visible="False" CssClass="btn btn-primary pull-left" />
                        </div>
                     
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
