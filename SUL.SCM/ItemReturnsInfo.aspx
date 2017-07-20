<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ItemReturnsInfo.aspx.cs" Inherits="SUL.SCM.ItemReturnsInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .incpayment {
            margin-left: 20px;
        }

        .RadComboBox_Metro table td.rcbInputCellLeft {
            height: 30px;
        }
    </style>
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Item Returns Information </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <asp:Panel runat="server" ID="showPanal" Visible="False">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Region</label>
                                                <div class="col-md-8">
                                                    <telerik:RadComboBox ID="rdropRegion" runat="server" Skin="Metro" Width="220px" Font-Names="" AutoPostBack="True" OnSelectedIndexChanged="rdropRegion_OnSelectedIndexChanged" OnDataBound="rdropRegion_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Area</label>
                                                <div class="col-md-8">

                                                    <telerik:RadComboBox ID="rdropArea" runat="server" Skin="Metro" Width="220px" OnSelectedIndexChanged="rdropArea_OnSelectedIndexChanged" AutoPostBack="True" Font-Names="" OnDataBound="rdropArea_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </asp:Panel>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropPDealer" runat="server" Skin="Metro" Width="220px" AutoPostBack="True" Font-Names="" OnDataBound="rdropPDealer_OnDataBound" OnSelectedIndexChanged="rdropPDealer_OnSelectedIndexChanged" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnShowPanal" runat="server" Text="Advance Search" OnClick="btnShowPanal_OnClick" CssClass="btn btn-inverse pull-right" />
                                            <%--<a href="javascript:void(0);" id="btnPopup" onclick="AddPopUpSearch()" class="btn btn-inverse pull-right">Search</a>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Return Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="rdtReturnsDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Remarks</label>
                                        <div class="col-md-8">
                                            <textarea class="form-control" runat="server" id="rtxtRemarks" rows="3" style="width: 220px"></textarea>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Charges</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtCharges" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Item Return Details Information </h3>
                        </div>
                        <div class="widget-body" id="detailsDiv">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="inputTitle">Product</label><br />
                                    <telerik:RadComboBox ID="rdropProduct" runat="server" Skin="Metro" Width="130px" OnSelectedIndexChanged="rdropProduct_OnSelectedIndexChanged" OnDataBound="rdropProduct_OnDataBound" AutoPostBack="True" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                </div>
                                 <div class="col-md-2">
                                    <label for="inputTitle">Color</label><br />
                                    <telerik:RadComboBox ID="rdropColor" runat="server" Skin="Metro" Width="130px" OnSelectedIndexChanged="rdropColor_OnSelectedIndexChanged" OnDataBound="rdropColor_OnDataBound" AutoPostBack="True" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label for="inputTitle">Return Quantity</label><br />
                                        <telerik:RadTextBox ID="rtxtReturnQuentity" CssClass="form-control col-md-6" Skin="Metro" Width="130px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <label for="inputTitle">Return Rate</label><br />
                                    <telerik:RadTextBox ID="rtxtReturnRate" Skin="Metro" Width="130px" CssClass="form-control col-md-6" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="inputTitle"></label>
                                    <br />
                                    <asp:Button ID="btnAddReturnDetails" runat="server" OnClick="btnAddReturnDetails_OnClick" Text="Add Return Details" CssClass="btn btn-inverse" />
                                </div>
                                <asp:Label runat="server" ID="lblsource" Visible="False" />
                            </div>



                            <div class="widget-body" id="divReqDetails">
                                <telerik:RadGrid ID="RadGridAddItemReturnsDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnItemCommand="RadGridAddItemReturnsDetails_OnItemCommand" Skin="Metro" Width="100%" Height="200px" GridLines="None">
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
                                            <telerik:GridBoundColumn DataField="ProductId" FilterControlAltText="Filter column column" HeaderText="ProductId" UniqueName="colProductId" Display="false">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter column column" HeaderText="Product Name" UniqueName="colProductName" Display="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ColorId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="ColorId" UniqueName="colColorId" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UnitId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="UnitId" UniqueName="colUnitId" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Color" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Color" UniqueName="colColor" Display="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ReturnQuentity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Return Quentity" UniqueName="colReturnQuentity" Display="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ReturnRate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Return Rate" UniqueName="colReturnRate" Display="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="LineTotal" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Line Total" UniqueName="colLineTotal" Display="True">
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
                                            <telerik:GridButtonColumn
                                                CommandName="btnDelete"
                                                HeaderText="Delete"
                                                SortExpression=""
                                                ConfirmText="Are you sure you want to delete this record?"
                                                ConfirmDialogType="RadWindow"
                                                ConfirmTitle="Delete"
                                                ButtonType="ImageButton"
                                                ImageUrl="Images/Delete.png"
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
                                <asp:Label ID="lblDetails" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblPaymentId" runat="server" Visible="False"></asp:Label>
                                    
                                     <asp:Label ID="lblColorId" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblColor" runat="server" Visible="False"></asp:Label>
                                 <asp:Label ID="lblUnitId" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblUnit" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblQuantity" runat="server" Visible="False"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 pull-right">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Total Price</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtItemTotal" CssClass="form-control" Width="220px" Enabled="False" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 pull-left">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left" />
                            <div style="float: left; height: 3px; width: 10px;"></div>
                            <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left" />
                            <div style="float: left; height: 3px; width: 10px;"></div>
                        </div>
                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblProductId" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblDP2" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblnewEntry" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblReturnCode" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblinvoiceCreate" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblpaymentStatus" runat="server" Visible="False"></asp:Label>
                    </div>
                    <br />

                </div>
            </div>

        </div>
    </div>

</asp:Content>
