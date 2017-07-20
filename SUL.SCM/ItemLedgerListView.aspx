<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="ItemLedgerListView.aspx.cs" Inherits="SUL.SCM.ItemLedgerListView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <style>
        .RadComboBox_Metro table td.rcbInputCellLeft { height: 30px; }

        .RadGrid_Metro .rgFooter td, .RadGrid_Metro .rgFooterWrapper {
            background-color: #525252;
            border-color: #525252 #525252;
            border-top: 1px solid;
            color: white;
        }
    </style>
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Search Item Journal List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">WareHouse</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="RadComboBox1" runat="server" Skin="Metro" Width="220px" AutoPostBack="True" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropProduct" runat="server" Skin="Metro" Width="220px" OnDataBound="rdropProduct_OnDataBound" AutoPostBack="True" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Color</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropColor" runat="server" Skin="Metro" Width="220px" AutoPostBack="True" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="row">
                                <div class="col-md-11" style="padding-right: 27px">
                                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_OnClick" Text="Search" CssClass="btn btn-inverse pull-left"/>
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                    <%-- <asp:Button ID="Button2" runat="server" Text="Clear Info" CssClass="btn btn-warning pull-left" />--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Item Ledger List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridItemLedger" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True"
                                             OnPageIndexChanged="RadGridItemLedger_OnPageIndexChanged" OnPageSizeChanged="RadGridItemLedger_OnPageSizeChanged"
                                             Skin="Metro" Width="100%" GridLines="None" OnItemCommand="RadGridItemLedger_OnItemCommand" ShowFooter="true">
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Scrolling AllowScroll="False" UseStaticHeaders="True"/>
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
                                        <telerik:GridBoundColumn DataField="product" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="product" UniqueName="colproduct" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UnitCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Unit" UniqueName="colUnit" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="WareHouse" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="WareHouse" UniqueName="colWareHouse" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ListValue" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Color" UniqueName="colListValue" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TotalIn" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left"
                                                                 HeaderText="Total In" UniqueName="colTotalIn" Display="True" Aggregate="Sum" FooterText="Total In: " ReadOnly="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="TotalOut" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left"
                                                                 HeaderText="Total Out" UniqueName="colTotalOut" Visible="True" Aggregate="Sum" FooterText="Total Out: " ReadOnly="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="Balance" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left"
                                                                 HeaderText="Balance" UniqueName="colBalance" Visible="True" Aggregate="Sum" FooterText="Balance: " ReadOnly="True">
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
                        <div class="row">
                            <div class="col-md-11" style="padding-right: 27px">
                                <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_OnClick" Text="Print" CssClass="btn btn-inverse pull-left"/>
                                <div style="float: left; height: 3px; width: 10px;"></div>
                                <asp:Button ID="btnExcel" runat="server" Text="Export To Excel" OnClick="btnExcel_OnClick" CssClass="btn btn-warning pull-left" />           
                                <asp:Label runat="server" ID="lblsearchBtn" Visible="False"></asp:Label>
                                <asp:Label runat="server" ID="lblsource" Visible="False"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>