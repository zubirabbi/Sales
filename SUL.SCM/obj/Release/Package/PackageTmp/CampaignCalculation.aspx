<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CampaignCalculation.aspx.cs" Inherits="SUL.SCM.CampaignCalculation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .RadComboBox_Metro table td.rcbInputCellLeft {
            height: 30px;
        }
    </style>
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadWindowManager ID="RadWindowMessage" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
    <div id="content">

        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Campaign Calculation Info</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Campaign Code</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropCampaignCode" runat="server" OnDataBound="rdropCampaignCode_OnDataBound" OnSelectedIndexChanged="rdropCampaignCode_OnSelectedIndexChanged" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 pull-left">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_OnClick" CssClass="btn btn-inverse pull-left" />
                            <div style="float: left; height: 3px; width: 10px;"></div>

                        </div>
                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblcompanyId" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblsource" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblStartDate" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblEndDate" runat="server" Visible="False"></asp:Label>

                    </div>
                </div>
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Campaign List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridCampaignCal" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True"
                                OnPageIndexChanged="RadGridCampaignCal_OnPageIndexChanged" OnPageSizeChanged="RadGridCampaignCal_OnPageSizeChanged" PageSize="20"
                                Height="450px" Skin="Metro" Width="100%" GridLines="None" AllowFilteringByColumn="True">
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
                                        <telerik:GridBoundColumn DataField="CampaignId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="CampaignId" UniqueName="colCampaignId" AutoPostBackOnFilter="True" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DealerId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="DealerId" UniqueName="colDealerId" AutoPostBackOnFilter="True" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DealerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="False" AllowFiltering="False" HeaderText="Dealer Name" UniqueName="colDealerName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TotalAmount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total Amount" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colTotalAmount" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CampaignName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Campaign Name" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colCampaignCode" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Discount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Discount" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colDiscount" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Amount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Amount" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colAmount" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="IsUpdate" HeaderText="Update" ColumnEditorID="GridTextBoxEditor" AutoPostBackOnFilter="False" AllowFiltering="False">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkIsUpdate" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
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
                    <div class="row">
                        <div class="col-md-6 pull-left">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left" />
                            <div style="float: left; height: 3px; width: 10px;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
