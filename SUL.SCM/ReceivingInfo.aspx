<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="ReceivingInfo.aspx.cs" Inherits="SUL.SCM.ReceiveingInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
<style>
    .RadComboBox_Metro table td.rcbInputCellLeft { height: 30px; }
</style>
<div id="content">
<div class="innerAll">
<div class="row">
<div class="widget widget-inverse">
<div class="widget-head">
    <h3 class="heading">Product Receiving</h3>
</div>
<div class="widget-body">
    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Receiving Code</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtReceiveCode" CssClass="form-control" Enabled="False" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Receiving Date</label>
                <div class="col-md-8">
                    <telerik:RadDatePicker ID="rdtPIDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>

        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Vendor Name</label>
                <div class="col-md-8">
                    <telerik:RadComboBox ID="rdropVandorName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropVandorName_OnSelectedIndexChanged" Skin="Metro" Width="220px" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                    </telerik:RadComboBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">L.C. Number</label>
                <div class="col-md-8">
                    <telerik:RadComboBox ID="rdropLCNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropLCNo_OnSelectedIndexChanged" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                    </telerik:RadComboBox>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>

        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">P.I Number</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtPINumber" Enabled="False" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">WareHouse</label>
                <div class="col-md-8">
                    <telerik:RadComboBox ID="rdropWareHouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropWareHouse_OnSelectedIndexChanged" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                    </telerik:RadComboBox>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>

        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">IMEI File Name</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtIMEIFileName" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Upload IMEI</label>
                <div class="col-md-8">
                    <asp:FileUpload ID="IMEIFileUpload" runat="server"/>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>

        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Received By</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtReceivedBy" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Invoice No</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtInvoiceNo" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="widget widget-inverse">
    <div class="widget-head">
        <h3 class="heading">Proforma Invoice Items </h3>
    </div>
    <div class="widget-body">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product Code</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropProductCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropProductCode_OnSelectedIndexChanged" OnDataBound="rdropProductCode_OnDataBound" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product Name</label>
                    <div class="col-md-8">
                        <asp:Label ID="lblProductName" runat="server" Visible="True"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Receive Quantity</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtReceiveQuantity" CssClass="form-control" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Color</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropColor" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <br/>


            <asp:Label ID="lblProductId" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblRemaining" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblLCQuantity" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblReceivedQuantity" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblUnite" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblDetailsId" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblAmount" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lbl" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblProductCode" runat="server" Visible="False"></asp:Label>

        </div>
        <div class="row">
            <div class="col-md-11" style="padding-right: 27px">
                <asp:Button ID="btnAddReceiveProduct" runat="server" OnClick="btnAddReceiveProduct_OnClick" Text="Add Receive Product" CssClass="btn btn-inverse pull-right"/>
                <div style="float: left; height: 3px; width: 10px;"></div>
                <%-- <asp:Button ID="Button2" runat="server" Text="Clear Info" CssClass="btn btn-warning pull-left" />--%>
            </div>
        </div>

        <div class="row">
            <telerik:RadGrid ID="RadGridAddReceivedDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnItemCommand="RadGridAddReceivedDetails_OnItemCommand" Skin="Metro" Width="100%" GridLines="None">
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
                        <telerik:GridBoundColumn DataField="ProductCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Code" UniqueName="colProductCode" Visible="true">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PIquantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="L.C. Quantity" UniqueName="colPIquantity">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="receivedQuantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Received Quantity" UniqueName="colreceivedQuantity" Visible="true">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="color" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="color" UniqueName="colcolor">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ReceiveQuantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Receive Quantity" UniqueName="colReceiveQuantity" Visible="true">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ProductUnit" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Unit" UniqueName="colUnit">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>

                        <telerik:GridButtonColumn
                            CommandName="btnSelect"
                            HeaderText=""
                            SortExpression=""
                            ButtonType="ImageButton"
                            ImageUrl="Images/Edit.png"
                            UniqueName="colEdit">
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
            <asp:Label ID="lblpriceosProduct" runat="server" Visible="False"></asp:Label>
        </div>

    </div>
    <div class="row">
        <div class="col-md-6 pull-right">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Total Price</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtTotalPrice" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>

        </div>
    </div>
    <br/>
</div>

<div class="row">
    <div class="col-md-6 pull-left">
        <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left"/>
        <div style="float: left; height: 3px; width: 10px;"></div>
        <asp:Button ID="btnClear" runat="server" Text="Clear Info" CssClass="btn btn-warning pull-left"/>
    </div>
    <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblPIId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSupId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblImageName" runat="server" Visible="False"></asp:Label>
</div>
</div>
</div>
<br/>
</div>
</div>
</asp:Content>