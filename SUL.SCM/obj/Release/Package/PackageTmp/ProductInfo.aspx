<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="ProductInfo.aspx.cs" Inherits="SUL.SCM.ProductInfo" %>

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
                            <h3 class="heading">Product Information </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product Code</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtProductCode" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product Category</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropProductCat" runat="server" Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Base Unit</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropBaseunite" runat="server" Skin="Metro" Width="303px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product Name</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtProductName" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Model No</label>
                                        <div class="col-md-8">
                                            <telerik:RadTextBox ID="rtxtModelNo" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                            </div>
                        </div>
                    </div>

                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Official Information </h3>
                        </div>
                        <asp:Panel runat="server" ID="showProduct" Visible="False">
                            <div class="widget-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">M.R.P</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtMRP" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">F.O.B</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtCostPrice" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Current Balance</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtCurrentPrice" CssClass="form-control" Skin="Metro" Text="0" Width="303px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">D.P</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtDP" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">R.P</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtRP" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                        <asp:Panel ID="dp2Panel" runat="server" Visible="False">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">D.P 2</label>
                                                <div class="col-md-8">
                                                    <telerik:RadTextBox ID="rtxtDP2" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="showSpairParts">
                            <div class="widget-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Cost Price</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtSPCostPrice" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Sales Price</label>
                                            <div class="col-md-8">
                                                <telerik:RadTextBox ID="rtxtSalesPrice" CssClass="form-control" Skin="Metro" Width="303px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>

                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Product Color Information </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false" />
                                    <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel2">
                                        <div class="list-panel">
                                            <telerik:RadListBox runat="server" ID="RadListColor" Height="250px"
                                                AllowTransfer="true" TransferToID="RadListBoxDestination" Style="left: 0px; top: 0px; width: 300px">
                                            </telerik:RadListBox>
                                        </div>
                                    </telerik:RadAjaxPanel>
                                </div>
                                <div class="col-md-4">
                                    <div class="list-panel">
                                        <telerik:RadListBox runat="server" ID="RadListBoxDestination" Height="250px" Width="300px" AutoPostBack="True" OnDeleted="RadListBoxDestination_OnDeleted" AutoPostBackOnDelete="true" AutoPostBackOnReorder="true" AllowDelete="true">
                                        </telerik:RadListBox>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6 pull-left">
                            <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left" />
                            <div style="float: left; height: 3px; width: 10px;"></div>
                            <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left" />
                        </div>
                        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lbldeletedItemCount" runat="server" Visible="False"></asp:Label>
                    </div>

                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Product List</h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="RadGridProducr" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                    Skin="Metro" Width="100%" GridLines="None" OnItemCommand="RadGridProducr_OnItemCommand"
                                    AllowFilteringByColumn="True" Height="500px ">
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
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
                                            <telerik:GridBoundColumn DataField="ProductCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Code" UniqueName="colProductCode" AutoPostBackOnFilter="True" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ProductName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Product Name" UniqueName="colProductName" AutoPostBackOnFilter="True" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ModelNo" FilterControlAltText="Filter column column" AutoPostBackOnFilter="False" AllowFiltering="False" ItemStyle-HorizontalAlign="Left" HeaderText="Model No" UniqueName="colModelNo" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ProductCategory" FilterControlAltText="Filter column column" AutoPostBackOnFilter="False" AllowFiltering="False" ItemStyle-HorizontalAlign="Left" HeaderText="Product Category" UniqueName="colProductCategory" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CategoryCode" FilterControlAltText="Filter column column" AutoPostBackOnFilter="False" AllowFiltering="False" ItemStyle-HorizontalAlign="Left" HeaderText="Product Category" UniqueName="colCategoryCode">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="BaseUnit" FilterControlAltText="Filter column column" AutoPostBackOnFilter="False" AllowFiltering="False" ItemStyle-HorizontalAlign="Left" HeaderText="Base Unit" UniqueName="colBaseUnit" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UnitCode" FilterControlAltText="Filter column column" AutoPostBackOnFilter="False" AllowFiltering="False" ItemStyle-HorizontalAlign="Left" HeaderText="Base Unit" UniqueName="colunitCode">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MRP" FilterControlAltText="Filter column column" AutoPostBackOnFilter="False" AllowFiltering="False" ItemStyle-HorizontalAlign="Left" HeaderText="M.R.P." UniqueName="colMRP" Visible="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DP" FilterControlAltText="Filter column column" AutoPostBackOnFilter="False" AllowFiltering="False" ItemStyle-HorizontalAlign="Left" HeaderText="D.P." UniqueName="colDP">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DP2" FilterControlAltText="Filter column column" AutoPostBackOnFilter="False" AllowFiltering="False" ItemStyle-HorizontalAlign="Left" HeaderText="D.P. 2" UniqueName="colDP2">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RP" FilterControlAltText="Filter column column" AutoPostBackOnFilter="False" AllowFiltering="False" ItemStyle-HorizontalAlign="Left" HeaderText="R.P." UniqueName="colRP">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CostPrice" FilterControlAltText="Filter column column" AutoPostBackOnFilter="False" AllowFiltering="False" ItemStyle-HorizontalAlign="Left" HeaderText="Cost Price" UniqueName="colCostPrice" Visible="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CurrentBalance" FilterControlAltText="Filter column column" AutoPostBackOnFilter="False" AllowFiltering="False" ItemStyle-HorizontalAlign="Left" HeaderText="Current Balance" UniqueName="colCurrentBalance" Visible="True">
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
