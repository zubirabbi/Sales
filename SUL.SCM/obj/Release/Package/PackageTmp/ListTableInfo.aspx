<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="ListTableInfo.aspx.cs" Inherits="SUL.SCM.ListTableInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
            </asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">List Information</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="strong">List Type</label>
                                <br/>
                                <asp:DropDownList ID="rdropListType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropListType_OnSelectedIndexChanged" OnDataBound="rdropListType_OnDataBound" CssClass="form-control" Skin="Metro" Width="303px" Font-Names="">
                                </asp:DropDownList>
                                <div class="separator bottom"></div>

                                <label class="strong">List Value</label>
                                <br/>
                                <telerik:RadTextBox ID="rtxtListValue" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                <div class="separator bottom"></div>


                            </div>
                            <div class="col-md-6">
                                <label class="strong">List Serial No</label>
                                <br/>
                                <telerik:RadTextBox ID="rtxtListId" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" Enabled="False" ViewStateMode="Enabled" LabelWidth="121px" Resize="None"></telerik:RadTextBox>
                                <div class="separator bottom"></div>

                                <asp:CheckBox runat="server" ID="chkIsActive" Checked="True"/>
                                <label class="strong" style="margin-top: 30px;">Is Active</label>
                            </div>
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
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">List Table List</h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="RadGridListTable" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnPageIndexChanged="RadGridListTable_OnPageIndexChanged" OnPageSizeChanged="RadGridListTable_OnPageSizeChanged" Skin="Metro" Width="90%" GridLines="None" OnItemCommand="RadGridListTable_OnItemCommand">
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
                                                HeaderText=""
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
                                            <telerik:GridBoundColumn DataField="ListType" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="List Type" UniqueName="colListType" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ListId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="List Serial No" UniqueName="colListId" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ListValue" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="List Value" UniqueName="colListValue" Visible="true">
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
    </div>
</asp:Content>