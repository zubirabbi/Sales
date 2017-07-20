<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="UserInformation.aspx.cs" Inherits="SUL.SCM.UserInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <style>
        .incpayment {
            margin-left: 20px;
        }

        .RadComboBox_Metro table td.rcbInputCellLeft {
            height: 30px;
        }
    </style>
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>

    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">User Information </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Employee Name</label>
                                            <div class="col-md-8">
                                                <telerik:RadComboBox ID = "rdropEmpName" runat="server" Skin="Metro"  Width="220px" Filter="Contains" MarkFirstMatch="True"  Font-Names="" DropDownWidth="300px" ViewStateMode="Enabled" ></telerik:RadComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">User Name</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtUserName" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Password</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtPassword" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Is Active</label>
                                            <div class="col-md-8">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="true"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">User Role</label>
                                        <div class="col-md-8">
                                                <div class="list-panel">
                                                    <telerik:RadListBox ID="lbRole" runat="server" CheckBoxes="True" ShowCheckAll="True"
                                                                        Skin="Metro" Width="250px" Height="300px" SelectionMode="Multiple">
                                                        
                                                    </telerik:RadListBox>
                                                </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
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
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Role List </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="dgvUser" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" AllowPaging="True" OnPageIndexChanged="dgvUser_PageIndexChanged" OnPageSizeChanged="dgvUser_PageSizeChanged" Skin="Metro" Width="98%" OnItemCommand="dgvUser_ItemCommand">
                                    <ClientSettings AllowColumnsReorder="false" AllowDragToGroup="false" ReorderColumnsOnClient="false">
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
                                            <telerik:GridBoundColumn DataField="UserName" FilterControlAltText="Filter column column" HeaderText="User Name" ItemStyle-HorizontalAlign="Left" UniqueName="colName">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UserPass" FilterControlAltText="Filter column column" HeaderText="Password" ItemStyle-HorizontalAlign="Left" UniqueName="colPass" Display="false">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmployeeId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Emp ID" UniqueName="colEmpId" Display="false">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmployeeName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Employee Ref" UniqueName="colEmployee">
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
                                                ImageUrl="Images/delete.png">
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                            </EditColumn>
                                        </EditFormSettings>
                                        <PagerStyle PageSizeControlType="RadComboBox" Position="Bottom" Mode="NextPrevAndNumeric"></PagerStyle>
                                    </MasterTableView>
                                    <FilterMenu EnableImageSprites="False">
                                    </FilterMenu>
                                    <HeaderStyle Width="100px" BackColor="#FF672E" BorderStyle="None" BorderWidth="0px" Font-Size="Larger" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="gridHeader"/>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>