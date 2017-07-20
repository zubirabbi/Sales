<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="PaymentInfoList.aspx.cs" Inherits="SUL.SCM.PaymentInfoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <script type="text/javascript">
        function ConfirmAction() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure want to varify this payment?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function CancelAction() {
            var message = document.createElement("INPUT");
            message.type = "hidden";
            message.name = "confirm_value";
            if (confirm("Are you sure want to cancel this payment?")) {
                message.value = "Yes";
            } else {
                message.value = "No";
            }
            document.forms[0].appendChild(message);
        }
    </script>
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Requisition Search </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-9">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">From Date</label>
                                            <div class="col-md-8">
                                                <telerik:RadDatePicker ID="dtfromDate" runat="server" Width="225px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">To Date</label>
                                            <div class="col-md-8">
                                                <telerik:RadDatePicker ID="dttoDate" runat="server" Width="225px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Region</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="rdropRegion" runat="server" CssClass="form-control" Skin="Metro" Width="220px" AutoPostBack="True" OnSelectedIndexChanged="rdropRegion_OnSelectedIndexChanged" Font-Names="" OnDataBound="rdropRegion_OnDataBound">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Area</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="rdropArea" runat="server" CssClass="form-control" Skin="Metro" Width="220px" AutoPostBack="True" Font-Names="" OnDataBound="rdropArea_OnDataBound" OnSelectedIndexChanged="rdropArea_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer</label>
                                            <div class="col-md-8">
                                                <telerik:RadComboBox ID="rdropPDealer" runat="server" Skin="Metro" Width="220px" AutoPostBack="True" Font-Names="" OnDataBound="rdropPDealer_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                                <div class="col-md-3">
                                    <telerik:RadListBox runat="server" ID="lstStatus" CheckBoxes="True" ShowCheckAll="True" Skin="Metro" Width="150px" Height="200px" SelectionMode="Multiple">
                                    </telerik:RadListBox>
                                </div>


                                <div class="clearfix"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 pull-left">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_OnClick" CssClass="btn btn-primary pull-left" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                    <%--<asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left" />--%>
                                </div>
                            </div>
                            <asp:Label runat="server" ID="lblisNewEntry" Visible="False" />
                            <asp:Label runat="server" ID="lblsearchBtn" Visible="False" />
                            <asp:Label runat="server" ID="lblsource" Visible="False" />
                        </div>
                        <div class="widget-head">
                            <h3 class="heading">Payment List</h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="RadGridPayment" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="RadGridPayment_OnPageIndexChanged" OnPageSizeChanged="RadGridPayment_OnPageSizeChanged" Skin="Metro" Width="100%" OnItemCommand="RadGridPayment_OnItemCommand" GroupPanelPosition="Top">
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
                                                    <asp:Label runat="server" ID="lblRowNumber" Text="<%# Container.DataSetIndex + 1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MoneyReceiptNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Money Receipt No" UniqueName="colMoneyReceiptNo" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RequisitionCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Requisition Code" UniqueName="colReqCode" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DealerId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="DealerId" UniqueName="colDealerId" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DealerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer" UniqueName="colDealerName" Display="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Address" UniqueName="colAddress" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="PaymentDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Payment Date" UniqueName="colPaymentDate" DataFormatString="{0:MMM dd, yyyy}" Display="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="PaymentType" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Payment Type" UniqueName="colPaymentType" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="LstPaymentType" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Payment Type" UniqueName="colLstPaymentType">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="Amount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Amount" UniqueName="colAmount" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="LstPaymentMode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Payment Mode" UniqueName="colLstPaymentMode">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridBoundColumn DataField="BankNameId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Bank Name Id" UniqueName="colBankNameId" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="BankName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Bank Name" UniqueName="colBankName" Display="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ReferenceNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Reference No" UniqueName="colReferenceNo">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IsVarified" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Is Verified" UniqueName="colIsVerified" Display="false">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Status" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Status" UniqueName="colStatus">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UpdateBy" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Update By" UniqueName="colUpdateBy" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="CreatedBy" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Created By" UniqueName="colCreatedBy">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" OnClick="btnEdit_OnClick" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="Images/Edit.png" Width="18px" Height="18px" ValidationGroup="button" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Verification" AllowFiltering="false" HeaderStyle-Width="70px" UniqueName="colVerified">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnVerified" OnClick="btnVerified_OnClick" OnClientClick="ConfirmAction()" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl='<%# Eval("varifiedStatus") %>' Width="18px" Height="18px" ValidationGroup="button" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Cancel" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colCancel">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnCancel" OnClick="btnCancel_OnClick" OnClientClick="CancelAction()" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                        <asp:Image ID="Image8" runat="server" ImageUrl="images/cencel.png" Width="18px" Height="18px" ValidationGroup="button" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <%--<telerik:GridButtonColumn
                                                CommandName="btnDelete"
                                                ConfirmText="Are you sure you want to delete this record?"
                                                ConfirmDialogType="RadWindow"
                                                HeaderText="Delete"
                                                ConfirmTitle="Delete"
                                                ButtonType="ImageButton"
                                                ImageUrl="Images/delete.png"
                                                UniqueName="colDelete">
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
                            </div>

                            <div class="row">
                                <div class="col-md-6 pull-left">
                                    <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" OnClick="btnExcel_OnClick" CssClass="btn btn-primary pull-left" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_OnClick" CssClass="btn btn-warning pull-left" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
