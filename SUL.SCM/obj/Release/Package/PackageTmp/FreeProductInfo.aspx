<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="FreeProductInfo.aspx.cs" Inherits="SUL.SCM.FreeProductInfo" %>
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
                        <h3 class="heading">Free Product Information </h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropProduct" runat="server" AutoPostBack="True" OnDataBound="rdropProduct_OnDataBound" OnSelectedIndexChanged="rdropProduct_OnSelectedIndexChanged" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Type</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropType" runat="server" AutoPostBack="True" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
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
                        </div>
                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                    </div>
                </div>
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Area List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridArea" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True"
                                             OnPageIndexChanged="RadGridArea_OnPageIndexChanged" OnPageSizeChanged="RadGridArea_OnPageSizeChanged"
                                             Skin="Metro" Width="100%" GridLines="None" OnItemCommand="RadGridArea_OnItemCommand" AllowFilteringByColumn="True">
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
                                        <telerik:GridBoundColumn DataField="AreaName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Area Name" UniqueName="colAreaName" AutoPostBackOnFilter="True" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AreaCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Area Code" UniqueName="colAreaCode" AutoPostBackOnFilter="True" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Description" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="False" AllowFiltering="False" HeaderText="Description" UniqueName="colDescription" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RegionId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Region Id" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colRegionId" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RegionName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Region" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colRegionName" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmpName1" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Channel Specialized" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colEmployeeName" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ChanelSpecialities" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Channel Specialized" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colChanelSpecialities" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="JrChanelSpecialities" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Jr.Channel Specialized" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colJrChanelSpecialities" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmpName2" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Jr.Channel Specialized" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colJrChanelSpecialities" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IsActive" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="IsActive" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colIsActive">
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