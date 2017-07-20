<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="SetupChannelDesignation.aspx.cs" Inherits="SUL.SCM.SetupChannelDesignation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Setup Channel Designation</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Channel Manager</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="rdropCM" CssClass="form-control" Skin="Metro" Width="303px" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Channel Specialized</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="rdropCS" runat="server" CssClass="form-control" Skin="Metro" Width="303px" Font-Names="" AutoPostBack="True" OnSelectedIndexChanged="rdropCS_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Assistant Channel Manager</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="rdropACM" CssClass="form-control" Skin="Metro" Width="303px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropACM_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Jr. Channel Specialized</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="rdropJrCS" runat="server" CssClass="form-control" Skin="Metro" Width="303px" Font-Names="" AutoPostBack="True" OnSelectedIndexChanged="rdropJrCS_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

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
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Designation List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridSetUpChennal" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" Skin="Metro" Width="100%" GridLines="None" OnItemCommand="RadGridSetUpChennal_OnItemCommand">
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
                                        <telerik:GridBoundColumn DataField="ChannelPosition" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Channel Position" UniqueName="colChannelPosition" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DesignationId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Designation Id" UniqueName="colDesignationId" Visible="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DesignationName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Designation Name" UniqueName="colDesignationName" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
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