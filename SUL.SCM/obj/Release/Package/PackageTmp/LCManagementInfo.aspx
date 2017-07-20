<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="LCManagementInfo.aspx.cs" Inherits="SUL.SCM.LCManagementInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
<style>
    .RadComboBox_Metro table td.rcbInputCellLeft { height: 30px; }
</style>
<div id="content">
<div class="wizard">
<!-- // Widget heading END -->
<div class="innerAll">
<div class="row">
<div class="widget widget-inverse">
    <div class="widget-head">
        <h3 class="heading">L/C Information </h3>
    </div>
    <div class="widget-body">
        <div class="row">

            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Vendor Name</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropVandorName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropVandorName_OnSelectedIndexChanged" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Vendor Address</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtVendorAddress" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">P.I. No</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropPINo" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">L/C Number</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtLcNumber" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">L/C Date</label>
                    <div class="col-md-8">
                        <telerik:RadDatePicker ID="rdtLcDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">L/C Expiry Date</label>
                    <div class="col-md-8">
                        <telerik:RadDatePicker ID="rdtLcExpDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">L/C Value</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtLCValue" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">L/C Status</label>
                    <div class="col-md-8">
                        <asp:DropDownList ID="rdropLCStatus" runat="server" CssClass="form-control" Skin="Metro" Width="220px" Font-Names="">
                            <asp:ListItem Value="selectone">--Select One--</asp:ListItem>
                            <asp:ListItem Value="lcopen">L/C Open</asp:ListItem>
                            <asp:ListItem Value="partiallyship">Partially Shipped</asp:ListItem>
                            <asp:ListItem Value="fullyship">Fully Shipped</asp:ListItem>
                            <asp:ListItem Value="lcclosed">L/C Closed</asp:ListItem>

                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Issuing Bank</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropIssuingBank" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Negotiating Bank</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropNegotiatingBank" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">File Name</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtFileName" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Scanned L/C Copy</label>
                    <div class="col-md-8">
                        <asp:FileUpload ID="ScanedCopy" runat="server"/>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</div>
<div class="widget widget-inverse">
    <div class="widget-head">
        <h3 class="heading">LC Amendment Information </h3>
    </div>
    <div class="widget-body">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Amendment Number</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtAmmendmentNo" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Amendment Date</label>
                    <div class="col-md-8">
                        <telerik:RadDatePicker ID="rdtAmmendementDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>


            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Amendment Description</label>
                    <div class="col-md-8">
                        <textarea runat="server" id="rtxtAmmendmentDesc" style="height: 70px; width: 220px;"></textarea>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                    <div class="col-md-8">
                        <asp:Button ID="btnAddLCAmendment" runat="server" OnClick="btnAddLCAmendment_OnClick" Text="Add LC Amendment" CssClass="btn btn-inverse"/>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="widget-body">
        <telerik:RadGrid ID="RadGridAddRequisitionDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnItemCommand="RadGridAddRequisitionDetails_OnItemCommand" Skin="Metro" Width="100%" GridLines="None">
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                <Scrolling AllowScroll="True" UseStaticHeaders="True"/>
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

                    <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="AmendmentNumber" FilterControlAltText="Filter column column" HeaderText="Amendment Number" UniqueName="colAmendmentNumber" Display="True">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="AmendementDate" FilterControlAltText="Filter column column" DataFormatString="{0:MMM dd, yyyy}" HeaderText="Amendment Date" UniqueName="colAmendmentDate" Display="True">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="AmendmentDescription" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Amendment Description" UniqueName="colAmendmentDescription" Display="True">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn
                        CommandName="btnSelect"
                        HeaderText=""
                        SortExpression=""
                        ButtonType="ImageButton"
                        ImageUrl="Images/Edit.png"
                        UniqueName="colEdit">
                    </telerik:GridButtonColumn>
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
        <asp:Label ID="lblDetails" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblPaymentId" runat="server" Visible="False"></asp:Label>
    </div>
</div>

<div class="row">
    <div class="col-md-6 pull-left">
        <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left"/>
        <div style="float: left; height: 3px; width: 10px;"></div>
        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_OnClick" Text="Clear Info" CssClass="btn btn-warning pull-left"/>
    </div>
    <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblImageName" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblAmmId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
</div>
<br/>
<%--  <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Product List</h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="RadGridProducr" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnPageIndexChanged="RadGridProducr_OnPageIndexChanged" OnPageSizeChanged="RadGridProducr_OnPageSizeChanged" Skin="Metro" Width="90%" GridLines="None" OnItemCommand="RadGridProducr_OnItemCommand">
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
                                            <telerik:GridButtonColumn
                                                CommandName="btnSelect"
                                                HeaderText=""
                                                SortExpression=""
                                                ButtonType="ImageButton"
                                                ImageUrl="Images/Edit.png"
                                                UniqueName="colEdit">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ProductCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Code" UniqueName="colProductCode" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Name" UniqueName="colProductName" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ModelNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Model No" UniqueName="colModelNo" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ProductCategory" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Category" UniqueName="colProductCategory">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="BaseUnit" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Base Unit" UniqueName="colBaseUnit">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MRP" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="M.R.P." UniqueName="colMRP" Visible="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DP" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="D.P." UniqueName="colDP">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RP" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="R.P." UniqueName="colRP">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CostPrice" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Cost Price" UniqueName="colCostPrice" Visible="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CurrentBalance" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Current Balance" UniqueName="colCurrentBalance" Visible="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn
                                                CommandName="btnDelete"
                                                ConfirmText="Are you sure you want to delete this record?"
                                                ConfirmDialogType="RadWindow"
                                                HeaderText="Delete"
                                                ConfirmTitle="Delete"
                                                ButtonType="ImageButton"
                                                ImageUrl="Images/delete.png"
                                                UniqueName="colDelete">
                                            </telerik:GridButtonColumn>
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
                    </div>--%>
</div>

</div>
</div>
</div>
</asp:Content>