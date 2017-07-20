<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ServiceListInfo.aspx.cs" Inherits="SUL.SCM.ServiceListInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
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
                            <div class="widget-body">
                                <div class="row">
                                    <telerik:RadGrid ID="RadGridSupplier" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                                     AllowPaging="True" 
                                                     Skin="Metro" Width="100%" Height="500px" GridLines="None"  AllowFilteringByColumn="True">
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
                                                <telerik:GridTemplateColumn HeaderText="#" UniqueName="RowNumber">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRowNumber" Width="50px" Text="<%# Container.DataSetIndex + 1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit"  CommandArgument='<%# Eval("Id") %>' runat="server">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="Images/Edit.png" Width="18px" Height="18px" ValidationGroup="button"/>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="ServiceId" FilterControlAltText="Filter column column" HeaderText="Service ID" UniqueName="colId" Display="false">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ReceiveData" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Receive Data" UniqueName="colName" AutoPostBackOnFilter="True" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ServiceCentre" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Service Centre" UniqueName="colCode" AutoPostBackOnFilter="True" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CustomerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Customer Name" UniqueName="colCompanyAddress" AutoPostBackOnFilter="False" AllowFiltering="False" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ContactNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Contact No" UniqueName="colFactoryAddress" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Address" UniqueName="colPhone" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ProblemDescription" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Problem Description" UniqueName="colMobile" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                
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
        </div>
    </div>
</asp:Content>
