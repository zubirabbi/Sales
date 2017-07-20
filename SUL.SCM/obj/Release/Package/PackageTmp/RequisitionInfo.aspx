<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="RequisitionInfo.aspx.cs" Inherits="SUL.SCM.RequisitionInfo" %>

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
        function Calculation() {
            var a = parseFloat(document.getElementById("<%= rtxtQuentity.ClientID %>").value);
            var b = parseFloat(document.getElementById("<%= rtxtPrice.ClientID %>").value);

            document.getElementById("<%= rtxtTotalPrice.ClientID %>").value = a * b;
            document.getElementById("<%= rtxtTotalPriceAfterDiscount.ClientID %>").value = a * b;
        }

        function CalculationDiscount() {
            var a = parseFloat(document.getElementById("<%= rtxtTotalPrice.ClientID %>").value);
            var b = parseFloat(document.getElementById("<%= rtxtDiscount.ClientID %>").value);
            var c = parseFloat(document.getElementById("<%= rtxtQuentity.ClientID %>").value);

            document.getElementById("<%= rtxtTotalPriceAfterDiscount.ClientID %>").value = a - (c * b);
        }

        function validationForQuantity() {
            var Quentity = document.getElementById('<%=rtxtQuentity.ClientID%>').value;

            var Check = /^[0-9]\d*(\.\d+)?$/;
            var matchQ = Quentity.match(Check);
            if (matchQ == null) {
                alert("Please enter a valid quantity");
                document.getElementById("<%=rtxtQuentity.ClientID%>").focus();
                return false;
            }
        }

        function validationForDiscount() {

            var discount = document.getElementById('<%=rtxtDiscount.ClientID%>').value;
            var check = /^[0-9]\d*(\.\d+)?$/;
            var matchD = discount.match(check);
            if (matchD == null) {
                alert("Please enter a valid Discount");
                document.getElementById("<%=rtxtDiscount.ClientID%>").focus();
                return false;
            }

        }
        function ConfirmAction() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to change the approval status?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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
                            <h3 class="heading">Requisition Information </h3>
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

                                                    <telerik:RadComboBox ID="rdropArea" runat="server" Skin="Metro" Width="220px" OnSelectedIndexChanged="rdropArea_OnSelectedIndexChanged" AutoPostBack="True" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
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
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Address</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtAddress" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Courier</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropCourier" runat="server" Skin="Metro" Width="220px" OnDataBound="rdropCourier_OnDataBound" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Requisition No</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtRequisitionNo" CssClass="form-control" Enabled="False" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Channel Specialized</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropCS" runat="server" OnDataBound="rdropCS_OnDataBound" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Jr.Channel Specialized</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropJrCS" runat="server" OnDataBound="rdropJrCS_OnDataBound" Skin="Metro" Width="220px" Font-Names="">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Requisition Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="rdtRequisitionDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>

                                        </div>
                                        <br />

                                        <div class="col-md-8 pull-right">

                                            <asp:Label ID="lblCampaignCode" runat="server" Visible="False" Font-Size="Medium" Font-Bold="True" ForeColor="#990000"></asp:Label>

                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Remarks</label>
                                        <div class="col-md-8">
                                            <textarea class="form-control" cols="1" runat="server" id="rtxtRemarks" rows="3" style="width: 220px"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Requisition Details Information </h3>
                        </div>
                        <div class="widget-body" id="detailsDiv">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropProduct" AutoPostBack="True" runat="server" Skin="Metro" OnSelectedIndexChanged="rdropProduct_OnSelectedIndexChanged" OnDataBound="rdropProduct_OnDataBound" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
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
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Price</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtPrice" Enabled="True" Skin="Metro" Width="220px" runat="server" Height="35px" onblur="javascript:Calculation();" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Total Price</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtTotalPrice" Enabled="False" Skin="Metro" Width="220px" runat="server" Height="35px" EnableViewState="true" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Discount</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtDiscount" Skin="Metro" Width="220px" onkeyup="javascript:validationForDiscount();" Text="0" runat="server" onblur="javascript:CalculationDiscount();" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Total Price After Discount</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtTotalPriceAfterDiscount" Enabled="False" Skin="Metro" Width="220px" Text="0" runat="server" Height="35px" EnableViewState="true" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-1"></div>
                                <div class="col-md-11">
                                    <div class="form-group">
                                        <label class="=col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                                        <div class="col-md-8">
                                            <asp:Button ID="btnAddRequisitionDetails" runat="server" OnClick="btnAddRequisitionDetails_OnClick" Text="Add Requisition Details" CssClass="btn btn-inverse pull-right" />
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>

                        <div class="widget-body" id="divReqDetails">
                            <telerik:RadGrid ID="RadGridAddRequisitionDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="False" OnItemCommand="RadGridAddRequisitionDetails_OnItemCommand" Skin="Metro" Width="100%" Height="200px" GridLines="None">
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
                                        <telerik:GridBoundColumn DataField="Price" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Price" UniqueName="colPrice" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Quantity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Quantity" UniqueName="colQuantity" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="TotalProductPrice" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Sub Total" UniqueName="colTotalProductPrice">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="Discount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Discount" UniqueName="colDiscount">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>


                                        <telerik:GridBoundColumn DataField="TotalProductPriceDiscount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Item Total" UniqueName="colTotalProductPriceDiscount">
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
                    <div class="widget widget-inverse">
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="inputTitle">Sub Total</label>
                                    <br />
                                    <telerik:RadTextBox ID="rtxtSubTotal" CssClass="form-control col-md-6" Width="130px" Enabled="False" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="inputTitle">Item Total Discount</label>
                                    <br />
                                    <telerik:RadTextBox ID="rtxtItemDiscount" CssClass="form-control col-md-6" Width="130px" Enabled="False" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="inputTitle">Item Total</label>
                                    <br />
                                    <telerik:RadTextBox ID="rtxtItemTotal" CssClass="form-control col-md-6" Width="130px" Enabled="False" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="inputTitle">Discount</label>
                                    <br />
                                    <telerik:RadTextBox ID="rtxtReqDiscount" CssClass="form-control col-md-6" Width="130px" Enabled="True" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro" AutoPostBack="True" OnTextChanged="rtxtReqDiscount_OnTextChanged"></telerik:RadTextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="inputTitle">Requisition Total</label>
                                    <br />
                                    <telerik:RadTextBox ID="rtxtReqTotal" CssClass="form-control col-md-6" Width="130px" Enabled="False" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Payment Information </h3>
                        </div>
                        <div class="widget-body" id="PayInfo">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Payment Type</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropPaymentType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropPaymentType_OnSelectedIndexChanged" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Reference No</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtReferenceNo" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Payment Mode</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropPaymentmode" runat="server" AutoPostBack="True" Skin="Metro" Width="220px" OnSelectedIndexChanged="rdropPaymentmode_OnSelectedIndexChanged" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Amount</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtAmount" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Bank Name</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropBankName" runat="server" Skin="Metro" OnDataBound="rdropBankName_OnDataBound" Width="220px" Font-Names="" DropDownWidth="300px">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Bank Charge</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtBankCharge" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Branch</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtBranch" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <asp:Panel ID="showDeposit" runat="server" Visible="False">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Cheque Bank</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtChequeBank" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Cheque Branch</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtChequeBranch" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Cheque Date</label>
                                            <div class="col-md-8">
                                                <telerik:RadDatePicker ID="rdtCheckDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 pull-left">
                        <asp:Button ID="btnSave" runat="server" Text="Save & Print" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left" />
                        <div style="float: left; height: 3px; width: 10px;"></div>
                        <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left" />
                        <div style="float: left; height: 3px; width: 10px;"></div>
                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_OnClick" Visible="False" CssClass="btn btn-warning pull-left" />
                    </div>
                    <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblDP2" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblnewEntry" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblnewPaymentEntry" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblinvoiceCreate" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblpaymentStatus" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblCampaignId" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblUpdateBy" runat="server" Visible="False"></asp:Label>
                </div>
                <br />

            </div>

        </div>

    </div>

</asp:Content>
