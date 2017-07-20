<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="CampaignSetup.aspx.cs" Inherits="SUL.SCM.CampaignSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Campaign Information </h3>
                    </div>
                    <div class="widget-body">
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Width="100%" SelectedIndex="0" Skin="Bootstrap" MultiPageID="rmpCampaignSetup" CssClass="RadTabStripTop_Metro" Font-Size="15px" Font-Family=" GE SS Text Light">
                            <Tabs>
                                <telerik:RadTab runat="server" class="glyphicons camera active" Text="Campaign Information" Value="1" SelectedIndex="0" Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" class="glyphicons camera active" Text="Campaign Details" Value="2" SelectedIndex="1">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="rmpCampaignSetup" runat="server" SelectedIndex="0" BackColor="#FFFFFF" BorderStyle="Solid" BorderWidth="1px" Width="100%">
                            <!----General Information ---->
                            <telerik:RadPageView ID="rpvCampaignInformation" runat="server" Width="90%" Selected="True">
                                <div class="widget-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Campaign Code</label>
                                                <div class="col-md-8">
                                                    <telerik:RadTextBox ID="rtxtCampaignCode" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Campaign On</label>
                                                <div class="col-md-8">
                                                    <telerik:RadComboBox ID="rdropCampaignType" runat="server" AutoPostBack="True" Skin="Metro" Width="220px" OnSelectedIndexChanged="rdropCampaignType_OnSelectedIndexChanged" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="Product" Value="Product" />
                                                            <telerik:RadComboBoxItem runat="server" Text="Value" Value="Value" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Start Date</label>
                                                <div class="col-md-8">
                                                    <telerik:RadDatePicker ID="rdtStartDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">End Date</label>
                                                <div class="col-md-8">
                                                    <telerik:RadDatePicker ID="rdtEndDate" runat="server" Width="220px" Height="30px"></telerik:RadDatePicker>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Region</label>
                                                <div class="col-md-8">
                                                    <asp:CheckBox ID="chkIsExclude" runat="server" Text="Excluded from Incentive" />
                                                    <br />
                                                    <asp:CheckBox ID="chkIsAdjusted" runat="server" Text="Adjusted After End" />
                                                    <br />
                                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" Text="Is Active" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Description</label>
                                                <div class="col-md-8">
                                                    <textarea id="rtxtDescription" cols="1" rows="3" runat="server" style="width: 220px"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Region</label>
                                                <div class="col-md-8">
                                                    <telerik:RadComboBox ID="rdropRegion" runat="server" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" OnDataBound="rdropRegion_OnDataBound" Filter="Contains" MarkFirstMatch="True">
                                                    </telerik:RadComboBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>

                                        <div class="col-md-6 pull-left">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left" />
                                            <div style="float: left; height: 3px; width: 10px;"></div>
                                            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_OnClick" CssClass="btn btn-inverse pull-left" Visible="False" />

                                        </div>
                                    </div>
                                </div>

                                <div class="widget-body" id="divDetailsGrid" runat="server" visible="False">
                                    <div class="row">
                                        <telerik:RadGrid ID="rgdCampaignDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                            Skin="Metro" Width="100%" Height="250px" GridLines="None" OnItemCommand="rgdCampaignDetails_OnItemCommand">
                                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                            </ClientSettings>
                                            <MasterTableView>
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" OnClick="btnEdit_OnClick" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="Images/Edit.png" Width="18px" Height="18px" ValidationGroup="button" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="#" UniqueName="RowNumber">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblRowNumber" Width="50px" Text="<%# Container.DataSetIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="Id" HeaderText="Id" UniqueName="colId" Display="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CampaignName" ItemStyle-HorizontalAlign="Left" HeaderText="Campaign Slab" UniqueName="colName" AutoPostBackOnFilter="False" AllowFiltering="False" Visible="true">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </telerik:RadPageView>

                            <!----Details ---->
                            <telerik:RadPageView ID="rpvDetails" runat="server">
                                <div class="widget widget-inverse" id="campDetails" runat="server" visible="True">
                                    <div class="widget-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Campaign Slab</label>
                                                    <div class="col-md-8">
                                                        <telerik:RadTextBox ID="rtxtCampaignName" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" Visible="true"></telerik:RadTextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <asp:Panel ID="ShowProductPanel" runat="server" Visible="False">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product Name</label>
                                                        <div class="col-md-8">
                                                            <telerik:RadComboBox ID="rdropProduct" runat="server" AutoPostBack="True" Skin="Metro" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True" OnSelectedIndexChanged="rdropProduct_OnSelectedIndexChanged" OnDataBound="rdropProduct_OnDataBound">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Price</label>
                                                        <div class="col-md-8">
                                                            <telerik:RadTextBox ID="rtxtPrice" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Quantity</label>
                                                        <div class="col-md-8">
                                                            <telerik:RadTextBox ID="rtxtStartQuantity" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="clearfix"></div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnAddMultiProduct" runat="server" Visible="True" OnClick="btnAddMultiProduct_OnClick" Text="Add Product" CssClass="btn btn-inverse pull-right" />
                                                        <div class="col-md-8">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>

                                                <div class="widget-body">
                                                    <telerik:RadGrid ID="RadGridAddProductCampaign" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" Height="150px" OnItemCommand="RadGridAddProductCampaign_OnItemCommand" Skin="Metro" Width="100%" GridLines="None" Visible="False">
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

                                                                <telerik:GridBoundColumn DataField="Id" HeaderText="Id" UniqueName="colId" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="ProductId" HeaderText="ProductId" UniqueName="colProductId" Display="false">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="ProductName" HeaderText="Product Name" UniqueName="colProductName" Display="True">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="StartQuantity" ItemStyle-HorizontalAlign="Right" HeaderText="Quantity" UniqueName="colStartQuantity" Display="True">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Price" ItemStyle-HorizontalAlign="Right" HeaderText="Price" UniqueName="colPrice" Visible="true">
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Amount" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" UniqueName="colAmount" Display="True">
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
                                                                    HeaderText=""
                                                                    SortExpression=""
                                                                    ButtonType="ImageButton"
                                                                    ImageUrl="Images/Delete.png"
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
                                                    <asp:Label ID="lblProductDetails" runat="server" Visible="False"></asp:Label>
                                                    <asp:Label ID="lblPaymentId" runat="server" Visible="False"></asp:Label>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="ShowValuePanel" runat="server" Visible="False">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Start Value</label>
                                                        <div class="col-md-8">
                                                            <telerik:RadTextBox ID="rtxtStartValue" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">End Value</label>
                                                        <div class="col-md-8">
                                                            <telerik:RadTextBox ID="rtxtEndvalue" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <div class="widget widget-inverse" id="campOffers">
                                    <div class="widget-head">
                                        <h3 class="heading">Campaign Offers</h3>
                                    </div>
                                    <div class="widget-body">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <div class="col-md-2">
                                                    <asp:CheckBox ID="chkDiscount" runat="server" AutoPostBack="True" Text="Discount" OnCheckedChanged="chkDiscount_OnCheckedChanged" />
                                                </div>
                                                <div class="col-md-6">
                                                    <telerik:RadTextBox ID="rtxtPercentage" CssClass="form-control" Skin="Metro" EmptyMessage="Percentage" Visible="False" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                                </div>
                                                <div class="col-md-4">
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="col-md-2">
                                                    <asp:CheckBox ID="chkAmount" runat="server" AutoPostBack="True" Text="Amount" OnCheckedChanged="chkAmount_OnCheckedChanged" />

                                                </div>
                                                <div class="col-md-6">
                                                    <telerik:RadTextBox ID="rtxtDiscountValue" CssClass="form-control" Skin="Metro" Visible="False" EmptyMessage="Value" Width="220px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>

                                                </div>
                                                <div class="col-md-4">
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="col-md-2">
                                                    <asp:CheckBox ID="chkGift" runat="server" AutoPostBack="True" Text="Gift" OnCheckedChanged="chkGift_OnCheckedChanged" />

                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product</label>
                                                        <div class="col-md-8">
                                                            <telerik:RadComboBox ID="rdropGiftProduct" runat="server" Skin="Metro" Width="160px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True" OnDataBound="rdropGiftProduct_OnDataBound">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="col-md-5">
                                                        <telerik:RadTextBox ID="rtxtGiftQuantity" CssClass="form-control" EmptyMessage="Quantity" Skin="Metro" Width="60px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-7">
                                                        <asp:Button ID="btnAddGiftProduct" runat="server" Text="Add" OnClick="btnAddGiftProduct_OnClick" CssClass="btn btn-inverse pull-left" />

                                                    </div>

                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="col-md-2">
                                                    <asp:CheckBox ID="chkFreeProduct" runat="server" AutoPostBack="True" Text="Free Product" OnCheckedChanged="chkFreeProduct_OnCheckedChanged" />
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Product</label>
                                                        <div class="col-md-8">
                                                            <telerik:RadComboBox ID="rdropFreeProduct" runat="server" Skin="Metro" Width="160px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True" OnDataBound="rdropFreeProduct_OnDataBound">
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="col-md-5">
                                                        <telerik:RadTextBox ID="rtxtFreeQuantity" CssClass="form-control" Skin="Metro" EmptyMessage="Quantity" Width="60px" runat="server" Height="35px" Enabled="True" ViewStateMode="Enabled"></telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-7">
                                                        <asp:Button ID="btnAddFreeProduct" runat="server" Text="Add" OnClick="btnAddFreeProduct_OnClick" CssClass="btn btn-inverse pull-left" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-4" style="margin-top: 40px;">
                                                <telerik:RadGrid ID="radgridCampnigeDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" Height="100px" OnItemCommand="radgridCampnigeDetails_OnItemCommand" Skin="Metro" Width="100%" GridLines="None" Visible="False">
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
                                                            <telerik:GridBoundColumn DataField="Id" HeaderText="Id" UniqueName="colId" Display="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ProductType" HeaderText="Type" UniqueName="colProdType" Display="True">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ProductId" HeaderText="ProductId" UniqueName="colProductId" Display="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ProductName" ItemStyle-HorizontalAlign="Left" HeaderText="Product" UniqueName="colProductName" Display="True">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Quantity" ItemStyle-HorizontalAlign="Left" HeaderText="Quantity" UniqueName="colQuantity" Visible="true">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridButtonColumn
                                                                CommandName="btnSelect"
                                                                HeaderText="Edit"
                                                                SortExpression=""
                                                                ButtonType="ImageButton"
                                                                ImageUrl="Images/Edit.png"
                                                                UniqueName="colEdit">
                                                            </telerik:GridButtonColumn>
                                                            <telerik:GridButtonColumn
                                                                CommandName="btnDelete"
                                                                HeaderText=""
                                                                SortExpression=""
                                                                ButtonType="ImageButton"
                                                                ImageUrl="Images/Delete.png"
                                                                UniqueName="colDelete">
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                        <EditFormSettings>
                                                        </EditFormSettings>
                                                        <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                                    </MasterTableView>
                                                    <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                                                    <FilterMenu EnableImageSprites="False">
                                                    </FilterMenu>
                                                </telerik:RadGrid>
                                                <asp:Label ID="lblCampaignDetails" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="col-md-6 pull-left">
                                            <div class="col-md-6">
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnSaveDetails" runat="server" Text="Save Details" OnClick="btnSaveDetails_OnClick" CssClass="btn btn-inverse pull-left" />

                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-inverse pull-left" />

                                                </div>

                                            </div>
                                            <div class="col-lg-6"></div>
                                            <div style="float: left; height: 3px; width: 10px;"></div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                                </div>


                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </div>
                </div>

            </div>
            <div class="row">

                <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblDP2" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
