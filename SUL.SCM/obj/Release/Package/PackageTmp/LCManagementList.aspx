<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="LCManagementList.aspx.cs" Inherits="SUL.SCM.LCManagementList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <style>
        .incpayment {
            margin-left: 20px;
        }

        .RadComboBox_Metro table td.rcbInputCellLeft {
            height: 30px;
        }
    </style>
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">L/C List</h3>
                    </div>
                    <asp:Label runat="server" ID="lblisNewEntry" Visible="False"/>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridLCInfo" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnPageIndexChanged="RadGridLCInfo_OnPageIndexChanged" OnPageSizeChanged="RadGridLCInfo_OnPageSizeChanged" Skin="Metro" Width="100%" Height="550px" GridLines="None" OnItemCommand="RadGridLCInfo_OnItemCommand">
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
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
                                        <telerik:GridBoundColumn DataField="VendorName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Vendor Name" UniqueName="colVendorName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="VendorAddress" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Vendor Address" UniqueName="colVandorAddress" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PINumber" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.I. No" UniqueName="colPINo" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LCNumber" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="L.C. Number" UniqueName="colLCNumber" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LCDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="L.C. Date" DataFormatString="{0:MMM dd, yyyy}" UniqueName="colLCDate" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LCExpiryDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="L.C. Expiry Date" DataFormatString="{0:MMM dd, yyyy}" UniqueName="colLCExpiryDate" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LCValue" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="L.C. Value" UniqueName="colLCValue" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LCStatus" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="L.C. Status" UniqueName="colLCStatus" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LCExpiryDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="L.C. Expiry Date" DataFormatString="{0:MMM dd, yyyy}" UniqueName="colLCExpiryDate" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="FileName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="File Name" UniqueName="colFileName" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                       <%-- <telerik:GridBoundColumn DataField="AmendmentNumber" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Amendment Number" UniqueName="colAmendmentNumber" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AmendementDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Amendment Date" DataFormatString="{0:MMM dd, yyyy}" UniqueName="colAmendmentDate" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>--%>

                                        <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" OnClick="btnEdit_OnClick" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="Images/Edit.png" Width="18px" Height="18px" ValidationGroup="button" />
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
