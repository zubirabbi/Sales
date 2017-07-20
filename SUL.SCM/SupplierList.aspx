<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="SupplierList.aspx.cs" Inherits="SUL.SCM.SupplierList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <div id="content">
        <h1 class="content-heading bg-white border-bottom">Supplier List</h1>
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget widget-inverse">
                            <div class="widget-head">
                                <h3 class="heading">Supplier List</h3>
                            </div>
                            <div class="widget-body">
                                <div class="row">
                                    <telerik:RadGrid ID="RadGridSupplier" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                                     AllowPaging="True" OnPageIndexChanged="RadGridSupplier_OnPageIndexChanged" OnPageSizeChanged="RadGridSupplier_OnPageSizeChanged"
                                                     Skin="Metro" Width="100%" Height="500px" GridLines="None" OnItemCommand="RadGridSupplier_OnItemCommand" AllowFilteringByColumn="True">
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
                                                        <asp:LinkButton ID="btnEdit" OnClick="btnEdit_OnClick" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="Images/Edit.png" Width="18px" Height="18px" ValidationGroup="button"/>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Name" UniqueName="colName" AutoPostBackOnFilter="True" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Code" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Code" UniqueName="colCode" AutoPostBackOnFilter="True" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CompanyAddress" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Company Address" UniqueName="colCompanyAddress" AutoPostBackOnFilter="False" AllowFiltering="False" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FactoryAddress" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Factory Address" UniqueName="colFactoryAddress" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Phone" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Phone" UniqueName="colPhone" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Mobile" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Mobile" UniqueName="colMobile" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Email" UniqueName="colEmail" Visible="true" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ContactPerson" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Contact Person" UniqueName="colContactPerson" Visible="true" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Designation" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Designation" UniqueName="colDesignation" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BankName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Bank Name" UniqueName="colBankName" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AccountNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Bank Account" UniqueName="colBankAccount" AutoPostBackOnFilter="False" AllowFiltering="False">
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