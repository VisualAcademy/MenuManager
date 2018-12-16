<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuMangerPage.aspx.cs" Inherits="MenuManager.Web.ManuMangerPage" %>

<%@ Register Src="~/MenuManagerUserControl.ascx" TagPrefix="uc1" TagName="MenuManagerUserControl" %>
<%@ Register Src="~/MenuSidebarUserControl.ascx" TagPrefix="uc1" TagName="MenuSidebarUserControl" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>메뉴 관리자</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:MenuManagerUserControl runat="server" ID="MenuManagerUserControl" />
            <hr />
            <uc1:MenuSidebarUserControl runat="server" id="MenuSidebarUserControl" />
        </div>
    </form>
</body>
</html>
