<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SpairPartsDeliveryList.aspx.cs" Inherits="SUL.SCM.SpairPartsDeliveryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <style>
        .incpayment {
            margin-left: 20px;
        }

        .RadComboBox_Metro table td.rcbInputCellLeft {
            height: 30px;
        }
    </style>
    <script type="text/javascript">
        function ConfirmAction() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to change the approval status?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Spair Parts Delivery List</h3>
                    </div>
                    <asp:Label runat="server" ID="lblisNewEntry" Visible="False" />
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadgridSPDeliveryList" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True" OnPageIndexChanged="RadgridSPDeliveryList_OnPageIndexChanged" OnPageSizeChanged="RadgridSPDeliveryList_OnPageSizeChanged" Skin="Metro" Width="100%" Height="550px" GridLines="None" OnItemCommand="RadgridSPDeliveryList_OnItemCommand">
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                </ClientSettings>
                                <MasterTableView>
                                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                    <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                        <HeaderStyle Width="20px" Height="100px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                        <HeaderStyle Width="20px" Height="100px"></HeaderStyle>
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
                                        <telerik:GridBoundColumn DataField="TransactionCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Transaction Code" UniqueName="colTransactionCode" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SCName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Service Center" UniqueName="colSCName" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CreateDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Create Date" DataFormatString="{0:MMM dd, yyyy}" UniqueName="colCreateDate" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DeliveryDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Delivery Date" DataFormatString="{0:MMM dd, yyyy}" UniqueName="colDeliveryDate" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DeliveryMethod" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Delivery Method" UniqueName="colDeliveryMethod" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="Status" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Status" UniqueName="colStatus" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false" HeaderStyle-Width="50px" UniqueName="colEdit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" OnClick="btnEdit_OnClick" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="Images/Edit.png" Width="18px" Height="18px" ValidationGroup="button" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Approval" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkStatus" OnClick="lnkStatus_OnClick" OnClientClick=" ConfirmAction() "
                                                    CommandArgument='<%# Eval("Id") + ";" + Eval("Status") %>' runat="server">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("ImageStatus") %>' Width="30px" Height="30px" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                        </telerik:GridTemplateColumn>

                                        <%--  --%>

                                        <telerik:GridTemplateColumn HeaderText="Receive" AllowFiltering="false" UniqueName="colDelivery">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelivery" OnClick="btnDelivery_OnClick" CommandArgument='<%# Eval("Id") %>' runat="server">
                                                    <asp:Image ID="Image4" runat="server" ImageUrl='<%# Eval("DevileryImage") %>' Width="18px" Height="18px" ValidationGroup="button" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
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
                        <asp:Label runat="server" Visible="False" ID="lblsource"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
