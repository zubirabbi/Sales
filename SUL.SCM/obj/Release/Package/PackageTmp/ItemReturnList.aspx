<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ItemReturnList.aspx.cs" Inherits="SUL.SCM.ItemReturnList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
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
        function CancelConfirmAction() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure want to cancel the requisition?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <div id="content">
        <div class="wizard">
            <!-- // Widget heading END -->
            <div class="innerAll">
                <div class="row">
                    <div class="widget widget-inverse">
                        <div class="widget-head">
                            <h3 class="heading">Item Return List</h3>
                        </div>
                        <div class="widget-body">
                            <div class="row">
                                <telerik:RadGrid ID="RadGridItemReturn" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                    AllowPaging="True" OnPageIndexChanged="RadGridItemReturn_OnPageIndexChanged" OnPageSizeChanged="RadGridItemReturn_OnPageSizeChanged"
                                    Skin="Metro" Width="100%" Height="550px" GridLines="None" OnItemCommand="RadGridItemReturn_OnItemCommand">
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
                                            
                                            <telerik:GridBoundColumn DataField="ReturnDate" FilterControlAltText="Filter column column" DataFormatString="{0:MMM dd, yyyy}" ItemStyle-HorizontalAlign="Left" HeaderText="Return Date" UniqueName="colReturnDate" AutoPostBackOnFilter="False" AllowFiltering="False" Display="True">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DealerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Name" UniqueName="colDealerName" AutoPostBackOnFilter="False" AllowFiltering="False" Visible="true">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ItemTotal" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total Value" UniqueName="colItemTotal" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Status" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Status" UniqueName="colItemTotal" AutoPostBackOnFilter="False" AllowFiltering="False">
                                                <ColumnValidationSettings>
                                                    <ModelErrorMessage Text=""></ModelErrorMessage>
                                                </ColumnValidationSettings>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Approval" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkStatus" OnClick="lnkStatus_OnClick" OnClientClick=" ConfirmAction() "
                                                        CommandArgument='<%# Eval("Id") + ";" + Eval("Status") %>' runat="server">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("ImageStatus") %>' Width="30px" Height="30px" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px"></HeaderStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Receive" AllowFiltering="false" UniqueName="colDelivery">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnReceive" OnClick="btnReceive_OnClick" CommandArgument='<%# Eval("Id") + ";" + Eval("Status") %>' runat="server">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl='<%# Eval("ReceiveImage") %>' Width="18px" Height="18px" ValidationGroup="button" />
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
