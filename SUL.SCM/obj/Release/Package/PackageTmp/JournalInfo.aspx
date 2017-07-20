<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="JournalInfo.aspx.cs" Inherits="SUL.SCM.JournalInfo" %>

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
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Journal Information </h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Journal Id</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtJournalId" CssClass="form-control" Skin="Metro" Enabled="False" Width="220px" runat="server" Height="30px"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Date</label>
                                    <div class="col-md-8">
                                        <telerik:RadDatePicker ID="rdtDate" runat="server" Width="220px" Height="30px" Culture="en-US" MinDate="1950-01-01"></telerik:RadDatePicker>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Type</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropType" runat="server" Skin="Metro" Width="220px" OnDataBound="rdropType_OnDataBound" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Contra Account</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtContraAccount" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="30px"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Effect</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="rdropEfhect" runat="server" CssClass="form-control" Skin="Metro" Width="220px" Font-Names="">
                                            <asp:ListItem Value="SelectOne">---Select One---</asp:ListItem>
                                            <asp:ListItem Value="Debit">Debit</asp:ListItem>
                                            <asp:ListItem Value="Credit">Credit</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>

                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Description</label>
                                    <div class="col-md-8">
                                        <textarea class="form-control" runat="server" id="rtxtDescription" rows="3" style="width: 220px"></textarea>
                                    </div>
                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>

                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Journal Details</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer</label>
                                    <div class="col-md-8">
                                        <telerik:RadComboBox ID="rdropDealer" runat="server" AutoPostBack="True" Skin="Metro" OnDataBound="rdropDealer_OnDataBound" OnSelectedIndexChanged="rdropDealer_OnSelectedIndexChanged" Width="220px" Font-Names="" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                        </telerik:RadComboBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Current Balance</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtCurrentBalance" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="30px"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Debit</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtDebit" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="30px"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Credit</label>
                                    <div class="col-md-8">
                                        <telerik:RadTextBox ID="rtxtCredit" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="30px"></telerik:RadTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Description</label>
                                    <div class="col-md-8">
                                        <textarea class="form-control" runat="server" id="rtxtDescription2" rows="3" style="width: 220px"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <div class="form-group">
                                    <asp:Button ID="btnAddJournalInfo" runat="server" Text="Add Journal Info" CssClass="btn btn-inverse pull-right" OnClick="btnAddJournalInfo_OnClick" />
                                </div>
                            </div>
                            <div class="clearfix"></div>



                        </div>
                    </div>
                </div>
            </div>

            <div class="widget-body">
                <div class="row">
                    <telerik:RadGrid ID="RadGridJournalDetails" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowFilteringByColumn="True" AllowPaging="False" Skin="Metro" Width="100%" Height="300px" GridLines="None" PageSize="20" OnItemCommand="RadGridJournalDetails_OnItemCommand">
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
                                <telerik:GridTemplateColumn HeaderText="#" UniqueName="RowNumber">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRowNumber" Width="10px" Text="<%# Container.DataSetIndex + 1 %>"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                    <ColumnValidationSettings>
                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                    </ColumnValidationSettings>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DealerId" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colDealerId" Display="false">
                                    <ColumnValidationSettings>
                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                    </ColumnValidationSettings>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DealerCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Code" UniqueName="colDealerCode" AutoPostBackOnFilter="True" Visible="true">
                                    <ColumnValidationSettings>
                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                    </ColumnValidationSettings>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DealerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText=" Dealer Name" UniqueName="colDealerName" AutoPostBackOnFilter="True" Visible="true">
                                    <ColumnValidationSettings>
                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                    </ColumnValidationSettings>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Balance" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Balance" UniqueName="colBalance" AutoPostBackOnFilter="False" AllowFiltering="False" Display="True">
                                    <ColumnValidationSettings>
                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                    </ColumnValidationSettings>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Debit" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Debit" UniqueName="colDebit" AutoPostBackOnFilter="False" AllowFiltering="False" Visible="true">
                                    <ColumnValidationSettings>
                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                    </ColumnValidationSettings>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Credit" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Credit" UniqueName="colCredit" AutoPostBackOnFilter="False" AllowFiltering="False" Display="True">
                                    <ColumnValidationSettings>
                                        <ModelErrorMessage Text=""></ModelErrorMessage>
                                    </ColumnValidationSettings>
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn DataField="Description" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Description" UniqueName="colDescription" Visible="True">
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
                                <%-- <telerik:GridButtonColumn
                                            CommandName="btnDelete"
                                            ConfirmText="Are you sure you want to delete this record?"
                                            ConfirmDialogType="RadWindow"
                                            HeaderText="Delete"
                                            ConfirmTitle="Delete"
                                            ButtonType="ImageButton"
                                            ImageUrl="Images/delete.png"
                                            UniqueName="colDelete">
                                        </telerik:GridButtonColumn>--%>
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

            <div class="row">
                <div class="col-md-6 pull-left">
                    <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left" />
                    <div style="float: left; height: 3px; width: 10px;"></div>
                    <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left" />
                    <asp:Label runat="server" ID="lblImageName" Visible="False"></asp:Label>
                    <asp:Label runat="server" ID="lblId" Visible="False"></asp:Label>

                    <asp:Label runat="server" ID="lblisNewEntry" Visible="False"></asp:Label>
                    <asp:Label runat="server" ID="lblDetailsId" Visible="False"></asp:Label>
                    <asp:Label runat="server" ID="lblsource" Visible="False"></asp:Label>
                </div>

            </div>
        </div>


    </div>
</asp:Content>
