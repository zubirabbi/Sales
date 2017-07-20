<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="DealerInfo.aspx.cs" Inherits="SUL.SCM.DealerInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <style>
        .RadComboBox_Metro table td.rcbInputCellLeft { height: 30px; }
    </style>
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Dealer Information </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Area</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropArea" runat="server" Skin="Metro" AutoPostBack="True" OnSelectedIndexChanged="rdropArea_OnSelectedIndexChanged" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer Code</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtDealerCode" Skin="Metro" Enabled="False" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer Name</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtDealername" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Proprietor Name</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtProprietorName" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>


                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Channel Specialist</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtCS" CssClass="form-control" Enabled="False" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Jr Channel Specialist</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtJrCS" CssClass="form-control" Enabled="False" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Phone</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtPhone" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Mobile</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtMobile" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Email</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtEmail" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Address</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtAddress" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer Category</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropDealerCate" runat="server" Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Start Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="rdtStartDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                 <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Credit Limit</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtCreditLimit" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Is Active</label>
                                        <div class="col-md-8" style="margin-top: 5px">
                                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="True"/>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 pull-left">
                                <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left"/>
                                <div style="float: left; height: 3px; width: 10px;"></div>
                                <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left"/>
                            </div>
                            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblEmpJrId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblWareHouseId" runat="server" Visible="False"></asp:Label>
                            
                            <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblsource" runat="server" Visible="False"></asp:Label>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>