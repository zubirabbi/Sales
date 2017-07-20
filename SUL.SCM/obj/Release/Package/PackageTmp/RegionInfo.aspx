<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="RegionInfo.aspx.cs" Inherits="SUL.SCM.RegionInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <style>
        .RadComboBox_Metro table td.rcbInputCellLeft { height: 30px; }
    </style>
    <div id="content">
        <h1 class="content-heading bg-white border-bottom">Region Information</h1>
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Region Info </h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Region Name</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtRegionName" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">

                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Region Code</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtRegionCode" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Description</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtDesc" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Channel Manager</label>
                                <div class="col-md-8">
                                    <telerik:RadComboBox ID="rdropCM" runat="server"  Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                    </telerik:RadComboBox>

                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Assistant Channel Manager</label>
                                <div class="col-md-8">
                                    <telerik:RadComboBox ID="rdropACM" runat="server"  Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                    </telerik:RadComboBox>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Is Active</label>
                                    <div class="col-md-8">
                                        <asp:CheckBox runat="server" ID="chkIsActive" Checked="True"/>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 pull-left">
                                <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left"/>
                                <div style="float: left; height: 3px; width: 10px;"></div>
                                <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left"/>
                            </div>
                            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Region List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridRegion" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True"
                                             OnPageIndexChanged="RadGridRegion_OnPageIndexChanged" OnPageSizeChanged="RadGridRegion_OnPageSizeChanged"
                                             Skin="Metro" Width="100%" GridLines="None" OnItemCommand="RadGridRegion_OnItemCommand" AllowFilteringByColumn="True">
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"/>
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
                                        <telerik:GridButtonColumn
                                            CommandName="btnSelect"
                                            HeaderText="Edit"
                                            SortExpression=""
                                            ButtonType="ImageButton"
                                            ImageUrl="Images/Edit.png"
                                            UniqueName="colEdit">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RegionName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Region Name" UniqueName="colRegionName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RegionCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Region Code" AutoPostBackOnFilter="True" UniqueName="colRegionCode" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Description" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Description" AutoPostBackOnFilter="True" UniqueName="colDescription" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Channel Manager" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colEmployeeName" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ChanelManager" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Chanel Manager" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colChanelManager" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AssistantChanelManager" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Assistant Chanel Manager" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colAssChanelManager" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmpName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Assistant Chanel Manager" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colAssChanelManagers" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IsActive" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="False" AllowFiltering="False" HeaderText="IsActive" UniqueName="colIsActive">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

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
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>