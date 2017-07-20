<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DealerBalanceReport.aspx.cs" Inherits="SUL.SCM.DealerBalanceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                    </div>
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Dealer Balance Report</h3>
                        </div>
                        <div class="widget-body">
                             <%--<div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="dtfromDate" runat="server" Width="225px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="row">
                                <telerik:RadGrid ID="RadGridDealerBalence" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                    AllowPaging="True" OnPageIndexChanged="RadGridDealer_OnPageIndexChanged" OnPageSizeChanged="RadGridDealer_OnPageSizeChanged"
                                    Skin="Metro" Width="100%" Height="350px" GridLines="None" AllowFilteringByColumn="True">
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
                                                    <asp:Label runat="server" ID="lblRowNumber" Width="50px" Text="<%# Container.DataSetIndex + 1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DealerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Name" UniqueName="colDealerName" AutoPostBackOnFilter="True" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DealerCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Code" UniqueName="colDealerCode" AutoPostBackOnFilter="True" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="TotalDebit" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total Debit" Display="True" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colDealerCategory">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TotalCredit" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total Credit" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colDealerCategoryCode">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Balance" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Balance" UniqueName="colStartDate" AutoPostBackOnFilter="False" AllowFiltering="False" Visible="True">
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
                <div class="row">
                    <div class="col-md-11" style="padding-right: 27px">
                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_OnClick" Text="Print" CssClass="btn btn-inverse pull-left" />
                        <div style="float: left; height: 3px; width: 10px;"></div>
                        <asp:Button ID="btnExportToExcel" runat="server" OnClick="btnExportToExcel_OnClick" Text="Export To Excel" CssClass="btn btn-inverse pull-left" />

                    </div>
                        <asp:Label runat="server" ID="lblsource" Visible="False"></asp:Label>

                </div>
            </div>
        </div>
    </div>
    </div>

</asp:Content>
