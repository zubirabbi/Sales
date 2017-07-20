<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="mainProduct.aspx.cs" Inherits="SUL.SCM.MainProductInfo" %>

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
                            <h3 class="heading">Spair Parts Setup </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Main Product</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropMainProduct" runat="server" Skin="Metro" Width="220px" AutoPostBack="True" OnDataBound="rdropMainProduct_OnDataBound" OnSelectedIndexChanged="rdropMainProduct_OnSelectedIndexChanged" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Spair Parts</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropSpairParts" runat="server" Skin="Metro" Width="220px" OnDataBound="rdropSpairParts_OnDataBound" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Quantity</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtQuantity" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                                <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>

                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_OnClick" CssClass="btn btn-inverse " Width="150px" />
                                        <div style="float: left; height: 3px; width: 10px;"></div>
                                    </div>
                                </div>
                                <asp:Label runat="server" Visible="false" ID="lblCount" />
                            </div>
                        </div>

                        <div class="widget-body" id="divReqDetails">
                            <telerik:RadGrid ID="RadGridAddRequisitionDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" Skin="Metro" Width="100%" GridLines="None"
                                OnItemCommand="RadGridAddRequisitionDetails_OnItemCommand" OnPageIndexChanged="RadGridAddRequisitionDetails_OnPageIndexChanged" OnPageSizeChanged="RadGridAddRequisitionDetails_OnPageSizeChanged">
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                </ClientSettings>
                                <MasterTableView>
                                    <Columns>

                                        <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Id" UniqueName="colId" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Product" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product" UniqueName="colProduct" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SpairParts" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Spair Parts" UniqueName="colSpairParts" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Quentity" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Quentity" UniqueName="colQuantity" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ProductId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Spair Parts" UniqueName="colProductId" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SpairPartId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Quantity" UniqueName="colSpairPartId" Display="False">
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
                            <asp:Label runat="server" Visible="False" ID="lblsource"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
