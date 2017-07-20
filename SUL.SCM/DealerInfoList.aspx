<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="DealerInfoList.aspx.cs" Inherits="SUL.SCM.DealerInfoList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Dealer Search </h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropPDealer" runat="server" Skin="Metro" Width="220px" Font-Names="" OnDataBound="rdropPDealer_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 pull-left">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_OnClick" CssClass="btn btn-primary pull-left" />
                                    <div style="float: left; height: 3px; width: 10px;"></div>
                                    <%--<asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="widget-head">
                            <h3 class="heading">Dealer List</h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="RadGridDealer" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                    AllowPaging="True" OnPageIndexChanged="RadGridDealer_OnPageIndexChanged" OnPageSizeChanged="RadGridDealer_OnPageSizeChanged"
                                    Skin="Metro" Width="100%" Height="850px" GridLines="None" OnItemCommand="RadGridDealer_OnItemCommand" AllowFilteringByColumn="True">
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
                                            <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DealerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Name" UniqueName="colDealerName" AutoPostBackOnFilter="True" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DealerCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Code" UniqueName="colDealerCode" AutoPostBackOnFilter="True" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Area" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Area" UniqueName="colArea" AutoPostBackOnFilter="False" AllowFiltering="False" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AreaName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Area" UniqueName="colAreaName" AutoPostBackOnFilter="False" AllowFiltering="False" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Address" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Address" UniqueName="colAddress" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CS" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="CS" UniqueName="colCS" AutoPostBackOnFilter="False" AllowFiltering="False" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="JrCS" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="CS" UniqueName="colJrCS" AutoPostBackOnFilter="False" AllowFiltering="False" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmployeeName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="CS" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colCSName">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="JrEmpName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Jr.CS" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colJrCSName">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ProprietorName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="ProprietorName" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colProprietorName">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Phone" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Phone" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colPhone">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Mobile" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Mobile" UniqueName="colMobile" AutoPostBackOnFilter="False" AllowFiltering="False" Visible="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Email" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colEmail">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DealerCategory" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Category" Display="False" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colDealerCategory">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CategoryCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Category" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colDealerCategoryCode">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="StartDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Start Date" UniqueName="colStartDate" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="False" AllowFiltering="False" Visible="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="WareHouseCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Ware House Code" UniqueName="colWareHouseCode" AutoPostBackOnFilter="False" AllowFiltering="False" Display="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IsActive" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Is Active" AutoPostBackOnFilter="False" AllowFiltering="False" UniqueName="colIsActive" Visible="True">
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
