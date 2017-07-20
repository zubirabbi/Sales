<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LedgerStatement.aspx.cs" Inherits="SUL.SCM.LedgerStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <style>
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
                        <h3 class="heading">Search Dealer Ledger Statement</h3>
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
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropDealer" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True" OnDataBound="rdropDealer_OnDataBound">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                                    <div class="col-md-8">
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_OnClick" Text="Search" CssClass="btn btn-inverse pull-left" />
                                    </div>
                                    <%-- <asp:Button ID="Button2" runat="server" Text="Clear Info" CssClass="btn btn-warning pull-left" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>

                    </div>
                </div>
            </div>
            <div class="widget widget-inverse">
                <div class="widget-head">
                    <h3 class="heading">Dealer Ledger Statement</h3>
                </div>
                <div class="widget-body">
                    <div class="row">
                        <telerik:RadGrid ID="RadGridLedgerStatement" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True"
                            OnPageIndexChanged="RadGridLedgerStatement_OnPageIndexChanged" OnPageSizeChanged="RadGridLedgerStatement_OnPageSizeChanged"
                            Skin="Metro" Width="100%" GridLines="None" ShowFooter="True">
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                <Scrolling AllowScroll="False" UseStaticHeaders="True" />
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
                                    <telerik:GridBoundColumn DataField="DealerId" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DealerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Name" UniqueName="colName" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TransactionDate" FilterControlAltText="Filter column column" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderText="Transaction Date" UniqueName="colTransactionDate" Display="True">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EnterBy" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Enter By" UniqueName="colEnterBy" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TransactionType" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Transaction Type" UniqueName="colTransactionType" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="InvoiceNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Invoice No" UniqueName="colInvoiceNo" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ModelNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Model No" UniqueName="colModelNo" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Name" UniqueName="colProductName" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Quantity" UniqueName="colQuantity" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BankName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Bank Name" UniqueName="colBankName" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Branch" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Branch" UniqueName="colBranch" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="OpeniModelNongBalance" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left"
                                        HeaderText="Opening Balance" UniqueName="colOpeningBalance" Display="True">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Debit" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left"
                                        HeaderText="Debit" UniqueName="colDebit" Visible="True" Aggregate="Sum" FooterText="Debit: " ReadOnly="True" DataType="System.Decimal">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="Cradit" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left"
                                        HeaderText="Credit" UniqueName="colCradit" Visible="True" Aggregate="Sum" FooterText="Cradit: " ReadOnly="True" DataType="System.Decimal">
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
                                    <telerik:GridBoundColumn DataField="Remarks" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Remarks" UniqueName="colRemarks" Visible="True">
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
                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_OnClick" Text="Print" CssClass="btn btn-inverse pull-left" />
                            <div style="float: left; height: 3px; width: 10px;"></div>
                            <asp:Button ID="btnExportToExcel" runat="server" OnClick="btnExportToExcel_OnClick" Text="Export To Excel" CssClass="btn btn-inverse pull-left" />

                        </div>
                        <asp:Label runat="server" ID="lblsource" Visible="False"></asp:Label>
                        <asp:Label runat="server" ID="lblsearchBtn" Visible="False"></asp:Label>
                        <asp:Label runat="server" ID="btnExcel" Visible="False"></asp:Label>
                        <asp:Label runat="server" ID="btnExportTo" Visible="False"></asp:Label>

                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
