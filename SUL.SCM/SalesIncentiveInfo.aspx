<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="SalesIncentiveInfo.aspx.cs" Inherits="SUL.SCM.SalesIncentiveInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .RadComboBox_Metro table td.rcbInputCellLeft { height: 30px; }
    </style>
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <div id="content">

        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Sales Incentive Setup</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Designation</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropDesignation" runat="server" AutoPostBack="True" Skin="Metro" Width="220px" Font-Names="" OnSelectedIndexChanged="rdropDesignation_SelectedIndexChanged" OnDataBound="rdropDesignation_DataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Start Value</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtStartValue" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">End Value</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtEndValue" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Sales Incentives</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-12">
                                <h4>Discount</h4>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Percentage</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtPercentage" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-12">
                                <h4>Amount</h4>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Value</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtDiscountValue" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-12">
                                <h4>Gift</h4>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropGiftProduct" runat="server" AutoPostBack="True" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True" OnDataBound="rdropGiftProduct_DataBound">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Quantity</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtGiftQuantity" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="widget-body">
                        <telerik:RadGrid ID="radgridSalesIncentivesDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnItemCommand="" Skin="Metro" Width="100%" GridLines="None">
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

                                    <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="StartValue" FilterControlAltText="Filter column column" HeaderText="Start Value" UniqueName="colStartValue" Display="True">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EndValue" FilterControlAltText="Filter column column" HeaderText="End Value" UniqueName="colEndValue" Display="True">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DiscountPcnt" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Discount Pcnt" UniqueName="colDiscountPcnt" Display="True">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="OfferAmount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="End Quantity" UniqueName="colOfferAmount" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="GiftProductId" FilterControlAltText="Filter column column" HeaderText="GiftProductId" UniqueName="colGiftProductId" Display="False">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="GiftProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="GiftProduct Name" UniqueName="colGiftProductName" Display="True">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="GiftQuantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Gift Quantity" UniqueName="colGiftQuantity" Visible="true">
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
                        <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                    </div>--%>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 pull-left">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-inverse pull-left"/>
                    <div style="float: left; height: 3px; width: 10px;"></div>
                </div>
                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblDP2" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblcompanyId" runat="server" Visible="False"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>