<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ServiceCreationInfo.aspx.cs" Inherits="SUL.SCM.serviceCreationInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Service Creation </h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Service Name</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtServiceName" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Description</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtDesceiption" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Non-Warrenty Cost</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtNonWarrentyCost" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                       <%-- <telerik:RadComboBox ID="rdropInCharge" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>--%>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Warrenty Cost</label>
                                    <div class="col-md-8">
                                      <telerik:RadTextBox ID="rtxtWarrentyCost" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox> 
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                                    <div class="col-md-8" style="margin-top: 5px">
                                        <asp:CheckBox runat="server" ID="chkIsSparePartsRequired" Text="Is Spare Parts Required" Checked="True"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Service Time</label>
                                    <div class="col-md-8">
                                      <telerik:RadDatePicker ID="rdtServiceTime" runat="server" Width="225px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>

                            <div class="clearfix"></div>

                             <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Service Level</label>
                                    <div class="col-md-8">
                                        
                                       <telerik:RadComboBox ID="rdropServiceLevel" runat="server" OnDataBound="rdropServiceLevel_DataBound" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>

                            <div class="clearfix"></div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 pull-left">
                            <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left"/>
                            <div style="float: left; height: 3px; width: 10px;"></div>
                            <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_Click" CssClass="btn btn-warning pull-left"/>
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
