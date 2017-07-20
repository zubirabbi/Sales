<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProductReceiving.aspx.cs" Inherits="SUL.SCM.ProductReceiving" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Product Receiving </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Transection Id</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdroptransectionId" runat="server" Skin="Metro" Width="220px" Font-Names="" AutoPostBack="True" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Send Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtSendDate" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Receive Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="rdtReceiveDate" runat="server" Width="225px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                            </div>

                        </div>
                    </div>
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Product Receiving Details</h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropProduct" runat="server" Skin="Metro" Width="220px" Font-Names="" AutoPostBack="True" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Color</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropColor" runat="server" Skin="Metro" Width="220px" Font-Names="" AutoPostBack="True" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Quantity</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtQuantity" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-6 pull-left">
                                    <asp:Button ID="btnAddDetails" runat="server" Text="Add Details" CssClass="btn btn-inverse pull-left" />

                                </div>
                            </div>
                        </div>

                        <div class="widget widget-inverse">
                            <div class="widget-body">
                                <div class="row">
                                    <telerik:RadGrid ID="RadGridProductReciving" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                        AllowPaging="True"
                                        Skin="Metro" Width="100%" Height="150px" GridLines="None" AllowFilteringByColumn="True">
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
                                                <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="Code" FilterControlAltText="Filter column column" HeaderText="Code" UniqueName="colId" Display="false">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Name" UniqueName="colName" AutoPostBackOnFilter="True" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Price" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Price" UniqueName="colCode" AutoPostBackOnFilter="True" Visible="true">
                                                    <ColumnValidationSettings>
                                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                                    </ColumnValidationSettings>
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Quantity" UniqueName="colCode" AutoPostBackOnFilter="True" Visible="true">
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
                    <div class="row">
                        <div class="col-md-6 pull-left">
                            <asp:Button ID="btnSave" runat="server" Text="Save Info" CssClass="btn btn-inverse pull-left" />
                            <div style="float: left; height: 3px; width: 10px;"></div>
                            <asp:Button ID="btnClear" runat="server" Text="Clear Info" CssClass="btn btn-warning pull-left" />
                            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>



            </div>
        </div>
    </div>
</asp:Content>
