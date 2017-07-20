﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="DeliveryInformation.aspx.cs" Inherits="SUL.SCM.DeliveryInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .incpayment { margin-left: 20px; }
</style>
<telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
<div id="content">
<div class="wizard">
<!-- // Widget heading END -->
<div class="innerAll">
<div class="row">
<div class="widget widget-inverse">
    <div class="widget-head">
        <h3 class="heading">Delivery Information </h3>
    </div>
    <div class="widget-body">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">WareHouse</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropWareHouse" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Delivery Address</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtDeliveryAddress" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>

            <div class="clearfix"></div>
        </div>
    </div>
</div>
<div class="widget widget-inverse">
    <div class="widget-head">
        <h3 class="heading">Invoice Information </h3>
    </div>
    <div class="widget-body">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Invoice No</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtInvoiceNo" Enabled="False" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropPDealer" Enabled="False" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>


            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Courier</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropCourier" Enabled="False" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Requisition No</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtRequisitionNo" Enabled="False" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Requisition Date</label>
                    <div class="col-md-8">
                        <telerik:RadDatePicker ID="rdtRequisitionDate" Enabled="False" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Channel Specialized</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropCS" Enabled="False" runat="server" CssClass="form-control" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Address</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtAddress" Enabled="False" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>

        </div>
    </div>
</div>
<div class="widget widget-inverse">
    <div class="widget-head">
        <h3 class="heading">Invoice Details Information </h3>
    </div>
    <div class="widget-body">
        <telerik:RadGrid ID="RadGridAddRequisitionDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                         AllowPaging="True" Skin="Metro" Width="100%" GridLines="None" AllowFilteringByColumn="True">
            <ClientSettings AllowColumnsReorder="True">
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
                    <telerik:GridBoundColumn DataField="CategoryId" FilterControlAltText="Filter column column" HeaderText="CategoryId" UniqueName="colCategoryId" Display="false">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProductCategory" FilterControlAltText="Filter column column" HeaderText="Product Category" UniqueName="colProductCategory" Display="False">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProductId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Id" UniqueName="colProductId" Display="False">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Name" UniqueName="colProductName" Visible="true">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Quantity" UniqueName="colQuantity" Visible="true">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Price" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Price" UniqueName="colPrice" Display="True">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Unit" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Unit" UniqueName="colUnit" Display="False">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Discount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Discount" UniqueName="colDiscount">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Color" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Color" UniqueName="colColor" Display="False">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ColorName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Color Name" UniqueName="colname">
                        <ColumnValidationSettings>
                            <ModelErrorMessage Text=""></ModelErrorMessage>
                        </ColumnValidationSettings>
                    </telerik:GridBoundColumn>

                    <%--  <telerik:GridButtonColumn
                                            CommandName="btnSelect"
                                            HeaderText=""
                                            SortExpression=""
                                            ButtonType="ImageButton"
                                            ImageUrl="Images/Edit.png"
                                            UniqueName="colEdit">
                                        </telerik:GridButtonColumn>--%>
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
        <asp:Label ID="lblDetails" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblPaymentId" runat="server" Visible="False"></asp:Label>
    </div>
</div>
</div>
<div class="row">
    <div class="col-md-6 pull-left">
        <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left"/>
        <div style="float: left; height: 3px; width: 10px;"></div>
        <asp:Button ID="btnPrintInvoice" runat="server" Text="Print Invoice"  OnClick="btnPrintInvoice_OnClick" Visible="False" CssClass="btn btn-warning pull-left"/>
        <div style="float: left; height: 3px; width: 10px;"></div>
        <asp:Button ID="btnPrintDelevary" runat="server" Text="Print Delivery" OnClick="btnPrintDelevary_OnClick" Visible="False" CssClass="btn btn-warning pull-left"/>
        <div style="float: left; height: 3px; width: 10px;"></div>
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_OnClick" Visible="False" CssClass="btn btn-warning pull-left"/>
    </div>
    <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblInvoiceId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblProductId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblisnewPaymentEntry" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblisInvoiceCreate" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="Label4" runat="server" Visible="False"></asp:Label>

</div>
<br/>

</div>

</div>
</div>
</asp:Content>