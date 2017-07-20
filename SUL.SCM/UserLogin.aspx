<%@ Page Language="C#" CodeBehind="UserLogin.aspx.cs" Inherits="SUL.SCM.UserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>User Login</title>

    <link type="text/css" rel="stylesheet" href="Styles/login.css"/>
</head>
<body>
<div id="container">
    <div id="log_header">
        <div id="log_title">Welcome to Elite ERP</div>
        <div style="padding-bottom: 10px;">
            <img src="images/logo_elite.png"/>
        </div>

        <form id="login" runat="server">
            <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
            </telerik:RadStyleSheetManager>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                <Scripts>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
                </Scripts>
            </telerik:RadScriptManager>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            </telerik:RadAjaxManager>
            <div id="Div1"><asp:Label runat="server" ID="lblText" Text=""></asp:Label></div>
            <div id="login_text">Login</div>
            <div id="responseText" style="display: none; margin: 0 0 5px 0; padding: 5px; word-wrap: break-word;"></div>
            <fieldset id="inputs" class="inputs">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="username" LabelWidth="64px" Resize="None" Width="350px">
                </asp:TextBox>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="password" Width="350px">
                </asp:TextBox>
            </fieldset>
            <fieldset id="actions" class="actions">
                <input type="submit" name="submit" id="submit" value="Login" runat="server" onserverclick="submit_OnServerClick"/>
                <%--<telerik:RadButton ID="submit" runat="server"   Text="Login" Height="40px" Width="250px" Font-Weight="600" Font-Size="18px" ForeColor="#0f82c2" OnClick="submit_OnClick"></telerik:RadButton>--%>
            </fieldset>
            <div class="ajax_target">
                <!--don't delete this div class="ajax_target" -->
            </div>
            <div class="ajax_notify" style="clear: both; display: none;">
            </div>
            <span class="ajax_wait" style="display: none;">

                    <span class="ajax_spinner"></span>
                    <!--don't delete this span class="ajax_wait"-->
                </span>
        </form>
    </div>
</div>
</body>
</html>