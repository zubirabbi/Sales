<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ServiceCentreList.aspx.cs" Inherits="SUL.SCM.ServiceCentreList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <div id="content">
        <h1 class="content-heading bg-white border-bottom">Service Centre List</h1>
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget widget-inverse">
                            <div class="widget-head">
                                <h3 class="heading">Service Centre List</h3>
                            </div>
                             <asp:Label runat="server" ID="lblisNewEntry" Visible="False"/>
                            <div class="widget-body">
                                <div class="row">
                                    <telerik:RadGrid ID="RadGridServiceCentre" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                                     AllowPaging="True" OnPageIndexChanged="RadGridServiceCentre_PageIndexChanged" OnPageSizeChanged="RadGridServiceCentre_PageSizeChanged"
                                                     Skin="Metro" Width="100%" Height="500px" GridLines="None" OnItemCommand="RadGridServiceCentre_ItemCommand" AllowFilteringByColumn="True">
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
                                                <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SCCode"  ItemStyle-HorizontalAlign="Left" HeaderText="SC Code" UniqueName="SCCode" AutoPostBackOnFilter="True" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AreaId" ItemStyle-HorizontalAlign="Left" HeaderText="Area Id" UniqueName="AreaId" AutoPostBackOnFilter="True" Display="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="SCName" ItemStyle-HorizontalAlign="Left" HeaderText="SC Name" UniqueName="SCName" AutoPostBackOnFilter="False" AllowFiltering="False" Display="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SCAddress" ItemStyle-HorizontalAlign="Left" HeaderText="SC Address" UniqueName="SCAddress" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AreaName" ItemStyle-HorizontalAlign="Left" HeaderText="Area Name" UniqueName="AreaName" AutoPostBackOnFilter="True" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="InChargeId" ItemStyle-HorizontalAlign="Left" HeaderText="InCharge Id" UniqueName="InChargeId" AutoPostBackOnFilter="False" AllowFiltering="False" Display="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EmployeeName" ItemStyle-HorizontalAlign="Left" HeaderText="Employee Name" UniqueName="EmployeeName" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EstablishDate" ItemStyle-HorizontalAlign="Left" HeaderText="Establish Date" UniqueName="EstablishDate" DataFormatString="{0:MMM dd, yyyy}" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IsActive" ItemStyle-HorizontalAlign="Left" HeaderText="Is Active" UniqueName="IsActive" Visible="true" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" OnClick="btnEdit_Click" CommandArgument='<%# Eval("Id") %>' runat="server">
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
                                    
                                       <asp:Label runat="server" Visible="False" ID="lblsource"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
