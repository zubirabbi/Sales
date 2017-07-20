<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="ReceiveingInfoList.aspx.cs" Inherits="SUL.SCM.ReceiveingInfoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Receiveing List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridReceiverMaster" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnPageIndexChanged="RadGridReceiverMaster_OnPageIndexChanged" OnPageSizeChanged="RadGridReceiverMaster_OnPageSizeChanged" Skin="Metro" Width="100%" Height="550px" GridLines="None" OnItemCommand="RadGridReceiverMaster_OnItemCommand">
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
                                        <telerik:GridBoundColumn DataField="ReceivingCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Receiving Code" UniqueName="colReceivingCode" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReceivingDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Receiving Date" DataFormatString="{0:MMM dd, yyyy}" UniqueName="colReceivingDate" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Code" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Vendor Code" UniqueName="colCode" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="LCNumber" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="L.C. Number" UniqueName="colLCNumber" Visible="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PINo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.I. No" UniqueName="colPINo" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Expr1" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Warehouse Name" UniqueName="colName" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="InvoiceNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Invoice No" UniqueName="colInvoiceNo" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReceivedBy" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Received By" UniqueName="colReceivedBy" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="IsInvoiceCreated" HeaderText="Invoice Status" ItemStyle-HorizontalAlign="Left" AllowFiltering="False" HeaderStyle-Width="100px" ColumnEditorID="GridTextBoxEditor" UniqueName="colIsInvoiceCreated" Display="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Left" AllowFiltering="False" HeaderStyle-Width="100px" ColumnEditorID="GridTextBoxEditor" UniqueName="colStatus" Display="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" OnClick="btnEdit_OnClick" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="Images/Edit.png" Width="18px" Height="18px" ValidationGroup="button"/>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="Invoice" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colInvoice">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnInvoice" OnClick="btnInvoice_OnClick" CommandArgument='<%# Eval("Id") + ";" + Eval("IsInvoiceCreated") %>' runat="server">
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="Images/Invoice.png" Width="18px" Height="18px" ValidationGroup="button"/>
                                                </asp:LinkButton>
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
                    
                    <asp:Label runat="server" Visible="False" ID="lblsource"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>