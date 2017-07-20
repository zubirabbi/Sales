<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="ListSetupPage.aspx.cs" Inherits="SUL.SCM.ListSetupPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">List setup Information </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">List Name</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropListType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropListType_OnSelectedIndexChanged" Skin="Metro" OnDataBound="rdropListType_OnDataBound"></telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Value</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtValue" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <br/>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 pull-left">
                                <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left"/>
                                <div style="float: left; height: 3px; width: 10px;"></div>
                                <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left"/>
                                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lbllstId" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">List setup List </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="RadGridListType" runat="server" AutoGenerateColumns="False" CssClass="gridCulture" CellSpacing="0" AllowPaging="True" Skin="Metro" Width="96%" GridLines="None" OnPageIndexChanged="RadGridListType_OnPageIndexChanged" OnPageSizeChanged="RadGridListType_OnPageSizeChanged" OnItemCommand="RadGridListType_OnItemCommand">
                                    <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" ReorderColumnsOnClient="True">
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
                                            <telerik:GridBoundColumn DataField="ListType" FilterControlAltText="Filter column column" HeaderText="List Type" UniqueName="colListType" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ListId" FilterControlAltText="Filter column column" HeaderText="List Id" UniqueName="colListId" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ListValue" FilterControlAltText="Filter column column" HeaderText="List Value" UniqueName="collistvalue" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn
                                                CommandName="btnSelect"
                                                HeaderText=""
                                                SortExpression=""
                                                Text="Select"
                                                ButtonType="LinkButton">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn
                                                CommandName="btnDelete"
                                                HeaderText=""
                                                SortExpression=""
                                                Text="Delete"
                                                ButtonType="LinkButton">
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
    </div>
</asp:Content>