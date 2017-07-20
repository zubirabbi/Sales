<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="ProformaInvoiceEntryInfo.aspx.cs" Inherits="SUL.SCM.ProformaInvoiceEntryInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    function Calculation() {
        var a = parseFloat(document.getElementById("<%= rtxtPOQuantity.ClientID %>").value);
        var b = parseFloat(document.getElementById("<%= rtxtPOunitPrice.ClientID %>").value);

        document.getElementById("<%= rtxtTotalUnitPrice.ClientID %>").value = a * b;

    }
</script>
<telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
<style>
    .RadComboBox_Metro table td.rcbInputCellLeft { height: 30px; }
</style>
<div id="content">
<div class="innerAll">
<div class="row">
<div class="widget widget-inverse">
<div class="widget-head">
    <h3 class="heading">Proforma Invoice</h3>
</div>
<div class="widget-body">
    <div class="row">
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
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">P.I. Number</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtPINumber" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Order Number</label>
                <div class="col-md-8">
                    <telerik:RadComboBox ID="rdropPONumber" runat="server" Skin="Metro" AutoPostBack="True" OnSelectedIndexChanged="rdropPONumber_OnSelectedIndexChanged" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                    </telerik:RadComboBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">P.I. Date</label>
                <div class="col-md-8">
                    <telerik:RadDatePicker ID="rdtPIDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Vendor Address</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtVendorAddress" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">P.I. Document Name</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtfileName" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Scanned P.I. Document</label>
                <div class="col-md-8">
                    <asp:FileUpload ID="PIFileUp" runat="server"/>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>

        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Vendor Bank</label>
                <div class="col-md-8">
                    <telerik:RadComboBox ID="rdropbankInfo" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                    </telerik:RadComboBox>
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
            <div class="col-md-11" style="padding-right: 27px">
                <asp:Button ID="rbtnAddNew" runat="server" OnClick="rbtnAddNew_OnClick" Text="Add New Item" CssClass="btn btn-inverse pull-left"/>
                <div style="float: left; height: 3px; width: 10px;"></div>
            </div>
        </div>
        <asp:Panel ID="showPI" runat="server" Visible="False">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Category</label>
                        <div class="col-md-8">
                            <telerik:RadComboBox ID="rdropPOCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropPOCategory_OnSelectedIndexChanged" Width="220px" Font-Names="" Skin="Metro" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                            </telerik:RadComboBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product</label>
                        <div class="col-md-8">
                            <telerik:RadComboBox ID="rdropPOProduct" runat="server" Width="220px" Font-Names="" AutoPostBack="True" OnSelectedIndexChanged="rdropPOProduct_OnSelectedIndexChanged" Skin="Metro" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                            </telerik:RadComboBox>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product Unit</label>
                        <div class="col-md-8">
                            <telerik:RadComboBox ID="rdropProductUnit" runat="server" Width="220px" Font-Names="" AutoPostBack="True" OnSelectedIndexChanged="rdropProductUnit_OnSelectedIndexChanged" Skin="Metro" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                            </telerik:RadComboBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Quantity</label>
                        <div class="col-md-8">
                            <telerik:RadTextBox ID="rtxtPOQuantity" CssClass="form-control" Width="220px" onblur="javascript:Calculation();" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Unit Price</label>
                        <div class="col-md-8">
                            <telerik:RadTextBox ID="rtxtPOunitPrice" CssClass="form-control" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Total Unit Price</label>
                        <div class="col-md-8">
                            <telerik:RadTextBox ID="rtxtTotalUnitPrice" Enabled="False" CssClass="form-control" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                        </div>
                    </div>
                </div>

                <div class="clearfix"></div>
                <br/>
            </div>
            <div class="row">
                <div class="col-md-11" style="padding-right: 27px">
                    <asp:Button ID="btnAddProductInPO" runat="server" OnClick="btnAddProductInPO_OnClick" Text="Add Product In P.I" CssClass="btn btn-inverse pull-right"/>
                    <div style="float: left; height: 3px; width: 10px;"></div>
                    <%-- <asp:Button ID="Button2" runat="server" Text="Clear Info" CssClass="btn btn-warning pull-left" />--%>
                </div>
            </div>
        </asp:Panel>
        <div class="row">
            <telerik:RadGrid ID="RadGridAddProforma" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnItemCommand="RadGridAddProforma_OnItemCommand" Skin="Metro" Width="100%" GridLines="None">
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
                        <telerik:GridBoundColumn DataField="OrderDetailsId" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colOrderDetailsId" Display="false">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PICategory" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.I. Category" UniqueName="colPICategory" Visible="true">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PICategory" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.I. Category" UniqueName="colPICategory" Visible="true">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PIProduct" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.I. Product" UniqueName="colPIProduct" Display="False">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PIProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.I. Product Name" UniqueName="colPIProductName" Visible="true">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PIQuantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.I. Quantity" UniqueName="colPIQuantity">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="POQuantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.O. Quantity" UniqueName="colPOQuantity">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PIUnitePrice" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.I. Unit Price" UniqueName="colPIUnitPrice">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PIUnitId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.I. Unite Id" UniqueName="colPIUnitId" Display="False">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PIUniteName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.I. Unit Name" UniqueName="colPIUnitName">
                            <ColumnValidationSettings>
                                <ModelErrorMessage Text=""></ModelErrorMessage>
                            </ColumnValidationSettings>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TotalProductPIPrice" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total Product Price" UniqueName="colTotalProductPIPrice">
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
        <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left"/>
    </div>
    <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblPIId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblDetailsId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSupId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblPIQuantity" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblImageName" runat="server" Visible="False"></asp:Label>
    
    <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblisNewDetailEntry" runat="server" Visible="False"></asp:Label>
</div>
</div>
</div>
<br/>
</div>
</div>
</asp:Content>