<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="PaymentInfo.aspx.cs" Inherits="SUL.SCM.PaymentInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <style>
        .RadComboBox_Metro table td.rcbInputCellLeft {
            height: 30px;
        }
    </style>
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Payment Information </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Money Receipt No</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtMoneyReceiptNo" Enabled="False" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            <asp:Label runat="server" ID="lblStatus" Visible="False"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Paynemt Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="rdtpaymentDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropPDealer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropPDealer_OnSelectedIndexChanged" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
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
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Payment Type</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropPaymentType" runat="server" Skin="Metro" Width="220px" AutoPostBack="True" OnSelectedIndexChanged="rdropPaymentType_OnSelectedIndexChanged" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
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
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Payment Mode</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropPaymentmode" runat="server" Skin="Metro" AutoPostBack="True" OnSelectedIndexChanged="rdropPaymentmode_OnSelectedIndexChanged" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Bank Name</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropBankName" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" OnDataBound="rdropBankName_OnDataBound" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Reference No</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtRefNo" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
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
                    <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left" />
                    <div style="float: left; height: 3px; width: 10px;"></div>
                    <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left" />
                </div>
                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblRequisitionId" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>

            </div>

        </div>
    </div>
    </div>
</asp:Content>
