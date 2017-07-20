<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="IncentiveSetupInfo.aspx.cs" Inherits="SUL.SCM.IncentiveSetupInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Incentive Setup </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Type</label>
                                            <div class="col-md-8">
                                                <telerik:RadComboBox ID="rdropType" runat="server" OnDataBound="rdropType_DataBound" Skin="Metro" Width="220px" Font-Names="" AutoPostBack="true" OnSelectedIndexChanged="rdropType_SelectedIndexChanged" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Designation</label>
                                            <div class="col-md-8">
                                                <telerik:RadComboBox ID="rdropSalesPerson" runat="server" Enabled="False" Skin="Metro" Width="220px" AutoPostBack="true" Font-Names="" OnDataBound="rdropSalesPerson_DataBound" OnSelectedIndexChanged="rdropSalesPerson_OnSelectedIndexChanged" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Incentive On</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="rdropInsectiveOn" runat="server" CssClass="form-control" Skin="Metro" AutoPostBack="True" OnSelectedIndexChanged="rdropInsectiveOn_OnSelectedIndexChanged" Width="220px" Font-Names="">
                                                    <asp:ListItem Value="SelectOne">---Select One---</asp:ListItem>
                                                    <asp:ListItem Value="Product">Product</asp:ListItem>
                                                    <asp:ListItem Value="Value">Value</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <asp:Panel runat="server" ID="showApplyOn" Visible="False">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product Name</label>
                                                <div class="col-md-8">
                                                    <telerik:RadComboBox ID="rdropProductName" runat="server" Skin="Metro" Width="220px" Font-Names="" OnDataBound="rdropProductName_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Apply On</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="rdropApplyOn" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="rdropApplyOn_OnSelectedIndexChanged" Skin="Metro" Width="220px" Font-Names="">
                                                        <asp:ListItem Value="SelectOne">---Select One---</asp:ListItem>
                                                        <asp:ListItem Value="Value">Value</asp:ListItem>
                                                        <asp:ListItem Value="Quantity">Quantity</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Incentive Setup Details  </h3>
                        </div>
                        <div class="widget-body" id="detailsDiv">
                            <div class="row">

                                <asp:Panel runat="server" ID="showValue" Visible="True">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Start value</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtStartValue" CssClass="form-control" onkeyup="javascript:validationForQuantity();" Skin="Metro" Width="220px" onblur="javascript:Calculation();" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">End Value</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtEndValue" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="clearfix"></div>
                                <asp:Panel runat="server" ID="showQuantity" Visible="False">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Start Quantity</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtStartQuantity" CssClass="form-control" onkeyup="javascript:validationForQuantity();" Skin="Metro" Width="220px" onblur="javascript:Calculation();" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">End Quantity</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtEndQuantity" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Percentage</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtPercentage" Skin="Metro" Width="220px" onkeyup="javascript:validationForDiscount();" Text="0" runat="server" onblur="javascript:CalculationDiscount();" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Value</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtValue" Skin="Metro" Width="220px" runat="server" Height="35px" EnableViewState="true" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                                <div class="col-md-1"></div>
                                <div class="col-md-11">
                                    <div class="form-group">
                                        <label class="=col-md-4 control-label" style="padding-top: 7px; text-align: right;"></label>
                                        <div class="col-md-8">
                                            <asp:Button ID="btnAddIncentiveDetails" runat="server" Text="Add Incentive Details" OnClick="btnAddIncentiveDetails_Click" CssClass="btn btn-inverse pull-right" />
                                        </div>
                                    </div>
                                    <asp:Label runat="server" ID="lblsource" Visible="False" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                        <telerik:RadScriptManager runat="server" ID="RadScriptManager2" />
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="RadGridIncentiveSetupinfo" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True"
                                    OnPageIndexChanged="RadGridIncentiveSetupinfo_OnPageIndexChanged" OnPageSizeChanged="RadGridIncentiveSetupinfo_PageSizeChanged"
                                    Skin="Metro" Width="100%" Height="550px" GridLines="None" OnItemCommand="RadGridIncentiveSetupInfo_ItemCommand">
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    </ClientSettings>
                                    <MasterTableView>
                                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                            <HeaderStyle Width="20px" Height="100px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                            <HeaderStyle Width="20px" Height="100px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Slno" FilterControlAltText="Filter column column" HeaderText="Slno" UniqueName="colSlno" Display="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="StartValue" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Start Value" UniqueName="colStartValue" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EndValue" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="End Value" UniqueName="colEndValue" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Percentage" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Percentage" UniqueName="colPercentage" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Value" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Value" UniqueName="colValue" Visible="true">
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
                                                ConfirmText="Are you sure you want to delete this record?"
                                                ConfirmDialogType="RadWindow"
                                                HeaderText="Delete"
                                                ConfirmTitle="Delete"
                                                ButtonType="ImageButton"
                                                ImageUrl="Images/delete.png"
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
                            </div>
                            <div class="row">
                                <div class="col-md-6 pull-left">
                                    <asp:Button ID="btnSave_OnClick" runat="server" Text="Save" OnClick="btnSave_OnClick_Click" CssClass="btn btn-inverse pull-left" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                    <asp:Button ID="btnClear_OnClick" runat="server" Text="Clear Info" OnClick="btnClear_OnClick_Click" CssClass="btn btn-warning pull-left" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                </div>
                                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblslno" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                            </div>
                        </div>
                        <asp:Label ID="lblDetails" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblPaymentId" runat="server" Visible="False"></asp:Label>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
