﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewExp_TransactionStatus.aspx.cs" Inherits="Reports_EXPORTReports_ViewExp_TransactionStatus" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="300px" ShowParameterPrompts="false"
                Width="750px" ShowPrintButton="true">
            </rsweb:ReportViewer>--%>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="300px" ShowParameterPrompts="false"
                Width="800px" ShowPrintButton="true">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

