<%@ Page Language="C#" CodeBehind="ReportTest.aspx.cs" Inherits="PdfReportTest.ReportTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnTest" Text="Show Basic Report" OnClick="btnTest_OnClick"/>
    </div>
    <br/>
    <div>
        <asp:Button runat="server" ID="btnSinglePageReport" Text="Show Single Page Report" OnClick="btnSinglePageReport_OnClick"/>
    </div>
    <br/>
    <div>
        <asp:Button runat="server" ID="btnInvoice" Text="Show Invoice" OnClick="btnInvoice_OnClick"/>
    </div>
</form>
</body>
</html>