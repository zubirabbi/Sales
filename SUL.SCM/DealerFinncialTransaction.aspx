<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="DealerFinncialTransaction.aspx.cs" Inherits="SUL.SCM.DealerFinncialTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
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
                                        <telerik:RadDatePicker ID="dtfromDate" runat="server" Width="280px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">To Date</label>
                                    <div class="col-md-8">
                                        <telerik:RadDatePicker ID="dttoDate" runat="server" Width="280px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropDealer" runat="server" Skin="Metro" Width="220px" OnDataBound="rdropDealer_OnDataBound" AutoPostBack="True" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
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
                        <h3 class="heading">Dealer Ledger List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridDealerTransaction" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnPageIndexChanged="RadGridDealerTransaction_OnPageIndexChanged" OnPageSizeChanged="RadGridDealerTransaction_OnPageSizeChanged" Skin="Metro" Width="100%" GridLines="None" OnItemCommand="RadGridDealerTransaction_OnItemCommand">
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
                                        <telerik:GridBoundColumn DataField="InvoiceDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Invoice Date" UniqueName="colInvoiceDate" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="InvoiceNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Invoice No" UniqueName="colInvoiceNo" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DealerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Name" UniqueName="colDealerName" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Name" UniqueName="colProductName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Quantity" UniqueName="colQuantity" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="Color" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Color" UniqueName="colColor" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="Price" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Price" UniqueName="colPrice" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ItemTotal" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Item Total" UniqueName="colItemTotal" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Discount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Discount" UniqueName="colDiscount" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Total" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total" UniqueName="colTotal" Visible="True">
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