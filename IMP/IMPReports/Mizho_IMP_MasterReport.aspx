﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mizho_IMP_MasterReport.aspx.cs" Inherits="Reports_Mizho_IMP_MasterReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="formrdata" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="500px" ShowParameterPrompts="false"
                Width="1090px" ShowPrintButton="true" ShowRefreshButton="true" SizeToReportContent="true">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
