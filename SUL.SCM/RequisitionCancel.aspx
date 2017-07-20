<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="RequisitionCancel.aspx.cs" Inherits="SUL.SCM.RequisitionCencel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .incpayment { margin-left: 20px; }

        .RadComboBox_Metro table td.rcbInputCellLeft { height: 30px; }
    </style>
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Requisition Cancellation </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropPDealer" Enabled="False" runat="server" Skin="Metro" Width="220px" AutoPostBack="True" Font-Names="" OnDataBound="rdropPDealer_OnDataBound" OnSelectedIndexChanged="rdropPDealer_OnSelectedIndexChanged" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
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

                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Requisition No</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtRequisitionNo" CssClass="form-control" Enabled="False" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Requisition Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="rdtRequisitionDate" Enabled="False" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Channel Specialized</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropCS" runat="server" Enabled="False" OnDataBound="rdropCS_OnDataBound" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
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
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Requisition Total</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtRequisitionTotal" CssClass="form-control" Enabled="False" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Payment Total</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtPayment" CssClass="form-control" Enabled="False" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Cancel Note</label>
                                        <div class="col-md-8">
                                            <%--<telerik:RadTextBox ID="rtxtCancelNote" CssClass="form-control" Enabled="False" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>--%>
                                            <textarea id="rtxtNote" class="form-control" runat="server" rows="4" cols="2"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 pull-left">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_OnClick" CssClass="btn btn-inverse pull-left"/>
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                    <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblisnewPaymentEntry" runat="server" Visible="False"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>