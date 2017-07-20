<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReceiptVoucherList.aspx.cs" Inherits="SUL.SCM.ReceiptVoucherList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Receive Voucher</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridReceiptVoucher" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnPageIndexChanged="RadGridReceiptVoucher_PageIndexChanged" OnPageSizeChanged="RadGridReceiptVoucher_PageSizeChanged" Skin="Metro" Width="100%" Height="550px" GridLines="None" OnItemCommand="RadGridReceiptVoucher_ItemCommand">
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"/>
                                </ClientSettings>
                                <MasterTableView>
                                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                    <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                        <HeaderStyle Width="20px" Height="100px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                        <HeaderStyle Width="20px" Height="100px"></HeaderStyle>
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
                                         <telerik:GridBoundColumn DataField="MoneyReceiptNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="VoucherType" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PaymentType" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="VoucherType" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PaymentDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Voucher Date" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="DealerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Cr_LedName" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="BankName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dr_LeadName" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="Amount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Vch_Amt" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LstPaymentType" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Transection Type" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MoneyReceiptNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Cheque_no/Instrument Number" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="Price" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Favouring Name" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BankName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Bank Name" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Branch Name" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="Status" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Narration" UniqueName="colName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                       
                                        <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="Images/Edit.png" Width="18px" Height="18px" ValidationGroup="button"/>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

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
                          <div class="row">
                        <div class="col-md-11" style="padding-right: 27px">
                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" CssClass="btn btn-inverse pull-left" />
                            <div style="float: left; height: 3px; width: 10px;"></div>
                            <asp:Button ID="btnExportToExcel" runat="server"  OnClick="btnExportToExcel_Click" Text="Export To Excel" CssClass="btn btn-inverse pull-left" />
                            <%-- <asp:Button ID="Button2" runat="server" Text="Clear Info" CssClass="btn btn-warning pull-left" />--%>
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
    </div>
</asp:Content>
