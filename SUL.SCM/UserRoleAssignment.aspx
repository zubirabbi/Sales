<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserRoleAssignment.aspx.cs" Inherits="SUL.SCM.UserRoleAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#CheckAllOn").click(function () {
                alert("ALL");
                if (this.click) {
                    $('.check').each(function () {
                        this.checked = true;
                    });
                } else {
                    $('.check').each(function () {
                        this.checked = false;
                    });
                }
            });
        });
    </script>
    <div id="content">

        <div class="innerAll spacing-x2">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Role Assignment </h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="text-align: right; padding-top: 7px;">Role/User Type</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="rdeopType" runat="server" CssClass="form-control" Skin="Metro" Width="303px" AutoPostBack="True" OnSelectedIndexChanged="rdeopType_OnSelectedIndexChanged" Font-Names="">
                                            <asp:ListItem Value="SelectOne">---Select One---</asp:ListItem>
                                            <asp:ListItem Value="Role">Role</asp:ListItem>
                                            <asp:ListItem Value="Users">Users</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="text-align: right; padding-top: 7px;">Role/User Name</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="rdropList" CssClass="form-control" runat="server" Width="220px" AutoPostBack="True" Skin="Metro" OnSelectedIndexChanged="rdropList_OnSelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="text-align: right; padding-top: 7px;">dasdasdasdasdasd</label>
                                    <div class="col-md-8">
                                        <button type="button" return="false" name="CheckAllOn" id="CheckAllOn" class="btn btn-primary"><i class="fa fa-check-circle"></i>Check All Below</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Role Assignment</h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="RadGridAppFunction" runat="server" AllowFilteringByColumn="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" ShowGroupPanel="True" Skin="Metro" Width="90%">
                                    <MasterTableView>
                                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="PermitionId" FilterControlAltText="Filter column column" HeaderText="Permission Id" UniqueName="colPerId" Display="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Functionality" HeaderText="Functionality Name" ColumnEditorID="GridTextBoxEditor">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="IsInsert" HeaderText="Save" ColumnEditorID="GridTextBoxEditor">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsInsert" runat="server" Checked='<%#Eval("IsInsert") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="IsUpdate" HeaderText="Update" ColumnEditorID="GridTextBoxEditor">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsUpdate" runat="server" Checked='<%#Eval("IsUpdate") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="IsDelete" HeaderText="Delete" ColumnEditorID="GridTextBoxEditor">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsDelete" runat="server" Checked='<%#Eval("IsDelete") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="IsView" HeaderText="View" ColumnEditorID="GridTextBoxEditor">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsView" runat="server" Checked='<%#Eval("IsView") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="IsApprove" HeaderText="Approve" ColumnEditorID="GridTextBoxEditor">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsApprove" runat="server" Checked='<%#Eval("IsApprove") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>

                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>

                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </ExpandCollapseColumn>

                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                                        </EditFormSettings>

                                        <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                    </MasterTableView>

                                    <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

                                    <FilterMenu EnableImageSprites="False"></FilterMenu>
                                </telerik:RadGrid>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 pull-left">
                                <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left" />
                                <div style="width: 10px; height: 3px; float: left"></div>
                                <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left" />
                            </div>
                            <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
