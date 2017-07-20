<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="PurchaseOrderEntryInfo.aspx.cs" Inherits="SUL.SCM.PurchaseOrderEntryInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function Calculation() {
            var a = parseFloat(document.getElementById("<%= rtxtPOQuentity.ClientID %>").value);
        var b = parseFloat(document.getElementById("<%= rtxtPOunitPrice.ClientID %>").value);

        document.getElementById("<%= rtxtTotalUnitePrice.ClientID %>").value = a * b;

    }
    </script>
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <style>
        .RadComboBox_Metro table td.rcbInputCellLeft {
            height: 30px;
        }
    </style>
    <div id="content">

        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Purchase Order</h3>
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
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Order Number</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtProductOrderNumber" Enabled="True" CssClass="form-control" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Vendor Address</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtVendorAddress" CssClass="form-control" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Order Date</label>
                                    <div class="col-md-8">
                                        <telerik:RadDatePicker ID="rdtProductOrderDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                    </div>
                                </div>

                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>

                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Order Items </h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropPOProduct" runat="server" Width="220px" Font-Names="" AutoPostBack="True" OnSelectedIndexChanged="rdropPOProduct_OnSelectedIndexChanged" Skin="Metro" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>

                           <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product Unit</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropProductUnit" runat="server" Width="220px" Font-Names="" AutoPostBack="True" OnSelectedIndexChanged="rdropPOProduct_OnSelectedIndexChanged" Skin="Metro">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="clearfix"></div>
                            
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Quantity</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtPOQuentity" CssClass="form-control" Width="220px" onblur="javascript:Calculation();" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Total Unite Price</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtTotalUnitePrice" Enabled="False" CssClass="form-control" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
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
                                <asp:Button ID="btnAddProductInPO" runat="server" OnClick="btnAddProductInPO_OnClick" Text="Add Product In P.O" CssClass="btn btn-inverse pull-right" />    
                            </div>
                             </div>

                            <div class="clearfix"></div>
                            <br />
                        </div>
                        
                        <div class="row">
                            <div class="col-md-12 pull-left">
                                <telerik:RadGrid ID="RadGridAddProduct" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnItemCommand="RadGridAddProduct_OnItemCommand" Skin="Metro" Width="100%" GridLines="None">
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

                                            <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="POCategory" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.O. Category" UniqueName="colPOCategory" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="POProduct" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.O. Product" UniqueName="colPOProduct" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="POProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.O. Product Name" UniqueName="colPOPOProductName" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="POQuentity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.O. Quentity" UniqueName="colPOQuentity">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="POUnitePrice" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.O. Unite Price" UniqueName="colPOUnitePrice">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="POUniteId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.O. Unite Id" UniqueName="colPOUniteId" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="POUniteName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="P.O. Unite Name" UniqueName="colPOUniteName">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TotalProductPrice" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total Product Price" UniqueName="colTotalProductPrice">
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
                                        <telerik:RadTextBox ID="rtxtTotalPrice" CssClass="form-control" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6 pull-left">
                                <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left" />
                                <div style="float: left; height: 3px; width: 10px;"></div>
                                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_OnClick" Text="Clear Info" CssClass="btn btn-warning pull-left" />
                            </div>
                            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblDetailsId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSupId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblisNewDetailEntry" runat="server" Visible="False"></asp:Label>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
