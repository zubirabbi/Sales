<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ServiceCenterInfo.aspx.cs" Inherits="SUL.SCM.ServiceCenterInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Service Centre</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Centre Code</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtCentreCode" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Area</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropArea" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>

                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Name</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtName" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>

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
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">In Charge</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropInCharge" runat="server" OnDataBound="rdropInCharge_DataBound" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>

                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Establish Date</label>
                                    <div class="col-md-8">
                                        <telerik:RadDatePicker ID="rdtEstablishDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Is Active</label>
                                    <div class="col-md-8" style="margin-top: 5px">
                                        <asp:CheckBox runat="server" ID="chkIsActive" Checked="True" />
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                        </div>

                        <div class="row">
                            <div class="col-md-6 pull-left">
                                <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left" />
                                <div style="float: left; height: 3px; width: 10px;"></div>
                                <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_Click" CssClass="btn btn-warning pull-left" />
                                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                                   <asp:Label runat="server" Visible="False" ID="lblsource"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
