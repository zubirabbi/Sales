<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SpairPartsDelivery.aspx.cs" Inherits="SUL.SCM.SpairPartsDelivery" %>

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
    <script type="text/javascript">
        function validationForQuantity() {
            var Quentity = document.getElementById('<%=rtxtQuentity.ClientID%>').value;
            var QuentityP = document.getElementById('<%=rtxtpQuentityP.ClientID%>').value;
            var Check = /^[1-9]\d*(\.\d+)?$/;
            var matchQ = Quentity.match(Check);
            var matchQP = QuentityP.match(Check);
            if (matchQ == null) {
                alert("Please enter a valid quantity");
                document.getElementById("<%=rtxtQuentity.ClientID%>").focus();
                return false;
            }
            if (matchQP == null) {
                alert("Please enter a valid quantity");
                document.getElementById("<%=rtxtpQuentityP.ClientID%>").focus();
                return false;
            }
        }
        function Calculation() {
            var a = parseFloat(document.getElementById("<%= rtxtQuentity.ClientID %>").value);
            var b = parseFloat(document.getElementById("<%= rtxtRate.ClientID %>").value);

            document.getElementById("<%= rtxtTotalPrice.ClientID %>").value = a * b;

        }
    </script>

    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Spair Parts Information </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Service Center</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropServiceCenter" runat="server" Skin="Metro" Width="220px" AutoPostBack="True" Font-Names="" AllowCustomText="True" OnDataBound="rdropServiceCenter_OnDataBound" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Create Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="rdtCreateDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Delivery Method</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropDeliveryMethod" runat="server" Skin="Metro" Width="220px" OnDataBound="rdropDeliveryMethod_OnDataBound" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Delivery Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="rdtDeliveryDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>

                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Spair Parts Details Information </h3>
                        </div>
                        <div class="widget-body" id="detailsDiv">
                            <div class="row">
                                <asp:Panel runat="server" ID="showPanal" Visible="False">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Main Product</label>
                                                <div class="col-md-8">
                                                    <telerik:RadComboBox ID="rdropProduct" runat="server" Skin="Metro" Width="220px" Font-Names="" AutoPostBack="True" OnSelectedIndexChanged="rdropProduct_OnSelectedIndexChanged" OnDataBound="rdropProduct_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Quantity</label>
                                                <div class="col-md-8">
                                                    <telerik:RadTextBox ID="rtxtpQuentityP" CssClass="form-control" onkeyup="javascript:validationForQuantity();" Skin="Metro" Width="220px" onblur="javascript:Calculation();" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                                    &nbsp;&nbsp;
                                            <asp:Button ID="btnNormal" runat="server" Text="Back Normal" OnClick="btnNormal_OnClick" CssClass="btn btn-inverse pull-right" Visible="True" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="ShowNormal" Visible="True">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Spair Parts</label>
                                            <div class="col-md-8">
                                                <telerik:RadComboBox ID="rdropSpairParts" AutoPostBack="True" runat="server" Skin="Metro" OnSelectedIndexChanged="rdropSpairParts_OnSelectedIndexChanged" OnDataBound="rdropSpairParts_OnDataBound" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Unit</label>
                                            <div class="col-md-8">
                                                <telerik:RadComboBox ID="rdropProductUnit" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                                </telerik:RadComboBox>
                                                &nbsp;&nbsp;
                                            <asp:Button ID="btnShowPanal" runat="server" Text="Advance Search" OnClick="btnShowPanal_OnClick" CssClass="btn btn-inverse pull-right" Visible="True" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Quantity</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtQuentity" CssClass="form-control" onkeyup="javascript:validationForQuantity();" Skin="Metro" Width="220px" onblur="javascript:Calculation();" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
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
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Rate</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtRate" Enabled="False" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Total Price</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtTotalPrice" Skin="Metro" Width="220px" runat="server" Height="35px" EnableViewState="true" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="clearfix"></div>
                                <div class="col-md-1"></div>
                                <div class="col-md-11">
                                    <div class="form-group">
                                        <label class="=col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                                        <div class="col-md-8">
                                            <asp:Button ID="btnAddRequisitionDetails" runat="server" OnClick="btnAddRequisitionDetails_OnClick" Text="Add Spair Parts Details" CssClass="btn btn-inverse pull-right" />
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                        <asp:Label ID="lblTransactionCode" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblDP2" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblnewEntry" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblAdvenceSearch" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblNormal" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblselect" runat="server" Visible="False"></asp:Label>
                        <div class="widget-body" id="divReqDetails">
                            <telerik:RadGrid ID="RadGridAddSPDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnItemCommand="RadGridAddSPDetails_OnItemCommand" Skin="Metro" Width="100%" Height="200px" GridLines="None">
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
                                        <telerik:GridBoundColumn DataField="SpairPartsId" FilterControlAltText="Filter column column" HeaderText="SpairPartsId" UniqueName="colSpairPartsId" Display="false">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SpairParts" FilterControlAltText="Filter column column" HeaderText="Product Category" UniqueName="colProductCategory" Display="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn DataField="Rate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Rate" UniqueName="colRate" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="Unit" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Unit" UniqueName="colUnit" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UnitName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Unit" UniqueName="colUnitName" Display="True">
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
                                        <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Quantity" UniqueName="colQuantity" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="TotalRate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Sub Total" UniqueName="colTotalProductPrice">
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
                    <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-6 pull-left">
                        <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left" />
                        <div style="float: left; height: 3px; width: 10px;"></div>
                        <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left" />
                           <asp:Label runat="server" Visible="False" ID="lblsource"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
