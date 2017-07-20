<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="SupplierLedgerListView.aspx.cs" Inherits="SUL.SCM.SupplierLedgerListView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
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
                        <h3 class="heading">Supplier Ledger List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridsupplierLedger" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True"
                                             OnPageIndexChanged="RadGridsupplierLedger_OnPageIndexChanged" OnPageSizeChanged="RadGridsupplierLedger_OnPageSizeChanged"
                                             Skin="Metro" Width="100%" Height="550px" GridLines="None" OnItemCommand="RadGridsupplierLedger_OnItemCommand" ShowFooter="True">
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
                                        <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Supplier Name" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TransactionType" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Transaction Type" UniqueName="colTransactionType" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TransactionDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Transaction Date" UniqueName="colTransactionDate" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SourceId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Source Id" UniqueName="colSourceId" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OpeningBalance" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left"
                                                                 HeaderText="Opening Balance" UniqueName="colOpeningBalance" Display="True" Aggregate="Sum" FooterText="Opening Balance: " ReadOnly="True" DataType="System.Decimal">
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
                                                                 HeaderText="Cradit" UniqueName="colCradit" Visible="True" Aggregate="Sum" FooterText="Cradit: " ReadOnly="True" DataType="System.Decimal">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ClosingBalance" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left"
                                                                 HeaderText="Closing Balance" UniqueName="colClosingBalance" Visible="True" Aggregate="Sum" FooterText="Closing Balance: " ReadOnly="True" DataType="System.Decimal">
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
                                <%-- <asp:Button ID="Button2" runat="server" Text="Clear Info" CssClass="btn btn-warning pull-left" />--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>