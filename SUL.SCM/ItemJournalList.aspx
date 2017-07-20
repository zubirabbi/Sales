<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="ItemJournalList.aspx.cs" Inherits="SUL.SCM.ItemJournalList" %>

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
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">From Date</label>
                                    <div class="col-md-8">
                                        <telerik:RadDatePicker ID="dtfromDate" runat="server" Width="225px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">To Date</label>
                                    <div class="col-md-8">
                                        <telerik:RadDatePicker ID="dttoDate" runat="server" Width="225px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>

                            <div class="clearfix"></div>
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
                        <h3 class="heading">Item Journal List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridItemJournal" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True"
                                             OnPageIndexChanged="RadGridItemJournal_OnPageIndexChanged" OnPageSizeChanged="RadGridItemJournal_OnPageSizeChanged"
                                             Skin="Metro" Width="100%" GridLines="None" OnItemCommand="RadGridItemJournal_OnItemCommand" ShowFooter="True">
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
                                        <telerik:GridBoundColumn DataField="TransactionDate" FilterControlAltText="Filter column column" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Transaction Date" UniqueName="colTransactionDate" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TransactionType" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Transaction Type" UniqueName="colTransactionType" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="WareHouse" UniqueName="colWareHouse" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="WareHouseFrom" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="WareHouse From" UniqueName="colWareHouseFrom" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Name" UniqueName="colListValu" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ListValue" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Color" UniqueName="colListValue" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="UnitCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Unit Code" UniqueName="colUnitCode" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SourceId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Source Id" UniqueName="colSourceId" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OpeningBalance" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Opening Balance"
                                                                 UniqueName="colOpeningBalance" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="QuantityIn" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left"
                                                                 HeaderText="Quantity In" UniqueName="colQuantityIn" Visible="True" Aggregate="Sum" FooterText="Quantity In: " ReadOnly="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="QuantityOut" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left"
                                                                 HeaderText="Quantity Out" UniqueName="colQuantityOut" Visible="True" Aggregate="Sum" FooterText="Quantity Out: " ReadOnly="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ClosingBalance" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left"
                                                                 HeaderText="Closing Balance" UniqueName="colClosingBalance" Visible="True">
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
                                 <asp:Button ID="btnExcel" runat="server" Text="Export Excel Report" OnClick="btnExcel_OnClick" CssClass="btn btn-warning pull-left" />
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