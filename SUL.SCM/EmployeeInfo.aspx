<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="EmployeeInfo.aspx.cs" Inherits="SUL.SCM.EmployeeInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
<style>
    .RadComboBox_Metro table td.rcbInputCellLeft { height: 30px; }
</style>
<div id="content">
<div class="innerAll">
<div class="row">
<div class="widget widget-inverse">
    <div class="widget-head">
        <h3 class="heading">Personal Information </h3>
    </div>
    <div class="widget-body">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Employee Name</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtEmployeeName" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="30px"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Employee Code</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtEmpCode" CssClass="form-control" Skin="Metro" Width="303px" Enabled="True" runat="server" Height="30px"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>

            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Id Type</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropIdType" runat="server" Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Id No</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtIdNo" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="30px"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>

            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Date Of Birth</label>
                    <div class="col-md-8">
                        <telerik:RadDatePicker ID="rdtDOB" runat="server" Width="280px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Gender</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropGender" runat="server" Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>

            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Blood Group</label>
                    <div class="col-md-8">
                        <asp:DropDownList ID="rdropBloodGroup" runat="server" CssClass="form-control" Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                            <asp:ListItem Value="SelectOne">---Select One---</asp:ListItem>
                            <asp:ListItem>A+</asp:ListItem>
                            <asp:ListItem>A-</asp:ListItem>
                            <asp:ListItem>AB+</asp:ListItem>
                            <asp:ListItem>AB-</asp:ListItem>
                            <asp:ListItem>B+</asp:ListItem>
                            <asp:ListItem>B-</asp:ListItem>
                            <asp:ListItem>O+</asp:ListItem>
                            <asp:ListItem>O-</asp:ListItem>

                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>

        </div>
    </div>
</div>

<div class="widget widget-inverse">
    <div class="widget-head">
        <h3 class="heading">Official Information </h3>
    </div>
    <div class="widget-body">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Employee Join Date</label>
                    <div class="col-md-8">
                        <telerik:RadDatePicker ID="rdtJoinDate" runat="server" Width="280px" Height="30px"></telerik:RadDatePicker>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Department</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropDept_OnSelectedIndexChanged"  Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>

                </div>

            </div>
            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Designation</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropDesignation" runat="server" Skin="Metro" OnDataBound="rdropDesignation_OnDataBound" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Supervisor</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropSupervisor" runat="server" AutoPostBack="true" OnDataBound="rdropSupervisor_OnDataBound" Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Email</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="RtxtEmail" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="30px"></telerik:RadTextBox>
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
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Employee Resignation Date</label>
                    <div class="col-md-8">
                        <telerik:RadDatePicker ID="rdtResignation" runat="server" Width="280px" Height="30px"></telerik:RadDatePicker>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="widget widget-inverse">
    <div class="widget-head">
        <h3 class="heading">Contact Information </h3>
    </div>
    <div class="widget-body">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Address</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtAddress" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="30px"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Nationality</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropNationality" runat="server" Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Phone</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtPhone" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="30px"></telerik:RadTextBox>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Mobile</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtMobile" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="30px"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Postal Code</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtPostalCode" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="30px"></telerik:RadTextBox>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Country</label>
                    <div class="col-md-8">
                        <telerik:RadComboBox ID="rdropCountry" runat="server" Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Photo</label>
                    <div class="col-md-8">
                        <asp:FileUpload ID="EmpPhoto" runat="server"/>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                    <div class="col-md-8">
                        <asp:Image ID="Image1" runat="server" Height="140px" Width="155px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" AlternateText="Employee Image" ImageAlign="Middle"/>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>

    </div>

</div>
</div>

<div class="row">
    <div class="col-md-6 pull-left">
        <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left"/>
        <div style="float: left; height: 3px; width: 10px;"></div>
        <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left"/>
        <asp:Label runat="server" ID="lblImageName" Visible="False"></asp:Label>
        <asp:Label runat="server" ID="lblId" Visible="False"></asp:Label>
        <asp:Label runat="server" ID="lblisNewEntry" Visible="False"></asp:Label>
    </div>

</div>
</div>


</div>


</asp:Content>