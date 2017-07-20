<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="SupplierInfo.aspx.cs" Inherits="SUL.SCM.SupplierInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
</telerik:RadStyleSheetManager>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
</telerik:RadAjaxManager>
<telerik:RadScriptManager runat="server" ID="RadScriptManager1"/>
<style>
    .RadComboBox_Metro table td.rcbInputCellLeft { height: 30px; }
</style>
<div id="content">
<div class="innerAll">
<div class="row">
<div class="widget widget-inverse">
<div class="widget-head">
    <h3 class="heading">Supplier Information</h3>
</div>
<div class="widget-body">
<div class="widget widget-tabs widget-tabs-double widget-tabs-gray">
<telerik:RadTabStrip ID="RadTabStrip1" runat="server" Width="100%" SelectedIndex="0" MultiPageID="rmpSupplierInfo" Font-Size="Medium">
    <Tabs>
        <telerik:RadTab runat="server" Text="Supplier Information" Value="1" SelectedIndex="0" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Product Tagging" Value="2" SelectedIndex="1">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Bank Information" Value="3" SelectedIndex="2">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="rmpsupplierInfo" runat="server" SelectedIndex="0" BackColor="#FFFFFF" BorderStyle="Solid" BorderWidth="1px" Width="100%">
<div class="tab-content">

<!-- Step 1 -->
<telerik:RadPageView ID="rmpInfo" runat="server" Width="100%" Selected="True">
    <br/>
    <div class="row">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Name</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtName" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Company Adderss</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtComAddress" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Contact Person</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtContractPerson" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Phone Number</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtPhone" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Email</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtEmail" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Code</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtCode" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Factory Address</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtFactory" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Designation</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtDesignation" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>

            <div class="clearfix"></div>
            <div class="col-md-6">
                <div class="form-group">
                    <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Mobile</label>
                    <div class="col-md-8">
                        <telerik:RadTextBox ID="rtxtMobile" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <asp:Button ID="btnSave" runat="server" Text="Save Info" OnClick="btnSave_OnClick" CssClass="btn btn-inverse pull-left"/>
            <div style="float: left; height: 3px; width: 10px;"></div>
            <asp:Button ID="btnClear" runat="server" Text="Clear Info" OnClick="btnClear_OnClick" CssClass="btn btn-warning pull-left"/>
        </div>
        <asp:Label ID="lblId" runat="server" Visible="False"></asp:Label>
    </div>

</telerik:RadPageView>
<!-- Step 2 -->
<telerik:RadPageView ID="rmpProductTagging" runat="server" Width="100%">
    <br/>
    <div class="row">
        <div class="col-md-12">
            <label class="strong">Product Category</label>
            <br/>
            <asp:DropDownList ID="rdropProductCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdropProductCat_OnSelectedIndexChanged" CssClass="form-control" Skin="Metro" Width="220px" Font-Names="">
            </asp:DropDownList>
            <div class="separator bottom"></div>
        </div>
        <div class="col-md-4">
            <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All" EnableRoundedCorners="false"/>
            <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel2">
                <div class="list-panel">
                    <telerik:RadListBox runat="server" ID="RadListProduct" Height="250px"
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
    <div class="row">
        <div class="col-md-6 pull-left">
            <asp:Button ID="btnsaveProductTag" OnClick="btnsaveProductTag_OnClick" runat="server" Text="Save Info" CssClass="btn btn-inverse pull-left"/>
            <div style="float: left; height: 3px; width: 10px;"></div>
            <asp:Button ID="btnClearProductTag" OnClick="btnClearProductTag_OnClick" runat="server" Text="Clear Info" CssClass="btn btn-warning pull-left"/>
        </div>
    </div>
</telerik:RadPageView>
<!-- Step 3 -->
<telerik:RadPageView ID="rmpSupplierBankInfo" runat="server" Width="100%">
    <br/>
    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Bank Name</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtBankName" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Account No</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtAccOn" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">SWIFT Code</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtSWIFTCode" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Country</label>
                <div class="col-md-8">
                    <asp:DropDownList ID="rdropCountry" runat="server" CssClass="form-control" Skin="Metro" Width="220px" Font-Names="">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Account Title</label>
                <div class="col-md-8">
                    <telerik:RadTextBox ID="rtxtAccTitle" CssClass="form-control" Skin="Metro" Width="220px" runat="server" Height="35px" ViewStateMode="Enabled"></telerik:RadTextBox>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label class="col-md-4 control-label" style="padding-top: 7px; text-align: right;">Is Default</label>
                <asp:CheckBox ID="chkIsDefault" runat="server"/>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
    <div class="row">
        <div class="col-md-6 pull-left">
            <asp:Button ID="btnSaveSuppBank" OnClick="btnSaveSuppBank_OnClick" runat="server" Text="Save Info" CssClass="btn btn-inverse pull-left"/>
            <div style="float: left; height: 3px; width: 10px;"></div>
            <asp:Button ID="btnClearSuppBank" OnClick="btnClearSuppBank_OnClick" runat="server" Text="Clear Info" CssClass="btn btn-warning pull-left"/>
            <asp:Label ID="lblbankId" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblisNewEntry" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblisNewEntryForProductTagging" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblisNewEntryForSupplierBank" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lbldeletedItemCount" runat="server" Visible="False"></asp:Label>
        </div>
    </div>
</telerik:RadPageView>
</div>
</telerik:RadMultiPage>
</div>
</div>
</div>
</div>
</div>
</div>
</asp:Content>