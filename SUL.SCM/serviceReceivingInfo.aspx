<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="mainProduct.aspx.cs" Inherits="SUL.SCM.MainProductInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <style type="text/css">
        #test {
            float: left;
            border: 2px solid black;
            margin-left: -92px;
        }
    </style>
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Service Receiving </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Service Id</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtServiceId" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Receiving Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="rdtReceivingDate" runat="server" Width="280px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Delivery Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="rdtDeliveryDate" runat="server" Width="225px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                            </div>
                        </div>

                        <div class="widget-head">
                            <h3 class="heading">Customer Details </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Customer Type</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropCustomerType" runat="server" Skin="Metro" Width="220px" Font-Names="" AutoPostBack="True" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Name</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rdtxtName" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Contact No</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rdtxtConatctNo" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Address</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rdtxtAddress" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Email</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rdtxtEmail" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Remarks</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rdtxtRemarks" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="widget-head">
                            <h3 class="heading">Product Information </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropProduct" runat="server" Skin="Metro" Width="220px" Font-Names="" AutoPostBack="True" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">IMEI No</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtImeiNo" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Parchase Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="rdtParchaseDate" runat="server" Width="225px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Problem Description</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtProblemDescription" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Failure List</label>
                                        <div class="col-md-8">
                                            <telerik:RadListBox ID="lstFailureList" runat="server" CheckBoxes="True" ShowCheckAll="True"
                                                Skin="Metro" Width="250px" Height="150px" SelectionMode="Multiple">
                                                <Items>
                                                    <telerik:RadListBoxItem Text="Sound Problem" />
                                                    <telerik:RadListBoxItem Text="Touch Problem" />
                                                    <telerik:RadListBoxItem Text="Battery Problem" />
                                                    <telerik:RadListBoxItem Text="Camera Problem" />
                                                </Items>
                                            </telerik:RadListBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Received Items</label>
                                        <div class="col-md-8">
                                            <telerik:RadListBox ID="lstReceivedItems" runat="server" CheckBoxes="True" ShowCheckAll="True"
                                                Skin="Metro" Width="250px" Height="150px" SelectionMode="Multiple">
                                                <Items>
                                                    <telerik:RadListBoxItem Text="Battery" />
                                                    <telerik:RadListBoxItem Text="Cover" />
                                                    <telerik:RadListBoxItem Text="Ear Phone" />
                                                    <telerik:RadListBoxItem Text="Charger" />
                                                </Items>
                                            </telerik:RadListBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                                        <div class="col-md-8">
                                            <asp:CheckBox ID="chkRepeat" runat="server" Checked="true" Text="Repeat" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Warrenty Type</label>
                                            <div class="col-md-8">
                                            </div>
                                        </div>
                                    </div>
                                    <div id="test" class="col-md-6">
                                        <div class="col-md-12">
                                            <asp:RadioButton ID="rdbWarrentyDetails" Text="Warrenty Deatils" SkinID="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></asp:RadioButton>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:RadioButton ID="rdbWarrenty" Text="Warrenty" SkinID="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></asp:RadioButton>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:RadioButton ID="rdbDOA" Text="DOA" SkinID="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></asp:RadioButton>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                            </div>
                        </div>



                        <div class="row">
                            <div class="col-md-6 pull-left">
                                <asp:Button ID="btnSave" runat="server" Text="Save Info" CssClass="btn btn-inverse pull-left" />
                                <div style="float: left; height: 3px; width: 10px;"></div>
                                <asp:Button ID="btnClear" runat="server" Text="Clear Info" CssClass="btn btn-warning pull-left" />
                                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                            </div>
                        </div>

                    </div>
                </div>
            </div>



        </div>
    </div>

</asp:Content>
