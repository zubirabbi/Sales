<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ServiceList.aspx.cs" Inherits="SUL.SCM.ServiceList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div id="content">
        <h1 class="content-heading bg-white border-bottom">Service List</h1>
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget widget-inverse">
                            <div class="widget-head">
                                <h3 class="heading">Service List</h3>
                            </div>
                            <asp:Label runat="server" ID="lblisNewEntry" Visible="False" />
                            <div class="widget-body">
                                <div class="row">
                                    <telerik:RadGrid ID="RadGridService" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                        AllowPaging="True" OnPageIndexChanged="RadGridService_PageIndexChanged" OnPageSizeChanged="RadGridService_PageSizeChanged"
                                        Skin="Metro" Width="100%" Height="500px" GridLines="None" OnItemCommand="RadGridService_ItemCommand" AllowFilteringByColumn="True">
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
                                                <%-- <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" OnClick="btnEdit_OnClick" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="Images/Edit.png" Width="18px" Height="18px" ValidationGroup="button"/>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>--%>
                                                <telerik:GridBoundColumn DataField="Id" HeaderText="Id" UniqueName="colId" Display="false">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ServiceName" ItemStyle-HorizontalAlign="Left" HeaderText="Service Name" UniqueName="ServiceName" AutoPostBackOnFilter="True" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ServiceDescription" ItemStyle-HorizontalAlign="Left" HeaderText="Service Description" UniqueName="ServiceDescription" AutoPostBackOnFilter="True" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NWCost" ItemStyle-HorizontalAlign="Left" HeaderText="Non-Warrenty Cost" UniqueName="Non-WarrentyCost" AutoPostBackOnFilter="False" AllowFiltering="False" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="WCost" ItemStyle-HorizontalAlign="Left" HeaderText="Warrenty Cost" UniqueName="WarrentyCost" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IsSPReqired" ItemStyle-HorizontalAlign="Left" HeaderText="Is SP Required" UniqueName="IsSparePartsRequired" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ServiceTime" ItemStyle-HorizontalAlign="Left" HeaderText="Service Time" UniqueName="ServiceTime" DataFormatString="{0:MMM dd, yyyy}" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ServiceLevel" ItemStyle-HorizontalAlign="Left" HeaderText="Service Level" UniqueName="ServiceLevel" Display="False" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                
                                                <telerik:GridBoundColumn DataField="ListValue" ItemStyle-HorizontalAlign="Left" HeaderText="Service Level" UniqueName="ListValue" Visible="true" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" OnClick="btnEdit_Click" CommandArgument='<%# Eval("Id") %>' runat="server">
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
                                   <asp:Label runat="server" Visible="False" ID="lblsource"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
