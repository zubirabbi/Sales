<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RequisitionHistory.aspx.cs" Inherits="SUL.SCM.RequisitionHistory" %>
<%@ Register TagPrefix="a" Namespace="Telerik.Web.UI.Gantt" Assembly="Telerik.Web.UI, Version=2015.1.225.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
    <script type="text/javascript">
        function GoToToday(controlId) {
            var datepicker = null;
            if (controlId === "1") {
                datepicker = window.$find("<%=dtfromDate.ClientID%>");
            }
            else {
                datepicker = window.$find("<%=dttoDate.ClientID%>");
            }
            var dt = new Date();
            datepicker.set_selectedDate(dt);
            datepicker.hidePopup();
        }
    </script>

    <div id="content">
        <div class="innerAll">
            <div class="row">
                <div class="widget widget-inverse">
                    <div class="widget-head">
                        <h3 class="heading">Requisition Search </h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">From Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="dtfromDate" runat="server" Width="225px" Height="30px" Culture="en-US" MinDate="1950-01-01">

                                                <Calendar ID="Calendar1" runat="server">
                                                    <FooterTemplate>
                                                        <div style="width: 100%; text-align: center; background-color: Gray;">
                                                            <input id="Button1" type="button" value="Today" onclick="GoToToday('1');" />
                                                        </div>
                                                    </FooterTemplate>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">To Date</label>
                                        <div class="col-md-8">
                                            <telerik:RadDatePicker ID="dttoDate" runat="server" Width="225px" Height="30px" Culture="en-US" MinDate="1950-01-01">

                                                <Calendar ID="Calendar2" runat="server">
                                                    <FooterTemplate>
                                                        <div style="width: 100%; text-align: center; background-color: Gray;">
                                                            <input id="Button1" type="button" value="Today" onclick="GoToToday('2');" />
                                                        </div>
                                                    </FooterTemplate>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Region</label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="rdropRegion" runat="server" CssClass="form-control" Skin="Metro" Width="220px" AutoPostBack="True" OnSelectedIndexChanged="rdropRegion_OnSelectedIndexChanged" Font-Names="" OnDataBound="rdropRegion_OnDataBound">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Area</label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="rdropArea" runat="server" CssClass="form-control" Skin="Metro" Width="220px" AutoPostBack="True" Font-Names="" OnDataBound="rdropArea_OnDataBound" OnSelectedIndexChanged="rdropArea_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Dealer</label>
                                        <div class="col-md-8">
                                            <telerik:RadComboBox ID="rdropPDealer" runat="server" Skin="Metro" Width="220px" AutoPostBack="True" Font-Names="" OnDataBound="rdropPDealer_OnDataBound" AllowCustomText="True" DropDownWidth="300px" Filter="Contains" MarkFirstMatch="True">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
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
                        <h3 class="heading">Requisition Information List</h3>
                    </div>
                    <div class="widget-body">
                        <div class="row">
                            <telerik:RadGrid ID="RadGridRequisition" runat="server" AutoGenerateColumns="False" CellSpacing="0" AllowPaging="True"
                                OnPageIndexChanged="RadGridRequisition_OnPageIndexChanged" OnPageSizeChanged="RadGridRequisition_OnPageSizeChanged"
                                Skin="Metro" Width="100%" GridLines="None">
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <Scrolling AllowScroll="False" UseStaticHeaders="True" />
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
                                                <asp:Label runat="server" ID="lblRowNumber" Text="<%# Container.DataSetIndex + 1 %>"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter column column" HeaderText="Id" UniqueName="colId" Display="false">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DealerId" FilterControlAltText="Filter column column" HeaderText="DealerId" UniqueName="colDealerId" Display="false">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RequisitionCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Requisition Code" UniqueName="colRequisitionCode" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RequisitionDate" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Requisition Date" DataFormatString="{0:MMM dd, yyyy}" UniqueName="colRequisitionDate" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                       
                                        <telerik:GridBoundColumn DataField="DealerCode" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Code" UniqueName="colDealerCode" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DealerName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Dealer Name" UniqueName="colDealerName" Visible="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="RequistionTotal" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Total Price" UniqueName="colRequistionTotal" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PaymentAmount" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Payment Amount" UniqueName="colPaymentAmount" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReferenceNo" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Reference No" UniqueName="colReferenceNo" Visible="true">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeName" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Employee Name" UniqueName="colEmployeeName" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UpdateBy" FilterControlAltText="Filter column column" ItemStyle-HorizontalAlign="Left" HeaderText="Update By" UniqueName="colUpdateBy" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Left" AllowFiltering="False" HeaderStyle-Width="100px" ColumnEditorID="GridTextBoxEditor" UniqueName="colIsActive">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IsInvoiceCreated" HeaderText="Status" ItemStyle-HorizontalAlign="Left" AllowFiltering="False" HeaderStyle-Width="100px" ColumnEditorID="GridTextBoxEditor" UniqueName="colIsInvoiceCreated" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" AllowFiltering="False" HeaderStyle-Width="100px" ColumnEditorID="GridTextBoxEditor" UniqueName="colRemarks" Display="True">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CampaignId" HeaderText="CampaignId" ItemStyle-HorizontalAlign="Left" AllowFiltering="False" HeaderStyle-Width="100px" ColumnEditorID="GridTextBoxEditor" UniqueName="colCampaignId" Display="False">
                                            <ColumnValidationSettings>
                                                <ModelErrorMessage Text=""></ModelErrorMessage>
                                            </ColumnValidationSettings>
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </telerik:GridBoundColumn>


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
                        <div class="row">
                            <div class="col-md-6 pull-left">
                                <asp:Button ID="btnPrintAll" runat="server" Text="Print" OnClick="btnPrintAll_OnClick" CssClass="btn btn-primary pull-left" />
                                <div style="float: left; height: 3px; width: 10px;"></div>
                                <asp:Button ID="btnExcel" runat="server" Text="Expot to Excel" OnClick="btnExcel_OnClick" CssClass="btn btn-primary pull-left" />
                                <div style="float: left; height: 3px; width: 10px;"></div>
                                <%--<asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left" />--%>
                            </div>
                            <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblsearchBtn" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblsource" runat="server" Visible="False"></asp:Label>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
