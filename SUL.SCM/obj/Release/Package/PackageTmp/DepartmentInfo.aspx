<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="DepartmentInfo.aspx.cs" Inherits="SUL.SCM.DepartmentInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .RadComboBox_Metro table td.rcbInputCellLeft { height: 30px; }
    </style>
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Department Information </h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Department Name</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtDeptName" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Location</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtLocation" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">InCharge</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropInCharge" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Parent Department</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropPerentDept" runat="server"  Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Is Active</label>
                                    <div class="col-md-8" style="margin-top: 5px">
                                        <asp:CheckBox runat="server" ID="chkIsActive" Checked="True"/>
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
                            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="widget widget-inverse">
                <div class="widget-head">
                    <h3 class="heading">Department List</h3>
                </div>
                <div class="widget-body">
                    <div class="row">
                        <telerik:RadGrid ID="RadGridDept" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnPageIndexChanged="RadGridDept_OnPageIndexChanged" OnPageSizeChanged="RadGridDept_OnPageSizeChanged" Skin="Metro" Width="100%" GridLines="None" OnItemCommand="RadGridDept_OnItemCommand">
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
                                    <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DepartmentName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Department Name" UniqueName="colDepartmentName" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ParentDepartment" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Parent Department" UniqueName="colParentDepartment" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PerentDepartmentId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Department Id" UniqueName="colPerentDepartmentId" Display="False">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Incharge" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Incharge Name" UniqueName="colIncharge" Visible="true">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="InchargeId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Incharge Id" UniqueName="colInchargeId" Display="False">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Location" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Location" UniqueName="colLocation">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IsActive" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Is Active" UniqueName="colIsActive">
                                        <ColumnValidationSettings>
                                            <ModelErrorMessage Text=""></ModelErrorMessage>
                                        </ColumnValidationSettings>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CompanyId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="CompanyId" UniqueName="colCompanyId" Visible="False">
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>