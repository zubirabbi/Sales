<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" CodeBehind="ErrorPage.aspx.cs" Inherits="SUL.SCM.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container content">
        <!--Error Block-->


        <link rel="stylesheet" href="assets/css/page_error4_404.css">
        <%--       <link rel="stylesheet" href="assets/css/app.css">
        <link rel="stylesheet" href="assets/css/style.css">
        <link rel="stylesheet" href="assets/css/line-icons.css">
        <link rel="stylesheet" href="assets/css/default.css" id="style_color">
        <link rel="stylesheet" href="assets/css/custom.css">--%>
        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                <div class="error-v4" style="text-align: center">
                    <h1>SORRY</h1>
                    <span class="sorry">Yor are not  athorized to access this page!</span>
                    <div class="row">
                        <div class="col-md-6 col-md-offset-3">
                            <a class="btn btn-primary" href="HomePage.aspx">Go Back to Main Page</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>