<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="CampaignRequisition.aspx.cs" Inherits="SUL.SCM.CampaignRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .incpayment {
            margin-left: 20px;
        }

        .RadComboBox_Metro table td.rcbInputCellLeft {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">


        function validationForQuantity() {
            var Quantity = document.getElementById('<%=rtxtQuantity.ClientID%>').value;

            var Check = /^[1-9]\d*(\.\d+)?$/;
            var matchQ = Quantity.match(Check);
            if (matchQ == null) {
                alert("Please enter a valid quantity");
                document.getElementById("<%=rtxtQuantity.ClientID%>").focus();
                return false;
            } else
                return true;
        }


        function ConfirmAction() {
            var confirmValue = document.createElement("INPUT");
            confirmValue.type = "hidden";
            confirmValue.name = "confirm_value";
            if (confirm("Do you want to change the approval status?")) {
                confirmValue.value = "Yes";
            } else {
                confirmValue.value = "No";
            }
            document.forms[0].appendChild(confirmValue);
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
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Remarks</label>
                                        <div class="col-md-8">
                                            <textarea class="form-control" runat="server" cols="2" id="rtxtRemarks" rows="3" style="width: 220px"></textarea>
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
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Campaign Code</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropCode" AutoPostBack="True" runat="server" Skin="Metro" OnSelectedIndexChanged="rdropCode_OnSelectedIndexChanged" OnDataBound="rdropProduct_OnDataBound" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Campaign Slab</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropName" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Quantity</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtQuantity" CssClass="form-control" onkeyup="javascript:validationForQuantity();" Skin="Metro" Width="220px" onblur="javascript:Calculation();" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                                        <div class="col-md-8">
                                            <asp:Button ID="btnAddRequisitionDetails" runat="server" OnClick="btnAddRequisitionDetails_OnClick" Text="Add Requisition Details" CssClass="btn btn-inverse pull-right" />

                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                            </div>
                        </div>

                        <div class="widget-body" id="divReqDetails">
                            <telerik:RadGrid ID="RadGridAddRequisitionDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnItemCommand="RadGridAddRequisitionDetails_OnItemCommand" Skin="Metro" Width="100%" Height="200px" GridLines="None">
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

                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Item Total</label>
                                <div class="col-md-8">
                                    <telerik:RadTextBox ID="rtxtItemTotal" CssClass="form-control" Width="220px" Enabled="False" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Discount</label>
                                <div class="col-md-8">
                                    <telerik:RadTextBox ID="rtxtReqDiscount" CssClass="form-control" Width="220px" Enabled="True" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro" AutoPostBack="True" OnTextChanged="rtxtReqDiscount_OnTextChanged"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Requisition Total</label>
                                <div class="col-md-8">
                                    <telerik:RadTextBox ID="rtxtReqTotal" CssClass="form-control" Width="220px" Enabled="False" runat="server" Height="35px" ViewStateMode="Enabled" Skin="Metro"></telerik:RadTextBox>
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
                                            <telerik:RadComboBox ID="rdropPaymentType" AutoPostBack="True" OnSelectedIndexChanged="rdropPaymentType_OnSelectedIndexChanged" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
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
                </div>
                <br />

            </div>

        </div>

    </div>

</asp:Content>
