<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_IDPMS_Checklist_OutwardRemitance.aspx.cs" Inherits="IDPMS_View_IDPMS_Checklist_OutwardRemitance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
     <form id="formrdata" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="580px"
    ShowParameterPrompts="false"  Width="95%" ShowPrintButton="true" ShowRefreshButton="true">
    </rsweb:ReportViewer>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
