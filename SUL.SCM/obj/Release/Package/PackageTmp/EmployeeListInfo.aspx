<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="EmployeeListInfo.aspx.cs" Inherits="SUL.SCM.EmployeeListInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Employee List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridEmpList" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowFilteringByColumn="True"
                                AllowPaging="True" OnPageIndexChanged="RadGridEmpList_OnPageIndexChanged" OnPageSizeChanged="RadGridEmpList_OnPageSizeChanged"
                                Skin="Metro" Width="100%" Height="550px" GridLines="None" OnItemCommand="RadGridEmpList_OnItemCommand" PageSize="20">
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
                                                <asp:Label runat="server" ID="lblRowNumber" Width="50px" Text="<%# Container.DataSetIndex + 1 %>"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Code" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Employee Code" UniqueName="colDepartmentNam" AutoPostBackOnFilter="True" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Employee Name" UniqueName="colEmployeeName" AutoPostBackOnFilter="True" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DepartmentName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Department Name" UniqueName="colDepartmentName" AutoPostBackOnFilter="False" AllowFiltering="False" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DesignationName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Designation Name" UniqueName="colDesignationName" AutoPostBackOnFilter="False" AllowFiltering="False" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="JoinDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Date Of Join" UniqueName="colJoinDate" AutoPostBackOnFilter="False" AllowFiltering="False" DataFormatString="{0:MMM dd, yyyy}" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="CompanyId" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="CompanyId" UniqueName="colCompanyId" Visible="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" OnClick="btnEdit_OnClick" CommandArgument='<%# Eval("Id") + ";" + Eval("CompanyId") %>' runat="server">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="Images/Edit.png" Width="18px" Height="18px" ValidationGroup="button" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
