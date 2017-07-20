<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DataExport.aspx.cs" Inherits="SUL.SCM.DataExport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Data Export </h3>
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
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 pull-left">
                            <asp:Button ID="btnSales" runat="server" Text="Sales Voucher" OnClick="btnSales_OnClick" CssClass="btn btn-inverse pull-left" />
                            <div style="float: left; height: 3px; width: 10px;"></div>
                            <asp:Button ID="btnReceipt" runat="server" Text="Receipt Voucher" OnClick="btnReceipt_OnClick" CssClass="btn btn-warning pull-left" />
                        </div>
                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                        <asp:Label runat="server" ID="lblsource" Visible="False" />
                    </div>
                </div>
                <asp:Panel runat="server" ID="showSales" Visible="False">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Sales Receipt</h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="RadGridSales" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnPageIndexChanged="RadGridSales_OnPageIndexChanged" OnPageSizeChanged="RadGridSales_OnPageSizeChanged" Skin="Metro" Width="100%" GridLines="None">
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
                                            <telerik:GridBoundColumn DataField="Sales_Voucher_Number " FilterControlAltText="Filter column column" HeaderText="Sales_Voucher_Number" UniqueName="colId" Display="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Refference_Number" FilterControlAltText="Filter column column"  ItemStyle-HorizontalAlign="Left" HeaderText="Refference_Number" UniqueName="colInvoiceDate" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Invoice_Date" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MMM dd, yyyy}" HeaderText="Invoice_Date" UniqueName="colInvoiceNo" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Party_Name" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Party_Name" UniqueName="colRequisitionCode" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Item_Name" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Item_Name" UniqueName="colDealerName" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Actual_Quantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Actual_Quantity" UniqueName="colProductName" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Billed_Quantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Billed_Quantity" UniqueName="colQuantity" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Rate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Rate" UniqueName="colPrice" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Discount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Discount" UniqueName="colItemTotal" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Amount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Amount" UniqueName="colDiscount" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Sale_Ledger" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Sale_Ledger" UniqueName="colCourierName" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Godown_Name" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Godown_Name" UniqueName="colCourierName" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Narration" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Narration" UniqueName="colCourierName" Visible="true">
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
                <asp:Panel runat="server" ID="showBank" Visible="False">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Receipt Voucher</h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="RadGridReceipt" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnPageIndexChanged="RadGridReceipt_OnPageIndexChanged" OnPageSizeChanged="RadGridReceipt_OnPageSizeChanged" Skin="Metro" Width="100%" GridLines="None">
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
                                            <telerik:GridBoundColumn DataField="Voucher_No" FilterControlAltText="Filter column column" HeaderText="Voucher_No" UniqueName="colVoucher_No" Display="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Vocuher_Type" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Voucher Type" UniqueName="colMoneyReceiptNo" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Voucher_Date" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Voucher_Date" DataFormatString="{0:MMM dd, yyyy}" UniqueName="colPaymentType" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cr_LedName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Cr_LedName (Cr. Side)" UniqueName="colLstPaymentMode" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Dr_LedName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dr_LedName (Dr. Side)" UniqueName="colAmount" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Vch_Amt" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Vch_Amt" UniqueName="colBankName" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Tranction_Type" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Tranction Type" UniqueName="colBranch" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cheque_No_Instrument_Number" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Cheque_No/ Instrument Number" UniqueName="colBranch" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Favouring_Name" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Favouring Name" UniqueName="colBranch" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Bank_Name_deposit_chque_bank" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Bank Name (deposit/ chque bank)" UniqueName="colBranch" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Branch_Name" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Branch" UniqueName="colBranch" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Narrantion" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Narrantion" UniqueName="colBranch" Visible="true">
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
                <asp:Label runat="server" ID="lblSales" Visible="False"></asp:Label>
                <asp:Label runat="server" ID="lblReceipt" Visible="False"></asp:Label>
                <div class="row">
                    <div class="col-md-6 pull-left">
                        <asp:Button ID="btnExccel" runat="server" Text="Export To Excel" OnClick="btnExccel_OnClick" CssClass="btn btn-inverse pull-left" />
                    </div>
                    <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
